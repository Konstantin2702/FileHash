using FileHash.Models;
using Newtonsoft.Json;

namespace FileHash
{
    public static class FileInfoReader
    {
        public static FileInfoList Read(string path)
        {
            FileInfoList files = new();
            using (StreamReader file = File.OpenText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                files = serializer.Deserialize(file, typeof(FileInfoList)) as FileInfoList;
                return files;
            }
           
        }
    }
}
