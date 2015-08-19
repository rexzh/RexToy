using System;
using System.Collections.Generic;

namespace RexToy.ORM.Dialect
{
    public interface ITable
    {
        IEnumerable<IColumn> Columns { get; }
        string DbName { get; }
        string CLRName { get; }
        string PrimaryKey { get; }

        string PKGenerate { get; }
    }
}
