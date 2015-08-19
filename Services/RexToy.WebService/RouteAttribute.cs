using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RexToy.WebService
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple =false)]
    public class RouteAttribute : Attribute
    {
        private bool _strict;
        private string _route;
        private string[] _segments;
        private bool[] _requireCapture;

        private string _webMethod = WebRequestMethods.Http.Get;
        public string WebMethod
        {
            get { return _webMethod; }
            set { _webMethod = value; }
        }

        public RouteAttribute(string route, bool strict = false)
        {
            _strict = strict;
            _route = route;
            _segments = route.Split(Const.PATH_DELIMITER, StringSplitOptions.RemoveEmptyEntries);
            _requireCapture = new bool[_segments.Length];
            for (int i = 0; i < _segments.Length; i++)
            {
                if (_segments[i].StartsWith(':'))
                {
                    _requireCapture[i] = true;
                }
                else
                {
                    if (!_strict)
                    {
                        _segments[i] = _segments[i].ToLower();
                    }
                }
            }
        }

        internal RouteMatchResult Match(string path)
        {
            string[] vals = path.Split(Const.PATH_DELIMITER, StringSplitOptions.RemoveEmptyEntries);
            if (vals.Length != _segments.Length)
                return RouteMatchResult.NotMatch;
            Dictionary<string, string> capture = new Dictionary<string, string>();
            for (int i = 0; i < _segments.Length; i++)
            {
                if (_requireCapture[i])
                    capture[_segments[i]] = vals[i];
                else
                {
                    if (_strict)
                    {
                        if (_segments[i] != vals[i])
                            return RouteMatchResult.NotMatch;
                    }
                    else
                    {
                        if (_segments[i] != vals[i].ToLower())
                            return RouteMatchResult.NotMatch;
                    }
                }
            }
            return new RouteMatchResult() { Match = true, Captured = capture };
        }
    }
}
