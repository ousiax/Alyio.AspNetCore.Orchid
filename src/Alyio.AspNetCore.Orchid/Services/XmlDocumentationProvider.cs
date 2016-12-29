using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.XPath;
using Orchid.Abstractions;
using Microsoft.Extensions.Logging;

namespace Orchid.Services
{
    /// <summary>
    /// A custom <see cref="IDocumentationProvider"/> that reads the API documentation from an XML documentation file.
    /// </summary>
    public class XmlDocumentationProvider : IDocumentationProvider
    {
        private const string TypeExpression = "/doc/members/member[@name='T:{0}']";
        private const string MethodExpression = "/doc/members/member[@name='M:{0}']";
        private const string PropertyExpression = "/doc/members/member[@name='P:{0}']";
        private const string FieldExpression = "/doc/members/member[@name='F:{0}']";
        private const string ParameterExpression = "param[@name='{0}']";

        private readonly ILogger<XmlDocumentationProvider> _logger;
        private readonly IDictionary<Assembly, XPathNavigator> _navigators = new ConcurrentDictionary<Assembly, XPathNavigator>();
        private readonly ISet<Assembly> _notFoundXmlAssemblies = new HashSet<Assembly>();

        /// <summary>
        /// Initializes a new instance of <see cref="XmlDocumentationProvider"/>.
        /// </summary>
        /// <param name="loggerFactory">The <see cref="ILoggerFactory"/>.</param>
        public XmlDocumentationProvider(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<XmlDocumentationProvider>();
        }

        /// <inheritdoc />
        public string GetDocumentation(MemberInfo member, string tagName = "summary")
        {
            if (member == null)
            {
                return null;
                // TODO throw new ArgumentNullException(nameof(member));
            }
            var assembly = (Assembly)null;
            if (member.MemberType == MemberTypes.TypeInfo || member.MemberType == MemberTypes.NestedType)
            {
                assembly = ((TypeInfo)member).Assembly;
            }
            else
            {
                assembly = member.DeclaringType.GetTypeInfo().Assembly;
            }
            XPathNavigator navigator = GetXPathNavigator(assembly);
            if (navigator == null)
            {
                return null;
            }
            var memberNode = GetMemberNode(navigator, member);
            if (memberNode == null)
            {
                return null;
            }
            return GetTagValue(memberNode, tagName);
        }

        /// <inheritdoc />
        public IDictionary<string, string> GetParameterDocumentation(MethodInfo methodInfo)
        {
            if (methodInfo == null)
            {
                return null;
                // TODO throw new ArgumentNullException(nameof(methodInfo));
            }

            IDictionary<string, string> descriptions = new Dictionary<string, string>();

            var assembly = methodInfo.DeclaringType.GetTypeInfo().Assembly;
            XPathNavigator navigator = GetXPathNavigator(assembly);
            if (navigator == null)
            {
                return descriptions;
            }
            var methodNode = GetMemberNode(navigator, methodInfo);
            if (methodNode != null)
            {
                foreach (var parameter in methodInfo.GetParameters())
                {
                    XPathNavigator parameterNode = methodNode.SelectSingleNode(string.Format(CultureInfo.InvariantCulture, ParameterExpression, parameter.Name));
                    if (parameterNode != null)
                    {
                        var desc = parameterNode.Value.Trim();
                        descriptions.Add(parameter.Name, desc);
                    }
                }
            }
            return descriptions;
        }

        private XPathNavigator GetXPathNavigator(Assembly assembly)
        {
            if (_notFoundXmlAssemblies.Contains(assembly))
            {
                return null;
            }
            var navigator = (XPathNavigator)null;
            if (!_navigators.TryGetValue(assembly, out navigator))
            {
                var path = Path.Combine(Path.GetDirectoryName(assembly.Location), assembly.GetName().Name + ".xml");
                if (!File.Exists(path))
                {
                    _notFoundXmlAssemblies.Add(assembly);
                    _logger.LogWarning($"Counld not find documentation file '{path}'.");
                    return null;
                }
                var document = new XPathDocument(path);
                navigator = document.CreateNavigator();
                _navigators.Add(assembly, navigator);
            }

            return navigator;
        }

        private XPathNavigator GetMemberNode(XPathNavigator navigator, MemberInfo member)
        {
            if (member == null) { return null; }

            string memberName = null;
            string expression = null;
            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                case MemberTypes.NestedType:
                    var typeInfo = (TypeInfo)member;
                    memberName = GetTypeName(typeInfo.AsType());
                    expression = TypeExpression;
                    break;
                case MemberTypes.Field:
                    memberName = $"{GetTypeName(member.DeclaringType)}.{member.Name}";
                    expression = FieldExpression;
                    break;
                case MemberTypes.Property:
                    memberName = $"{GetTypeName(member.DeclaringType)}.{member.Name}";
                    expression = PropertyExpression;
                    break;
                case MemberTypes.Method:
                    memberName = GetMethodName((MethodInfo)member);
                    expression = MethodExpression;
                    break;
                default:
                    throw new NotSupportedException();
            }
            string selectExpression = string.Format(CultureInfo.InvariantCulture, expression, memberName);
            var node = navigator.SelectSingleNode(selectExpression);
            if (node == null)
            {
                _logger.LogWarning($"Counld not find xml node named [{selectExpression}].");
            }
            return node;
        }

        private static string GetTagValue(XPathNavigator navigator, string tagName)
        {
            XPathNavigator node = navigator.SelectSingleNode(tagName);
            if (node != null)
            {
                return node.Value.Trim();
            }

            return null;
        }

        private static string GetTypeName(Type type)
        {
            return type.FullName.Split('[')[0].Replace("+", ".");
        }

        private static string GetMethodName(MethodInfo methodInfo)
        {
            string memberName = $"{GetTypeName(methodInfo.DeclaringType)}.{methodInfo.Name}";
            if (methodInfo.GetParameters().Length > 0)
            {
                memberName += $"({string.Join(",", methodInfo.GetParameters().Select(p => GetParameterTypeName(p.ParameterType.GetTypeInfo())))})";
            }

            return memberName;
        }

        // TODO counld not resolve all generic type parameter.
        private static string GetParameterTypeName(TypeInfo typeInfo)
        {
            var name = typeInfo.FullName.Replace("+", ".");
            if (typeInfo.IsGenericType)
            {
                name = name.Split('`')[0];
                name += $"{{{string.Join(",", typeInfo.GetGenericArguments().Select(ga => GetParameterTypeName(ga.GetTypeInfo())))}}}";
            }
            return name;
        }
    }
}
