using System;
using System.Collections.Generic;

using RexToy.Xml;

namespace RexToy.IoC
{
    class SetterPolicy : IPolicy
    {
        private const string PROPERTY_ATTR = "@property";
        private const string VALUE_ATTR = "@value";
        private const string TEARDOWN_ATTR = "@teardown";

        private string _propertyName;
        private string _propertyValue;
        private bool _teardown;

        #region IPolicy Members

        public Stages Stage
        {
            get { return Stages.Initialization; }
        }

        public void Initialize(XAccessor x)
        {
            _teardown = x.GetValue<bool>(TEARDOWN_ATTR) ?? false;
            _propertyName = x.GetStringValue(PROPERTY_ATTR);
            if (string.IsNullOrEmpty(_propertyName))
                ExceptionHelper.ThrowPolicyInitNullError(this.GetType(), PROPERTY_ATTR);
            _propertyValue = x.GetStringValue(VALUE_ATTR);
        }

        public bool ReadyToBuild(IObjectBuildContext ctx)
        {
            Type targetType = (ctx.ComponentType != null) ? ctx.ComponentType : ctx.ServiceType;
            IReflector r = Reflector.Bind(targetType, ReflectorPolicy.TypePublic);
            return PolicyUtility.IsReady(_propertyValue, r.GetPropertyType(_propertyName), ctx);
        }

        public void BuildUp(IObjectBuildContext ctx)
        {
            if (ctx.SkipInitializationPolicy)
                return;

            IReflector r = Reflector.Bind(ctx.Instance);
            object val = PolicyUtility.Build(_propertyValue, r.GetPropertyType(_propertyName), ctx);
            r.SetPropertyValue(_propertyName, val);
        }

        public void OnBuildComplete(IObjectBuildContext ctx)
        {
            //Note:Nothing to do.
        }

        public void TearDown(object instance, IObjectBuildContext ctx)
        {
            if (!ctx.LifeCycleManagement || !_teardown)
                return;
            if (ctx.SkipInitializationPolicy)
                return;

            //Note:Not care about "direct casted value", it's not coming from container.
            if (_propertyValue == null || (_propertyValue.StartsWith("#{") && _propertyValue.EndsWith("}")))
            {
                IReflector r = Reflector.Bind(ctx.Instance);
                object val = r.GetPropertyValue(_propertyName);
                if (!val.GetType().IsValueType)
                    r.SetPropertyValue(_propertyName, null);
                ctx.Kernal.TearDown(val);
            }
        }

        #endregion
    }
}
