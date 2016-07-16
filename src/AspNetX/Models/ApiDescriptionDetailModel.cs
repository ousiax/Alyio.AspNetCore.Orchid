using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AspNetX.Models
{
    [DataContract]
    public class ApiDescriptionDetailModel : ApiDescriptionModel
    {
        [DataMember(Name = "requestInformation", Order = 1000)]
        public RequestInformation RequestInformation { get; set; }

        [DataMember(Name = "responseInformation", Order = 1001)]
        public ResponseInformation ResponseInformation { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as ApiDescriptionDetailModel;
            if (other != null)
            {
                return string.Equals(this.Id, other.Id, StringComparison.OrdinalIgnoreCase);
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.Id?.GetHashCode() ?? 0;
        }
    }

    public class RequestInformation
    {
        public IReadOnlyList<object> UriParameters { get; set; }

    }

    public class ResponseInformation
    {
    }
}
