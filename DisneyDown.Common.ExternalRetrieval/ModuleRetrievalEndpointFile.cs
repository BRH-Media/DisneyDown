namespace DisneyDown.Common.ExternalRetrieval
{
    public class ModuleRetrievalEndpointFile
    {
        public string FileName { get; set; } = @"";
        public ModuleRetrievalEndpointChecksum Checksum { get; set; } = new ModuleRetrievalEndpointChecksum();
    }
}