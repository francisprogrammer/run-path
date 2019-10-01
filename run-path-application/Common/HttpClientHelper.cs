using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RP.App.Common
{
    class HttpClientHelper
    {
        public static async Task<Response<T>> Get<T>(string url, string errorMessage) where T: class
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage result;

                try
                {
                    result = await client.GetAsync(url);
                }
                catch (Exception e)
                {
                    return Response<T>.Failed($"{e}");
                }

                return !result.IsSuccessStatusCode
                    ? Response<T>.Failed("Error retrieving photos, oh no!\nPlease try again later")
                    : Response<T>.Success(JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync()));
            }
        }
    }
}