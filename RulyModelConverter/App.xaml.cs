using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RulyModelConverter
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private static ViewModel viewModel = new ViewModel();
        public static ViewModel VM
        {
            get
            {
                return viewModel;
            }

            private set
            {
                viewModel = value;
            }
        }
    }
}
