using System;

using RexToy.ORM.MappingInfo;

namespace RexToy.ORM.Dialect.MSSql
{
    class JoinExpressionVisitor : AbstractJoinExpressionVisitor
    {
        public JoinExpressionVisitor(ISQLTranslator tr, IObjectMapInfoCache cache)
            : base(tr, cache)
        {
        }
    }
}
