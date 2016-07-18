using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace AspNetX.Json.Converters
{
    public class BindingSourceConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(BindingSource);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var bindingSource = value as BindingSource;
            if (bindingSource == null)
            {
                writer.WriteNull();
                return;
            }
            writer.WriteValue(bindingSource.DisplayName);
        }
    }
}
