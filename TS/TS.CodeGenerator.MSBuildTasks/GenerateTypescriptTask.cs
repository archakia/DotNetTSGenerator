using System.IO;
using System.Reflection;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace TS.CodeGenerator.MSBuildTasks
{
    public class GenerateTypescriptTask : Task
    {
        public string InputDLL { get; set; }
        public string OutputDTS { get; set; }

        public override bool Execute()
        {
            Log.LogMessage(MessageImportance.High, $"Input dll path: '{InputDLL}'");

            Settings.MethodReturnTypeFormatString = "{0}";
            string inputFolder = Path.GetDirectoryName(InputDLL);
            
            Assembly asm = Assembly.LoadFrom(InputDLL);

            string[] files = Directory.GetFiles(inputFolder, "*.dll");
            foreach (string file in files)
            {
                Assembly loadedAssembly = Assembly.LoadFrom(file);
            }

            AssemblyReader reader = new AssemblyReader();
            reader.AddAssembly(asm);

            if (File.Exists(OutputDTS))
            {
                File.Delete(OutputDTS);
            }

            using (var outputFile = File.OpenWrite(OutputDTS))
            {
                using (var streamWriter = new StreamWriter(outputFile))
                {
                    string types = reader.GenerateTypingsString();
                    // sw.WriteLine(@"/// <reference path=""../jquery/jquery.d.ts"" />");
                    streamWriter.WriteLine(types);
                }
            }

            Log.LogMessage(MessageImportance.High, $"Generated TS to path '{OutputDTS}'.");

            return true;
        }
    }
}
