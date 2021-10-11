SeaweedFs.Net
============

C# [seaweedfs](https://github.com/chrislusf/seaweedfs) client. [Example](./examples/SeaweedFs.Client.Example) usage  
Currently- only [Filer](https://github.com/chrislusf/seaweedfs/wiki/Filer-Setup) is implemented  

POC
------- 
 ### IFilerStore
`IFilerStore`- root directory. Entry `http://localhost:8888/`. Basically, it's a `IFilerCatalog` provider        

            /// <summary>
            /// Interface IFilerStore
            /// </summary>
            public interface IFilerStore
            {
                /// <summary>
                /// Gets the catalog.
                /// </summary>
                /// <param name="directory">The directory.</param>
                /// <returns>IFilerCatalog.</returns>
                IFilerCatalog GetCatalog(string directory);
            }
 
 ### IFilerCatalog
`IFilerCatalog`- directory entry from root path eg. `http://localhost:8888/documents`  

              var documentsCatalog = _filerStore.GetCatalog("documents");
              

  
`Subdirectories concept`- mean, that we could create catalog entry with subdirectories. Bear in mind that **subdirectories are not supported** but it's next thing **TODO**     
            
             var documentInvoicesCatalog = _filerStore.GetCatalog("documents/invoices");

It's should be really powerfull with dynamic catalog subdirectories    

            var userInvoiceAttachmentsCat = _filerStore.GetCatalog($"documents/{userId}/invoices/{invoiceId}/attachments");  
                      
           

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

            //get catalog
            var catalog = _filerStore.GetCatalog("documents");

            //delete all files
            var blobInfos = await catalog.ListAsync();
            foreach (var bi in blobInfos)
            {
                await catalog.DeleteAsync(bi);
            }

            //create simple blob
            var blob = new Blob($"{Guid.NewGuid()}.txt", exampleFileStream);

            //add custom header value
            blob.BlobInfo.Headers.Add("Seaweed-OwnerId", $"{Guid.NewGuid()}");

            //push blob
            await catalog.PushAsync(blob);

            //get blob
            using var uploadedBlob = await catalog.GetAsync(blob.BlobInfo.Name);
