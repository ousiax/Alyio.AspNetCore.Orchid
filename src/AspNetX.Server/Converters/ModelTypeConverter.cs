using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace AspNetX.Server.Converters
{
    public class ModelTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var type = value as Type;
            if (type == null)
            {
                writer.WriteNull();
                return;
            }
            var name = GetTypeName(type);
            writer.WriteValue(name);
        }

        private static string GetTypeName(Type type)
        {
            string modelName = type.Name;
            if (type.GetTypeInfo().IsGenericType)
            {
                Type genericType = type.GetGenericTypeDefinition();
                Type[] genericArguments = type.GetGenericArguments();
                string genericTypeName = genericType.Name;

                genericTypeName = genericTypeName.Substring(0, genericTypeName.IndexOf('`'));
                string[] argumentTypeNames = genericArguments.Select(t => GetTypeName(t)).ToArray();
                modelName = $"{genericTypeName}<{String.Join(",", argumentTypeNames)}>";
            }

            return modelName;
        }
    }
}
