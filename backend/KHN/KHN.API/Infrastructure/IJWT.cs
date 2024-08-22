using Microsoft.IdentityModel.Tokens;

namespace KHN.API.Infrastructure
{
    public interface IJWT
    {
        bool ValidateToken(string token);
        string GenerateToken(int expirePerminute, string usernmae, int role);
    }
}