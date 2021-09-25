using System;

namespace Incapsulation.RationalNumbers
{
    public class Rational
    {
        private static readonly Rational NanRational = new Rational(1, 0);

        public Rational(int numerator, int denominator = 1)
        {
            Numerator = numerator;
            Denominator = denominator;
            if (IsNan) return;

            if (denominator < 0)
            {
                Numerator = -numerator;
                Denominator = -denominator;
            }

            if (numerator == 1 || numerator == -1 || denominator == 1)
                return;

            var divider = GetGreatestCommonDivisor(numerator, denominator);
            Numerator /= divider;
            Denominator /= divider;
        }

        public int Numerator { get; }
        public int Denominator { get; }
        public bool IsNan => Denominator == 0;

        public static Rational operator +(Rational first, Rational second)
        {
            return new Rational(first.Numerator * second.Denominator + second.Numerator * first.Denominator,
                first.Denominator * second.Denominator);
        }


        public static Rational operator -(Rational first, Rational second)
        {
            return first + new Rational(-second.Numerator, second.Denominator);
        }

        public static Rational operator *(Rational first, Rational second)
        {
            return new Rational(first.Numerator * second.Numerator, first.Denominator * second.Denominator);
        }

        public static Rational operator /(Rational first, Rational second)
        {
            if (first.IsNan || second.IsNan) return NanRational;
            return first * new Rational(second.Denominator, second.Numerator);
            ;
        }

        public static implicit operator double(Rational rational)
        {
            if (rational.IsNan)
                return double.NaN;

            return rational.Numerator * 1.0f / rational.Denominator;
        }

        public static implicit operator Rational(int number)
        {
            return new Rational(number);
        }

        public static explicit operator int(Rational rational)
        {
            if (rational.Denominator != 1)
                throw new Exception();

            return rational.Numerator;
        }

        private static int GetGreatestCommonDivisor(int a, int b)
        {
            if (b < 0)
                b = -b;

            if (a < 0)
                a = -a;

            while (b > 0)
            {
                var temp = b;
                b = a % b;
                a = temp;
            }

            return a;
        }
    }
}