SeaweedFs.Client
============

C# [seaweedfs](https://github.com/chrislusf/seaweedfs) client. [Example](./examples/SeaweedFs.Client.Example) usage.

Quick Start
-------
Setup:

    public class SeaweedOptions
    {
        public string MasterUrl { get; set; }
        public string MasterHttpClientName { get; set; }
        public string FilerUrl { get; set; }
        public string FilerHttpClientName { get; set; }
    }
