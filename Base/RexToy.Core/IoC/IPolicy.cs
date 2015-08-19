using System;

using RexToy.DesignPattern;
using RexToy.Xml;

namespace RexToy.IoC
{
    public interface IPolicy
    {
        Stages Stage { get; }
        void Initialize(XAccessor x);
        void BuildUp(IObjectBuildContext ctx);
        void OnBuildComplete(IObjectBuildContext ctx);
        void TearDown(object instance, IObjectBuildContext ctx);
        bool ReadyToBuild(IObjectBuildContext ctx);
    }
}
