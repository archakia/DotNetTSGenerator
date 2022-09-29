using System;
using System.Collections.Generic;

namespace TS.CodeGenerator.MSBuildTasks
{
    public class OverrideSettings : ISettings
    {
        public string Indentation { get; set; }
        public string EndOfLine { get; set; }
        public string InterfaceFormat { get; set; }
        public string MethodReturnTypeFormatString { get; set; }
        public string DictionaryFormat { get; set; }
        public string PrependText { get; set; }
        public string PostpendText { get; set; }

        public bool? MakeMethodsOptional { get; set; }
        public bool? ConstEnumsEnabled { get; set; }

        public Dictionary<Type, string> StartingTypeMap { get; set; }
        public List<string> IgnoreInterfaces { get; set; }
    }
}