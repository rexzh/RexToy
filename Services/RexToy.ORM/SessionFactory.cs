using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using RexToy.ORM.Configuration;
using RexToy.ORM.MappingInfo;
using RexToy.ORM.Session;
using RexToy.ORM.Dialect;

namespace RexToy.ORM
{
    public static class SessionFactory
    {
        public static ISession OpenSession()
        {
            var cfg = ORMConfig.ORMConfiguration;
            if (cfg.DatabaseCount != 1)
            {
                ExceptionHelper.ThrowMoreThanOneDatabase();
            }
            string databaseId = cfg.GetAllDbIds().First();
            return _OpenSession(databaseId);
        }

        public static ISession OpenSession(string databaseId)
        {
            var cfg = ORMConfig.ORMConfiguration;
            if (!cfg.ExistDatabase(databaseId))
            {
                ExceptionHelper.ThrowNotExistDatabaseId(databaseId);
            }
            return _OpenSession(databaseId);
        }

        private static ISession _OpenSession(string databaseId)
        {
            try
            {
                var info = ORMConfig.ORMConfiguration.GetDatabaseInfo(databaseId);
                IDbConnection cnn = CoreFactory.CreateDbProvider(databaseId).CreateConnection(info.ConnectString);
                ISQLExecutor exe = new SQLExecutor(databaseId, cnn);
                IObjectMapInfoCache cache = CoreFactory.ObjectMapInfoCache;
                IEntityManager mgr = new EntityManager(exe, CoreFactory.CreateDialectProvider(databaseId).CreateMappingSQLEmit(cache));
                IEntityQuery qry = new EntityQuery(exe, CoreFactory.CreateDialectProvider(databaseId).CreateQuerySQLEmit(cache));
                INativeSQL native = new NativeSQL(exe);

                if (cnn.State != ConnectionState.Open)
                    cnn.Open();

                return new SimpleSession(exe, mgr, qry, native);
            }
            catch (Exception ex)
            {
                throw ex.CreateWrapException<SessionException>();
            }
        }
    }
}
