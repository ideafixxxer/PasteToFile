using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace PasteToFile.Savers
{
    class JpegFileSaver : IFileSaver
    {
        public string Name => "Picture (.jpg)";

        public string Extension => ".jpg";

        public bool IsSupported => Clipboard.ContainsImage();

        public string Filter => "Picture|*.jpg";

        public void Save(string path)
        {
            var content = Clipboard.GetImage();
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                BitmapEncoder encoder = new JpegBitmapEncoder() { QualityLevel = 90 };
                encoder.Frames.Add(BitmapFrame.Create(content));
                encoder.Save(fileStream);
            }
        }
    }
}
