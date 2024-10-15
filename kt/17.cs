using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace learningcsh.kt
{
    internal class _17
    {
        #region 1
        public class FileReader : IDisposable
        {
            private StreamReader _reader;

            public FileReader(string filePath)
            {
                _reader = new StreamReader(filePath);
            }

            public string ReadLine()
            {
                return _reader.ReadLine();
            }

            public void Dispose()
            {
                if (_reader != null)
                {
                    _reader.Dispose();
                    _reader = null;
                }
            }
        }

        public static void Test1()
        {
            using (var fileReader = new FileReader("test.txt"))
            {
                string line;
                while ((line = fileReader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }
        #endregion

        #region 2
        public class DatabaseConnection : IDisposable
        {
            private SQLiteConnection _connection;

            public DatabaseConnection(string connectionString)
            {
                _connection = new SQLiteConnection(connectionString);
                _connection.Open();
            }

            public int ExecuteQuery(string query)
            {
                using (var command = new SQLiteCommand(query, _connection))
                {
                   return command.ExecuteNonQuery();
                }
            }

            public SQLiteDataReader ExecuteReader(string query)
            {
                var command = new SQLiteCommand(query, _connection);
                return command.ExecuteReader();
            }


            public void Dispose()
            {
                if (_connection != null)
                {
                    _connection.Close();
                    _connection.Dispose();
                    _connection = null;
                }
            }
        }

        public static void Test2()
        {
            using (var dbConnection = new DatabaseConnection("Data Source=users.db;version=3"))
            {
                var s = dbConnection.ExecuteReader("SELECT * FROM Users");

                using (var reader = dbConnection.ExecuteReader("SELECT Id, Name FROM Users"))
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Id: {reader["Id"]}, Name: {reader["Name"]}");
                    }
                }
            }

        }
        #endregion

        #region 3
        public class BitmapImage : IDisposable
        {
            private Bitmap _image;

            public BitmapImage(string filePath)
            {
                _image = new Bitmap(filePath);
            }

            public void Save(string savePath)
            {
                _image.Save(savePath);
            }

            public void Dispose()
            {
                if (_image != null)
                {
                    _image.Dispose();
                    _image = null;
                }
            }
        }

        public static void Test3()
        {
            using (var image = new BitmapImage("image.bmp"))
            {
                image.Save("output.bmp");
            } 
            
        }
        #endregion
    }
}
