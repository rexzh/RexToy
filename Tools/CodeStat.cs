using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CodeStat
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length <= 1)
            {
                Console.WriteLine("Useage: CodeStat dir ext1[,extN]");
                return;
            }
            string[] exts = args[1].Split(',');

            string curDir = args[0];
            DirectoryInfo dir = new DirectoryInfo(curDir);

            int lines = Count(dir, exts);

            Console.WriteLine(lines);
            //Console.ReadLine();
        }

        static int Count(DirectoryInfo dir, string[] exts)
        {
            int count = 0;
            foreach (DirectoryInfo subdir in dir.GetDirectories())
            {
                count += Count(subdir, exts);
            }

            foreach (FileInfo file in dir.GetFiles())
            {
                foreach (string ext in exts)
                {
                    if (file.Name.EndsWith(ext))
                    {
                        StreamReader sr = new StreamReader(file.FullName);
                        while (sr.ReadLine() != null)
                            count++;
                    }
                }
            }

            return count;
        }
    }
}