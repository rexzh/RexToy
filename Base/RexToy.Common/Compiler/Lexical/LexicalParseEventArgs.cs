using System;
using System.Collections.Generic;

namespace RexToy.Compiler.Lexical
{
    public class LexicalParseEventArgs : EventArgs
    {
        private char _ch;
        public char Ch
        {
            get { return _ch; }
        }

        private int _status;
        public int Status
        {
            get { return _status; }
        }

        public LexicalParseEventArgs(char ch, int status)
        {
            this._ch = ch;
            this._status = status;
        }
    }
}
