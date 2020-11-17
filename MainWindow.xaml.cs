using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PasteToFile.Savers;

namespace PasteToFile
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            var list = FileSavers.GetAllFileSavers().Where(s => s.IsSupported).Select(s => s.Name);
            TypesCombo.ItemsSource = list;
            SaveButton.IsEnabled = BrowseButton.IsEnabled = list.Any();
            if (TypesCombo.Items.Count > 0)
            {
                TypesCombo.SelectedIndex = 0;
            }
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            if (TypesCombo.SelectedItem == null) return;
            var saver = GetSaver();

            // Create OpenFileDialog
            var dlg = new Microsoft.Win32.SaveFileDialog
            {
                // Set filter for file extension and default file extension
                DefaultExt = saver.Extension,
                Filter = saver.Filter,
                FileName = FileNameTextBox.Text
            };

            // Display OpenFileDialog by calling ShowDialog method
            bool? result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                FileNameTextBox.Text = filename;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (TypesCombo.SelectedItem == null) return;

            string fileName = FileNameTextBox.Text;

            if (Path.GetDirectoryName(fileName) == string.Empty)
            {
                fileName = Path.Combine(Environment.CurrentDirectory, fileName);
            }

            if (File.Exists(fileName) 
                && MessageBox.Show("File exists. Overwrite?", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.Cancel)
            {
                return;
            }

            var saver = GetSaver();

            try
            {
                saver.Save(FileNameTextBox.Text);
                var result = MessageBox.Show("Do you want to open the file?", "File saved successfully", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Process.Start(new ProcessStartInfo(FileNameTextBox.Text) { UseShellExecute = true });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private IFileSaver GetSaver()
        {
            return FileSavers.ByName(TypesCombo.SelectedItem.ToString());
        }

        private void TypesCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TypesCombo.SelectedItem == null) return;
            var saver = GetSaver();

            if (Path.GetExtension(FileNameTextBox.Text) != saver.Extension)
            {
                FileNameTextBox.Text = Path.ChangeExtension(FileNameTextBox.Text, saver.Extension);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FileNameTextBox.Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), FileNameTextBox.Text);
        }
    }
}
