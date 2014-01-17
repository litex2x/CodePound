using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CodePound.Utility.MediaPacker
{
    public class MediaAnalyzer
    {
        public MediaEntity[] Entities { get; private set; }

        public MediaAnalyzer(string inputPath)
        {
            string[] files = Directory.GetFileSystemEntries(inputPath);

            Entities = new MediaEntity[files.Length];

            for (int index = 0; index < files.Length; index++)
            {
                Entities[index] = new MediaEntity(files[index]);
            }
        }

        public string[] FindBestSet(long max, float threshold)
        {
            int maxCombinations = (int)Math.Pow(2, Entities.Length) - 1;
            string bestCombination = string.Empty;
            string currentBinaryCombination = string.Empty;
            double bestCombinationSize = 0;
            double currentSize = 0;
            List<string> package = new List<string>();

            //  go through each combination
            for (int combination = 1; combination <= maxCombinations; combination++)
            {
                currentSize = 0;
                currentBinaryCombination = Convert.ToString(combination, 2);

                //  find the sum of all the sizes
                for (int index = 0; index < currentBinaryCombination.Length; index++)
                {
                    if (currentBinaryCombination[index] == '1')
                    {
                        currentSize += Entities[currentBinaryCombination.Length - 1 - index].Size;
                    }
                }

                //  Remember the best combination size that is within threshold range
                if (currentSize > bestCombinationSize &&
                    currentSize >= max - (max * threshold) &&
                    currentSize <= max)
                {
                    bestCombination = currentBinaryCombination;
                    bestCombinationSize = currentSize;
                }
            }

            //  gather media paths into list
            for (int index = 0; index < bestCombination.Length; index++)
            {
                if (bestCombination[index] == '1')
                {
                    package.Add(Entities[bestCombination.Length - 1 - index].FilePath);
                }
            }

            return package.ToArray();
        }
    }
}
