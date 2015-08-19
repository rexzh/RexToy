using System;
using System.Collections.Generic;

namespace RexToy.ORM
{
    static class ExceptionHelper
    {
        public static void ThrowMoreThanOneDatabase()
        {
            throw new SessionException("More than one database defined in configuration file.");
        }

        public static void ThrowNotExistDatabaseId(string databaseId)
        {
            throw new SessionException(string.Format("Database id [{0}] is not defined in configuration.", databaseId));
        }

        public static void ThrowNoTransactionPending()
        {
            throw new SessionException("There is no transaction pending, can not commit or rollback.");
        }

        public static void ThrowTransactionPending()
        {
            throw new SessionException("There is one transaction pending, parallel transaction is not supported.");
        }
    }
}
