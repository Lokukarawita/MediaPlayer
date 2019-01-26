// Decompiled with JetBrains decompiler
// Type: RadioDesk.Core.RadioDeskEqualizer
// Assembly: RadioDesk.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 855D0D1C-52A8-4CAD-BE15-1C45DC37A846
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\RadioDesk.Core.dll

using libZPlay;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace RadioDesk.Core
{
  public sealed class RadioDeskEqualizer
  {
    private bool enabled = false;
    private ZPlay libzply_obj;
    private TStreamStatus stream_status;

    public event EQSettingsLoaded SettingsLoaded;

    public RadioDeskEqualizer(RadioDeskPlayer ply)
    {
      this.libzply_obj = (ZPlay) ply.GetType().GetField("player", BindingFlags.Instance | BindingFlags.NonPublic).GetValue((object) ply);
      int[] FreqPointArray = new int[10]
      {
        60,
        170,
        310,
        600,
        1000,
        3000,
        6000,
        12000,
        14000,
        16000
      };
      if (!this.libzply_obj.SetEqualizerPoints(ref FreqPointArray, 10))
        throw new MediaPlaybackException(this.libzply_obj.GetError());
    }

    private static int BandValueForSet(int inval)
    {
      if (inval > 40)
        inval = 40;
      else if (inval < 0)
        inval = 10;
      inval = (20000 - inval * 1000) * -1;
      return inval;
    }

    private static int BandValueForGet(int inval)
    {
      inval = (20000 + inval) / 1000;
      return inval;
    }

    private void SetAllBands(
      int pre,
      int e1,
      int e2,
      int e3,
      int e4,
      int e5,
      int e6,
      int e7,
      int e8,
      int e9,
      int e10)
    {
      this.PreampGain = pre;
      this.Band60Hz = e1;
      this.Band170Hz = e2;
      this.Band310Hz = e3;
      this.Band600Hz = e4;
      this.Band1KHz = e5;
      this.Band3KHz = e6;
      this.Band6KHz = e7;
      this.Band12KHz = e8;
      this.Band14KHz = e9;
      this.Band16KHz = e10;
    }

    public void SetEQByUsing(EQPreset pre, bool Enable)
    {
      switch (pre)
      {
        case EQPreset.Default:
          this.Enable = Enable;
          this.SetAllBands(20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20);
          if (this.SettingsLoaded == null)
            break;
          this.SettingsLoaded((object) this, new EventArgs());
          break;
        case EQPreset.BassTreble:
          this.Enable = Enable;
          this.SetAllBands(18, 25, 24, 23, 20, 19, 18, 19, 22, 24, 22);
          if (this.SettingsLoaded == null)
            break;
          this.SettingsLoaded((object) this, new EventArgs());
          break;
      }
    }

    public void SetEQByUsing(EQSettings sett)
    {
      this.libzply_obj.EnableEqualizer(sett.Enabled);
      this.enabled = sett.Enabled;
      this.Band60Hz = sett.Band60Hz;
      this.Band170Hz = sett.Band170Hz;
      this.Band310Hz = sett.Band310Hz;
      this.Band600Hz = sett.Band600Hz;
      this.Band1KHz = sett.Band1KHz;
      this.Band3KHz = sett.Band3KHz;
      this.Band6KHz = sett.Band6KHz;
      this.Band12KHz = sett.Band12KHz;
      this.Band14KHz = sett.Band14KHz;
      this.Band16KHz = sett.Band16KHz;
      if (this.SettingsLoaded == null)
        return;
      this.SettingsLoaded((object) this, new EventArgs());
    }

    public bool Enable
    {
      get
      {
        return this.enabled;
      }
      set
      {
        this.enabled = value;
        this.libzply_obj.EnableEqualizer(value);
      }
    }

    public int PreampGain
    {
      get
      {
        return RadioDeskEqualizer.BandValueForGet(this.libzply_obj.GetEqualizerPreampGain());
      }
      set
      {
        this.libzply_obj.SetEqualizerPreampGain(RadioDeskEqualizer.BandValueForSet(value));
      }
    }

    public int Band60Hz
    {
      get
      {
        return RadioDeskEqualizer.BandValueForGet(this.libzply_obj.GetEqualizerBandGain(0));
      }
      set
      {
        this.libzply_obj.SetEqualizerBandGain(0, RadioDeskEqualizer.BandValueForSet(value));
      }
    }

    public int Band170Hz
    {
      get
      {
        return RadioDeskEqualizer.BandValueForGet(this.libzply_obj.GetEqualizerBandGain(1));
      }
      set
      {
        this.libzply_obj.SetEqualizerBandGain(1, RadioDeskEqualizer.BandValueForSet(value));
      }
    }

    public int Band310Hz
    {
      get
      {
        return RadioDeskEqualizer.BandValueForGet(this.libzply_obj.GetEqualizerBandGain(2));
      }
      set
      {
        this.libzply_obj.SetEqualizerBandGain(2, RadioDeskEqualizer.BandValueForSet(value));
      }
    }

    public int Band600Hz
    {
      get
      {
        return RadioDeskEqualizer.BandValueForGet(this.libzply_obj.GetEqualizerBandGain(3));
      }
      set
      {
        this.libzply_obj.SetEqualizerBandGain(3, RadioDeskEqualizer.BandValueForSet(value));
      }
    }

    public int Band1KHz
    {
      get
      {
        return RadioDeskEqualizer.BandValueForGet(this.libzply_obj.GetEqualizerBandGain(4));
      }
      set
      {
        this.libzply_obj.SetEqualizerBandGain(4, RadioDeskEqualizer.BandValueForSet(value));
      }
    }

    public int Band3KHz
    {
      get
      {
        return RadioDeskEqualizer.BandValueForGet(this.libzply_obj.GetEqualizerBandGain(5));
      }
      set
      {
        this.libzply_obj.SetEqualizerBandGain(5, RadioDeskEqualizer.BandValueForSet(value));
      }
    }

    public int Band6KHz
    {
      get
      {
        return RadioDeskEqualizer.BandValueForGet(this.libzply_obj.GetEqualizerBandGain(6));
      }
      set
      {
        this.libzply_obj.SetEqualizerBandGain(6, RadioDeskEqualizer.BandValueForSet(value));
      }
    }

    public int Band12KHz
    {
      get
      {
        return RadioDeskEqualizer.BandValueForGet(this.libzply_obj.GetEqualizerBandGain(7));
      }
      set
      {
        this.libzply_obj.SetEqualizerBandGain(7, RadioDeskEqualizer.BandValueForSet(value));
      }
    }

    public int Band14KHz
    {
      get
      {
        return RadioDeskEqualizer.BandValueForGet(this.libzply_obj.GetEqualizerBandGain(8));
      }
      set
      {
        this.libzply_obj.SetEqualizerBandGain(8, RadioDeskEqualizer.BandValueForSet(value));
      }
    }

    public int Band16KHz
    {
      get
      {
        return RadioDeskEqualizer.BandValueForGet(this.libzply_obj.GetEqualizerBandGain(9));
      }
      set
      {
        this.libzply_obj.SetEqualizerBandGain(9, RadioDeskEqualizer.BandValueForSet(value));
      }
    }

    public bool Save(string path)
    {
      Encoding encoding = Encoding.Default;
      try
      {
        StreamWriter streamWriter = new StreamWriter(path, false, encoding);
        streamWriter.WriteLine(this.Enable);
        streamWriter.WriteLine(this.PreampGain);
        streamWriter.WriteLine(this.Band60Hz);
        streamWriter.WriteLine(this.Band170Hz);
        streamWriter.WriteLine(this.Band310Hz);
        streamWriter.WriteLine(this.Band600Hz);
        streamWriter.WriteLine(this.Band1KHz);
        streamWriter.WriteLine(this.Band3KHz);
        streamWriter.WriteLine(this.Band6KHz);
        streamWriter.WriteLine(this.Band12KHz);
        streamWriter.WriteLine(this.Band14KHz);
        streamWriter.WriteLine(this.Band16KHz);
        streamWriter.Close();
        streamWriter.Dispose();
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public bool Load(string path)
    {
      Encoding encoding = Encoding.Default;
      try
      {
        StreamReader streamReader = new StreamReader(path, encoding);
        this.Enable = bool.Parse(streamReader.ReadLine());
        this.PreampGain = int.Parse(streamReader.ReadLine());
        this.Band60Hz = int.Parse(streamReader.ReadLine());
        this.Band170Hz = int.Parse(streamReader.ReadLine());
        this.Band310Hz = int.Parse(streamReader.ReadLine());
        this.Band600Hz = int.Parse(streamReader.ReadLine());
        this.Band1KHz = int.Parse(streamReader.ReadLine());
        this.Band3KHz = int.Parse(streamReader.ReadLine());
        this.Band6KHz = int.Parse(streamReader.ReadLine());
        this.Band12KHz = int.Parse(streamReader.ReadLine());
        this.Band14KHz = int.Parse(streamReader.ReadLine());
        this.Band16KHz = int.Parse(streamReader.ReadLine());
        streamReader.Close();
        streamReader.Dispose();
        if (this.SettingsLoaded != null)
          this.SettingsLoaded((object) this, new EventArgs());
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public EQSettings GetEQSettings()
    {
      return new EQSettings()
      {
        Enabled = this.Enable,
        Band60Hz = this.Band60Hz,
        Band170Hz = this.Band170Hz,
        Band310Hz = this.Band310Hz,
        Band600Hz = this.Band600Hz,
        Band1KHz = this.Band1KHz,
        Band3KHz = this.Band3KHz,
        Band6KHz = this.Band6KHz,
        Band12KHz = this.Band12KHz,
        Band14KHz = this.Band14KHz,
        Band16KHz = this.Band16KHz
      };
    }
  }
}
