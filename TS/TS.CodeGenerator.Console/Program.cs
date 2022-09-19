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

            System.Console.WriteLine($"Input Path {input}");
            System.Console.WriteLine($"Output Path {output}");
            if (!File.Exists(input))
            {
                System.Console.Error.WriteLine($"Could Not Find input {input}");
                return;
            }

            Settings.MethodReturnTypeFormatString = "{0}";
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
                    // sw.WriteLine(@"/// <reference path=""../jquery/jquery.d.ts"" />");
                    streamWriter.WriteLine(types);
                }
            }

            System.Console.WriteLine("...");
            System.Console.WriteLine("Completed");
        }
    }
}