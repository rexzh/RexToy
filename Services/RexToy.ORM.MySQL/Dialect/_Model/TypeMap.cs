using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RexToy.ORM.Dialect.MySQL
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
                case TypeCode.String:
                case TypeCode.Char:
                    return " nvarchar";

                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                    return " int ";

                case TypeCode.Int64:
                case TypeCode.UInt64:
                    return " long ";

                case TypeCode.DateTime:
                    return " datetime ";

                case TypeCode.Boolean:
                    return " bit ";

                case TypeCode.Decimal:
                    return " decimal ";

                case TypeCode.Single:
                    return " decimal ";

                case TypeCode.Byte:
                    return " tinyint ";

                default:
                    if (clrType == typeof(Guid))
                        return " varchar(64) ";
                    GenerateExceptionHelper.ThrowUnknownMapType(clrType);
                    return string.Empty;
            }
        }


        public Type MapDbToClr(string dbType)
        {
            switch (dbType)
            {
                case "int":                
                    return typeof(int);

                case "bigint":
                    return typeof(long);

                case "bit":
                    return typeof(bool);

                case "decimal":
                case "numeric":
                    return typeof(decimal);

                case "varchar":
                case "nvarchar":
                case "char":
                case "nchar":
                case "text":
                case "mediumtext":
                    return typeof(string);

                case "float":
                    return typeof(double);

                case "real":
                    return typeof(float);

                case "date":
                case "datetime":
                    return typeof(DateTime);

                case "binary":
                case "varbinary":
                    return typeof(byte[]);

                case "smallint":
                    return typeof(short);

                case "tinyint":
                    return typeof(byte);

                default:
                    GenerateExceptionHelper.ThrowUnknownMapType(dbType);
                    return typeof(void);
            }
        }
    }
}
