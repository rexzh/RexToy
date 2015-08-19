using System;
using System.Collections.Generic;
using System.Text;

using RexToy.ORM.MappingInfo;

namespace RexToy.ORM.Dialect
{
    public abstract class AbstractModelColumnsBuilder : IModelColumnsBuilder
    {
        protected const string PK_PREFIX = "PK_";
        protected const int DEFAULT_COL_LENGTH = 64;
        protected const char SPACE = ' ';

        protected ISQLTranslator _tr;
        protected ITypeMap _tm;

        public AbstractModelColumnsBuilder(ISQLTranslator tr, ITypeMap tm)
        {
            tr.ThrowIfNullArgument(nameof(tr));
            tm.ThrowIfNullArgument(nameof(tm));

            this._tr = tr;
            this._tm = tm;
        }

        #region IModelColumnsBuilder Members

        protected StringBuilder _str;
        protected IObjectMapInfo _map;
        public StringBuilder BuildCreateTableColumns(IObjectMapInfo map)
        {
            map.ThrowIfNullArgument(nameof(map));
            
            _map = map;
            _str = new StringBuilder();
            BuildCreateTableColumns();
            return _str;
        }

        #endregion

        protected abstract void BuildCreateTableColumns();
    }
}
