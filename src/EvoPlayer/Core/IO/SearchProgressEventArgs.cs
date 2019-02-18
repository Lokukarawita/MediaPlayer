using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoPlayer.Core.IO
{
    public class SearchProgressEventArgs : EventArgs
    {
        public SearchProgressEventArgs()
        {
            FoundFiles = new List<string>();
            FailedDirectories = new List<string>();
        }

        public string CurrentDirectory { get; set; }
        public List<string> FoundFiles { get; set; }
        public List<string> FailedDirectories { get; set; }
        public int RemainingDirectoryCount { get; set; }
    }
}
