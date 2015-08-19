using System;
using System.Collections.Generic;

using RexToy.Collections;
using RexToy.ExpressionLanguage;

namespace RexToy.Validation
{
    /// <summary>
    /// EL Syntax:
    /// "value" refer to current property.
    /// "instance" refer to whole instance.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct)]
    public class ExpressionLanguageValidateAttribute : ValidatePropertyAttribute
    {
        private const string INSTANCE = "instance";
        private const string VALUE = "value";
        private const string MSG = "Validate error: [{1}.{0}] is [{2}]. fail to pass [{3}]";

        private string _expr;
        public ExpressionLanguageValidateAttribute(string expr)
        {
            _expr = expr;
        }

        protected override void DoCheck(IValidateResult result, object instance, object propertyValue)
        {
            //Extend:use cache to improve performance.
            ExpressionLanguageEngineConfig cfg = new ExpressionLanguageEngineConfig(EvalExceptionHandlingPolicy.IgnorePolicy);
            ExpressionLanguageEngine engine = ExpressionLanguageEngine.CreateEngine(cfg);
            engine.Assign(INSTANCE, instance);
            engine.Assign(VALUE, propertyValue);
            object b = engine.Eval(_expr);
            if (!engine.EvalToBool(b))
            {
                string msg = string.Format(MSG, _pInfo.Name, instance.GetType().Name, propertyValue, _expr);
                this.LogValidateResult(result, msg);
            }
        }
    }
}
