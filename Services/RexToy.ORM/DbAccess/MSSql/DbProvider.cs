using System;
using System.Data;
using System.Data.SqlClient;

namespace RexToy.ORM.DbAccess.MSSql
{
    class DbProvider : IDbProvider
    {
        #region IDbProvider Members

        public IDbConnection CreateConnection(string cnnstr)
        {
            return new SqlConnection(cnnstr);
        }

        public IDbDataAdapter CreateAdapter(IDbCommand cmd)
        {
            IDbDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            return da;
        }

        public IDbCommand CreateTextCommand(string sql)
        {
            IDbCommand cmd = new SqlCommand(sql);
            cmd.CommandType = CommandType.Text;
            return cmd;
        }

        public IDbCommand CreateStoredProcedure(string spName)
        {
            IDbCommand cmd = new SqlCommand(spName);
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }

        #endregion
    }
}
