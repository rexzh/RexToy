using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

using MySql.Data.MySqlClient;

namespace RexToy.ORM.DbAccess.MySQL
{
    class DbProvider : IDbProvider
    {
        public IDbDataAdapter CreateAdapter(IDbCommand cmd)
        {
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = (MySqlCommand)cmd;
            return da;
        }

        public IDbConnection CreateConnection(string cnnstr)
        {
            return new MySqlConnection(cnnstr);
        }

        public IDbCommand CreateStoredProcedure(string spName)
        {
            IDbCommand cmd = new MySqlCommand(spName);
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }

        public IDbCommand CreateTextCommand(string sql)
        {
            IDbCommand cmd = new MySqlCommand(sql);
            cmd.CommandType = CommandType.Text;
            return cmd;
        }
    }
}
