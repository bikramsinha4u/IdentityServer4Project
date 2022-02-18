using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontendConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var tokenService = new TokenService();

            var tokenResponse = await tokenService.GetToken("api1");

            Console.WriteLine($"token is : {tokenResponse.AccessToken}");

            await CallApi(tokenResponse);
        }

        public static async Task CallApi(TokenResponse tokenResponse)
        {
            HttpClient client = new HttpClient();

            client.SetBearerToken(tokenResponse.AccessToken);

            var apiResponse = await client.GetAsync("https://localhost:6001/weatherforecast");
            if (!apiResponse.IsSuccessStatusCode)
            {
                Console.WriteLine(apiResponse.StatusCode);
            }
            else
            {
                var content = await apiResponse.Content.ReadAsStringAsync();
                Console.WriteLine(content.ToString());
            }
        }
    }
}
