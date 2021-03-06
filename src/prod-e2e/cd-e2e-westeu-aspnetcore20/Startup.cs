﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ApplicationInsights.AspNetCore;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Options;
using Microsoft.ApplicationInsights.SnapshotCollector;

namespace cd_e2e_westeu_aspnetcore20
{
    public class Startup
    {
        private class SnapshotCollectorTelemetryProcessorFactory : ITelemetryProcessorFactory
        {
            private readonly IServiceProvider _serviceProvider;

            public SnapshotCollectorTelemetryProcessorFactory(IServiceProvider serviceProvider) =>
                _serviceProvider = serviceProvider;

            public ITelemetryProcessor Create(ITelemetryProcessor next)
            {
                SnapshotCollectorConfiguration configuration = new SnapshotCollectorConfiguration()
                {
                    //IsEnabled = false,
                    //IsEnabledInDeveloperMode = true,
                    //ThresholdForSnapshotting = 1,   // How many times we need to see an exception before we ask for snapshots. Default is 5. The value cannot be less than 1.
                    MaximumSnapshotsRequired = 900,   //The maximum number of snapshots we collect for a single problem. Default is 3. The value must be between 1 and 999.
                    ProblemCounterResetInterval = TimeSpan.FromMinutes(60),
                    SnapshotsPerTenMinutesLimit = 100,    // Momoey throttling is not working since zipping is introduced.
                    //AgentEndpoint = "https://ppe-test.azureserviceprofiler.net",
                    SnapshotsPerDayLimit = 0,      // The maximum number of snapshots allowed in one day (24 hours). Default is 50. 0 means not limit. The limit must not be negative.
                    SnapshotInLowPriorityThread = false
                };
                //SnapshotCollectorConfiguration configuration = new SnapshotCollectorConfiguration();
                return new SnapshotCollectorTelemetryProcessor(next, configuration);
            }
        }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            // Configure SnapshotCollector from application settings
            services.Configure<SnapshotCollectorConfiguration>(Configuration.GetSection(nameof(SnapshotCollectorConfiguration)));

            // Add SnapshotCollector telemetry processor.
            services.AddSingleton<ITelemetryProcessorFactory>(sp => new SnapshotCollectorTelemetryProcessorFactory(sp));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
