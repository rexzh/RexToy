using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using RexToy.ORM.MappingInfo;
using RexToy.ORM.QueryModel;

namespace RexToy.ORM.Dialect
{
    public abstract class AbstractFilterExpressionVisitor : ExpressionVisitor, IFilterExpressionVisitor
    {
        protected ISQLTranslator _tr;
        protected IObjectMapInfoCache _cache;
        protected AbstractFilterExpressionVisitor(ISQLTranslator tr, IObjectMapInfoCache cache)
        {
            tr.ThrowIfNullArgument(nameof(tr));
            cache.ThrowIfNullArgument(nameof(cache));
            _tr = tr;
            _cache = cache;
        }

        #region IFilterExpressionVisitor Members

        protected StringBuilder _str;
        protected IEnumerable<SingleEntityView> _svList;
        public virtual StringBuilder GetWhereClause(Expression expr, IEnumerable<SingleEntityView> svList)
        {
            expr.ThrowIfNullArgument(nameof(expr));
            svList.ThrowIfNullArgument(nameof(svList));
            
            _svList = svList;
            _str = new StringBuilder();
            Visit(expr);
            return _str;
        }

        #endregion

        protected override Expression VisitLambda<T>(Expression<T> l)
        {
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
            string name = p.ChooseNameFromView(_svList, _cache);
            _str.Append(_tr.GetEscapedTableName(name));

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
                case ExpressionType.NotEqual:
                    if (b.Right.IsConstantNullExpression())
                        _str.Append(_tr.IsNot);
                    else
                        _str.Append(_tr.NotEqual);
                    break;
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                    _str.Append(_tr.GetCompareOperator(b.NodeType));
                    break;
                default:
                    ParseExceptionHelper.ThrowNotSupportedExpression(b);
                    break;
            }
            this.Visit(b.Right);
            _str.Append(StringPair.Parenthesis.End);
            return b;
        }

        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            if (m.Method.DeclaringType.Name == ExtensionFunctionNames.EXT_CLASS)
            {
                _str.Append(StringPair.Parenthesis.Begin);

                switch (m.Method.Name)
                {
                    case ExtensionFunctionNames.FUNC_TRUE:
                        string v = _tr.GetValueString(1);
                        _str.Append(v).Append(_tr.Equal).Append(v);
                        break;

                    case ExtensionFunctionNames.FUNC_FALSE:
                        string v1 = _tr.GetValueString(1);
                        string v0 = _tr.GetValueString(0);
                        _str.Append(v1).Append(_tr.Equal).Append(v0);
                        break;

                    case ExtensionFunctionNames.FUNC_IN:
                        Visit(m.Arguments[0]);//Note:Extension method, visit the 1st param first.
                        _str.Append(_tr.In);
                        _str.Append(StringPair.Parenthesis.Begin);

                        //Note: the 2nd param is an IEnumerable(params T[] is also IEnumerable).
                        IEnumerable seq = (m.Arguments[1] as ConstantExpression).Value as IEnumerable;
                        foreach (object item in seq)
                            _str.Append(_tr.GetValueString(item)).Append(_tr.ColumnDelimiter);

                        _str.RemoveEnd(_tr.ColumnDelimiter);

                        _str.Append(StringPair.Parenthesis.End);

                        break;

                    case ExtensionFunctionNames.FUNC_LIKE:
                        Visit(m.Arguments[0]);//Note:Extension method, visit the 1st param first.
                        _str.Append(_tr.Like);

                        object pattern = (m.Arguments[1] as ConstantExpression).Value;
                        _str.Append(_tr.GetValueString(pattern));

                        break;

                    default:
                        ParseExceptionHelper.ThrowNotSupportedExpression(m);
                        break;
                }

                _str.Append(StringPair.Parenthesis.End);
                return m;
            }

            ParseExceptionHelper.ThrowNotSupportedExpression(m);
            return m;
        }

        protected override Expression VisitUnary(UnaryExpression u)
        {
            switch (u.NodeType)
            {
                case ExpressionType.Not:
                    _str.Append(_tr.Not);
                    this.Visit(u.Operand);
                    break;

                case ExpressionType.Convert:
                    this.Visit(u.Operand);
                    break;

                default:
                    ParseExceptionHelper.ThrowNotSupportedExpression(u);
                    break;
            }
            return u;
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            if (c.Value == null)
                _str.Append(_tr.Null);
            else
                _str.Append(_tr.GetValueString(c.Value));
            return c;
        }
    }
}
