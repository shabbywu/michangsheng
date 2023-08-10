using KBEngine;
using YSGame.EquipRandom;

public static class RandomNPCEquip
{
	public static void CreateLoveEquip(ref int ItemID, ref JSONObject ItemJson, int ShuXingID, Avatar Maker = null, int EquipQuality = -1)
	{
		RandomEquip.CreateRandomEquip(ref ItemID, ref ItemJson, EquipQuality, ShuXingID, -1, -1, -1, Maker);
	}
}
