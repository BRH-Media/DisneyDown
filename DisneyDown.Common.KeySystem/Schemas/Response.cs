namespace DisneyDown.Common.KeySystem.Schemas
{
    public class Response
    {
        public Status Status { get; set; }
        public StoredKey[] Data { get; set; }
    }
}