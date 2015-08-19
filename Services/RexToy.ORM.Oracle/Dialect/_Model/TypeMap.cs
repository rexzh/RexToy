using System;
using System.Collections.Generic;

namespace RexToy.ORM.Dialect.Oracle
{
    class TypeMap : ITypeMap
    {
        #region ITypeMap Members

        public string MapClrToDb(Type clrType)
        {
            if (clrType.IsGenericType && clrType.GetGenericTypeDefinition() == typeof(Nullable<>))
                clrType = Nullable.GetUnderlyingType(clrType);

            if (clrType.IsEnum)
                return " integer ";

            switch (Type.GetTypeCode(clrType))
            {
                case TypeCode.String:
                case TypeCode.Char:
                    return " varchar2";

                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                    return " integer ";

                case TypeCode.Int64:
                case TypeCode.UInt64:
                case TypeCode.Decimal:
                case TypeCode.Single:
                case TypeCode.Double:
                    return " decimal ";

                case TypeCode.DateTime:
                    return " date ";

                case TypeCode.Boolean:
                    return " integer ";

                default:
                    if (clrType == typeof(Guid))
                        return " varchar2(64) ";
                    else if (clrType == typeof(byte[]))
                        return " BLOB ";
                    else
                    {
                        GenerateExceptionHelper.ThrowUnknownMapType(clrType);
                        return string.Empty;
                    }
            }
        }

        public Type MapDbToClr(string dbType)
        {
            switch (dbType)
            {
                case "CHAR":
                case "VARCHAR":
                case "VARCHAR2":
                case "ROWID":
                case "LONG":
                case "CLOB":
                    return typeof(string);

                case "NUMBER":
                    return typeof(decimal);

                case "DATE":
                    return typeof(DateTime);

                case "BLOB":
                case "RAW":
                    return typeof(byte[]);                

                default:
                    GenerateExceptionHelper.ThrowUnknownMapType(dbType);
                    return typeof(void);
            }
        }

        #endregion
    }
}