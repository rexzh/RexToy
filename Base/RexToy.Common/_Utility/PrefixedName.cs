using System;
using System.Collections.Generic;

namespace RexToy
{
    public class PrefixedName
    {
        public const char ColonDelimiter = ':';
        public const char DotDelimiter = '.';

        private string _prefix;
        public string Prefix
        {
            get { return _prefix; }
        }

        private string _localName;
        public string LocalName
        {
            get { return _localName; }
        }

        public PrefixedName(string fullName, char delimiter = PrefixedName.ColonDelimiter)
        {
            fullName.ThrowIfNullArgument(nameof(fullName));

            string[] segs = fullName.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
            switch (segs.Length)
            {
                case 1:
                    if (fullName.StartsWith(delimiter))
                    {
                        _prefix = string.Empty;
                        _localName = segs[0];
                    }
                    if (fullName.EndsWith(delimiter))
                    {
                        _prefix = segs[0];
                        _localName = string.Empty;
                    }
                    break;

                case 2:
                    _prefix = segs[0];
                    _localName = segs[1];
                    break;

                default:
                    throw new ArgumentException(string.Format("Invalid prefix name:[{0}].", fullName), "fullName");
            }
        }
    }
}
