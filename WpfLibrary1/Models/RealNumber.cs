using System;

namespace RealNumberApp.Models
{
    public class RealNumber : IComparable<RealNumber>
    {
        public int Sign { get; private set; }
        public double Mantissa { get; private set; }
        public int Exponent { get; private set; }

        public RealNumber(int sign, double mantissa, int exponent)
        {
            if (mantissa < 0)
                throw new ArgumentException("Мантисса должна быть неотрицательной");

            Sign = mantissa == 0 ? 1 : Math.Sign(sign);
            Mantissa = mantissa;
            Exponent = exponent;

            Normalize();
        }

        private void Normalize()
        {
            if (Mantissa == 0) return;

            while (Mantissa >= 10)
            {
                Mantissa /= 10;
                Exponent++;
            }

            while (Mantissa < 1)
            {
                Mantissa *= 10;
                Exponent--;
            }
        }

        public double ToDouble()
        {
            return Sign * Mantissa * Math.Pow(10, Exponent);
        }

        public static RealNumber FromDouble(double value)
        {
            if (value == 0) return new RealNumber(1, 0, 0);

            int sign = Math.Sign(value);
            value = Math.Abs(value);

            int exponent = (int)Math.Floor(Math.Log10(value));
            double mantissa = value / Math.Pow(10, exponent);

            return new RealNumber(sign, mantissa, exponent);
        }

        public override string ToString()
        {
            return $"{(Sign < 0 ? "-" : "")}{Mantissa}E{Exponent}";
        }

        public int CompareTo(RealNumber other)
        {
            return ToDouble().CompareTo(other.ToDouble());
        }

        public static RealNumber operator +(RealNumber a, RealNumber b)
            => FromDouble(a.ToDouble() + b.ToDouble());

        public static RealNumber operator -(RealNumber a, RealNumber b)
            => FromDouble(a.ToDouble() - b.ToDouble());

        public static RealNumber operator *(RealNumber a, RealNumber b)
        {
            int sign = a.Sign * b.Sign;
            double mantissa = a.Mantissa * b.Mantissa;
            int exponent = a.Exponent + b.Exponent;

            return new RealNumber(sign, mantissa, exponent);
        }

        public static RealNumber operator /(RealNumber a, RealNumber b)
        {
            if (b.Mantissa == 0)
                throw new DivideByZeroException();

            int sign = a.Sign * b.Sign;
            double mantissa = a.Mantissa / b.Mantissa;
            int exponent = a.Exponent - b.Exponent;

            return new RealNumber(sign, mantissa, exponent);
        }
    }
}