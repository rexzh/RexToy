using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using RexToy.ORM.QueryModel;

namespace RexToy.ORM.Dialect.MySQL
{
    class SQLTranslator : ISQLTranslator
    {
        private static StringPair Escaper = StringPair.Create("`", "`");

        public string Equal
        {
            get { return " = "; }
        }

        public string NotEqual
        {
            get { return " <> "; }
        }

        public string Is
        {
            get { return " IS "; }
        }

        public string IsNot
        {
            get { return " IS NOT "; }
        }

        public string Null
        {
            get { return "NULL"; }
        }

        public string Like
        {
            get { return " LIKE "; }
        }

        public string In
        {
            get { return " IN "; }
        }

        public string NotIn
        {
            get { return " NOT IN "; }
        }

        public string And
        {
            get { return " AND "; }
        }

        public string Or
        {
            get { return " OR "; }
        }

        public string Not
        {
            get { return "NOT "; }
        }

        public string Select
        {
            get { return "SELECT "; }
        }

        public string Delete
        {
            get { return "DELETE "; }
        }

        public string Insert
        {
            get { return "INSERT INTO "; }
        }

        public string Update
        {
            get { return "UPDATE "; }
        }

        public string Values
        {
            get { return " VALUES"; }
        }

        public string Set
        {
            get { return " SET "; }
        }

        public string Where
        {
            get { return " WHERE "; }
        }

        public string From
        {
            get { return " FROM "; }
        }

        public string As
        {
            get { return " AS "; }
        }

        public string On
        {
            get { return " ON "; }
        }

        public string OrderBy
        {
            get { return " ORDER BY "; }
        }

        public string Asc
        {
            get { return " ASC"; }
        }

        public string Desc
        {
            get { return " DESC"; }
        }

        public string CountRow
        {
            get { return "Count(0)"; }
        }

        public string DropTable
        {
            get { return "DROP TABLE "; }
        }

        public string TruncateTable
        {
            get { return "TRUNCATE TABLE "; }
        }

        public string CreateTable
        {
            get { return "CREATE TABLE "; }
        }

        public string PrimaryKey
        {
            get { return " PRIMARY KEY"; }
        }

        public string Constraint
        {
            get { return "CONSTRAINT "; }
        }

        public string GetEscapedTableName(string table)
        {
            return table.ToUpper().Bracketing(SQLTranslator.Escaper);
        }

        public string GetEscapedColumnName(string column)
        {
            return column.ToUpper().Bracketing(SQLTranslator.Escaper);
        }

        public string GetValueString(object val)
        {
            if (val == null)
                return this.Null;

            TypeCode code = Type.GetTypeCode(val.GetType());
            switch (code)
            {
                case TypeCode.Boolean:
                    return ((bool)val) ? "1" : "0";

                case TypeCode.DateTime:
                    DateTime dtVal = (DateTime)val;
                    return dtVal.ToString("yyyy-MM-dd HH:mm:ss").Bracketing(StringPair.SingleQuote);

                case TypeCode.String:
                case TypeCode.Char:
                    return val.ToString().Bracketing(StringPair.SingleQuote);
            }

            if (val is Enum)
                return Enum.Format(val.GetType(), val, "D");
            else if (val is Guid)
                return val.ToString().Bracketing(StringPair.SingleQuote);
            else
                return val.ToString();
        }

        public string MemberAccess
        {
            get { return "."; }
        }

        public string ColumnDelimiter
        {
            get { return ", "; }
        }

        public string GetJoinKeyword(JoinType joinType)
        {
            joinType.ThrowIfEnumOutOfRange();

            switch (joinType)
            {
                case JoinType.Inner:
                    return " JOIN ";

                case JoinType.Left:
                    return " LEFT JOIN ";

                case JoinType.Right:
                    return " RIGHT JOIN ";

                case JoinType.Outer:
                    return " OUTER JOIN ";

                default:
                    return string.Empty;
            }
        }

        public string GetCompareOperator(ExpressionType exprType)
        {
            switch (exprType)
            {
                case ExpressionType.GreaterThan:
                    return " > ";

                case ExpressionType.GreaterThanOrEqual:
                    return " >= ";

                case ExpressionType.LessThan:
                    return " < ";

                case ExpressionType.LessThanOrEqual:
                    return " <= ";

                default:
                    ParseExceptionHelper.ThrowNotSupportedExpression(exprType);
                    return string.Empty;
            }
        }
    }
}
