using Microsoft.ApplicationInsights;
using SnapshotTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cd_e2e_web_role.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            var ai = new TelemetryClient();
            try
            {
                CPUIntensiveComputation.RecusiveCall1(12);
            }
            catch (Exception ex)
            {
                ai.TrackException(ex);
            }
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}