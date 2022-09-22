using Microsoft.Build.Utilities;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace TS.CodeGenerator.MSBuildTasks
{
    public class GenerateTypescriptTask : Task
    {
        public string InputDLL { get; set; }
        public string OutputDTS { get; set; }
        public string SettingsJson { get; set; }

        public override bool Execute()
        {
            Log.LogMessage($"Input dll path: '{InputDLL}'");

            Settings.MethodReturnTypeFormatString = "{0}";
            string inputFolder = Path.GetDirectoryName(InputDLL);


            if (!string.IsNullOrWhiteSpace(SettingsJson))
            {
                string settingsFile = Path.GetFullPath(SettingsJson);

                Log.LogMessage($"Using generation settings override {settingsFile}");

                var s = File.ReadAllText(settingsFile);
                var x = new JsonParser().Parse<OverrideSettings>(s);
                Settings.OverwriteDefaults(x);
            }


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
                    streamWriter.WriteLine(Settings.PrependText);
                    streamWriter.WriteLine(types);
                    streamWriter.WriteLine(Settings.PostpendText);
                }
            }

            Log.LogMessage($"Generated TS to path '{OutputDTS}'.");

            return true;
        }
    }
}
