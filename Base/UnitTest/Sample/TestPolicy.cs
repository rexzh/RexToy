using System;
using System.Collections.Generic;

using RexToy.IoC;
using RexToy.Xml;

namespace UnitTest.Sample
{
    class TestPolicy : IPolicy
    {
        #region IPolicy Members

        public Stages Stage
        {
            get { return Stages.PreCreation; }
        }

        public void Initialize(XAccessor x)
        {

        }

        public void BuildUp(IObjectBuildContext ctx)
        {

        }

        public void OnBuildComplete(IObjectBuildContext ctx)
        {

        }

        public void TearDown(object instance, IObjectBuildContext ctx)
        {

        }

        public bool ReadyToBuild(IObjectBuildContext ctx)
        {
            return true;
        }

        #endregion
    }
}
