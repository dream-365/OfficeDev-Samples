using Newtonsoft.Json.Linq;
using System;

namespace OAuth2_basic
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            new OAuthBasic().Run();
        }
    }
}
