using System;
using System.Collections.Generic;

namespace RexToy.Numeric
{
    public struct MeasureUnit
    {
        public static readonly MeasureUnit None = MeasureUnit.Parse(string.Empty);

        public static MeasureUnit Parse(string u)
        {
            u.ThrowIfNullArgument(nameof(u));

            if (u == string.Empty)
                return new MeasureUnit();

            string[] parts = u.Split('/');
            switch (parts.Length)
            {
                case 1:
                    var p = parts[0].Split('*');
                    return new MeasureUnit(p, new string[0]);

                case 2:
                    var n = parts[0].Split('*');
                    string[] d = parts[1].UnBracketing(StringPair.Parenthesis).Split('*');
                    return new MeasureUnit(n, d);

                default:
                    throw new FormatException(ExceptionHelper.Format_Err);
            }
        }

        private MeasureUnit ReduceFractions()
        {
            string fraction;
            do
            {
                fraction = null;
                int nIdx = 0, dIdx = 0;
                for (var i = 0; i < _numerator.Length; i++)
                {
                    dIdx = Array.IndexOf<string>(_denominator, _numerator[i]);
                    if (dIdx >= 0)
                    {
                        nIdx = i;
                        fraction = _numerator[i];
                        break;
                    }
                }
                if (!string.IsNullOrEmpty(fraction))
                {
                    _numerator = _numerator.Reduce(nIdx);
                    _denominator = _denominator.Reduce(dIdx);
                }
            } while (fraction != null);

            return this;
        }

        internal MeasureUnit Reciprocal()
        {
            return new MeasureUnit(_denominator, _numerator);
        }

        private string[] _numerator;
        private string[] _denominator;

        internal MeasureUnit(string[] n, string[] d)
        {
            _numerator = n ?? new string[0];
            _denominator = d ?? new string[0];
        }

        public override string ToString()
        {
            var n = (_numerator == null) ? string.Empty : _numerator.Join('*');
            var d = (_denominator == null) ? string.Empty : _denominator.Join('*');
            if (_denominator != null && _denominator.Length > 1)
                d = d.Bracketing(StringPair.Parenthesis);
            if (d.Length == 0)
                return n;
            else
                return string.Format("{0}/{1}", n, d);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is MeasureUnit))
                return false;

            MeasureUnit that = (MeasureUnit)obj;
            if (StringArrayEqual(this._numerator, that._numerator) && StringArrayEqual(this._denominator, that._denominator))
                return true;
            else
                return false;
        }

        private bool StringArrayEqual(string[] x, string[] y)
        {
            if (x == null && y == null)
                return true;
            if (x == null && y != null && y.Length == 0)
                return true;
            if (x != null && x.Length == 0 && y == null)
                return true;

            if (x.Length != y.Length)
                return false;
            string[] a = x.Sort();
            string[] b = y.Sort();
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                    return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public static bool operator ==(MeasureUnit x, MeasureUnit y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(MeasureUnit x, MeasureUnit y)
        {
            return !x.Equals(y);
        }

        public static MeasureUnit operator *(MeasureUnit x, MeasureUnit y)
        {
            var u = new MeasureUnit(x._numerator.Combine(y._numerator), x._denominator.Combine(y._denominator));
            return u.ReduceFractions();
        }

        public static MeasureUnit operator /(MeasureUnit x, MeasureUnit y)
        {
            var u = new MeasureUnit(x._numerator.Combine(y._denominator), x._denominator.Combine(y._numerator));
            return u.ReduceFractions();
        }
    }
}
