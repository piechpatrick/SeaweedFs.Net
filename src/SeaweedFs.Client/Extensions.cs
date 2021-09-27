using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SeaweedFs.Client.Core;
using SeaweedFs.Client.Core.Master;
using SeaweedFs.Client.Infrastructure;
using System;
using System.Net.Http.Headers;
using System.Net.Mime;

namespace SeaweedFs.Client
{
    public static class Extensions
    {
        private const string SectionName = "seaweed";
        public static IServiceCollection AddSeaweed(this IServiceCollection serviceCollection, string sectionName = SectionName)
        {

            if (string.IsNullOrEmpty(sectionName))
            {
                sectionName = SectionName;
            }

            using var serviceProvider = serviceCollection.BuildServiceProvider();

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

            serviceCollection.AddTransient<IMasterHttpRequestHandler, MasterHttpRequestHandler>();
            serviceCollection.AddTransient<IFilerHttpRequestHandler, FilerHttpRequestHandler>();
            serviceCollection.AddTransient<IMaster, Master>();
            serviceCollection.AddTransient<IFiler, Filer>();
            serviceCollection.AddTransient<ISeaweed, Seaweed>();
            serviceCollection.AddFactory<IFilerHttpRequestHandler, FilerHttpRequestHandler>();

            return serviceCollection;
        }


        public static TModel GetOptions<TModel>(this IServiceCollection serviceCollection, string sectionName) where TModel : class, new()
        {
            using var serviceProvider = serviceCollection.BuildServiceProvider();
            var configuration = serviceProvider.GetService<IConfiguration>();
            return configuration.GetOptions<TModel>(sectionName);
        }
        public static TModel GetOptions<TModel>(this IConfiguration configuration, string sectionName)
            where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(sectionName).Bind(model);
            return model;
        }
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