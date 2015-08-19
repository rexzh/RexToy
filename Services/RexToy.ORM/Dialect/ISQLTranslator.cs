using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using RexToy.ORM.QueryModel;

namespace RexToy.ORM.Dialect
{
    public interface ISQLTranslator
    {
        string Select { get; }
        string Insert { get; }
        string Values { get; }
        string Update { get; }
        string Set { get; }
        string Delete { get; }
        string From { get; }
        string Where { get; }
        string As { get; }
        string On { get; }
        string OrderBy { get; }
        string Asc { get; }
        string Desc { get; }

        string ColumnDelimiter { get; }
        string MemberAccess { get; }

        string And { get; }
        string Or { get; }
        string Is { get; }
        string IsNot { get; }
        string Null { get; }
        string Not { get; }
        string Equal { get; }
        string NotEqual { get; }

        string NotIn { get; }
        string In { get; }
        string Like { get; }

        string CountRow { get; }

        string CreateTable { get; }
        string DropTable { get; }
        string TruncateTable { get; }
        string Constraint { get; }
        string PrimaryKey { get; }

        string GetCompareOperator(ExpressionType exprType);
        string GetJoinKeyword(JoinType joinType);

        string GetEscapedTableName(string tableName);
        string GetEscapedColumnName(string columnName);
        string GetValueString(object obj);
    }
}