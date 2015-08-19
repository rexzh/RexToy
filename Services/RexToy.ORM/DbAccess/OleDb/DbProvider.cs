using System;
using System.Data;
using System.Data.OleDb;

namespace RexToy.ORM.DbAccess.OleDb
{
    class DbProvider : IDbProvider
    {
        #region IDbProvider Members

        public IDbConnection CreateConnection(string cnnstr)
        {
            return new OleDbConnection(cnnstr);
        }

        public IDbDataAdapter CreateAdapter(IDbCommand cmd)
        {
            IDbDataAdapter da = new OleDbDataAdapter();
            da.SelectCommand = cmd;
            return da;
        }

        public IDbCommand CreateTextCommand(string sql)
        {
            IDbCommand cmd = new OleDbCommand(sql);
            cmd.CommandType = CommandType.Text;
            return cmd;
        }

        public IDbCommand CreateStoredProcedure(string spName)
        {
            IDbCommand cmd = new OleDbCommand(spName);
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }

        #endregion
    }
}
