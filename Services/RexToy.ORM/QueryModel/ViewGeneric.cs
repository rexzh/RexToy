using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RexToy.ORM.QueryModel
{
    #region View<T1>
    public class View<T1> : SingleEntityView
    {
        public View()
            : base(typeof(T1))
        {
        }

        internal View(string alias)
            : base(typeof(T1))
        {
            if (string.IsNullOrWhiteSpace(alias))
                throw new ArgumentException(alias, "alias");
            _alias = alias;
        }

        public View<T1> As(string alias)
        {
            return new View<T1>(alias);
        }

        public View<T1, T2> Join<T2>(View<T2> query, Expression<Func<T1, T2, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2>(this, JoinType.Inner, query, joinKey);
        }

        public View<T1, T2> LeftJoin<T2>(View<T2> query, Expression<Func<T1, T2, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2>(this, JoinType.Left, query, joinKey);
        }

        public View<T1, T2> RightJoin<T2>(View<T2> query, Expression<Func<T1, T2, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2>(this, JoinType.Right, query, joinKey);
        }

        public View<T1, T2> OuterJoin<T2>(View<T2> query, Expression<Func<T1, T2, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2>(this, JoinType.Outer, query, joinKey);
        }

        public View<T1, T2, T3> Join<T2, T3>(View<T2, T3> query, Expression<Func<T1, T2, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3>(this, JoinType.Inner, query, joinKey);
        }

        public View<T1, T2, T3> LeftJoin<T2, T3>(View<T2, T3> query, Expression<Func<T1, T2, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3>(this, JoinType.Left, query, joinKey);
        }

        public View<T1, T2, T3> RightJoin<T2, T3>(View<T2, T3> query, Expression<Func<T1, T2, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3>(this, JoinType.Right, query, joinKey);
        }

        public View<T1, T2, T3> OuterJoin<T2, T3>(View<T2, T3> query, Expression<Func<T1, T2, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3>(this, JoinType.Outer, query, joinKey);
        }

        public View<T1, T2, T3, T4> Join<T2, T3, T4>(View<T2, T3, T4> query, Expression<Func<T1, T2, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4>(this, JoinType.Inner, query, joinKey);
        }

        public View<T1, T2, T3, T4> LeftJoin<T2, T3, T4>(View<T2, T3, T4> query, Expression<Func<T1, T2, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4>(this, JoinType.Left, query, joinKey);
        }

        public View<T1, T2, T3, T4> RightJoin<T2, T3, T4>(View<T2, T3, T4> query, Expression<Func<T1, T2, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4>(this, JoinType.Right, query, joinKey);
        }

        public View<T1, T2, T3, T4> OuterJoin<T2, T3, T4>(View<T2, T3, T4> query, Expression<Func<T1, T2, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4>(this, JoinType.Outer, query, joinKey);
        }

        public View<T1, T2, T3, T4, T5> Join<T2, T3, T4, T5>(View<T2, T3, T4, T5> query, Expression<Func<T1, T2, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5>(this, JoinType.Inner, query, joinKey);
        }

        public View<T1, T2, T3, T4, T5> LeftJoin<T2, T3, T4, T5>(View<T2, T3, T4, T5> query, Expression<Func<T1, T2, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5>(this, JoinType.Left, query, joinKey);
        }

        public View<T1, T2, T3, T4, T5> RightJoin<T2, T3, T4, T5>(View<T2, T3, T4, T5> query, Expression<Func<T1, T2, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5>(this, JoinType.Right, query, joinKey);
        }

        public View<T1, T2, T3, T4, T5> OuterJoin<T2, T3, T4, T5>(View<T2, T3, T4, T5> query, Expression<Func<T1, T2, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5>(this, JoinType.Outer, query, joinKey);
        }

        public View<T1, T2, T3, T4, T5, T6> Join<T2, T3, T4, T5, T6>(View<T2, T3, T4, T5, T6> query, Expression<Func<T1, T2, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5, T6>(this, JoinType.Inner, query, joinKey);
        }

        public View<T1, T2, T3, T4, T5, T6> LeftJoin<T2, T3, T4, T5, T6>(View<T2, T3, T4, T5, T6> query, Expression<Func<T1, T2, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5, T6>(this, JoinType.Left, query, joinKey);
        }

        public View<T1, T2, T3, T4, T5, T6> RightJoin<T2, T3, T4, T5, T6>(View<T2, T3, T4, T5, T6> query, Expression<Func<T1, T2, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5, T6>(this, JoinType.Right, query, joinKey);
        }

        public View<T1, T2, T3, T4, T5, T6> OuterJoin<T2, T3, T4, T5, T6>(View<T2, T3, T4, T5, T6> query, Expression<Func<T1, T2, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5, T6>(this, JoinType.Outer, query, joinKey);
        }
    }
    #endregion

    #region View<T1, T2>
    public class View<T1, T2> : JoinView
    {
        internal View(View<T1> left, JoinType joinType, View<T2> right, Expression<Func<T1, T2, bool>> joinKey)
            : base(left, joinType, right, joinKey)
        {
        }

        public View<T1, T2, T3> Join<T3>(View<T3> query, Expression<Func<T1, T3, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3>(this, JoinType.Inner, query, joinKey);
        }

        public View<T1, T2, T3> LeftJoin<T3>(View<T3> query, Expression<Func<T1, T3, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3>(this, JoinType.Left, query, joinKey);
        }

        public View<T1, T2, T3> RightJoin<T3>(View<T3> query, Expression<Func<T1, T3, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3>(this, JoinType.Right, query, joinKey);
        }

        public View<T1, T2, T3> OuterJoin<T3>(View<T3> query, Expression<Func<T1, T3, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3>(this, JoinType.Outer, query, joinKey);
        }

        public View<T1, T2, T3, T4> Join<T3, T4>(View<T3, T4> query, Expression<Func<T1, T3, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4>(this, JoinType.Inner, query, joinKey);
        }

        public View<T1, T2, T3, T4> LeftJoin<T3, T4>(View<T3, T4> query, Expression<Func<T1, T3, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4>(this, JoinType.Left, query, joinKey);
        }

        public View<T1, T2, T3, T4> RightJoin<T3, T4>(View<T3, T4> query, Expression<Func<T1, T3, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4>(this, JoinType.Right, query, joinKey);
        }

        public View<T1, T2, T3, T4> OuterJoin<T3, T4>(View<T3, T4> query, Expression<Func<T1, T3, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4>(this, JoinType.Outer, query, joinKey);
        }

        public View<T1, T2, T3, T4, T5> Join<T3, T4, T5>(View<T3, T4, T5> query, Expression<Func<T1, T3, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5>(this, JoinType.Inner, query, joinKey);
        }

        public View<T1, T2, T3, T4, T5> LeftJoin<T3, T4, T5>(View<T3, T4, T5> query, Expression<Func<T1, T3, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5>(this, JoinType.Left, query, joinKey);
        }

        public View<T1, T2, T3, T4, T5> RightJoin<T3, T4, T5>(View<T3, T4, T5> query, Expression<Func<T1, T3, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5>(this, JoinType.Right, query, joinKey);
        }

        public View<T1, T2, T3, T4, T5> OuterJoin<T3, T4, T5>(View<T3, T4, T5> query, Expression<Func<T1, T3, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5>(this, JoinType.Outer, query, joinKey);
        }

        public View<T1, T2, T3, T4, T5, T6> Join<T3, T4, T5, T6>(View<T3, T4, T5, T6> query, Expression<Func<T1, T3, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5, T6>(this, JoinType.Inner, query, joinKey);
        }

        public View<T1, T2, T3, T4, T5, T6> LeftJoin<T3, T4, T5, T6>(View<T3, T4, T5, T6> query, Expression<Func<T1, T3, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));            
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5, T6>(this, JoinType.Left, query, joinKey);
        }

        public View<T1, T2, T3, T4, T5, T6> RightJoin<T3, T4, T5, T6>(View<T3, T4, T5, T6> query, Expression<Func<T1, T3, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5, T6>(this, JoinType.Right, query, joinKey);
        }

        public View<T1, T2, T3, T4, T5, T6> OuterJoin<T3, T4, T5, T6>(View<T3, T4, T5, T6> query, Expression<Func<T1, T3, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5, T6>(this, JoinType.Outer, query, joinKey);
        }
    }
    #endregion

    #region View<T1, T2, T3>
    public class View<T1, T2, T3> : JoinView
    {
        internal View(View<T1> left, JoinType joinType, View<T2, T3> right, Expression<Func<T1, T2, bool>> joinKey)
            : base(left, joinType, right, joinKey)
        {
        }

        internal View(View<T1, T2> left, JoinType joinType, View<T3> right, Expression<Func<T1, T3, bool>> joinKey)
            : base(left, joinType, right, joinKey)
        {
        }

        public View<T1, T2, T3, T4> Join<T4>(View<T4> query, Expression<Func<T1, T4, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4>(this, JoinType.Inner, query, joinKey);
        }

        public View<T1, T2, T3, T4> LeftJoin<T4>(View<T4> query, Expression<Func<T1, T4, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4>(this, JoinType.Left, query, joinKey);
        }

        public View<T1, T2, T3, T4> RightJoin<T4>(View<T4> query, Expression<Func<T1, T4, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4>(this, JoinType.Right, query, joinKey);
        }

        public View<T1, T2, T3, T4> OuterJoin<T4>(View<T4> query, Expression<Func<T1, T4, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4>(this, JoinType.Outer, query, joinKey);
        }

        public View<T1, T2, T3, T4, T5> Join<T4, T5>(View<T4, T5> query, Expression<Func<T1, T4, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5>(this, JoinType.Inner, query, joinKey);
        }

        public View<T1, T2, T3, T4, T5> LeftJoin<T4, T5>(View<T4, T5> query, Expression<Func<T1, T4, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5>(this, JoinType.Left, query, joinKey);
        }

        public View<T1, T2, T3, T4, T5> RightJoin<T4, T5>(View<T4, T5> query, Expression<Func<T1, T4, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5>(this, JoinType.Right, query, joinKey);
        }

        public View<T1, T2, T3, T4, T5> OuterJoin<T4, T5>(View<T4, T5> query, Expression<Func<T1, T4, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5>(this, JoinType.Outer, query, joinKey);
        }

        public View<T1, T2, T3, T4, T5, T6> Join<T4, T5, T6>(View<T4, T5, T6> query, Expression<Func<T1, T4, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5, T6>(this, JoinType.Inner, query, joinKey);
        }

        public View<T1, T2, T3, T4, T5, T6> LeftJoin<T4, T5, T6>(View<T4, T5, T6> query, Expression<Func<T1, T4, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5, T6>(this, JoinType.Left, query, joinKey);
        }

        public View<T1, T2, T3, T4, T5, T6> RightJoin<T4, T5, T6>(View<T4, T5, T6> query, Expression<Func<T1, T4, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5, T6>(this, JoinType.Right, query, joinKey);
        }

        public View<T1, T2, T3, T4, T5, T6> OuterJoin<T4, T5, T6>(View<T4, T5, T6> query, Expression<Func<T1, T4, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5, T6>(this, JoinType.Outer, query, joinKey);
        }
    }
    #endregion

    #region View<T1, T2, T3, T4>
    public class View<T1, T2, T3, T4> : JoinView
    {
        internal View(View<T1> left, JoinType joinType, View<T2, T3, T4> right, Expression<Func<T1, T2, bool>> joinKey)
            : base(left, joinType, right, joinKey)
        {
        }

        internal View(View<T1, T2> left, JoinType joinType, View<T3, T4> right, Expression<Func<T1, T3, bool>> joinKey)
            : base(left, joinType, right, joinKey)
        {
        }

        internal View(View<T1, T2, T3> left, JoinType joinType, View<T4> right, Expression<Func<T1, T4, bool>> joinKey)
            : base(left, joinType, right, joinKey)
        {
        }

        public View<T1, T2, T3, T4, T5> Join<T5>(View<T5> query, Expression<Func<T1, T5, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5>(this, JoinType.Inner, query, joinKey);
        }

        public View<T1, T2, T3, T4, T5> LeftJoin<T5>(View<T5> query, Expression<Func<T1, T5, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5>(this, JoinType.Left, query, joinKey);
        }

        public View<T1, T2, T3, T4, T5> RightJoin<T5>(View<T5> query, Expression<Func<T1, T5, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5>(this, JoinType.Right, query, joinKey);
        }

        public View<T1, T2, T3, T4, T5> OuterJoin<T5>(View<T5> query, Expression<Func<T1, T5, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5>(this, JoinType.Outer, query, joinKey);
        }

        public View<T1, T2, T3, T4, T5, T6> Join<T5, T6>(View<T5, T6> query, Expression<Func<T1, T5, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5, T6>(this, JoinType.Inner, query, joinKey);
        }

        public View<T1, T2, T3, T4, T5, T6> LeftJoin<T5, T6>(View<T5, T6> query, Expression<Func<T1, T5, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5, T6>(this, JoinType.Left, query, joinKey);
        }

        public View<T1, T2, T3, T4, T5, T6> RightJoin<T5, T6>(View<T5, T6> query, Expression<Func<T1, T5, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5, T6>(this, JoinType.Right, query, joinKey);
        }

        public View<T1, T2, T3, T4, T5, T6> OuterJoin<T5, T6>(View<T5, T6> query, Expression<Func<T1, T5, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5, T6>(this, JoinType.Outer, query, joinKey);
        }
    }
    #endregion

    #region View<T1, T2, T3, T4, T5>
    public class View<T1, T2, T3, T4, T5> : JoinView
    {
        internal View(View<T1> left, JoinType joinType, View<T2, T3, T4, T5> right, Expression<Func<T1, T2, bool>> joinKey)
            : base(left, joinType, right, joinKey)
        {
        }

        internal View(View<T1, T2> left, JoinType joinType, View<T3, T4, T5> right, Expression<Func<T1, T3, bool>> joinKey)
            : base(left, joinType, right, joinKey)
        {
        }

        internal View(View<T1, T2, T3> left, JoinType joinType, View<T4, T5> right, Expression<Func<T1, T4, bool>> joinKey)
            : base(left, joinType, right, joinKey)
        {
        }

        internal View(View<T1, T2, T3, T4> left, JoinType joinType, View<T5> right, Expression<Func<T1, T5, bool>> joinKey)
            : base(left, joinType, right, joinKey)
        {
        }

        public View<T1, T2, T3, T4, T5, T6> Join<T6>(View<T6> query, Expression<Func<T1, T6, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5, T6>(this, JoinType.Inner, query, joinKey);
        }

        public View<T1, T2, T3, T4, T5, T6> LeftJoin<T6>(View<T6> query, Expression<Func<T1, T6, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5, T6>(this, JoinType.Left, query, joinKey);
        }

        public View<T1, T2, T3, T4, T5, T6> RightJoin<T6>(View<T6> query, Expression<Func<T1, T6, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5, T6>(this, JoinType.Right, query, joinKey);
        }

        public View<T1, T2, T3, T4, T5, T6> OuterJoin<T6>(View<T6> query, Expression<Func<T1, T6, bool>> joinKey)
        {
            query.ThrowIfNullArgument(nameof(query));
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            return new View<T1, T2, T3, T4, T5, T6>(this, JoinType.Outer, query, joinKey);
        }
    }
    #endregion

    #region View<T1, T2, T3, T4, T5, T6>
    public class View<T1, T2, T3, T4, T5, T6> : JoinView
    {
        internal View(View<T1> left, JoinType joinType, View<T2, T3, T4, T5, T6> right, Expression<Func<T1, T2, bool>> joinKey)
            : base(left, joinType, right, joinKey)
        {
        }

        internal View(View<T1, T2> left, JoinType joinType, View<T3, T4, T5, T6> right, Expression<Func<T1, T3, bool>> joinKey)
            : base(left, joinType, right, joinKey)
        {
        }

        internal View(View<T1, T2, T3> left, JoinType joinType, View<T4, T5, T6> right, Expression<Func<T1, T4, bool>> joinKey)
            : base(left, joinType, right, joinKey)
        {
        }

        internal View(View<T1, T2, T3, T4> left, JoinType joinType, View<T5, T6> right, Expression<Func<T1, T5, bool>> joinKey)
            : base(left, joinType, right, joinKey)
        {
        }

        internal View(View<T1, T2, T3, T4, T5> left, JoinType joinType, View<T6> right, Expression<Func<T1, T6, bool>> joinKey)
            : base(left, joinType, right, joinKey)
        {
        }
    }
    #endregion
}
