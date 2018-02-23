using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ApplicationInsights;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace cd_e2e_web_role
{
    public class WebRole : RoleEntryPoint
    {
        public override bool OnStart()
        {
            // For information on handling configuration changes
            // see the MSDN topic at https://go.microsoft.com/fwlink/?LinkId=166357.

            TelemetryClient client = new TelemetryClient();
            // This is used for detect site restart.
            client.TrackEvent("SiteStart");
            client.Flush();

            return base.OnStart();
        }
    }
}
