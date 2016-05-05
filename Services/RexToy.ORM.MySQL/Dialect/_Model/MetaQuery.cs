using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MySql.Data.MySqlClient;

using RexToy.ORM.Session;

namespace RexToy.ORM.Dialect.MySQL
{
    class MetaQuery : IMetaQuery
    {
        private ISQLExecutor _exe;
        public MetaQuery(ISQLExecutor exe)
        {
            exe.ThrowIfNullArgument(nameof(exe));
            _exe = exe;
        }

        public IDatabaseMeta ReadMetaInfo()
        {
            AbstractDatabaseMeta meta = new DatabaseMeta(new TypeMap());
            
            var b = new MySqlConnectionStringBuilder(_exe.Connection.ConnectionString);
            

            var dtTable = _exe.ExecuteQuery(string.Format("SELECT * FROM information_schema.tables WHERE table_schema = '{0}'", b.Database));
            foreach (DataRow dr in dtTable.Rows)
            {
                string name = (string)dr["TABLE_NAME"];
                meta.AddTable(name);
            }

            DataTable dtColumn = _exe.ExecuteQuery(string.Format("SELECT * FROM information_schema.columns WHERE table_schema = '{0}'", b.Database));
            string sqlPK = "SELECT * FROM information_schema.columns WHERE table_schema = '{0}' AND column_key = 'PRI'";
            DataTable dtPK = _exe.ExecuteQuery(string.Format(sqlPK, b.Database));

            foreach (DataRow dr in dtColumn.Rows)
            {
                string tblName = (string)dr["TABLE_NAME"];
                string colName = (string)dr["COLUMN_NAME"];
                bool nullable = (string)dr["IS_NULLABLE"] != "N";
                string dbType = (string)dr["DATA_TYPE"];

                bool isPK = false;
                foreach (DataRow row in dtPK.Rows)
                {
                    if ((string)row["COLUMN_NAME"] == colName)
                    {
                        isPK = true;
                        break;
                    }
                }

                meta.AddColumn(tblName, colName, dbType, isPK, nullable, 0);//Extend: length
            }

            meta.Build();
            return meta;
        }
    }
}
