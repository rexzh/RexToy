using System;
using System.Diagnostics.CodeAnalysis;

namespace RexToy
{
    public class StringPair
    {
        /// <summary>
        /// Represent &lt; &gt;
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104")]
        public static readonly StringPair AngleBracket = new StringPair("<", ">");

        /// <summary>
        /// Represent [ ]
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104")]        
        public static readonly StringPair SquareBracket = new StringPair("[", "]");

        /// <summary>
        /// Represent ( )
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104")]
        public static readonly StringPair Parenthesis = new StringPair("(", ")");

        /// <summary>
        /// Represent { }
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104")]
        public static readonly StringPair CurlyBracket = new StringPair("{", "}");

        /// <summary>
        /// Represent ' '
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104")]
        public static readonly StringPair SingleQuote = new StringPair("'", "'");

        /// <summary>
        /// Represent " "
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104")]
        public static readonly StringPair DoubleQuote = new StringPair("\"", "\"");

        /// <summary>
        /// Represent #{ }
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104")]
        public static readonly StringPair Template_Bracket = new StringPair("#{", "}");

        /// <summary>
        /// Represent ${ }
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104")]
        public static readonly StringPair Delayed_Template_Bracket = new StringPair("${", "}");

        public static StringPair Create(string begin, string end)
        {
            return new StringPair(begin, end);
        }

        private StringPair(string begin, string end)
        {
            begin.ThrowIfNullArgument(nameof(begin));
            end.ThrowIfNullArgument(nameof(end));

            _begin = begin;
            _end = end;
        }

        private string _begin;
        public string Begin
        {
            get { return _begin; }
        }

        private string _end;
        public string End
        {
            get { return _end; }
        }
    }
}
