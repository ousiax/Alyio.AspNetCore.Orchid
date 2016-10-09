using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace DoqX
{
    public static class HttpResponseExtensions
    {
        public static async Task WriteJsonAsync(this HttpResponse response, object value, Formatting formatting = Formatting.None, CancellationToken cancellationToken = default(CancellationToken))
        {
            await response.WriteJsonAsync(value, formatting, Encoding.UTF8, cancellationToken);
        }

        public static async Task WriteJsonAsync(this HttpResponse response, object value, Formatting formatting, Encoding encoding, CancellationToken cancellationToken = default(CancellationToken))
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore, //TODO Newtonsoft.Json.ReferenceLoopHandling
                Formatting = formatting
            };
            var text = JsonConvert.SerializeObject(value, settings);
            await response.WriteAsync(text, encoding, cancellationToken);
        }
    }
}
