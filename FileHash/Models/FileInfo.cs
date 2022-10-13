using Newtonsoft.Json;

namespace FileHash
{
    public class FileInfo
    {
        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }
    }
}
