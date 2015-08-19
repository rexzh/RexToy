using System;
using System.Collections.Generic;
using System.Text;

using RexToy.ORM.MappingInfo;

namespace RexToy.ORM.Dialect
{
    public interface IModelColumnsBuilder
    {
        StringBuilder BuildCreateTableColumns(IObjectMapInfo map);
    }
}
