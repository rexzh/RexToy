using System;

namespace RexToy.Logging
{
    public interface IWriter : IDisposable
    {
        void Write(string msg);
    }
}
