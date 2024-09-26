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
using System.Threading;

namespace learningcsh
{
    #region 1
    class Timer
    {
        public event EventHandler Tick;

        public void Start()
        {
            while (true)
            {
                Thread.Sleep(1000);
                OnTick();
            }
        }

        protected virtual void OnTick()
        {
            Tick?.Invoke(this, EventArgs.Empty);
        }
    }

    class Clock
    {
        public void Subscribe(Timer timer)
        {
            timer.Tick += ShowTime;
        }

        private void ShowTime(object sender, EventArgs e)
        {
            Console.WriteLine($"Текущее время: {DateTime.Now.ToLongTimeString()}");
        }
    }   

    class Counter
    {
        private int count = 0;

        public void Subscribe(Timer timer)
        {
            timer.Tick += IncrementCounter;
        }

        private void IncrementCounter(object sender, EventArgs e)
        {
            count++;
            Console.WriteLine($"Счетчик: {count}");
        }
    }
    #endregion

    #region 2
    class BankAccount
    {
        private decimal balance;
        public decimal Balance
        {
            get { return balance; }
            private set
            {
                balance = value;
                OnBalanceChanged(value);
            }
        }

        public event Action<decimal> BalanceChanged;

        protected virtual void OnBalanceChanged(decimal newBalance)
        {
            BalanceChanged?.Invoke(newBalance);
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= Balance)
            {
                Balance -= amount;
            }
            else
            {
                Console.WriteLine("Недостаточно средств.");
            }
        }
    }

    class Logger
    {
        private string logFilePath = "balance_log.txt";

        public Logger(BankAccount account)
        {
            account.BalanceChanged += LogBalanceChange;
        }

        private void LogBalanceChange(decimal newBalance)
        {
            string logMessage = $"Баланс изменен: {newBalance}, Время: {DateTime.Now}\n";
            File.AppendAllText(logFilePath, logMessage);
            Console.WriteLine("Изменение баланса записано в лог.");
        }
    }
    #endregion

    #region 3
    class Button
    {
        private string text;
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                Console.WriteLine($"Текст кнопки изменен на: {text}");
            }
        }

        private event EventHandler click;
        private int maxSubscribers = 3;
        private int currentSubscribers = 0;

        public event EventHandler Click
        {
            add
            {
                if (currentSubscribers >= maxSubscribers)
                {
                    Console.WriteLine("Достигнуто максимальное количество подписчиков.");
                }
                else if (click?.GetInvocationList().Contains(value) == true)
                {
                    Console.WriteLine("Этот подписчик уже добавлен.");
                }
                else
                {
                    click += value;
                    currentSubscribers++;
                }
            }
            remove
            {
                if (click != null && click.GetInvocationList().Contains(value))
                {
                    click -= value;
                    currentSubscribers--;
                }
            }
        }

        public void Press()
        {
            click?.Invoke(this, EventArgs.Empty);
        }
    }
    #endregion

    internal class Program
    {

        static void Test1() 
        {
            Timer timer = new Timer();
            Clock clock = new Clock();
            Counter counter = new Counter();

            clock.Subscribe(timer);
            counter.Subscribe(timer);

            timer.Start();
            
        }

        static void Test2()
        {
            BankAccount account = new BankAccount();
            Logger logger = new Logger(account);

            account.Deposit(500);
            account.Withdraw(200);
            account.Withdraw(1000);
        }


        static void ShowButtonText(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                Console.WriteLine($"Кнопка нажата: {btn.Text}");
            }
        }

        static void ChangeButtonColor(object sender, EventArgs e)
        {
            Console.WriteLine("Цвет кнопки изменен.");
        }

        static void Test3()
        {
            Button button = new Button { Text = "Нажми меня" };

            button.Click += ShowButtonText;
            button.Click += ChangeButtonColor;

            button.Click += (sender, e) => Console.WriteLine("Третий подписчик.");

            button.Click += (sender, e) => Console.WriteLine("Четвертый подписчик не будет добавлен.");

            button.Press();
        }

        static void Main(string[] args)
        {
            _9.Test1();
            Console.WriteLine("\n\n\n");
            _9.Test2();
            Console.WriteLine("\n\n\n");
            _9.Test3();
        }
    }
}
