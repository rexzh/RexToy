using System;
using System.Data;

using RexToy.ORM.Session;

namespace RexToy.ORM
{
    public class Row
    {
        private DataRow _dr;
        internal Row(DataRow dr)
        {
            _dr = dr;
        }

        public T GetEntity<T>()
        {
            return GetEntity<T>(typeof(T).Name);
        }

        public T GetEntity<T>(string alias)
        {
            return _dr.MapToEntity<T>(alias);
        }

        public object this[int index]
        {
            get { return _dr[index]; }
        }

        public object this[string colName]
        {
            get { return _dr[colName]; }
        }
    }
}
