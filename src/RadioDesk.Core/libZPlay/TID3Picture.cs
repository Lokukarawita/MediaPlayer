// Decompiled with JetBrains decompiler
// Type: libZPlay.TID3Picture
// Assembly: RadioDesk.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 855D0D1C-52A8-4CAD-BE15-1C45DC37A846
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\RadioDesk.Core.dll

using System.Drawing;
using System.IO;

namespace libZPlay
{
  public struct TID3Picture
  {
    public bool PicturePresent;
    public int PictureType;
    public string Description;
    public Bitmap Bitmap;
    public MemoryStream BitStream;
  }
}
