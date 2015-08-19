using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

namespace RexToy
{
    public static class Runtime
    {
        private static string _startupDir;
        public static void SetStartupDirectory(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                SetStartupDirectory(Assembly.GetExecutingAssembly());
                return;
            }

            Assertion.IsTrue(Directory.Exists(path), "The directory [{0}] not exist.", path);
            _startupDir = path;
        }

        public static void SetStartupDirectory(Assembly assembly = null)
        {
            if (assembly == null)
                assembly = Assembly.GetExecutingAssembly();

            if (assembly.GlobalAssemblyCache)
            {
                throw new InvalidOperationException("The assembly is in GAC, can not use to setup startup path.");
            }

            string p = assembly.CodeBase;
            if (!p.StartsWith(LocalFilePath.LOCAL_FILE_SCHEME))
                throw new InvalidOperationException("Assembly not locate in local file system?");

            FileInfo f = new FileInfo(p.RemoveBegin(LocalFilePath.LOCAL_FILE_SCHEME));
            _startupDir = f.DirectoryName;
        }

        public static string StartupDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(_startupDir))
                    SetStartupDirectory();

                return _startupDir;
            }
        }

        public static string GetPath(string path)
        {
            if (string.IsNullOrEmpty(_startupDir))
                SetStartupDirectory();

            return Path.Combine(_startupDir, path);
        }
    }
}
