// Decompiled with JetBrains decompiler
// Type: MediaPlayerFinal.FFTGraph
// Assembly: MediaPlayerFinal, Version=1.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: FA266A67-626E-46F0-81D3-A2E3F3CE1F98
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\MediaPlayerFinal.exe

using RadioDesk.Core;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Timers;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Markup;

namespace MediaPlayerFinal
{
    public partial class FFTGraph : Window, IComponentConnector
    {
        private RadioDeskFFTGraph fft;
        private System.Timers.Timer tmr1;
        //internal System.Windows.Controls.ContextMenu mnuMain;
        //internal System.Windows.Controls.MenuItem mnuSacle;
        //internal WindowsFormsHost windowsFormsHost1;
        //internal PictureBox FFTGraphPicture;
        //private bool _contentLoaded;

        public FFTGraph()
        {
            this.InitializeComponent();
        }

        public FFTGraph(RadioDeskPlayer ply)
        {
            this.InitializeComponent();
            this.fft = new RadioDeskFFTGraph(ply);
            this.fft.SetControlToDraw(this.FFTGraphPicture.Handle, 0, 0, this.FFTGraphPicture.Width, this.FFTGraphPicture.Height, false);
            this.fft.ScaleDistribution = FFTScaleDistribution.Linear;
            this.fft.GraphStyle = FFTGraphStyle.Spectrum;
            this.tmr1 = new System.Timers.Timer(20.0);
            this.tmr1.Elapsed += new ElapsedEventHandler(this.tmr1_Elapsed);
            this.tmr1.Start();
        }

        private void tmr1_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.fft.Draw();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.windowsFormsHost1.Width = e.NewSize.Width - -22.0;
            this.windowsFormsHost1.Height = e.NewSize.Height - 39.0;
            this.FFTGraphPicture.Width = (int)this.windowsFormsHost1.Width - 38;
            this.FFTGraphPicture.Height = (int)this.windowsFormsHost1.Height;
            this.tmr1.Stop();
            this.fft.SetControlToDraw(this.FFTGraphPicture.Handle, 0, 0, this.FFTGraphPicture.Width, this.FFTGraphPicture.Height, false);
            this.tmr1.Start();
        }

        //[DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //        return;
        //    this._contentLoaded = true;
        //    System.Windows.Application.LoadComponent((object)this, new Uri("/MediaPlayerFinal;component/fftgraph.xaml", UriKind.Relative));
        //}

        //[EditorBrowsable(EditorBrowsableState.Never)]
        //[DebuggerNonUserCode]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //        case 1:
        //            ((Window)target).StateChanged += new EventHandler(this.Window_StateChanged);
        //            ((FrameworkElement)target).SizeChanged += new SizeChangedEventHandler(this.Window_SizeChanged);
        //            break;
        //        case 2:
        //            this.mnuMain = (System.Windows.Controls.ContextMenu)target;
        //            break;
        //        case 3:
        //            this.mnuSacle = (System.Windows.Controls.MenuItem)target;
        //            break;
        //        case 4:
        //            this.windowsFormsHost1 = (WindowsFormsHost)target;
        //            break;
        //        case 5:
        //            this.FFTGraphPicture = (PictureBox)target;
        //            break;
        //        default:
        //            this._contentLoaded = true;
        //            break;
        //    }
        //}
    }
}
