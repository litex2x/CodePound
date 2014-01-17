using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using CodePound.Utility.MediaPacker;

namespace MediaPacker.Tests
{
    [TestClass]
    public class MediaEntityUnitTest
    {
        public TestContext TestContext { get; set; }
        public string MediaRootDirectory { get; set; }

        [TestInitialize]
        public void TestInit()
        {
            string sub = string.Empty;

            MediaRootDirectory = Path.Combine(TestContext.TestRunDirectory, Helper.CreateDummyDirectory(TestContext.TestRunDirectory));
            sub = Path.Combine(MediaRootDirectory, Helper.CreateDummyDirectory(MediaRootDirectory));
            Helper.CreateDummyFile(MediaRootDirectory, 1024);
            Helper.CreateDummyFile(MediaRootDirectory, 1024);
            Helper.CreateDummyFile(sub, 1024 * 2);
        }

        [TestCleanup]
        public void TestClean()
        {
            if (Directory.Exists(MediaRootDirectory))
            {
                Directory.Delete(MediaRootDirectory, true);
            }
        }

        /// <summary>
        /// Asserts the explicit constructor sets the publics properties properly
        /// </summary>
        [TestMethod]
        public void MediaEntityConstructorTest()
        {
            MediaEntity element = new MediaEntity(MediaRootDirectory);

            Assert.AreEqual(MediaRootDirectory, element.FilePath);
            Assert.AreEqual(element.Size, 1024 * 4);
        }
    }
}
