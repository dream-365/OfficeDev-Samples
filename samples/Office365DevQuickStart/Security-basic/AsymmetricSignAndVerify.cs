using System;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Security_basic
{
    public class AsymmetricSignAndVerify
    {
        public void Start()
        {
            byte[] data = Encoding.UTF8.GetBytes("data to validate");

            byte[] dataChanged = Encoding.UTF8.GetBytes("data to validate!");

            var sig = Sign(data);

            var valid = Verify(data, sig);

            var invalid = Verify(dataChanged, sig);
        }

        public byte[] Sign(byte[] input)
        {
            X509Certificate2 x509Certificate2 = new X509Certificate2(
               Convert.FromBase64String(Keys.DefaultX509Data_2048),
               Keys.CertPassword,
               X509KeyStorageFlags.MachineKeySet);

            X509AsymmetricSecurityKey signSecurityKey = new X509AsymmetricSecurityKey(x509Certificate2);

            var algorithm = SecurityAlgorithms.RsaSha256Signature;

            var hash = signSecurityKey.GetHashAlgorithmForSignature(algorithm);

            var formatter = signSecurityKey.GetSignatureFormatter(algorithm);

            formatter.SetHashAlgorithm(hash.GetType().ToString());

            var sig = formatter.CreateSignature(hash.ComputeHash(input));

            return sig;
        }

        public bool Verify(byte[] input, byte[] sig)
        {
             X509Certificate2 x509Certificate2 = new X509Certificate2(
               Convert.FromBase64String(Keys.DefaultX509Data_2048),
               Keys.CertPassword,
               X509KeyStorageFlags.MachineKeySet);

            var publicKey = x509Certificate2.Export(X509ContentType.Cert);

            X509Certificate2 cert = new X509Certificate2(publicKey);

            X509AsymmetricSecurityKey securityKey = new X509AsymmetricSecurityKey(cert);

            var algorithm = SecurityAlgorithms.RsaSha256Signature;

            var hash = securityKey.GetHashAlgorithmForSignature(algorithm);

            var deformatter = securityKey.GetSignatureDeformatter(algorithm);

            deformatter.SetHashAlgorithm(hash.GetType().ToString());

            return deformatter.VerifySignature(hash.ComputeHash(input), sig);
        }
    }
}
