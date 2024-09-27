using System;
using System.Collections.Generic;

// Интерфейс IEntity
public interface IEntity
{
    int Id { get; set; }
}

// Обобщенный интерфейс IRepository
public interface IRepository<T> where T : IEntity
{
    void Add(T item);
    void Delete(T item);
    T FindById(int id);
    IEnumerable<T> GetAll();
}

// Класс Product
public class Product : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

// Класс Customer
public class Customer : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
}

// Реализация ProductRepository
public class ProductRepository : IRepository<Product>
{
    private List<Product> _products = new List<Product>();

    public void Add(Product item)
    {
        _products.Add(item);
    }

    public void Delete(Product item)
    {
        _products.Remove(item);
    }

    public Product FindById(int id)
    {
        return _products.Find(p => p.Id == id);
    }

    public IEnumerable<Product> GetAll()
    {
        return _products;
    }
}

// Реализация CustomerRepository
public class CustomerRepository : IRepository<Customer>
{
    private List<Customer> _customers = new List<Customer>();

    public void Add(Customer item)
    {
        _customers.Add(item);
    }

    public void Delete(Customer item)
    {
        _customers.Remove(item);
    }

    public Customer FindById(int id)
    {
        return _customers.Find(c => c.Id == id);
    }

    public IEnumerable<Customer> GetAll()
    {
        return _customers;
    }
}

// Интерфейс IClonable<T>
public interface IClonable<T>
{
    T Clone();
}

// Класс Point, реализующий IClonable<Point>
public class Point : IClonable<Point>
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point() { }

    public Point(Point other)
    {
        X = other.X;
        Y = other.Y;
    }

    public Point Clone()
    {
        return new Point(this);
    }
}

// Класс Rectangle, реализующий IClonable<Rectangle>
public class Rectangle : IClonable<Rectangle>
{
    public int Width { get; set; }
    public int Height { get; set; }

    public Rectangle() { }

    public Rectangle(Rectangle other)
    {
        Width = other.Width;
        Height = other.Height;
    }

    public Rectangle Clone()
    {
        return new Rectangle(this);
    }
}

// Метод клонирования
public static class CloningHelper
{
    public static T CloneObject<T>(T obj) where T : IClonable<T>
    {
        return obj.Clone();
    }
}

// Интерфейс IComparer<T> для структур
public interface IComparer<T> where T : struct
{
    int Compare(T x, T y);
}

public struct ComplexNumber : IComparer<ComplexNumber>
{
    public double Real { get; set; }
    public double Imaginary { get; set; }

    public int Compare(ComplexNumber x, ComplexNumber y)
    {
        return x.Real.CompareTo(y.Real);
    }
}

public struct RationalNumber : IComparer<RationalNumber>
{
    public int Numerator { get; set; }
    public int Denominator { get; set; }

    public int Compare(RationalNumber x, RationalNumber y)
    {
        double valueX = (double)x.Numerator / x.Denominator;
        double valueY = (double)y.Numerator / y.Denominator;
        return valueX.CompareTo(valueY);
    }
}

public class Program
{
    public static void Main()
    {
        TestRepositories();
        TestClonable();
        TestComparers();
    }

    public static void TestRepositories()
    {
        var productRepo = new ProductRepository();
        var customerRepo = new CustomerRepository();

        var product = new Product { Id = 1, Name = "Laptop", Price = 1000m };
        var customer = new Customer { Id = 1, Name = "John Doe", Address = "123 Main St" };

        productRepo.Add(product);
        customerRepo.Add(customer);

        Console.WriteLine("Added Product: " + productRepo.FindById(1).Name);
        Console.WriteLine("Added Customer: " + customerRepo.FindById(1).Name);

        productRepo.Delete(product);
        customerRepo.Delete(customer);

        Console.WriteLine("Product exists: " + (productRepo.FindById(1) != null));
        Console.WriteLine("Customer exists: " + (customerRepo.FindById(1) != null));
    }

    public static void TestClonable()
    {
        var point = new Point { X = 10, Y = 20 };
        var clonedPoint = CloningHelper.CloneObject(point);

        Console.WriteLine($"Original Point: X={point.X}, Y={point.Y}");
        Console.WriteLine($"Cloned Point: X={clonedPoint.X}, Y={clonedPoint.Y}");

        var rectangle = new Rectangle { Width = 30, Height = 40 };
        var clonedRectangle = CloningHelper.CloneObject(rectangle);

        Console.WriteLine($"Original Rectangle: Width={rectangle.Width}, Height={rectangle.Height}");
        Console.WriteLine($"Cloned Rectangle: Width={clonedRectangle.Width}, Height={clonedRectangle.Height}");
    }

    public static void TestComparers()
    {
        var complexComparer = new ComplexNumber();
        var complex1 = new ComplexNumber { Real = 5, Imaginary = 2 };
        var complex2 = new ComplexNumber { Real = 3, Imaginary = 1 };

        Console.WriteLine("Complex Comparison: " + complexComparer.Compare(complex1, complex2));

        var rationalComparer = new RationalNumber();
        var rational1 = new RationalNumber { Numerator = 3, Denominator = 4 };
        var rational2 = new RationalNumber { Numerator = 1, Denominator = 2 };

        Console.WriteLine("Rational Comparison: " + rationalComparer.Compare(rational1, rational2));
    }
}
