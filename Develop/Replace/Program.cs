using System;
using System.Collections.Generic;
using CommandLine;
using Replace.DataModel;

namespace Replace
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(RunOptionsAndReturnExitCode)
                .WithNotParsed<Options>(HandleParseError);
        }

        private static void HandleParseError(IEnumerable<Error> error)
        {
            Console.WriteLine("Usage: replace.exe -f file -s regex -r replacement");
        }

        private static void RunOptionsAndReturnExitCode(Options options)
        {
            if (options.Config != null)
            {

            }
            else
            {
                FileReplacement(options);
            }
        }

        private static void FileReplacement(Options options)
        {
            Console.WriteLine("File: " + options.File);
            Console.WriteLine("Search regex pattern: " + options.Regex);
            Console.WriteLine("Replacement: " + options.Replacement);

            FileReplacer replacer = new FileReplacer(options.File);

            try
            {
                int count = replacer.Replace(options.Regex, options.Replacement);
                Console.WriteLine("Replace count: " + count);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to replace: " + ex);
            }
        }
    }
}
