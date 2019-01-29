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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NewPaint
{
    public partial class ExportWindow : Window
    {
        public ExportWindow()
        {
            InitializeComponent();
            pathBox.Text = AppDomain.CurrentDomain.BaseDirectory;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Serializator.Serialize(nameBox.Text + ".xml");
            }
            catch
            {
                MessageBox.Show("Некорректный файл");
            }
        }

        private void LoadBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Serializator.Deserialize(nameBox.Text + ".xml");
            }
            catch
            {
                MessageBox.Show("Данный файл не существует");
            }
        }

        private void SvgBtn_Click(object sender, RoutedEventArgs e)
        {
            Converter.ConvertToSVG(nameBox.Text + ".html");
        }

        private void PngBtn_Click(object sender, RoutedEventArgs e)
        {
            Converter.ConvertToPNG(nameBox.Text + ".png");
        }
    }
}
