// Decompiled with JetBrains decompiler
// Type: RadioDesk.Data.Extra.DBConnection
// Assembly: MediaPlayerFinal, Version=1.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: FA266A67-626E-46F0-81D3-A2E3F3CE1F98
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\MediaPlayerFinal.exe

using System;
using System.Data;
using System.Data.OleDb;

namespace RadioDesk.Data.Extra
{
  internal class DBConnection : IDisposable
  {
    private string conString;
    public OleDbConnection oledbconn;

    public DBConnection(string dbPath)
    {
      try
      {
        this.conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + dbPath;
        this.oledbconn = new OleDbConnection(this.conString);
      }
      catch (Exception ex)
      {
        throw new FileAdderException("Cannot connect to database.");
      }
    }

    public bool Open()
    {
      try
      {
        if (this.oledbconn.State == ConnectionState.Broken || this.oledbconn.State == ConnectionState.Closed)
          this.oledbconn.Open();
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public void Close()
    {
      try
      {
        this.oledbconn.Close();
      }
      catch (Exception ex)
      {
        this.oledbconn.Close();
      }
    }

    public bool IsOpened
    {
      get
      {
        return this.oledbconn.State == ConnectionState.Open;
      }
    }

    public void Dispose()
    {
      if (this.IsOpened)
        this.oledbconn.Close();
      this.oledbconn.Dispose();
    }
  }
}
