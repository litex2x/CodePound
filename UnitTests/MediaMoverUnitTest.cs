using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using CodePound.MediaPacker.ConsoleApplication;

namespace CodePound.MediaPacker.UnitTests
{
    [TestClass]
    public class MediaMoverUnitTest
    {
        public TestContext TestContext { get; set; }
        public string MediaRootDirectory { get; set; }
        public string ExpectedDirectory { get; set; }
        public string OutputDirectory { get; set; }

        [TestInitialize]
        public void TestInit()
        {
            OutputDirectory = Path.Combine(TestContext.TestRunDirectory, Path.GetRandomFileName());
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

            if (Directory.Exists(OutputDirectory))
            {
                Directory.Delete(OutputDirectory, true);
            }
        }

        [TestMethod]
        public void MediaMoverConstructorTest()
        {
            MediaAnalyzer organizer = new MediaAnalyzer(MediaRootDirectory);
            string[] paths = organizer.FindBestSet(1024 * 2, 0.01f);
            MediaMover mover = new MediaMover(paths);
        }

        [TestMethod]
        public void MoveTest()
        {
            MediaAnalyzer organizer = new MediaAnalyzer(MediaRootDirectory);
            string[] originalPaths = organizer.FindBestSet(1024 * 2, 0.01f);
            MediaMover mover = new MediaMover(originalPaths);

            mover.Move(OutputDirectory);

            foreach (string path in originalPaths)
            {
                Assert.IsFalse(File.Exists(path) || Directory.Exists(path));
            }

            for (int i = 0; i < originalPaths.Length; i++)
            {
                originalPaths[i] = originalPaths[i].Replace(MediaRootDirectory, OutputDirectory);
            }

            foreach (string path in originalPaths)
            {
                Assert.IsTrue(File.Exists(path) || Directory.Exists(path));
            }
        }
    }
}
