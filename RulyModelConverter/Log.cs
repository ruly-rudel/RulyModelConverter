using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RulyModelConverter
{
    public class Log
    {
        public static void Debug(string tag, string data)
        {
            App.VM.LogStr += "[" + tag + "] " + data + "\n";
        }
    }
}
