using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using RexToy.ORM.MappingInfo;

namespace RexToy.ORM.Dialect
{
    public interface IMappingOrderExpressionVisitor
    {
        string Translate(Expression expression, IObjectMapInfo mapInfo);
    }
}
