using System;
using System.Collections.Generic;
using System.Data;

namespace RexToy.ORM.DbAccess
{
    public interface IDbProvider
    {
        IDbConnection CreateConnection(string cnnstr);
        IDbDataAdapter CreateAdapter(IDbCommand cmd);
        IDbCommand CreateTextCommand(string sql);
        IDbCommand CreateStoredProcedure(string spName);
    }
}
