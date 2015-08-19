using System;
using System.ComponentModel;
using System.Globalization;

namespace RexToy.Numeric
{
    public class PercentageConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType.Implemented(typeof(IConvertible)) && sourceType != typeof(DateTime))
                return true;

            return base.CanConvertFrom(context, sourceType);
        }


        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string str = value as string;
            if (str != null)
                return Percentage.Parse(str);

            IConvertible cvtable = value as IConvertible;
            if (cvtable != null)
            {
                double d = cvtable.ToDouble(null);
                return new Percentage(d);
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}
