using System;
using KBEngine;
using YSGame.EquipRandom;

// Token: 0x020003B4 RID: 948
public static class RandomNPCEquip
{
	// Token: 0x06001EC1 RID: 7873 RVA: 0x000D7DF6 File Offset: 0x000D5FF6
	public static void CreateLoveEquip(ref int ItemID, ref JSONObject ItemJson, int ShuXingID, Avatar Maker = null, int EquipQuality = -1)
	{
		RandomEquip.CreateRandomEquip(ref ItemID, ref ItemJson, EquipQuality, ShuXingID, -1, -1, -1, Maker);
	}
}
