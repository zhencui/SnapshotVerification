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
                "http://cd-248-aspnet46-110.azurewebsites.net/Home/About",
                "http://cd-248-aspnet46-110.azurewebsites.net/Home/Contact",
                "http://cd-248-aspnetcore20-110.azurewebsites.net/Home/About",
                "http://cd-248-aspnetcore20-110.azurewebsites.net/Home/Contact",
                "https://cd-248-aspnet46-110-248.azurewebsites.net/Home/About",
                "https://cd-248-aspnet46-110-248.azurewebsites.net/Home/Contact",
                "https://cd-248-aspnetcore20-110-248.azurewebsites.net/Home/About",
                "https://cd-248-aspnetcore20-110-248.azurewebsites.net/Home/Contact",
                "http://cd-248-aspnet46-111b4-248.azurewebsites.net/Home/About",
                "http://cd-248-aspnet46-111b4-248.azurewebsites.net/Home/Contact",
                "http://cd-248-aspnetcore20-111b4-248.azurewebsites.net/Home/About",
                "http://cd-248-aspnetcore20-111b4-248.azurewebsites.net/Home/Contact"
            };

            using (var sender = new RequestSender(urls))
            {
                sender.Run();
            }
        }
    }
}
