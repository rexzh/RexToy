using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using RexToy.ORM.MappingInfo;

namespace RexToy.ORM.Dialect
{
    public abstract class AbstractMappingSQLEmit : IMappingSQLEmit
    {
        protected IObjectMapInfoCache _cache;
        protected IMappingColumnsBuilder _cb;
        protected ISQLTranslator _tr;
        protected IMappingConditionExpressionVisitor _cv;
        protected IMappingOrderExpressionVisitor _ov;
        protected AbstractMappingSQLEmit(IObjectMapInfoCache cache, IMappingColumnsBuilder cb, ISQLTranslator tr, IMappingConditionExpressionVisitor cv, IMappingOrderExpressionVisitor ov)
        {
            cache.ThrowIfNullArgument(nameof(cache));
            cb.ThrowIfNullArgument(nameof(cb));
            tr.ThrowIfNullArgument(nameof(tr));
            cv.ThrowIfNullArgument(nameof(cv));
            ov.ThrowIfNullArgument(nameof(ov));

            _cache = cache;
            _cb = cb;
            _tr = tr;
            _cv = cv;
            _ov = ov;
        }

        #region ISQLEmit Members

        public virtual string FindByPK<T>(T pk)
        {
            pk.ThrowIfNullArgument(nameof(pk));
            var map = _cache.GetMapInfo(typeof(T), true);
            if (map.PrimaryKeyMaps.Count() == 0)
            {
                GenerateExceptionHelper.ThrowNoPrimaryKeyDefine(typeof(T));
            }
            try
            {
                StringBuilder str = new StringBuilder();

                str.Append(_tr.Select).Append(_cb.BuildSelectColumns(map)).Append(_tr.From).Append(_tr.GetEscapedTableName(map.Table.LocalName));
                str.Append(_tr.Where).Append(_cb.BuildWherePrimaryKey(map, pk));

                return str.ToString();
            }
            catch (Exception ex)
            {
                throw ex.CreateWrapException<SQLGenerateException>();
            }
        }

        public virtual string FindBy<T>()
        {
            var map = _cache.GetMapInfo(typeof(T), true);
            try
            {
                StringBuilder str = new StringBuilder();
                str.Append(_tr.Select).Append(_cb.BuildSelectColumns(map)).Append(_tr.From).Append(_tr.GetEscapedTableName(map.Table.LocalName));
                return str.ToString();
            }
            catch (Exception ex)
            {
                throw ex.CreateWrapException<SQLGenerateException>();
            }
        }

        public virtual string FindBy<T>(Expression<Func<T, bool>> where)
        {
            where.ThrowIfNullArgument(nameof(where));
            var map = _cache.GetMapInfo(typeof(T), true);
            try
            {
                StringBuilder str = new StringBuilder();
                str.Append(_tr.Select).Append(_cb.BuildSelectColumns(map)).Append(_tr.From).Append(_tr.GetEscapedTableName(map.Table.LocalName));
                str.Append(_tr.Where).Append(_cv.Translate(where.PartialEval(), map));

                return str.ToString();
            }
            catch (Exception ex)
            {
                throw ex.CreateWrapException<SQLGenerateException>();
            }
        }

        public virtual string FindBy<T>(Expression<Func<T, object>> order, int top, OrderType type = OrderType.Asc)
        {
            order.ThrowIfNullArgument(nameof(order));
            var map = _cache.GetMapInfo(typeof(T), true);
            try
            {
                StringBuilder str = new StringBuilder();
                str.Append(_tr.Select).AppendFormat("TOP {0} ", top).Append(_cb.BuildSelectColumns(map)).Append(_tr.From).Append(_tr.GetEscapedTableName(map.Table.LocalName));
                str.Append(_tr.OrderBy).Append(_ov.Translate(order, map));
                str.Append(type == OrderType.Asc ? _tr.Asc : _tr.Desc);

                return str.ToString();
            }
            catch (Exception ex)
            {
                throw ex.CreateWrapException<SQLGenerateException>();
            }
        }

        public virtual string FindBy<T>(Expression<Func<T, bool>> where, Expression<Func<T, object>> order, int top, OrderType type = OrderType.Asc)
        {
            where.ThrowIfNullArgument(nameof(where));
            order.ThrowIfNullArgument(nameof(order));
            var map = _cache.GetMapInfo(typeof(T), true);
            try
            {
                StringBuilder str = new StringBuilder();
                str.Append(_tr.Select).AppendFormat("TOP {0} ", top).Append(_cb.BuildSelectColumns(map)).Append(_tr.From).Append(_tr.GetEscapedTableName(map.Table.LocalName));
                str.Append(_tr.Where).Append(_cv.Translate(where.PartialEval(), map));
                str.Append(_tr.OrderBy).Append(_ov.Translate(order, map));
                str.Append(type == OrderType.Asc ? _tr.Asc : _tr.Desc);

                return str.ToString();
            }
            catch (Exception ex)
            {
                throw ex.CreateWrapException<SQLGenerateException>();
            }
        }

        public abstract string FindIdentity<T>();

        public virtual IPropertyMapInfo GetPrimaryKeyNeedBinding<T>()
        {
            var map = _cache.GetMapInfo(typeof(T), true);

            if (map.PrimaryKeyGenerate == PrimaryKeyGenerate.Auto)
            {
                if (map.PKStatus == PrimaryKeyStatus.Single)
                    return map.PrimaryKeyMaps.First();
                else
                    GenerateExceptionHelper.ThrowPrimaryKeyDefineNotSupport(typeof(T));
            }

            return null;
        }

        public virtual string Create<T>(T entity)
        {
            var map = _cache.GetMapInfo(typeof(T), true);
            try
            {
                StringBuilder str = new StringBuilder();

                str.Append(_tr.Insert).Append(_tr.GetEscapedTableName(map.Table.LocalName));
                str.Append(StringPair.Parenthesis.Begin).Append(_cb.BuildInsertColumns(map, entity)).Append(StringPair.Parenthesis.End);
                str.Append(_tr.Values).Append(StringPair.Parenthesis.Begin).Append(_cb.BuildInsertValues(map, entity)).Append(StringPair.Parenthesis.End);

                return str.ToString();
            }
            catch (Exception ex)
            {
                throw ex.CreateWrapException<SQLGenerateException>();
            }
        }

        public virtual string Update<T>(T entity)
        {
            var map = _cache.GetMapInfo(typeof(T), true);
            if (map.PrimaryKeyMaps.Count() == 0)
            {
                GenerateExceptionHelper.ThrowNoPrimaryKeyDefine(typeof(T));
            }

            try
            {
                StringBuilder str = new StringBuilder();
                str.Append(_tr.Update).Append(_tr.GetEscapedTableName(map.Table.LocalName));
                str.Append(_tr.Set).Append(_cb.BuildUpdateSets(map, entity));
                str.Append(_tr.Where).Append(_cb.BuildWherePrimaryKey(map, entity));
                return str.ToString();
            }
            catch (Exception ex)
            {
                throw ex.CreateWrapException<SQLGenerateException>();
            }
        }

        public virtual string Remove<T>(T entity)
        {
            var map = _cache.GetMapInfo(typeof(T), true);
            if (map.PrimaryKeyMaps.Count() == 0)
            {
                GenerateExceptionHelper.ThrowNoPrimaryKeyDefine(typeof(T));
            }
            try
            {
                StringBuilder str = new StringBuilder();
                str.Append(_tr.Delete).Append(_tr.GetEscapedTableName(map.Table.LocalName));
                str.Append(_tr.Where).Append(_cb.BuildWherePrimaryKey(map, entity));
                return str.ToString();
            }
            catch (Exception ex)
            {
                throw ex.CreateWrapException<SQLGenerateException>();
            }
        }

        public virtual string RemoveBy<T>(Expression<Func<T, bool>> where)
        {
            var map = _cache.GetMapInfo(typeof(T), true);

            try
            {
                StringBuilder str = new StringBuilder();
                str.Append(_tr.Delete).Append(_tr.GetEscapedTableName(map.Table.LocalName));
                str.Append(_tr.Where).Append(_cv.Translate(where.PartialEval(), map));
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
