using System;
using System.Collections.Generic;

namespace RexToy.Configuration
{
    public static class ConfigFactory
    {
        public static IConfig CreateXmlConfig(string path = "config.xml")
        {
            path.ThrowIfNullArgument(nameof(path));
            string realPath = Runtime.GetPath(path);
            return new XmlConfig(realPath);
        }

        public static IConfig CreateTextConfig(string path = "config.ini")
        {
            path.ThrowIfNullArgument(nameof(path));
            string realPath = Runtime.GetPath(path);
            return new TextConfig(realPath);
        }
    }
}
