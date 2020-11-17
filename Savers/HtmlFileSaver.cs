using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace PasteToFile.Savers
{
    class HtmlFileSaver : IFileSaver
    {
        public string Name => "HTML document (.htm)";

        public string Extension => ".htm";

        public bool IsSupported => Clipboard.ContainsText(TextDataFormat.Html);

        public string Filter => "HTML documents|*.htm";

        public void Save(string path)
        {
            var content = Clipboard.GetText(TextDataFormat.Html);
            File.WriteAllText(path, content);
        }
    }
}
