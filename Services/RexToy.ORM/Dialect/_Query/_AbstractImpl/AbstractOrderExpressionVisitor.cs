using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

using RexToy.ORM.MappingInfo;
using RexToy.ORM.QueryModel;

namespace RexToy.ORM.Dialect
{
    public abstract class AbstractOrderExpressionVisitor : ExpressionVisitor, IOrderExpressionVisitor
    {
        protected ISQLTranslator _tr;
        protected IObjectMapInfoCache _cache;
        protected AbstractOrderExpressionVisitor(ISQLTranslator tr, IObjectMapInfoCache cache)
        {
            tr.ThrowIfNullArgument(nameof(tr));
            cache.ThrowIfNullArgument(nameof(cache));
            _tr = tr;
            _cache = cache;
        }

        protected StringBuilder _str;
        protected IEnumerable<SingleEntityView> _svList;
        public virtual StringBuilder GetOrderByClause(Expression expr, IEnumerable<SingleEntityView> svList)
        {
            svList.ThrowIfNullArgument(nameof(svList));
            _svList = svList;
            _str = new StringBuilder();
            Visit(expr);
            return _str;
        }

        protected override Expression VisitLambda<T>(Expression<T> l)
        {
            Visit(l.Body);
            return l;
        }


        protected override Expression VisitParameter(ParameterExpression p)
        {
            string name = p.ChooseNameFromView(_svList, _cache);
            _str.Append(_tr.GetEscapedTableName(name));
            return p;
        }

        protected override Expression VisitMember(MemberExpression m)
        {
            if (m.Expression != null && m.Expression.NodeType == ExpressionType.Parameter)
            {
                Visit(m.Expression);

                _str.Append(_tr.MemberAccess);
                var map = _cache.GetMapInfo(m.Expression.Type, true);
                string colName = map.GetColumnFromProperty(m.Member.Name);
                _str.Append(_tr.GetEscapedColumnName(colName));
                return m;
            }

            ParseExceptionHelper.ThrowNotSupportedExpression(m);
            return m;
        }
    }
}
