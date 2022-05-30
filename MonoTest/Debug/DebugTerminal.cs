using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Debug
{
    public static class DebugTerminal
    {
        [DllImport("kernel32")]
        static extern bool AllocConsole();


        public static Dictionary<string, Action<string[]>> Commands { get; } = new Dictionary<string, Action<string[]>>();
        public static void Initialize()
        {
            AllocConsole();
            Console.WriteLine("--- DEBUG TERMINAL ---");
            Task.Run(() => Run());
        }


        private static void Run()
        {
            //var stringReader = new StreamReader(stdin.BaseStream);
            while(true)
            {
                Console.Write("> ");
                ParseCommand(Console.ReadLine());
            }
        }

        private static void ParseCommand(string command)
        {
            try
            {
                var commandArgs = command.Split(" ");
                if(commandArgs.Length >= 1 && commandArgs[0] != "")
                {
                    var cmd = commandArgs[0].ToLower();
                    var args = commandArgs[1..];
                    if(Commands.TryGetValue(cmd, out var handler))
                    {
                        handler(args);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("--- Start of error ---");
                Console.WriteLine(ex.Message);
                Console.WriteLine("--- End of error ---");


            }
        }
    }
}
