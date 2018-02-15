﻿using System;
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
                "http://auto-light-up-asp-net46-old.azurewebsites.net/Home/About",
                "http://auto-light-up-asp-net46-old.azurewebsites.net/Home/Contact",
                "http://auto-light-up-asp-net-core20-old.azurewebsites.net/Home/About",
                "http://auto-light-up-asp-net-core20-old.azurewebsites.net/Home/Contact",
                "http://auto-light-up-asp-net46.azurewebsites.net/Home/About",
                "http://auto-light-up-asp-net46.azurewebsites.net/Home/Contact",
                "http://auto-light-up-asp-net-core20.azurewebsites.net/Home/About",
                "http://auto-light-up-asp-net-core20.azurewebsites.net/Home/Contact"
            };

            using (var sender = new RequestSender(urls))
            {
                sender.Run();
            }
        }
    }
}
