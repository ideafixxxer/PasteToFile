using System.IO;
using System.Windows;

namespace PasteToFile.Savers
{
    class TextFileSaver : IFileSaver
    {
        public string Name => "Text document (.txt)";

        public string Extension => ".txt";

        public bool IsSupported => Clipboard.ContainsText();

        public string Filter => "Text documents|*.txt";

        public void Save(string path)
        {
            var content = Clipboard.GetText();
            File.WriteAllText(path, content);
        }
    }
}
