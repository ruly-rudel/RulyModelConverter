using Microsoft.Win32;
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

namespace RulyModelConverter
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = App.VM;
        }

        private void InputButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "";
            ofd.DefaultExt = "*.pmd";
            ofd.FilterIndex = 1;
            ofd.Filter = "PMDファイル|*.pmd";
            if (ofd.ShowDialog() == true)
            {
                App.VM.InputFile = ofd.FileName;
            }
        }

        private void OutputButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "*.pmf";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.Filter = "PMFファイル|*.pmf";
            bool? result = saveFileDialog.ShowDialog();
            if (result == true)
            {
                App.VM.OutputFile = saveFileDialog.SafeFileName;
            }

        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            App.VM.Convert();
            return;
        }
    }
}
