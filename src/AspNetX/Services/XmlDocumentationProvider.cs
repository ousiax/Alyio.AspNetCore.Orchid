using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml.XPath;
using AspNetX.Abstractions;

namespace AspNetX.Services
{
    /// <summary>
    /// A custom <see cref="IDocumentationProvider"/> that reads the API documentation from an XML documentation file.
    /// </summary>
    public class XmlDocumentationProvider : IDocumentationProvider
    {
        private XPathNavigator _documentNavigator;
        private const string TypeExpression = "/doc/members/member[@name='T:{0}']";
        private const string MethodExpression = "/doc/members/member[@name='M:{0}']";
        private const string PropertyExpression = "/doc/members/member[@name='P:{0}']";
        private const string FieldExpression = "/doc/members/member[@name='F:{0}']";
        private const string ParameterExpression = "param[@name='{0}']";

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlDocumentationProvider"/> class.
        /// </summary>
        /// <param name="documentPath">The physical path to XML document.</param>
        public XmlDocumentationProvider(string documentPath)
        {
            if (documentPath == null)
            {
                throw new ArgumentNullException(nameof(documentPath));
            }
            XPathDocument xpath = new XPathDocument(documentPath);
            _documentNavigator = xpath.CreateNavigator();
        }

        public string GetDocumentation(MemberInfo member)
        {
            if (member == null) { return null; }

            string memberName = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", GetTypeName(member.DeclaringType), member.Name);
            string expression = TypeExpression;
            switch (member.MemberType)
            {
                case MemberTypes.Field:
                    expression = FieldExpression;
                    break;
                case MemberTypes.Method:
                    MethodInfo methodInfo = (MethodInfo)member;
                    string[] paremeterTypeFullNames = methodInfo.GetParameters().Select(p => p.ParameterType.FullName).ToArray();
                    if (paremeterTypeFullNames.Length > 0)
                    {
                        memberName += $"({string.Join(",", paremeterTypeFullNames)})"; // fix memeberName for method type.
                    }
                    expression = MethodExpression;
                    break;
                case MemberTypes.Property:
                    expression = PropertyExpression;
                    break;
            }
            string selectExpression = string.Format(CultureInfo.InvariantCulture, expression, memberName);
            XPathNavigator propertyNode = _documentNavigator.SelectSingleNode(selectExpression);
            return GetTagValue(propertyNode, "summary");
        }

        public IDictionary<string, string> GetParameterDocumentation(MethodInfo methodInfo)
        {
            IDictionary<string, string> descriptions = new Dictionary<string, string>();
            if (methodInfo != null)
            {
                XPathNavigator methodNode = GetMethodNode(methodInfo);
                if (methodNode != null)
                {
                    foreach (var parameterName in methodInfo.GetParameters())
                    {
                        XPathNavigator parameterNode = methodNode.SelectSingleNode(string.Format(CultureInfo.InvariantCulture, ParameterExpression, parameterName.Name));
                        if (parameterNode != null)
                        {
                            var desc = parameterNode.Value.Trim();
                            descriptions.Add(parameterName.Name, desc);
                        }
                    }
                }
            }
            return descriptions;
        }

        public string GetDocumentation(Type type)
        {
            XPathNavigator typeNode = GetTypeNode(type);
            return GetTagValue(typeNode, "summary");
        }

        private XPathNavigator GetMethodNode(MethodInfo method)
        {
            string memberName = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", GetTypeName(method.DeclaringType), method.Name);
            string[] paremeterTypeFullNames = method.GetParameters().Select(p => p.ParameterType.FullName).ToArray();
            if (paremeterTypeFullNames.Length > 0)
            {
                memberName += $"({string.Join(",", paremeterTypeFullNames)})"; // fix memeberName for method type.
            }
            string selectExpression = string.Format(CultureInfo.InvariantCulture, MethodExpression, memberName);
            XPathNavigator methodNode = _documentNavigator.SelectSingleNode(selectExpression);
            return methodNode;
        }

        private static string GetTagValue(XPathNavigator parentNode, string tagName)
        {
            if (parentNode != null)
            {
                XPathNavigator node = parentNode.SelectSingleNode(tagName);
                if (node != null)
                {
                    return node.Value.Trim();
                }
            }

            return null;
        }

        private XPathNavigator GetTypeNode(Type type)
        {
            string controllerTypeName = GetTypeName(type);
            string selectExpression = String.Format(CultureInfo.InvariantCulture, TypeExpression, controllerTypeName);
            return _documentNavigator.SelectSingleNode(selectExpression);
        }

        private static string GetTypeName(Type type)
        {
            string name = type.FullName;
            if (type.GetTypeInfo().IsGenericType)
            {
                // Format the generic type name to something like: Generic{System.Int32,System.String}
                Type genericType = type.GetGenericTypeDefinition();
                Type[] genericArguments = type.GetGenericArguments();
                string genericTypeName = genericType.FullName;

                // Trim the generic parameter counts from the name
                genericTypeName = genericTypeName.Substring(0, genericTypeName.IndexOf('`'));
                string[] argumentTypeNames = genericArguments.Select(t => GetTypeName(t)).ToArray();
                name = String.Format(CultureInfo.InvariantCulture, "{0}{{{1}}}", genericTypeName, String.Join(",", argumentTypeNames));
            }
            if (type.IsNested)
            {
                // Changing the nested type name from OuterType+InnerType to OuterType.InnerType to match the XML documentation syntax.
                name = name.Replace("+", ".");
            }

            return name;
        }
    }
}
