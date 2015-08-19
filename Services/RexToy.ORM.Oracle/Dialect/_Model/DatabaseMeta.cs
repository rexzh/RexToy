using System;
using System.Collections.Generic;
using System.Text;

namespace RexToy.ORM.Dialect.Oracle
{
    class DatabaseMeta : AbstractDatabaseMeta
    {
        public DatabaseMeta(ITypeMap map)
            : base(map)
        {
        }

        protected override string CLRName(string dbName)
        {
            StringBuilder str = new StringBuilder();
            var list = dbName.Split('_');

            foreach (var p in list)
            {
                str.Append(p[0] + p.Substring(1).ToLower());
            }

            return str.ToString();
        }

        protected override ITable BuildTable(string dbName, string clrName, IEnumerable<IColumn> columns)
        {
            return new Table(dbName, clrName, columns);
        }
    }
}
