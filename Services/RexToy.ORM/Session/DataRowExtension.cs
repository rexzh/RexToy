using System;
using System.Collections.Generic;
using System.Data;

using RexToy.ORM.MappingInfo;

namespace RexToy.ORM.Session
{
    public static class DataRowExtension
    {
        internal static T MapToEntity<T>(this DataRow dr, IObjectMapInfo map, string alias = null)
        {
            if (IsNull(dr, alias, map))
                return default(T);

            T entity = Activator.CreateInstance<T>();
            IReflector r = Reflector.Bind(entity);

            foreach (var pmi in map.PropertyMaps)
            {
                object val = dr[alias + pmi.ColumnName];

                Type propertyType = r.GetPropertyType(pmi.PropertyName);
                if (propertyType.IsEnum)
                {
                    if (val is string)
                        r.SetPropertyValue(pmi.PropertyName, EnumEx.Parse(propertyType, val as string, false));
                    else
                        r.SetPropertyValue(pmi.PropertyName, EnumEx.Parse(propertyType, (int)System.Convert.ChangeType(val, typeof(int))));
                }
                else
                {
                    if (val is System.DBNull)
                        r.SetPropertyValue(pmi.PropertyName, null);
                    else
                        r.SetPropertyValue(pmi.PropertyName, TypeCast.ChangeToTypeOrNullableType(val, propertyType));
                }
            }

            return entity;
        }

        public static T MapToEntity<T>(this DataRow dr, string alias = null)
        {
            var map = CoreFactory.ObjectMapInfoCache.GetMapInfo(typeof(T), true);
            return MapToEntity<T>(dr, map, alias);
        }

        private static bool IsNull(DataRow dr, string alias, IObjectMapInfo map)
        {
            foreach (var pmi in map.PrimaryKeyMaps)
            {
                object val = dr[alias + pmi.ColumnName];
                if (val != DBNull.Value)
                    return false;
            }
            return true;
        }
    }
}
