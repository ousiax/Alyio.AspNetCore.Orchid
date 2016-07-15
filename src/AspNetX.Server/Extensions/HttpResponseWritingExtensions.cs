using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AspNetX.Server
{
    public static class HttpResponseWritingExtensions
    {
        public static async Task WriteJsonAsync(this HttpResponse response, object value, CancellationToken cancellationToken = default(CancellationToken))
        {
            await response.WriteJsonAsync(value, Encoding.UTF8, cancellationToken);
        }

        public static async Task WriteJsonAsync(this HttpResponse response, object value, Encoding encoding, CancellationToken cancellationToken = default(CancellationToken))
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore, //TODO Newtonsoft.Json.ReferenceLoopHandling
#if DEBUG
                Formatting = Formatting.Indented
#endif
            };
            var text = JsonConvert.SerializeObject(value, settings);
            await response.WriteAsync(text, encoding, cancellationToken);
        }
    }
}
