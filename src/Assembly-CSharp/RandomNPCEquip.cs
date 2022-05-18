using System;
using KBEngine;
using YSGame.EquipRandom;

// Token: 0x0200053F RID: 1343
public static class RandomNPCEquip
{
	// Token: 0x06002244 RID: 8772 RVA: 0x0001C169 File Offset: 0x0001A369
	public static void CreateLoveEquip(ref int ItemID, ref JSONObject ItemJson, int ShuXingID, Avatar Maker = null, int EquipQuality = -1)
	{
		RandomEquip.CreateRandomEquip(ref ItemID, ref ItemJson, EquipQuality, ShuXingID, -1, -1, -1, Maker);
	}
}
