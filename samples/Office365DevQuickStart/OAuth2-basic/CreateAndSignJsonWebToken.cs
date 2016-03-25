using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OAuth2_basic
{
    public class CreateAndSignJsonWebToken
    {
        public void Run()
        {

        }

        public string CreateToken()
        {
            X509Certificate2 x509Certificate2 = new X509Certificate2(
               Convert.FromBase64String(Keys.DefaultX509Data_2048),
               Keys.CertPassword,
               X509KeyStorageFlags.MachineKeySet);

            X509SigningCredentials signingCredentials = new X509SigningCredentials(x509Certificate2, SecurityAlgorithms.RsaSha256Signature, SecurityAlgorithms.Sha256Digest);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            var originalIssuer = "http://sts.contoso.com";

            var issuer = originalIssuer;

            var claims = new List<Claim> {
                new Claim("aud", "http://outlook.contoso.com", ClaimValueTypes.String, issuer, originalIssuer),
                new Claim(ClaimTypes.Email, "user@contoso.com", ClaimValueTypes.String, issuer, originalIssuer),
                new Claim(ClaimTypes.Name, "user@contoso.com", ClaimValueTypes.String, issuer, originalIssuer),
                new Claim("preferred_username", "user_name", ClaimValueTypes.String, issuer, originalIssuer)
            };

            DateTime utcNow = DateTime.UtcNow;

            DateTime expire = utcNow + TimeSpan.FromHours(1);

            ClaimsIdentity subject = new ClaimsIdentity(claims: claims);

            JwtSecurityToken jwtToken = tokenHandler.CreateToken(
                issuer: issuer,
                signingCredentials: signingCredentials,
                subject: subject) as JwtSecurityToken;

            var token = tokenHandler.WriteToken(jwtToken);

            return token;
        }
    }
}
