using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(@"   _____                _       _        _____                      _      ");
            Console.WriteLine(@"  / ____|              | |     | |      / ____|                    | |     ");
            Console.WriteLine(@" | (___   ___ _ __ __ _| |_ ___| |__   | |     ___  _ __  ___  ___ | | ___ ");
            Console.WriteLine(@"  \___ \ / __| '__/ _` | __/ __| '_ \  | |    / _ \| '_ \/ __|/ _ \| |/ _ \");
            Console.WriteLine(@"  ____) | (__| | | (_| | || (__| | | | | |___| (_) | | | \__ \ (_) | |  __/");
            Console.WriteLine(@" |_____/ \___|_|  \__,_|\__\___|_| |_|  \_____\___/|_| |_|___/\___/|_|\___|");

            var port = 15004;

            var options = new StartOptions()
            {
                ServerFactory = "Nowin",
                Port = port
            };

            Console.WriteLine();

            try
            {
                using (WebApp.Start<Startup>(options))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Woohoow! The Scratch Console is running!");

                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("The server is listening on port " + port);

                    Console.WriteLine();
                    Console.WriteLine(" - Start the Scratch offline editor");
                    Console.WriteLine(" - Click 'File' while holding the SHIFT key");
                    Console.WriteLine(" - Load the 'ScratchConsole Extension.s2e'");
                    Console.WriteLine(" - Look in 'More blocks' and have fun!");

                    while (true)
                    {
                        var command = Console.ReadLine();
                        ScratchController.NewCommand(command);
                    }
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Oops! Something is kaput :-(");

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Here's some more info, hope it's helpful...");
                Console.WriteLine();
                Console.WriteLine(e);
                Console.ReadLine();

            }
        }
    }
}
