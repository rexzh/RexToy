using System;
using System.Collections.Generic;
using System.Reflection;

using RexToy.Logging;
using RexToy.Xml;

namespace RexToy.IoC
{
    class FactoryPolicy : CreationPolicy
    {
        private const string CLASS = "class";
        private const string METHOD = "method";

        private static ILog _log = LogContext.GetLogger<FactoryPolicy>();

        private Type _factoryType;
        private string _methodName;

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
            string strFactoryType = x.GetStringValue(CLASS);
            if (string.IsNullOrEmpty(strFactoryType))
                ExceptionHelper.ThrowPolicyInitNullError(this.GetType(), CLASS);
            _factoryType = Reflector.LoadType(strFactoryType);
            _methodName = x.GetStringValue(METHOD);
            if (string.IsNullOrEmpty(_methodName))
                ExceptionHelper.ThrowPolicyInitNullError(this.GetType(), METHOD);
        }

        public override void OnBuildComplete(IObjectBuildContext ctx)
        {
            //Note:Nothing to do.
        }

        public override void BuildUp(IObjectBuildContext ctx)
        {
            if (ctx.SkipCreationPolicy)
            {
                _log.Verbose("Factory policy is skipped.");
                return;
            }

            IReflector r = Reflector.Bind(_factoryType, ReflectorPolicy.TypeAll);
            MethodBase[] methods = FilterByHintsAndSort(r.GetMethods(_methodName));
            if (methods.Length == 0)
                ExceptionHelper.ThrowNoValidMethod(_factoryType, _methodName);
            foreach (MethodBase method in methods)
            {
                if (IsReady(method, ctx))
                {
                    object[] args = PrepareArgs(ctx, method);
                    object instance = r.Invoke(_methodName, args);
                    ctx.Instance = instance;
                    _log.Verbose("Factory policy create an instance of type [{0}].", instance.GetType());
                    return;
                }
            }

            _log.Debug("Factory policy create instance fail.");
            ExceptionHelper.ThrowMethodNotReady(_factoryType, _methodName);
        }

        public override bool ReadyToBuild(IObjectBuildContext ctx)
        {
            IReflector r = Reflector.Bind(_factoryType, ReflectorPolicy.TypePublic);
            MethodBase[] methods = FilterByHintsAndSort(r.GetMethods(this._methodName));
            if (methods.Length == 0)
                return false;

            foreach (MethodBase method in methods)
            {
                if (IsReady(method, ctx))
                    return true;
            }
            return false;
        }

        #endregion
    }
}
