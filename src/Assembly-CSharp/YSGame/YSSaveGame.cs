using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace YSGame
{
	// Token: 0x02000DA6 RID: 3494
	public class YSSaveGame
	{
		// Token: 0x06005455 RID: 21589 RVA: 0x002315C4 File Offset: 0x0022F7C4
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

		// Token: 0x06005456 RID: 21590 RVA: 0x0023161C File Offset: 0x0022F81C
		public static void save(string name, JSONObject json, string gamePath = "-1")
		{
			JSONObject value = new JSONObject(json.ToString(), -2, false, false);
			YSSaveGame.saveJSONObject[name] = value;
			YSSaveGame.WriteIntoTxt(name, YSSaveGame.saveJSONObject[name].ToString(), gamePath);
		}

		// Token: 0x06005457 RID: 21591 RVA: 0x0023165C File Offset: 0x0022F85C
		public static void save(string name, JObject json)
		{
			JObject value = new JObject(json.ToString());
			YSSaveGame.saveJObject[name] = value;
			YSSaveGame.WriteIntoTxt(name, YSSaveGame.saveJObject[name].ToString(), "-1");
		}

		// Token: 0x06005458 RID: 21592 RVA: 0x0003C654 File Offset: 0x0003A854
		public static void save(string name, string json, string gamePath = "-1")
		{
			YSSaveGame.saveString[name] = json;
			YSSaveGame.WriteIntoTxt(name, YSSaveGame.saveString[name], gamePath);
		}

		// Token: 0x06005459 RID: 21593 RVA: 0x0023169C File Offset: 0x0022F89C
		public static void save(string name, int json, string gamePath = "-1")
		{
			YSSaveGame.saveInt[name] = json;
			YSSaveGame.WriteIntoTxt(name, YSSaveGame.saveInt[name].ToString(), gamePath);
		}

		// Token: 0x0600545A RID: 21594 RVA: 0x0003C674 File Offset: 0x0003A874
		public static string GetString(JSONObject json, string name, string ret = "")
		{
			json.GetField(ref ret, name, null);
			return ret;
		}

		// Token: 0x0600545B RID: 21595 RVA: 0x0003C682 File Offset: 0x0003A882
		public static int GetInt(JSONObject json, string name, int ret = 0)
		{
			json.GetField(ref ret, name, null);
			return ret;
		}

		// Token: 0x0600545C RID: 21596 RVA: 0x0003C690 File Offset: 0x0003A890
		public static float GetFloat(JSONObject json, string name, float ret = 0f)
		{
			json.GetField(ref ret, name, null);
			return ret;
		}

		// Token: 0x0600545D RID: 21597 RVA: 0x0003C69E File Offset: 0x0003A89E
		public static JSONObject GetJsonObject(JSONObject json, string name, JSONObject ret = null)
		{
			return json.GetField(name);
		}

		// Token: 0x0600545E RID: 21598 RVA: 0x002316D0 File Offset: 0x0022F8D0
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

		// Token: 0x0600545F RID: 21599 RVA: 0x00231704 File Offset: 0x0022F904
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

		// Token: 0x06005460 RID: 21600 RVA: 0x00231740 File Offset: 0x0022F940
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

		// Token: 0x06005461 RID: 21601 RVA: 0x00231778 File Offset: 0x0022F978
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

		// Token: 0x06005462 RID: 21602 RVA: 0x0003C6A7 File Offset: 0x0003A8A7
		public static bool HasKey(string str)
		{
			return new FileInfo(Application.dataPath + "/" + str + ".sav").Exists;
		}

		// Token: 0x06005463 RID: 21603 RVA: 0x0003C6CD File Offset: 0x0003A8CD
		public static bool HasFile(string path, string str)
		{
			return new FileInfo(path + "/" + str + ".sav").Exists;
		}

		// Token: 0x06005464 RID: 21604 RVA: 0x00231824 File Offset: 0x0022FA24
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

		// Token: 0x06005465 RID: 21605 RVA: 0x002318CC File Offset: 0x0022FACC
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

		// Token: 0x06005466 RID: 21606 RVA: 0x0003C6EF File Offset: 0x0003A8EF
		public static string GetTextNameData(string TextName)
		{
			YSSaveGame.ReadOutTxt(TextName);
			return YSSaveGame.readerText;
		}

		// Token: 0x06005467 RID: 21607 RVA: 0x0003C6FC File Offset: 0x0003A8FC
		public static void InitYSSaveGame()
		{
			YSSaveGame.SaveJsonData = new JSONObject("", -2, false, false);
		}

		// Token: 0x06005468 RID: 21608 RVA: 0x00231980 File Offset: 0x0022FB80
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

		// Token: 0x0400540B RID: 21515
		public static Dictionary<string, int> saveInt = new Dictionary<string, int>();

		// Token: 0x0400540C RID: 21516
		private static Dictionary<string, float> saveFloat = new Dictionary<string, float>();

		// Token: 0x0400540D RID: 21517
		public static Dictionary<string, string> saveString = new Dictionary<string, string>();

		// Token: 0x0400540E RID: 21518
		public static Dictionary<string, JSONObject> saveJSONObject = new Dictionary<string, JSONObject>();

		// Token: 0x0400540F RID: 21519
		public static Dictionary<string, JObject> saveJObject = new Dictionary<string, JObject>();

		// Token: 0x04005410 RID: 21520
		public static JSONObject SaveJsonData = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005411 RID: 21521
		public static int curSaveIndex = -1;

		// Token: 0x04005412 RID: 21522
		private static StreamWriter writer;

		// Token: 0x04005413 RID: 21523
		private static StreamReader reader;

		// Token: 0x04005414 RID: 21524
		private static string readerText = "";

		// Token: 0x04005415 RID: 21525
		public static char huanHangChar = 'þ';
	}
}
