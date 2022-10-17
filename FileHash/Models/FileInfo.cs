namespace FileHash
{
    public class FileInfo
    {
        public string Path { get; set; }
   
        public string Hash { get; set; }

        public bool IsEqualHash(string computedHash)
        {
            return Hash.Equals(computedHash, StringComparison.OrdinalIgnoreCase);
        }
    }
}
