﻿// Copyright (c) 2022 Jonathan Lang
using System;
using System.Text;
using Baracuda.Monitoring.Internal.Utilities;

namespace Baracuda.Monitoring.Internal.Profiling
{
    internal static partial class ValueProcessorFactory
    {
        /*
         * Integers   
         */
        
        private static Func<int, string> Int32Processor(FormatData formatData)
        {
            var stringBuilder = new StringBuilder();
            var label = formatData.Label;
            var format = formatData.Format;
            if (format != null)
            {
                return (value) =>
                {
                    stringBuilder.Clear();
                    stringBuilder.Append(label);
                    stringBuilder.Append(": ");
                    stringBuilder.Append(value.ToString(format));
                    return stringBuilder.ToString();
                };
            }
            else
            {
                return (value) =>
                {
                    stringBuilder.Clear();
                    stringBuilder.Append(label);
                    stringBuilder.Append(": ");
                    stringBuilder.Append(value);
                    return stringBuilder.ToString();
                };
            }
        }

        private static Func<long, string> Int64Processor(FormatData formatData)
        {
            var stringBuilder = new StringBuilder();
            var label = formatData.Label;
            return (value) =>
            {
                stringBuilder.Clear();
                stringBuilder.Append(label);
                stringBuilder.Append(": ");
                stringBuilder.Append(value);
                return stringBuilder.ToString();
            };
        }

        /*
         * Floating Points   
         */

        private static Func<float, string> SingleProcessor(FormatData formatData)
        {
            var stringBuilder = new StringBuilder();
            var label = formatData.Label;
            return (value) =>
            {
                stringBuilder.Clear();
                stringBuilder.Append(label);
                stringBuilder.Append(": ");
                stringBuilder.Append(value.ToString("0.00"));
                return stringBuilder.ToString();
            };
        }

        private static Func<double, string> DoubleProcessor(FormatData formatData)
        {
            var stringBuilder = new StringBuilder();
            var label = formatData.Label;
            return (value) =>
            {
                stringBuilder.Clear();
                stringBuilder.Append(label);
                stringBuilder.Append(": ");
                stringBuilder.Append(value.ToString("0.00"));
                return stringBuilder.ToString();
            };
        }

        /*
         * Boolean   
         */
                
        private static Func<bool, string> CreateBooleanProcessor(FormatData formatData)
        {
            var trueString  =  $"{formatData.Label}: {trueColored}";
            var falseString =  $"{formatData.Label}: {falseColored}";
            return (value) => value ? trueString : falseString;
        }
    }
}