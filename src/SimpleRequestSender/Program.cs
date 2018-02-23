using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRequestSender
{
    class Program
    {
        static void Main(string[] args)
        {
            // Change this to the url you want to ping.
            

            using (var sender = new RequestSender(CommonHelper.URLs))
            {
                sender.Run();
            }
        }
    }
}
