using System;
using System.Collections.Generic;
using System.Text;

namespace PasteToFile.Savers
{
    static class FileSavers
    {
        public static IFileSaver TextFileSaver { get; } = new TextFileSaver();
        public static IFileSaver CsvFileSaver { get; } = new CsvFileSaver();
        public static IFileSaver HtmlFileSaver { get; } = new HtmlFileSaver();
        public static IFileSaver JpegFileSaver { get; } = new JpegFileSaver();
        public static IFileSaver PngFileSaver { get; } = new PngFileSaver();

        private static readonly IDictionary<string, IFileSaver> AllSavers = new SortedDictionary<string, IFileSaver>()
        {
            { TextFileSaver.Name, TextFileSaver },
            { CsvFileSaver.Name, CsvFileSaver },
            { HtmlFileSaver.Name, HtmlFileSaver },
            { JpegFileSaver.Name, JpegFileSaver },
            { PngFileSaver.Name, PngFileSaver },
        };

        public static IFileSaver ByName(string name)
        {
            return AllSavers.TryGetValue(name, out IFileSaver result) ? result : null;
        }

        public static IEnumerable<IFileSaver> GetAllFileSavers()
        {
            return AllSavers.Values;
        }
    }
}
