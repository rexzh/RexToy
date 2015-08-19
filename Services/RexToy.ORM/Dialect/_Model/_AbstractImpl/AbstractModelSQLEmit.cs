using System;
using System.Collections.Generic;
using System.Text;

using RexToy.ORM.MappingInfo;

namespace RexToy.ORM.Dialect
{
    public abstract class AbstractModelSQLEmit : IModelSQLEmit
    {
        protected ISQLTranslator _tr;
        protected ITypeMap _tm;
        protected IModelColumnsBuilder _cb;
        protected IObjectMapInfoCache _cache;
        protected AbstractModelSQLEmit(IObjectMapInfoCache cache, ISQLTranslator tr, ITypeMap tm, IModelColumnsBuilder cb)
        {
            tr.ThrowIfNullArgument(nameof(tr));
            tm.ThrowIfNullArgument(nameof(tm));
            cb.ThrowIfNullArgument(nameof(cb));
            cache.ThrowIfNullArgument(nameof(cache));
            this._tr = tr;
            this._tm = tm;
            this._cb = cb;
            this._cache = cache;
        }

        #region IModelSQLEmit Members

        public abstract string CreateTable<T>();

        public virtual string DropTable<T>()
        {
            IObjectMapInfo mapInfo = _cache.GetMapInfo(typeof(T), true);

            try
            {
                StringBuilder str = new StringBuilder();
                str.Append(_tr.DropTable).Append(_tr.GetEscapedTableName(mapInfo.Table.LocalName));
                return str.ToString();
            }
            catch (Exception ex)
            {
                throw ex.CreateWrapException<SQLGenerateException>();
            }
        }

        public virtual string TruncateTable<T>()
        {
            IObjectMapInfo mapInfo = _cache.GetMapInfo(typeof(T), true);

            try
            {
                StringBuilder str = new StringBuilder();
                str.Append(_tr.TruncateTable).Append(_tr.GetEscapedTableName(mapInfo.Table.LocalName));
                return str.ToString();
            }
            catch (Exception ex)
            {
                throw ex.CreateWrapException<SQLGenerateException>();
            }
        }

        #endregion
    }
}
