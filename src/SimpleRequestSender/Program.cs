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
            List<string> urls = new List<string>
            {
                "http://aspnetcore20mvc.azurewebsites.net/Home/About",
                "http://aspnetcore20mvc.azurewebsites.net/Home/Contact",
                "http://aspnet46mvc.azurewebsites.net/Home/About",
                "http://aspnet46mvc.azurewebsites.net/Home/Contact"
            };

            using (var sender = new RequestSender(urls))
            {
                sender.Run();
            }
        }
    }
}
