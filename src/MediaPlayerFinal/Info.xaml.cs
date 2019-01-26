// Decompiled with JetBrains decompiler
// Type: MediaPlayerFinal.Info
// Assembly: MediaPlayerFinal, Version=1.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: FA266A67-626E-46F0-81D3-A2E3F3CE1F98
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\MediaPlayerFinal.exe

using RadioDesk.Core;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MediaPlayerFinal
{
    public partial class Info : Window, IComponentConnector
    {
        private TagInfo tg = new TagInfo();
        private string track;
        private string artist;
        private string album;
        private DataSet dsTrack;
        private DataSet dsAlbum;
        private DataSet dsArtist;
        //internal TabControl tabControl1;
        //internal TabItem tabItem1;
        //internal GroupBox groupBox1;
        //internal Grid grid1;
        //internal Label label1;
        //internal Label label2;
        //internal Label label3;
        //internal Label label4;
        //internal Label label5;
        //internal Label label6;
        //internal Label label7;
        //internal TextBox txtBitRate;
        //internal TextBox txtLength;
        //internal TextBox txtYear;
        //internal TextBox txtGenre;
        //internal TextBox txtTrackAlbum;
        //internal TextBox txtTrackArtist;
        //internal TextBox txtTitle;
        //internal System.Windows.Controls.Image imgAlbumArt;
        //internal Grid grid2;
        //internal GroupBox groupBox2;
        //internal Label label8;
        //internal TextBox txtArtistName;
        //internal Label label9;
        //internal TextBox txtArtistDOB;
        //internal Label label10;
        //internal TextBox txtArtistAddr;
        //internal Label label11;
        //internal TextBox txtArtistTel;
        //internal Label label12;
        //internal TextBox txtArtistEmail;
        //internal GroupBox groupBox3;
        //internal Label label13;
        //internal Label label14;
        //internal Label label15;
        //internal Label label16;
        //internal TextBox txtAlbumTitle;
        //internal TextBox txtAlbumArtist;
        //internal TextBox txtAlbumYear;
        //internal TextBox txtAlbumStudio;
        //internal Button btnClose;
        //private bool _contentLoaded;

        public Info()
        {
            this.InitializeComponent();
        }

        public Info(string track, string artist, string album)
        {
            this.InitializeComponent();
            this.track = track;
            this.artist = artist;
            this.album = album;
            this.dsArtist = ExtendedQuerying.GetArtistDetails(artist);
            this.dsTrack = ExtendedQuerying.GetTrackDetails(track, artist);
            this.dsAlbum = ExtendedQuerying.GetAlbumDetails(album, artist);
            this.SetTrackInfoFields();
            this.SetArtistInfoFields();
            this.SetAlbumInfoFields();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ResizeMode = ResizeMode.NoResize;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SetTrackInfoFields()
        {
            if (this.dsTrack == null)
                return;
            if (this.dsTrack.Tables[0].Rows.Count != 1)
                return;
            try
            {
                this.tg.File = this.dsTrack.Tables[0].Rows[0]["location"].ToString();
                this.txtTitle.Text = this.dsTrack.Tables[0].Rows[0]["song_name"].ToString();
                this.txtTrackArtist.Text = this.dsTrack.Tables[0].Rows[0]["artist"].ToString();
                this.txtTrackAlbum.Text = this.dsTrack.Tables[0].Rows[0]["album"].ToString();
                this.txtGenre.Text = this.dsTrack.Tables[0].Rows[0]["genre"].ToString();
                this.txtYear.Text = this.dsTrack.Tables[0].Rows[0]["song_year"].ToString();
                int num = (int)this.dsTrack.Tables[0].Rows[0]["bit_rate"];
                this.txtBitRate.Text = num.ToString() + " " + (num < 1000 ? " kbps" : " Mbps");
                this.txtLength.Text = RadioDeskUtils.FormatToTime(uint.Parse(this.dsTrack.Tables[0].Rows[0]["length"].ToString()));
                this.imgAlbumArt.Source = this.tg.FrontCover == null ? (ImageSource)new BitmapImage(new Uri("/MediaPlayerFinal;component/Images/music.png", UriKind.Relative)) : (ImageSource)this.ConvertToWPFImageSource(this.tg.FrontCover);
            }
            catch (TagReadException ex)
            {
            }
            catch (Exception ex)
            {
            }
        }

        private void SetArtistInfoFields()
        {
            try
            {
                if (this.dsArtist == null || this.dsArtist.Tables[0].Rows.Count != 1)
                    return;
                this.txtArtistName.Text = this.dsArtist.Tables[0].Rows[0]["artist_name"].ToString();
                this.txtArtistTel.Text = this.dsArtist.Tables[0].Rows[0]["telephone"].ToString();
                this.txtArtistEmail.Text = this.dsArtist.Tables[0].Rows[0]["email"].ToString();
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine(this.dsArtist.Tables[0].Rows[0]["address_L1"].ToString());
                stringBuilder.AppendLine(this.dsArtist.Tables[0].Rows[0]["address_L2"].ToString());
                stringBuilder.AppendLine(this.dsArtist.Tables[0].Rows[0]["address_L3"].ToString());
                this.txtArtistAddr.Text = stringBuilder.ToString();
                this.txtArtistDOB.Text = Convert.ToDateTime(this.dsArtist.Tables[0].Rows[0]["DOB"].ToString(), (IFormatProvider)CultureInfo.CurrentCulture).ToString("yyyy MMMM d");
            }
            catch (Exception ex)
            {
            }
        }

        private void SetAlbumInfoFields()
        {
            if (this.dsAlbum == null)
                return;
            if (this.dsAlbum.Tables[0].Rows.Count != 1)
                return;
            try
            {
                this.txtAlbumTitle.Text = this.dsAlbum.Tables[0].Rows[0]["album_name"].ToString();
                this.txtAlbumArtist.Text = this.dsAlbum.Tables[0].Rows[0]["artist"].ToString();
                this.txtAlbumYear.Text = this.dsAlbum.Tables[0].Rows[0]["album_year"].ToString();
                this.txtAlbumStudio.Text = this.dsAlbum.Tables[0].Rows[0]["studio"].ToString();
            }
            catch (Exception ex)
            {
            }
        }

        private BitmapImage ConvertToWPFImageSource(System.Drawing.Image SourceImg)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            MemoryStream memoryStream = new MemoryStream();
            this.tg.FrontCover.Save((Stream)memoryStream, ImageFormat.Bmp);
            memoryStream.Seek(0L, SeekOrigin.Begin);
            bitmapImage.StreamSource = (Stream)memoryStream;
            bitmapImage.EndInit();
            return bitmapImage;
        }

        //[DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //        return;
        //    this._contentLoaded = true;
        //    Application.LoadComponent((object)this, new Uri("/MediaPlayerFinal;component/info.xaml", UriKind.Relative));
        //}

        //[DebuggerNonUserCode]
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //        case 1:
        //            ((FrameworkElement)target).Loaded += new RoutedEventHandler(this.Window_Loaded);
        //            break;
        //        case 2:
        //            this.tabControl1 = (TabControl)target;
        //            break;
        //        case 3:
        //            this.tabItem1 = (TabItem)target;
        //            break;
        //        case 4:
        //            this.groupBox1 = (GroupBox)target;
        //            break;
        //        case 5:
        //            this.grid1 = (Grid)target;
        //            break;
        //        case 6:
        //            this.label1 = (Label)target;
        //            break;
        //        case 7:
        //            this.label2 = (Label)target;
        //            break;
        //        case 8:
        //            this.label3 = (Label)target;
        //            break;
        //        case 9:
        //            this.label4 = (Label)target;
        //            break;
        //        case 10:
        //            this.label5 = (Label)target;
        //            break;
        //        case 11:
        //            this.label6 = (Label)target;
        //            break;
        //        case 12:
        //            this.label7 = (Label)target;
        //            break;
        //        case 13:
        //            this.txtBitRate = (TextBox)target;
        //            break;
        //        case 14:
        //            this.txtLength = (TextBox)target;
        //            break;
        //        case 15:
        //            this.txtYear = (TextBox)target;
        //            break;
        //        case 16:
        //            this.txtGenre = (TextBox)target;
        //            break;
        //        case 17:
        //            this.txtTrackAlbum = (TextBox)target;
        //            break;
        //        case 18:
        //            this.txtTrackArtist = (TextBox)target;
        //            break;
        //        case 19:
        //            this.txtTitle = (TextBox)target;
        //            break;
        //        case 20:
        //            this.imgAlbumArt = (System.Windows.Controls.Image)target;
        //            break;
        //        case 21:
        //            this.grid2 = (Grid)target;
        //            break;
        //        case 22:
        //            this.groupBox2 = (GroupBox)target;
        //            break;
        //        case 23:
        //            this.label8 = (Label)target;
        //            break;
        //        case 24:
        //            this.txtArtistName = (TextBox)target;
        //            break;
        //        case 25:
        //            this.label9 = (Label)target;
        //            break;
        //        case 26:
        //            this.txtArtistDOB = (TextBox)target;
        //            break;
        //        case 27:
        //            this.label10 = (Label)target;
        //            break;
        //        case 28:
        //            this.txtArtistAddr = (TextBox)target;
        //            break;
        //        case 29:
        //            this.label11 = (Label)target;
        //            break;
        //        case 30:
        //            this.txtArtistTel = (TextBox)target;
        //            break;
        //        case 31:
        //            this.label12 = (Label)target;
        //            break;
        //        case 32:
        //            this.txtArtistEmail = (TextBox)target;
        //            break;
        //        case 33:
        //            this.groupBox3 = (GroupBox)target;
        //            break;
        //        case 34:
        //            this.label13 = (Label)target;
        //            break;
        //        case 35:
        //            this.label14 = (Label)target;
        //            break;
        //        case 36:
        //            this.label15 = (Label)target;
        //            break;
        //        case 37:
        //            this.label16 = (Label)target;
        //            break;
        //        case 38:
        //            this.txtAlbumTitle = (TextBox)target;
        //            break;
        //        case 39:
        //            this.txtAlbumArtist = (TextBox)target;
        //            break;
        //        case 40:
        //            this.txtAlbumYear = (TextBox)target;
        //            break;
        //        case 41:
        //            this.txtAlbumStudio = (TextBox)target;
        //            break;
        //        case 42:
        //            this.btnClose = (Button)target;
        //            this.btnClose.Click += new RoutedEventHandler(this.btnClose_Click);
        //            break;
        //        default:
        //            this._contentLoaded = true;
        //            break;
        //    }
        //}
    }
}
