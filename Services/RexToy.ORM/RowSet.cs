using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace RexToy.ORM
{
    public class RowSet : IEnumerable<Row>
    {
        private List<Row> _rows;
        internal RowSet(DataTable dt)
        {
            _dt = dt;
            _rows = new List<Row>();
            foreach (DataRow dr in dt.Rows)
            {
                Row r = new Row(dr);
                _rows.Add(r);
            }
        }

        private DataTable _dt;
        public DataTable DataTable
        {
            get { return _dt; }
        }

        public IEnumerator<Row> GetEnumerator()
        {
            return _rows.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _rows.GetEnumerator();
        }

        public IEnumerable<T> GetEntities<T>()
        {
            foreach (Row r in _rows)
            {
                yield return r.GetEntity<T>();
            }
        }

        public IEnumerable<T> GetEntities<T>(string alias)
        {
            foreach (Row r in _rows)
            {
                yield return r.GetEntity<T>(alias);
            }
        }

        public int Count
        {
            get { return _rows.Count; }
        }

        public Row this[int index]
        {
            get { return _rows[index]; }
        }

        //Extend:Pivot
    }
}
