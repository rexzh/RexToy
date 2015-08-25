using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RexToy.WebService
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class StartupAttribute : Attribute
    {
        private Type _type;
        public StartupAttribute(Type type)
        {
            Assertion.IsNotNull(type, "Startup type null.");
            Assertion.IsTrue(type.Implemented(typeof(IStartup)), "Must implement IStartup");
            _type = type;
        }

        public Type StartType
        {
            get { return _type; }
        }
    }
}
