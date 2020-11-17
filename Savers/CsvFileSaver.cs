using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace PasteToFile.Savers
{
    class CsvFileSaver : IFileSaver
    {
        public string Name => "Comma separated (.csv)";

        public string Extension => ".csv";

        public bool IsSupported
        {
            get {
                if (Clipboard.ContainsText(TextDataFormat.CommaSeparatedValue)) return true;
                if (!Clipboard.ContainsText()) return false;
                var text = Clipboard.GetText();
                return ContainsTabSeparatedText(text) || ContainsSqlText(text);
            }
        }

        private bool ContainsTabSeparatedText(string text)
        {
            using (var r = new StringReader(text))
            {
                var line1 = r.ReadLine();
                var line2 = r.ReadLine();
                if (line1 != null && line2 != null && line1.Count(c => c == '\t') == line2.Count(c => c == '\t'))
                {
                    return true;
                }
                return false;
            }
        }

        private bool ContainsSqlText(string text)
        {
            using (var r = new StringReader(text))
            {
                var line1 = r.ReadLine();
                var line2 = r.ReadLine();
                if (line1 != null && line2 != null && SqlTextResultParser.IsDivider(line2))
                {
                    return true;
                }
                return false;
            }
        }

        public string Filter => "Comma separated|*.csv";

        public void Save(string path)
        {
            var content = Clipboard.GetText(TextDataFormat.CommaSeparatedValue);
            if (string.IsNullOrEmpty(content))
            {
                var result = Clipboard.GetText();
                if (ContainsTabSeparatedText(result))
                {
                    content = ParseTabSeparatedValues(result);
                }
                if (ContainsSqlText(result))
                {
                    content = ParseSqlTextResult(result);
                }
            }
            File.WriteAllText(path, content);
        }

        private static string ParseSqlTextResult(string result)
        {
            using (var reader = new StringReader(result))
            {
                using (var writer = new StringWriter())
                {
                    new SqlTextResultParser().Parse(reader, writer);
                    return writer.ToString();
                }
            }
        }

        private static string ParseTabSeparatedValues(string result)
        {
            return string.Join(Environment.NewLine, result.Split(Environment.NewLine)
                .Select(l => string.Join(',',
                    l.Split('\t').Select(v => v.Escape()))));
        }

    }
}
