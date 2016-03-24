using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Security_basic
{
    public class AsymmetricSignAndVerify
    {
        public void Start()
        {
            X509Certificate2 x509Certificate2 = new X509Certificate2(
               Convert.FromBase64String(Keys.DefaultX509Data_2048),
               Keys.CertPassword,
               X509KeyStorageFlags.MachineKeySet);

            X509AsymmetricSecurityKey securityKey = new X509AsymmetricSecurityKey(x509Certificate2);

            var algorithm = SecurityAlgorithms.RsaSha256Signature;

            var hash = securityKey.GetHashAlgorithmForSignature(algorithm);

            byte[] data = Encoding.UTF8.GetBytes("data to validate");

            byte[] dataChanged = Encoding.UTF8.GetBytes("data to validate!");

            var formatter = securityKey.GetSignatureFormatter(algorithm);

            formatter.SetHashAlgorithm(hash.GetType().ToString());

            var deformatter = securityKey.GetSignatureDeformatter(algorithm);

            deformatter.SetHashAlgorithm(hash.GetType().ToString());

            var sig = formatter.CreateSignature(hash.ComputeHash(data));

            var valid = deformatter.VerifySignature(hash.ComputeHash(data), sig);

            var invalid = deformatter.VerifySignature(hash.ComputeHash(dataChanged), sig);
        }
    }
}
