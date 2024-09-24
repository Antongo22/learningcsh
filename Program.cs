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
using System.IO;
using FileLibrary;

namespace learningcsh
{


    internal class Program
    {
        static void Main(string[] args)
        {
            FileHandler fileHandler = new FileHandler();

            // Демонстрация чтения файла
            Console.WriteLine("Демонстрация чтения файла:");
            string filePath = "example.txt"; 
            string content = fileHandler.ReadFile(filePath);
            Console.WriteLine("Содержимое файла: " + content);
            Console.WriteLine();

            // Демонстрация записи в файл
            Console.WriteLine("Демонстрация записи в файл:");
            string newFilePath = "output.txt";
            fileHandler.WriteFile(newFilePath, "Это текст для записи в файл.");
            Console.WriteLine("Файл записан.");
            Console.WriteLine();

            // Демонстрация работы с checked исключениями
            Console.WriteLine("Демонстрация checked исключения:");
            fileHandler.DemoCheckedException();
            Console.WriteLine();

            // Демонстрация работы с unchecked исключениями
            Console.WriteLine("Демонстрация unchecked исключения:");
            fileHandler.DemoUncheckedException();
            Console.WriteLine();

            // Демонстрация генерации исключения при записи в закрытый файл
            Console.WriteLine("Демонстрация генерации исключения:");
            fileHandler.WriteToClosedFile();
            Console.WriteLine();

            // Демонстрация работы с corrupted state exceptions
            Console.WriteLine("Демонстрация работы с corrupted state exceptions:");
            fileHandler.HandleCorruptedStateExceptions();
            Console.WriteLine();
        }
    }
}
