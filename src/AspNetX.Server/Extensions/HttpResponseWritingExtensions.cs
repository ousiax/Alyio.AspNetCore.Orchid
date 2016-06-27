using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
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
#if DEBUG
            var text = JsonConvert.SerializeObject(value, Formatting.Indented);
#else
            var text = JsonConvert.SerializeObject(value, Formatting.None);
#endif
            await response.WriteAsync(text, encoding, cancellationToken);
        }
    }
}
