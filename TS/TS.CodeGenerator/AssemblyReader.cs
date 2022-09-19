using System;
using System.IO;
using System.Reflection;

namespace TS.CodeGenerator
{
    public class AssemblyReader : IAssemblyReader
    {
        private TSGenerator _generator;
        private string _resolveDirectory;

        public AssemblyReader()
        {
            //AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            //AppDomain.CurrentDomain.SetupInformation.PrivateBinPath = dir;
            //AppDomain.CurrentDomain.AppendPrivatePath(_resolveDirectory);

            //   Assembly asm = Assembly.Load(.LoadFile(dllPath);//null;
            //foreach (var file in files)
            //{

            //    var tasm =  Assembly.LoadFile(file);
            //    if (fi.FullName == new FileInfo(file).FullName)
            //    {
            //        asm = tasm;
            //    }
            //}

            //var asm = Assembly.LoadFile(dllPath);
            //AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            _generator = new TSGenerator();
        }

        [Obsolete]
        public AssemblyReader(string dllPath)
        {
            if (!File.Exists(dllPath))
            {
                throw new Exception("DLL attempting to generate " + dllPath + " Does NOT EXIST");
            }

            _resolveDirectory = Path.GetDirectoryName(dllPath);
            var files = Directory.EnumerateFiles(_resolveDirectory, "*.dll");
            var fi = new FileInfo(dllPath);

            Assembly asm = Assembly.LoadFrom(fi.FullName);

            AddAssembly(asm);
        }

        public void AddAssembly(Assembly asm)
        {
            _generator.AddFollowAssembly(asm);

            foreach (var type in asm.GetExportedTypes())
            {
                if (type.GetTypeInfo().IsEnum)
                {
                    if (Settings.ConstEnumsEnabled)
                    {
                        _generator.AddEnumeration(type);
                    }
                    continue;
                }

                _generator.AddInterface(type);
            }
        }

        public Stream GenerateTypingsStream()
        {
            MemoryStream ms = new MemoryStream();
            StreamWriter sw = new StreamWriter(ms);

            sw.Write(_generator.ToTSString());
            sw.Flush();

            return ms;
        }

        public string GenerateTypingsString()
        {
            return _generator.ToTSString();
        }
    }
}