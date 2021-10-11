SeaweedFs.Client
============

C# [seaweedfs](https://github.com/chrislusf/seaweedfs) client. [Example](./examples/SeaweedFs.Client.Example) usage.

Quick Start
-------
Setup:

             public static IHostBuilder CreateHostBuilder(string[] args) =>
              Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices(services =>
                        {
                            services.AddSwaggerGen(c =>
                            {
                                c.SwaggerDoc("v1", new OpenApiInfo {Title = "SeaweedFs.Client", Version = "v1"});
                            });

                            services.AddSeaweedFiler("http://localhost:8888");
                            services.AddControllers();
                        })
                        .Configure(app =>
                        {
                            var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
                            if (env.IsDevelopment())
                            {
                                app.UseDeveloperExceptionPage();
                                app.UseSwagger();
                                app.UseSwaggerUI(c =>
                                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SeaweedFs.Client v1"));
                            }

                            app.UseRouting();
                            app.UseAuthorization();
                            app.UseEndpoints(endpoints =>
                            {
                                endpoints.MapControllers();
                            });
                        })
                        .UseLogging(LoggerOptions.Default);
                });
            
            
Usage:

            //create simple blob
            var blob = new Blob($"{Guid.NewGuid()}.txt", exampleFileStream);

            //add custom header value
            blob.BlobInfo.Headers.Add("Seaweed-OwnerId", $"{Guid.NewGuid()}");

            //push blob
            await catalog.PushAsync(blob);

            //get blob
            using var uploadedBlob = await catalog.GetAsync(blob.BlobInfo.Name);
