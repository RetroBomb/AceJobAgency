using AceJobAgency.Services;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AceJobAgency.Service
{
    public class GoogleCaptchaService
    {
        public async Task<bool> VerifyToken(string token)
        {
            try
            {
                var url = $"https://www.google.com/recaptcha/api/siteverify?secret=6LdhElUkAAAAAJ1o9jWWDJprWaqGTjOFr9tHKbc1&response={token}";
                using (var client = new HttpClient())
                {
                    var httpResult = await client.GetAsync(url);
                    if (httpResult.StatusCode != HttpStatusCode.OK)
                    {
                        return false;
                    }

                    var responseString = await httpResult.Content.ReadAsStringAsync();

                    var googleResult = JsonConvert.DeserializeObject<GoogleCaptchaResponse>(responseString);

                    return googleResult.success && googleResult.score >= 0.5;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
