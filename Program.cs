using System;
using System.Collections.Generic;
using learningcsh.kt;


namespace learningcsh
{
    public class Program
    {
        public static void Main()
        {
            Test1();
            Console.WriteLine();
            Test2();
            Console.WriteLine();
            Test3();
        }

        public static void Test1()
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

        public static void Test2()
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

        public static void Test3()
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
}