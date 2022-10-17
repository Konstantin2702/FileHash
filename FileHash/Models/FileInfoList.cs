namespace FileHash.Models
{
    public class FileInfoList
    {
        public IEnumerable<FileInfo> Files { get; set; }

        public static IEnumerable<string> GetWrongHashFiles(FileInfoList fileInfoList)
        {
            List<string> result = new();
            foreach(var file in fileInfoList.Files)
            {
                if (File.Exists(file.Path))
                {
                    if (!file.IsEqualHash(Hash.ComputeFileHash(file.Path)))
                    {
                        result.Add(file.Path);
                    }
                }
                else
                {
                    Console.WriteLine($"File in Data.json with path {file.Path} doesn't exits");
                }
            }
            return result;
        }

        public static void PrintWrongHash(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    FileInfoList files = FileInfoReader.Read(filePath);
                    var filesWithWrongHash = FileInfoList.GetWrongHashFiles(files);
                    if (filesWithWrongHash.Count() == 0)
                    {
                        Console.WriteLine("There are no files with wrong hashes");
                    }
                    else
                    {
                        Console.WriteLine("Files with wrong hashes:");
                        foreach (var path in filesWithWrongHash)
                        {
                            Console.WriteLine(path);
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"File doesn't exits at this path {filePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
