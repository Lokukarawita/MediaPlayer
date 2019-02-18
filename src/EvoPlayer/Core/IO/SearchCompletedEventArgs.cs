using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoPlayer.Core.IO
{
    public class SearchCompletedEventArgs : SearchProgressEventArgs
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public Exception Error { get; set; }
        public int TotalDirectoryCount { get; set; }
    }
}
