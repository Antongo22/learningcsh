using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faker.Resources;
using Faker.Extensions;

namespace learningcsh
{
    class User
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public User(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }


    internal class Program
    {

        

        static void DB()
        {
            string connectionString = "Data Source=users.db;Version=3;";
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Users (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Age INTEGER NOT NULL
                );";
                using (var createTableCommand = new SQLiteCommand(createTableQuery, connection))
                {
                    createTableCommand.ExecuteNonQuery();
                }


                for (int i = 0; i < 10; i++)
                {
                    var name = Faker.Name.First();
                    var age = Faker.RandomNumber.Next(1, 70);

                    User user = new User(name, age);

                    try
                    {
                        if (age < 14)
                        {
                            throw new Exception("Возраст пользователя должен быть не менее 14 лет1");
                        }

                        string insertQuery = "INSERT INTO Users (Name, Age) VALUES (@Name, @Age)";
                        using (var insertCommand = new SQLiteCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@Name", name);
                            insertCommand.Parameters.AddWithValue("@Age", age);
                            insertCommand.ExecuteNonQuery();
                        }

                        Console.WriteLine($"Пользователь {name}, возраст {age} добавлен в базу данных.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Пользователь {name}, возраст {age} не был добавлен в базу данных из-за возраста.");
                    }


                }

                connection.Close();
            }
        }

        static void PrintUsers()
        {
            string connectionString = "Data Source=users.db;Version=3;";
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT Id, Name, Age FROM Users";
                using (var selectCommand = new SQLiteCommand(selectQuery, connection))
                {
                    using (var reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            int age = reader.GetInt32(2);

                            Console.WriteLine($"ID: {id}, Name: {name}, Age: {age}");
                        }
                    }
                }

                connection.Close();
            }
        }

        static void Create()
        {
            string connectionString = "Data Source=users.db;Version=3;";
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Users (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Age INTEGER NOT NULL
                );";
                using (var createTableCommand = new SQLiteCommand(createTableQuery, connection))
                {
                    createTableCommand.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        static void Main()
        {

            Create();

            do
            {
                Console.WriteLine("Введите команду: n - добавить пользователя, s - вывести список пользователей, 0 - выход из программы");
                Console.Write("> ");
                string input = Console.ReadLine();
                if (input == "n")
                {
                    DB();
                }
                else if (input == "s")
                {
                    PrintUsers();
                }
                else if (input == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Повторите команду ещё раз!");
                }

                Console.WriteLine("\n\n");
            } while (true);

            Console.WriteLine("Пока!");
        }
    }
}
