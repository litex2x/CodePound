using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePound.MediaPacker.UnitTests
{
    public class Helper
    {
        public static string CreateDummyFile(string path, long size)
        {
            string filePath = Path.Combine(path, Path.GetRandomFileName());

            using (FileStream stream = new FileStream(filePath, FileMode.CreateNew))
            {
                stream.SetLength(size);
            }

            return filePath;
        }

        public static string CreateDummyDirectory(string path)
        {
            string directoryPath = Path.Combine(path, Path.GetRandomFileName());

            Directory.CreateDirectory(directoryPath);

            return directoryPath;
        }
    }
}
