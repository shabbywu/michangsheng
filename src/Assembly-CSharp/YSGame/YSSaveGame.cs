using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace YSGame;

public class YSSaveGame
{
	public static Dictionary<string, int> saveInt = new Dictionary<string, int>();

	private static Dictionary<string, float> saveFloat = new Dictionary<string, float>();

	public static Dictionary<string, string> saveString = new Dictionary<string, string>();

	public static Dictionary<string, JSONObject> saveJSONObject = new Dictionary<string, JSONObject>();

	public static Dictionary<string, JObject> saveJObject = new Dictionary<string, JObject>();

	public static JSONObject SaveJsonData = new JSONObject(JSONObject.Type.OBJECT);

	public static int curSaveIndex = -1;

	private static StreamWriter writer;

	private static StreamReader reader;

	private static string readerText = "";

	public static char huanHangChar = 'þ';

	public static void Reset()
	{
		saveInt = new Dictionary<string, int>();
		saveFloat = new Dictionary<string, float>();
		saveString = new Dictionary<string, string>();
		saveJSONObject = new Dictionary<string, JSONObject>();
		SaveJsonData = new JSONObject(JSONObject.Type.OBJECT);
		saveJObject = new Dictionary<string, JObject>();
		jsonData.instance.AvatarBackpackJsonData = null;
	}

	public static void save(string name, JSONObject json, string gamePath = "-1")
	{
		JSONObject value = new JSONObject(json.ToString());
		saveJSONObject[name] = value;
		WriteIntoTxt(name, saveJSONObject[name].ToString(), gamePath);
	}

	public static void save(string name, JObject json)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		JObject value = new JObject((object)((object)json).ToString());
		saveJObject[name] = value;
		WriteIntoTxt(name, ((object)saveJObject[name]).ToString());
	}

	public static void save(string name, string json, string gamePath = "-1")
	{
		saveString[name] = json;
		WriteIntoTxt(name, saveString[name], gamePath);
	}

	public static void save(string name, int json, string gamePath = "-1")
	{
		saveInt[name] = json;
		WriteIntoTxt(name, saveInt[name].ToString(), gamePath);
	}

	public static string GetString(JSONObject json, string name, string ret = "")
	{
		json.GetField(ref ret, name);
		return ret;
	}

	public static int GetInt(JSONObject json, string name, int ret = 0)
	{
		json.GetField(ref ret, name);
		return ret;
	}

	public static float GetFloat(JSONObject json, string name, float ret = 0f)
	{
		json.GetField(ref ret, name);
		return ret;
	}

	public static JSONObject GetJsonObject(JSONObject json, string name, JSONObject ret = null)
	{
		return json.GetField(name);
	}

	public static string GetString(string name, string ret = "")
	{
		string text = ret;
		if (saveString.ContainsKey(name))
		{
			return saveString[name];
		}
		return GetTextNameData(name);
	}

	public static JSONObject GetJsonObject(string name, JSONObject json = null)
	{
		JSONObject jSONObject = json;
		if (saveJSONObject.ContainsKey(name))
		{
			return saveJSONObject[name];
		}
		return new JSONObject(GetTextNameData(name));
	}

	public static JObject GetJObject(string name, JObject json = null)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		JObject val = json;
		if (saveJObject.ContainsKey(name))
		{
			return saveJObject[name];
		}
		return new JObject((object)GetTextNameData(name));
	}

	public static int GetInt(string name, int ret = 0)
	{
		int result = ret;
		if (saveInt.ContainsKey(name))
		{
			result = saveInt[name];
		}
		else
		{
			try
			{
				string textNameData = GetTextNameData(name);
				if (textNameData == "")
				{
					return result;
				}
				saveInt[name] = int.Parse(textNameData);
				SaveJsonData.SetField(name, int.Parse(textNameData));
				result = (int)SaveJsonData[name].n;
			}
			catch (Exception ex)
			{
				Debug.LogError((object)ex.ToString());
				UIPopTip.Inst.Pop(ex.ToString());
			}
		}
		return result;
	}

	public static bool HasKey(string str)
	{
		if (!new FileInfo(Application.dataPath + "/" + str + ".sav").Exists)
		{
			return false;
		}
		return true;
	}

	public static bool HasFile(string path, string str)
	{
		if (!new FileInfo(path + "/" + str + ".sav").Exists)
		{
			return false;
		}
		return true;
	}

	public static void WriteIntoTxt(string TextName, string message, string persistentDataPath = "-1")
	{
		string value = message.Replace('\n', huanHangChar);
		FileInfo fileInfo = ((!(persistentDataPath == "-1")) ? new FileInfo(persistentDataPath + "/" + TextName + ".sav") : new FileInfo(Paths.GetSavePath() + "/" + TextName + ".sav"));
		if (!fileInfo.Exists)
		{
			writer = fileInfo.CreateText();
		}
		else
		{
			writer = fileInfo.CreateText();
		}
		writer.Write(value);
		writer.Flush();
		writer.Dispose();
		writer.Close();
	}

	public static void ReadOutTxt(string TextName)
	{
		readerText = "";
		try
		{
			string path = Paths.GetSavePath() + "/" + TextName + ".sav";
			if (File.Exists(path))
			{
				reader = new StreamReader(path, Encoding.UTF8);
				readerText = reader.ReadToEnd();
				readerText = readerText.Replace(huanHangChar, '\n');
				reader.Dispose();
				reader.Close();
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("读取文本出现异常:" + ex.Message + "\n" + ex.StackTrace));
		}
	}

	public static string GetTextNameData(string TextName)
	{
		ReadOutTxt(TextName);
		return readerText;
	}

	public static void InitYSSaveGame()
	{
		SaveJsonData = new JSONObject("");
	}

	public static void CheckAndDelOldSave()
	{
		DirectoryInfo directoryInfo = new DirectoryInfo(Paths.GetSavePath());
		if (directoryInfo.GetFiles("JieSuanData*.sav").Length != 0)
		{
			return;
		}
		FileInfo[] files = directoryInfo.GetFiles("*.sav");
		foreach (FileInfo fileInfo in files)
		{
			if (!(fileInfo.Name == "TuJianSave.sav"))
			{
				fileInfo.Delete();
			}
		}
	}
}
