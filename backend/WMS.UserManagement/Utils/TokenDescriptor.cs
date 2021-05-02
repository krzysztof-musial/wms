using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using WMS.UserManagement.Model;
using WMS.UserManagement.Model.Db;

namespace WMS.UserManagement.Utils
{
    public static class TokenDescriptor
    {
        public static SecurityTokenDescriptor GetTokenDescriptor(User user, Warehouse warehouse, SymmetricSecurityKey symmetricSecurityKey)
        {
            SigningCredentials signingCredentials = GetSigningCredentials(symmetricSecurityKey);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                            {
                            new Claim("userId", user.Id.ToString()),
                            new Claim("userEmail", user.Email),
                            new Claim("userFirstName", user.FirstName),
                            new Claim("userLastName", user.LastName),
                            new Claim("warehouseId", warehouse == null ? "" : warehouse.Id.ToString())
                            }
                        ),
                SigningCredentials = signingCredentials
            };
            return tokenDescriptor;
        }
        private static SigningCredentials GetSigningCredentials(SymmetricSecurityKey symmetricSecurityKey)
        {
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            return signingCredentials;             
        }
    }
}
