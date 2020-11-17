using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PasteToFile.Savers
{
    class SqlTextResultParser
    {
        private int[] colSizes;
        private int[] colStarts;
        private int minLength;
        private int maxLength;

        public static bool IsDivider(string line2)
        {
            return line2 != null && line2.Distinct().SequenceEqual(new[] { '-', ' ' });
        }

        public void Parse(TextReader reader, TextWriter writer)
        {
            var line1 = reader.ReadLine();
            var line2 = reader.ReadLine();
            InitializeColumns(line2);
            var line = line1;
            ParseRows(reader, writer, line);
        }

        private void ParseRows(TextReader reader, TextWriter writer, string headers)
        {
            string line = headers;
            while (line != null)
            {
                var next = reader.ReadLine();

                if (!IsDivider(next))
                {
                    if (line.Length >= minLength && line.Length <= maxLength)
                    {
                        writer.WriteLine(string.Join(',', colSizes.Select((size, c) =>
                            line.Extract(colStarts[c], size).TrimEnd().Escape())));
                    }
                    else
                    {
                        writer.WriteLine(line);
                    }
                    line = next;
                }
                else
                {
                    InitializeColumns(next);
                    ParseRows(reader, writer, line);
                    break;
                }
            }
       }

        private void InitializeColumns(string line2)
        {
            colSizes = line2.Split(' ').Select(p => p.Length).ToArray();
            minLength = colSizes.Take(colSizes.Length - 1).Sum() + colSizes.Length;
            maxLength = line2.Length;
            int curIndex = 0;
            colStarts = Enumerable.Repeat(0, 1).Union(colSizes.Take(colSizes.Length - 1).Select(size => curIndex += size + 1)).ToArray();
        }
    }
}
