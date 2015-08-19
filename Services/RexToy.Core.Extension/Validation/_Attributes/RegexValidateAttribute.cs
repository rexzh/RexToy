using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RexToy.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RegexValidator : ValidatePropertyAttribute
    {
        private const string MSG = "Validate error: [{1}].[{0}] = [{3}] can not match Regex[\"{2}\"].";

        private Regex _regex;
        public RegexValidator(string pattern)
        {
            _regex = new Regex(pattern);
        }

        protected override void DoCheck(IValidateResult result, object instance, object propertyValue)
        {
            if (propertyValue == null)
            {
                string msg = string.Format(MSG, _pInfo.Name, instance.GetType().Name, _regex, propertyValue);
                this.LogValidateResult(result, msg);
                return;
            }

            if (!(propertyValue is string))
            {
                ExceptionHelper.ThrowInvalidPropertyAttribute(this, _pInfo);
            }

            bool match = _regex.IsMatch((string)propertyValue);
            if (!match)
            {
                string msg = string.Format(MSG, _pInfo.Name, instance.GetType().Name, _regex, propertyValue);
                this.LogValidateResult(result, msg);
            }
        }
    }
}
