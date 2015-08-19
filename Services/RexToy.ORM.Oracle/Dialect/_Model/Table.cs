using System;
using System.Collections.Generic;

namespace RexToy.ORM.Dialect.Oracle
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
                string g = "seq:";
                foreach(var c in _columns)
                {
                    if (c.IsPrimaryKey)
                        g += "SEQ_" + c.DbName + ';';
                }
                if (g.Length > 0)
                    g = g.RemoveEnd(';');
                return g;
            }
        }
    }
}
