using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learningcsh
{
    public static class _9
    {
        public static void Test1()
        {
            Stack<int> intStack = new Stack<int>();
            intStack.Push(10);
            intStack.Push(20);
            intStack.Push(5);
            Console.WriteLine($"Максимальный элемент стека: {intStack.Max()}");
        }

        public static void Test2()
        {
            Pair<string, string> pair = new Pair<string, string>("Первый", "Второй");
            Console.WriteLine($"До Swap: First = {pair.First}, Second = {pair.Second}");
            pair.Swap();
            Console.WriteLine($"После Swap: First = {pair.First}, Second = {pair.Second}");
        }

        public static void Test3()
        {
            Calculator<int> intCalculator = new Calculator<int>();
            int sum = Calculator<int>.Add(5, 10);
            Console.WriteLine($"Сумма: {sum}"); 
            int zero = intCalculator.Zero();
            Console.WriteLine($"Ноль: {zero}"); 
        }

        #region 1
        public class Stack<T> where T : IComparable<T>
        {
            private List<T> _elements = new List<T>();

            public void Push(T item)
            {
                _elements.Add(item);
            }

            public T Pop()
            {
                if (_elements.Count == 0)
                    throw new InvalidOperationException("Стек пуст.");

                T item = _elements[_elements.Count - 1];
                _elements.RemoveAt(_elements.Count - 1);
                return item;
            }

            public T Peek()
            {
                if (_elements.Count == 0)
                    throw new InvalidOperationException("Стек пуст.");

                return _elements[_elements.Count - 1];
            }

            public int Count()
            {
                return _elements.Count;
            }

            public T Max()
            {
                if (_elements.Count == 0)
                    throw new InvalidOperationException("Стек пуст.");

                T max = _elements[0];

                foreach (T item in _elements)
                {
                    if (item.CompareTo(max) > 0)
                    {
                        max = item;
                    }
                }

                return max;
            }
        }

        #endregion

        #region 2
        public class Pair<T, U> where T : class where U : class
        {
            public T First { get; set; }
            public U Second { get; set; }

            public Pair(T first, U second)
            {
                First = first;
                Second = second;
            }

            public void Swap()
            {
                T temp = First;
                First = (T)(object)Second;
                Second = (U)(object)temp;
            }
        }
        #endregion

        #region 3
        public class Calculator<T> where T : new()
        {
            public static T Add(T x, T y) 
            {
                dynamic a = x;
                dynamic b = y;
                return a + b;
            }

            public T Zero()
            {
                return new T();
            }
        }
        #endregion
    }
}
