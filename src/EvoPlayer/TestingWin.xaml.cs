using EvoPlayer.Core.Data;
using EvoPlayer.Core.Ops;
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
using System.Windows.Shapes;

namespace EvoPlayer
{
    /// <summary>
    /// Interaction logic for TestingWin.xaml
    /// </summary>
    public partial class TestingWin : Window
    {
        public TestingWin()
        {
            InitializeComponent();

            var np = new Core.Data.Domain.Playlist();
            np.Entries.Add(new Core.Data.Domain.PlaylistEntry()
            {
                StorageType = Core.Data.Domain.MediaStorageType.Remote,
                Path = "http://192.168.1.38:32469/object/c707e4a95144a831aa54/file.mp3"
            });

            var player = new Core.Ops.libZPlay.LibZPlayPlayer();
            player.Playlist.ChangePlaylist(np);
            player.Play();
            //player.Playlist.


        }
    }
}
