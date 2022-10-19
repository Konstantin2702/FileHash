using FileHash.Models;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace FileHash
{
    public static class FileInfoReader
    {
        public static FileInfoList Read(string path, Log log)
        {
            try
            {
                using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                var files = JsonSerializer.Deserialize<FileInfoList>(fs);
                return CheckCorrectRead(files,path, log) ? files : new FileInfoList();
            }
            catch (Exception e)
            {
                log.Exceptions.Add( "--" + e.Message + " at " + path);
                return new FileInfoList();
            }
        }
        private static bool CheckCorrectRead(FileInfoList files, string path, Log log)
        {
            const string pathPattern =
                "^(([a-zA-Z]:|/)/)?(((\\.)|(\\.\\.)|" +
                "([^\\\\/:\\*\\?\"\\|<>\\. ](([^\\\\/:\\*\\?\"\\|<>\\. ])|" +
                "([^\\\\/:\\*\\?\"\\|<>]*[^\\\\/:\\*\\?\"\\|<>\\. ]))?))/)*[^\\\\/:\\*\\?\"\\|<>\\. ](([^\\\\/:\\*\\?\"\\|<>\\. ])" +
                "|([^\\\\/:\\*\\?\"\\|<>]*[^\\\\/:\\*\\?\"\\|<>\\. ]))?$";
            const string hashPattern = @"^[0-9a-f]{32}$";
            if (files is null || files.Files.Any(_ => _.Path.Equals("") || _.Hash.Equals("")))
            {
                log.Exceptions.Add("--" + "wrong syntax of " + path);
                return false;
            }

            var wrongPaths = files.Files.Where(_ => !Regex.IsMatch(_.Path, pathPattern));
            var wrongHashes = files.Files.Where(_ => !Regex.IsMatch(_.Hash, hashPattern, RegexOptions.IgnoreCase));

            foreach (var ex in wrongPaths)
            {
                log.Exceptions.Add("--" + "wrong format of Path at " + ex.Path);
            }
            foreach (var ex in wrongHashes)
            {
                log.Exceptions.Add("--" + "wrong format of Hash at  " + ex.Path);
            }
            return !(wrongPaths.Any() || wrongHashes.Any());


        }
    }


}

