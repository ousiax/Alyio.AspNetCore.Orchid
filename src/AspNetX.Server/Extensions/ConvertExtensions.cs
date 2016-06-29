using System;
using System.Globalization;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.AspNet.Mvc.ModelBinding.Metadata;

namespace AspNetX.Server
{
    internal static class ConvertExtensions
    {
        public static int? ToInt32(this object value)
        {
            if (value == null) return null;
            if (value.GetType() == typeof(int)) { return (int)value; }
            var convertible = value as IConvertible;
            if (convertible != null)
            {
                return convertible.ToInt32(CultureInfo.InvariantCulture);
            }
            int result;
            if (int.TryParse(value.ToStringExt() ?? "NaN", out result))
            {
                return result;
            }
            return null;
        }

        public static string ToStringExt(this object value)
        {
            if (value == null) return null;
            if (value is string) { return (string)value; }
            IConvertible convertible = value as IConvertible;
            if (convertible != null)
                return ((IConvertible)value).ToString(CultureInfo.InvariantCulture);
            return value.ToString();
        }

        public static ModelMetadataIdentity ToMetadataIdentity(this ModelMetadata metadata)
        {
            ModelMetadataIdentity identity;
            if (metadata.MetadataKind == ModelMetadataKind.Type)
            {
                identity = ModelMetadataIdentity.ForType(metadata.ModelType);
            }
            else
            {
                identity = ModelMetadataIdentity.ForProperty(metadata.ModelType, metadata.PropertyName, metadata.ContainerType);
            }
            return identity;
        }
    }
}
