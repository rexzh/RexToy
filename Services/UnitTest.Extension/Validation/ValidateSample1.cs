using System;
using System.Collections.Generic;

using RexToy.Validation;

namespace UnitTest.Extension.Validation
{
    [CustomizeValidate]
    public class ValidateSample1
    {
        [NotNullValidate]
        [ExpressionLanguageValidate("value.Length>0 && value.Length<=16")]
        public string Name { get; set; }

        [NotNullValidate]
        [RegexValidator("^\\d{3}-\\d{4}$")]
        public string Code { get; set; }
        
        public int Salary { get; set; }

        [ValidateMethod]
        public void SelfCheck(IValidateResult result)
        {
            if (Salary <= 0)
            {
                result.Set("Salary", "Validate Error : Salary <= 0");
            }
        }
    }
}
