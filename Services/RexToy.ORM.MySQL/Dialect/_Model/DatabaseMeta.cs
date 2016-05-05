using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RexToy.ORM.Dialect.MySQL
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
