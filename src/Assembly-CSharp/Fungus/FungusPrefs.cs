using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F04 RID: 3844
	public static class FungusPrefs
	{
		// Token: 0x06006C31 RID: 27697 RVA: 0x00298541 File Offset: 0x00296741
		public static void DeleteAll()
		{
			PlayerPrefs.DeleteAll();
		}

		// Token: 0x06006C32 RID: 27698 RVA: 0x00298548 File Offset: 0x00296748
		public static void DeleteKey(int slot, string key)
		{
			PlayerPrefs.DeleteKey(FungusPrefs.GetSlotKey(slot, key));
		}

		// Token: 0x06006C33 RID: 27699 RVA: 0x00298556 File Offset: 0x00296756
		public static float GetFloat(int slot, string key, float defaultValue = 0f)
		{
			return PlayerPrefs.GetFloat(FungusPrefs.GetSlotKey(slot, key), defaultValue);
		}

		// Token: 0x06006C34 RID: 27700 RVA: 0x00298565 File Offset: 0x00296765
		public static int GetInt(int slot, string key, int defaultValue = 0)
		{
			return PlayerPrefs.GetInt(FungusPrefs.GetSlotKey(slot, key), defaultValue);
		}

		// Token: 0x06006C35 RID: 27701 RVA: 0x00298574 File Offset: 0x00296774
		public static string GetString(int slot, string key, string defaultValue = "")
		{
			return PlayerPrefs.GetString(FungusPrefs.GetSlotKey(slot, key), defaultValue);
		}

		// Token: 0x06006C36 RID: 27702 RVA: 0x00298583 File Offset: 0x00296783
		public static bool HasKey(int slot, string key)
		{
			return PlayerPrefs.HasKey(FungusPrefs.GetSlotKey(slot, key));
		}

		// Token: 0x06006C37 RID: 27703 RVA: 0x00298591 File Offset: 0x00296791
		public static void Save()
		{
			PlayerPrefs.Save();
		}

		// Token: 0x06006C38 RID: 27704 RVA: 0x00298598 File Offset: 0x00296798
		public static void SetFloat(int slot, string key, float value)
		{
			PlayerPrefs.SetFloat(FungusPrefs.GetSlotKey(slot, key), value);
		}

		// Token: 0x06006C39 RID: 27705 RVA: 0x002985A7 File Offset: 0x002967A7
		public static void SetInt(int slot, string key, int value)
		{
			PlayerPrefs.SetInt(FungusPrefs.GetSlotKey(slot, key), value);
		}

		// Token: 0x06006C3A RID: 27706 RVA: 0x002985B6 File Offset: 0x002967B6
		public static void SetString(int slot, string key, string value)
		{
			PlayerPrefs.SetString(FungusPrefs.GetSlotKey(slot, key), value);
		}

		// Token: 0x06006C3B RID: 27707 RVA: 0x002985C5 File Offset: 0x002967C5
		private static string GetSlotKey(int slot, string key)
		{
			return slot.ToString() + ":" + key;
		}
	}
}
