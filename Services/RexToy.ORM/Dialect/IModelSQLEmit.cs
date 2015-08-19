using System;
using System.Collections.Generic;

namespace RexToy.ORM.Dialect
{
    public interface IModelSQLEmit
    {
        string CreateTable<T>();
        string DropTable<T>();
        string TruncateTable<T>();
    }
}
