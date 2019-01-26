    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFRoman.LogerImplementation
{
    class LogEventArgs : EventArgs
    {
        public LogEventArgs(string mss)
        {
            Mss = mss;
        }

        public string Mss { get; }
    }
}
