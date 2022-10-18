using FileHash;
using FileHash.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace TestFileHash
{
    public class Tests
    {
        List<FileInformation> readWithRightHashes;
        List<FileInformation> readWithWrongHashes;

        [SetUp]
        public void Setup()
        {
            FileInformation fileWithRightHash1 = new FileInformation
            {
                Path = "../../../Data/testFile1.txt",
                Hash = "AE6F066EE24382637BC69E26C18D4FAA"
            };
            FileInformation fileWithRightHash2 = new FileInformation
            {
                Path = "../../../Data/testFile2.txt",
                Hash = "AFEFE44B002F7B835E190DBD498A80D0"
            };
            FileInformation fileWithWrongHash1 = new FileInformation
            {
                Path = "../../../Data/testFile1.txt",
                Hash = "AE6F066EE24382637BC69EDFC18D4FAA"
            };
            FileInformation fileWithWrongHash2 = new FileInformation
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
            List<FileInformation> testRead = new();
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
            FileInformation file = new FileInformation
            {
                Path = "../../../Data/testFile1.txt"
            };
            string testHash = Hash.ComputeFileHash(file.Path);
            Assert.AreEqual(rightHash, testHash);
        }

        [Test]
        public void TestRightFilesHashes()
        {
            FileInfoList filesWithRightHash = new();
            filesWithRightHash.Files = readWithRightHashes;
            var test = HashGetHandler.GetWrongHashFiles(filesWithRightHash.Files);
            Assert.Zero(test.Count());
        }

        [Test]
        public void TestWrongFilesHashed()
        {
            FileInfoList filesWithWrongHash = new();
            filesWithWrongHash.Files = readWithWrongHashes;
            var test = HashGetHandler.GetWrongHashFiles(filesWithWrongHash.Files);
            Assert.AreEqual(readWithWrongHashes.Count(), test.Count());
        }
    }
}