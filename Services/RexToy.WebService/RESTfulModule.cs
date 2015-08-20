using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Reflection;

using RexToy.Logging;
using RexToy.Json;
using RexToy.WebService.Configuration;

namespace RexToy.WebService
{
    class RESTfulModule : IHttpModule
    {
        private static ILog _log = LogContext.GetLogger<RESTfulModule>();

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += context_BeginRequest;
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication app = sender as HttpApplication;
            var req = app.Request;
            var res = app.Response;
            var filePath = req.FilePath;
            var path = req.Path;

            var b = WebServiceConfig.WebServiceConfiguration.BaseUrl;

            if (path.StartsWith(b) && path[b.Length] == Const.PATH_DELIMITER)
            {
                _log.Debug("Dispatch path: [{0}]", path);

                var segments = path.Substring(b.Length).Split(Const.PATH_DELIMITER, StringSplitOptions.RemoveEmptyEntries);
                if (segments.Length == 0)
                {
                    ServiceResponse.NotImplemented.CompleteRequest(app);
                    return;
                }
                
                ServiceCache.Instance.Dispatch(app).CompleteRequest(app);
            }
            else
            {
                _log.Debug("Skip path: [{0}]", path);
            }
        }

        /*
        private object[] BuildParameters(MethodInfo m)
        {
            var attr = m.GetSingleAttribute<WebFunctionAttribute>();
            var req = HttpContext.Current.Request;

            if (attr.Method != req.HttpMethod)
            {
                throw new InvalidOperationException(string.Format("Use [{0}] to request, but require [{1}].", req.HttpMethod, attr.Method));
            }

            switch (req.HttpMethod)
            {
                case WebMethod.GET:
                    List<object> paramList = new List<object>();
                    foreach (var p in m.GetParameters())
                    {
                        string arg = req.QueryString[p.Name];
                        paramList.Add(Convert.ChangeType(arg, p.ParameterType));
                    }
                    return paramList.ToArray();

                case WebMethod.POST:
                case WebMethod.PUT:
                case WebMethod.DELETE:
                    using (StreamReader r = new StreamReader(req.InputStream))
                    {
                        var json = r.ReadToEnd().ParseToJsonObject() as JsonObject;
                        List<object> args = new List<object>();
                        foreach (var p in m.GetParameters())
                        {
                            var item = json[p.Name];

                            var data = JsonHelper.Render(item, p.ParameterType, false);
                            args.Add(data);
                        }
                        return args.ToArray();
                    }

                default:
                    ExceptionHelper.ThrowNotSupportMethod(req.HttpMethod);
                    return new object[] { };
            }
        }
        */
    }
}
