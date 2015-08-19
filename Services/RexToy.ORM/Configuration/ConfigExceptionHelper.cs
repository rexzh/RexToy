using System;

namespace RexToy.ORM.Configuration
{
    static partial class ConfigExceptionHelper
    {
        public static void ThrowDatabaseIdNotDefined()
        {
            throw new ORMConfigException("Config [orm,database] define an empty entry.");
        }

        public static void ThrowDatabaseNotDefined(string dialectId)
        {
            throw new ORMConfigException(string.Format("Database id=[{0}] not defined.", dialectId));
        }

        public static void ThrowMultiDatabaseDefined()
        {
            throw new ORMConfigException("Multi database defined, must provide id.");
        }

        public static void ThrowDialectIdForDatabaseNotDefined(string dbId)
        {
            throw new ORMConfigException(string.Format("Database [{0}] node don't have dialectId attribute.", dbId));
        }

        public static void ThrowNoDbProvider(string dbId, string dialectId)
        {
            throw new ORMConfigException(string.Format("Database [{0}](Dialect=[{1}]) Db provider not defined.", dbId, dialectId));
        }

        public static void ThrowNoDialectProviderDefined(string dialectId)
        {
            throw new ORMConfigException(string.Format("Dialect [{0}] provider not defined.", dialectId));
        }

        public static void ThrowMapFileNotExist(string path)
        {
            throw new ORMConfigException(string.Format("Map file [path={0}] not exist.", path));
        }
    }
}
