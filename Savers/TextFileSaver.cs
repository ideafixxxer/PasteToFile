using System.IO;
using System.Windows;

namespace PasteToFile.Savers
{
    class TextFileSaver : IFileSaver
    {
        public string Name => "Text document (.txt)";

        public string Extension => ".txt";

        public bool IsSupported(IDataObject dataObject) => dataObject.GetDataPresent(DataFormats.Text) || dataObject.GetDataPresent(DataFormats.UnicodeText);

        public string Filter => "Text documents|*.txt";

        public void Save(IDataObject dataObject, string path)
        {
            var content = (string) dataObject.GetData(DataFormats.UnicodeText);
            File.WriteAllText(path, content);
        }
    }
}
