using System;
using System.Collections.Generic;

using RexToy.Xml;

namespace RexToy.IoC
{
    class StatelessPolicy : IPolicy
    {
        #region IPolicy Members

        public Stages Stage
        {
            get { return Stages.PreCreation; }
        }

        public void Initialize(XAccessor x)
        {
            //Note:No additional settings.
        }

        public bool ReadyToBuild(IObjectBuildContext ctx)
        {
            return true;
        }

        public void BuildUp(IObjectBuildContext ctx)
        {
            //Note:Nothing to do.
        }

        public void OnBuildComplete(IObjectBuildContext ctx)
        {
            //Note:Nothing to do.
        }

        public void TearDown(object instance, IObjectBuildContext ctx)
        {
            IDisposable d = instance as IDisposable;
            if (d != null)
                d.Dispose();
        }

        #endregion
    }
}
