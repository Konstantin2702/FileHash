using FileHash.Models;
using System.Text.Json;

namespace FileHash
{
    public static class FileInfoReader
    {
        public static FileInfoList Read(string path)
        {
            try
            {
                using var fs = new FileStream(path, FileMode.OpenOrCreate);
                var files = JsonSerializer.Deserialize<FileInfoList>(fs);
                return CheckCorrectRead(files)?files:null;
            }
            catch
            {
                return null;
            }
        }
        private static bool CheckCorrectRead(FileInfoList files)
        {
            return !(files is null || files.Files.Any(_ => _.Path is null || _.Hash is null));
        }
    }

   
}

