using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learningcsh.kt
{
    internal class _11
    {
        #region 1

        public static void Swap<T>(ref T x, ref T y) where T : struct
        {
            T temp = x;
            x = y;
            y = temp;
        }

        public static void TestSwap()
        {
            int a = 5, b = 10;
            double x = 1.5, y = 2.5;
            bool p = true, q = false;

            Console.WriteLine("== Тест Swap<T> ==\n");

            Console.WriteLine($"Before swap: a = {a}, b = {b}");
            Swap(ref a, ref b);
            Console.WriteLine($"After swap: a = {a}, b = {b}\n");

            Console.WriteLine($"Before swap: x = {x}, y = {y}");
            Swap(ref x, ref y);
            Console.WriteLine($"After swap: x = {x}, y = {y}\n");

            Console.WriteLine($"Before swap: p = {p}, q = {q}");
            Swap(ref p, ref q);
            Console.WriteLine($"After swap: p = {p}, q = {q}\n");
        }

        #endregion

        #region 2

        public class LinkedList<T> where T : class
        {
            private class Node
            {
                public T Data { get; set; }
                public Node Next { get; set; }

                public Node(T data)
                {
                    Data = data;
                    Next = null;
                }
            }

            private Node head;

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
            }

            public bool Remove(T item)
            {
                if (head == null)
                {
                    return false;
                }

                if (head.Data.Equals(item))
                {
                    head = head.Next;
                    return true;
                }

                Node current = head;
                while (current.Next != null)
                {
                    if (current.Next.Data.Equals(item))
                    {
                        current.Next = current.Next.Next;
                        return true;
                    }
                    current = current.Next;
                }

                return false;
            }

            public bool Contains(T item)
            {
                Node current = head;
                while (current != null)
                {
                    if (current.Data.Equals(item))
                    {
                        return true;
                    }
                    current = current.Next;
                }
                return false;
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
        }

        public class Book
        {
            public string Title { get; set; }

            public Book(string title)
            {
                Title = title;
            }
        }

        public static void TestLinkedList()
        {
            Console.WriteLine("== Тест LinkedList<T> ==\n");

            LinkedList<string> stringList = new LinkedList<string>();
            stringList.Add("Hello");
            stringList.Add("World");
            Console.WriteLine($"Contains 'World': {stringList.Contains("World")}");
            stringList.Remove("World");
            Console.WriteLine($"Contains 'World' after removal: {stringList.Contains("World")}\n");

            LinkedList<Person> peopleList = new LinkedList<Person>();
            Person person1 = new Person("John", 30);
            Person person2 = new Person("Jane", 25);
            peopleList.Add(person1);
            peopleList.Add(person2);
            Console.WriteLine($"Contains John: {peopleList.Contains(person1)}");
            peopleList.Remove(person1);
            Console.WriteLine($"Contains John after removal: {peopleList.Contains(person1)}\n");

            LinkedList<Book> bookList = new LinkedList<Book>();
            Book book1 = new Book("C# Programming");
            bookList.Add(book1);
            Console.WriteLine($"Contains 'C# Programming': {bookList.Contains(book1)}\n");
        }

        #endregion

        #region 3

        public interface IPrintable<T> 
        {
            void Print();
        }

        public class Student : IPrintable<Student>
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public double Grade { get; set; }

            public Student(string name, int age, double grade)
            {
                Name = name;
                Age = age;
                Grade = grade;
            }

            public void Print()
            {
                Console.WriteLine($"Student: Name = {Name}, Age = {Age}, Grade = {Grade}");
            }
        }

        public struct Vector : IPrintable<Vector>
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }

            public Vector(double x, double y, double z)
            {
                X = x;
                Y = y;
                Z = z;
            }

            public void Print()
            {
                Console.WriteLine($"Vector: X = {X}, Y = {Y}, Z = {Z}");
            }
        }

        public static void TestIPrintable()
        {
            Console.WriteLine("== Тест IPrintable<T> ==\n");

            Student student = new Student("Alice", 20, 4.0);
            student.Print();

            Vector vector = new Vector(1.0, 2.0, 3.0);
            vector.Print();
        }

        #endregion
    }
}
