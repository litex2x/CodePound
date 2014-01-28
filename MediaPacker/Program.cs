using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePound.Utility.MediaPacker
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputPath = string.Empty;
            string outputPath = string.Empty;
            long size = -1;
            float threshold = -1;
            string[] targetPaths = null;
            MediaMover mover = null;
            MediaAnalyzer analyzer = null;

            if (args.Length != 4)
            {
                Console.WriteLine("Missing argument(s).");
                Console.WriteLine("mp <input path> <output path> <size> <threshold>");
            }
            else
            {
                try
                {
                    inputPath = args[0];
                    outputPath = args[1];
                    size = Convert.ToInt64(args[2]);
                    threshold = Convert.ToSingle(args[3]);
                    analyzer = new MediaAnalyzer(inputPath);
                    targetPaths = analyzer.FindBestSet(size, threshold);

                    if (targetPaths.Length == 0)
                    {
                        Console.WriteLine("Could not meet size and threshold requirements.");
                    }
                    else
                    {
                        Console.WriteLine("Moving the following files or directories:");

                        foreach (string path in targetPaths)
                        {
                            Console.WriteLine(string.Format("{0}", path));
                        }

                        mover = new MediaMover(targetPaths);
                        mover.Move(outputPath);
                        Console.WriteLine("Process complete!");
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine("An unexpected exception was thrown.");
                    Console.WriteLine(exception.Message);
                    Console.WriteLine(exception.Source);
                    Console.WriteLine(exception.StackTrace);
                }
            }
        }
    }
}
