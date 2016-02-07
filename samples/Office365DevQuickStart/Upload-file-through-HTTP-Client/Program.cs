using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upload_file_through_HTTP_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var operations = new FileStreamOperations();

            var task = operations.Upload();

            task.Wait();

            Console.ReadLine();
        }
    }
}
