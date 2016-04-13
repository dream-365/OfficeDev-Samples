using System;
using System.Collections.Generic;
using System.Linq;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWT_Token
{
    public class CreateAndSignJsonWebToken
    {
        public void Run()
        {
            var x509Certificate2 = new X509Certificate2(@"{FILE PATH}\office_365_app.pfx", "PASS_WORD");

            X509SigningCredentials signingCredentials = new X509SigningCredentials(x509Certificate2, SecurityAlgorithms.RsaSha256Signature, SecurityAlgorithms.Sha256Digest);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            var originalIssuer = "{YOUR CLIENT ID}";

            var issuer = originalIssuer;

            DateTime utcNow = DateTime.UtcNow;

            DateTime expired = utcNow + TimeSpan.FromHours(1);

            var claims = new List<Claim> {
                new Claim("aud", "https://login.microsoftonline.com/{YOUR_TENENT_ID}/oauth2/token", ClaimValueTypes.String, issuer, originalIssuer),
                new Claim("exp", "1460534173", ClaimValueTypes.DateTime, issuer, originalIssuer), 
                new Claim("jti", "{SOME GUID YOU ASSIGN}", ClaimValueTypes.String, issuer, originalIssuer),
                new Claim("nbf", "1460533573", ClaimValueTypes.String, issuer, originalIssuer),
                new Claim("sub", "{YOUR CLIENT ID}", ClaimValueTypes.String, issuer, originalIssuer)
            };

            ClaimsIdentity subject = new ClaimsIdentity(claims: claims);

            JwtSecurityToken jwtToken = tokenHandler.CreateToken(
                issuer: issuer,
                signingCredentials: signingCredentials,
                subject: subject) as JwtSecurityToken;

            jwtToken.Header.Remove("typ");

            var token = tokenHandler.WriteToken(jwtToken);
        }
    }
}
