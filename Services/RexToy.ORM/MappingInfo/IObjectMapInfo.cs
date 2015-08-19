using System;
using System.Collections.Generic;

namespace RexToy.ORM.MappingInfo
{
    public interface IObjectMapInfo
    {
        PrefixedName Table { get; }
        Type EntityType { get; }

        IEnumerable<IPropertyMapInfo> PropertyMaps { get; }//Note:All property(include PK)
        IEnumerable<IPropertyMapInfo> PrimaryKeyMaps { get; }
        IEnumerable<IPropertyMapInfo> NonPKPropertyMaps { get; }

        PrimaryKeyStatus PKStatus { get; }

        PrimaryKeyGenerate PrimaryKeyGenerate { get; }
        string PKGenerateString { get; }

        string GetColumnFromProperty(string propertyName);
        string GetPropertyFromColumn(string columnName);

        IPropertyMapInfo GetPropertyMapInfo(string propertyName);
    }
}
