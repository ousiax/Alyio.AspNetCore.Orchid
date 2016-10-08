using System.Collections.Generic;
using System.Reflection;

namespace DocX.Abstractions
{
    /// <summary>
    /// Defines the provider responsible for documenting the service.
    /// </summary>
    public interface IDocumentationProvider
    {
        /// <summary>
        /// Gets the documentation based on <see cref="System.Reflection.MemberInfo"/>.
        /// </summary>
        /// <param name="member">The <c>System.Reflection.MemberInfo</c> of a <c>System.Type</c></param>
        /// <param name="tagName">The documentation tag name. The default is summary.</param>
        /// <returns>The documentation for <paramref name="member"/>.</returns>
        string GetDocumentation(MemberInfo member, string tagName = "summary");

        /// <summary>
        /// Gets the documentation of method's parameters based on <see cref="System.Reflection.MethodInfo"/>.
        /// </summary>
        /// <param name="method">The <see cref="System.Reflection.MethodInfo"/>.</param>
        /// <returns>The <see cref="IDictionary{TKey, TValue}"/>.</returns>
        IDictionary<string,string> GetParameterDocumentation(MethodInfo method);
    }
}
