/*
 * Разработчик: Толокольникова А.Ю.
 * Модуль:      СЛАУ (метод Гаусса)
 * Дата:        20.04.2018
 * ВУЗ:         МГТУ СТАНКИН
 */

using System;
using slau_calc.Exceptions;

namespace slau_calc
{
    public class Slau : Object
    {

        private double _accuracy; 
            
        public Slau()
        {
        }

        private Fraction[,] _matrix;

        // Методы для вычисления по Гауссу 
        // ******************************************************************************************************
        public Fraction[] GaussCalc(Fraction[,] matrix)
        {
            this._matrix = matrix;
            TriangleMatrixCalculate(matrix); // Приводим к ступенчатому виду

            // Получаем решение системы путом подстановик снизу вверх
            Fraction[] result = new Fraction[this._matrix.GetLength(1) - 1];
            // Инициализируем нулями
            for (int i = 0; i < result.GetLength(0); i++)
            {
                result[i] = new Fraction(0, 1);
            }

            int k = this._matrix.GetLength(1) - 1;
            int xNumber = this._matrix.GetLength(1) - 2; // Номер неизвестного икса (x)
            for (int i = this._matrix.GetLength(0) - 1; i >= 0; i--)
            {
                Fraction rVal = new Fraction(0, 1);
                Fraction lVal = new Fraction(0, 1);
                Fraction sum = new Fraction(0, 1);

                lVal = this._matrix[i, xNumber];

                rVal = this._matrix[i, this._matrix.GetLength(1) - 1];

                for (int j = xNumber + 1; j < this._matrix.GetLength(1) - 1; j++)
                {
                    rVal = rVal + new Fraction(-1, 1) * this._matrix[i, j] * result[j];
                }

                result[xNumber--] = rVal / lVal;
//                --xNumber;
            }

            return result;
        }

        public Fraction[,] TriangleMatrixCalculate(Fraction[,] matrix)
        {
            this._matrix = matrix;
            return TriangleMatrixCalculate();
        }

        public Fraction[,] TriangleMatrixCalculate()
        {
//            Fraction[,] matrix = this._matrix;
            Fraction[,] tmp_matrix = this._matrix;
            for (int i = 0; i < this._matrix.GetLength(0); i++)
            {
                bool last = i == this._matrix.GetLength(0) - 1;
                if (i == 0)
                    tmp_matrix = TriangleMatrixCalculateNextIter(tmp_matrix, last); // Получаем 
                // преобразование для части матрицы
                else
                    // Получаем преобразование для части матрицы
                    tmp_matrix = TriangleMatrixCalculateNextIter(_decrimentTrimMatrix(tmp_matrix), last);
                // Можно было бы сделать и без замен. Навернякак быстрее бы работало
                _replacePartOfMatrix(tmp_matrix, i); // Вовзращаем преобразованное значение на место


//                Console.WriteLine("-------------------------------------------------------------------------------------");
//                for (int k = 0; k < tmp_matrix.GetLength(0); k++)
//                {
//                    for (int o = 0; o < tmp_matrix.GetLength(1); o++)
//                    {
//                        Console.Write("{0} ", this._matrix[k,o]);
//                    }
//                    Console.Write("\n");
//                }
            }

//            Console.WriteLine("-------------------------------------------------------------------------------------");

            return this._matrix;
        }

        private void _replacePartOfMatrix(Fraction[,] matrix, int boundary)
        {
            for (int i = boundary; i < this._matrix.GetLength(0); i++)
            {
                for (int j = boundary; j < this._matrix.GetLength(1); j++)
                {
                    this._matrix[i, j] = matrix[i - boundary, j - boundary];
                }
            }
        }

        private Fraction[,] _decrimentTrimMatrix(Fraction[,] matrix)
        {
            Fraction[,] result = new Fraction[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1];

            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                for (int j = 1; j < matrix.GetLength(1); j++)
                {
                    result[i - 1, j - 1] = matrix[i, j];
                }
            }

            return result;
        }

        private Fraction[,] TriangleMatrixCalculateNextIter(Fraction[,] matrix, bool last)
        {
            // Шаг 1.
            // В первом столбце выбрать элемент, отличный от нуля (ведущий элемент). Строку с ведущим элементом
            // (ведущая строка), если она не первая, переставить на место первой строки (преобразование I типа). Если в первом
            // столбце нет ведущего (все элементы равны нулю), то исключаем этот столбец, и продолжаем поиск ведущего элемента
            // в оставшейся части матрицы. Преобразования заканчиваются, если исключены все столбцы или в оставшейся части
            // матрицы все элементы нулевые.
            int lines = matrix.GetLength(0); // Количество строк
            int columns = matrix.GetLength(1); // Количество столбцов
            Fraction LeadingElement;

            Coordinates CurrentLeadingElement = _getLeadingElement(matrix); // Получаем ведущий элемент
            if (CurrentLeadingElement != new Coordinates(0, 0)
            ) // Если ведущий элемент не в первой строке - перемещаем его на первую строку
            {
                // Ну собственно перемещаем
                for (int j = 0; j < columns; j++)
                {
                    Fraction tmp = matrix[CurrentLeadingElement.X, j];
                    matrix[CurrentLeadingElement.X, j] = matrix[0, j];
                    matrix[0, j] = tmp;
                }
            }

            // Шаг 2
            // Разделить все элементы ведущей строки на ведущий элемент (преобразование II типа). Если ведущая
            // строка последняя, то на этом преобразования следует закончить.
            LeadingElement = matrix[0, 0];
            for (int j = 0; j < columns; j++)
            {
                matrix[0, j] = matrix[0, j] / LeadingElement;
            }

            if (last)
                return matrix;

            // Шаг 3
            // К каждой строке, расположенной ниже ведущей, прибавить ведущую строку, умноженную соответственно на
            // такое число, чтобы элементы, стоящие под ведущим оказались равными нулю (преобразование III типа).

            for (int i = 1; i < lines; i++)
            {
                // Решаем уравнение. Нужно, чтобы matrix[i, 0] + matrix[0, 0] * x = 0

                Fraction
                    factor = new Fraction(-1, 1) *
                             (matrix[i, 0] /
                              matrix[0, 0]); // Пока не реализована операция "-", будем умножать на -1. Что равнозначно
                for (int j = 0; j < columns; j++)
                {
//                    Fraction addendum = LeadingElement * factor;
                    matrix[i, j] = matrix[i, j] + (matrix[0, j] * factor);
                }
            }

            return matrix;
        }


        private Coordinates _getLeadingElement(Fraction[,] matrix)
        {
            // Возвращаем координаты элемента отличного от нуля (ведущий элемент)
            Coordinates LeadingElement = new Coordinates(0, 0);
            for (int i = 0; i < matrix.GetLength(0); i++) // Перебираем строки и ищем ведущий элемент
            {
                if (matrix[i, 0] != 0) // Пока смотрим только на первые столбцы
                {
                    LeadingElement.X = i;
                    LeadingElement.Y = 0;
                    return LeadingElement;
                }
            }

            throw new NotFoundLeadingElement();
        }

        // Методы для вычисления по Iter
        // ******************************************************************************************************

public Fraction[] ZeidelCalc(Fraction[,] matrix, double accuracy)
{
    Fraction[] currentIterX = new Fraction[matrix.GetLength(1) - 1];
    Fraction[] previosIterX = new Fraction[matrix.GetLength(1) - 1];
    this._accuracy = accuracy;
    // Шаг 1. Трансформация матрицы к диагонально приобладающему вилду
    matrix = this._transformMatrix(matrix);

    // Шаг 2. Делим все элементы строки на на элемент главной диагонали текущей строки
    for (int i = 0; i < matrix.GetLength(0); i++)
    {
        Fraction tmp = matrix[i, i];
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            matrix[i, j] = matrix[i, j] / tmp;
        }
    }


    // Шаг 3. Начальная итерация для всех X равна значению свободного вектора для данной строки
    for (int i = 0; i < currentIterX.Length; i++)
    {
        currentIterX[i] = matrix[i, matrix.GetLength(1) - 1];
    }

    // Шаг 4. Выносим каждый диагональный икс (x) и выполняем итерации до тех пор, пока не получим нужной точности
    do
    {
        for (int i = 0; i < previosIterX.Length; i++)
        {
            previosIterX[i] = currentIterX[i];
        }
        
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            Fraction tmp = new Fraction(0, 1);
            for (int j = 0; j < matrix.GetLength(1) - 1; j++) // Иcключаем эл-т свободного вектора.
            {                                                 // Его знак менять не надо, тк он не переносится
                if (i != j)
                    tmp = tmp + (new Fraction(-1, 1) * matrix[i, j] * currentIterX[j]);
            }

            currentIterX[i] = tmp + matrix[i, matrix.GetLength(1) - 1];
        }
    } while (!this._accuracyCheck(previosIterX, currentIterX));
    return currentIterX;
}

    private bool _accuracyCheck(Fraction[] oldVal, Fraction[] newVal)
    {
        Fraction val = new Fraction(0, 1);
        for (int i = 0; i < oldVal.Length; i++)
        {
            val = val + (newVal[i] - oldVal[i]) * (newVal[i] - oldVal[i]);
        }

        if (Math.Sqrt(val.ToDouble()) < this._accuracy)
            return true;
        else
            return false;
    }

        private Fraction[,] _transformMatrix(Fraction[,] matrix)
        {
            // TODO: Не учитывается, что максимальные элементы могут повторяться

            Fraction[] line = new Fraction[matrix.GetLength(1) - 1];
            int[] maxIdx = new int[matrix.GetLength(0)];

            // Ищем максимальные значения
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                int max = 0;
                for (int j = 1; j < matrix.GetLength(1) - 1; j++)
                {
                    if (Math.Abs(matrix[i, max].ToDouble()) < Math.Abs(matrix[i, j].ToDouble()))
                        max = j;
                }

                maxIdx[i] = max;
            }

            // Если не на данный момент не получился диагонально преобладающий вид, то
            // пробуем ее трансформировать
            for (int i = 0; i < maxIdx.Length; i++)
            {
                if (maxIdx[i] != i)
                {
                    for (int j = 0; j < maxIdx.Length; j++)
                    {
                        if (j == i)
                            continue;
                        else if (maxIdx[j] == i)
                        {
                            // Меняем эти строки и меняем maxIdx
                            matrix = _swapLines(matrix, i, j);
                            int tmp = maxIdx[i];
                            maxIdx[i] = maxIdx[j];
                            maxIdx[j] = tmp;
                        }
                    }
                }
            }

            // Проверяем, является ли матрица диагонально преобладающей
            for (int i = 0; i < maxIdx.GetLength(0); i++)
            {
                if (maxIdx[i] != i)
                    throw new CastPrevailingViewError();
            }

            return matrix;
        }


        public Fraction[] IterCalc(Fraction[,] matrix, double accuracy)
        {
            Fraction[] currentIterX = new Fraction[matrix.GetLength(1) - 1];
            Fraction[] previosIterX = new Fraction[matrix.GetLength(1) - 1];
            this._accuracy = accuracy;
            // Шаг 1. Трансформация матрицы к диагонально приобладающему вилду
            matrix = this._transformMatrix(matrix);

            // Шаг 2. Делим все элементы строки на на элемент главной диагонали текущей строки
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Fraction tmp = matrix[i, i];
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = matrix[i, j] / tmp;
                }
            }


            // Шаг 3. Начальная итерация для всех X равна значению свободного вектора для данной строки
            for (int i = 0; i < currentIterX.Length; i++)
            {
                currentIterX[i] = matrix[i, matrix.GetLength(1) - 1];
            }

            // Шаг 4. Выносим каждый диагональный икс (x) и выполняем итерации до тех пор, пока не получим нужной точности
            do
            {
                for (int i = 0; i < previosIterX.Length; i++)
                {
                    previosIterX[i] = currentIterX[i];
                }
                
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    Fraction tmp = new Fraction(0, 1);
                    for (int j = 0; j < matrix.GetLength(1) - 1; j++) // Иcключаем эл-т свободного вектора.
                    {                                                 // Его знак менять не надо, тк он не переносится
                        if (i != j)
                            tmp = tmp + (new Fraction(-1, 1) * matrix[i, j] * previosIterX[j]); // Вместо текущих рассчетов, в отличе от Зейделя используем текущие значения
                    }

                    currentIterX[i] = tmp + matrix[i, matrix.GetLength(1) - 1];
                }
            } while (!this._accuracyCheck(previosIterX, currentIterX));
            return currentIterX;
        }
        
        private Fraction[,] _swapLines(Fraction[,] matrix, int idx1, int idx2)
        {
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                Fraction tmp = matrix[idx1, i];
                matrix[idx1, i] = matrix[idx2, i];
                matrix[idx2, i] = tmp;
            }

            return matrix;
        }
    }
}