using System;
using System.Collections.Generic;

namespace TS.CodeGenerator
{
    public static class Settings
    {
        static Settings()
        {
            MakeMethodsOptional = true;
            //FollowExternalAssemblies = false;
            EndOfLine = "\r\n";
            Indentation = "\t";
            InterfaceFormat = "I{0}";
            MethodReturnTypeFormatString = "JQueryPromise<{0}>";
            DictionaryFormat = "IDictionary<{0}, {1}>";
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
            if (!string.IsNullOrEmpty(settings.Indentation))
            {
                Indentation = settings.Indentation;
            }
            if (!string.IsNullOrEmpty(settings.MethodReturnTypeFormatString))
            {
                MethodReturnTypeFormatString = settings.MethodReturnTypeFormatString;
            }
            if (!string.IsNullOrEmpty(settings.InterfaceFormat))
            {
                InterfaceFormat = settings.InterfaceFormat;
            }
            if (!string.IsNullOrEmpty(settings.EndOfLine))
            {
                EndOfLine = settings.EndOfLine;
            }
            if (!string.IsNullOrEmpty(settings.DictionaryFormat))
            {
                DictionaryFormat = settings.DictionaryFormat;
            }
            if (!string.IsNullOrEmpty(settings.PrependText))
            {
                PrependText = settings.PrependText;
            }
            if (!string.IsNullOrEmpty(settings.PostpendText))
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
        }

        public static string PrependText { get; set; }
        public static string PostpendText { get; set; }
        public static string Indentation { get; set; }
        public static string MethodReturnTypeFormatString { get; set; }
        public static bool MakeMethodsOptional { get; set; }
        //  public static bool FollowExternalAssemblies { get; set; }
        public static Dictionary<Type, string> StartingTypeMap { get; set; }
        public static string EndOfLine { get; set; }
        public static bool ConstEnumsEnabled { get; set; }
        public static string InterfaceFormat { get; set; }
        public static List<string> IgnoreInterfaces { get; set; }
        public static string DictionaryFormat { get; set; }
    }
}
