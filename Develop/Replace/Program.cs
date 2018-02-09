using System;

namespace Replace
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: replace.exe file.txt regex replacement");
                return;
            }

            string sourceFilePath = args[0];
            Console.WriteLine("file: " + sourceFilePath);
            string regex = args[1];
            Console.WriteLine("regex: " + regex);
            string replacement = args[2];
            Console.WriteLine("replacement: " + replacement);

            FileReplacer replacer = new FileReplacer(sourceFilePath);

            try
            {
                int count = replacer.Replace(regex, replacement);
                Console.WriteLine("Replace count: " + count);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to replace: " + ex);
            }
        }
    }
}
