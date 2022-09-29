using System;
using System.Collections.Generic;

namespace TS.CodeGenerator
{
    public static class Settings
    {
        public static string Indentation { get; set; }
        public static string EndOfLine { get; set; }
        public static string InterfaceFormat { get; set; }
        public static string MethodReturnTypeFormatString { get; set; }
        public static string DictionaryFormat { get; set; }
        public static string PrependText { get; set; }
        public static string PostpendText { get; set; }

        public static bool MakeMethodsOptional { get; set; }
        public static bool ConstEnumsEnabled { get; set; }

        public static Dictionary<Type, string> StartingTypeMap { get; set; }
        public static List<string> IgnoreInterfaces { get; set; }

        static Settings()
        {
            Indentation = "\t";
            EndOfLine = "\r\n";
            InterfaceFormat = "I{0}";
            MethodReturnTypeFormatString = "JQueryPromise<{0}>";
            DictionaryFormat = "IDictionary<{0}, {1}>";
            PrependText = String.Empty;
            PostpendText = String.Empty;

            MakeMethodsOptional = true;
            ConstEnumsEnabled = false;

            StartingTypeMap = new Dictionary<Type, string>
            {
                {typeof (string), Types.String},
                {typeof (DateTime), Types.String},
                {typeof (DateTime?), Types.String},
                {typeof (Guid), Types.String},
                {typeof (Guid?), Types.String},
                {typeof (bool), Types.Boolean},
                {typeof (bool?), Types.Boolean},
                {typeof (Double),Types.Number},
                {typeof (Int16),Types.Number},
                {typeof (Int32),Types.Number},
                {typeof (Int64),Types.Number},
                {typeof (UInt16),Types.Number},
                {typeof (UInt32),Types.Number},
                {typeof (UInt64),Types.Number},
                {typeof (Decimal),Types.Number},
                {typeof (Byte),Types.Number},
                {typeof (SByte),Types.Number},
                {typeof (Single),Types.Number},
                {typeof (Double?),Types.Number},
                {typeof (Int16?),Types.Number},
                {typeof (Int32?),Types.Number},
                {typeof (Int64?),Types.Number},
                {typeof (UInt16?),Types.Number},
                {typeof (UInt32?),Types.Number},
                {typeof (UInt64?),Types.Number},
                {typeof (Decimal?),Types.Number},
                {typeof (Byte?),Types.Number},
                {typeof (SByte?),Types.Number},
                {typeof (Single?),Types.Number},
                {typeof (void), Types.Void}
            };
            IgnoreInterfaces = new List<string>();
        }

        public static void OverwriteDefaults(ISettings settings)
        {
            if (settings.Indentation != null)
            {
                Indentation = settings.Indentation;
            }
            if (settings.EndOfLine != null)
            {
                EndOfLine = settings.EndOfLine;
            }
            if (settings.InterfaceFormat != null)
            {
                InterfaceFormat = settings.InterfaceFormat;
            }
            if (settings.MethodReturnTypeFormatString != null)
            {
                MethodReturnTypeFormatString = settings.MethodReturnTypeFormatString;
            }
            if (settings.DictionaryFormat != null)
            {
                DictionaryFormat = settings.DictionaryFormat;
            }
            if (settings.PrependText != null)
            {
                PrependText = settings.PrependText;
            }
            if (settings.PostpendText != null)
            {
                PostpendText = settings.PostpendText;
            }

            if (settings.MakeMethodsOptional.HasValue)
            {
                MakeMethodsOptional = settings.MakeMethodsOptional.Value;
            }
            if (settings.ConstEnumsEnabled.HasValue)
            {
                ConstEnumsEnabled = settings.ConstEnumsEnabled.Value;
            }

            if (settings.StartingTypeMap != null)
            {
                foreach (var key in settings.StartingTypeMap.Keys)
                {
                    StartingTypeMap[key] = settings.StartingTypeMap[key];
                }
            }
            if (settings.IgnoreInterfaces != null)
            {
                IgnoreInterfaces = settings.IgnoreInterfaces;
            }
        }
    }
}
