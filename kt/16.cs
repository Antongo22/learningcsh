using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace learningcsh.kt
{
    internal class _16
    {
        #region 1
        public static void Test1()
        {
            int objectCount = 100000000;

            GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;
            MeasureGC(objectCount, "Параллельная сборка мусора");

            GCSettings.LatencyMode = GCLatencyMode.LowLatency;
            MeasureGC(objectCount, "Фоновая сборка мусора");

            GCSettings.LatencyMode = GCLatencyMode.Batch;
            MeasureGC(objectCount, "Непараллельная сборка мусора");
        }

        static void MeasureGC(int objectCount, string mode)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < objectCount; i++)
            {
                var obj = new object();
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();

            stopwatch.Stop();
            Console.WriteLine($"{mode}: {stopwatch.ElapsedMilliseconds} мс");
        }
        #endregion

        #region 2
        public static void Test2()
        {
            Console.WriteLine("Включена ли серверная сборка - " + GCSettings.IsServerGC);
            GC.RegisterForFullGCNotification(10, 10);

            Task[] tasks = new Task[3];
            tasks[0] = Task.Run(() => AllocateMemory());
            tasks[1] = Task.Run(() => ReadFromFile());
            tasks[2] = Task.Run(() => WriteToConsole());

            Task.Run(() => MonitorGC());

            Task.WaitAll(tasks);  

            Console.WriteLine("Завершено выполнение задач.");
        }

        static void MonitorGC()
        {
            while (true)
            {
                GCNotificationStatus status = GC.WaitForFullGCApproach();
                if (status == GCNotificationStatus.Succeeded)
                {
                    Console.WriteLine("Начало полной сборки мусора.");
                    Console.WriteLine($"Поколение: {GC.MaxGeneration}");
                    Console.WriteLine($"Выделенная память: {GC.GetTotalMemory(false)} байт");
                }

                status = GC.WaitForFullGCComplete();
                if (status == GCNotificationStatus.Succeeded)
                {
                    Console.WriteLine("Завершение полной сборки мусора.");
                    Console.WriteLine($"Поколение: {GC.MaxGeneration}");
                    Console.WriteLine($"Выделенная память: {GC.GetTotalMemory(false)} байт");
                }
            }
        }

        static void AllocateMemory()
        {
            Console.WriteLine("Выделение памяти...");
            for (int i = 0; i < 100; i++)
            {
                byte[] memory = new byte[1024 * 1024];
                Thread.Sleep(100); 
            }
        }

        static void ReadFromFile()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("Чтение из файла...");
                using (StreamReader reader = new StreamReader("test.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        Console.WriteLine("Строка - " + line);
                        Thread.Sleep(100);
                    }
                }
            }
        }

        static void WriteToConsole()
        {
            Console.WriteLine("Вывод на консоль...");
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"Сообщение {i}");
                Thread.Sleep(100); 
            }
        }
        #endregion

        #region 3
        public static void Test3()
        {
            long totalSize = 1024 * 1024 * 10;

            if (GC.TryStartNoGCRegion(totalSize)) 
            {
                Console.WriteLine("Сборка мусора временно запрещена.");
                try
                {
                    CreateObjects();

                    if (GCSettings.IsServerGC)
                    {
                        Console.WriteLine("Серверная сборка мусора активна.");
                    }
                    else
                    {
                        Console.WriteLine("Серверная сборка мусора не активна.");
                    }

                    GC.EndNoGCRegion();
                    Console.WriteLine("Запрет сборки мусора завершен.");

                    GC.Collect(1, GCCollectionMode.Forced, true);
                    Console.WriteLine("Высокоприоритетная сборка мусора завершена.");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Не удалось начать регион без сборки мусора.");
            }
        }

        static void CreateObjects()
        {
            Console.WriteLine("Создание объектов...");
            for (int i = 0; i < 100000; i++)
            {
                byte[] obj = new byte[1024];
            }
            Console.WriteLine("Объекты созданы.");
        }
        #endregion

    }
}
