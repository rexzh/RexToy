using System;
using System.Collections.Generic;

namespace RexToy.ORM.Dialect.MSSql
{
    class DatabaseMeta : AbstractDatabaseMeta
    {
        public DatabaseMeta(ITypeMap map)
            : base(map)
        {
        }

        protected override string CLRName(string dbName)
        {
            return dbName;
        }

        protected override ITable BuildTable(string dbName, string clrName, IEnumerable<IColumn> columns)
        {
            return new Table(dbName, clrName, columns);
        }
    }
}
