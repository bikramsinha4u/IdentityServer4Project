using IdentityModel.Client;
using System.Threading.Tasks;

namespace FrontendConsoleApp
{
    public interface ITokenService
    {
        Task<TokenResponse> GetToken(string scope);
    }
}
