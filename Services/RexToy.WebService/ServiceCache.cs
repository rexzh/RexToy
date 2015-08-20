﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Reflection;
using System.Diagnostics;

using RexToy.Collections;
using RexToy.Configuration;
using RexToy.WebService.Configuration;

namespace RexToy.WebService
{
    class ServiceCache
    {
        public static ServiceCache Instance = new ServiceCache();

        private Dictionary<RouteAttribute, MethodInfo> _dict;
        private LazyLoadDictionary<Type, object> _instanceCache;
        private ServiceCache()
        {
            _instanceCache = new InstanceCache();
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

        public ServiceResponse Dispatch(HttpApplication app)
        {
            HttpRequest req = app.Request;
            string path = req.Path.RemoveBegin(WebServiceConfig.WebServiceConfiguration.BaseUrl);
            bool urlMatch = false;
            foreach (var kvp in _dict)
            {
                var result = kvp.Key.MatchPath(path);
                if (result.Match)
                {
                    if (req.HttpMethod == kvp.Key.WebMethod)
                    {
                        var method = kvp.Value;
                        var argInfos = method.GetParameters();
                        object[] args = new object[method.GetParameters().Length];
                        for (int i = 0; i < args.Length; i++)
                        {
                            //TODO:param attribute
                            var value = result.Captured[argInfos[i].Name];
                            args[i] = Convert.ChangeType(value, argInfos[i].ParameterType);
                        }
                        object obj = method.Invoke(_instanceCache[method.ReflectedType], args);
                        if (method.ReturnType != typeof(void) && !(obj is string))
                        {
                            return ServiceResponse.NotCorrectImplemented;
                        }

                        ServiceResponse res = new ServiceResponse(200, obj as string);
                        return res;
                    }
                    else
                    {
                        urlMatch = true;
                    }
                }

            }
            if (urlMatch)
                return ServiceResponse.MethodNotAllow;
            else
                return ServiceResponse.NotImplemented;
        }
    }
}
