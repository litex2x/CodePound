using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePound.MediaPacker.ConsoleApplication
{
    public class MediaMover
    {
        private string[] InputPaths { get; set; }

        public MediaMover(string[] inputPaths)
        {
            InputPaths = inputPaths;
        }

        public void Move(string outputPath)
        {
            string[] childPath = InputPaths[0].Split('\\');
            string parentPath = string.Empty;

            parentPath = InputPaths[0].Replace(childPath[childPath.Length - 1], string.Empty);

            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            foreach (string path in InputPaths)
            {
                Directory.Move(path, Path.Combine(outputPath, path.Replace(parentPath, string.Empty)));
            }
        }
    }
}
