using System;
using System.Collections.Generic;

using RexToy.Xml;
using RexToy.Logging;

namespace RexToy.IoC
{
    class InstancePolicy : IPolicy
    {
        private static ILog _log = LogContext.GetLogger<InstancePolicy>();

        private const string CLASS = "class";
        private const string MEMBER = "member";

        public Stages Stage
        {
            get { return Stages.Creation; }
        }

        private object _instance;
        public void Initialize(Xml.XAccessor x)
        {
            string strHostType = x.GetStringValue(CLASS);
            if (string.IsNullOrEmpty(strHostType))
                ExceptionHelper.ThrowPolicyInitNullError(this.GetType(), CLASS);
            Type hostType = Reflector.LoadType(strHostType);
            string memberName = x.GetStringValue(MEMBER);
            if (string.IsNullOrEmpty(memberName))
                ExceptionHelper.ThrowPolicyInitNullError(this.GetType(), MEMBER);

            IReflector r = Reflector.Bind(hostType, ReflectorPolicy.TypePublic);
            _instance = r.GetPropertyOrFieldValue(memberName, false);
        }

        public void BuildUp(IObjectBuildContext ctx)
        {
            if (ctx.SkipCreationPolicy)
            {
                _log.Verbose("Instance policy is skipped.");
                return;
            }

            _log.Verbose("Instance policy return instance of type [{0}].", _instance.GetType());
            ctx.Instance = _instance;
        }

        public void OnBuildComplete(IObjectBuildContext ctx)
        {
            //Note:Nothing to do.
        }

        public void TearDown(object instance, IObjectBuildContext ctx)
        {
            //Note:Nothing to do.
        }

        public bool ReadyToBuild(IObjectBuildContext ctx)
        {
            return true;
        }
    }
}
