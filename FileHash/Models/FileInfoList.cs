using Newtonsoft.Json;


namespace FileHash.Models
{
    public class FileInfoList
    {
        [JsonProperty("files")]
        public IEnumerable<FileInfo> Files { get; set; }

        public static IEnumerable<string> GetWrongHashFiles(FileInfoList fileInfoList)
        {
            return fileInfoList.Files
                .Where(_ => !_.IsEqualHash(Hash.GetFileHash(_.Path)))
                .Select(_ => _.Path).ToList();
        }
    }
}
