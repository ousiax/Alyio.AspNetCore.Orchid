using System;
using System.Collections.Generic;
using System.Reflection;

namespace AspNetX.Abstractions
{
    /// <summary>
    /// Defines the provider responsible for documenting the service.
    /// </summary>
    public interface IDocumentationProvider
    {
        /// <summary>
        /// Gets the documentation based on <c>System.Reflection.MemberInfo</c>.
        /// </summary>
        /// <param name="member">The <c>System.Reflection.MemberInfo</c> of a <c>System.Type</c></param>
        /// <returns>The documentation for <paramref name="member"/>.</returns>
        string GetDocumentation(MemberInfo member);

        /// <summary>
        /// Gets the documentation based on <c>System.Type</c>.
        /// </summary>
        /// <param name="type">The <c>System.Type</c>.</c></param>
        /// <returns>The documentation for <paramref name="type"/>.</returns>
        string GetDocumentation(Type type);

        /// <summary>
        /// Gets the documentation of method's parameters based on <c>System.Reflection.MethodInfo</c>.
        /// </summary>
        /// <param name="method">The <c>System.Reflection.MethodInfo</c> of a <c>System.Type</c></param>
        /// <returns>The documentation for <paramref name="method"/>.</returns>
        IDictionary<string, string> GetParameterDocumentation(MethodInfo method);
    }
}
