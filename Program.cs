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
using System.Runtime.InteropServices;
using System.Reflection;

namespace learningcsh
{
    #region 1
    [AttributeUsage(AttributeTargets.Field)]
    public class DisplayNameAttribute : Attribute
    {
        public string Name { get; }

        public DisplayNameAttribute(string name)
        {
            Name = name;
        }
    }

    public enum DayOfWeek
    {
        [DisplayName("Понедельник")]
        Monday,
        [DisplayName("Вторник")]
        Tuesday,
        [DisplayName("Среда")]
        Wednesday,
        [DisplayName("Четверг")]
        Thursday,
        [DisplayName("Пятница")]
        Friday,
        [DisplayName("Суббота")]
        Saturday,
        [DisplayName("Воскресенье")]
        Sunday
    }


    #endregion

    #region 2
    [AttributeUsage(AttributeTargets.Field)]
    public class HexCodeAttribute : Attribute
    {
        public string HexCode { get; }
        public string Name { get; }

        public HexCodeAttribute(string hexCode, string name)
        {
            HexCode = hexCode;
            Name = name;
        }
    }

    public enum Color
    {
        [HexCode("#FF0000", "Красный")]
        Red,
        [HexCode("#00FF00", "Зелёный")]
        Green,
        [HexCode("#0000FF", "Синий")]
        Blue,
        [HexCode("#FFFF00", "Жёлтый")]
        Yellow,
        [HexCode("#00FFFF", "Бирюзовый")]
        Cyan,
        [HexCode("#FF00FF", "Маджента")]
        Magenta
    }
    #endregion

    #region 3
    [AttributeUsage(AttributeTargets.Field)]
    public class OperationAttribute : Attribute
    {
        public Func<double, double, double> OperationFunc { get; }

        public OperationAttribute(Type delegateType, string methodName)
        {
            MethodInfo methodInfo = delegateType.GetMethod(methodName);
            OperationFunc = (Func<double, double, double>)Delegate.CreateDelegate(typeof(Func<double, double, double>), methodInfo);
        }
    }

    public static class Operations
    {
        public static double Add(double a, double b) => a + b;
        public static double Subtract(double a, double b) => a - b;
        public static double Multiply(double a, double b) => a * b;
        public static double Divide(double a, double b)
        {
            if (b == 0) throw new DivideByZeroException("Cannot divide by zero.");
            return a / b;
        }
    }

    public enum Operation
    {
        [OperationAttribute(typeof(Operations), nameof(Operations.Add))]
        Add,

        [OperationAttribute(typeof(Operations), nameof(Operations.Subtract))]
        Subtract,

        [OperationAttribute(typeof(Operations), nameof(Operations.Multiply))]
        Multiply,

        [OperationAttribute(typeof(Operations), nameof(Operations.Divide))]
        Divide
    }
    #endregion


    internal class Program
    {
        public static string GetDisplayName(DayOfWeek enumValue)
        {
            FieldInfo fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            DisplayNameAttribute attribute = fieldInfo.GetCustomAttribute<DisplayNameAttribute>();

            return attribute?.Name ?? enumValue.ToString();
        }

        public static string GetHexCode(Color enumValue)
        {
            FieldInfo fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            HexCodeAttribute attribute = fieldInfo.GetCustomAttribute<HexCodeAttribute>();

            return attribute?.HexCode + " - " + attribute?.Name ?? "#000000";
        }

        public static double PerformOperation(double a, double b, Operation operation)
        {
            FieldInfo fieldInfo = operation.GetType().GetField(operation.ToString());
            OperationAttribute attribute = fieldInfo.GetCustomAttribute<OperationAttribute>();

            if (attribute == null)
                throw new ArgumentException("Unknown operation.");

            return attribute.OperationFunc(a, b);
        }


        static void Main(string[] args)
        {
            Console.WriteLine(GetDisplayName(DayOfWeek.Monday));
            Console.WriteLine(GetHexCode(Color.Red));
            Console.WriteLine(PerformOperation(10, 5, Operation.Add));
        }
    }
}
