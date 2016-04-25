using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RexToy.ORM.MappingInfo;

namespace RexToy.ORM.Dialect
{
    public abstract class AbstractMappingOrderExpressionVisitor : ExpressionVisitor, IMappingOrderExpressionVisitor
    {
        protected ISQLTranslator _tr;
        protected IObjectMapInfo _map;
        protected AbstractMappingOrderExpressionVisitor(ISQLTranslator tr)
        {
            tr.ThrowIfNullArgument(nameof(tr));

            _tr = tr;
        }

        protected StringBuilder _str;
        public string Translate(Expression expression, IObjectMapInfo mapInfo)
        {
            mapInfo.ThrowIfNullArgument(nameof(mapInfo));
            expression.ThrowIfNullArgument(nameof(expression));
            _map = mapInfo;
            _str = new StringBuilder();
            this.Visit(expression);
            return _str.ToString();
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.NodeType == ExpressionType.MemberAccess)
            {
                _str.Append(_tr.GetEscapedColumnName(_map.GetColumnFromProperty(node.Member.Name)));
            }
            else
            {
                ParseExceptionHelper.ThrowNotSupportedExpression(node);
            }
            return node;
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            Visit(node.Body);
            return node;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            
            return node;
        }
    }
}
