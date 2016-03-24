using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Security_basic
{
    class Program
    {
        static void Main(string[] args)
        {
            var demo = new AsymmetricSignAndVerify();

            demo.Start();
        }
    }
}
