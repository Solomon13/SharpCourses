using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public TestWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var fileSelectionDialog = new OpenFileDialog();
            fileSelectionDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            fileSelectionDialog.Filter = "Bitmap|*.bmp";
            fileSelectionDialog.Multiselect = false;

            if (fileSelectionDialog.ShowDialog() == true)
            {
                PreviewImage.Source = new BitmapImage(new Uri(fileSelectionDialog.FileName));
                ImageFileText.Text = fileSelectionDialog.FileName;
            }
        }
    }
}
