using System;

namespace RealNumberApp.Models
{
    public class RealNumber : IComparable<RealNumber>
    {
        public int Sign { get; private set; } // +1 или -1
        public double Mantissa { get; private set; } // >= 0
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

        public override string ToString()
        {
            return $"{(Sign < 0 ? "-" : "")}{Mantissa}E{Exponent}";
        }

        // 🔥 Сложение без double
        public static RealNumber operator +(RealNumber a, RealNumber b)
        {
            AlignExponents(ref a, ref b);

            double mantissa = a.Sign * a.Mantissa + b.Sign * b.Mantissa;
            int sign = Math.Sign(mantissa);

            return new RealNumber(sign, Math.Abs(mantissa), a.Exponent);
        }

        // 🔥 Вычитание
        public static RealNumber operator -(RealNumber a, RealNumber b)
        {
            return a + new RealNumber(-b.Sign, b.Mantissa, b.Exponent);
        }

        // 🔥 Умножение
        public static RealNumber operator *(RealNumber a, RealNumber b)
        {
            int sign = a.Sign * b.Sign;
            double mantissa = a.Mantissa * b.Mantissa;
            int exponent = a.Exponent + b.Exponent;

            return new RealNumber(sign, mantissa, exponent);
        }

        // 🔥 Деление
        public static RealNumber operator /(RealNumber a, RealNumber b)
        {
            if (b.Mantissa == 0)
                throw new DivideByZeroException();

            int sign = a.Sign * b.Sign;
            double mantissa = a.Mantissa / b.Mantissa;
            int exponent = a.Exponent - b.Exponent;

            return new RealNumber(sign, mantissa, exponent);
        }

        // 🔥 Сравнение без double
        public int CompareTo(RealNumber other)
        {
            if (this.Sign != other.Sign)
                return this.Sign.CompareTo(other.Sign);

            if (this.Exponent != other.Exponent)
                return this.Sign * this.Exponent.CompareTo(other.Exponent);

            return this.Sign * this.Mantissa.CompareTo(other.Mantissa);
        }
        internal class RealNumberRaw : RealNumber
        {
            public RealNumberRaw(int sign, double mantissa, int exponent)
                : base(1, 1, 0)
            {
                Sign = mantissa == 0 ? 1 : Math.Sign(sign);
                Mantissa = mantissa;
                Exponent = exponent;
            }
        }
        private static void AlignExponents(ref RealNumber a, ref RealNumber b)
        {
            if (a.Exponent > b.Exponent)
            {
                int diff = a.Exponent - b.Exponent;

                double newMantissa = b.Mantissa;
                for (int i = 0; i < diff; i++)
                    newMantissa /= 10;

                b = new RealNumberRaw(b.Sign, newMantissa, a.Exponent);
            }
            else if (b.Exponent > a.Exponent)
            {
                int diff = b.Exponent - a.Exponent;

                double newMantissa = a.Mantissa;
                for (int i = 0; i < diff; i++)
                    newMantissa /= 10;

                a = new RealNumberRaw(a.Sign, newMantissa, b.Exponent);
            }
        }
    }
}