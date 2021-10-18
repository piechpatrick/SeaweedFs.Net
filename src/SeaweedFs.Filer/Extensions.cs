// ***********************************************************************
// Assembly         : SeaweedFs.Filer
// Author           : piechpatrick
// Created          : 10-09-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-11-2021
// ***********************************************************************

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using SeaweedFs.Filer.Internals;
using SeaweedFs.Filer.Internals.Operations;
using SeaweedFs.Filer.Store;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;

namespace SeaweedFs.Filer
{
    /// <summary>
    /// Class Extensions.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// The section name
        /// </summary>
        private const string SectionName = "seaweed";
        /// <summary>
        /// Adds the seaweed.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <param name="url">The URL.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddSeaweedFiler(this IServiceCollection serviceCollection, string url)
        {
            serviceCollection.AddMemoryCache();
            serviceCollection.AddHttpClient(url, c =>
            {
                c.BaseAddress = new Uri(url);
                c.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            }).AddPolicyHandler(message =>
            {
                return HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                    .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,
                        retryAttempt)));
            });


            FilerClient filerClient = default(FilerClient);
            serviceCollection.AddSingleton<IFilerClient>(sp =>
            {
                var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient(url);
                filerClient = new FilerClient(httpClient);
                return filerClient;
            });
            serviceCollection.AddTransient<IFilerOperationsExecutor>(sp => new FilerOperationsExecutor(filerClient));
            serviceCollection.AddTransient<IFilerStore>(sp =>
            {
                var filerStore = new FilerStore(sp.GetRequiredService<IFilerClient>(), sp.GetRequiredService<IFilerOperationsExecutor>());
                return filerStore;
            });

            return serviceCollection;
        }


        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <typeparam name="TModel">The type of the t model.</typeparam>
        /// <param name="serviceCollection">The service collection.</param>
        /// <param name="sectionName">Name of the section.</param>
        /// <returns>TModel.</returns>
        public static TModel GetOptions<TModel>(this IServiceCollection serviceCollection, string sectionName) where TModel : class, new()
        {
            using var serviceProvider = serviceCollection.BuildServiceProvider();
            var configuration = serviceProvider.GetService<IConfiguration>();
            return configuration.GetOptions<TModel>(sectionName);
        }
        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <typeparam name="TModel">The type of the t model.</typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <param name="sectionName">Name of the section.</param>
        /// <returns>TModel.</returns>
        public static TModel GetOptions<TModel>(this IConfiguration configuration, string sectionName)
            where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(sectionName).Bind(model);
            return model;
        }
        /// <summary>
        /// Adds the factory.
        /// </summary>
        /// <typeparam name="TService">The type of the t service.</typeparam>
        /// <typeparam name="TImplementation">The type of the t implementation.</typeparam>
        /// <param name="serviceCollection">The service collection.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddFactory<TService, TImplementation>(this IServiceCollection serviceCollection)
            where TService : class
            where TImplementation : class, TService
        {
            serviceCollection.AddTransient<TService, TImplementation>();
            serviceCollection.AddSingleton<Func<TService>>(x => () => x.GetService<TService>());
            return serviceCollection;
        }


    }
}