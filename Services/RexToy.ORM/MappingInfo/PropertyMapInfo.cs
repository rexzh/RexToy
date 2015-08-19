using System;
using System.Collections.Generic;

namespace RexToy.ORM.MappingInfo
{
    public class PropertyMapInfo : IPropertyMapInfo
    {
        public PropertyMapInfo(string property, string column, int length, bool nullable)
        {
            _propertyName = property;
            _columnName = column;
            _nullable = nullable;
            _length = length;
        }

        private string _propertyName;
        public string PropertyName
        {
            get { return _propertyName; }
        }

        private string _columnName;
        public string ColumnName
        {
            get { return _columnName; }
        }

        private int _length;
        public int Length
        {
            get { return _length; }
        }

        private bool _nullable;
        public bool Nullable
        {
            get { return _nullable; }
        }
    }
}
