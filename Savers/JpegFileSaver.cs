using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace PasteToFile.Savers
{
    class JpegFileSaver : IFileSaver
    {
        public string Name => "Picture (.jpg)";

        public string Extension => ".jpg";

        public bool IsSupported(IDataObject dataObject) => dataObject.GetDataPresent(DataFormats.Bitmap);

        public string Filter => "Picture|*.jpg";

        public void Save(IDataObject dataObject, string path)
        {
            var content = (BitmapSource)dataObject.GetData(DataFormats.Bitmap);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                BitmapEncoder encoder = new JpegBitmapEncoder() { QualityLevel = 90 };
                encoder.Frames.Add(BitmapFrame.Create(content));
                encoder.Save(fileStream);
            }
        }
    }
}
