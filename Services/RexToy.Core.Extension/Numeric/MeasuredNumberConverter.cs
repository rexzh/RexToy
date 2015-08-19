using System;
using System.ComponentModel;
using System.Globalization;

namespace RexToy.Numeric
{
    public class MeasuredNumberConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string) || sourceType == typeof(MeasuredNumber))
                return true;

            return false;
        }


        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string str = value as string;
            if (str != null)
                return MeasuredNumber.Parse(str);

            return null;
        }
    }
}
