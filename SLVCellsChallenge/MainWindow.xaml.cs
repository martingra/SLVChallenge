using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace SLVCellsChallenge
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Image<Bgr, byte> _originalImage;

        public MainWindow()
        {
            InitializeComponent();
            _originalImage = new Image<Bgr, byte>(200, 200, new Bgr(255, 255, 255)); // only for intialization.
        }

        private void LoadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.png;*.bmp"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                _originalImage = new Image<Bgr, byte>(openFileDialog.FileName);
                DisplayImage(_originalImage, OriginalImage);
            }
        }

        private void ProcessImage_Click(object sender, RoutedEventArgs e)
        {
            if (_originalImage == null)
            {
                MessageBox.Show("Please load an image first.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var processedImage = _originalImage.Clone();
            // TODO: Implement cell detection logic here

            DisplayImage(processedImage, ProcessedImage);
            ResultLabel.Content = "The number of cells goes here";
        }

        private static void DisplayImage(Image<Bgr, byte> image, Image imageControl)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                image.ToBitmap().Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = memory;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();

                imageControl.Source = bitmap;
            }
        }
    }
}