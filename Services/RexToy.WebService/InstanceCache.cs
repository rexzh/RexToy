using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RexToy.Collections;

namespace RexToy.WebService
{
    class InstanceCache : LazyLoadDictionary<Type, object>
    {
        protected override object Load(Type key)
        {
            return Activator.CreateInstance(key);
        }
    }
}
