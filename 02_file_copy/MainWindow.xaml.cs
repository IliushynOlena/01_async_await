using IOExtensions;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace _02_file_copy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       ViewModel viewModel ;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new ViewModel()
            {
                Source = @"C:\Users\helen\Desktop\Заняття\0_1.txt",
                Destination = @"C:\Users\helen\Desktop\TestFolder",
                Progress = 0
            };
            this.DataContext = viewModel;
        }

        private async void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            #region Type 1 Type 2
            // type 1 - using File class
            //C:\Users\helen\Desktop\TestFolder\0_1.txt
            string filename = Path.GetFileName(viewModel.Source);
            string destFilePath = Path.Combine(viewModel.Destination, filename);
            //File.Copy(Source, destFilePath, true);
            //MessageBox.Show("Complete!");
            //2
            await CopyFileAsync(viewModel.Source, destFilePath);
            MessageBox.Show("Complete!");
            //viewModel.Progress = 0;
            
            #endregion        

        }
        private Task CopyFileAsync(string src, string dest)
        {
            /*
            return Task.Run(() =>
            {
                // type 2- using FileStream class
                using FileStream streamSource = new FileStream(src, FileMode.Open, FileAccess.Read);
                using FileStream streamDest = new FileStream(dest, FileMode.Create, FileAccess.Write);

                byte[] buffer = new byte[1024 * 8];//8KB   //12
                int bytes = 0;
                do
                {
                    bytes = streamSource.Read(buffer, 0, buffer.Length);//0.5

                    streamDest.Write(buffer, 0, bytes);//8
                                                       //% = total  received 
                    float procent = streamDest.Length / (streamSource.Length / 100);
                    //model.Progress = procent;
                    viewModel.Progress = procent;

                } while (bytes > 0);              
            });
            */
            return FileTransferManager.CopyWithProgressAsync(src, dest, (progress) =>
            {
                viewModel.Progress = progress.Percentage;
            }, false);
           
        }

            private void OpenSourceBtn_Click(object sender, RoutedEventArgs e)
            {
                OpenFileDialog dialog = new OpenFileDialog();
                if (dialog.ShowDialog() == true)
                {
                   viewModel.Source = dialog.FileName;
                }
            }

            private void OpenDestBtn_Click(object sender, RoutedEventArgs e)
            {
                CommonOpenFileDialog dialog = new CommonOpenFileDialog();
                dialog.IsFolderPicker = true;
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                viewModel.Destination = dialog.FileName;
                }

            }
    }
    [AddINotifyPropertyChangedInterface]
    class ViewModel
    {
        public string Source { get; set; }
        public string Destination { get; set; }
        public double Progress { get; set; }
        public bool IsWaiting => Progress == 0;

    }

}
