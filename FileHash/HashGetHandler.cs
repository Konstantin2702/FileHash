using FileHash.Models;
namespace FileHash
{
    public static class HashGetHandler
    {
        public enum Errors
        {
            WrongPath,
            WrongRead
        }
        public static List<Tuple<string, Errors>> WrongFilePaths { get; set; } = new();

        public static IEnumerable<string> GetWrongHashFiles(IEnumerable<FileInformation> files)
        {
            foreach (var file in files)
            {
                if (File.Exists(file.Path))
                {
                    var computeHash = Hash.ComputeFileHash(file.Path);
                    if (computeHash is not null)
                    {
                        if (!file.IsEqualHash(computeHash))
                        {
                            yield return file.Path;
                        }
                    }
                    else
                    {
                        WrongFilePaths.Add(new Tuple<string, Errors>(file.Path, Errors.WrongRead));
                    }
                }
                else
                {
                    WrongFilePaths.Add(new Tuple<string, Errors>(file.Path, Errors.WrongPath));
                }
            }
        }
    }
}
