using System;
using System.Collections.Generic;

namespace RexToy.ORM.Dialect.OleDb
{
    class TypeMap : ITypeMap
    {
        public string MapClrToDb(Type clrType)
        {
            if (clrType.IsGenericType && clrType.GetGenericTypeDefinition() == typeof(Nullable<>))
                clrType = Nullable.GetUnderlyingType(clrType);

            if (clrType.IsEnum)
                return " int ";

            switch (Type.GetTypeCode(clrType))
            {
                case TypeCode.Char:
                case TypeCode.String:
                    return " text";

                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                    return " int ";

                case TypeCode.Int64:
                case TypeCode.UInt64:
                case TypeCode.Decimal:
                case TypeCode.Single:
                case TypeCode.Double:
                    return " number ";

                case TypeCode.DateTime:
                    return " datetime ";

                case TypeCode.Boolean:
                    return " bit ";

                default:
                    if (clrType == typeof(Guid))
                        return " guid ";
                    else if (clrType == typeof(byte[]))
                        return " binary ";
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
                case "130":
                case "202":
                    return typeof(string);

                case "128":
                    return typeof(byte[]);

                case "20":
                    return typeof(long);

                case "3":
                    return typeof(int);

                case "11":
                    return typeof(bool);

                case "6":
                case "14":
                case "131":
                    return typeof(decimal);

                case "7":
                case "133":
                case "134":
                    return typeof(DateTime);

                case "72":
                    return typeof(Guid);

                case "2":
                    return typeof(short);

                case "4":
                    return typeof(float);

                case "16":
                    return typeof(byte);

                default:
                    GenerateExceptionHelper.ThrowUnknownMapType(dbType);
                    return typeof(void);
            }
        }
    }
}
