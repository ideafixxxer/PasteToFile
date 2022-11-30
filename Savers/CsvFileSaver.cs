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

        public bool IsSupported(IDataObject dataObject)
        {
            if (dataObject.GetDataPresent(DataFormats.CommaSeparatedValue)) return true;
            string text;
            if (dataObject.GetDataPresent(DataFormats.Text))
            {
                text = (string) dataObject.GetData(DataFormats.Text);
            }
            else if (dataObject.GetDataPresent(DataFormats.UnicodeText))
            {
                text = (string)dataObject.GetData(DataFormats.UnicodeText);
            }
            else
            {
                return false;
            }
            return ContainsTabSeparatedText(text) || ContainsSqlText(text);
        }

        private bool ContainsTabSeparatedText(string text)
        {
            using (var r = new StringReader(text))
            {
                var line1 = r.ReadLine();
                var line2 = r.ReadLine();
                var tabs = line1?.Count(c => c == '\t') ?? 0;
                if (tabs > 0 && line1 != null && line2 != null 
                    && tabs == line2.Count(c => c == '\t'))
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

        public void Save(IDataObject dataObject, string path)
        {
            string content = null;
            if (dataObject.GetDataPresent(DataFormats.CommaSeparatedValue))
                content = (string) dataObject.GetData(DataFormats.CommaSeparatedValue);
            else
            {
                string result = null;
                if (dataObject.GetDataPresent(DataFormats.Text))
                    result = (string)dataObject.GetData(DataFormats.Text);
                else if (dataObject.GetDataPresent(DataFormats.UnicodeText))
                    result = (string)dataObject.GetData(DataFormats.UnicodeText);
                if (!string.IsNullOrEmpty(result))
                {
                    if (ContainsTabSeparatedText(result))
                    {
                        content = ParseTabSeparatedValues(result);
                    }
                    if (ContainsSqlText(result))
                    {
                        content = ParseSqlTextResult(result);
                    }
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
