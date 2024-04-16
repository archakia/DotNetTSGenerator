using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace TS.CodeGenerator.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Path.GetFullPath(args[0]);
            string output = Path.GetFullPath(args[1]);
            string inputFolder = Path.GetDirectoryName(input);

            System.Console.WriteLine($"Input path: '{input}'");
            System.Console.WriteLine($"Output path: '{output}'");
            if (!File.Exists(input))
            {
                string message = $"Could not find input: '{input}'";
                System.Console.Error.WriteLine(message);
                throw new ArgumentException(message);
            }

            Settings.MethodReturnTypeFormatString = "{0}";

            if (args.Length >= 3)
            {
                string settingsFileArg = args[2];

                System.Console.WriteLine($"Optional settings file path: '{settingsFileArg}'");
                if (File.Exists(settingsFileArg))
                {
                    string settingsFilePath = Path.GetFullPath(args[2]);

                    System.Console.WriteLine($"Using overridden generator settings from file: '{settingsFilePath}'");

                    string settingsFileContent = File.ReadAllText(settingsFilePath);
                    System.Console.WriteLine(settingsFileContent);

                    OverrideSettings overrideSettings = JsonConvert.DeserializeObject<OverrideSettings>(settingsFileContent);
                    if (overrideSettings == null)
                    {
                        throw new ArgumentNullException("The provided serialized settings object was empty.");
                    }
                    System.Console.WriteLine(JsonConvert.SerializeObject(overrideSettings));

                    Settings.OverwriteDefaults(overrideSettings);
                }
                else
                {
                    System.Console.WriteLine($"Could not locate optional settings file at path: '{settingsFileArg}'");
                }
            }

            AssemblyLoadContext inputContext = new AssemblyLoadContext("InputContext");

            Assembly asm = inputContext.LoadFromAssemblyPath(input);

            string[] files = Directory.GetFiles(inputFolder, "*.dll");
            foreach (string file in files)
            {
                inputContext.LoadFromAssemblyPath(file);
            }

            AssemblyReader reader = new AssemblyReader();
            reader.AddAssembly(asm);

            if (File.Exists(output))
            {
                File.Delete(output);
            }

            using (var outputFile = File.OpenWrite(output))
            {
                using (var streamWriter = new StreamWriter(outputFile))
                {
                    string types = reader.GenerateTypingsString();
                    streamWriter.WriteLine(types);
                }
            }

            System.Console.WriteLine("Typescript generation completed!");
        }
    }
}