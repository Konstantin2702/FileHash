using Newtonsoft.Json;


namespace FileHash.Models
{
    public class FileInfoList
    {
        [JsonProperty("files")]
        public IEnumerable<FileInfo> Files { get; set; }
    }
}
