using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace YSGame
{
	// Token: 0x02000A77 RID: 2679
	public class YSSaveGame
	{
		// Token: 0x06004B41 RID: 19265 RVA: 0x001FFB08 File Offset: 0x001FDD08
		public static void Reset()
		{
			YSSaveGame.saveInt = new Dictionary<string, int>();
			YSSaveGame.saveFloat = new Dictionary<string, float>();
			YSSaveGame.saveString = new Dictionary<string, string>();
			YSSaveGame.saveJSONObject = new Dictionary<string, JSONObject>();
			YSSaveGame.SaveJsonData = new JSONObject(JSONObject.Type.OBJECT);
			YSSaveGame.saveJObject = new Dictionary<string, JObject>();
			jsonData.instance.AvatarBackpackJsonData = null;
		}

		// Token: 0x06004B42 RID: 19266 RVA: 0x001FFB60 File Offset: 0x001FDD60
		public static void save(string name, JSONObject json, string gamePath = "-1")
		{
			JSONObject value = new JSONObject(json.ToString(), -2, false, false);
			YSSaveGame.saveJSONObject[name] = value;
			YSSaveGame.WriteIntoTxt(name, YSSaveGame.saveJSONObject[name].ToString(), gamePath);
		}

		// Token: 0x06004B43 RID: 19267 RVA: 0x001FFBA0 File Offset: 0x001FDDA0
		public static void save(string name, JObject json)
		{
			JObject value = new JObject(json.ToString());
			YSSaveGame.saveJObject[name] = value;
			YSSaveGame.WriteIntoTxt(name, YSSaveGame.saveJObject[name].ToString(), "-1");
		}

		// Token: 0x06004B44 RID: 19268 RVA: 0x001FFBE0 File Offset: 0x001FDDE0
		public static void save(string name, string json, string gamePath = "-1")
		{
			YSSaveGame.saveString[name] = json;
			YSSaveGame.WriteIntoTxt(name, YSSaveGame.saveString[name], gamePath);
		}

		// Token: 0x06004B45 RID: 19269 RVA: 0x001FFC00 File Offset: 0x001FDE00
		public static void save(string name, int json, string gamePath = "-1")
		{
			YSSaveGame.saveInt[name] = json;
			YSSaveGame.WriteIntoTxt(name, YSSaveGame.saveInt[name].ToString(), gamePath);
		}

		// Token: 0x06004B46 RID: 19270 RVA: 0x00075181 File Offset: 0x00073381
		public static string GetString(JSONObject json, string name, string ret = "")
		{
			json.GetField(ref ret, name, null);
			return ret;
		}

		// Token: 0x06004B47 RID: 19271 RVA: 0x00075165 File Offset: 0x00073365
		public static int GetInt(JSONObject json, string name, int ret = 0)
		{
			json.GetField(ref ret, name, null);
			return ret;
		}

		// Token: 0x06004B48 RID: 19272 RVA: 0x00075173 File Offset: 0x00073373
		public static float GetFloat(JSONObject json, string name, float ret = 0f)
		{
			json.GetField(ref ret, name, null);
			return ret;
		}

		// Token: 0x06004B49 RID: 19273 RVA: 0x001FFC33 File Offset: 0x001FDE33
		public static JSONObject GetJsonObject(JSONObject json, string name, JSONObject ret = null)
		{
			return json.GetField(name);
		}

		// Token: 0x06004B4A RID: 19274 RVA: 0x001FFC3C File Offset: 0x001FDE3C
		public static string GetString(string name, string ret = "")
		{
			string result;
			if (YSSaveGame.saveString.ContainsKey(name))
			{
				result = YSSaveGame.saveString[name];
			}
			else
			{
				result = YSSaveGame.GetTextNameData(name);
			}
			return result;
		}

		// Token: 0x06004B4B RID: 19275 RVA: 0x001FFC70 File Offset: 0x001FDE70
		public static JSONObject GetJsonObject(string name, JSONObject json = null)
		{
			JSONObject result;
			if (YSSaveGame.saveJSONObject.ContainsKey(name))
			{
				result = YSSaveGame.saveJSONObject[name];
			}
			else
			{
				result = new JSONObject(YSSaveGame.GetTextNameData(name), -2, false, false);
			}
			return result;
		}

		// Token: 0x06004B4C RID: 19276 RVA: 0x001FFCAC File Offset: 0x001FDEAC
		public static JObject GetJObject(string name, JObject json = null)
		{
			JObject result;
			if (YSSaveGame.saveJObject.ContainsKey(name))
			{
				result = YSSaveGame.saveJObject[name];
			}
			else
			{
				result = new JObject(YSSaveGame.GetTextNameData(name));
			}
			return result;
		}

		// Token: 0x06004B4D RID: 19277 RVA: 0x001FFCE4 File Offset: 0x001FDEE4
		public static int GetInt(string name, int ret = 0)
		{
			int result = ret;
			if (!YSSaveGame.saveInt.ContainsKey(name))
			{
				try
				{
					string textNameData = YSSaveGame.GetTextNameData(name);
					if (textNameData == "")
					{
						return result;
					}
					YSSaveGame.saveInt[name] = int.Parse(textNameData);
					YSSaveGame.SaveJsonData.SetField(name, int.Parse(textNameData));
					result = (int)YSSaveGame.SaveJsonData[name].n;
				}
				catch (Exception ex)
				{
					Debug.LogError(ex.ToString());
					UIPopTip.Inst.Pop(ex.ToString(), PopTipIconType.叹号);
				}
				return result;
			}
			result = YSSaveGame.saveInt[name];
			return result;
		}

		// Token: 0x06004B4E RID: 19278 RVA: 0x001FFD90 File Offset: 0x001FDF90
		public static bool HasKey(string str)
		{
			return new FileInfo(Application.dataPath + "/" + str + ".sav").Exists;
		}

		// Token: 0x06004B4F RID: 19279 RVA: 0x001FFDB6 File Offset: 0x001FDFB6
		public static bool HasFile(string path, string str)
		{
			return new FileInfo(path + "/" + str + ".sav").Exists;
		}

		// Token: 0x06004B50 RID: 19280 RVA: 0x001FFDD8 File Offset: 0x001FDFD8
		public static void WriteIntoTxt(string TextName, string message, string persistentDataPath = "-1")
		{
			string value = message.Replace('\n', YSSaveGame.huanHangChar);
			FileInfo fileInfo;
			if (persistentDataPath == "-1")
			{
				fileInfo = new FileInfo(Paths.GetSavePath() + "/" + TextName + ".sav");
			}
			else
			{
				fileInfo = new FileInfo(persistentDataPath + "/" + TextName + ".sav");
			}
			if (!fileInfo.Exists)
			{
				YSSaveGame.writer = fileInfo.CreateText();
			}
			else
			{
				YSSaveGame.writer = fileInfo.CreateText();
			}
			YSSaveGame.writer.Write(value);
			YSSaveGame.writer.Flush();
			YSSaveGame.writer.Dispose();
			YSSaveGame.writer.Close();
		}

		// Token: 0x06004B51 RID: 19281 RVA: 0x001FFE80 File Offset: 0x001FE080
		public static void ReadOutTxt(string TextName)
		{
			YSSaveGame.readerText = "";
			try
			{
				string path = Paths.GetSavePath() + "/" + TextName + ".sav";
				if (File.Exists(path))
				{
					YSSaveGame.reader = new StreamReader(path, Encoding.UTF8);
					YSSaveGame.readerText = YSSaveGame.reader.ReadToEnd();
					YSSaveGame.readerText = YSSaveGame.readerText.Replace(YSSaveGame.huanHangChar, '\n');
					YSSaveGame.reader.Dispose();
					YSSaveGame.reader.Close();
				}
			}
			catch (Exception ex)
			{
				Debug.LogError("读取文本出现异常:" + ex.Message + "\n" + ex.StackTrace);
			}
		}

		// Token: 0x06004B52 RID: 19282 RVA: 0x001FFF34 File Offset: 0x001FE134
		public static string GetTextNameData(string TextName)
		{
			YSSaveGame.ReadOutTxt(TextName);
			return YSSaveGame.readerText;
		}

		// Token: 0x06004B53 RID: 19283 RVA: 0x001FFF41 File Offset: 0x001FE141
		public static void InitYSSaveGame()
		{
			YSSaveGame.SaveJsonData = new JSONObject("", -2, false, false);
		}

		// Token: 0x06004B54 RID: 19284 RVA: 0x001FFF58 File Offset: 0x001FE158
		public static void CheckAndDelOldSave()
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(Paths.GetSavePath());
			if (directoryInfo.GetFiles("JieSuanData*.sav").Length == 0)
			{
				foreach (FileInfo fileInfo in directoryInfo.GetFiles("*.sav"))
				{
					if (!(fileInfo.Name == "TuJianSave.sav"))
					{
						fileInfo.Delete();
					}
				}
			}
		}

		// Token: 0x04004A66 RID: 19046
		public static Dictionary<string, int> saveInt = new Dictionary<string, int>();

		// Token: 0x04004A67 RID: 19047
		private static Dictionary<string, float> saveFloat = new Dictionary<string, float>();

		// Token: 0x04004A68 RID: 19048
		public static Dictionary<string, string> saveString = new Dictionary<string, string>();

		// Token: 0x04004A69 RID: 19049
		public static Dictionary<string, JSONObject> saveJSONObject = new Dictionary<string, JSONObject>();

		// Token: 0x04004A6A RID: 19050
		public static Dictionary<string, JObject> saveJObject = new Dictionary<string, JObject>();

		// Token: 0x04004A6B RID: 19051
		public static JSONObject SaveJsonData = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04004A6C RID: 19052
		public static int curSaveIndex = -1;

		// Token: 0x04004A6D RID: 19053
		private static StreamWriter writer;

		// Token: 0x04004A6E RID: 19054
		private static StreamReader reader;

		// Token: 0x04004A6F RID: 19055
		private static string readerText = "";

		// Token: 0x04004A70 RID: 19056
		public static char huanHangChar = 'þ';
	}
}
