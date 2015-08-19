using System;
using System.Collections.Generic;

namespace RexToy.ORM.MappingInfo
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class TableAttribute : Attribute
    {
        PrefixedName _pName;
        public TableAttribute(string prefixedName)
        {
            _pName = new PrefixedName(prefixedName);
            _primaryKeyGenerate = RexToy.ORM.MappingInfo.PrimaryKeyGenerate.Auto.ToString();
        }

        public PrefixedName TableName
        {
            get { return _pName; }
        }

        private string _primaryKeyGenerate;
        public string PrimaryKeyGenerate
        {
            get { return _primaryKeyGenerate; }
            set { _primaryKeyGenerate = value; }
        }
    }
}
