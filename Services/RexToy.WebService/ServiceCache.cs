using System;
using System.Collections.Generic;
using System.Web;
using System.Reflection;
using System.Diagnostics;

using RexToy.Configuration;
using RexToy.WebService.Configuration;

namespace RexToy.WebService
{
    class ServiceCache
    {
        public static ServiceCache Instance = new ServiceCache();

        private Dictionary<RouteAttribute, MethodInfo> _dict;
        private ServiceCache()
        {
            _dict = new Dictionary<RouteAttribute, MethodInfo>();
            foreach (var a in WebServiceConfig.WebServiceConfiguration.Assemblies)
            {
                foreach (Type t in a.GetTypes())
                {
                    if (t.Implemented(typeof(IRESTfulApi)))
                    {
                        foreach (MethodInfo method in t.GetMethods())
                        {
                            var route = method.GetSingleAttribute<RouteAttribute>();
                            if (route != null)
                                _dict[route] = method;
                        }
                    }
                }
            }
        }

        [Conditional("DEBUG")]
        private void Check(HttpRequest req)
        {
            List<RouteAttribute> match = new List<RouteAttribute>();
            List<RouteAttribute> urlMatch = new List<RouteAttribute>();
            foreach (var kvp in _dict)
            {
                if (kvp.Key.Match(req.Path).Match)
                {
                    if (kvp.Key.WebMethod == req.HttpMethod)
                        match.Add(kvp.Key);
                    else
                        urlMatch.Add(kvp.Key);
                }
            }

            if (match.Count > 1)
            {
                //TODO:There is conflict
            }
            if (match.Count == 0 && urlMatch.Count > 0)
            {
                //TODO:Method not allowed
            }
        }

        public ServiceResponse Dispatch(HttpRequest req)
        {
            foreach (var kvp in _dict)
            {
                var result = kvp.Key.Match(req.Path);
                if (result.Match && req.HttpMethod == kvp.Key.WebMethod)
                {
                    //TODO:Invoke
                    //result.Captured
                }
            }
            return ServiceResponse.NotImplemented;
        }
    }
}
