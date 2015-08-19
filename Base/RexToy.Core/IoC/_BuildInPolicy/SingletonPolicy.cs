using System;
using System.Collections.Generic;

using RexToy.Xml;
using RexToy.Logging;

namespace RexToy.IoC
{
    class SingletonPolicy : IPolicy
    {
        private static ILog _log = LogContext.GetLogger<SingletonPolicy>();

        private static object _lock = new object();

        private object _instance;

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
            if (_instance != null)
            {
                _log.Verbose("Singleton policy already hold an instance.");

                ctx.Instance = _instance;
                ctx.SkipCreationPolicy = true;
                ctx.SkipInitializationPolicy = true;
                ctx.SkipPostInitializationPolicy = true;
            }
            else
            {
                _log.Verbose("Singleton policy hold nothing, pass to next policy.");
            }
        }

        public void OnBuildComplete(IObjectBuildContext ctx)
        {
            lock (_lock)
            {
                if (_instance == null)
                    _instance = ctx.Instance;
                else
                {
                    if (!Object.ReferenceEquals(_instance, ctx.Instance))
                    {
                        //Note:Another thread create an instance already, release instance build by this thread
                        ctx.Kernal.TearDown(ctx.Instance);
                        //Note:And return the instance created by other thread.
                        ctx.Instance = _instance;
                    }
                }
            }
        }

        public void TearDown(object instance, IObjectBuildContext ctx)
        {
            //Note:Nothing to do.
        }

        #endregion
    }
}
