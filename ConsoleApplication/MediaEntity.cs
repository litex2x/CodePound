using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CodePound.Utility.MediaPacker
{
    public class MediaEntity
    {
        public string FilePath { get; private set; }
        public long Size { get; private set; }

        public MediaEntity(string path)
        {
            FilePath = path;
            Size = GetSize(FilePath);
        }

        private long GetSize(string path)
        {
            long size = 0;
            FileAttributes attributes = File.GetAttributes(path);

            if (attributes.HasFlag(FileAttributes.Directory))
            {
                foreach (string entry in Directory.EnumerateFileSystemEntries(path))
                {
                    size += GetSize(entry);
                }
            }
            else
            {
                size = GetFileSize(path);
            }

            return size;
        }

        private long GetFileSize(string path)
        {
            FileInfo info = new FileInfo(path);

            return info.Length;
        }
    }
}
