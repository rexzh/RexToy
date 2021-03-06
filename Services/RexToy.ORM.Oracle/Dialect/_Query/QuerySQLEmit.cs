﻿using System;
using System.Text;

using RexToy.ORM.MappingInfo;
using RexToy.ORM.QueryModel;

namespace RexToy.ORM.Dialect.Oracle
{
    class QuerySQLEmit : AbstractQuerySQLEmit
    {
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
                string pagedQuery = "SELECT * FROM (SELECT row_.*, rownum rownum_ FROM ({0}) row_ WHERE rownum <= {1}) WHERE rownum_ > {2}";
                var n1 = numberPerPage * page;
                var n2 = numberPerPage * (page - 1);

                var svList = _vv.VisitAlias(query.View);

                StringBuilder str = new StringBuilder();
                str.Append(_tr.Select).Append(_cb.BuildSelectColumns(query.View));
                str.Append(_tr.From).Append(_vv.BuildJoinClause(query.View));
                if (query.Criteria != null)
                {
                    str.Append(_tr.Where).Append(_cv.BuildWhereFilters(query.Criteria, svList));
                }

                str.Append(_tr.OrderBy).Append(_ov.BuildOrderClause(query.Order, svList));
                return string.Format(pagedQuery, str, n1, n2);
            }
            catch (Exception ex)
            {
                throw ex.CreateWrapException<SQLGenerateException>();
            }
        }
    }
}
