using NUnit.Framework;
using FluentAssertions;
using System.Collections.Generic;

namespace learningcsh.Tests
{
    public class RepositoryTests
    {
        [Test]
        public void ProductRepository_Add_ShouldAddProductToRepository()
        {
            var productRepo = new ProductRepository();
            var product = new Product { Id = 1, Name = "Laptop", Price = 1000m };

            productRepo.Add(product);

            // Assert
            productRepo.FindById(1).Should().NotBeNull();
            productRepo.FindById(1).Name.Should().Be("Laptop");
        }

        [Test]
        public void ProductRepository_Delete_ShouldRemoveProductFromRepository()
        {
            // Arrange
            var productRepo = new ProductRepository();
            var product = new Product { Id = 1, Name = "Laptop", Price = 1000m };
            productRepo.Add(product);

            // Act
            productRepo.Delete(product);

            // Assert
            productRepo.FindById(1).Should().BeNull();
        }

        [Test]
        public void CustomerRepository_Add_ShouldAddCustomerToRepository()
        {
            // Arrange
            var customerRepo = new CustomerRepository();
            var customer = new Customer { Id = 1, Name = "John Doe", Address = "123 Main St" };

            // Act
            customerRepo.Add(customer);

            // Assert
            customerRepo.FindById(1).Should().NotBeNull();
            customerRepo.FindById(1).Name.Should().Be("John Doe");
        }

        [Test]
        public void CustomerRepository_Delete_ShouldRemoveCustomerFromRepository()
        {
            // Arrange
            var customerRepo = new CustomerRepository();
            var customer = new Customer { Id = 1, Name = "John Doe", Address = "123 Main St" };
            customerRepo.Add(customer);

            // Act
            customerRepo.Delete(customer);

            // Assert
            customerRepo.FindById(1).Should().BeNull();
        }
    }

    public class CloneableTests
    {
        [Test]
        public void Point_Clone_ShouldReturnExactCopyOfPoint()
        {
            // Arrange
            var point = new Point { X = 10, Y = 20 };

            // Act
            var clonedPoint = CloningHelper.CloneObject(point);

            // Assert
            clonedPoint.Should().NotBeSameAs(point);
            clonedPoint.X.Should().Be(10);
            clonedPoint.Y.Should().Be(20);
        }

        [Test]
        public void Rectangle_Clone_ShouldReturnExactCopyOfRectangle()
        {
            // Arrange
            var rectangle = new Rectangle { Width = 30, Height = 40 };

            // Act
            var clonedRectangle = CloningHelper.CloneObject(rectangle);

            // Assert
            clonedRectangle.Should().NotBeSameAs(rectangle);
            clonedRectangle.Width.Should().Be(30);
            clonedRectangle.Height.Should().Be(40);
        }
    }

    public class ComparerTests
    {
        [Test]
        public void ComplexNumber_Compare_ShouldReturnCorrectComparison()
        {
            // Arrange
            var complexComparer = new ComplexNumber();
            var complex1 = new ComplexNumber { Real = 5, Imaginary = 2 };
            var complex2 = new ComplexNumber { Real = 3, Imaginary = 1 };

            // Act
            var result = complexComparer.Compare(complex1, complex2);

            // Assert
            result.Should().BeGreaterThan(0);  // complex1.Real > complex2.Real
        }

        [Test]
        public void RationalNumber_Compare_ShouldReturnCorrectComparison()
        {
            // Arrange
            var rationalComparer = new RationalNumber();
            var rational1 = new RationalNumber { Numerator = 3, Denominator = 4 };
            var rational2 = new RationalNumber { Numerator = 1, Denominator = 2 };

            // Act
            var result = rationalComparer.Compare(rational1, rational2);

            // Assert
            result.Should().BeGreaterThan(0);  // 3/4 > 1/2
        }
    }
}
