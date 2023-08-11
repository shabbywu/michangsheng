using UnityEngine;

namespace Fungus;

public static class FungusPrefs
{
	public static void DeleteAll()
	{
		PlayerPrefs.DeleteAll();
	}

	public static void DeleteKey(int slot, string key)
	{
		PlayerPrefs.DeleteKey(GetSlotKey(slot, key));
	}

	public static float GetFloat(int slot, string key, float defaultValue = 0f)
	{
		return PlayerPrefs.GetFloat(GetSlotKey(slot, key), defaultValue);
	}

	public static int GetInt(int slot, string key, int defaultValue = 0)
	{
		return PlayerPrefs.GetInt(GetSlotKey(slot, key), defaultValue);
	}

	public static string GetString(int slot, string key, string defaultValue = "")
	{
		return PlayerPrefs.GetString(GetSlotKey(slot, key), defaultValue);
	}

	public static bool HasKey(int slot, string key)
	{
		return PlayerPrefs.HasKey(GetSlotKey(slot, key));
	}

	public static void Save()
	{
		PlayerPrefs.Save();
	}

	public static void SetFloat(int slot, string key, float value)
	{
		PlayerPrefs.SetFloat(GetSlotKey(slot, key), value);
	}

	public static void SetInt(int slot, string key, int value)
	{
		PlayerPrefs.SetInt(GetSlotKey(slot, key), value);
	}

	public static void SetString(int slot, string key, string value)
	{
		PlayerPrefs.SetString(GetSlotKey(slot, key), value);
	}

	private static string GetSlotKey(int slot, string key)
	{
		return slot + ":" + key;
	}
}
