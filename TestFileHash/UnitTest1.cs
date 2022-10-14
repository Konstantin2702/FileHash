using FileHash;
using FileHash.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace TestFileHash
{
    public class Tests
    {

        List<FileInfo> readWithRightHashes;
        List<FileInfo> readWithWrongHashes;

        [SetUp]
        public void Setup()
        {
            FileInfo fileWithRightHash1 = new FileInfo
            {
                Path = "../../../Data/testFile1.txt",
                Hash = "AE6F066EE24382637BC69E26C18D4FAA"
            };
            FileInfo fileWithRightHash2 = new FileInfo
            {
                Path = "../../../Data/testFile2.txt",
                Hash = "AFEFE44B002F7B835E190DBD498A80D0"
            };
            FileInfo fileWithWrongHash1 = new FileInfo
            {
                Path = "../../../Data/testFile1.txt",
                Hash = "AE6F066EE24382637BC69EDFC18D4FAA"
            };
            FileInfo fileWithWrongHash2 = new FileInfo
            {
                Path = "../../../Data/testFile2.txt",
                Hash = "AFEFE44B002F7B83DF190DBD498A80D0"
            };
            readWithRightHashes = new();
            readWithWrongHashes = new();
            readWithRightHashes.Add(fileWithRightHash1); readWithRightHashes.Add(fileWithRightHash2);
            readWithWrongHashes.Add(fileWithWrongHash1); readWithWrongHashes.Add(fileWithWrongHash2);
        }

        [Test]
        public void TestReadFromJson()
        {
            string pathForTestRead = "../../../Data/testFileJson.json";
            List<FileInfo> testRead = new();
            testRead = FileInfoReader.Read(pathForTestRead).Files.ToList();

            for (int i = 0; i < readWithRightHashes.Count(); i++)
            {
                Assert.AreEqual(readWithRightHashes[i].Hash, testRead[i].Hash);
                Assert.AreEqual(readWithRightHashes[i].Path, testRead[i].Path);
            }
        }

        [Test]
        public void TestHash()
        {
            string rightHash = "AE6F066EE24382637BC69E26C18D4FAA";
            FileInfo file = new FileInfo
            {
                Path = "../../../Data/testFile1.txt"
            };
            string testHash = Hash.GetFileHash(file.Path);
            Assert.AreEqual(rightHash, testHash);
        }

        [Test]
        public void TestRightFilesHashes()
        {
            FileInfoList filesWithRightHash = new();
            filesWithRightHash.Files = readWithRightHashes;
            var test = FileInfoList.GetWrongHashFiles(filesWithRightHash);
            Assert.Zero(test.Count());
        }

        [Test]
        public void TestWrongFilesHashed()
        {
            FileInfoList filesWithWrongHash = new();
            filesWithWrongHash.Files = readWithWrongHashes;
            var test = FileInfoList.GetWrongHashFiles(filesWithWrongHash);
            Assert.AreEqual(readWithWrongHashes.Count(), test.Count());
        }
    }
}