using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace RexToy.Numeric
{
    [Serializable]
    [TypeConverter(typeof(MeasuredNumberConverter))]
    public struct MeasuredNumber : IFormattable, IEquatable<MeasuredNumber>, IComparable, IComparable<MeasuredNumber>
    {
        private double _number;
        public double Number
        {
            get { return _number; }
        }

        private MeasureUnit _unit;
        public MeasureUnit Unit
        {
            get { return _unit; }
        }

        private MeasuredNumber(double n, string u)
        {
            _number = n;
            _unit = MeasureUnit.Parse(u);
        }

        private MeasuredNumber(double n, MeasureUnit u)
        {
            _number = n;
            _unit = u;
        }


        public static bool TryParse(string val, out MeasuredNumber v)
        {
            v = new MeasuredNumber(0, string.Empty);
            if (string.IsNullOrWhiteSpace(val))
                return false;

            bool negative;
            string num, unit;
            bool b = InternalParse(val, out negative, out num, out unit);
            if (!b)
                return false;

            double d;
            b = double.TryParse(num, out d);
            if (!b)
                return false;
            else
            {
                v = new MeasuredNumber((negative ? -1 : 1) * d, unit);
                return true;
            }
        }

        public static MeasuredNumber Parse(string val)
        {
            val.ThrowIfNullArgument(nameof(val));
            if (string.IsNullOrWhiteSpace(val))
                throw new FormatException(ExceptionHelper.Format_Err);

            bool negative;
            string num, unit;
            bool b = InternalParse(val, out negative, out num, out unit);
            if (!b)
                throw new FormatException(ExceptionHelper.Format_Err);

            double d;
            b = double.TryParse(num, out d);
            if (!b)
                throw new FormatException(ExceptionHelper.Format_Err);

            return new MeasuredNumber((negative ? -1 : 1) * d, unit);
        }

        private static bool InternalParse(string val, out bool negative, out string num, out string unit)
        {
            //Note:str null/empty won't pass in.
            negative = false;
            num = string.Empty;
            unit = string.Empty;

            char fst = val[0];
            if (fst == '-' || fst == '+')
            {
                negative = (fst == '-');
                val = val.Substring(1);
            }

            var idx = -1;
            for (var i = 0; i < val.Length; i++)
            {
                if (char.IsDigit(val[i]) || val[i] == '.')
                {
                    continue;
                }
                idx = i;
                break;
            }

            switch (idx)
            {
                case -1:
                    num = val;
                    return true;

                case 0://Note:Error, no number                    
                    return false;

                default://Note:>0
                    num = val.Substring(0, idx);
                    unit = val.Substring(idx);
                    return true;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}{1}", _number, _unit);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return this._number.ToString(format, formatProvider) + _unit.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj is MeasuredNumber)
            {
                MeasuredNumber other = (MeasuredNumber)obj;
                return (other.Number == this.Number) && (other.Unit == this.Unit);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return _number.GetHashCode();
        }

        public static MeasuredNumber operator +(MeasuredNumber l, MeasuredNumber r)
        {
            if (l.Unit != r.Unit)
                throw new UnitMismatchException(string.Format(ExceptionHelper.Unit_Mismatch_Err, l, "+", r));
            else
                return new MeasuredNumber(l.Number + r.Number, l.Unit);
        }

        public static MeasuredNumber operator -(MeasuredNumber l, MeasuredNumber r)
        {
            if (l.Unit != r.Unit)
                throw new UnitMismatchException(string.Format(ExceptionHelper.Unit_Mismatch_Err, l, "-", r));
            else
                return new MeasuredNumber(l.Number - r.Number, l.Unit);
        }

        public static MeasuredNumber operator *(MeasuredNumber l, MeasuredNumber r)
        {
            return new MeasuredNumber(l.Number * r.Number, l.Unit * r.Unit);
        }

        public static MeasuredNumber operator /(MeasuredNumber l, MeasuredNumber r)
        {
            return new MeasuredNumber(l.Number / r.Number, l.Unit / r.Unit);
        }

        public static MeasuredNumber operator *(MeasuredNumber l, int r)
        {
            return new MeasuredNumber(l.Number * r, l.Unit);
        }

        public static MeasuredNumber operator *(int l, MeasuredNumber r)
        {
            return new MeasuredNumber(l * r.Number, r.Unit);
        }

        public static MeasuredNumber operator /(MeasuredNumber l, int r)
        {
            return new MeasuredNumber(l.Number / r, l.Unit);
        }

        public static MeasuredNumber operator /(int l, MeasuredNumber r)
        {
            return new MeasuredNumber(l / r.Number, r.Unit.Reciprocal());
        }

        public static MeasuredNumber operator *(MeasuredNumber l, double r)
        {
            return new MeasuredNumber(l.Number * r, l.Unit);
        }

        public static MeasuredNumber operator *(double l, MeasuredNumber r)
        {
            return new MeasuredNumber(l * r.Number, r.Unit);
        }

        public static MeasuredNumber operator /(MeasuredNumber l, double r)
        {
            return new MeasuredNumber(l.Number / r, l.Unit);
        }

        public static MeasuredNumber operator /(double l, MeasuredNumber r)
        {
            return new MeasuredNumber(l / r.Number, r.Unit.Reciprocal());
        }

        public static bool operator ==(MeasuredNumber x, MeasuredNumber y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(MeasuredNumber x, MeasuredNumber y)
        {
            return !x.Equals(y);
        }

        public bool Equals(MeasuredNumber other)
        {
            return this.Number == other.Number && this.Unit == other.Unit;
        }

        public static bool operator <(MeasuredNumber l, MeasuredNumber r)
        {
            if (l.Unit != r.Unit)
                throw new UnitMismatchException(string.Format(ExceptionHelper.Unit_Mismatch_Err, l, "<", r));
            return l._number < r._number;
        }

        public static bool operator <=(MeasuredNumber l, MeasuredNumber r)
        {
            if (l.Unit != r.Unit)
                throw new UnitMismatchException(string.Format(ExceptionHelper.Unit_Mismatch_Err, l, "<=", r));
            return l._number <= r._number;
        }

        public static bool operator >(MeasuredNumber l, MeasuredNumber r)
        {
            if (l.Unit != r.Unit)
                throw new UnitMismatchException(string.Format(ExceptionHelper.Unit_Mismatch_Err, l, ">", r));
            return l._number > r._number;
        }

        public static bool operator >=(MeasuredNumber l, MeasuredNumber r)
        {
            if (l.Unit != r.Unit)
                throw new UnitMismatchException(string.Format(ExceptionHelper.Unit_Mismatch_Err, l, ">=", r));
            return l._number >= r._number;
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            if (obj is MeasuredNumber)
            {
                MeasuredNumber other = (MeasuredNumber)obj;
                return CompareTo(other);
            }
            else
            {
                throw new ArgumentException(ExceptionHelper.Must_Compare_To_MeasuredNumber);
            }
        }

        public int CompareTo(MeasuredNumber other)
        {
            if (this._unit != other._unit)
                throw new ArgumentException(ExceptionHelper.Compare_Unit_Mismatch);

            if (this._number < other._number)
                return -1;
            if (this._number == other._number)
                return 0;
            return 1;
        }
    }
}
