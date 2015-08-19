using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using RexToy.ORM.Configuration;
using RexToy.ORM.DbAccess;
using RexToy.ORM.MappingInfo;
using RexToy.ORM.Dialect;
using RexToy.ORM.Session;

namespace RexToy.ORM
{
    public static class DatabaseFactory
    {
        public static IDatabase OpenDatabase()
        {
            var cfg = ORMConfig.ORMConfiguration;
            if (cfg.DatabaseCount != 1)
            {
                ExceptionHelper.ThrowMoreThanOneDatabase();
            }
            string databaseId = cfg.GetAllDbIds().First();
            return _OpenDatabase(databaseId);
        }

        public static IDatabase OpenDatabase(string databaseId)
        {
            var cfg = ORMConfig.ORMConfiguration;
            if (!cfg.ExistDatabase(databaseId))
            {
                ExceptionHelper.ThrowNotExistDatabaseId(databaseId);
            }
            return _OpenDatabase(databaseId);
        }

        private static IDatabase _OpenDatabase(string databaseId)
        {
            try
            {
                var info = ORMConfig.ORMConfiguration.GetDatabaseInfo(databaseId);
                IDbConnection cnn = CoreFactory.CreateDbProvider(databaseId).CreateConnection(info.ConnectString);
                ISQLExecutor exe = new SQLExecutor(databaseId, cnn);
                IObjectMapInfoCache cache = CoreFactory.ObjectMapInfoCache;

                var dialect = CoreFactory.CreateDialectProvider(databaseId);
                IModelSQLEmit emit = dialect.CreateModelSQLEmit(cache);
                IMetaQuery meta = dialect.CreateMetaQuery(exe);

                if (cnn.State != ConnectionState.Open)
                    cnn.Open();
                
                return new Database(exe, emit, meta);
            }
            catch (Exception ex)
            {
                throw ex.CreateWrapException<SessionException>();
            }
        }
    }
}
