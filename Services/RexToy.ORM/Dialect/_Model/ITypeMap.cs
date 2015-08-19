using System;
using System.Collections.Generic;

namespace RexToy.ORM.Dialect
{
    public interface ITypeMap
    {
        string MapClrToDb(Type clrType);
        Type MapDbToClr(string dbType);
    }
}
