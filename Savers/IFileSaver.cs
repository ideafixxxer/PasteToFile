using System;
using System.Collections.Generic;
using System.Text;

namespace PasteToFile.Savers
{
    interface IFileSaver
    {
        string Name { get; }

        string Extension { get; }

        string Filter { get; }

        void Save(string path);

        bool IsSupported { get; }
    }
}
