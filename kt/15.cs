using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learningcsh.kt
{
    internal class _15
    {
        #region 1
        public class Fibonacci : IEnumerable<int>
        {
            public IEnumerator<int> GetEnumerator()
            {
                int previous = 0, current = 1;

                yield return previous; 
                yield return current;  

                while (true)
                {
                    int next = previous + current;
                    yield return next;
                    previous = current;
                    current = next;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        public static void TestFibonacci()
        {
            Console.WriteLine("== Первые 10 чисел Фибоначчи ==");
            var fibonacci = new Fibonacci();

            int count = 0;
            foreach (int number in fibonacci)
            {
                Console.WriteLine(number);
                count++;
                if (count == 10) break; 
            }
        }
        #endregion

        #region 2
        public class Matrix
        {
            private double[,] matrix;

            public Matrix(double[,] matrix)
            {
                this.matrix = matrix;
            }

            public IEnumerable<double> GetRow(int index)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    yield return matrix[index, col];
                }
            }

            public IEnumerable<double> GetColumn(int index)
            {
                for (int row = 0; row < matrix.GetLength(0); row++)
                {
                    yield return matrix[row, index];
                }
            }
        }

        public static void TestMatrix()
        {
            Console.WriteLine("\n== Матрица по строкам и столбцам ==");

            double[,] data = {
                { 1.1, 2.2, 3.3 },
                { 4.4, 5.5, 6.6 },
                { 7.7, 8.8, 9.9 }
            };

            var matrix = new Matrix(data);

            for (int i = 0; i < data.GetLength(0); i++)
            {
                Console.Write($"Строка {i + 1}: ");
                foreach (var element in matrix.GetRow(i))
                {
                    Console.Write(element + " ");
                }
                Console.WriteLine();
            }

            for (int i = 0; i < data.GetLength(1); i++)
            {
                Console.Write($"Столбец {i + 1}: ");
                foreach (var element in matrix.GetColumn(i))
                {
                    Console.Write(element + " ");
                }
                Console.WriteLine();
            }
        }
        #endregion

        #region 3
        public class PrimeNumbers : IEnumerator<int>
        {
            private int current;

            public PrimeNumbers()
            {
                current = 1; 
            }

            public int Current => current;

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                current++;
                while (!IsPrime(current))
                {
                    current++;
                }
                return true;
            }

            public void Reset()
            {
                current = 1;
            }

            public void Dispose()
            {
                // Нет ресурсов для освобождения
            }

            private bool IsPrime(int number)
            {
                if (number < 2) return false;
                for (int i = 2; i * i <= number; i++)
                {
                    if (number % i == 0) return false;
                }
                return true;
            }
        }

        public static void TestPrimeNumbers()
        {
            Console.WriteLine("\n== Первые 10 простых чисел ==");

            using (var primeNumbers = new PrimeNumbers())
            {
                int count = 0;
                while (primeNumbers.MoveNext() && count < 10)
                {
                    Console.WriteLine(primeNumbers.Current);
                    count++;
                }
            }
        }
        #endregion
    }
}
