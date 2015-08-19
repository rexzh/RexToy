using System;
using System.Collections.Generic;

using RexToy.ORM.Configuration;
using RexToy.ORM.DbAccess;
using RexToy.ORM.Dialect;
using RexToy.ORM.MappingInfo;

namespace RexToy.ORM.Session
{
    public static class CoreFactory
    {
        public static IDbProvider CreateDbProvider(string databaseId)
        {
            databaseId.ThrowIfNullArgument(nameof(databaseId));

            IORMConfig cfg = ORMConfig.ORMConfiguration;
            IDatabaseInfo info = cfg.GetDatabaseInfo(databaseId);

            if (string.IsNullOrEmpty(info.Provider))
            {
                switch (info.DialectId)
                {
                    case Dialect.OLEDB:
                        return new RexToy.ORM.DbAccess.OleDb.DbProvider();
                    case Dialect.MSSQL:
                        return new RexToy.ORM.DbAccess.MSSql.DbProvider();
                    default:
                        CoreFactoryExceptionHelper.ThrowNoDefaultDbProvider(info.DialectId);
                        return null;
                }
            }
            else
            {
                try
                {
                    return Reflector.LoadInstance<IDbProvider>(info.Provider);
                }
                catch (Exception ex)
                {
                    throw ex.CreateWrapException<CoreFactoryException>();
                }
            }
        }

        public static IDialectProvider CreateDialectProvider(string databaseId)
        {
            databaseId.ThrowIfNullArgument(nameof(databaseId));

            IORMConfig cfg = ORMConfig.ORMConfiguration;
            IDatabaseInfo info = cfg.GetDatabaseInfo(databaseId);

            if (string.IsNullOrEmpty(info.DialectProvider))
            {
                switch (info.DialectId)
                {
                    case Dialect.OLEDB:
                        return new RexToy.ORM.Dialect.OleDb.DialectProvider();

                    case Dialect.MSSQL:
                        return new RexToy.ORM.Dialect.MSSql.DialectProvider();

                    default:
                        CoreFactoryExceptionHelper.ThrowNoDefaultDialectProvider(info.DialectId);
                        return null;
                }
            }
            else
            {
                try
                {
                    return Reflector.LoadInstance<IDialectProvider>(info.DialectProvider);
                }
                catch (Exception ex)
                {
                    throw ex.CreateWrapException<CoreFactoryException>();
                }
            }
        }

        private static object _lock = new object();
        private static IObjectMapInfoCache _cache;
        public static IObjectMapInfoCache ObjectMapInfoCache
        {
            get
            {
                lock (_lock)
                {
                    if (_cache == null)
                    {
                        _cache = new ObjectMapInfoCache();
                        MappingInfoLoader.Load(_cache);
                    }

                    return _cache;
                }
            }
            set
            {
                lock (_lock)
                {
                    if (_cache != null)
                    {
                        CoreFactoryExceptionHelper.ThrowMappingInfoAlreadyExist();
                    }
                    else
                    {
                        value.ThrowIfNullArgument(nameof(value));
                        _cache = value;
                        MappingInfoLoader.Load(_cache);
                    }
                }
            }
        }
    }
}
