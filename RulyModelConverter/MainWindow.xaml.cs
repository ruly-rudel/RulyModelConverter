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
            ofd.Filter = "PMDファイル|*.pmd";
            if (ofd.ShowDialog() == true)
            {
                App.VM.InputFile = ofd.FileName;
            }
        }

        private void OutputButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PMFファイル|*.pmf";
            if (saveFileDialog.ShowDialog() == true)
            {
                App.VM.OutputFile = saveFileDialog.FileName;
            }

        }

        private async void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => App.VM.Convert());
            App.VM.LogStr += "Convert End.\n";
            return;
        }
    }
}
