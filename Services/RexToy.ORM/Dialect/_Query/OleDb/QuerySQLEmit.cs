using System;
using System.Text;
using System.Collections.Generic;

using RexToy.ORM.MappingInfo;
using RexToy.ORM.QueryModel;

namespace RexToy.ORM.Dialect.OleDb
{
    class QuerySQLEmit : AbstractQuerySQLEmit
    {
        const string TOP_N = "TOP {0} ";

        public QuerySQLEmit(IObjectMapInfoCache cache, ISQLTranslator tr, IQueryViewVisitor vv, IQueryColumnBuilder cb, IQueryCriteriaVisitor cv, IQueryOrderVisitor ov)
            : base(cache, tr, vv, cb, cv, ov)
        {
        }

        public override string PagedQuery(IQuery query, uint numberPerPage, uint page)
        {
            query.ThrowIfNullArgument(nameof(query));
            if (page == 0 || numberPerPage == 0)
                GenerateExceptionHelper.ThrowMustGreaterThanZero();

            if (query.Order == null)
                GenerateExceptionHelper.ThrowPagedQueryMustSpecifyOrder();

            try
            {
                IReadOnlyList<SingleEntityView> svList = _vv.VisitAlias(query.View);
                StringBuilder sql = new StringBuilder();
                if (page == 1)
                {
                    sql.Append(_tr.Select).AppendFormat(TOP_N, numberPerPage);
                    sql.Append(_cb.BuildSelectColumns(query.View));
                    sql.Append(_tr.From).Append(_vv.BuildJoinClause(query.View));
                    if (query.Criteria != null)
                    {
                        sql.Append(_tr.Where);
                        sql.Append(_cv.BuildWhereFilters(query.Criteria, svList));
                    }
                    sql.Append(_tr.OrderBy).Append(_ov.BuildOrderClause(query.Order, svList));
                    return sql.ToString();
                }
                else
                {
                    StringBuilder sub = new StringBuilder();
                    sub.Append(_tr.Select).AppendFormat(TOP_N, (page - 1) * numberPerPage).Append(_ov.BuildOrderSelectColumns(query.Order, svList));
                    sub.Append(_tr.From).Append(_vv.BuildJoinClause(query.View));
                    if (query.Criteria != null)
                    {
                        sub.Append(_tr.Where).Append(_cv.BuildWhereFilters(query.Criteria, svList));
                    }
                    sub.Append(_tr.OrderBy).Append(_ov.BuildOrderClause(query.Order, svList));

                    sql.Append(_tr.Select).AppendFormat(TOP_N, numberPerPage).Append(_cb.BuildSelectColumns(query.View));
                    sql.Append(_tr.From).Append(_vv.BuildJoinClause(query.View));
                    sql.Append(_tr.Where).Append(_ov.BuildOrderSelectColumns(query.Order, svList)).Append(_tr.NotIn).Append(sub.Bracketing(StringPair.Parenthesis));
                    if (query.Criteria != null)
                    {
                        sql.Append(_tr.And).Append(_cv.BuildWhereFilters(query.Criteria, svList));
                    }
                    sql.Append(_tr.OrderBy).Append(_ov.BuildOrderClause(query.Order, svList));

                    return sql.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex.CreateWrapException<SQLGenerateException>();
            }
        }
    }
}
