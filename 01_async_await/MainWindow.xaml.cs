using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace _01_async_await
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random random = new Random();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //async - allow method to use await keywords
            //await - wait task without freezing
       
            //task.Wait();//freeze
            ///int value = GenerateValue();
            //int value = await Task.Run(GenerateValue);
            //listBox.Items.Add(task.Result);   //freeze
            //listBox.Items.Add(value);   //freeze
            //listBox.Items.Add(await Task.Run(GenerateValue));   //freeze


            listBox.Items.Add(await GenerateValueAsync());
            GenerateValueAsync();
            GenerateValueAsync();
            GenerateValueAsync();
            //MessageBox.Show("Complete!");



        }

        Task<int> GenerateValueAsync()
        {
            return Task.Run(() =>
            {
                Thread.Sleep(random.Next(5000));
                //MessageBox.Show("Generate"); 
                return random.Next(1000);
            });
        }
        int GenerateValue()
        {
         
                Thread.Sleep(random.Next(5000));
                //MessageBox.Show("Generate"); 
                return random.Next(1000);
 
        }
    }
}
