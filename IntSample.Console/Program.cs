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
                var tokenResponse = await GetCredentials(true);
                WriteLine();
                WriteLine("Calling api...");
                WriteLine();
                await CallApi(tokenResponse);

            }
            catch (Exception ex)
            {
                WriteLine(ex);
            }


            WriteLine("Press any key to exit...");
            ReadLine();
        }

        static async Task<TokenResponse> GetCredentials(bool useResourceOwnerCredentials)
        {
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
            if (disco.IsError)
            {
                WriteLine(disco.Error);
                return null;
            }

            TokenResponse tokenResponse = null;

            if (useResourceOwnerCredentials)
            {
                var tokenClient = new TokenClient(disco.TokenEndpoint, "ro.client", "secret");
                tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("alice", "pass");
            }
            else
            {
                var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
                tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");
            }


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
