using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

using RexToy.ORM.QueryModel;

namespace RexToy.ORM.Dialect
{
    public abstract class AbstractQueryCriteriaVisitor : IQueryCriteriaVisitor
    {
        protected ISQLTranslator _tr;
        protected IFilterExpressionVisitor _fv;
        public AbstractQueryCriteriaVisitor(ISQLTranslator tr, IFilterExpressionVisitor fv)
        {
            tr.ThrowIfNullArgument(nameof(tr));
            fv.ThrowIfNullArgument(nameof(fv));

            _tr = tr;
            _fv = fv;
        }

        #region IQueryCriteriaVisitor Members

        protected StringBuilder _str;
        protected IEnumerable<SingleEntityView> _svList;
        public virtual StringBuilder BuildWhereFilters(Criteria criteria, IEnumerable<SingleEntityView> svList)
        {
            criteria.ThrowIfNullArgument(nameof(criteria));
            svList.ThrowIfNullArgument(nameof(svList));

            _svList = svList;
            _str = new StringBuilder();
            Visit(criteria);
            _str.UnBracketing(StringPair.Parenthesis);
            return _str;
        }

        #endregion

        protected void Visit(Criteria criteria)
        {
            switch (criteria.CriteriaType)
            {
                case CriteriaType.Single:
                    Visit((SingleExpressionCriteria)criteria);
                    break;

                case CriteriaType.Composite:
                    Visit((CompositeCriteria)criteria);
                    break;
            }
        }

        protected void Visit(CompositeCriteria criteria)
        {
            _str.Append(StringPair.Parenthesis.Begin);
            Visit(criteria.Left);
            switch (criteria.Logic)
            {
                case LogicType.And:
                    _str.Append(_tr.And);
                    break;

                case LogicType.Or:
                    _str.Append(_tr.Or);
                    break;
            }
            Visit(criteria.Right);
            _str.Append(StringPair.Parenthesis.End);
        }

        protected void Visit(SingleExpressionCriteria criteria)
        {
            _str.Append(_fv.GetWhereClause(criteria.Expression.PartialEval(), _svList));
        }
    }
}
