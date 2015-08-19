using System;

using RexToy.ORM.MappingInfo;

namespace RexToy.ORM.Dialect.OleDb
{
    class JoinExpressionVisitor : AbstractJoinExpressionVisitor
    {
        public JoinExpressionVisitor(ISQLTranslator tr, IObjectMapInfoCache cache)
            : base(tr, cache)
        {
        }
    }
}
