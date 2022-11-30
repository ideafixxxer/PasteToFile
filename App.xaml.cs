using PasteToFile.Savers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PasteToFile
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            if (e.Args.Length > 2)
            {
                string command = e.Args[0];
                string source = e.Args[1];
                string target = e.Args[2];

                if (command == "convertsql")
                {
                    if (!File.Exists(source))
                    {
                        Console.WriteLine("File not found");
                        Shutdown(-1);
                    }
                    var parser = new SqlTextResultParser();
                    using (var reader = File.OpenText(source))
                    {
                        using (var writer = File.CreateText(target))
                        {
                            parser.Parse(reader, writer);
                        }
                    }
                    Shutdown();
                }
            }
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
        }
    }
}
