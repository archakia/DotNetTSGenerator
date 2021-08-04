using System.IO;
using System.Reflection;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace TS.CodeGenerator.MSBuildTasks
{
    public class GenerateTypescriptTask : Task
    {

        public override bool Execute()
        {

            Log.LogMessage(MessageImportance.High, $"Inupt dll {InputDLL}");



            Settings.MethodReturnTypeFormatString = "{0}";
            var inputFolder = Path.GetDirectoryName(InputDLL);

            
            Assembly asm = Assembly.LoadFrom(InputDLL); //AssemblyLoadContext.Default.LoadFromAssemblyPath(InputDLL);

            var files = Directory.GetFiles(inputFolder, "*.dll");
            foreach (var file in files)
            {
                var l = Assembly.LoadFrom(file);// AssemblyLoadContext.Default.LoadFromAssemblyPath(file);
            }

            var reader = new AssemblyReader();
            reader.AddAssembly(asm);

            if (File.Exists(OutputDTS))
            {
                File.Delete(OutputDTS);
            }
            using (var of = File.OpenWrite(OutputDTS))
            using (var sw = new StreamWriter(of))
            {
                var types = reader.GenerateTypingsString();
                // sw.WriteLine(@"/// <reference path=""../jquery/jquery.d.ts"" />");
                sw.WriteLine(types);

            }



            Log.LogMessage(MessageImportance.High, $"Generated TS to {OutputDTS}");


            return true;
        }

        public string InputDLL { get; set; }
        public string OutputDTS { get; set; }

    }
}
