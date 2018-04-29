using System;

namespace slau_calc
{
    internal class MainC
    {
        public static void Main(string[] args)
        {
            Slau s = new Slau(); 
            Fraction[,] matrix =
            { // Создаем матрицу рациональных чисел
                {new Fraction(7, 1), new Fraction(6, 1), new Fraction(6, 1), new Fraction(0, 1)},
                {new Fraction(3, 1), new Fraction(9, 1), new Fraction(4, 1), new Fraction(1, 1)},
                {new Fraction(8, 1), new Fraction(1, 1), new Fraction(9, 1), new Fraction(7, 1)}
            };
            
            Console.WriteLine("-------------------------------------------------------------------------------------");
            Console.WriteLine("Решение СЛАУ по Гауссу");
            Fraction[] result = s.GaussCalc(matrix);

            for (int i = 0; i < result.GetLength(0); i++)
            {
                Console.Write("x{0} = {1} ", i + 1, result[i]);
            }
            Console.WriteLine();
            
            Console.WriteLine("-------------------------------------------------------------------------------------");
            Console.WriteLine("Решение СЛАУ по Зейделю");
            
            
            Fraction[,] zeidelMatrix = {
                {new Fraction(3, 1), new Fraction(2, 1), new Fraction(9, 1), new Fraction(2, 1)},
                {new Fraction(-8, 1), new Fraction(1, 1), new Fraction(4, 1), new Fraction(1, 1)},
                {new Fraction(4, 1), new Fraction(-6, 1), new Fraction(1, 1), new Fraction(0, 1)}
            };
            
            result = s.ZeidelCalc(zeidelMatrix, 0.001);

            for (int i = 0; i < result.GetLength(0); i++)
            {
                Console.Write("x{0} = {1} ", i + 1, result[i]);
            }
            
            Console.WriteLine();
            
            Console.WriteLine("-------------------------------------------------------------------------------------");
            Console.WriteLine("Решение СЛАУ по методу итераций");
            
            Fraction[,] iterMatrix = {
                {new Fraction(3, 1), new Fraction(2, 1), new Fraction(9, 1), new Fraction(2, 1)},
                {new Fraction(-8, 1), new Fraction(1, 1), new Fraction(4, 1), new Fraction(1, 1)},
                {new Fraction(4, 1), new Fraction(-6, 1), new Fraction(1, 1), new Fraction(0, 1)}
            };
            
            result = s.ZeidelCalc(iterMatrix, 0.001);

            for (int i = 0; i < result.GetLength(0); i++)
            {
                Console.Write("x{0} = {1} ", i + 1, result[i]);
            }
            


        }
    }
}