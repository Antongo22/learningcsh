using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;

namespace learningcsh.kt
{
    internal class _14
    {
        #region 1

        public static class WordFrequency
        {
            public static Dictionary<string, int> CountWordFrequency(string filePath)
            {
                var wordFrequency = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                if (File.Exists(filePath))
                {
                    string text = File.ReadAllText(filePath);
                    var words = Regex.Matches(text, @"\w+");

                    foreach (Match word in words)
                    {
                        string key = word.Value;
                        if (wordFrequency.ContainsKey(key))
                        {
                            wordFrequency[key]++;
                        }
                        else
                        {
                            wordFrequency[key] = 1;
                        }
                    }
                }
                return wordFrequency.OrderByDescending(kv => kv.Value).ToDictionary(kv => kv.Key, kv => kv.Value);
            }

            public static void DisplayWordFrequency(Dictionary<string, int> wordFrequency)
            {
                foreach (var word in wordFrequency)
                {
                    Console.WriteLine($"{word.Key}: {word.Value}");
                }
            }
        }


        public static void TestWordFrequency()
        {
            Console.WriteLine("== Тест задания 1: Частота слов ==\n");
            string filePath = "test.txt"; 
            var wordFrequency = WordFrequency.CountWordFrequency(filePath);
            WordFrequency.DisplayWordFrequency(wordFrequency);
        }
        

        #endregion

        #region 2

        public static class ArithmeticExpressionEvaluator
        {
            private static readonly Dictionary<char, int> OperatorPrecedence = new Dictionary<char, int>
            {
                { '+', 1 },
                { '-', 1 },
                { '*', 2 },
                { '/', 2 }
            };

            public static double Evaluate(string expression)
            {
                var postfix = ConvertToPostfix(expression);

                Console.WriteLine("Postfix: " + string.Join(",", postfix));


                return EvaluatePostfix(postfix);
            }

            private static List<string> ConvertToPostfix(string expression)
            {
                var output = new List<string>();
                var operators = new Stack<char>();

                for (int i = 0; i < expression.Length; i++)
                {
                    char token = expression[i];

                    if (char.IsDigit(token))
                    {
                        string number = "";
                        while (i < expression.Length && (char.IsDigit(expression[i]) || expression[i] == '.'))
                        {
                            number += expression[i];
                            i++;
                        }
                        i--;
                        output.Add(number);
                    }
                    else if (OperatorPrecedence.ContainsKey(token))
                    {
                        while (operators.Count > 0 && OperatorPrecedence.ContainsKey(operators.Peek()) && OperatorPrecedence[operators.Peek()] >= OperatorPrecedence[token])
                        {
                            output.Add(operators.Pop().ToString());
                        }
                        operators.Push(token);
                    }
                    else if (token == '(')
                    {
                        operators.Push(token);
                    }
                    else if (token == ')')
                    {
                        while (operators.Peek() != '(')
                        {
                            output.Add(operators.Pop().ToString());
                        }
                        operators.Pop();  
                    }
                }

                while (operators.Count > 0)
                {
                    output.Add(operators.Pop().ToString());
                }

                return output;
            }

            private static double EvaluatePostfix(List<string> postfix)
            {
                var stack = new Stack<double>();

                foreach (string token in postfix)
                {
                    if (double.TryParse(token, out double number))
                    {
                        stack.Push(number);
                    }
                    else
                    {
                        double b = stack.Pop();
                        double a = stack.Pop();

                        switch (token)
                        {
                            case "+":
                                stack.Push(a + b);
                                break;
                            case "-":
                                stack.Push(a - b);
                                break;
                            case "*":
                                stack.Push(a * b);
                                break;
                            case "/":
                                stack.Push(a / b);
                                break;
                        }
                    }
                }

                return stack.Pop();
            }
        }


        public static void TestArithmeticExpression()
        {
            Console.WriteLine("\n== Тест задания 2: Вычисление выражения ==\n");

            string expression = "(3 + 5 * (2 - 8)) / 9";
            double result = ArithmeticExpressionEvaluator.Evaluate(expression);
            Console.WriteLine($"Результат выражения \"{expression}\" = {result}");
        }
        

        #endregion

        #region 3

        public static class CountryInfo
        {
            private static readonly Dictionary<string, string> CountryCapitals = new Dictionary<string, string>
            {
                { "Россия", "Москва" },
                { "США", "Вашингтон" },
                { "Франция", "Париж" }
            };

            private static readonly Dictionary<string, int> CountryPopulations = new Dictionary<string, int>
            {
                { "Россия", 146000000 },
                { "США", 331000000 },
                { "Франция", 67000000 }
            };

            public static void GetCountryInfo(string country)
            {
                if (CountryCapitals.ContainsKey(country) && CountryPopulations.ContainsKey(country))
                {
                    Console.WriteLine($"Страна: {country}");
                    Console.WriteLine($"Столица: {CountryCapitals[country]}");
                    Console.WriteLine($"Население: {CountryPopulations[country]:N0}");
                }
                else
                {
                    Console.WriteLine("Информация о стране не найдена.");
                }
            }
        }


        public static void TestCountryInfo()
        {
            Console.WriteLine("\n== Тест задания 3: Информация о стране ==\n");

            string country = "Россия"; 
            CountryInfo.GetCountryInfo(country);

            country = "Германия";  
            CountryInfo.GetCountryInfo(country);
        }
        

        #endregion
    }
}
