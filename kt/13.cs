using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learningcsh.kt
{
    internal class _13
    {
        #region 1

        public static class MaxUtility
        {
            public static T Max<T>(T x, T y) where T : IComparable<T>
            {
                return x.CompareTo(y) > 0 ? x : y;
            }
        }


        public static void TestMax()
        {
            Console.WriteLine("== Тест Max<T> ==\n");

            int a = 5;
            int b = 10;
            Console.WriteLine($"Max({a}, {b}) = {MaxUtility.Max(a, b)}");

            string str1 = "apple";
            string str2 = "banana";
            Console.WriteLine($"Max(\"{str1}\", \"{str2}\") = {MaxUtility.Max(str1, str2)}");

            DateTime dt1 = new DateTime(2023, 1, 1);
            DateTime dt2 = new DateTime(2024, 1, 1);
            Console.WriteLine($"Max({dt1.ToShortDateString()}, {dt2.ToShortDateString()}) = {MaxUtility.Max(dt1, dt2)}");
        }
        

        #endregion

        #region 2

        public static class SwapUtility
        {
            public static void Swap<T>(ref T x, ref T y) where T : class
            {
                T temp = x;
                x = y;
                y = temp;
            }

            public static void SwapStruct<T>(ref T x, ref T y) where T : struct
            {
                T temp = x;
                x = y;
                y = temp;
            }
        }

        public class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }

            public Person(string name, int age)
            {
                Name = name;
                Age = age;
            }

            public override string ToString()
            {
                return $"{Name}, {Age}";
            }
        }


        public static void TestSwap()
        {
            Console.WriteLine("\n== Тест Swap<T> ==\n");

            int a = 10;
            int b = 20;
            Console.WriteLine($"До SwapStruct: a = {a}, b = {b}");
            SwapUtility.SwapStruct(ref a, ref b);
            Console.WriteLine($"После SwapStruct: a = {a}, b = {b}");

            string str1 = "Hello";
            string str2 = "World";
            Console.WriteLine($"\nДо Swap: str1 = {str1}, str2 = {str2}");
            SwapUtility.Swap(ref str1, ref str2);
            Console.WriteLine($"После Swap: str1 = {str1}, str2 = {str2}");

            Person p1 = new Person("John", 30);
            Person p2 = new Person("Jane", 25);
            Console.WriteLine($"\nДо Swap: p1 = {p1}, p2 = {p2}");
            SwapUtility.Swap(ref p1, ref p2);
            Console.WriteLine($"После Swap: p1 = {p1}, p2 = {p2}");
        }
        

        #endregion

        #region 3

        public static class PrintUtility
        {
            public static void Print<T>(T[] array) where T : class
            {
                for (int i = 0; i < array.Length; i++)
                {
                    Console.Write(array[i]);
                    if (i < array.Length - 1) Console.Write(", ");
                }
                Console.WriteLine();
            }
        }

        public class Book
        {
            public string Title { get; set; }
            public double Price { get; set; }

            public Book(string title, double price)
            {
                Title = title;
                Price = price;
            }

            public override string ToString()
            {
                return $"{Title} - {Price:C}";
            }
        }


        public static void TestPrint()
        {
            Console.WriteLine("\n== Тест Print<T> ==\n");

            int[] intArray = { 1, 2, 3, 4, 5 };
            Console.Write("Массив int[]: ");
            PrintUtility.Print(intArray.Cast<object>().ToArray());  


            string[] stringArray = { "apple", "banana", "kiwi" };
            Console.Write("Массив string[]: ");
            PrintUtility.Print(stringArray);

            Book[] bookArray = {
                new Book("Book A", 9.99),
                new Book("Book B", 12.49),
                new Book("Book C", 7.99)
            };
            Console.Write("Массив Book[]: ");
            PrintUtility.Print(bookArray);
        }
        
        #endregion

    }
}
