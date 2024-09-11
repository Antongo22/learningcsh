using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faker.Resources;
using Faker.Extensions;
using System.Data.Entity.Infrastructure;

namespace learningcsh
{

    #region 1
    interface IAnimal
    {
        string Name { get; }

        void MakeSound();
    }

    public class Dog : IAnimal
    {
        public string Name { get; set; }

        public Dog(string name)
        {
            Name = name;
        }

        public void MakeSound()
        {
            Console.WriteLine("Гав-гав!");
        }
    }
    public class Cat : IAnimal
    {
        public string Name { get; set; }

        public Cat(string name)
        {
            Name = name;
        }

        public void MakeSound()
        {
            Console.WriteLine("Мяу!");
        }
    }

    #endregion

    #region 2
    public interface IShape
    {
        double Area { get; }
        double Perimeter { get; }
    }

    public class Circle : IShape
    {
        public double Radius { get; set; }

        public Circle(double radius)
        {
            Radius = radius;
        }

        public double Area => Math.PI * Radius * Radius;

        public double Perimeter => 2 * Math.PI * Radius;
    }

    public class Rectangle : IShape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public double Area => Width * Height;

        public double Perimeter => 2 * (Width + Height);
    }

    public class Triangle : IShape
    {
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }

        public Triangle(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
        }

        public double Area
        {
            get
            {
                double s = (A + B + C) / 2;
                return Math.Sqrt(s * (s - A) * (s - B) * (s - C));
            }
        }

        public double Perimeter => A + B + C;
    }
    #endregion

    #region 3
    public interface IComparable<T>
    {
        int CompareTo(T other);
    }


    public enum StudentComparisonField 
    {
        Name,
        Age,
        Grade
    }

    public enum BookComparisonField
    {
        Title,
        Author,
        Price
    }

    public class Student : IComparable<Student>
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

        public int CompareTo(Student other)
        {
            return CompareTo(other, StudentComparisonField.Age); 
        }

        public int CompareTo(Student other, StudentComparisonField field)
        {
            switch (field)
            {
                case StudentComparisonField.Name:
                    return string.Compare(Name, other.Name, StringComparison.OrdinalIgnoreCase);
                case StudentComparisonField.Age:
                    return Age.CompareTo(other.Age);
                case StudentComparisonField.Grade:
                    return Grade.CompareTo(other.Grade);
                default:
                    throw new ArgumentException("Неправильное поле сравнения");
            }
        }

        public override string ToString()
        {
            return $"{Name}, {Age} лет, оценка: {Grade}";
        }
    }

    public class Book : IComparable<Book>
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }

        public Book(string title, string author, double price)
        {
            Title = title;
            Author = author;
            Price = price;
        }

        public int CompareTo(Book other)
        {
            return CompareTo(other, BookComparisonField.Price); 
        }

        public int CompareTo(Book other, BookComparisonField field)
        {
            switch (field)
            {
                case BookComparisonField.Title:
                    return string.Compare(Title, other.Title, StringComparison.OrdinalIgnoreCase);
                case BookComparisonField.Author:
                    return string.Compare(Author, other.Author, StringComparison.OrdinalIgnoreCase);
                case BookComparisonField.Price:
                    return Price.CompareTo(other.Price);
                default:
                    throw new ArgumentException("Неправильное поле сравнения");
            }
        }

        public override string ToString()
        {
            return $"{Title} от {Author}, цена: {Price}";
        }
    }

    #endregion

    internal class Program
    {
        static void Test1()
        {
            IAnimal dog = new Dog("Шарик");
            IAnimal cat = new Cat("Мурка");

            Console.WriteLine($"{dog.Name} говорит: ");
            dog.MakeSound();

            Console.WriteLine($"{cat.Name} говорит: ");
            cat.MakeSound();
        }

        static void Test2()
        {
            IShape circle = new Circle(5);
            IShape rectangle = new Rectangle(4, 7);
            IShape triangle = new Triangle(3, 4, 5);

            Console.WriteLine($"Круг - Площадь: {circle.Area}, Периметр: {circle.Perimeter}");
            Console.WriteLine($"Прямоугольник - Площадь: {rectangle.Area}, Периметр: {rectangle.Perimeter}");
            Console.WriteLine($"Треугольник - Площадь: {triangle.Area}, Периметр: {triangle.Perimeter}");

        }


        static void Test3()
        {
            Student student1 = new Student("Антон", 20, 5.0);
            Student student2 = new Student("Гоша", 22, 3.9);

            Console.WriteLine("Сравнение студентов по имени:");
            Console.WriteLine(student1.CompareTo(student2, StudentComparisonField.Name));

            Console.WriteLine("Сравнение студентов по возрасту:");
            Console.WriteLine(student1.CompareTo(student2, StudentComparisonField.Age));

            Console.WriteLine("Сравнение студентов по оценке:");
            Console.WriteLine(student1.CompareTo(student2, StudentComparisonField.Grade));


            Book book1 = new Book("1984", "Джордж Оруэлл", 500);
            Book book2 = new Book("О дивный новый мир", "Олдос Хаксли", 300);


            Console.WriteLine("Сравнение книг по названию:");
            Console.WriteLine(book1.CompareTo(book2, BookComparisonField.Title));

            Console.WriteLine("Сравнение книг по автору:");
            Console.WriteLine(book1.CompareTo(book2, BookComparisonField.Author));

            Console.WriteLine("Сравнение книг по цене:");
            Console.WriteLine(book1.CompareTo(book2, BookComparisonField.Price));
        }


        static void Main()
        {
            Test1();
            Console.WriteLine();
            Test2();
            Console.WriteLine();
            Test3();
        }
    }
}
