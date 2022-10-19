using FileHash.Models;
namespace FileHash
{
    public static class HashGetHandler
    {
        private static readonly object Locker = new();
        public static IEnumerable<string> GetWrongHashFiles(IEnumerable<FileInformation> files, Log log)
        {
            lock (Locker)
            {
                foreach (var file in files)
                {
                    if (File.Exists(file.Path))
                    {
                        var computeHash = Hash.ComputeFileHash(file.Path);
                        if (!computeHash.Contains("--"))
                        {
                            if (!file.IsEqualHash(computeHash))
                            {
                                yield return file.Path;
                            }
                        }
                        else
                        {
                            log.Exceptions.Add(computeHash);
                        }
                    }
                    else
                    {
                        log.Exceptions.Add("--" + "File doesn't exist" + " at " + file.Path);
                    }
                }
            }
        }
    }
}
