using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using static System.Console;

namespace IntSample.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var tokenResponse = await GetCredentials();

                WriteLine();
                WriteLine("Got a token, calling api...");
                WriteLine();

                await CallApi(tokenResponse);

            }
            catch (Exception ex)
            {
                WriteLine(ex);
            }


            WriteLine();
            WriteLine("Press any key to exit...");
            ReadLine();
        }

        static async Task<TokenResponse> GetCredentials()
        {
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
            if (disco.IsError)
            {
                WriteLine(disco.Error);
                return null;
            }

            var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");
            if (tokenResponse.IsError)
            {
                WriteLine(tokenResponse.Error);
                return null;
            }

            WriteLine(tokenResponse.Json);
            return tokenResponse;
        }

        static async Task CallApi(TokenResponse tokenResponse)
        {
            // call api
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:5001/identity");
            if (!response.IsSuccessStatusCode)
            {
                WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                WriteLine(JArray.Parse(content));
            }
        }
    }
}
