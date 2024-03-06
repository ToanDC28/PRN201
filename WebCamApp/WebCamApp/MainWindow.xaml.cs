using AForge.Video;
using AForge.Video.DirectShow;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebCamApp.utils;

namespace WebCamApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            Closing += MainWindow_Closing;

        }

        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosed(e);
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
                //videoSource.Stop();
            }
            
        }

        //void MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{

        //    //if (listView.SelectedItem is FileItem selectedItem)
        //    //{
        //    //    string path = selectedItem.FilePath;
        //    //    cameraImage.Source = path;
        //    //}

        //}
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videoDevices.Count > 0)
            {
                videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
                videoSource.NewFrame += VideoSource_NewFrame;
                videoSource.Start();
            }
            else
            {
                System.Windows.MessageBox.Show("No camera devices found.");
            }

            
        }

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            using (Bitmap newFrame = (Bitmap)eventArgs.Frame.Clone())
            {
                this.Dispatcher.Invoke(() =>
                {
                    cameraImage.Source = Utils.BitmapToImageSource(newFrame);
                });
            }
        }

        private void CaptureButton_Click(object sender, RoutedEventArgs e)
        {
            string path = address.Text;
            if (videoSource != null && videoSource.IsRunning && path != "")
            {
                try
                {
                    BitmapSource bitmapSource = (BitmapSource)cameraImage.Source;
                    if (bitmapSource != null)
                    {
                        Bitmap currentFrame = BitmapSourceToBitmap(bitmapSource);

                        
                        string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
                        string fullPath = System.IO.Path.Combine(path, fileName);

                        currentFrame.Save(fullPath, System.Drawing.Imaging.ImageFormat.Png);
                        LoadImagesFromDirectory(path);

                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Failed to capture the frame.");
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("An error occurred while saving the image: " + ex.Message);
                }
            }
            else
            {
                System.Windows.MessageBox.Show("No video source is available.");
            }

        }
        private void LoadImagesFromDirectory(string directoryPath)
        {
            try
            {
                string[] imageFiles = Directory.GetFiles(directoryPath, "*.png");
                List<FileItem> loadedItems = new List<FileItem>();

                foreach (string filePath in imageFiles)
                {
                    string fileName = System.IO.Path.GetFileName(filePath);

                    FileItem fileItem = new FileItem
                    {
                        Image = filePath,
                        FileName = fileName,
                        FilePath = filePath
                    };

                    loadedItems.Add(fileItem);
                }

                loadedItems.AddRange(loadedItems);
                listView.ItemsSource = loadedItems;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("An error occurred while loading images: " + ex.Message);
            }
        }
        private Bitmap BitmapSourceToBitmap(BitmapSource bitmapSource)
        {
            Bitmap bitmap;
            using (MemoryStream stream = new MemoryStream())
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                encoder.Save(stream);
                bitmap = new Bitmap(stream);
            }
            return bitmap;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            var result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                String path = dialog.SelectedPath;
                address.Text = path;
                LoadImagesFromDirectory(path);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(listView.SelectedItem is FileItem selectedItem)
            {
                
            }
        }
    }
}
