using System;
using System.Collections.Generic;
using System.Text;

using RexToy.Collections;
using RexToy.ORM.MappingInfo;
using RexToy.ORM.QueryModel;

namespace RexToy.ORM.Dialect
{
    public abstract class AbstractQueryViewVisitor : IQueryViewVisitor
    {
        protected const char SPACE = ' ';

        protected IObjectMapInfoCache _cache;
        protected ISQLTranslator _tr;
        protected IJoinExpressionVisitor _jv;
        protected AbstractQueryViewVisitor(ISQLTranslator tr, IJoinExpressionVisitor jv, IObjectMapInfoCache cache)
        {
            tr.ThrowIfNullArgument(nameof(tr));
            jv.ThrowIfNullArgument(nameof(jv));
            cache.ThrowIfNullArgument(nameof(cache));

            _cache = cache;
            _tr = tr;
            _jv = jv;
        }

        #region IQueryViewVisitor Members

        protected StringBuilder _str;
        public virtual StringBuilder BuildJoinClause(View view)
        {
            view.ThrowIfNullArgument(nameof(view));

            _str = new StringBuilder();
            Visit(view);
            _str.UnBracketing(StringPair.Parenthesis);
            return _str;
        }

        protected List<SingleEntityView> _list;
        public IReadOnlyList<SingleEntityView> VisitAlias(View view)
        {
            _list = new List<SingleEntityView>();
            VisitViewAlias(view);
            return _list;
        }

        #endregion

        private void Visit(View view)
        {
            view.ViewType.ThrowIfEnumOutOfRange();
            switch (view.ViewType)
            {
                case ViewType.Single:
                    Visit(view as SingleEntityView);
                    return;

                case ViewType.Join:
                    Visit(view as JoinView);
                    return;
            }
        }

        private void Visit(SingleEntityView sv)
        {
            var map = _cache.GetMapInfo(sv.EntityType, true);
            _str.Append(_tr.GetEscapedTableName(map.Table.LocalName));
            if (!string.IsNullOrEmpty(sv.Alias))
            {
                _str.Append(SPACE).Append(_tr.GetEscapedTableName(sv.Alias));
            }
        }

        private void Visit(JoinView jv)
        {
            _str.Append(StringPair.Parenthesis.Begin);

            Visit(jv.Left);
            _str.Append(_tr.GetJoinKeyword(jv.JoinType));
            Visit(jv.Right);

            _str.Append(_tr.On);
            _str.Append(_jv.GetJoinEquation(jv));

            _str.Append(StringPair.Parenthesis.End);
        }

        private void VisitViewAlias(View view)
        {
            view.ViewType.ThrowIfEnumOutOfRange();

            switch (view.ViewType)
            {
                case ViewType.Single:
                    VisitViewAlias(view as SingleEntityView);
                    break;

                case ViewType.Join:
                    VisitViewAlias(view as JoinView);
                    break;
            }
        }

        private void VisitViewAlias(SingleEntityView view)
        {
            _list.Add(view);
        }

        private void VisitViewAlias(JoinView view)
        {
            VisitViewAlias(view.Left);
            VisitViewAlias(view.Right);
        }
    }
}
