using System.Collections.Generic;
using IdentityServer4.Models;

namespace AuthServer.Web.Configuration.Clients
{
    public class RoClient : Client
    {
        public RoClient()
        {
            ClientId = "ro.client";
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword;

            ClientSecrets = new List<Secret>
            {
                new Secret("secret".Sha256())
            };
            AllowedScopes = new List<string> { "api1" };
        }
    }
}
