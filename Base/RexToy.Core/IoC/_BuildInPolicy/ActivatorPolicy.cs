using System;
using System.Collections.Generic;
using System.Reflection;

using RexToy.Xml;
using RexToy.Logging;

namespace RexToy.IoC
{
    class ActivatorPolicy : CreationPolicy
    {
        private static ILog _log = LogContext.GetLogger<ActivatorPolicy>();

        #region IPolicy Members

        public override void Initialize(XAccessor x)
        {
            _teardown = x.GetValue<bool>(TEARDOWN_ATTR) ?? false;

            var xParams = x.NavigateToList(PARAM);
            foreach (var xParam in xParams)
            {
                string name = xParam.GetStringValue(NAME_ATTR);
                string val = xParam.GetStringValue(VALUE_ATTR);
                if (string.IsNullOrEmpty(name))
                    ExceptionHelper.ThrowPolicyInitNullError(this.GetType(), NAME_ATTR);
                _injectionHints.Add(name, val);
            }
        }

        public override void OnBuildComplete(IObjectBuildContext ctx)
        {
            //Note:Nothing to do.
        }

        #endregion

        public override bool ReadyToBuild(IObjectBuildContext ctx)
        {
            ConstructorInfo[] ctors = ctx.ComponentType.GetConstructors();
            MethodBase[] methods = FilterByHintsAndSort(ctors);
            if (methods.Length == 0)
                return false;

            foreach (MethodBase method in methods)
            {
                if (IsReady(method, ctx))
                    return true;
            }
            return false;
        }

        public override void BuildUp(IObjectBuildContext ctx)
        {
            if (ctx.SkipCreationPolicy)
            {
                _log.Verbose("Activator policy is skipped.");
                return;
            }

            ConstructorInfo[] ctors = ctx.ComponentType.GetConstructors();
            MethodBase[] methods = FilterByHintsAndSort(ctors);
            if (methods.Length == 0)
                ExceptionHelper.ThrowNoValidConstructor(ctx.ComponentType);
            foreach (MethodBase method in methods)
            {
                if (IsReady(method, ctx))
                {
                    object[] args = PrepareArgs(ctx, method);
                    object instance = Activator.CreateInstance(ctx.ComponentType, args);
                    ctx.Instance = instance;
                    _log.Verbose("Activator policy create an instance of type [{0}].", instance.GetType());
                    return;
                }
            }

            _log.Debug("Activator policy create instance fail.");
            ExceptionHelper.ThrowConstructorNotReady(ctx.ComponentType);
        }
    }
}
