using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRequestSender
{
    public static class CommonHelper
    {
        public static List<string> URLs = new List<string>
            {
                "http://cd-restart-doublecheck2-asp46.azurewebsites.net/Home/About",
                "http://cd-restart-doublecheck2-asp46.azurewebsites.net/Home/Contact",
                "http://cd-restart-doublecheck-asp46.azurewebsites.net/Home/About",
                "http://cd-restart-doublecheck-asp46.azurewebsites.net/Home/Contact",
                "https://cd-prod-westus2-asp-netcore-20.azurewebsites.net/Home/About",
                "https://cd-prod-westus2-asp-netcore-20.azurewebsites.net/Home/Contact",
                "http://cd-prod-westus2-asp-net-46.azurewebsites.net/Home/About",
                "http://cd-prod-westus2-asp-net-46.azurewebsites.net/Home/Contact",
                "http://cd-prod-northeu-asp-netcore-20.azurewebsites.net/Home/About",
                "http://cd-prod-northeu-asp-netcore-20.azurewebsites.net/Home/Contact",
                "http://cd-prod-northeu-asp-net-46.azurewebsites.net/Home/About",
                "http://cd-prod-northeu-asp-net-46.azurewebsites.net/Home/Contact",

                "http://cd-prod-inject-wcus-64bit.azurewebsites.net/Home/About",
                "http://cd-prod-inject-wcus-64bit.azurewebsites.net/Home/Contact",
                "http://cd-prod-inject-wcus-32bit.azurewebsites.net/Home/About",
                "http://cd-prod-inject-wcus-32bit.azurewebsites.net/Home/Contact",
                "http://cd-prod-inject-wcus-disable.azurewebsites.net/Home/About",
                "http://cd-prod-inject-wcus-disable.azurewebsites.net/Home/Contact",
                "http://cd-prod-inject-wcus-empty.azurewebsites.net/Home/About",
                "http://cd-prod-inject-wcus-empty.azurewebsites.net/Home/Contact",
                "http://cd-prod-inject-weu-64bit.azurewebsites.net/Home/About",
                "http://cd-prod-inject-weu-64bit.azurewebsites.net/Home/Contact",
                "http://cd-prod-inject-weu-32bit.azurewebsites.net/Home/About",
                "http://cd-prod-inject-weu-32bit.azurewebsites.net/Home/Contact",
                "http://cd-prod-inject-weu-disable.azurewebsites.net/Home/About",
                "http://cd-prod-inject-weu-disable.azurewebsites.net/Home/Contact",
                "http://cd-prod-inject-weu-empty.azurewebsites.net/Home/About",
                "http://cd-prod-inject-weu-empty.azurewebsites.net/Home/Contact",

                "http://cd-e2e-cloud-service.cloudapp.net/Home/About",
                "http://cd-e2e-cloud-service.cloudapp.net/Home/Contact",

                "http://cd-test-dev-asp-net46.azurewebsites.net/Home/About",
                "http://cd-test-dev-asp-net46.azurewebsites.net/Home/Contact",
                "http://cd-test-dev-asp-net-core20.azurewebsites.net/Home/About",
                "http://cd-test-dev-asp-net-core20.azurewebsites.net/Home/Contact"
            };


        public static bool ShouldBlock()
        {
            var minute = DateTime.Now.Minute;
            if ((minute / 15) % 2 == 0)
            {
                return true;
            }
            return false;
        }

    }
}
