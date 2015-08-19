using System;
using System.Collections.Generic;

using RexToy.ORM.Dialect;

namespace RexToy.ORM
{
    public interface IDatabase : IDisposable
    {
        void CreateTable<T>();
        void DropTable<T>();
        void TruncateTable<T>();

        IDatabaseMeta QueryMeta();
    }
}
