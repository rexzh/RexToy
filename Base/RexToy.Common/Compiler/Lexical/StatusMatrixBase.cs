using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RexToy.Compiler.Lexical
{
    public abstract class StatusMatrixBase
    {
        public event EventHandler<LexicalParseEventArgs> TokenTerminate;

        public abstract int GetCharType(char ch);
        public abstract int GetStatusTransform(int origin, char ch);

        private bool _tokenTerminated;
        public bool TokenTerminated
        {
            get { return _tokenTerminated; }
            protected set { _tokenTerminated = value; }
        }

        [SuppressMessage("Microsoft.Design", "CA1030")]
        protected virtual void Fire_TokenTerminate(char ch, int status)
        {
            var temp = TokenTerminate;
            if (temp != null)
                temp(this, new LexicalParseEventArgs(ch, status));
            _tokenTerminated = true;
        }
    }
}
