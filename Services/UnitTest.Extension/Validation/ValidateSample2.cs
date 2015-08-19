using System;
using System.Collections.Generic;

using RexToy.Validation;

namespace UnitTest.Extension.Validation
{
    public class ValidateSample2
    {
        [ExpressionLanguageValidate("value > 0")]
        public int LowerBound { get; set; }

        [ExpressionLanguageValidate("value > instance.LowerBound")]
        public int HighBound { get; set; }
    }
}
