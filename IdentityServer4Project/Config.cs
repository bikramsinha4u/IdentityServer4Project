using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer4Project
{
    public class Config
    {
        public static IEnumerable<ApiScope> Scopes
        {
            get
            {
                return new List<ApiScope>
                {
                    new ApiScope("api1", "My Scope Name")
                };
            }
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    ClientName = "Projects SPA",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    RequireClientSecret = true,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    RequirePkce = true,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    AllowOfflineAccess = true,

                    AllowedScopes =
                    {
                        //IdentityServerConstants.StandardScopes.OpenId,
                        //IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },

                    //client.AllowClientCredentialsOnly = true
                    //AllowedGrantTypes = GrantTypes.Code,
                    /*
                    RedirectUris =           { "http://localhost:4200/signin-callback", "http://localhost:4200/assets/silent-callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:4200/signout-callback" },
                    AllowedCorsOrigins =     { "http://localhost:4200", "http://localhost:7000" }
                    */
                }
            };
        }

        public static List<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                //new IdentityResources.OpenId(),
                //new IdentityResources.Profile() // <-- usefull
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                //new ApiResource("api1", "My API")
            };
        }
    }
}
