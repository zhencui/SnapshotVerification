using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.SnapshotCollector;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using SnapshotTest;
using Microsoft.ApplicationInsights.DataContracts;

namespace cd_e2e_sf_worker
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class cd_e2e_sf_worker : StatelessService
    {
        private readonly TelemetryClient telemetryClient;
        private readonly TelemetryConfiguration telemetryConfiguration;

        public cd_e2e_sf_worker(StatelessServiceContext context)
            : base(context)
        {
            telemetryConfiguration = new TelemetryConfiguration("");
            telemetryConfiguration.TelemetryProcessorChainBuilder.UseSnapshotCollector(new SnapshotCollectorConfiguration
            {
                IsEnabledInDeveloperMode = true
            });

            telemetryConfiguration.TelemetryInitializers.Add(new OperationCorrelationTelemetryInitializer());

            telemetryClient = new TelemetryClient(telemetryConfiguration);

            var ignored = telemetryConfiguration.TelemetryProcessors;
        }

        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new ServiceInstanceListener[0];
        }

        /// <summary>
        /// This is the main entry point for your service instance.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service instance.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following sample code with your own logic 
            //       or remove this RunAsync override if it's not needed in your service.

            long iterations = 0;
            var client = telemetryClient;

            while (true)
            {
                using (var operation = telemetryClient.StartOperation<RequestTelemetry>("RunAsync", $"Op{iterations}"))
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    ServiceEventSource.Current.ServiceMessage(this.Context, "Working-{0}", ++iterations);

                    await Task.Delay(TimeSpan.FromSeconds(100), cancellationToken);

                    try
                    {
                        CPUIntensiveComputation.RecusiveCall1(12);
                    }
                    catch (Exception e)
                    {
                        client.TrackException(e);
                        operation.Telemetry.Success = false;
                    }
                }
            }
        }
    }
}
