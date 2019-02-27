using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoPlayer.Core.Ops
{
    public interface IEqualizerController
    {

        List<EQBand> GetBand();
        EQBand GetBand(int bandIndex);
        List<EQBand> GetBand(string name);
        void SetBand(int bandIndex, double value);
 
        bool Enabled { get; set; }
    }
}
