using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using RexToy.Logging;
using RexToy.Collections;

namespace RexToy.IoC
{
    class ObjectBuilder : IObjectBuilder
    {
        private static ILog _log = LogContext.GetLogger<ObjectBuilder>();

        private Kernal _k;
        public ObjectBuilder(Kernal k)
        {
            _k = k;
        }

        #region IObjectBuilder Members

        public bool ReadyToBuild(IComponentInfo info)
        {
            IObjectBuildContext ctx = new ObjectBuildContext(info, _k);
            foreach (IPolicy p in info.BuildPolicies)
            {
                if (!p.ReadyToBuild(ctx))
                    return false;
            }
            return true;
        }

        public IObjectBuildContext Build(IComponentInfo info)
        {
            IObjectBuildContext ctx = new ObjectBuildContext(info, _k);

            _log.Debug("Build [PreCreation] policies.");
            BuildUpSteps(ctx, Stages.PreCreation);
            _log.Debug("Build [Creation] policies.");
            BuildUpSteps(ctx, Stages.Creation);
            _log.Debug("Build [Initialization] policies.");
            BuildUpSteps(ctx, Stages.Initialization);
            _log.Debug("Build [PostInitialization] policies.");
            BuildUpSteps(ctx, Stages.PostInitialization);

            //Note:Notify all policy(let precreation policy have chance to know instance created).
            _log.Debug("Notify policies build complete.");
            foreach (IPolicy policy in info.BuildPolicies)
                policy.OnBuildComplete(ctx);

            _log.Debug("Build completed.");
            return ctx;
        }

        public void TearDown(object instance, IObjectBuildContext ctx)
        {
            //Note:Reverse order to teardown
            _log.Debug("Teardown [PostInitialization] policies.");
            TearDownSteps(instance, ctx, Stages.PostInitialization);
            _log.Debug("Teardown [Initialization] policies.");
            TearDownSteps(instance, ctx, Stages.Initialization);
            _log.Debug("Teardown [Creation] policies.");
            TearDownSteps(instance, ctx, Stages.Creation);
            _log.Debug("Teardown [PreCreation] policies.");
            TearDownSteps(instance, ctx, Stages.PreCreation);
            _log.Debug("Teardown complete.");
        }

        #endregion

        [SuppressMessage("Microsoft.Design", "CA1031")]
        private static void TearDownSteps(object instance, IObjectBuildContext ctx, Stages stage)
        {
            try
            {
                for (int idx = ctx.Policies.Count - 1; idx >= 0; idx--)
                {
                    if (ctx.Policies[idx].Stage == stage)
                        ctx.Policies[idx].TearDown(instance, ctx);
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.ThrowTearDownError(ex, stage);
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1031")]
        private static void BuildUpSteps(IObjectBuildContext ctx, Stages stage)
        {
            try
            {
                for (int idx = 0; idx < ctx.Policies.Count; idx++)
                {
                    if (ctx.Policies[idx].Stage == stage)
                        ctx.Policies[idx].BuildUp(ctx);
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.ThrowBuildUpError(ex, stage);
            }
        }
    }
}
