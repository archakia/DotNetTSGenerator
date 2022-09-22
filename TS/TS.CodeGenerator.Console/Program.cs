using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Text.Json;

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

            if (args.Length == 3 && File.Exists(args[2]))
            {
                string settingsFile = Path.GetFullPath(args[2]);

                var s = File.ReadAllText(settingsFile);
                var o = JsonSerializer.Deserialize<OverrideSettings>(s)!;
                Settings.OverwriteDefaults(o);
            }

            Assembly asm = AssemblyLoadContext.Default.LoadFromAssemblyPath(input);

            string[] files = Directory.GetFiles(inputFolder, "*.dll");
            foreach (string file in files)
            {
                AssemblyLoadContext.Default.LoadFromAssemblyPath(file);
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
                    streamWriter.WriteLine(Settings.PrependText);
                    streamWriter.WriteLine(types);
                    streamWriter.WriteLine(Settings.PostpendText);
                }
            }

            System.Console.WriteLine("Typescript generation completed!");
        }
    }
}