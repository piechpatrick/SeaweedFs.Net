using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace SeaweedFs.Client.Example
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices(services =>
                    {
                        services.AddControllers();
                        services.AddSwaggerGen(c =>
                        {
                            c.SwaggerDoc("v1", new OpenApiInfo { Title = "SeaweedFs.Client", Version = "v1" });
                        });

                        services.AddSeaweed();
                    })
                   .Configure(app =>
                   {
                       var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
                       if (env.IsDevelopment())
                       {
                           app.UseDeveloperExceptionPage();
                           app.UseSwagger();
                           app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SeaweedFs.Client v1"));
                       }
                       app.UseRouting();
                       app.UseAuthorization();
                       app.UseEndpoints(endpoints =>
                       {
                           endpoints.MapControllers();
                       });
                   });
                });
    }
}
