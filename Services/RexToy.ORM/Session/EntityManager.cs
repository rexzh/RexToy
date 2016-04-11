using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Data;

using RexToy.Logging;
using RexToy.ORM.Dialect;

namespace RexToy.ORM.Session
{
    internal class EntityManager : IEntityManager
    {
        private static ILog _log = LogContext.GetLogger<EntityManager>();

        private IMappingSQLEmit _emit;
        private ISQLExecutor _exe;
        public EntityManager(ISQLExecutor exe, IMappingSQLEmit emit)
        {
            exe.ThrowIfNullArgument(nameof(exe));
            emit.ThrowIfNullArgument(nameof(emit));
            _exe = exe;
            _emit = emit;
        }

        #region IEntityManager Members

        public T FindByPK<T>(T pk)
        {
            DataTable dt = null;
            try
            {
                string sql = _emit.FindByPK(pk);
                _log.Debug(sql);
                dt = _exe.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex.CreateWrapException<ORMException>();
            }

            Assertion.IsNotNull(dt, "Data table should be filled already.");

            switch (dt.Rows.Count)
            {
                case 0:
                    return default(T);

                case 1:
                    try
                    {
                        return dt.Rows[0].MapToEntity<T>();
                    }
                    catch (Exception ex)
                    {
                        throw ex.CreateWrapException<ORMException>();
                    }

                default:
                    ORMExceptionHelper.ThrowFindByPKResultNotUnique(typeof(T));
                    break;
            }
            return default(T);
        }

        public List<T> FindBy<T>(Expression<Func<T, bool>> func)
        {
            try
            {
                string sql = _emit.FindBy(func);
                _log.Debug(sql);
                DataTable dt = _exe.ExecuteQuery(sql);
                return dt.MapToList<T>();
            }
            catch (Exception ex)
            {
                throw ex.CreateWrapException<ORMException>();
            }
        }

        public List<T> FindBy<T>()
        {
            try
            {
                string sql = _emit.FindBy<T>();
                _log.Debug(sql);
                DataTable dt = _exe.ExecuteQuery(sql);
                return dt.MapToList<T>();
            }
            catch (Exception ex)
            {
                throw ex.CreateWrapException<ORMException>();
            }
        }

        public T Create<T>(T entity)
        {
            try
            {
                string sql = _emit.Create(entity);
                _log.Debug(sql);
                _exe.ExecuteNonQuery(sql);

                var pk = _emit.GetPrimaryKeyNeedBinding<T>();
                if (pk != null)
                {
                    string sqlId = _emit.FindIdentity<T>();
                    _log.Debug(sqlId);
                    var id = _exe.ExecuteScalar(sqlId);
                    IReflector r = Reflector.Bind(entity);
                    r.SetPropertyValue(pk.PropertyName, TypeCast.ChangeToTypeOrNullableType(id, r.GetPropertyType(pk.PropertyName)));
                }
                return entity;
            }
            catch (Exception ex)
            {
                throw ex.CreateWrapException<ORMException>();
            }
        }

        public long Update<T>(T entity)
        {
            try
            {
                string sql = _emit.Update(entity);
                _log.Debug(sql);
                _exe.ExecuteNonQuery(sql);
                return _exe.AffectedRows;
            }
            catch (Exception ex)
            {
                throw ex.CreateWrapException<ORMException>();
            }
        }

        public long Remove<T>(T entity)
        {
            try
            {
                string sql = _emit.Remove(entity);
                _log.Debug(sql);
                _exe.ExecuteNonQuery(sql);
                return _exe.AffectedRows;
            }
            catch (Exception ex)
            {
                throw ex.CreateWrapException<ORMException>();
            }
        }

        public long RemoveBy<T>(Expression<Func<T, bool>> func)
        {
            try
            {
                string sql = _emit.RemoveBy(func);
                _log.Debug(sql);
                _exe.ExecuteNonQuery(sql);
                return _exe.AffectedRows;
            }
            catch (Exception ex)
            {
                throw ex.CreateWrapException<ORMException>();
            }
        }
        #endregion
    }
}
