using FileHash;
using FileHash.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace TestFileHash
{
    public class Tests
    {
        private List<FileInformation> _readWithRightHashes;
        private List<FileInformation> _readWithWrongHashes;
        private Log _log;

        [SetUp]
        public void Setup()
        {
            _log = new Log();
            var fileWithRightHash1 = new FileInformation
            {
                Path = "../../../Data/testFile1.txt",
                Hash = "AE6F066EE24382637BC69E26C18D4FAA"
            };
            var fileWithRightHash2 = new FileInformation
            {
                Path = "../../../Data/testFile2.txt",
                Hash = "AFEFE44B002F7B835E190DBD498A80D0"
            };
            var fileWithWrongHash1 = new FileInformation
            {
                Path = "../../../Data/testFile1.txt",
                Hash = "AE6F066EE24382637BC69EDFC18D4FAA"
            };
            var fileWithWrongHash2 = new FileInformation
            {
                Path = "../../../Data/testFile2.txt",
                Hash = "AFEFE44B002F7B83DF190DBD498A80D0"
            };
            _readWithRightHashes = new();
            _readWithWrongHashes = new();
            _readWithRightHashes.Add(fileWithRightHash1); _readWithRightHashes.Add(fileWithRightHash2);
            _readWithWrongHashes.Add(fileWithWrongHash1); _readWithWrongHashes.Add(fileWithWrongHash2);
        }

        [Test]
        public void TestReadFromJson()
        {
            var pathForTestRead = "../../../Data/testFileJson.json";
            List<FileInformation> testRead = new();
            testRead = FileInfoReader.Read(pathForTestRead, _log).Files.ToList();

            for (int i = 0; i < _readWithRightHashes.Count(); i++)
            {
                Assert.AreEqual(_readWithRightHashes[i].Hash, testRead[i].Hash);
                Assert.AreEqual(_readWithRightHashes[i].Path, testRead[i].Path);
            }
        }

        [Test]
        public void TestHash()
        {
            var rightHash = "AE6F066EE24382637BC69E26C18D4FAA";
            var file = new FileInformation
            {
                Path = "../../../Data/testFile1.txt"
            };
            var testHash = Hash.ComputeFileHash(file.Path);
            Assert.AreEqual(rightHash, testHash);
        }

        [Test]
        public void TestRightFilesHashes()
        {
            FileInfoList filesWithRightHash = new();
            filesWithRightHash.Files = _readWithRightHashes;
            var test = HashGetHandler.GetWrongHashFiles(filesWithRightHash.Files, _log);
            Assert.Zero(test.Count());
        }

        [Test]
        public void TestWrongFilesHashed()
        {
            FileInfoList filesWithWrongHash = new();
            filesWithWrongHash.Files = _readWithWrongHashes;
            var test = HashGetHandler.GetWrongHashFiles(filesWithWrongHash.Files, _log);
            Assert.AreEqual(_readWithWrongHashes.Count(), test.Count());
        }
    }
}