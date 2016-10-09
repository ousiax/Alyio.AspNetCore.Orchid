using System;
using System.Linq;
using System.Reflection;

namespace DoqX
{
    public static class TypeExtensions
    {
        /// <summary>
        /// E.g. List{Object}
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetTypeName(this Type type)
        {
            string modelName = type.Name;
            if (type.GetTypeInfo().IsGenericType)
            {
                Type genericType = type.GetGenericTypeDefinition();
                Type[] genericArguments = type.GetGenericArguments();
                string genericTypeName = genericType.Name;

                genericTypeName = genericTypeName.Substring(0, genericTypeName.IndexOf('`'));
                string[] argumentTypeNames = genericArguments.Select(t => GetTypeName(t)).ToArray();
                modelName = $"{genericTypeName}<{string.Join(",", argumentTypeNames)}>";
            }

            return modelName;
        }

        /// <summary>
        /// E.g System-Collections-Generic-List-System-Object
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetTypeId(this Type type)
        {
            string modelName = type.FullName;
            if (type.GetTypeInfo().IsGenericType)
            {
                Type genericType = type.GetGenericTypeDefinition();
                Type[] genericArguments = type.GetGenericArguments();
                string genericTypeName = genericType.FullName;

                genericTypeName = genericTypeName.Substring(0, genericTypeName.IndexOf('`'));
                string[] argumentTypeNames = genericArguments.Select(t => GetTypeId(t)).ToArray();
                modelName = $"{genericTypeName}-{string.Join(",", argumentTypeNames)}";
            }

            return modelName.Replace('.', '-').Replace(',', '-');
        }
    }
}
