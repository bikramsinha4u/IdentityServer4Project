using IdentityModel.Client;
using System.Threading.Tasks;

namespace WebAppProject.Services
{
    public interface ITokenService
    {
        Task<TokenResponse> GetToken(string scope);
    }
}
