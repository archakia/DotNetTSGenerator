using Microsoft.Build.Utilities;
using System;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace TS.CodeGenerator.MSBuildTasks
{
    public class GenerateTypescriptTask : Task
    {
        public string InputDLL { get; set; }
        public string OutputDTS { get; set; }
        public string OverrideSettingsFilePath { get; set; }

        public override bool Execute()
        {
            Log.LogMessage($"Input dll path: '{InputDLL}'");

            Settings.MethodReturnTypeFormatString = "{0}";
            string inputFolder = Path.GetDirectoryName(InputDLL);

            if (!string.IsNullOrWhiteSpace(OverrideSettingsFilePath))
            {
                string settingsFilePath = Path.GetFullPath(OverrideSettingsFilePath);

                Log.LogMessage($"Using overridden generator settings from file: '{settingsFilePath}'");

                string settingsFileContent = File.ReadAllText(settingsFilePath);
                OverrideSettings overrideSettings = JsonSerializer.Deserialize<OverrideSettings>(settingsFileContent);
                if (overrideSettings == null)
                {
                    throw new ArgumentNullException("The provided serialized settings object was empty.");
                }

                Settings.OverwriteDefaults(overrideSettings);
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
                    streamWriter.WriteLine(types);
                }
            }

            Log.LogMessage($"Generated TS to path '{OutputDTS}'.");

            return true;
        }
    }
}
