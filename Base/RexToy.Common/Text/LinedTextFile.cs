using System;
using System.Collections.Generic;
using System.IO;

using RexToy.Collections;

namespace RexToy.Text
{
    public class LinedTextFile
    {
        private string _path;
        public LinedTextFile(string path)
        {
            _path = path;
        }

        public IEnumerable<string> ReadLines()
        {
            using (StreamReader reader = new StreamReader(_path))
            {
                while (!reader.EndOfStream)
                {
                    yield return reader.ReadLine();
                }
            }
        }
    }
}
