using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using RexToy.ORM.QueryModel;

namespace RexToy.ORM
{
    public static class CriteriaExtension
    {
        public static Criteria And(this Criteria left, Criteria right)
        {
            left.ThrowIfNullArgument(nameof(left));
            right.ThrowIfNullArgument(nameof(right));

            return new CompositeCriteria(left, LogicType.And, right);
        }

        public static Criteria Or(this Criteria left, Criteria right)
        {
            left.ThrowIfNullArgument(nameof(left));
            right.ThrowIfNullArgument(nameof(right));
            return new CompositeCriteria(left, LogicType.Or, right);
        }
    }
}
