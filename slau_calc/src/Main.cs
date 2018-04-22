using System;

namespace slau_calc
{
    internal class MainC
    {
        public static void Main(string[] args)
        {
            Slau s = new Slau();
            Fraction[,] matrix =
            {
                {new Fraction(3, 1), new Fraction(7, 1), new Fraction(9, 1), new Fraction(0, 1)},
                {new Fraction(2, 1), new Fraction(4, 1), new Fraction(6, 1), new Fraction(5, 1)},
                {new Fraction(3, 1), new Fraction(2, 1), new Fraction(5, 1), new Fraction(2, 1)}
            };
            
//            Fraction[,] matrix =
//            {
//                {new Fraction(2, 1), new Fraction(4, 1)},
//                {new Fraction(3, 1), new Fraction(5, 1)},
//                {new Fraction(6, 1), new Fraction(7, 1)}
//            };
            
            Console.WriteLine("i = {0} j = {1}", matrix.GetLength(0), matrix.GetLength(1));
            
            matrix = s.TriangleMatrixCalculate(matrix);
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write("{0} ", matrix[i,j]);
                }
                Console.Write("\n");
            }
            
            Console.WriteLine("-------------------------------------------------------------------------------------");
            Console.WriteLine("Решение СЛАУ по Гауссу");
            Fraction[] result = s.GaussCalc(matrix);

            for (int i = 0; i < result.GetLength(0); i++)
            {
                Console.Write("x{0} = {1} ", i + 1, result[i]);
            }


        }
    }
}