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
                "http://cd-e2e-westeu-aspnet46-site.azurewebsites.net/Home/Contact",
                "http://cd-e2e-westeu-aspnetcore20-site.azurewebsites.net/Home/Contact",
                "http://cd-e2e-westus2-aspnet46-site.azurewebsites.net/Home/Contact",
                "http://cd-e2e-westus2-aspnetcore20-site.azurewebsites.net/Home/Contact",
                "http://cd-e2e-westeu-aspnet46-site.azurewebsites.net/Home/About",
                "http://cd-e2e-westeu-aspnetcore20-site.azurewebsites.net/Home/About",
                "http://cd-e2e-westus2-aspnet46-site.azurewebsites.net/Home/About",
                "http://cd-e2e-westus2-aspnetcore20-site.azurewebsites.net/Home/About"
            };

            using (var sender = new RequestSender(urls))
            {
                sender.Run();
            }
        }
    }
}
