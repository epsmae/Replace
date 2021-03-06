﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using CommandLine;
using Replace.Service;
using Replace.Service.DataModel;

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

        /// <summary>
        /// Main for testing purpose to set a diffrent output
        /// </summary>
        /// <param name="args"></param>
        /// <param name="writer"></param>
        public static void Main(string[] args, StringWriter writer)
        {
            Console.SetOut(writer);
            Main(args);
        }

        private static void HandleParseError(IEnumerable<Error> error)
        {
            if (!error.Any(e => e.Tag == ErrorType.VersionRequestedError || e.Tag == ErrorType.HelpRequestedError))
            {
                ShowUsage();
            }
        }

        private static void ShowUsage()
        {
            Console.WriteLine("Usage: replace.exe -f file -s regex -r replacement");
            Console.WriteLine("Usage: replace.exe -c config.xml");
            Console.WriteLine("Usage: replace.exe -c config.xml -t %0,Tag0 %1,Tag1");
        }

        private static void RunOptionsAndReturnExitCode(Options options)
        {
            Console.WriteLine(string.Empty);

            if (HasConfigFileParameters(options))
            {
                ConfigReplacement(options);
            }
            else if (HasFileParameters(options))
            {
                FileReplacement(options);
            }
            else
            {
                ShowUsage();
            }
        }

        private static bool HasFileParameters(Options options)
        {
            return (options.Config == null && options.File != null && options.Regex != null &&
                    options.Replacement != null);
        }

        private static bool HasConfigFileParameters(Options options)
        {
            return options.Config != null && options.File == null && options.Regex == null &&
                   options.Replacement == null;
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

        private static void ConfigReplacement(Options options)
        {
            Console.WriteLine("Config file path: " + options.Config);

            try
            {
                Config config = GetConfig(options);

                ReplaceConfig replaceConfig = TagReplacer.ReplaceTags(config);
                ConfigReplacer replacer = new ConfigReplacer(replaceConfig);

                DisplayConfig(replaceConfig);

                ReplaceResult result = replacer.Replace();

                if (options.Debug)
                {
                    DisplayReplacements(result.Replacements);
                }

                Console.WriteLine("Replace count: " + result);
            }
            catch (Exception)
            {
                // ignore
            }
        }

        private static void DisplayReplacements(List<KeyValuePair<string, string>> replacements)
        {
            Console.WriteLine(string.Empty);
            Console.WriteLine("Replacements:");
            foreach (KeyValuePair<string, string> valuePair in replacements)
            {
                Console.WriteLine($"\t{valuePair.Key} {valuePair.Value}");
            }
        }

        private static void DisplayConfig(ReplaceConfig config)
        {
            Console.WriteLine("Search path: " + config.PathToSearch);
            Console.WriteLine("Searching in file extensions: " + GetAsString(config.FileExtensions));
            Console.WriteLine("Regex replace values:");
            foreach (RegexReplaceValue replaceValue in config.RegexReplaceValues)
            {
                Console.WriteLine($"\t{replaceValue.Regex} {replaceValue.ReplaceValue}");
            }
        }

        private static string GetAsString(List<string> items)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string item in items)
            {
                builder.Append(item + " ");
            }
            return builder.ToString();
        }

        private static Config GetConfig(Options options)
        {
            try
            {
                ConfigPersistence persistence = new ConfigPersistence();

                Config config = persistence.Load(options.Config);
                config.TagReplacements = new List<KeyValuePair<string, string>>();

                foreach (string replacement in options.TagReplacements)
                {
                    IList<string> itemList = replacement.Split(',');
                    config.TagReplacements.Add(new KeyValuePair<string, string>(itemList[0], itemList[1]));
                }

                return config;
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Failed to read config={0}  {1}", options.Config, ex));
                throw;
            }
        }
    }
}
