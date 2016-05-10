using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RexToy.ORM.MappingInfo;
using RexToy.ORM.Session;

namespace RexToy.ORM.Dialect.MySQL
{
    class DialectProvider : IDialectProvider
    {
        public IMappingSQLEmit CreateMappingSQLEmit(IObjectMapInfoCache cache)
        {
            ISQLTranslator tr = new SQLTranslator();
            IMappingColumnsBuilder cb = new MappingColumnsBuilder(tr);
            IMappingConditionExpressionVisitor cv = new MappingConditionExpressionVisitor(tr);
            IMappingOrderExpressionVisitor ov = new MappingOrderExpressionVisitor(tr);
            return new MappingSQLEmit(cache, cb, tr, cv, ov);
        }

        public IMetaQuery CreateMetaQuery(ISQLExecutor exe)
        {
            return new MetaQuery(exe);
        }

        public IModelSQLEmit CreateModelSQLEmit(IObjectMapInfoCache cache)
        {
            ITypeMap tm = new TypeMap();
            ISQLTranslator tr = new SQLTranslator();
            IModelColumnsBuilder cb = new ModelColumnsBuilder(tr, tm);
            return new ModelSQLEmit(cache, tr, tm, cb);
        }

        public IQuerySQLEmit CreateQuerySQLEmit(IObjectMapInfoCache cache)
        {   
            ISQLTranslator tr = new SQLTranslator();
            IJoinExpressionVisitor jev = new JoinExpressionVisitor(tr, cache);
            IFilterExpressionVisitor fev = new FilterExpressionVisitor(tr, cache);
            IQueryViewVisitor vv = new QueryViewVisitor(tr, jev, cache);
            IQueryColumnBuilder cb = new QueryColumnBuilder(tr, cache);
            IQueryCriteriaVisitor cv = new QueryCriteriaVisitor(tr, fev);
            IOrderExpressionVisitor oev = new OrderExpressionVisitor(tr, cache);
            IQueryOrderVisitor ov = new QueryOrderVisitor(tr, oev);
            return new QuerySQLEmit(cache, tr, vv, cb, cv, ov);
        }
    }
}
