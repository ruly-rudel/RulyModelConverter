using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RulyModelConverter
{
    public class ViewModel : INotifyPropertyChanged
    {
        private string inputFile;
        public string InputFile { get { return inputFile; } set { inputFile = value; NotifyPropertyChanged();  } }

        private string outputFile;
        public string OutputFile { get { return outputFile; } set { outputFile = value; NotifyPropertyChanged();  } }

        private string logStr;
        public string LogStr { get { return logStr; } set { logStr = value; NotifyPropertyChanged();  } }


        private PMD pmd;
        private PMF pmf;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        internal void Convert()
        {
            pmd = new PMD(InputFile);
            pmd.SetupShellSurface();
            pmf = new PMF(pmd);
            pmf.Save(OutputFile);
        }
    }
}
