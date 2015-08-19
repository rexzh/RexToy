using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Collections.Specialized;

using System.Web;

namespace RexToy.WebService
{
    public interface IRESTfulApi
    {
    }

    public static class RESTfulApiExtension
    {
        public static HttpContext GetHttpContext(this IRESTfulApi rest)
        {
            return HttpContext.Current;
        }

        public static string GetRequestJson(this IRESTfulApi rest)
        {
            var input = HttpContext.Current.Request.InputStream;
            MemoryStream m = new MemoryStream();
            input.CopyTo(m);
            m.Position = 0;
            input.Position = 0;
            using (var reader = new StreamReader(m))
            {
                return reader.ReadToEnd();
            }
        }

        public static NameValueCollection GetQueryParam(this IRESTfulApi rest)
        {
            return HttpContext.Current.Request.QueryString;
        }
    }
}
