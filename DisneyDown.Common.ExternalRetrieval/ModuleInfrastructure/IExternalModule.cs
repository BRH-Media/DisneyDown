namespace DisneyDown.Common.ExternalRetrieval.ModuleInfrastructure
{
    public interface IExternalModule
    {
        public bool DownloadAndProcess();

        public byte[] FetchArchive();
    }
}