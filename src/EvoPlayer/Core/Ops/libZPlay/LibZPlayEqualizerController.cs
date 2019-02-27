using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoPlayer.Core.Ops.libZPlay
{
    public class LibZPlayEqualizerController : IEqualizerController
    {
        #region Static
        private static int[] DEFAULT_BAND_POINTS = new int[] { 115, 240, 455, 800, 2000, 4500, 9000, 13000, 15000 };

        public static List<EQBand> GetDefaultBands()
        {
            return DEFAULT_BAND_POINTS
                .Select(x => new EQBand()
                {
                    Index = Array.IndexOf(DEFAULT_BAND_POINTS, x),
                    Name = $"{x} Hz",
                    Value = 0
                })
                .ToList();
        }

        #endregion

        private Lib.ZPlay _zply;

        public LibZPlayEqualizerController(Lib.ZPlay zplay)
        {
            this._zply = zplay;
        }

        private Lib.TStreamStatus GetStatus()
        {
            Lib.TStreamStatus status = default(Lib.TStreamStatus);
            _zply.GetStatus(ref status);
            return status;
        }

        public List<EQBand> GetBand()
        {
            int preamp = 0;
            int[] bandsVals = new int[DEFAULT_BAND_POINTS.Length];
            _zply.GetEqualizerParam(ref preamp, ref bandsVals);

            var bands = GetDefaultBands();
            bands.ForEach(x => { x.Value = bandsVals[x.Index]; });
            return bands; 
        }

        public List<EQBand> GetBand(string name)
        {
            var bands = GetBand();
            return bands.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();
        }

        public EQBand GetBand(int bandIndex)
        {
            var bands = GetBand();
            if(bandIndex >-1 && bandIndex < bands.Count)
            {
                return bands[bandIndex];
            }
            else
            {
                throw new IndexOutOfRangeException($"{nameof(bandIndex)} value {bandIndex} is outside the specified band list");
            }
        }

        public void SetBand(int bandIndex, double value)
        {
            var bands = GetBand();
            if (bandIndex > -1 && bandIndex < bands.Count)
            {
                _zply.SetEqualizerBandGain(bandIndex, (int)value);
            }
            else
            {
                throw new IndexOutOfRangeException($"{nameof(bandIndex)} value {bandIndex} is outside the specified band list");
            }
        }

        public bool Enabled
        {
            get
            {
                return GetStatus().fEqualizer;
            }
            set
            {
                _zply.EnableEqualizer(value);
            }
        }
    }
}
