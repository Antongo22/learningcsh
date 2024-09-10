using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learningcsh
{
    public class MyClass
    {
        public int Value { get; set; }

        public MyClass(int value)
        {
            Value = value;
        }

        public static bool operator ==(MyClass a, MyClass b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Value == b.Value;
        }

        public static bool operator !=(MyClass a, MyClass b)
        {
            return !(a == b);
        }

        public static bool operator true(MyClass obj)
        {
            return obj.Value > 0;
        }

        public static bool operator false(MyClass obj)
        {
            return obj.Value <= 0;
        }

        public override bool Equals(object obj)
        {
            if (obj is MyClass other)
            {
                return this == other;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static explicit operator int(MyClass obj)
        {
            return obj.Value;
        }
    }


    internal class Program
    {

        static void Main(string[] args)
        {
            MyClass obj1 = new MyClass(10);
            MyClass obj2 = new MyClass(10);
            MyClass obj3 = new MyClass(-5);

            Console.WriteLine(obj1 == obj2);  
            Console.WriteLine(obj1 != obj3);  

            if (obj1)
            {
                Console.WriteLine("obj1 is true");
            }
            else
            {
                Console.WriteLine("obj1 is false");
            }

            if (obj3)
            {
                Console.WriteLine("obj3 is true");
            }
            else
            {
                Console.WriteLine("obj3 is false");
            }
        }
    }
}
