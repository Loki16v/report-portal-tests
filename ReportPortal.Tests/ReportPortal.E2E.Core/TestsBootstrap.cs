﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReportPortal.E2E.Core.Extensions;
using ReportPortal.E2E.Core.HttpMessageHandlers;
using ReportPortal.E2E.Core.Logger;
using ReportPortal.E2E.Core.Models;
using Serilog;

namespace ReportPortal.E2E.Core
{
    public sealed class TestsBootstrap
    {
        private static readonly Lazy<TestsBootstrap> Inst = new(() => new TestsBootstrap(), true);

        public readonly IConfigurationRoot Configuration;

        private TestsBootstrap()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", true, false)
                .AddJsonFile("TestUsers.json", true, false)
                .AddJsonFile("RemoteDriverConfig.json", true, false)
                .AddJsonFile("LocalDriverConfig.json", true, false);

            Configuration = builder.Build();
            ServiceProvider = ConfigureServices();
        }

        public static TestsBootstrap Instance => Inst.Value;

        public IServiceProvider ServiceProvider { get; }

        private IServiceProvider ConfigureServices()
        {
            var config = bool.Parse(Configuration.GetSection("RemoteRun").GetValueOrThrow())
                ? "RemoteRunOptions"
                : "LocalRunOptions";
            var services = new ServiceCollection();
            var defaultUser = new UserCredentials
            {
                UserName = Configuration.GetSection($"{config}:DefaultUser:UserName").GetValueOrThrow(),
                Password = Configuration.GetSection($"{config}:DefaultUser:DefaultPassword").GetValueOrThrow()
            };
            services.AddSingleton(defaultUser);

            var reportPortalConfig = new ReportPortalConfig
            {
                BaseUrl = Configuration.GetSection($"{config}:BaseUrl").GetValueOrThrow(),
                ApiAuthUrl = Configuration.GetSection("ApiAuthUrl").GetValueOrThrow(),
                PersonalProject = Configuration.GetSection($"{config}:PersonalProject").Value
            };
            services.AddSingleton(reportPortalConfig);

            var remoteRunOptionsModel = new RemoteRunOptions
            {
                UserName = Configuration.GetSection("RemoteRunOptions:UserName").Value,
                AccessKey = Configuration.GetSection("RemoteRunOptions:AccessKey").Value,
                RemoteDriverUrl = Configuration.GetSection("RemoteRunOptions:RemoteDriverUrl").Value,
                PlatformName = Configuration.GetSection("RemoteRunOptions:PlatformName").Value,
                ChromeVersion = Configuration.GetSection("RemoteRunOptions:ChromeVersion").Value,
                EdgeVersion = Configuration.GetSection("RemoteRunOptions:EdgeVersion").Value

            };
            services.AddSingleton(remoteRunOptionsModel);

            services.AddLogging(builder => builder.AddSerilog(SerilogFactory
                .CreateSerilogLogger(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Configuration.GetSection("LogsPath").GetValueOrThrow()))));
            services.AddSingleton<ClientsHandler>();
            return services.BuildServiceProvider();
        }

    }
}