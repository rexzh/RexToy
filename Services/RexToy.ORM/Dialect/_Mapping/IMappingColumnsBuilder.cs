using System;
using System.Collections.Generic;
using System.Text;

using RexToy.ORM.MappingInfo;

namespace RexToy.ORM.Dialect
{
    public interface IMappingColumnsBuilder
    {
        StringBuilder BuildSelectColumns(IObjectMapInfo map);
        StringBuilder BuildUpdateSets(IObjectMapInfo map, object entity);
        StringBuilder BuildInsertColumns(IObjectMapInfo map, object entity);
        StringBuilder BuildInsertValues(IObjectMapInfo map, object entity);
        StringBuilder BuildWherePrimaryKey(IObjectMapInfo map, object entity);
    }
}
