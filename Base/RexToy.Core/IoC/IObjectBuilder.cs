using System;
using System.Collections.Generic;

namespace RexToy.IoC
{
    interface IObjectBuilder
    {
        bool ReadyToBuild(IComponentInfo info);
        IObjectBuildContext Build(IComponentInfo info);
        void TearDown(object instance, IObjectBuildContext ctx);
    }
}
