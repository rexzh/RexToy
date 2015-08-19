using System;
using System.Collections.Generic;

namespace RexToy.IoC
{
    interface IComponentInfoStore
    {
        IComponentInfo FindById(string id);
        IComponentInfo FindByContract(Type type);

        void AddComponentInfo(IComponentInfo info);
    }
}
