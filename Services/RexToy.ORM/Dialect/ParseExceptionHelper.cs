using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RexToy.ORM.Dialect
{
    public static class ParseExceptionHelper
    {
        public static void ThrowNotSupportedExpression(MethodCallExpression m)
        {
            throw new ExpressionParseException(string.Format("The method '[{0}.{1}]' is not supported", m.Method.DeclaringType, m.Method.Name));
        }

        public static void ThrowNotSupportedExpression(ExpressionType exprType)
        {
            throw new ExpressionParseException(string.Format("The operator '[{0}]' is not supported", exprType));
        }

        public static void ThrowNotSupportedExpression(UnaryExpression u)
        {
            throw new ExpressionParseException(string.Format("The unary operator '[{0}]' is not supported", u.NodeType));
        }

        public static void ThrowNotSupportedExpression(BinaryExpression b)
        {
            throw new ExpressionParseException(string.Format("The binary operator '[{0}]' is not supported.", b.NodeType));
        }

        public static void ThrowNotSupportedExpression(MemberExpression m)
        {
            throw new ExpressionParseException(string.Format("The member '[{0}]' is not supported.", m.Member.Name));
        }

        public static void ThrowTypeNotDefinedInView(ParameterExpression p)
        {
            throw new ExpressionParseException(string.Format("Parameter [{0}:Type={1}] is not defined in the view of query.", p.Name, p.Type));
        }

        public static void ThrowParameterNameNotMatchViewAlias(string paramName, string alias, Type entityType)
        {
            throw new ExpressionParseException(string.Format("Parameter [{0}] not match View[{1}] Alias [{2}].", paramName, entityType, alias));
        }

        public static void ThrowNoAliasForParameterName(ParameterExpression p)
        {
            throw new ExpressionParseException(string.Format("There is no alias defined for parameter [{0}:Type={1}].", p.Name, p.Type));
        }

        public static void ThrowMultiTypeHaveSameAlias(ParameterExpression p)
        {
            throw new ExpressionParseException(string.Format("Multiple type [{0}] defined in the view have same alias.", p.Type));
        }
    }
}
