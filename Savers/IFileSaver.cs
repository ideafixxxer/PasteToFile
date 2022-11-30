using System.Windows;

namespace PasteToFile.Savers
{
    interface IFileSaver
    {
        string Name { get; }

        string Extension { get; }

        string Filter { get; }

        void Save(IDataObject dataObject, string path);

        bool IsSupported(IDataObject dataObject);
    }
}
