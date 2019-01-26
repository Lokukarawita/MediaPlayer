// Decompiled with JetBrains decompiler
// Type: libZPlay.TCallbackFunc
// Assembly: RadioDesk.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 855D0D1C-52A8-4CAD-BE15-1C45DC37A846
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\RadioDesk.Core.dll

namespace libZPlay
{
  public delegate int TCallbackFunc(
    uint objptr,
    int user_data,
    TCallbackMessage msg,
    uint param1,
    uint param2);
}
