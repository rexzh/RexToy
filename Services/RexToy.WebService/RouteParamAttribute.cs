using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RexToy.WebService
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class RouteParamAttribute : Attribute
    {
        private string _argName;

        public RouteParamAttribute(string argName = "")
        {
            _argName = argName;
        }

        public string ArgName
        {
            get { return _argName; }
        }
    }
}
