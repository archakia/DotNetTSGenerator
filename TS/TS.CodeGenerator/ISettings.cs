using System;
using System.Collections.Generic;

namespace TS.CodeGenerator
{
    public interface ISettings
    {
        string Indentation { get; set; }
        string EndOfLine { get; set; }
        string InterfaceFormat { get; set; }
        string MethodReturnTypeFormatString { get; set; }
        string DictionaryFormat { get; set; }
        string PrependText { get; set; }
        string PostpendText { get; set; }

        bool? MakeMethodsOptional { get; set; }
        bool? ConstEnumsEnabled { get; set; }

        Dictionary<Type, string> StartingTypeMap { get; set; }
        List<string> IgnoreInterfaces { get; set; }
    }
}
