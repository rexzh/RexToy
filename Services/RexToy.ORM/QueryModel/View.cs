using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RexToy.ORM.QueryModel
{
    public abstract class View
    {
        protected ViewType _viewType;
        public ViewType ViewType
        {
            get { return _viewType; }
        }

        public Query AsQuery()
        {
            Query query = new Query(this, null);
            return query;
        }
    }

    public abstract class SingleEntityView : View
    {
        protected SingleEntityView(Type entityType)
        {
            entityType.ThrowIfNullArgument(nameof(entityType));

            _entityType = entityType;
            this._viewType = ViewType.Single;
        }

        protected string _alias;
        public string Alias
        {
            get { return _alias; }
        }

        protected Type _entityType;
        public Type EntityType
        {
            get { return _entityType; }
        }
    }

    public abstract class JoinView : View
    {
        protected JoinView(View left, JoinType joinType, View right, Expression joinKey)
        {
            left.ThrowIfNullArgument(nameof(left));
            right.ThrowIfNullArgument(nameof(right));
            joinType.ThrowIfEnumOutOfRange();
            joinKey.ThrowIfNullArgument(nameof(joinKey));

            _left = left;
            _right = right;
            _joinType = joinType;
            _joinKey = joinKey;

            this._viewType = ViewType.Join;
        }

        protected View _left;
        public View Left
        {
            get { return _left; }
        }

        protected View _right;
        public View Right
        {
            get { return _right; }
        }

        protected JoinType _joinType;
        public JoinType JoinType
        {
            get { return _joinType; }
        }

        protected Expression _joinKey;
        public Expression JoinKey
        {
            get { return _joinKey; }
        }
    }
}
