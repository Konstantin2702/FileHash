namespace FileHash
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var path = args[0];
            while (true)
            {
                if (File.Exists(path))
                {
                    var files = FileInfoReader.Read(path);
                    if (files is not null)
                    {
                        var filesWithWrongHash = HashGetHandler.GetWrongHashFiles(files.Files);

                        PrintWrongHash(filesWithWrongHash);
                        if (HashGetHandler.WrongFilePaths.Any())
                        {
                            PrintWrongPaths(HashGetHandler.WrongFilePaths);
                        }

                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Check content of your {path} and run the app again");
                        break;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nFile doesn't exits at this path {path}");
                    Console.ResetColor();
                    Console.WriteLine("Write right path of file");
                    path = Console.ReadLine();
                }
            }
        }

        private static void PrintWrongHash(IEnumerable<string> files)
        {
            if (!files.Any())
            {
                Console.WriteLine("\nThere are no files with wrong hashes.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nFiles with wrong hashes:");
                Console.ResetColor();
                foreach (var path in files)
                {
                    Console.WriteLine(path);
                }
            }
        }

        private static void PrintWrongPaths(IEnumerable<Tuple<string, HashGetHandler.Errors>> wrongPaths)
        {
            if (wrongPaths.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nErrors in files:");
                Console.ResetColor();
                foreach (var tuple in wrongPaths)
                {
                    Console.WriteLine(tuple.Item2 + "\t" + tuple.Item1);
                }
            }

        }
    }
}



