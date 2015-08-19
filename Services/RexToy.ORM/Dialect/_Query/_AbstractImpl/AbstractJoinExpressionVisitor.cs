using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

using RexToy.ORM.MappingInfo;
using RexToy.ORM.QueryModel;

namespace RexToy.ORM.Dialect
{
    public abstract class AbstractJoinExpressionVisitor : ExpressionVisitor, IJoinExpressionVisitor
    {
        protected ISQLTranslator _tr;
        protected IObjectMapInfoCache _cache;
        protected AbstractJoinExpressionVisitor(ISQLTranslator tr, IObjectMapInfoCache cache)
        {
            tr.ThrowIfNullArgument(nameof(tr));
            cache.ThrowIfNullArgument(nameof(cache));

            _tr = tr;
            _cache = cache;
        }

        protected SingleEntityView GetMostLeftSingleView(View view)
        {
            View result = view;
            while (result.ViewType == ViewType.Join)
            {
                result = ((JoinView)result).Left;
            }
            return (SingleEntityView)result;
        }
        
        protected void CheckParameterName(string paramName, SingleEntityView view)
        {
            if ((!string.IsNullOrEmpty(view.Alias)) && paramName != view.Alias)
                ParseExceptionHelper.ThrowParameterNameNotMatchViewAlias(paramName, view.Alias, view.EntityType);
        }

        protected StringBuilder _str;
        protected SingleEntityView _left;
        protected SingleEntityView _right;
        public virtual StringBuilder GetJoinEquation(JoinView view)
        {
            _left = GetMostLeftSingleView(view.Left);
            _right = GetMostLeftSingleView(view.Right);

            _str = new StringBuilder();
            this.Visit(view.JoinKey);
            _str.UnBracketing(StringPair.Parenthesis);
            return _str;
        }

        private string _lParamName;
        private string _rParamName;
        protected override Expression VisitLambda<T>(Expression<T> l)
        {
            //Note:This is a join condition, we can sure there is only 2 parameters.
            _lParamName = l.Parameters[0].Name;
            _rParamName = l.Parameters[1].Name;

            CheckParameterName(_lParamName, _left);
            CheckParameterName(_rParamName, _right);

            Visit(l.Body);
            return l;
        }

        protected override Expression VisitMember(MemberExpression m)
        {
            if (m.Expression != null && m.Expression.NodeType == ExpressionType.Parameter)
            {
                Visit(m.Expression);

                var map = _cache.GetMapInfo(m.Expression.Type, true);
                _str.Append(_tr.MemberAccess);
                string colName = map.GetColumnFromProperty(m.Member.Name);
                _str.Append(_tr.GetEscapedColumnName(colName));
                return m;
            }
            ParseExceptionHelper.ThrowNotSupportedExpression(m);
            return m;
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            SingleEntityView op = null;
            if (p.Name == _lParamName)
                op = _left;
            if (p.Name == _rParamName)
                op = _right;

            Assertion.IsNotNull(op, "parameter expression invalid.");

            if (!string.IsNullOrEmpty(op.Alias))
                _str.Append(_tr.GetEscapedTableName(op.Alias));
            else
            {
                var map = _cache.GetMapInfo(p.Type, true);
                _str.Append(_tr.GetEscapedTableName(map.Table.LocalName));
            }

            return p;
        }

        protected override Expression VisitBinary(BinaryExpression b)
        {
            _str.Append(StringPair.Parenthesis.Begin);
            this.Visit(b.Left);
            switch (b.NodeType)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    _str.Append(_tr.And);
                    break;
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    _str.Append(_tr.Or);
                    break;
                case ExpressionType.Equal:
                    if (b.Right.IsConstantNullExpression())
                        _str.Append(_tr.Is);
                    else
                        _str.Append(_tr.Equal);
                    break;
                default:
                    ParseExceptionHelper.ThrowNotSupportedExpression(b);
                    break;
            }
            this.Visit(b.Right);
            _str.Append(StringPair.Parenthesis.End);
            return b;
        }
    }
}
