using System.Runtime.Serialization;

namespace DocX.Models
{
    /// <summary>
    /// Represents a description class for the enumeration type.
    /// </summary>
    [DataContract(Name = "enumFieldDescription")]
    public class EnumFieldDescription
    {
        /// <summary>
        /// Gets or sets the string containing the name of the enumerated constant in enumType.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value of the constants in enumType.
        /// </summary>
        [DataMember(Name = "value")]
        public string Value { get; set; }

        /// <summary>
        /// A summary tag value of the field's xml documentation.
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }
    }
}
