using IdentityModel.Client;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontendConsoleApp
{
    public class TokenService : ITokenService
    {
        private TokenResponse myTokenResponse { get; set; }

        public async Task<TokenResponse> GetToken(string scope)
        {
            HttpClient httpClient = new HttpClient();
            DiscoveryDocumentResponse discoveryDocument = await httpClient.GetDiscoveryDocumentAsync("http://localhost:5000");

            ClientCredentialsTokenRequest clientCredentialsTokenRequest = new ClientCredentialsTokenRequest()
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "secret",
                Scope = scope
            };

            var token = await httpClient.RequestClientCredentialsTokenAsync(clientCredentialsTokenRequest);
            myTokenResponse = token;

            return token;
        }        
    }
}
