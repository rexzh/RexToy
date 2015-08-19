using System;
using System.Collections.Generic;

namespace RexToy.ORM.MappingInfo
{
    public interface IPropertyMapInfo
    {
        string PropertyName { get; }
        string ColumnName { get; }

        int Length { get; }
        bool Nullable { get; }
    }
}
