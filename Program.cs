using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learningcsh
{
    internal class Program
    {
        class MyArray
        {
            List<int> arr;
            public int Length;

            public MyArray(int size)
            {
                arr = new List<int>(size);
                Length = size;
            }

            public MyArray()
            {
                arr = new List<int>();
                Length = 0;
            }

            public int this[string index]
            {
                get
                {
                    return arr[(int)Math.Round(double.Parse(index, CultureInfo.InvariantCulture))];
                }
                set
                {
                    if ((int)Math.Round(double.Parse(index, CultureInfo.InvariantCulture)) >= arr.Count)
                    {
                        arr.Add(value);
                        Length ++;
                    }
                    arr[(int)Math.Round(double.Parse(index, CultureInfo.InvariantCulture))] = value;
                }
            }
        }


        static void Main(string[] args)
        {
            MyArray myArray = new MyArray();
            for (int i = 0; i < 5; i++)
            {
                myArray[i.ToString() + ".4"] = i;
            }

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(myArray[i.ToString()]);
            }

            Console.WriteLine("Длина - " + myArray.Length);

            Console.ReadLine();
        }
    }
}
