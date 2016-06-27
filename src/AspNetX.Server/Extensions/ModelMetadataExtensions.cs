using System;
using System.Linq;
using System.Reflection;

namespace AspNetX.Server
{
    public static class ModelMetadataExtensions
    {
        public static string GetTypeName(this Type type)
        {
            string modelName = type.Name;
            if (type.GetTypeInfo().IsGenericType)
            {
                // Format the generic type name to something like: GenericOfAgurment1AndArgument2
                Type genericType = type.GetGenericTypeDefinition();
                Type[] genericArguments = type.GetGenericArguments();
                string genericTypeName = genericType.Name;

                // Trim the generic parameter counts from the name
                genericTypeName = genericTypeName.Substring(0, genericTypeName.IndexOf('`'));
                string[] argumentTypeNames = genericArguments.Select(t => t.GetTypeName()).ToArray();
                modelName = $"{genericTypeName}Of{String.Join("And", argumentTypeNames)}";
            }

            return modelName;
        }
    }
}
