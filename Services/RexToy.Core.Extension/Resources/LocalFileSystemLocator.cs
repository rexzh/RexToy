using System;
using System.Collections.Generic;
using System.IO;

namespace RexToy.Resources
{
    class LocalFileSystemLocator : ITargetLocator
    {
        private string _path;
        public LocalFileSystemLocator(string path)
        {
            _path = path;
        }

        public ITargetLocator Combine(string path)
        {
            string p = Path.Combine(_path, path);
            return new LocalFileSystemLocator(p);
        }

        public Stream GetStream(string path, bool throwOnNotFound = false)
        {
            string p = Path.Combine(_path, path);
            if (File.Exists(p))
            {
                return new FileStream(p, FileMode.Open);
            }
            else
            {
                if (throwOnNotFound)
                {
                    ExceptionHelper.ThrowNotFound(p);
                }
                return null;
            }
        }

        public Stream GetStream(bool throwOnNotFound = false)
        {
            if (File.Exists(_path))
            {
                return new FileStream(_path, FileMode.Open);
            }
            else
            {
                if (throwOnNotFound)
                {
                    ExceptionHelper.ThrowNotFound(_path);
                }
                return null;
            }
        }

        public IEnumerable<string> EnumItems()
        {
            DirectoryInfo d = new DirectoryInfo(_path);
            foreach (var file in d.EnumerateFiles())
            {
                yield return file.Name;
            }
        }
    }
}
