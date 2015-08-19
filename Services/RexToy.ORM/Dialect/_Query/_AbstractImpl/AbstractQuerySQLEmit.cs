using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

using RexToy.Collections;
using RexToy.ORM.MappingInfo;
using RexToy.ORM.QueryModel;

namespace RexToy.ORM.Dialect
{
    public abstract class AbstractQuerySQLEmit : IQuerySQLEmit
    {
        protected IObjectMapInfoCache _cache;
        protected ISQLTranslator _tr;
        protected IQueryViewVisitor _vv;
        protected IQueryColumnBuilder _cb;
        protected IQueryCriteriaVisitor _cv;
        protected IQueryOrderVisitor _ov;

        public AbstractQuerySQLEmit(IObjectMapInfoCache cache, ISQLTranslator tr, IQueryViewVisitor vv, IQueryColumnBuilder cb, IQueryCriteriaVisitor cv, IQueryOrderVisitor ov)
        {
            cache.ThrowIfNullArgument(nameof(cache));
            tr.ThrowIfNullArgument(nameof(tr));
            vv.ThrowIfNullArgument(nameof(vv));
            cb.ThrowIfNullArgument(nameof(cb));
            cv.ThrowIfNullArgument(nameof(cv));
            ov.ThrowIfNullArgument(nameof(ov));

            _cache = cache;
            _tr = tr;
            _vv = vv;
            _cb = cb;
            _cv = cv;
            _ov = ov;
        }

        public virtual string Query(IQuery query)
        {
            try
            {
                StringBuilder str = new StringBuilder();
                IReadOnlyList<SingleEntityView> svList = _vv.VisitAlias(query.View);

                str.Append(_tr.Select);
                str.Append(_cb.BuildSelectColumns(query.View));
                str.Append(_tr.From);
                str.Append(_vv.BuildJoinClause(query.View));
                if (query.Criteria != null)
                {
                    str.Append(_tr.Where);
                    str.Append(_cv.BuildWhereFilters(query.Criteria, svList));
                }
                if (query.Order != null)
                {
                    str.Append(_tr.OrderBy);
                    str.Append(_ov.BuildOrderClause(query.Order, svList));
                }
                return str.ToString();
            }
            catch (Exception ex)
            {
                throw ex.CreateWrapException<SQLGenerateException>();
            }
        }

        public virtual string QueryCount(IQuery query)
        {
            try
            {
                StringBuilder str = new StringBuilder();
                IReadOnlyList<SingleEntityView> svList = _vv.VisitAlias(query.View);

                str.Append(_tr.Select);
                str.Append(_tr.CountRow);
                str.Append(_tr.From);
                str.Append(_vv.BuildJoinClause(query.View));
                if (query.Criteria != null)
                {
                    str.Append(_tr.Where);
                    str.Append(_cv.BuildWhereFilters(query.Criteria, svList));
                }
                return str.ToString();
            }
            catch (Exception ex)
            {
                throw ex.CreateWrapException<SQLGenerateException>();
            }
        }

        public abstract string PagedQuery(IQuery query, uint numberPerPage, uint page);
    }
}
