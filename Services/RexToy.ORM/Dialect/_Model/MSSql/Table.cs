using System;
using System.Collections.Generic;

namespace RexToy.ORM.Dialect.MSSql
{
    class Table : AbstractTable
    {
        public Table(string dbName, string clrName, IEnumerable<IColumn> columns)
            : base(dbName, clrName, columns)
        {
        }

        public override string PKGenerate
        {
            get
            {
                return "Auto";
            }
        }
    }
}
