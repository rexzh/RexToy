using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using RexToy.ORM.MappingInfo;
using RexToy.ORM.QueryModel;

namespace RexToy.ORM.Dialect
{
    public static class ParameterUtility
    {
        public static string ChooseNameFromView(this ParameterExpression p, IEnumerable<SingleEntityView> svList, IObjectMapInfoCache cache)
        {
            var matchType = from v in svList
                            where v.EntityType == p.Type
                            select v;

            switch (matchType.Count())
            {
                case 0:
                    ParseExceptionHelper.ThrowTypeNotDefinedInView(p);
                    break;

                case 1:
                    SingleEntityView view = matchType.First();
                    if ((!string.IsNullOrEmpty(view.Alias)) && view.Alias != p.Name)
                        ParseExceptionHelper.ThrowParameterNameNotMatchViewAlias(p.Name, view.Alias, view.EntityType);

                    if (!string.IsNullOrEmpty(view.Alias))
                        return view.Alias;
                    else
                    {
                        var map = cache.GetMapInfo(p.Type, true);
                        return map.Table.LocalName;
                    }

                default:
                    var q = from v in matchType
                            where v.Alias == p.Name
                            select v;

                    switch (q.Count())
                    {
                        case 0:
                            ParseExceptionHelper.ThrowNoAliasForParameterName(p);
                            break;

                        case 1:
                            return q.First().Alias;

                        default:
                            ParseExceptionHelper.ThrowMultiTypeHaveSameAlias(p);
                            break;
                    }

                    break;
            }
            return string.Empty;
        }
    }
}
