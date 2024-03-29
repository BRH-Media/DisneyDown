﻿namespace DisneyDown.Common.ExternalRetrieval
{
    public class ModuleRetrievalEndpoint
    {
        public string DownloadUrl { get; set; } = @"";
        public ModuleRetrievalEndpointFile[] Files { get; set; } = { };
        public ModuleRetrievalEndpointChecksum Checksum { get; set; } = new ModuleRetrievalEndpointChecksum();
    }
}