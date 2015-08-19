using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RexToy.ORM.Dialect
{
    public static class GenerateExceptionHelper
    {
        public static void ThrowNoPrimaryKeyDefine(Type entityType)
        {
            throw new SQLGenerateException(string.Format("Type [{0}] does not have primary key defined.", entityType));
        }

        public static void ThrowPrimaryKeyDefineNotSupport(Type entityType)
        {
            throw new SQLGenerateException(string.Format("Primary key define on type [{0}] not support.", entityType));
        }

        public static void ThrowUnknownMapType(Type clrType)
        {
            throw new SQLGenerateException(string.Format("Can not map [{0}] to database type.", clrType.Name));
        }

        public static void ThrowUnknownMapType(string dbType)
        {
            throw new SQLGenerateException(string.Format("Can not map [{0}] to CLR type.", dbType));
        }

        public static void ThrowMustGreaterThanZero()
        {
            throw new SQLGenerateException("Error in paged query: page number and number/page must > 0");
        }

        public static void ThrowPagedQueryMustSpecifyOrder()
        {
            throw new SQLGenerateException("Paged query must have orderby property.");
        }
    }
}
