using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

using RexToy.Logging;

namespace RexToy.ORM.MappingInfo
{
    public class AttributeMapLoader
    {
        private static ILog _log = LogContext.GetLogger<AttributeMapLoader>();

        private IObjectMapInfoCache _cache;
        private string _file;

        public AttributeMapLoader(IObjectMapInfoCache cache, string file)
        {
            cache.ThrowIfNullArgument(nameof(cache));
            file.ThrowIfNullArgument(nameof(file));

            _cache = cache;
            _file = file;
        }

        public void Load()
        {
            _log.Info("Try load mapping information from [{0}]...", _file);

            Assembly assembly = Assembly.LoadFrom(_file);
            Load(assembly);

            _log.Info("Load mapping information from [{0}] success.", _file);
        }

        private void Load(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                var table = type.GetSingleAttribute<TableAttribute>();
                if (table != null)
                {
                    List<string> pkList = new List<string>();
                    List<IPropertyMapInfo> fields = new List<IPropertyMapInfo>();

                    foreach (PropertyInfo property in type.GetProperties())
                    {
                        ColumnAttribute col = property.GetSingleAttribute<ColumnAttribute>(true);
                        if (col != null)
                        {
                            IPropertyMapInfo pInfo = new PropertyMapInfo(property.Name, col.Name, col.Length, col.Nullable);
                            fields.Add(pInfo);
                            if (col.PrimaryKey)
                                pkList.Add(property.Name);
                        }
                    }

                    IObjectMapInfo info = new ObjectMapInfo(table.TableName, type, pkList.ToArray().Join(ObjectMapInfo.Composite_PK_Delimiter), table.PrimaryKeyGenerate, fields);
                    _cache.SetMapInfo(type, info);
                }
            }
        }
    }
}
