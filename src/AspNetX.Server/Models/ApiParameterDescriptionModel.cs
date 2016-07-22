using System;
using System.Runtime.Serialization;
using AspNetX.Json.Converters;
using Microsoft.AspNet.Mvc.ApiExplorer;
using Microsoft.AspNet.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace AspNetX.Models
{
    /// <summary>
    /// Represents a description model for an <see cref="Microsoft.AspNet.Mvc.ApiExplorer.ApiParameterDescription"/>.
    /// </summary>
    [DataContract]
    public class ApiParameterDescriptionModel
    {
        /// <summary>
        /// Gets or sets a general description of the current instance.
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name => this.ApiParameterDescription.Name;

        /// <summary>
        /// Gets the <see cref="BindingSource"/>.
        /// </summary>
        [DataMember(Name = "source")]
        [JsonConverter(typeof(BindingSourceConverter))]
        public BindingSource Source => this.ApiParameterDescription.Source;

        /// <summary>
        /// Gets the parameter type.
        /// </summary>
        [DataMember(Name = "type")]
        [JsonConverter(typeof(TypeConverter))]
        public Type Type => this.ApiParameterDescription.Type;

        /// <summary>
        /// Gets or sets the id of the parameter <see cref="Type"/>.
        /// </summary>
        [DataMember(Name = "metadata")]
        public ModelMetadataWrapper Metadata { get; set; }

        /// <summary>
        /// Gets or sets a metadata description of an input to an API.
        /// </summary>
        public ApiParameterDescription ApiParameterDescription { get; set; }
    }
}
