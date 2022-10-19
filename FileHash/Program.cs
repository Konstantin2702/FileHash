namespace FileHash
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Log log = new();
            var path = args[0];

            if (File.Exists(path))
            {
                var files = FileInfoReader.Read(path, log);
                if (files.Files is not null)
                {
                    var filesWithWrongHash = HashGetHandler.GetWrongHashFiles(files.Files, log);
                    PrintWrongHash(filesWithWrongHash);

                }
            }
            else
            {
                log.Exceptions.Add("File doesn't exists at this path {path}");
            }
            PrintExceptions(log.Exceptions);
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

        private static void PrintExceptions(IEnumerable<string> exceptions)
        {
            if (exceptions.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nErrors in files:");
                Console.ResetColor();
                foreach (var str in exceptions)
                {
                    Console.WriteLine(str);
                }
            }

        }
    }
}



