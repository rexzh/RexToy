using System;

namespace RexToy.Numeric
{
    public static class ExceptionHelper
    {
        public const string Must_Compare_To_Percentage = "Must compare with Percentage.";
        public const string Must_Compare_To_MeasuredNumber = "Must compare with MeasuredNumber.";

        public const string Unit_Mismatch_Err = "Invalid operation [{0}] {1} [{2}].";        

        public const string Format_Err = "Input string was not in a correct format.";
        
        public const string Compare_Unit_Mismatch = "Can not compare measured number have different unit.";
    }
}
