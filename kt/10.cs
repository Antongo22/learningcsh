
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learningcsh.kt
{
    #region 1
    public interface IEntity
    {
        int Id { get; set; }
    }

    public interface IRepository<T> where T : IEntity
    {
        void Add(T item);
        void Delete(T item);
        T FindById(int id);
        IEnumerable<T> GetAll();
    }

    public class Product : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class Customer : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

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

    #endregion


    #region 2
    public interface IClonable<T>
    {
        T Clone();
    }

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

    public static class CloningHelper
    {
        public static T CloneObject<T>(T obj) where T : IClonable<T>
        {
            return obj.Clone();
        }
    }

    #endregion


    #region 3
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
    #endregion
}
