using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

using RexToy.ORM.Session;

namespace RexToy.ORM.Dialect.OleDb
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
            var meta = new DatabaseMeta(new TypeMap());

            var cnn = _exe.Connection as OleDbConnection;
            DataTable dtTable = cnn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            foreach (DataRow dr in dtTable.Rows)
            {
                string name = (string)dr["TABLE_NAME"];
                meta.AddTable(name);
            }

            DataTable dtColumn = cnn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, null, null });
            DataTable dtPK = cnn.GetOleDbSchemaTable(OleDbSchemaGuid.Primary_Keys, new object[] { });
            foreach (DataRow dr in dtColumn.Rows)
            {
                string tblName = (string)dr["TABLE_NAME"];
                string colName = (string)dr["COLUMN_NAME"];

                bool isPK = false;
                foreach (DataRow r in dtPK.Rows)
                {
                    if ((string)r["TABLE_NAME"] == tblName && (string)r["COLUMN_NAME"] == colName)
                        isPK = true;
                }

                bool nullable = (bool)dr["IS_NULLABLE"];
                string dbType = ((int)dr["DATA_TYPE"]).ToString();

                meta.AddColumn(tblName, colName, dbType, nullable, isPK, 0);//Extend: length
            }

            meta.Build();
            return meta;
        }
    }
}
