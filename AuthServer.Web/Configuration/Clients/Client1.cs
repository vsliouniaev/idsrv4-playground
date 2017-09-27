using System.Collections.Generic;
using IdentityServer4.Models;

namespace AuthServer.Web.Configuration.Clients
{
    public class Client1 : Client
    {
        public Client1()
        {
            ClientId = "client";
            AllowedGrantTypes = GrantTypes.ClientCredentials;
            AllowedScopes = new List<string> {"api1"};
        }
    }
}