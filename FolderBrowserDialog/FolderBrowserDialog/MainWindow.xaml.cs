using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FolderBrowserDialog
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

        private void AdjustTextBoxWidth(string path)
        {
            double pixelsPerCharacter = 6;

            double desiredWidth = path.Length * pixelsPerCharacter;
            if (desiredWidth > address.MinWidth)
            {
                address.Width = desiredWidth;
            }
            else
            {
                address.Width = address.MinWidth;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            var result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                List<Infor> infor = new List<Infor>();
                String path = dialog.SelectedPath;
                AdjustTextBoxWidth(path);
                address.Text = path;

                showInfor(path);
            }
        }
        void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            if (listView.SelectedItem is Infor selectedItem)
            {
                if(selectedItem.FileType == "Folder")
                {
                    string path = selectedItem.FilePath;
                    AdjustTextBoxWidth(path);
                    address.Text = path;
                    showInfor(path);
                }
                else
                {
                    string path = selectedItem.FilePath;
                    AdjustTextBoxWidth(path);
                    address.Text = path;
                }
            }

        }
        private void showInfor(String path)
        {
            var items = new DirectoryInfo(path).GetFileSystemInfos();
            List<Infor> infor = new List<Infor>();

            foreach (var item in items)
            {
                string type = item is DirectoryInfo ? "Folder" : "File";
                string name = item.Name;
                string filepath = item.FullName;
                infor.Add(new Infor { FileType = type, FileName = name, FilePath = filepath });
            }

            listView.Items.Clear();

            foreach (var item in infor)
            {
                if (item.FileType == "Folder")
                {
                    
                    listView.Items.Add(item);
                }
            }

            foreach (var item in infor)
            {
                if (item.FileType == "File")
                {
                    listView.Items.Add(item);
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if(address.Text.Length == 0)
            {
                return;
            }
            string newFolderName = "NewFolder";
            string newFolderPath = System.IO.Path.Combine(address.Text, newFolderName);
            System.IO.Directory.CreateDirectory(newFolderPath);
            showInfor(address.Text);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (address.Text.Length == 0)
            {
                return;
            }
            string newFileName = "file.txt";
            string newFilePath = System.IO.Path.Combine(address.Text, newFileName);
            System.IO.File.Create(newFilePath).Close();
            showInfor(address.Text);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            List<Infor> selectedItems = new List<Infor>();

            foreach (var selectedItem in listView.SelectedItems)
            {
                if (selectedItem is Infor selectedInfor)
                {
                    selectedItems.Add(selectedInfor);
                }
            }
            foreach (var selectedItem in selectedItems)
            {
                if (selectedItem.FileType == "Folder")
                {
                    System.IO.Directory.Delete(selectedItem.FilePath, true);
                }
                else if (selectedItem.FileType == "File")
                {
                    System.IO.File.Delete(selectedItem.FilePath);
                }
            }
            showInfor(address.Text);
        }
        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listView.SelectedItem != null)
            {
                Infor selectedFileItem = (Infor)listView.SelectedItem;

                editPopup.IsOpen = true;

                editTextBox.Text = selectedFileItem.FileName;
                editTextBox.Focus();
                editTextBox.SelectAll();
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if(listView.SelectedItem is Infor selectedItem)
            {
                if (selectedItem.FileType == "Folder")
                {
                    string newFilePath = System.IO.Path.Combine(address.Text, editTextBox.Text);
                    string oldFilePath = System.IO.Path.Combine(address.Text, selectedItem.FileName);
                    System.IO.File.Move(oldFilePath, newFilePath);
                }
                else
                {
                    string newFilePath = System.IO.Path.Combine(address.Text, editTextBox.Text);
                    string oldFilePath = System.IO.Path.Combine(address.Text, selectedItem.FileName);
                    System.IO.File.Move(oldFilePath, newFilePath);
                }
            }

        }
    }
}