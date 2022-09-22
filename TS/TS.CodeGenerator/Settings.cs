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

        public static void OverwriteDefaults(ISettings set)
        {
            if (!string.IsNullOrEmpty(set.Indentation))
            {
                Indentation = set.Indentation;
            }
            if (!string.IsNullOrEmpty(set.MethodReturnTypeFormatString))
            {
                MethodReturnTypeFormatString = set.MethodReturnTypeFormatString;
            }
            if (!string.IsNullOrEmpty(set.InterfaceFormat))
            {
                InterfaceFormat = set.InterfaceFormat;
            }
            if (!string.IsNullOrEmpty(set.EndOfLine))
            {
                EndOfLine = set.EndOfLine;
            }
            if (!string.IsNullOrEmpty(set.DictionaryFormat))
            {
                DictionaryFormat = set.DictionaryFormat;
            }
            if (!string.IsNullOrEmpty(set.PrependText))
            {
                PrependText = set.PrependText;
            }
            if (!string.IsNullOrEmpty(set.PostpendText))
            {
                PostpendText = set.PostpendText;
            }

            if (set.MakeMethodsOptional.HasValue)
            {
                MakeMethodsOptional = set.MakeMethodsOptional.Value;
            }
            if (set.ConstEnumsEnabled.HasValue)
            {
                ConstEnumsEnabled = set.ConstEnumsEnabled.Value;
            }
        }

public static string PrependText{get;set;}
public static string PostpendText{get;set;}
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
