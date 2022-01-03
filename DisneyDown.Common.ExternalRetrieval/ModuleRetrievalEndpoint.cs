namespace DisneyDown.Common.ExternalRetrieval
{
    public class ModuleRetrievalEndpoint
    {
        public string DownloadUrl { get; set; } = @"";
        public string[] FileNames { get; set; } = { };
        public ModuleRetrievalEndpointChecksum Checksum { get; set; } = new ModuleRetrievalEndpointChecksum();
    }
}