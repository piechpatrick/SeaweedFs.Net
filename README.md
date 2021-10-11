SeaweedFs.Client
============

C# [seaweedfs](https://github.com/chrislusf/seaweedfs) client. [Example](./examples/SeaweedFs.Client.Example) usage.

Quick Start
-------
Setup:

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
