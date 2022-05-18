using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000BB RID: 187
public static class Localization
{
	// Token: 0x170000D9 RID: 217
	// (get) Token: 0x060006FB RID: 1787 RVA: 0x0000A021 File Offset: 0x00008221
	// (set) Token: 0x060006FC RID: 1788 RVA: 0x0000A043 File Offset: 0x00008243
	public static Dictionary<string, string[]> dictionary
	{
		get
		{
			if (!Localization.localizationHasBeenSet)
			{
				Localization.language = PlayerPrefs.GetString("Language", "English");
			}
			return Localization.mDictionary;
		}
		set
		{
			Localization.localizationHasBeenSet = (value != null);
			Localization.mDictionary = value;
		}
	}

	// Token: 0x170000DA RID: 218
	// (get) Token: 0x060006FD RID: 1789 RVA: 0x0000A054 File Offset: 0x00008254
	public static string[] knownLanguages
	{
		get
		{
			if (!Localization.localizationHasBeenSet)
			{
				Localization.LoadDictionary(PlayerPrefs.GetString("Language", "English"));
			}
			return Localization.mLanguages;
		}
	}

	// Token: 0x170000DB RID: 219
	// (get) Token: 0x060006FE RID: 1790 RVA: 0x00079FCC File Offset: 0x000781CC
	// (set) Token: 0x060006FF RID: 1791 RVA: 0x0000A077 File Offset: 0x00008277
	public static string language
	{
		get
		{
			if (string.IsNullOrEmpty(Localization.mLanguage))
			{
				string[] knownLanguages = Localization.knownLanguages;
				Localization.mLanguage = PlayerPrefs.GetString("Language", (knownLanguages != null) ? knownLanguages[0] : "English");
				Localization.LoadAndSelect(Localization.mLanguage);
			}
			return Localization.mLanguage;
		}
		set
		{
			if (Localization.mLanguage != value)
			{
				Localization.mLanguage = value;
				Localization.LoadAndSelect(value);
			}
		}
	}

	// Token: 0x06000700 RID: 1792 RVA: 0x0007A018 File Offset: 0x00078218
	private static bool LoadDictionary(string value)
	{
		TextAsset textAsset = Localization.localizationHasBeenSet ? null : (Resources.Load("Localization", typeof(TextAsset)) as TextAsset);
		Localization.localizationHasBeenSet = true;
		if (textAsset != null && Localization.LoadCSV(textAsset))
		{
			return true;
		}
		if (string.IsNullOrEmpty(value))
		{
			return false;
		}
		textAsset = (Resources.Load(value, typeof(TextAsset)) as TextAsset);
		if (textAsset != null)
		{
			Localization.Load(textAsset);
			return true;
		}
		return false;
	}

	// Token: 0x06000701 RID: 1793 RVA: 0x0007A094 File Offset: 0x00078294
	private static bool LoadAndSelect(string value)
	{
		if (!string.IsNullOrEmpty(value))
		{
			if (Localization.mDictionary.Count == 0 && !Localization.LoadDictionary(value))
			{
				return false;
			}
			if (Localization.SelectLanguage(value))
			{
				return true;
			}
		}
		if (Localization.mOldDictionary.Count > 0)
		{
			return true;
		}
		Localization.mOldDictionary.Clear();
		Localization.mDictionary.Clear();
		if (string.IsNullOrEmpty(value))
		{
			PlayerPrefs.DeleteKey("Language");
		}
		return false;
	}

	// Token: 0x06000702 RID: 1794 RVA: 0x0007A100 File Offset: 0x00078300
	public static void Load(TextAsset asset)
	{
		ByteReader byteReader = new ByteReader(asset);
		Localization.Set(asset.name, byteReader.ReadDictionary());
	}

	// Token: 0x06000703 RID: 1795 RVA: 0x0007A128 File Offset: 0x00078328
	public static bool LoadCSV(TextAsset asset)
	{
		ByteReader byteReader = new ByteReader(asset);
		BetterList<string> betterList = byteReader.ReadCSV();
		if (betterList.size < 2)
		{
			return false;
		}
		betterList[0] = "KEY";
		if (!string.Equals(betterList[0], "KEY"))
		{
			Debug.LogError("Invalid localization CSV file. The first value is expected to be 'KEY', followed by language columns.\nInstead found '" + betterList[0] + "'", asset);
			return false;
		}
		Localization.mLanguages = new string[betterList.size - 1];
		for (int i = 0; i < Localization.mLanguages.Length; i++)
		{
			Localization.mLanguages[i] = betterList[i + 1];
		}
		Localization.mDictionary.Clear();
		while (betterList != null)
		{
			Localization.AddCSV(betterList);
			betterList = byteReader.ReadCSV();
		}
		return true;
	}

	// Token: 0x06000704 RID: 1796 RVA: 0x0007A1DC File Offset: 0x000783DC
	private static bool SelectLanguage(string language)
	{
		Localization.mLanguageIndex = -1;
		if (Localization.mDictionary.Count == 0)
		{
			return false;
		}
		string[] array;
		if (Localization.mDictionary.TryGetValue("KEY", out array))
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == language)
				{
					Localization.mOldDictionary.Clear();
					Localization.mLanguageIndex = i;
					Localization.mLanguage = language;
					PlayerPrefs.SetString("Language", Localization.mLanguage);
					UIRoot.Broadcast("OnLocalize");
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000705 RID: 1797 RVA: 0x0007A25C File Offset: 0x0007845C
	private static void AddCSV(BetterList<string> values)
	{
		if (values.size < 2)
		{
			return;
		}
		string[] array = new string[values.size - 1];
		for (int i = 1; i < values.size; i++)
		{
			array[i - 1] = values[i];
		}
		try
		{
			Localization.mDictionary.Add(values[0], array);
		}
		catch (Exception ex)
		{
			Debug.LogError("Unable to add '" + values[0] + "' to the Localization dictionary.\n" + ex.Message);
		}
	}

	// Token: 0x06000706 RID: 1798 RVA: 0x0007A2E8 File Offset: 0x000784E8
	public static void Set(string languageName, Dictionary<string, string> dictionary)
	{
		Localization.mLanguage = languageName;
		PlayerPrefs.SetString("Language", Localization.mLanguage);
		Localization.mOldDictionary = dictionary;
		Localization.localizationHasBeenSet = false;
		Localization.mLanguageIndex = -1;
		Localization.mLanguages = new string[]
		{
			languageName
		};
		UIRoot.Broadcast("OnLocalize");
	}

	// Token: 0x06000707 RID: 1799 RVA: 0x0007A338 File Offset: 0x00078538
	public static string Get(string key)
	{
		if (!Localization.localizationHasBeenSet)
		{
			Localization.language = PlayerPrefs.GetString("Language", "English");
		}
		string[] array;
		string result;
		if (Localization.mLanguageIndex != -1 && Localization.mDictionary.TryGetValue(key, out array))
		{
			if (Localization.mLanguageIndex < array.Length)
			{
				return array[Localization.mLanguageIndex];
			}
		}
		else if (Localization.mOldDictionary.TryGetValue(key, out result))
		{
			return result;
		}
		return key;
	}

	// Token: 0x170000DC RID: 220
	// (get) Token: 0x06000708 RID: 1800 RVA: 0x0000A093 File Offset: 0x00008293
	[Obsolete("Localization is now always active. You no longer need to check this property.")]
	public static bool isActive
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06000709 RID: 1801 RVA: 0x0000A096 File Offset: 0x00008296
	[Obsolete("Use Localization.Get instead")]
	public static string Localize(string key)
	{
		return Localization.Get(key);
	}

	// Token: 0x0600070A RID: 1802 RVA: 0x0000A09E File Offset: 0x0000829E
	public static bool Exists(string key)
	{
		if (!Localization.localizationHasBeenSet)
		{
			Localization.language = PlayerPrefs.GetString("Language", "English");
		}
		return Localization.mDictionary.ContainsKey(key) || Localization.mOldDictionary.ContainsKey(key);
	}

	// Token: 0x0400052B RID: 1323
	public static bool localizationHasBeenSet = false;

	// Token: 0x0400052C RID: 1324
	private static string[] mLanguages = null;

	// Token: 0x0400052D RID: 1325
	private static Dictionary<string, string> mOldDictionary = new Dictionary<string, string>();

	// Token: 0x0400052E RID: 1326
	private static Dictionary<string, string[]> mDictionary = new Dictionary<string, string[]>();

	// Token: 0x0400052F RID: 1327
	private static int mLanguageIndex = -1;

	// Token: 0x04000530 RID: 1328
	private static string mLanguage;
}
