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

        public bool IsSupported(IDataObject dataObject)
        {
            if (dataObject.GetDataPresent(DataFormats.Html)) return true;
            return false;
        }

        public string Filter => "HTML documents|*.htm";

        public void Save(IDataObject dataObject, string path)
        {
            var content = (string)dataObject.GetData(DataFormats.Html);
            File.WriteAllText(path, content);
        }
    }
}
