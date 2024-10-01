using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learningcsh.kt
{
    internal class _12
    {
        #region 1
        public interface IList<T>
        {
            void Add(T item);
            void Remove(T item);
            T Get(int index);
            void Set(int index, T item);
            int Count { get; }
        }

        public class ArrayList<T> : IList<T>
        {
            private T[] items;
            private int count;

            public ArrayList()
            {
                items = new T[10];
                count = 0;
            }

            public void Add(T item)
            {
                if (count == items.Length)
                {
                    Array.Resize(ref items, items.Length * 2);
                }
                items[count++] = item;
            }

            public void Remove(T item)
            {
                int index = Array.IndexOf(items, item, 0, count);
                if (index >= 0)
                {
                    for (int i = index; i < count - 1; i++)
                    {
                        items[i] = items[i + 1];
                    }
                    count--;
                }
            }

            public T Get(int index)
            {
                if (index >= 0 && index < count)
                {
                    return items[index];
                }
                throw new ArgumentOutOfRangeException();
            }

            public void Set(int index, T item)
            {
                if (index >= 0 && index < count)
                {
                    items[index] = item;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }

            public int Count => count;
        }

        public class LinkedList<T> : IList<T>
        {
            private class Node
            {
                public T Data;
                public Node Next;
                public Node(T data)
                {
                    Data = data;
                    Next = null;
                }
            }

            private Node head;
            private int count;

            public void Add(T item)
            {
                if (head == null)
                {
                    head = new Node(item);
                }
                else
                {
                    Node current = head;
                    while (current.Next != null)
                    {
                        current = current.Next;
                    }
                    current.Next = new Node(item);
                }
                count++;
            }

            public void Remove(T item)
            {
                if (head == null) return;

                if (head.Data.Equals(item))
                {
                    head = head.Next;
                    count--;
                    return;
                }

                Node current = head;
                while (current.Next != null)
                {
                    if (current.Next.Data.Equals(item))
                    {
                        current.Next = current.Next.Next;
                        count--;
                        return;
                    }
                    current = current.Next;
                }
            }

            public T Get(int index)
            {
                if (index >= 0 && index < count)
                {
                    Node current = head;
                    for (int i = 0; i < index; i++)
                    {
                        current = current.Next;
                    }
                    return current.Data;
                }
                throw new ArgumentOutOfRangeException();
            }

            public void Set(int index, T item)
            {
                if (index >= 0 && index < count)
                {
                    Node current = head;
                    for (int i = 0; i < index; i++)
                    {
                        current = current.Next;
                    }
                    current.Data = item;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }

            public int Count => count;
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


        public static void TestList()
        {
            Console.WriteLine("== Тест ArrayList<T> ==\n");
            IList<int> intArrayList = new ArrayList<int>();
            intArrayList.Add(1);
            intArrayList.Add(2);
            intArrayList.Add(3);
            Console.WriteLine($"Element at index 1: {intArrayList.Get(1)}");

            IList<string> stringArrayList = new ArrayList<string>();
            stringArrayList.Add("Hello");
            stringArrayList.Add("World");
            Console.WriteLine($"Element at index 1: {stringArrayList.Get(1)}");

            Console.WriteLine("\n== Тест LinkedList<T> ==\n");
            IList<Person> personLinkedList = new LinkedList<Person>();
            personLinkedList.Add(new Person("John", 30));
            personLinkedList.Add(new Person("Jane", 25));
            Console.WriteLine($"Person at index 0: {personLinkedList.Get(0)}");
        }
        

        #endregion

        #region 2

        public interface IComparer<T>
        {
            int Compare(T x, T y);
        }

        public class StringComparer : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                return x.Length.CompareTo(y.Length);
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
                return $"{Title}, {Price:C}";
            }
        }

        public class BookComparer : IComparer<Book>
        {
            public int Compare(Book x, Book y)
            {
                return x.Price.CompareTo(y.Price);
            }
        }

        public static void Sort<T>(T[] array, IComparer<T> comparer)
        {
            Array.Sort(array, comparer.Compare);
        }


        public static void TestSort()
        {
            Console.WriteLine("== Сортировка строк ==\n");
            string[] strings = { "apple", "banana", "kiwi", "orange" };
            Sort(strings, new StringComparer());
            foreach (var str in strings)
            {
                Console.WriteLine(str);
            }

            Console.WriteLine("\n== Сортировка книг ==\n");
            Book[] books = {
                new Book("Book A", 10.99),
                new Book("Book B", 5.99),
                new Book("Book C", 15.49)
            };
            Sort(books, new BookComparer());
            foreach (var book in books)
            {
                Console.WriteLine(book);
            }
            }
        

        #endregion

        #region 3

        public interface IFactory<T>
        {
            T Create();
        }

        public class RandomNumberFactory : IFactory<int>
        {
            private Random random = new Random();

            public int Create()
            {
                return random.Next(1, 101);
            }
        }

        public class PersonFactory : IFactory<Person>
        {
            public Person Create()
            {
                Console.Write("Введите имя: ");
                string name = Console.ReadLine();
                Console.Write("Введите возраст: ");
                int age = int.Parse(Console.ReadLine());
                return new Person(name, age);
            }
        }

        public static T[] CreateArray<T>(IFactory<T> factory, int n)
        {
            T[] array = new T[n];
            for (int i = 0; i < n; i++)
            {
                array[i] = factory.Create();
            }
            return array;
        }

        public static void TestFactory()
        {
            Console.WriteLine("== Создание массива случайных чисел ==\n");
            var numberFactory = new RandomNumberFactory();
            int[] randomNumbers = CreateArray(numberFactory, 5);
            foreach (var number in randomNumbers)
            {
                Console.WriteLine(number);
            }

            Console.WriteLine("\n== Создание массива объектов Person ==\n");
            var personFactory = new PersonFactory();
            Person[] people = CreateArray(personFactory, 2);
            foreach (var person in people)
            {
                Console.WriteLine(person);
            }
        }
        

        #endregion
    }
}
