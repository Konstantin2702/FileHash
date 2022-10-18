using System.Security.Cryptography;

namespace FileHash
{
    public static class Hash
    {
        public static string ComputeFileHash(string path)
        {
            try
            {
                using var fs = File.OpenRead(path);
                using var md5 = MD5.Create();
                return BitConverter.ToString(md5.ComputeHash(fs)).Replace("-", string.Empty);
            }
            catch
            {
                return null;
            }
        }
    }
}

