using System;
using System.ComponentModel;

namespace RexToy.Numeric
{
    [Serializable]
    [TypeConverter(typeof(PercentageConverter))]
    public struct Percentage : IConvertible, IEquatable<Percentage>, IComparable, IComparable<Percentage>, IFormattable
    {
        public static readonly Percentage HundredPercent = Percentage.Parse("100%");
        public static readonly Percentage ZeroPercent = Percentage.Parse("0%");

        private double numerator;
        public Percentage(double val)
        {
            numerator = val * 100;
        }

        public double Value
        {
            get { return numerator / 100; }
        }

        public override string ToString()
        {
            return numerator.ToString() + '%';
        }

        public string ToString(IFormatProvider provider)
        {
            return numerator.ToString(provider) + '%';
        }

        public string ToString(string format)
        {
            return numerator.ToString(format) + '%';
        }

        public string ToString(string format, IFormatProvider provider)
        {
            return numerator.ToString(format, provider) + '%';
        }

        public static explicit operator double(Percentage p)
        {
            return (double)p.Value;
        }

        public static explicit operator Percentage(double d)
        {
            return new Percentage(d);
        }

        public static Percentage Parse(string str)
        {
            str.ThrowIfNullArgument(nameof(str));
            if (string.IsNullOrWhiteSpace(str))
                throw new FormatException(ExceptionHelper.Format_Err);

            str = str.Trim();
            if (!str.EndsWith("%"))
            {
                double d = double.Parse(str);
                return new Percentage(d);
            }
            else
            {
                double d = double.Parse(str.Substring(0, str.Length - 1));
                return new Percentage(d / 100);
            }
        }

        public static bool TryParse(string str, out Percentage p)
        {
            p = Percentage.ZeroPercent;
            if (string.IsNullOrWhiteSpace(str))
                return false;
            str = str.Trim();
            if (!str.EndsWith("%"))
            {
                return false;
            }
            else
            {
                double d;
                bool result = double.TryParse(str.Substring(0, str.Length - 1), out d);
                if (result)
                    p = new Percentage(d / 100);
                else
                    p = Percentage.ZeroPercent;
                return result;
            }
        }

        public static bool operator ==(Percentage lhs, Percentage rhs)
        {
            return lhs.Value == rhs.Value;
        }

        public static bool operator !=(Percentage lhs, Percentage rhs)
        {
            return lhs.Value != rhs.Value;
        }

        public bool Equals(Percentage other)
        {
            return this.numerator == other.numerator;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj is Percentage)
            {
                return this.numerator == ((Percentage)obj).numerator;
            }
            return this.Value.Equals(obj);
        }

        public static bool operator >(Percentage lhs, Percentage rhs)
        {
            return lhs.Value > rhs.Value;
        }

        public static bool operator <(Percentage lhs, Percentage rhs)
        {
            return lhs.Value < rhs.Value;
        }

        public static bool operator >=(Percentage lhs, Percentage rhs)
        {
            return lhs.Value >= rhs.Value;
        }

        public static bool operator <=(Percentage lhs, Percentage rhs)
        {
            return lhs.Value <= rhs.Value;
        }

        public static Percentage operator +(Percentage lhs, Percentage rhs)
        {
            return new Percentage(lhs.Value + rhs.Value);
        }

        public static Percentage operator -(Percentage lhs, Percentage rhs)
        {
            return new Percentage(lhs.Value - rhs.Value);
        }

        public static Percentage operator *(Percentage lhs, Percentage rhs)
        {
            return new Percentage(lhs.Value * rhs.Value);
        }

        public static Percentage operator /(Percentage lhs, Percentage rhs)
        {
            return new Percentage(lhs.Value / rhs.Value);
        }

        public static double operator *(double lhs, Percentage rhs)
        {
            return lhs * rhs.Value;
        }

        public static double operator /(double lhs, Percentage rhs)
        {
            return lhs / rhs.Value;
        }

        public static double operator *(Percentage lhs, double rhs)
        {
            return lhs.Value * rhs;
        }

        public static Percentage operator /(Percentage lhs, double rhs)
        {
            return new Percentage(lhs.Value / rhs);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        #region IConvertible
        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            return (bool)Convert.ChangeType(Value, typeof(bool), provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return (byte)Convert.ChangeType(Value, typeof(byte), provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            return (char)Convert.ChangeType(Value, typeof(char), provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            return (DateTime)Convert.ChangeType(Value, typeof(DateTime), provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return (decimal)Value;
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return (double)Value;
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return (short)Value;
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return (int)Value;
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return (long)Value;
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return (sbyte)Value;
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return (float)Value;
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            return this.ToString();
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType(Value, conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return (ushort)Value;
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return (uint)Value;
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return (ulong)Value;
        }
        #endregion

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            if (obj is Percentage)
            {
                Percentage other = (Percentage)obj;
                return CompareTo(other);
            }
            else
            {
                throw new ArgumentException(ExceptionHelper.Must_Compare_To_Percentage);
            }
        }

        public int CompareTo(Percentage other)
        {
            if (this.numerator < other.numerator)
                return -1;
            if (this.numerator == other.numerator)
                return 0;
            return 1;
        }
    }
}
