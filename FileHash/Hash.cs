using System.Security.Cryptography;

namespace FileHash
{
    public static class Hash
    {
        public static string GetFileHash(string path)
        {

            using (FileStream fs = File.OpenRead(path))
            using (MD5 md5 = MD5.Create())
            {
                return BitConverter.ToString(md5.ComputeHash(fs)).Replace("-", string.Empty);
            }
        }
    }
}
