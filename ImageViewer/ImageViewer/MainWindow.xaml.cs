using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Runtime.InteropServices;
using ImageViewer.ThomasLevesque;

namespace ImageViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            CommandBindings.Add(
                new CommandBinding(ApplicationCommands.Paste, PasteExecuted, CanPaste));
            CommandBindings.Add(
                new CommandBinding(ApplicationCommands.Stop, StopExecuted, CanStop));
            Focus();
        }

        private void PasteExecuted(object sender, ExecutedRoutedEventArgs e)
        {
        }

        private void CanPaste(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Clipboard.ContainsImage();
        }

        private void StopExecuted(object sender, ExecutedRoutedEventArgs e)
        {
        }

        private void CanStop(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void LoadImage(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    BitmapImage newImage = new BitmapImage(new Uri(filePath));
                    image.Source = newImage;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, filePath);
            }
        }

        private void ImageGrid_DragEnter(object sender, DragEventArgs e)
        {
            var d = e.Data.GetDataPresent(DataFormats.FileDrop);
        }

        private void ImageGrid_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                LoadImage(files[0]);
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mItem = sender as MenuItem;
            if (mItem.Header.Equals("Paste"))
            {
                if (Clipboard.GetDataObject().GetDataPresent("Bitmap"))
                {
                    object o = Clipboard.GetDataObject().GetData("Bitmap");
                    if (o != null)
                    {
                        var s = ClipboardImages.ImageFromClipboardDib();
                        image.Source = s;
                    }
                }
            }
            if (mItem.Header.Equals("Exit"))
            {
                Application.Current.Shutdown();
            }
        }


    }
}
