// Decompiled with JetBrains decompiler
// Type: RadioDesk.Core.RadioDeskUtils
// Assembly: RadioDesk.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 855D0D1C-52A8-4CAD-BE15-1C45DC37A846
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\RadioDesk.Core.dll

using System;

namespace RadioDesk.Core
{
  public static class RadioDeskUtils
  {
    public static string FormatToTime(uint time)
    {
      uint num1 = time / 60U;
      if (num1 >= 60U)
        num1 = 0U;
      uint num2 = num1 / 60U;
      uint num3 = time % 60U;
      if (num3 >= 60U)
        num3 = 0U;
      return new TimeSpan((int) num2, (int) num1, (int) num3).ToString();
    }
  }
}
