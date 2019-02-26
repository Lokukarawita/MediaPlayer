using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoPlayer.Core.Player
{
    public interface IPlaylistControl<T>
    {
        void Add(T[] entries);
        void Remove(T[] entries);
        void Clear();
        T Next(bool shuffled);
        bool IsEmpty { get; }
    }
}
