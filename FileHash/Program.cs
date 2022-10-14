using FileHash;
using FileHash.Models;
const string PATH = "../../../Data/Data.json";

void PrintWrongHash()
{
    try
    {
        if (File.Exists(PATH))
        {
            FileInfoList files = FileInfoReader.Read(PATH);
            var filesWithWrongHash = FileInfoList.GetWrongHashFiles(files);
            if (files.Files.Count() == 0)
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
            Console.WriteLine($"File doesn't exits at this path {PATH}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

PrintWrongHash();