using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Security_basic
{
    public class GetInfoOfCertificates
    {
        public void PrintInfo()
        {
            var x509Certificate2 = new X509Certificate2(@"C:\Workbench\certificates\office_365_app.pfx", "123456");

            var certRawData = x509Certificate2.Export(X509ContentType.Cert);

            var cert = new X509Certificate2(certRawData);

            var base64RawData = Convert.ToBase64String(cert.GetRawCertData());

            var hash = cert.GetCertHash();

            var base64Thumbprint = Convert.ToBase64String(hash);

            Console.WriteLine("Raw data: {0}", base64RawData);

            Console.WriteLine("Thumbprint: {0}", base64Thumbprint);
        }
    }
}
