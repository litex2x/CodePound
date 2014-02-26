using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using CodePound.Utility.MediaPacker;

namespace MediaPacker.Tests
{
    [TestClass]
    public class MediaOrganizerUnitTest
    {
        public TestContext TestContext { get; set; }
        public string MediaRootDirectory { get; set; }
        public string ExpectedDirectory { get; set; }

        [TestInitialize]
        public void TestInit()
        {
            MediaRootDirectory = Path.Combine(TestContext.TestRunDirectory, Helper.CreateDummyDirectory(TestContext.TestRunDirectory));
            ExpectedDirectory = Path.Combine(MediaRootDirectory, Helper.CreateDummyDirectory(MediaRootDirectory));
            Helper.CreateDummyFile(MediaRootDirectory, 512);
            Helper.CreateDummyFile(MediaRootDirectory, 512);
            Helper.CreateDummyFile(MediaRootDirectory, 512);
            Helper.CreateDummyFile(ExpectedDirectory, 1024 * 2);
        }

        [TestCleanup]
        public void TestClean()
        {
            if (Directory.Exists(MediaRootDirectory))
            {
                Directory.Delete(MediaRootDirectory, true);
            }
        }

        [TestMethod]
        public void MediaAnalyzerConstructorTest()
        {
            MediaAnalyzer set = new MediaAnalyzer(MediaRootDirectory);
            string[] entries = Directory.GetFileSystemEntries(MediaRootDirectory);
            MediaEntity[] elements = new MediaEntity[entries.Length];

            Assert.AreEqual(4, set.Entities.Length);

            for (int i = 0; i < entries.Length; i++)
            {
                elements[i] = new MediaEntity(entries[i]);
                Assert.AreEqual(elements[i].FilePath, set.Entities[i].FilePath);
                Assert.AreEqual(elements[i].Size, set.Entities[i].Size);
            }            
        }

        [TestMethod]
        public void PackTest()
        {
            MediaAnalyzer set = new MediaAnalyzer(MediaRootDirectory);
            string[] actual = set.FindBestSet(1024 * 2, 0.01f);

            Assert.AreEqual(1, actual.Length);
            Assert.AreEqual(ExpectedDirectory, actual[0]);
        }
    }
}
