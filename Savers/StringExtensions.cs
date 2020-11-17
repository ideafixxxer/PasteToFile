using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PasteToFile.Savers
{
    static class StringExtensions
    {
        public static string Extract(this string line, int start, int size)
        {
            return new string(line.Skip(start).Take(size).ToArray());
        }


        public static string Escape(this string v)
        {
            return v.Contains(',') || v.Contains(Environment.NewLine)
                ? "\"" + v.Replace("\"", "\\\"") + "\""
                : v;
        }
    }
}
