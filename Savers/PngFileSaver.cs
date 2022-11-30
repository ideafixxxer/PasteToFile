using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace PasteToFile.Savers
{
    class PngFileSaver : IFileSaver
    {
        public string Name => "Picture (.png)";

        public string Extension => ".png";

        public bool IsSupported(IDataObject dataObject) => dataObject.GetDataPresent(DataFormats.Bitmap);

        public string Filter => "Picture|*.png";

        public void Save(IDataObject dataObject, string path)
        {
            var content = (BitmapSource) dataObject.GetData(DataFormats.Bitmap);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(content));
                encoder.Save(fileStream);
            }
        }
    }
}
