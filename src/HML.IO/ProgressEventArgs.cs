// Decompiled with JetBrains decompiler
// Type: HML.IO.Search.ProgressEventArgs
// Assembly: HML.IO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1C3A47EF-9E7B-42FA-BC01-DB283160390B
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\HML.IO.dll

using System;

namespace HML.IO.Search
{
  public sealed class ProgressEventArgs : EventArgs
  {
    private string curDir;
    private int filcnt;
    private string[] curfils;
    private string curFailFol;
    private bool hasfailed;

    public ProgressEventArgs(string dir, int filcnt, string[] curf, string curff)
    {
      this.curDir = dir;
      this.filcnt = filcnt;
      this.curfils = curf;
      this.curFailFol = curff;
      if (this.curFailFol == null)
        return;
      this.hasfailed = true;
    }

    public string CurrentDirectory
    {
      get
      {
        return this.curDir;
      }
    }

    public int NumberOfFilesFound
    {
      get
      {
        return this.filcnt;
      }
    }

    public string[] RecentFilesFound
    {
      get
      {
        return this.curfils;
      }
    }

    public string RecentFailedFolder
    {
      get
      {
        return this.curFailFol;
      }
    }

    public bool HasFailedFolder
    {
      get
      {
        return this.hasfailed;
      }
    }
  }
}
