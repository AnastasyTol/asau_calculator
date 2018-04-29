using System;
using System.Runtime.Remoting.Services;

namespace slau_calc
{
    public class Fraction : object
    {
        public override string ToString()
        {
            return String.Format("{0}/{1}", this.Numerator, this.Denominator);
        }

        public double ToDouble()
        {
            return this.Numerator / this.Denominator;
        }

        // Нужна еще нормализация дроби по алгоритму Евклида (НОД)
        protected int Numerator, Denominator;

        public Fraction(int numerator, int denominator)
        {
            this.Numerator = numerator;
            this.Denominator = numerator != 0 ? denominator : 1;
        }
        
        public Fraction(int numerator)
        {
            this.Numerator = numerator;
            this.Denominator = 1;
        }
        
        // Приведени типа
        public static explicit operator Fraction(int value)
        {
            return new Fraction(value, 0);
        }
        
        public static Fraction operator +(Fraction a, Fraction b)
        {
            Fraction result = new Fraction(a.Numerator * b.Denominator + b.Numerator * a.Denominator,
                a.Denominator * b.Denominator);
            if (result.Numerator == 0)
                return result;
            else
                return result.Normalization();
        }
        
        public static Fraction operator -(Fraction a, Fraction b)
        {
            Fraction result = new Fraction(a.Numerator * b.Denominator - b.Numerator * a.Denominator,
                a.Denominator * b.Denominator);
            if (result.Numerator == 0)
                return result;
            else
                return result.Normalization();
        }
        
        public static Fraction operator *(Fraction a, Fraction b)
        {
            Fraction result = new Fraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator);
            if (result.Numerator == 0)
                return result;
            else
                return result.Normalization();
        }
        
        // ----------------------------------------------------------------------------------------
        // Операция равенства
        
        public override bool Equals(Object obj)
        {
            if (obj == null || !(obj is Fraction))
                return false;
            else
                return (Fraction)obj == this;
        }
        
        public static bool operator == (Fraction a, Fraction b)
        {
            return a.Numerator == b.Numerator && a.Denominator == b.Denominator;
        }
        
        public static bool operator != (Fraction a, Fraction b)
        {
            return a.Numerator != b.Numerator && a.Denominator != b.Denominator;
        }
        
        public static bool operator == (Fraction a, int b)
        {
            return (double)a.Numerator / (double)a.Denominator == (double)b;
        }
        
        public static bool operator != (Fraction a, int b)
        {
            return (double)a.Numerator / (double)a.Denominator != (double)b;
        }
        
        public static bool operator == (int a, Fraction b)
        {
            return (double)b.Numerator / (double)b.Denominator == (double)a;
        }
        
        public static bool operator != (int a, Fraction b)
        {
            return (double)b.Numerator / (double)b.Denominator != (double)a;
        }
        
        
        public static bool operator > (Fraction a, Fraction b)
        {
            if ((double) a.Numerator / (double) a.Denominator > (double) b.Numerator / (double) b.Denominator)
                return true;
            else
                return false;
        }
        
        public static bool operator < (Fraction a, Fraction b)
        {
            if ((double) a.Numerator / (double) a.Denominator < (double) b.Numerator / (double) b.Denominator)
                return true;
            else
                return false;
        }
        
//        public override bool Equals(Object obj) 
//        {
//            // Check for null values and compare run-time types.
//            if (obj == null || GetType() != obj.GetType()) 
//                return false;
//
//            Point p = (Point)obj;
//            return (x == p.x) && (y == p.y);
//        }
        // ----------------------------------------------------------------------------------------
        
        
        public static Fraction operator /(Fraction a, Fraction b)
        {
            Fraction result = new Fraction(a.Numerator * b.Denominator, b.Numerator * a.Denominator);
            if (result.Numerator == 0)
                return result;
            else
                return result.Normalization();
                
        }

//        public void Normalization()
//        {
//            ;
//        }
        
        public override int GetHashCode() 
        {
            return this.Numerator ^ this.Denominator;
        }
        
        public Fraction Normalization()//Нормализация дроби
        {
            return new Fraction(Numerator / GetCommonDivisor(Numerator, Denominator), Denominator / GetCommonDivisor(Numerator, Denominator));
        }
        
        private static int GetCommonDivisor(int i, int j)//Алгоритм Евклида НОД
        {
            i = Math.Abs(i);
            j = Math.Abs(j);
            while (i != j)
                if (i > j) { i -= j; }
                else { j -= i; }
            return i;
        }
        
    }
}