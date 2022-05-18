using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013A9 RID: 5033
	public static class FungusPrefs
	{
		// Token: 0x060079E5 RID: 31205 RVA: 0x0005326C File Offset: 0x0005146C
		public static void DeleteAll()
		{
			PlayerPrefs.DeleteAll();
		}

		// Token: 0x060079E6 RID: 31206 RVA: 0x00053273 File Offset: 0x00051473
		public static void DeleteKey(int slot, string key)
		{
			PlayerPrefs.DeleteKey(FungusPrefs.GetSlotKey(slot, key));
		}

		// Token: 0x060079E7 RID: 31207 RVA: 0x00053281 File Offset: 0x00051481
		public static float GetFloat(int slot, string key, float defaultValue = 0f)
		{
			return PlayerPrefs.GetFloat(FungusPrefs.GetSlotKey(slot, key), defaultValue);
		}

		// Token: 0x060079E8 RID: 31208 RVA: 0x00053290 File Offset: 0x00051490
		public static int GetInt(int slot, string key, int defaultValue = 0)
		{
			return PlayerPrefs.GetInt(FungusPrefs.GetSlotKey(slot, key), defaultValue);
		}

		// Token: 0x060079E9 RID: 31209 RVA: 0x0005329F File Offset: 0x0005149F
		public static string GetString(int slot, string key, string defaultValue = "")
		{
			return PlayerPrefs.GetString(FungusPrefs.GetSlotKey(slot, key), defaultValue);
		}

		// Token: 0x060079EA RID: 31210 RVA: 0x000532AE File Offset: 0x000514AE
		public static bool HasKey(int slot, string key)
		{
			return PlayerPrefs.HasKey(FungusPrefs.GetSlotKey(slot, key));
		}

		// Token: 0x060079EB RID: 31211 RVA: 0x000532BC File Offset: 0x000514BC
		public static void Save()
		{
			PlayerPrefs.Save();
		}

		// Token: 0x060079EC RID: 31212 RVA: 0x000532C3 File Offset: 0x000514C3
		public static void SetFloat(int slot, string key, float value)
		{
			PlayerPrefs.SetFloat(FungusPrefs.GetSlotKey(slot, key), value);
		}

		// Token: 0x060079ED RID: 31213 RVA: 0x000532D2 File Offset: 0x000514D2
		public static void SetInt(int slot, string key, int value)
		{
			PlayerPrefs.SetInt(FungusPrefs.GetSlotKey(slot, key), value);
		}

		// Token: 0x060079EE RID: 31214 RVA: 0x000532E1 File Offset: 0x000514E1
		public static void SetString(int slot, string key, string value)
		{
			PlayerPrefs.SetString(FungusPrefs.GetSlotKey(slot, key), value);
		}

		// Token: 0x060079EF RID: 31215 RVA: 0x000532F0 File Offset: 0x000514F0
		private static string GetSlotKey(int slot, string key)
		{
			return slot.ToString() + ":" + key;
		}
	}
}
