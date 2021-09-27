namespace SeaweedFs.Client.Infrastructure
{
    public class SeaweedOptions
    {
        public string MasterUrl { get; set; }
        public string MasterHttpClientName { get; set; }
        public string FilerUrl { get; set; }
        public string FilerHttpClientName { get; set; }
    }
}