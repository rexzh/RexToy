using System;
using System.Collections.Generic;
using System.Data;

using RexToy.ORM.Session;

namespace RexToy.ORM.Dialect.MSSql
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

            string sqlTable = "SELECT * FROM SysObjects WHERE xtype='u'";
            DataTable dtTable = _exe.ExecuteQuery(sqlTable);

            foreach (DataRow dr in dtTable.Rows)
            {
                string name = (string)dr["NAME"];
                meta.AddTable(name);
            }

            string sqlColumn = "SELECT * FROM INFORMATION_SCHEMA.COLUMNS";
            DataTable dtColumn = _exe.ExecuteQuery(sqlColumn);

            string sqlPK = "SELECT u.TABLE_NAME,u.COLUMN_NAME FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE u " +
                            "INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS t ON t.CONSTRAINT_NAME = u.CONSTRAINT_NAME " +
                            "WHERE t.CONSTRAINT_TYPE = 'PRIMARY KEY'";
            DataTable dtPK = _exe.ExecuteQuery(sqlPK);

            foreach (DataRow dr in dtColumn.Rows)
            {
                string tblName = (string)dr["table_name"];
                string colName = (string)dr["column_name"];
                bool nullable = (string)dr["is_nullable"] == "YES";
                string dbType = (string)dr["data_type"];

                bool isPK = false;
                foreach (DataRow row in dtPK.Rows)
                {
                    if ((string)row["column_name"] == colName && (string)row["table_name"] == tblName)
                    {
                        isPK = true;
                        break;
                    }
                }

                meta.AddColumn(tblName, colName, dbType, nullable, isPK, 0);//Extend: length
            }
            meta.Build();

            return meta;
        }
    }
}
