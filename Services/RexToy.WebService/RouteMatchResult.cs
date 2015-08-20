using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RexToy.WebService
{
    public class RouteMatchResult
    {
        public bool Match;
        public Dictionary<string, string> Captured;

        public static readonly RouteMatchResult NotMatch = new RouteMatchResult() { Match = false, Captured = new Dictionary<string, string>() };
    }
}
