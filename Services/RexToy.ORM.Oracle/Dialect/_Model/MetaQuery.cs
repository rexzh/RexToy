using System;
using System.Collections.Generic;
using System.Data;

using Oracle.ManagedDataAccess.Client;

using RexToy.ORM.Session;

namespace RexToy.ORM.Dialect.Oracle
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

            var b = new OracleConnectionStringBuilder(_exe.Connection.ConnectionString);

            var dtTable = _exe.ExecuteQuery(string.Format("SELECT * FROM ALL_TABLES WHERE OWNER = '{0}'", b.UserID.ToUpper()));
            foreach (DataRow dr in dtTable.Rows)
            {
                string name = (string)dr["TABLE_NAME"];
                meta.AddTable(name);
            }

            DataTable dtColumn = _exe.ExecuteQuery(string.Format("SELECT * FROM ALL_TAB_COLUMNS WHERE OWNER = '{0}'", b.UserID.ToUpper()));
            string sqlPK = "SELECT * FROM user_cons_columns WHERE constraint_name IN (SELECT constraint_name FROM user_constraints WHERE Constraint_type='P') AND owner='{0}'";
            DataTable dtPK = _exe.ExecuteQuery(string.Format(sqlPK, b.UserID.ToUpper()));

            foreach (DataRow dr in dtColumn.Rows)
            {
                string tblName = (string)dr["TABLE_NAME"];
                string colName = (string)dr["COLUMN_NAME"];
                bool nullable = (string)dr["NULLABLE"] != "N";
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