using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using RexToy.ORM.MappingInfo;

namespace RexToy.ORM.Dialect
{
    public interface IMappingConditionExpressionVisitor
    {
        string Translate(Expression expression, IObjectMapInfo mapInfo);
    }
}
