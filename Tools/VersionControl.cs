using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VersionController
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = args[0];
            string ver = args[1];

            DateTime now = DateTime.Now;
            int build = (now - new DateTime(2009, 7, 22)).Days;
            int revision = (int)(now.TimeOfDay.TotalSeconds / 2);
            string version = ver + "." + build + "." + revision;
            Console.WriteLine("Build Version:" + version);

            DirectoryInfo dir = new DirectoryInfo(path);
            foreach (FileInfo file in dir.GetFiles("AssemblyInfo.template", SearchOption.AllDirectories))
            {
                StringBuilder txt = new StringBuilder();
                using (FileStream fs = new FileStream(file.FullName, FileMode.Open, FileAccess.ReadWrite))
                {
                    using (StreamReader reader = new StreamReader(fs))
                    {
                        while (!reader.EndOfStream)
                        {
                            string content = reader.ReadLine();
                            if (content.StartsWith("[assembly: AssemblyVersion("))
                            {
                                content = "[assembly: AssemblyVersion(\"" + ver + ".0.0" +"\")]";
                            }
                            if (content.StartsWith("[assembly: AssemblyFileVersion("))
                            {
                                content = "[assembly: AssemblyFileVersion(\"" + version + "\")]";
                            }
                            txt.AppendLine(content);
                        }
                    }
                }

                using (StreamWriter writer = new StreamWriter(file.DirectoryName + "\\" + "AssemblyInfo.cs", false))
                {
                    writer.Write(txt.ToString());
                    writer.Flush();
                }
            }
        }
    }
}
