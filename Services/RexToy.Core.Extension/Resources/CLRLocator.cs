using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace RexToy.Resources
{
    class CLRLocator : ITargetLocator
    {
        private string _assemblyName;
        private string _namespace;
        public CLRLocator(string path)
        {
            ClrClassPath clrPath = path;

            _namespace = clrPath.ClassPath;
            _assemblyName = clrPath.AssemblyName;
        }

        private CLRLocator(string assemblyName, string nameSpace)
        {
            _assemblyName = assemblyName;
            _namespace = nameSpace;
        }

        public ITargetLocator Combine(string path)
        {
            return new CLRLocator(_assemblyName, _namespace + Type.Delimiter + path);
        }

        public Stream GetStream(string path, bool throwOnNotFound = false)
        {
            string fullName = _namespace + Type.Delimiter + path;
            Assembly assembly = Reflector.LoadAssembly(_assemblyName);
            Stream s = assembly.GetManifestResourceStream(fullName);
            if (s == null && throwOnNotFound)
                ExceptionHelper.ThrowNotFound(string.Format("{0},Assembly = {1}", fullName, _assemblyName));
            return s;
        }

        public Stream GetStream(bool throwOnNotFound = false)
        {
            Assembly assembly = Reflector.LoadAssembly(_assemblyName);
            Stream s = assembly.GetManifestResourceStream(_namespace);
            if (s == null && throwOnNotFound)
                ExceptionHelper.ThrowNotFound(string.Format("{0},Assembly = {1}", _namespace, _assemblyName));
            return s;
        }

        public IEnumerable<string> EnumItems()
        {
            Assembly assembly = Reflector.LoadAssembly(_assemblyName);
            string[] names = assembly.GetManifestResourceNames();

            foreach (var name in names)
            {
                if (name.StartsWith(_namespace))
                    yield return name.RemoveBegin(_namespace).RemoveBegin(Type.Delimiter);
            }
        }
    }
}
