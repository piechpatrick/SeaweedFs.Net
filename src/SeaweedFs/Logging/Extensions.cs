// ***********************************************************************
// Assembly         : SeaweedFs
// Author           : piechpatrick
// Created          : 10-09-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-10-2021
// ***********************************************************************

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SeaweedFs.Logging.Options;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeaweedFs.Logging
{
    /// <summary>
    /// Class Extensions.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// The logging level switch
        /// </summary>
        internal static LoggingLevelSwitch LoggingLevelSwitch = new();

        /// <summary>
        /// Uses the logging.
        /// </summary>
        /// <param name="hostBuilder">The host builder.</param>
        /// <param name="loggerOptions">The logger options.</param>
        /// <param name="configure">The configure.</param>
        /// <returns>IHostBuilder.</returns>
        public static IHostBuilder UseLogging(this IHostBuilder hostBuilder,
            LoggerOptions loggerOptions,
            Action<HostBuilderContext, LoggerConfiguration> configure = null)
            => hostBuilder
                .ConfigureServices(services => services.AddSingleton<ILoggingService, LoggingService>())
                .UseSerilog((context, loggerConfiguration) =>
                {
                    MapOptions(loggerOptions, loggerConfiguration, context.HostingEnvironment.EnvironmentName);
                    configure?.Invoke(context, loggerConfiguration);
                });

        /// <summary>
        /// Uses the logging.
        /// </summary>
        /// <param name="webHostBuilder">The web host builder.</param>
        /// <param name="loggerOptions">The logger options.</param>
        /// <param name="configure">The configure.</param>
        /// <returns>IWebHostBuilder.</returns>
        public static IWebHostBuilder UseLogging(this IWebHostBuilder webHostBuilder,
            LoggerOptions loggerOptions,
            Action<WebHostBuilderContext, LoggerConfiguration> configure = null)
            => webHostBuilder
                .ConfigureServices(services => services.AddSingleton<ILoggingService, LoggingService>())
                .UseSerilog((context, loggerConfiguration) =>
                {
                    MapOptions(loggerOptions, loggerConfiguration,
                        context.HostingEnvironment.EnvironmentName);
                    configure?.Invoke(context, loggerConfiguration);
                });

        /// <summary>
        /// Maps the log level handler.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="endpointRoute">The endpoint route.</param>
        /// <returns>IEndpointConventionBuilder.</returns>
        public static IEndpointConventionBuilder MapLogLevelHandler(this IEndpointRouteBuilder builder,
            string endpointRoute = "~/logging/level")
            => builder.MapPost(endpointRoute, LevelSwitch);

        /// <summary>
        /// Maps the options.
        /// </summary>
        /// <param name="loggerOptions">The logger options.</param>
        /// <param name="loggerConfiguration">The logger configuration.</param>
        /// <param name="environmentName">Name of the environment.</param>
        private static void MapOptions(LoggerOptions loggerOptions,
            LoggerConfiguration loggerConfiguration, string environmentName)
        {
            LoggingLevelSwitch.MinimumLevel = GetLogEventLevel(loggerOptions.Level);

            loggerConfiguration.Enrich.FromLogContext()
                .MinimumLevel.ControlledBy(LoggingLevelSwitch)
                .Enrich.WithProperty("Environment", environmentName);

            foreach (var (key, value) in loggerOptions.Tags ?? new Dictionary<string, object>())
            {
                loggerConfiguration.Enrich.WithProperty(key, value);
            }

            foreach (var (key, value) in loggerOptions.MinimumLevelOverrides ?? new Dictionary<string, string>())
            {
                var logLevel = GetLogEventLevel(value);
                loggerConfiguration.MinimumLevel.Override(key, logLevel);
            }

            loggerOptions.ExcludePaths?.ToList().ForEach(p => loggerConfiguration.Filter
                .ByExcluding(Matching.WithProperty<string>("RequestPath", n => n.EndsWith(p))));

            loggerOptions.ExcludeProperties?.ToList().ForEach(p => loggerConfiguration.Filter
                .ByExcluding(Matching.WithProperty(p)));

            Configure(loggerConfiguration, loggerOptions);
        }

        /// <summary>
        /// Configures the specified logger configuration.
        /// </summary>
        /// <param name="loggerConfiguration">The logger configuration.</param>
        /// <param name="options">The options.</param>
        private static void Configure(LoggerConfiguration loggerConfiguration,
            LoggerOptions options)
        {
            var consoleOptions = options.Console ?? new ConsoleOptions();
            var fileOptions = options.File ?? new FileOptions();

            if (consoleOptions.Enabled)
            {
                loggerConfiguration.WriteTo.Console();
            }

            if (fileOptions.Enabled)
            {
                var path = string.IsNullOrWhiteSpace(fileOptions.Path) ? "logs/logs.txt" : fileOptions.Path;
                if (!Enum.TryParse<RollingInterval>(fileOptions.Interval, true, out var interval))
                {
                    interval = RollingInterval.Day;
                }

                loggerConfiguration.WriteTo.File(path, rollingInterval: interval);
            }
        }

        /// <summary>
        /// Gets the log event level.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <returns>LogEventLevel.</returns>
        internal static LogEventLevel GetLogEventLevel(string level)
            => Enum.TryParse<LogEventLevel>(level, true, out var logLevel)
                ? logLevel
                : LogEventLevel.Information;

        /// <summary>
        /// Levels the switch.
        /// </summary>
        /// <param name="context">The context.</param>
        private static async Task LevelSwitch(HttpContext context)
        {
            var service = context.RequestServices.GetService<ILoggingService>();
            if (service is null)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("ILoggingService is not registered. Add UseLogging() to your Program.cs.");
                return;
            }

            var level = context.Request.Query["level"].ToString();

            if (string.IsNullOrEmpty(level))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid value for logging level.");
                return;
            }

            service.SetLoggingLevel(level);

            context.Response.StatusCode = StatusCodes.Status200OK;
        }
    }
}