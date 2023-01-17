using System;
using System.Collections.Generic;
using Mono.Options;

namespace CLITest.Mono.Options
{
    internal class Program
    {
        private static readonly List<string> names = new List<string>();

// thses are the available options, not that they set the variables
        private static readonly OptionSet options = new OptionSet
        {
            { "n|name=", "the name of someone to greet.", n => names.Add(n) },
            { "r|repeat=", "the number of times to repeat the greeting.", (int r) => repeat = r },
            {
                "v", "increase debug message verbosity", v =>
                {
                    if (v != null)
                        ++verbosity;
                }
            },
            { "h|help", "show this message and exit", h => shouldShowHelp = h != null }
        };

        private static int repeat = 1;
        private static bool shouldShowHelp;

        // these variables will be set when the command line is parsed
        private static int verbosity;

        public static void Main(string[] args)
        {
            List<string> extra;
            try
            {
                // parse the command line
                extra = options.Parse(args);

                // show some app description message
                Console.WriteLine("Usage: OptionsSample.exe [OPTIONS]+ message");
                Console.WriteLine("Greet a list of individuals with an optional message.");
                Console.WriteLine("If no message is specified, a generic greeting is used.");
                Console.WriteLine();

                // output the options
                Console.WriteLine("Options:");
                options.WriteOptionDescriptions(Console.Out);
            }
            catch (OptionException e)
            {
                // output some error message
                Console.Write("greet: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `greet --help' for more information.");
            }
        }
    }
}