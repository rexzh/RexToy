using System;
using System.Collections.Generic;
using System.Data;

using RexToy.ORM.MappingInfo;
using RexToy.ORM.Session;

namespace RexToy.ORM.Dialect
{
    public interface IDialectProvider
    {
        IMappingSQLEmit CreateMappingSQLEmit(IObjectMapInfoCache cache);
        IQuerySQLEmit CreateQuerySQLEmit(IObjectMapInfoCache cache);
        IModelSQLEmit CreateModelSQLEmit(IObjectMapInfoCache cache);

        IMetaQuery CreateMetaQuery(ISQLExecutor exe);
    }
}
