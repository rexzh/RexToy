using System;
using System.Collections.Generic;
using System.Data;

using RexToy.ORM.MappingInfo;

namespace RexToy.ORM.Session
{
    public static class DataTableExtension
    {
        public static List<T> MapToList<T>(this DataTable dt, string alias = null)
        {
            List<T> list = new List<T>();
            var map = CoreFactory.ObjectMapInfoCache.GetMapInfo(typeof(T), true);

            foreach (DataRow dr in dt.Rows)
            {
                T entity = dr.MapToEntity<T>(map);
                list.Add(entity);
            }
            return list;
        }
    }
}
