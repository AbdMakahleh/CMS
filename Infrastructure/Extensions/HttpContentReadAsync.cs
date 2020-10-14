
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;
namespace Infrastructure.Extensions
{
    public static class HttpContentReadAsync
    {
        public static async Task<T> ReadAsAsync<T>(this HttpContent content)
        {
          return  await JsonSerializer.DeserializeAsync<T>(await content.ReadAsStreamAsync());
        }
     


        public static async Task<T> ReadAsAsync<T>(this HttpContent content, JsonSerializerSettings serializerSettings)
        {
            return JsonConvert.DeserializeObject<T>(await content.ReadAsStringAsync(), serializerSettings);
        }


    }
}
