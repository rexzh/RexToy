using System;
using System.Collections.Generic;

namespace RexToy.ORM.MappingInfo
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ColumnAttribute : Attribute
    {
        public ColumnAttribute()
        {
            _nullable = true;
        }

        private string _name;
        public string Name
        {
            get { return _name; }
        }

        public ColumnAttribute(string colName)
        {
            _name = colName;
        }

        public bool PrimaryKey { get; set; }

        public int Length { get; set; }

        public bool _nullable;
        public bool Nullable
        {
            get { return _nullable; }
            set { _nullable = value; }
        }
    }
}
