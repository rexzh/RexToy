using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RexToy.ORM.Dialect.MySQL
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
