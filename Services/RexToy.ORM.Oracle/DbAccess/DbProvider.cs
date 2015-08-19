using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace RexToy.ORM.DbAccess.Oracle
{
    class DbProvider : IDbProvider
    {
        public IDbConnection CreateConnection(string cnnstr)
        {
            return new OracleConnection(cnnstr);
        }

        public IDbDataAdapter CreateAdapter(IDbCommand cmd)
        {
            OracleDataAdapter da = new OracleDataAdapter();
            da.SelectCommand = (OracleCommand)cmd;
            return da;
        }

        public IDbCommand CreateTextCommand(string sql)
        {
            IDbCommand cmd = new OracleCommand(sql);
            cmd.CommandType = CommandType.Text;
            return cmd;
        }

        public IDbCommand CreateStoredProcedure(string spName)
        {
            IDbCommand cmd = new OracleCommand(spName);
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }
    }
}
