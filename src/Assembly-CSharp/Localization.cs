using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000083 RID: 131
public static class Localization
{
	// Token: 0x170000C5 RID: 197
	// (get) Token: 0x06000679 RID: 1657 RVA: 0x0002481E File Offset: 0x00022A1E
	// (set) Token: 0x0600067A RID: 1658 RVA: 0x00024840 File Offset: 0x00022A40
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

	// Token: 0x170000C6 RID: 198
	// (get) Token: 0x0600067B RID: 1659 RVA: 0x00024851 File Offset: 0x00022A51
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

	// Token: 0x170000C7 RID: 199
	// (get) Token: 0x0600067C RID: 1660 RVA: 0x00024874 File Offset: 0x00022A74
	// (set) Token: 0x0600067D RID: 1661 RVA: 0x000248BF File Offset: 0x00022ABF
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

	// Token: 0x0600067E RID: 1662 RVA: 0x000248DC File Offset: 0x00022ADC
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

	// Token: 0x0600067F RID: 1663 RVA: 0x00024958 File Offset: 0x00022B58
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

	// Token: 0x06000680 RID: 1664 RVA: 0x000249C4 File Offset: 0x00022BC4
	public static void Load(TextAsset asset)
	{
		ByteReader byteReader = new ByteReader(asset);
		Localization.Set(asset.name, byteReader.ReadDictionary());
	}

	// Token: 0x06000681 RID: 1665 RVA: 0x000249EC File Offset: 0x00022BEC
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

	// Token: 0x06000682 RID: 1666 RVA: 0x00024AA0 File Offset: 0x00022CA0
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

	// Token: 0x06000683 RID: 1667 RVA: 0x00024B20 File Offset: 0x00022D20
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

	// Token: 0x06000684 RID: 1668 RVA: 0x00024BAC File Offset: 0x00022DAC
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

	// Token: 0x06000685 RID: 1669 RVA: 0x00024BFC File Offset: 0x00022DFC
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

	// Token: 0x170000C8 RID: 200
	// (get) Token: 0x06000686 RID: 1670 RVA: 0x00024C5F File Offset: 0x00022E5F
	[Obsolete("Localization is now always active. You no longer need to check this property.")]
	public static bool isActive
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06000687 RID: 1671 RVA: 0x00024C62 File Offset: 0x00022E62
	[Obsolete("Use Localization.Get instead")]
	public static string Localize(string key)
	{
		return Localization.Get(key);
	}

	// Token: 0x06000688 RID: 1672 RVA: 0x00024C6A File Offset: 0x00022E6A
	public static bool Exists(string key)
	{
		if (!Localization.localizationHasBeenSet)
		{
			Localization.language = PlayerPrefs.GetString("Language", "English");
		}
		return Localization.mDictionary.ContainsKey(key) || Localization.mOldDictionary.ContainsKey(key);
	}

	// Token: 0x04000451 RID: 1105
	public static bool localizationHasBeenSet = false;

	// Token: 0x04000452 RID: 1106
	private static string[] mLanguages = null;

	// Token: 0x04000453 RID: 1107
	private static Dictionary<string, string> mOldDictionary = new Dictionary<string, string>();

	// Token: 0x04000454 RID: 1108
	private static Dictionary<string, string[]> mDictionary = new Dictionary<string, string[]>();

	// Token: 0x04000455 RID: 1109
	private static int mLanguageIndex = -1;

	// Token: 0x04000456 RID: 1110
	private static string mLanguage;
}
