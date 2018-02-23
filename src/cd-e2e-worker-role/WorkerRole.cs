using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;

namespace cd_e2e_worker_role
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);

        public override void Run()
        {
            Trace.TraceInformation("cd-e2e-worker-role is running");
            TelemetryClient client = new TelemetryClient();
            // This is used for detect site restart.
            client.TrackEvent("SiteStart");
            client.Flush();

            try
            {
                this.RunAsync(this.cancellationTokenSource.Token).Wait();
            }
            finally
            {
                this.runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at https://go.microsoft.com/fwlink/?LinkId=166357.

            bool result = base.OnStart();

            Trace.TraceInformation("cd-e2e-worker-role has been started");

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("cd-e2e-worker-role is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("cd-e2e-worker-role has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            Trace.TraceInformation("Working");
            var client = new TelemetryClient();
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(1000);
                    Worker w = new Worker(client);
                    w.DoWork();
                }
                catch (Exception ex)
                {
                    client.TrackException(ex);
                }
            }
        }
    }
}
