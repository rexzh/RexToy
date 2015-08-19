using System;
using System.Collections.Generic;
using System.Reflection;

namespace RexToy
{
    /// <summary>
    /// Clr path: the format is
    /// clr-ns://&lt;namespace&gt;, Assembly=&lt;assembly name&gt;
    /// </summary>
    public class ClrClassPath
    {
        public const string CLR_SCHEME = "clr-ns://";
        public const string EXE = ".exe";
        public const string DLL = ".dll";

        public static implicit operator ClrClassPath(string clrPath)
        {
            return new ClrClassPath(clrPath);
        }

        private string _assemblyName;
        public string AssemblyName
        {
            get { return _assemblyName; }
        }

        private string _classPath;
        public string ClassPath
        {
            get { return _classPath; }
        }

        private ClrClassPath(string assemblyName, string classPath)
        {
            _assemblyName = assemblyName;
            _classPath = classPath;
        }

        private ClrClassPath(string path)
        {
            int idx = path.IndexOf(',');
            if (idx < 0)
                ThrowHelper.ThrowInvalidClrClassPath(path);
            _classPath = path.Substring(0, idx).Trim();
            string assemblyPart = path.Substring(idx + 1);
            idx = assemblyPart.IndexOf('=');
            if (idx < 0 || assemblyPart.Substring(0, idx).Trim() != "Assembly")
                ThrowHelper.ThrowInvalidClrClassPath(path);
            _assemblyName = assemblyPart.Substring(idx + 1).Trim();
        }

        public ClrClassPath Navigate(string relativePath)
        {
            string classPath = _classPath + Type.Delimiter + relativePath;
            return new ClrClassPath(_assemblyName, classPath);
        }

        public Type MakeType(string relativePath)
        {
            string cls = _classPath + Type.Delimiter + relativePath;
            Assembly a = Assembly.LoadFrom(Runtime.GetPath(_assemblyName));
            return a.GetType(cls);
        }
    }
}
