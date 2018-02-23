using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using SimpleRequestSender;
using SnapshotTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace cd_e2e_worker_role
{
    public class Worker
    {
        private readonly TelemetryClient _client;

        public Worker(TelemetryClient client)
        {
            _client = client;
        }

        public void DoWork()
        {
            int taskNumber = DateTime.UtcNow.Second % 13;
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < taskNumber; i++)
            {
                Task t = null;
                if (taskNumber % 2 == 0)
                {
                    t = DoComputationWorks();
                }
                else
                {
                    t = DoRequestWorks();
                }
                tasks.Add(t);
            }
            Task.WaitAll(tasks.ToArray());
        }

        private async Task DoComputationWorks()
        {
            await WorkWrapperAsync(nameof(DoComputationWorks), () =>
            {
                return Task.Run(() => CPUIntensiveComputation.RecusiveCall1(12));
            }).ConfigureAwait(false);

        }

        private async Task DoRequestWorks()
        {
            foreach (var url in CommonHelper.URLs)
            {
                if (!CommonHelper.ShouldBlock())
                {
                    await WorkWrapperAsync(nameof(SendHttpRequestAsync), () => SendHttpRequestAsync(url)).ConfigureAwait(false);
                }
            }

        }

        private async Task SendHttpRequestAsync(string url)
        {
            var client = new HttpClient();
            var rst = await client.GetAsync(url).ConfigureAwait(false);
            if (!rst.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Request failed with status code: " + rst.StatusCode);
            }
        }

        private async Task WorkWrapperAsync(string operationName, Func<Task> func)
        {
            var requestTelemetry = new RequestTelemetry
            {
                Name = operationName
            };
            using (var operation = _client.StartOperation(requestTelemetry))
            {
                try
                {
                    await func().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    requestTelemetry.Success = false;
                    requestTelemetry.ResponseCode = "503";
                    _client.TrackException(ex);
                }
            }
        }
    }
}
