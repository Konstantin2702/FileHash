using FileHash.Models;
using System.Text.Json;

namespace FileHash
{
    public static class FileInfoReader
    {
        public static FileInfoList Read(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {

                FileInfoList files = JsonSerializer.Deserialize<FileInfoList>(fs);
                return files;
            }
           
        }
    }
}
