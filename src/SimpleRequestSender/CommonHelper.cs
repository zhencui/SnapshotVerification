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

                "http://cd-e2e-cloud-service.cloudapp.net/Home/About",
                "http://cd-e2e-cloud-service.cloudapp.net/Home/Contact"

                //"http://auto-light-up-asp-net46-old.azurewebsites.net/Home/About",
                //"http://auto-light-up-asp-net46-old.azurewebsites.net/Home/Contact",
                //"http://auto-light-up-asp-net-core20-old.azurewebsites.net/Home/About",
                //"http://auto-light-up-asp-net-core20-old.azurewebsites.net/Home/Contact",
                //"http://auto-light-up-asp-net46.azurewebsites.net/Home/About",
                //"http://auto-light-up-asp-net46.azurewebsites.net/Home/Contact",
                //"http://auto-light-up-asp-net-core20.azurewebsites.net/Home/About",
                //"http://auto-light-up-asp-net-core20.azurewebsites.net/Home/Contact",
                //"http://auto-ena-restart.azurewebsites.net/Home/About",
                //"http://auto-ena-restart.azurewebsites.net/Home/Contact",
                //"https://cd-inject-asp20.azurewebsites.net/Home/About"

                //"http://nuget-12-restart-46-1.azurewebsites.net/Home/About",
                //"http://nuget-12-restart-46-1.azurewebsites.net/Home/Contact",
                //"http://nuget-12-restart-46-2.azurewebsites.net/Home/About",
                //"http://nuget-12-restart-46-2.azurewebsites.net/Home/Contact",
                //"http://nuget-12-restart-core20-1.azurewebsites.net/Home/About",
                //"http://nuget-12-restart-core20-1.azurewebsites.net/Home/Contact",
                //"http://nuget-12-restart-core20-2.azurewebsites.net/Home/About",
                //"http://nuget-12-restart-core20-2.azurewebsites.net/Home/Contact"
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
