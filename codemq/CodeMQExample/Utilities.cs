using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeMQ;
using System.Runtime.CompilerServices;

namespace CodeMQExample
{
    public static class Utilities
    {
        public static string NumberToText(int num)
        {
            var s = num.ToString();
            Dictionary<int, string> mapping = new Dictionary<int, string>() { { '1', "one" },
                                                  { '2', "two" },
                                                  { '3', "three" },
                                                  { '4', "four" },
                                                  { '5', "five" },
                                                  { '6', "six" },
                                                  { '7', "seven" },
                                                  { '8', "eight" },
                                                  { '9', "nine" },
                                                  { '0', "zero" },
                                             };


            string output = "";
            string space = "";

            foreach (char c in s)
            {
                output += space + mapping[c];
                space = " ";
            }

            return output;
        }
        
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Write(ConsoleColor c, string txt)
        {
            Console.ForegroundColor = c;
            Console.WriteLine(txt);
        }

        public static void PrintQueueDepths()
        {
            lock (typeof(Utilities))
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;

                var left = Console.CursorLeft;
                var top = Console.CursorTop;

                var depths = CodeMQManager.GetQueueDepths();
                List<string> texts = new List<string>();
                texts.Add(" ");
                texts.Add(" Queue depths ");
                texts.Add(" ");
                texts.AddRange(depths.Select(n => " " + n.Key.Name + ": " + n.Value + " "));
                texts.Add(" ");

                int newLeft = Console.WindowWidth - texts.Max(n => n.Length);
                int wt = Console.WindowTop;
                Console.CursorTop = wt;
                Console.CursorLeft = newLeft;

                for (int i = 0; i < texts.Count; i++)
                {
                    Console.Write(texts[i].PadLeft(texts.Max(n => n.Length)));
                    wt += 1;
                    Console.CursorTop = wt;
                    Console.CursorLeft = newLeft;
                }

                Console.BackgroundColor = ConsoleColor.Black;
                Console.CursorTop = top;
                Console.CursorLeft = left;
            }
        }
    }
}
