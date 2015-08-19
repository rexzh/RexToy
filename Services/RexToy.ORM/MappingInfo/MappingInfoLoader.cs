using System;
using System.Collections.Generic;
using System.IO;

using RexToy.ORM.Configuration;

namespace RexToy.ORM.MappingInfo
{
    public static class MappingInfoLoader
    {
        public static void Load(IObjectMapInfoCache cache)
        {
            cache.ThrowIfNullArgument(nameof(cache));

            DirectoryInfo dir = new DirectoryInfo(Runtime.StartupDirectory);
            var files = dir.EnumerateFiles("*", SearchOption.TopDirectoryOnly);
            foreach (var file in files)
            {
                if (file.Extension == ClrClassPath.DLL || file.Extension == ClrClassPath.EXE)
                {
                    try
                    {
                        AttributeMapLoader loader = new AttributeMapLoader(cache, Runtime.GetPath(file.Name));
                        loader.Load();
                    }
                    catch (BadImageFormatException)
                    {
                    }
                }
            }

            string[] paths = ORMConfig.ORMConfiguration.GetObjectMapPaths();
            foreach (string path in paths)
            {
                XmlMapLoader loader = new XmlMapLoader(cache, path);
                loader.Load();
            }
        }
    }
}
