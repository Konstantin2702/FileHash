using Newtonsoft.Json;
namespace FileHash.Models
{
    public class FileInfoList
    {
        [JsonProperty("files")]
        public IEnumerable<FileInfo> Files { get; set; }

        public static IEnumerable<string> GetWrongHashFiles(FileInfoList fileInfoList)
        {
            List<string> result = new();
            foreach(var file in fileInfoList.Files)
            {
                if (File.Exists(file.Path))
                {
                    if (!file.IsEqualHash(Hash.GetFileHash(file.Path)))
                    {
                        result.Add(file.Path);
                    }
                }
                else
                {
                    Console.WriteLine($"File doesn't exits at this path {file.Path}");
                }
            }
            return result;
        }
    }
}
