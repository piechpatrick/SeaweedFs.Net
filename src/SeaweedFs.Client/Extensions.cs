// ***********************************************************************
// Assembly         : SeaweedFs.Client
// Author           : piechpatrick
// Created          : 10-09-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-09-2021
// ***********************************************************************
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http.Headers;
using System.Net.Mime;

namespace SeaweedFs.Client
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
        /// <param name="sectionName">Name of the section.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddSeaweed(this IServiceCollection serviceCollection, string sectionName = SectionName)
        {

            if (string.IsNullOrEmpty(sectionName))
            {
                sectionName = SectionName;
            }

            using var serviceProvider = serviceCollection.BuildServiceProvider();

            serviceCollection.AddMemoryCache();

            var options = serviceCollection.GetOptions<SeaweedOptions>(sectionName);

            serviceCollection.AddSingleton(options);

            serviceCollection.AddHttpClient(options.MasterHttpClientName, c =>
            {
                c.BaseAddress = new Uri(options.MasterUrl);
                c.DefaultRequestHeaders.UserAgent.Add(ProductInfoHeaderValue.Parse(options.MasterHttpClientName));
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            });
            serviceCollection.AddHttpClient(options.FilerHttpClientName, c =>
            {
                c.BaseAddress = new Uri(options.FilerUrl);
                c.DefaultRequestHeaders.UserAgent.Add(ProductInfoHeaderValue.Parse(options.FilerHttpClientName));
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            });

            Console.WriteLine(Figgle.FiggleFonts.Doom.Render($"{options.Name}"));

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