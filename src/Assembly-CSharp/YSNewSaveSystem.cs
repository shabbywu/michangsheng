using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using CaiYao;
using Fungus;
using GUIPackage;
using KBEngine;
using Newtonsoft.Json.Linq;
using PaiMai;
using QiYu;
using script.ItemSource.Interface;
using script.NewLianDan;
using script.Submit;
using Steamworks;
using Tab;
using UnityEngine;
using YSGame;
using YSGame.TuJian;

// Token: 0x020001D5 RID: 469
public static class YSNewSaveSystem
{
	// Token: 0x060013EE RID: 5102 RVA: 0x0007ED70 File Offset: 0x0007CF70
	public static void UploadCloudSaveData(int slot = 0)
	{
		string text = string.Format("{0}/CloudSave_{1}.zip", Paths.GetCloudSavePath(), slot);
		if (File.Exists(text))
		{
			try
			{
				byte[] array = File.ReadAllBytes(text);
				int num = array.Length;
				bool flag = SteamRemoteStorage.FileWrite(string.Format("CloudSave_{0}.zip", slot), array, num);
				SteamRemoteStorage.SetSyncPlatforms(text, -1);
				if (flag)
				{
					Debug.Log("上传成功");
				}
				else
				{
					Debug.LogError("上传失败");
				}
				return;
			}
			catch (Exception arg)
			{
				Debug.LogError(string.Format("上传云存档失败 异常:{0}", arg));
				return;
			}
		}
		Debug.LogError("上传失败，找不到云存档压缩文件 " + text);
	}

	// Token: 0x060013EF RID: 5103 RVA: 0x0007EE14 File Offset: 0x0007D014
	public static void UploadCloudSaveDataDesc(int slot = 0)
	{
		string text = string.Format("{0}/CloudSave_{1}_Desc.txt", Paths.GetCloudSavePath(), slot);
		if (File.Exists(text))
		{
			try
			{
				byte[] array = File.ReadAllBytes(text);
				int num = array.Length;
				bool flag = SteamRemoteStorage.FileWrite(string.Format("CloudSave_{0}_Desc.txt", slot), array, num);
				SteamRemoteStorage.SetSyncPlatforms(text, -1);
				if (flag)
				{
					Debug.Log("上传备注成功");
				}
				else
				{
					Debug.LogError("上传备注失败");
				}
				return;
			}
			catch (Exception arg)
			{
				Debug.LogError(string.Format("上传云存档备注失败 异常:{0}", arg));
				return;
			}
		}
		Debug.LogError("上传备注失败，找不到备注文件 " + text);
	}

	// Token: 0x060013F0 RID: 5104 RVA: 0x0007EEB8 File Offset: 0x0007D0B8
	public static void ZipAndUploadCloudSaveData(int slot = 0, string desc = "")
	{
		YSZip.ZipFile(Paths.GetNewSavePath(), string.Format("{0}/CloudSave_{1}.zip", Paths.GetCloudSavePath(), slot));
		if (string.IsNullOrEmpty(desc))
		{
			desc = " ";
		}
		File.WriteAllText(string.Format("{0}/CloudSave_{1}_Desc.txt", Paths.GetCloudSavePath(), slot), desc);
		YSNewSaveSystem.UploadCloudSaveData(slot);
		YSNewSaveSystem.UploadCloudSaveDataDesc(slot);
	}

	// Token: 0x060013F1 RID: 5105 RVA: 0x0007EF1C File Offset: 0x0007D11C
	public static void DownloadCloudSave(int slot = 0)
	{
		string text = string.Format("{0}/CloudSave_{1}.zip", Paths.GetCloudSavePath(), slot);
		string text2 = string.Format("CloudSave_{0}.zip", slot);
		try
		{
			if (SteamRemoteStorage.FileExists(text2))
			{
				int fileSize = SteamRemoteStorage.GetFileSize(text2);
				byte[] array = new byte[fileSize];
				SteamRemoteStorage.FileRead(text2, array, fileSize);
				FileInfo fileInfo = new FileInfo(text);
				if (!fileInfo.Directory.Exists)
				{
					fileInfo.Directory.Create();
				}
				File.WriteAllBytes(text, array);
				Debug.Log(string.Concat(new string[]
				{
					"下载云存档 ",
					text2,
					" 到 ",
					text,
					" 完毕"
				}));
			}
			else
			{
				Debug.LogError("下载云存档失败，steam云上不存在文件 " + text2);
			}
		}
		catch (Exception arg)
		{
			Debug.LogError(string.Format("下载云存档失败 异常:{0}", arg));
		}
	}

	// Token: 0x060013F2 RID: 5106 RVA: 0x0007F004 File Offset: 0x0007D204
	public static void DownloadCloudSaveDesc(int slot = 0)
	{
		string text = string.Format("{0}/CloudSave_{1}_Desc.txt", Paths.GetCloudSavePath(), slot);
		string text2 = string.Format("CloudSave_{0}_Desc.txt", slot);
		try
		{
			if (SteamRemoteStorage.FileExists(text2))
			{
				int fileSize = SteamRemoteStorage.GetFileSize(text2);
				byte[] array = new byte[fileSize];
				SteamRemoteStorage.FileRead(text2, array, fileSize);
				FileInfo fileInfo = new FileInfo(text);
				if (!fileInfo.Directory.Exists)
				{
					fileInfo.Directory.Create();
				}
				File.WriteAllBytes(text, array);
				Debug.Log(string.Concat(new string[]
				{
					"下载云存档备注 ",
					text2,
					" 到 ",
					text,
					" 完毕"
				}));
			}
			else
			{
				Debug.LogError("下载云存档备注失败，steam云上不存在文件 " + text2);
			}
		}
		catch (Exception arg)
		{
			Debug.LogError(string.Format("下载云存档备注失败 异常:{0}", arg));
		}
	}

	// Token: 0x060013F3 RID: 5107 RVA: 0x0007F0EC File Offset: 0x0007D2EC
	public static List<CloudSaveFileData> GetCloudSaveData()
	{
		List<CloudSaveFileData> list = new List<CloudSaveFileData>();
		for (int i = 0; i < YSNewSaveSystem.CloudSaveSlotCountLimit; i++)
		{
			CloudSaveFileData cloudSaveFileData = new CloudSaveFileData();
			cloudSaveFileData.FileName = string.Format("CloudSave_{0}.zip", i);
			try
			{
				if (SteamRemoteStorage.FileExists(cloudSaveFileData.FileName))
				{
					long fileTimestamp = SteamRemoteStorage.GetFileTimestamp(cloudSaveFileData.FileName);
					DateTime fileTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)).AddSeconds((double)fileTimestamp);
					cloudSaveFileData.FileTime = fileTime;
					cloudSaveFileData.FileSize = SteamRemoteStorage.GetFileSize(cloudSaveFileData.FileName);
					cloudSaveFileData.HasFile = true;
				}
				if (SteamRemoteStorage.FileExists(string.Format("CloudSave_{0}_Desc.txt", i)))
				{
					YSNewSaveSystem.DownloadCloudSaveDesc(i);
					string fileDesc = File.ReadAllText(string.Format("{0}/CloudSave_{1}_Desc.txt", Paths.GetCloudSavePath(), i));
					cloudSaveFileData.FileDesc = fileDesc;
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(string.Format("获取云存档文件信息时出现异常: {0}", ex));
				cloudSaveFileData.HasFile = false;
				throw ex;
			}
			cloudSaveFileData.Slot = i;
			list.Add(cloudSaveFileData);
		}
		return list;
	}

	// Token: 0x060013F4 RID: 5108 RVA: 0x0007F20C File Offset: 0x0007D40C
	public static void DeleteCloudSave(int slot)
	{
		if (SteamRemoteStorage.FileExists(string.Format("CloudSave_{0}.zip", slot)))
		{
			SteamRemoteStorage.FileDelete(string.Format("CloudSave_{0}.zip", slot));
		}
		if (SteamRemoteStorage.FileExists(string.Format("CloudSave_{0}_Desc.txt", slot)))
		{
			SteamRemoteStorage.FileDelete(string.Format("CloudSave_{0}_Desc.txt", slot));
		}
	}

	// Token: 0x060013F5 RID: 5109 RVA: 0x0007F274 File Offset: 0x0007D474
	public static void LogCloudFiles()
	{
		int fileCount = SteamRemoteStorage.GetFileCount();
		Debug.Log(string.Format("云上共有{0}个文件", fileCount));
		for (int i = 0; i < fileCount; i++)
		{
			int num;
			string fileNameAndSize = SteamRemoteStorage.GetFileNameAndSize(i, ref num);
			long fileTimestamp = SteamRemoteStorage.GetFileTimestamp(fileNameAndSize);
			int num2 = num / 1024;
			DateTime dateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)).AddSeconds((double)fileTimestamp);
			Debug.Log(string.Format("index:{0} size:{1}kb time:{2} file:{3}", new object[]
			{
				i,
				num2,
				dateTime,
				fileNameAndSize
			}));
		}
	}

	// Token: 0x060013F6 RID: 5110 RVA: 0x0007F31C File Offset: 0x0007D51C
	public static void Reset()
	{
		YSNewSaveSystem.saveInt = new Dictionary<string, int>();
		YSNewSaveSystem.saveString = new Dictionary<string, string>();
		YSNewSaveSystem.saveJSONObject = new Dictionary<string, JSONObject>();
		YSNewSaveSystem.SaveJsonData = new JSONObject(JSONObject.Type.OBJECT);
		jsonData.instance.AvatarBackpackJsonData = null;
	}

	// Token: 0x060013F7 RID: 5111 RVA: 0x0007F352 File Offset: 0x0007D552
	public static string GetAvatarSavePathPre(int avatarIndex, int slot)
	{
		return string.Format("Avatar{0}/Slot{1}", avatarIndex, slot);
	}

	// Token: 0x060013F8 RID: 5112 RVA: 0x0007F36C File Offset: 0x0007D56C
	public static void Save(string fileName, JSONObject json, bool autoPath = true)
	{
		JSONObject value = new JSONObject(json.ToString(), -2, false, false);
		YSNewSaveSystem.saveJSONObject[fileName] = value;
		YSNewSaveSystem.WriteIntoTxt(fileName, YSNewSaveSystem.saveJSONObject[fileName].ToString(), autoPath);
	}

	// Token: 0x060013F9 RID: 5113 RVA: 0x0007F3AC File Offset: 0x0007D5AC
	public static void Save(string fileName, string value, bool autoPath = true)
	{
		YSNewSaveSystem.saveString[fileName] = value;
		YSNewSaveSystem.WriteIntoTxt(fileName, YSNewSaveSystem.saveString[fileName], autoPath);
	}

	// Token: 0x060013FA RID: 5114 RVA: 0x0007F3CC File Offset: 0x0007D5CC
	public static void Save(string fileName, int value, bool autoPath = true)
	{
		YSNewSaveSystem.saveInt[fileName] = value;
		YSNewSaveSystem.WriteIntoTxt(fileName, YSNewSaveSystem.saveInt[fileName].ToString(), autoPath);
	}

	// Token: 0x060013FB RID: 5115 RVA: 0x0007F400 File Offset: 0x0007D600
	public static JSONObject GetJsonObject(string name, JSONObject json = null)
	{
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		JSONObject result;
		if (YSNewSaveSystem.saveJSONObject.ContainsKey(name))
		{
			result = YSNewSaveSystem.saveJSONObject[name];
		}
		else
		{
			result = new JSONObject(YSNewSaveSystem.ReadText(name), -2, false, false);
		}
		stopwatch.Stop();
		return result;
	}

	// Token: 0x060013FC RID: 5116 RVA: 0x0007F44C File Offset: 0x0007D64C
	public static int GetInt(string name, int ret = 0)
	{
		int result = ret;
		if (!YSNewSaveSystem.saveInt.ContainsKey(name))
		{
			try
			{
				string text = YSNewSaveSystem.ReadText(name);
				if (string.IsNullOrWhiteSpace(text))
				{
					return result;
				}
				YSNewSaveSystem.saveInt[name] = int.Parse(text);
				YSNewSaveSystem.SaveJsonData.SetField(name, int.Parse(text));
				result = (int)YSNewSaveSystem.SaveJsonData[name].n;
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
				UIPopTip.Inst.Pop(ex.ToString(), PopTipIconType.叹号);
			}
			return result;
		}
		result = YSNewSaveSystem.saveInt[name];
		return result;
	}

	// Token: 0x060013FD RID: 5117 RVA: 0x0007F4F4 File Offset: 0x0007D6F4
	public static int LoadInt(string fileName, bool autoPath = true)
	{
		try
		{
			string fileName2 = fileName;
			if (autoPath)
			{
				fileName2 = YSNewSaveSystem.NowAvatarPathPre + "/" + fileName;
			}
			string text = YSNewSaveSystem.ReadText(fileName2);
			if (text != null && text != "")
			{
				return int.Parse(text);
			}
		}
		catch (Exception)
		{
			return 0;
		}
		return 0;
	}

	// Token: 0x060013FE RID: 5118 RVA: 0x0007F554 File Offset: 0x0007D754
	public static JSONObject LoadJSONObject(string fileName, bool autoPath = true)
	{
		try
		{
			string fileName2 = fileName;
			if (autoPath)
			{
				fileName2 = YSNewSaveSystem.NowAvatarPathPre + "/" + fileName;
			}
			string text = YSNewSaveSystem.ReadText(fileName2);
			if (text != null && text != "")
			{
				return new JSONObject(text, -2, false, false);
			}
		}
		catch (Exception)
		{
			return new JSONObject();
		}
		return new JSONObject();
	}

	// Token: 0x060013FF RID: 5119 RVA: 0x0007F5C0 File Offset: 0x0007D7C0
	public static bool HasFile(string path)
	{
		return new FileInfo(path).Exists;
	}

	// Token: 0x06001400 RID: 5120 RVA: 0x0007F5D4 File Offset: 0x0007D7D4
	public static void WriteIntoTxt(string fileName, string text, bool autoPath = true)
	{
		try
		{
			string fileName2;
			if (autoPath)
			{
				fileName2 = string.Concat(new string[]
				{
					Paths.GetNewSavePath(),
					"/",
					YSNewSaveSystem.NowAvatarPathPre,
					"/",
					fileName
				});
			}
			else
			{
				fileName2 = Paths.GetNewSavePath() + "/" + fileName;
			}
			string value = text.Replace('\n', YSNewSaveSystem.huanHangChar).ToCN();
			FileInfo fileInfo = new FileInfo(fileName2);
			if (!fileInfo.Directory.Exists)
			{
				fileInfo.Directory.Create();
				Debug.Log("创建" + fileInfo.Directory.FullName);
			}
			Debug.Log("开始写入文件" + fileInfo.FullName);
			YSNewSaveSystem.writer = fileInfo.CreateText();
			YSNewSaveSystem.writer.Write(value);
			YSNewSaveSystem.writer.Flush();
			YSNewSaveSystem.writer.Dispose();
			YSNewSaveSystem.writer.Close();
		}
		catch (Exception ex)
		{
			Debug.LogError("在写入存档文件时发生异常:");
			Debug.LogException(ex);
		}
	}

	// Token: 0x06001401 RID: 5121 RVA: 0x0007F6E8 File Offset: 0x0007D8E8
	public static string ReadText(string fileName)
	{
		string text = "";
		try
		{
			string path = Paths.GetNewSavePath() + "/" + fileName;
			if (File.Exists(path))
			{
				YSNewSaveSystem.reader = new StreamReader(path, Encoding.UTF8);
				text = YSNewSaveSystem.reader.ReadToEnd();
				text = text.Replace(YSNewSaveSystem.huanHangChar, '\n');
				YSNewSaveSystem.reader.Dispose();
				YSNewSaveSystem.reader.Close();
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("在读取存档文件时发生异常:");
			Debug.LogException(ex);
		}
		return text;
	}

	// Token: 0x06001402 RID: 5122 RVA: 0x0007F778 File Offset: 0x0007D978
	public static void AutoLoad()
	{
		YSNewSaveSystem.LoadSave(PlayerPrefs.GetInt("NowPlayerFileAvatar"), 1, -1);
	}

	// Token: 0x06001403 RID: 5123 RVA: 0x0007F78B File Offset: 0x0007D98B
	public static void AutoSave()
	{
		if (SingletonMono<TabUIMag>.Instance != null)
		{
			SingletonMono<TabUIMag>.Instance.TryEscClose();
		}
		YSNewSaveSystem.SaveGame(PlayerPrefs.GetInt("NowPlayerFileAvatar"), 1, null, false);
	}

	// Token: 0x06001404 RID: 5124 RVA: 0x0007F7B8 File Offset: 0x0007D9B8
	public static void DeleteSave(int avatarIndex)
	{
		string path = string.Format("{0}/Avatar{1}", Paths.GetNewSavePath(), avatarIndex);
		if (Directory.Exists(path))
		{
			try
			{
				Directory.Delete(path, true);
				Debug.Log(string.Format("删除了存档{0}", avatarIndex));
			}
			catch (Exception arg)
			{
				Debug.LogError(string.Format("删除存档{0}时出现异常，{1}", avatarIndex, arg));
			}
		}
	}

	// Token: 0x06001405 RID: 5125 RVA: 0x0007F82C File Offset: 0x0007DA2C
	public static void CloseUI()
	{
		if (SayDialog.GetSayDialog().gameObject != null)
		{
			Object.Destroy(SayDialog.GetSayDialog().gameObject);
		}
		if (SetFaceUI.Inst != null)
		{
			Object.Destroy(SetFaceUI.Inst.gameObject);
		}
		if (FpUIMag.inst != null)
		{
			Object.Destroy(FpUIMag.inst.gameObject);
		}
		if (LianDanUIMag.Instance != null)
		{
			Object.Destroy(LianDanUIMag.Instance.gameObject);
		}
		if (TpUIMag.inst != null)
		{
			Object.Destroy(TpUIMag.inst.gameObject);
		}
		if (SubmitUIMag.Inst != null)
		{
			SubmitUIMag.Inst.Close();
		}
		if (QiYuUIMag.Inst != null)
		{
			Object.Destroy(QiYuUIMag.Inst.gameObject);
		}
		if (CaiYaoUIMag.Inst != null)
		{
			Object.Destroy(CaiYaoUIMag.Inst.gameObject);
		}
		if (PanelMamager.inst.UISceneGameObject != null)
		{
			PanelMamager.inst.UISceneGameObject.SetActive(false);
		}
		if (SingletonMono<TabUIMag>.Instance != null)
		{
			SingletonMono<TabUIMag>.Instance.TryEscClose();
		}
		if (LianQiTotalManager.inst != null)
		{
			Object.Destroy(LianQiTotalManager.inst.gameObject);
		}
		if (SingletonMono<PaiMaiUiMag>.Instance != null)
		{
			Object.Destroy(SingletonMono<PaiMaiUiMag>.Instance.gameObject);
			Time.timeScale = 1f;
		}
		ESCCloseManager.Inst.CloseAll();
	}

	// Token: 0x06001406 RID: 5126 RVA: 0x0007F9A0 File Offset: 0x0007DBA0
	public static void LoadOldSave(int Id, int Index)
	{
		YSNewSaveSystem.CloseUI();
		YSGame.YSSaveGame.Reset();
		KBEngineApp.app.entities[10] = null;
		KBEngineApp.app.entities.Remove(10);
		GameObject gameObject = new GameObject();
		gameObject.AddComponent<StartGame>();
		gameObject.GetComponent<StartGame>().startGame(Id, Index, -1);
	}

	// Token: 0x06001407 RID: 5127 RVA: 0x0007F9F4 File Offset: 0x0007DBF4
	public static SaveSlotData GetAvatarSaveData(int avatarIndex, int slot)
	{
		SaveSlotData saveSlotData = new SaveSlotData();
		if (YSNewSaveSystem.CheckHasNewSaveAvatarInfo(avatarIndex, slot))
		{
			saveSlotData.HasSave = true;
			saveSlotData.IsNewSaveSystem = true;
		}
		else
		{
			saveSlotData.IsNewSaveSystem = false;
			if (YSNewSaveSystem.CheckHasOldSaveAvatarInfo(avatarIndex, slot))
			{
				saveSlotData.HasSave = true;
			}
			else
			{
				saveSlotData.HasSave = false;
			}
		}
		if (saveSlotData.HasSave)
		{
			JSONObject jsonobject = null;
			bool flag = false;
			if (saveSlotData.IsNewSaveSystem)
			{
				try
				{
					string avatarSavePathPre = YSNewSaveSystem.GetAvatarSavePathPre(avatarIndex, slot);
					jsonobject = YSNewSaveSystem.GetJsonObject(avatarSavePathPre + "/AvatarInfo.json", null);
					saveSlotData.RealSaveTime = YSNewSaveSystem.ReadText(avatarSavePathPre + "/AvatarSavetime.txt");
					goto IL_CF;
				}
				catch
				{
					goto IL_CF;
				}
			}
			try
			{
				jsonobject = YSGame.YSSaveGame.GetJsonObject("AvatarInfo" + Tools.instance.getSaveID(avatarIndex, slot), null);
				saveSlotData.RealSaveTime = YSGame.YSSaveGame.GetTextNameData("AvatarSavetime" + Tools.instance.getSaveID(avatarIndex, slot));
			}
			catch
			{
			}
			IL_CF:
			try
			{
				saveSlotData.AvatarLevel = jsonobject["avatarLevel"].I;
				JSONObject jsonobject2 = jsonData.instance.LevelUpDataJsonData[saveSlotData.AvatarLevel.ToString()];
				saveSlotData.AvatarLevelText = jsonobject2["Name"].Str;
				saveSlotData.AvatarLevelSprite = ResManager.inst.LoadSprite(string.Format("NewUI/Fight/LevelIcon/icon_{0}", saveSlotData.AvatarLevel));
				DateTime dateTime = DateTime.Parse(jsonobject["gameTime"].Str);
				saveSlotData.GameTime = string.Format("{0}年{1}月{2}日", dateTime.Year, dateTime.Month, dateTime.Day);
				flag = true;
			}
			catch
			{
			}
			if (!flag)
			{
				saveSlotData.IsBreak = true;
			}
		}
		return saveSlotData;
	}

	// Token: 0x06001408 RID: 5128 RVA: 0x0007FBC4 File Offset: 0x0007DDC4
	private static bool CheckHasNewSaveAvatarInfo(int avatarIndex, int slot)
	{
		string avatarSavePathPre = YSNewSaveSystem.GetAvatarSavePathPre(avatarIndex, slot);
		bool result;
		if (YSNewSaveSystem.HasFile(Paths.GetNewSavePath() + "/" + avatarSavePathPre + "/AvatarInfo.json"))
		{
			if (YSNewSaveSystem.GetJsonObject(avatarSavePathPre + "/AvatarInfo.json", null).IsNull)
			{
				result = false;
			}
			else
			{
				result = true;
				if (YSNewSaveSystem.LoadInt(avatarSavePathPre + "/GameVersion.txt", true) > 4 && !YSNewSaveSystem.HasFile(Paths.GetNewSavePath() + "/" + avatarSavePathPre + "/IsComplete.txt"))
				{
					result = false;
				}
			}
		}
		else
		{
			result = false;
		}
		return result;
	}

	// Token: 0x06001409 RID: 5129 RVA: 0x0007FC4C File Offset: 0x0007DE4C
	private static bool CheckHasOldSaveAvatarInfo(int avatarIndex, int slot)
	{
		bool result;
		if (YSGame.YSSaveGame.HasFile(Paths.GetSavePath(), "AvatarInfo" + Tools.instance.getSaveID(avatarIndex, slot)))
		{
			if (YSGame.YSSaveGame.GetJsonObject("AvatarInfo" + Tools.instance.getSaveID(avatarIndex, slot), null).IsNull)
			{
				result = false;
			}
			else
			{
				result = true;
				if (FactoryManager.inst.SaveLoadFactory.GetInt("GameVersion" + Tools.instance.getSaveID(avatarIndex, slot)) > 4 && !YSGame.YSSaveGame.HasFile(Paths.GetSavePath(), "IsComplete" + Tools.instance.getSaveID(avatarIndex, slot)))
				{
					result = false;
				}
			}
		}
		else
		{
			result = false;
		}
		return result;
	}

	// Token: 0x0600140A RID: 5130 RVA: 0x0007FCF8 File Offset: 0x0007DEF8
	public static void LoadSave(int avatarIndex, int slot, int DFIndex = -1)
	{
		ABItemSourceMag.SetNull();
		if (DFIndex < 0)
		{
			Tools.instance.IsInDF = false;
		}
		YSNewSaveSystem.CloseUI();
		YSNewSaveSystem.Reset();
		KBEngineApp.app.entities[10] = null;
		KBEngineApp.app.entities.Remove(10);
		YSNewSaveSystem.NowUsingAvatarIndex = avatarIndex;
		YSNewSaveSystem.NowUsingSlot = slot;
		YSNewSaveSystem.NowAvatarPathPre = (YSNewSaveSystem.GetAvatarSavePathPre(avatarIndex, slot) ?? "");
		Tools.instance.IsCanLoadSetTalk = false;
		MusicMag.instance.stopMusic();
		YSNewSaveSystem.AddAvatar(avatarIndex, slot);
		Avatar player = PlayerEx.Player;
		YSNewSaveSystem.LoadStreamData(player);
		player.nomelTaskMag.restAllTaskType();
		if (player.age > player.shouYuan)
		{
			UIDeath.Inst.Show(DeathType.寿元已尽);
			return;
		}
		if (player.lastScence == "LoadingScreen" || player.lastScence == "" || player.lastScence == "MainMenu")
		{
			player.lastScence = "AllMaps";
			if (DFIndex > 0)
			{
				Tools.instance.IsInDF = true;
				Tools.instance.getPlayer().lastScence = "S" + DFIndex;
			}
			else
			{
				Tools.instance.IsInDF = false;
			}
		}
		PlayerPrefs.SetString("sceneToLoad", player.lastScence);
		Fader fader = Object.FindObjectOfType<Fader>();
		Tools.instance.IsLoadData = true;
		if (DFIndex > 0)
		{
			FactoryManager.inst.loadPlayerDateFactory.isLoadComplete = true;
		}
		else
		{
			FactoryManager.inst.loadPlayerDateFactory.NewLoadPlayerData(avatarIndex, slot);
			Tools.instance.ResetEquipSeid();
		}
		if (fader == null)
		{
			Tools.instance.loadOtherScenes("LoadingScreen");
			return;
		}
		fader.FadeIntoLevel("LoadingScreen");
	}

	// Token: 0x0600140B RID: 5131 RVA: 0x0007FEA8 File Offset: 0x0007E0A8
	public static void AddAvatar(int id, int index)
	{
		YSNewSaveSystem.CreatAvatar(10, 51, 40, new Vector3(-5f, -1.7f, -1f), new Vector3(0f, 0f, 80f), 1);
		KBEngineApp.app.entity_id = 10;
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		YSNewSaveSystem.LoadAvatar(avatar);
		YSNewSaveSystem.InitSkill();
		YSNewSaveSystem.LoadAvatarFace(id, index);
		StaticSkill.resetSeid(avatar);
		WuDaoStaticSkill.resetWuDaoSeid(avatar);
		JieDanSkill.resetJieDanSeid(avatar);
		PlayerPrefs.SetInt("NowPlayerFileAvatar", id);
		avatar.seaNodeMag.INITSEA();
	}

	// Token: 0x0600140C RID: 5132 RVA: 0x0007FF40 File Offset: 0x0007E140
	public static void LoadAvatarFace(int id, int index)
	{
		jsonData instance = jsonData.instance;
		instance.AvatarRandomJsonData = YSNewSaveSystem.LoadJSONObject("AvatarRandomJsonData.json", true);
		instance.AvatarBackpackJsonData = YSNewSaveSystem.LoadJSONObject("AvatarBackpackJsonData.json", true);
		if (instance.AvatarRandomJsonData.Count != instance.AvatarJsonData.Count || instance.reloadRandomAvatarFace)
		{
			YSNewSaveSystem.Save("FirstSetAvatarRandomJsonData.txt", 0, true);
			YSNewSaveSystem.NewInitAvatarFace(id, index, 1);
			instance.IsResetAvatarFace = true;
			List<int> list = new List<int>();
			foreach (KeyValuePair<string, JToken> keyValuePair in instance.ResetAvatarBackpackBanBen)
			{
				if (!list.Contains((int)keyValuePair.Value["BanBenID"]))
				{
					list.Add((int)keyValuePair.Value["BanBenID"]);
				}
			}
			Tools.instance.getPlayer().BanBenHao = list.Max();
		}
	}

	// Token: 0x0600140D RID: 5133 RVA: 0x00080044 File Offset: 0x0007E244
	public static void InitSkill()
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		avatar.equipSkillList = avatar.configEquipSkill[avatar.nowConfigEquipSkill];
		avatar.equipStaticSkillList = avatar.configEquipStaticSkill[avatar.nowConfigEquipStaticSkill];
	}

	// Token: 0x0600140E RID: 5134 RVA: 0x00080088 File Offset: 0x0007E288
	public static void SetAvatar(int avaterID, int roleType, int HP_Max, Vector3 position, Vector3 direction, Avatar avatar, int AvatarID = 1)
	{
		avatar.position = position;
		avatar.direction = direction;
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[string.Concat(AvatarID)];
		int num = 0;
		foreach (JSONObject jsonobject2 in jsonobject["skills"].list)
		{
			avatar.addHasSkillList((int)jsonobject2.n);
			avatar.equipSkill((int)jsonobject2.n, num);
			num++;
		}
		int num2 = 0;
		foreach (JSONObject jsonobject3 in jsonobject["staticSkills"].list)
		{
			avatar.addHasStaticSkillList((int)jsonobject3.n, 1);
			avatar.equipStaticSkill((int)jsonobject3.n, num2);
			num2++;
		}
		for (int j = 0; j < jsonobject["LingGen"].Count; j++)
		{
			int item = (int)jsonobject["LingGen"][j].n;
			avatar.LingGeng.Add(item);
		}
		avatar.ZiZhi = (int)jsonobject["ziZhi"].n;
		avatar.dunSu = (int)jsonobject["dunSu"].n;
		avatar.wuXin = (uint)jsonobject["wuXin"].n;
		avatar.shengShi = (int)jsonobject["shengShi"].n;
		avatar.shaQi = (uint)jsonobject["shaQi"].n;
		avatar.shouYuan = (uint)jsonobject["shouYuan"].n;
		avatar.age = (uint)jsonobject["age"].n;
		avatar.HP_Max = (int)jsonobject["HP"].n;
		avatar.HP = (int)jsonobject["HP"].n;
		avatar.money = (ulong)((uint)jsonobject["MoneyType"].n);
		avatar.level = (ushort)jsonobject["Level"].n;
		avatar.AvatarType = (uint)((ushort)jsonobject["AvatarType"].n);
		avatar.roleTypeCell = (uint)jsonobject["fightFace"].n;
		avatar.roleType = (uint)jsonobject["face"].n;
		avatar.Sex = (int)jsonobject["SexType"].n;
		avatar.configEquipSkill[0] = avatar.equipSkillList;
		avatar.configEquipStaticSkill[0] = avatar.equipStaticSkillList;
		avatar.equipItemList.values.ForEach(delegate(ITEM_INFO i)
		{
			avatar.configEquipItem[0].values.Add(i);
		});
	}

	// Token: 0x0600140F RID: 5135 RVA: 0x00080408 File Offset: 0x0007E608
	public static void CreatAvatar(int avaterID, int roleType, int HP_Max, Vector3 position, Vector3 direction, int AvatarID = 1)
	{
		KBEngineApp.app.Client_onCreatedProxies((ulong)((long)avaterID), avaterID, "Avatar");
		Avatar avatar = (Avatar)KBEngineApp.app.entities[avaterID];
		YSNewSaveSystem.SetAvatar(avaterID, roleType, HP_Max, position, direction, avatar, AvatarID);
	}

	// Token: 0x06001410 RID: 5136 RVA: 0x0008044C File Offset: 0x0007E64C
	public static void LoadAvatar(Avatar avatar)
	{
		JSONObject jsonobject = YSNewSaveSystem.LoadJSONObject("Avatar.json", true);
		FieldInfo[] fields = typeof(Avatar).GetFields();
		int i = 0;
		while (i < fields.Length)
		{
			string name = fields[i].Name;
			string name2 = fields[i].FieldType.Name;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(name2);
			if (num <= 1615808600U)
			{
				if (num <= 1108380682U)
				{
					if (num <= 765439473U)
					{
						if (num != 697196164U)
						{
							if (num == 765439473U)
							{
								if (name2 == "Int16")
								{
									goto IL_387;
								}
							}
						}
						else if (name2 == "Int64")
						{
							goto IL_36B;
						}
					}
					else if (num != 815609665U)
					{
						if (num != 851515688U)
						{
							if (num == 1108380682U)
							{
								if (name2 == "WorldTime")
								{
									WorldTime worldTime = new WorldTime();
									worldTime.isLoadDate = true;
									jsonobject.GetString("worldTimeMag", "0001-1-1");
									worldTime.nowTime = jsonobject.GetString("worldTimeMag", "0001-1-1");
									fields[i].SetValue(avatar, worldTime);
								}
							}
						}
						else if (name2 == "ITEM_INFO_LIST")
						{
							ITEM_INFO_LIST item_INFO_LIST = new ITEM_INFO_LIST();
							foreach (JSONObject jsonobject2 in jsonobject.GetField(name).list)
							{
								ITEM_INFO item_INFO = new ITEM_INFO();
								item_INFO.uuid = jsonobject2.GetString("UUID", "");
								if (item_INFO.uuid == "")
								{
									item_INFO.uuid = Tools.getUUID();
								}
								item_INFO.itemId = jsonobject2.GetInt("id", 0);
								item_INFO.itemCount = (uint)jsonobject2.GetInt("count", 0);
								item_INFO.itemIndex = jsonobject2.GetInt("index", 0);
								item_INFO.Seid = jsonobject2.GetField("Seid");
								item_INFO_LIST.values.Add(item_INFO);
							}
							fields[i].SetValue(avatar, item_INFO_LIST);
						}
					}
					else if (name2 == "uInt")
					{
						goto IL_36B;
					}
				}
				else if (num <= 1323747186U)
				{
					if (num != 1283547685U)
					{
						if (num == 1323747186U)
						{
							if (name2 == "UInt16")
							{
								goto IL_387;
							}
						}
					}
					else if (name2 == "Float")
					{
						fields[i].SetValue(avatar, jsonobject.GetFloat(name, 0f));
					}
				}
				else if (num != 1324880019U)
				{
					if (num != 1438686222U)
					{
						if (num == 1615808600U)
						{
							if (name2 == "String")
							{
								fields[i].SetValue(avatar, jsonobject.GetString(name, ""));
							}
						}
					}
					else if (name2 == "List`1[]")
					{
						if (fields[i].Name == "configEquipSkill" || fields[i].Name == "configEquipStaticSkill")
						{
							List<SkillItem>[] array = new List<SkillItem>[]
							{
								new List<SkillItem>(),
								new List<SkillItem>(),
								new List<SkillItem>(),
								new List<SkillItem>(),
								new List<SkillItem>()
							};
							JSONObject field = jsonobject.GetField(name);
							for (int j = 0; j < 5; j++)
							{
								foreach (JSONObject jsonobject3 in field.list[j].list)
								{
									SkillItem skillItem = new SkillItem();
									skillItem.uuid = jsonobject3.GetString("uuid", "");
									skillItem.itemId = jsonobject3.GetInt("id", 0);
									skillItem.level = jsonobject3.GetInt("level", 0);
									skillItem.itemIndex = jsonobject3.GetInt("index", 0);
									skillItem.Seid = jsonobject3.GetField("Seid");
									array[j].Add(skillItem);
								}
							}
							fields[i].SetValue(avatar, array);
						}
					}
				}
				else if (name2 == "UInt64")
				{
					goto IL_387;
				}
			}
			else if (num <= 2388225411U)
			{
				if (num <= 1907276658U)
				{
					if (num != 1731900476U)
					{
						if (num == 1907276658U)
						{
							if (name2 == "JObject")
							{
								JSONObject field2 = jsonobject.GetField(name);
								if (field2 != null)
								{
									JObject value = JObject.Parse(field2.ToString());
									fields[i].SetValue(avatar, value);
								}
							}
						}
					}
					else if (name2 == "ITEM_INFO_LIST[]")
					{
						ITEM_INFO_LIST[] array2 = new ITEM_INFO_LIST[]
						{
							new ITEM_INFO_LIST(),
							new ITEM_INFO_LIST(),
							new ITEM_INFO_LIST(),
							new ITEM_INFO_LIST(),
							new ITEM_INFO_LIST()
						};
						JSONObject field3 = jsonobject.GetField(name);
						for (int k = 0; k < 5; k++)
						{
							foreach (JSONObject jsonobject4 in field3.list[k].list)
							{
								ITEM_INFO item_INFO2 = new ITEM_INFO();
								item_INFO2.uuid = jsonobject4.GetString("uuid", "");
								if (item_INFO2.uuid == "")
								{
									item_INFO2.uuid = Tools.getUUID();
								}
								item_INFO2.itemId = jsonobject4.GetInt("id", 0);
								item_INFO2.itemCount = (uint)jsonobject4.GetInt("count", 0);
								item_INFO2.itemIndex = jsonobject4.GetInt("index", 0);
								item_INFO2.Seid = jsonobject4.GetField("Seid");
								array2[k].values.Add(item_INFO2);
							}
						}
						fields[i].SetValue(avatar, array2);
					}
				}
				else if (num != 1926157539U)
				{
					if (num != 1966515832U)
					{
						if (num == 2388225411U)
						{
							if (name2 == "TaskMag")
							{
								JSONObject field4 = jsonobject.GetField(name);
								TaskMag taskMag = new TaskMag(avatar);
								taskMag._TaskData = field4;
								fields[i].SetValue(avatar, taskMag);
							}
						}
					}
					else if (name2 == "JSONObject")
					{
						JSONObject field5 = jsonobject.GetField(name);
						fields[i].SetValue(avatar, field5);
					}
				}
				else if (name2 == "AvatarStaticValue")
				{
					JSONObject field6 = jsonobject.GetField(name);
					List<int> list = field6.GetField("value").ToList();
					AvatarStaticValue avatarStaticValue = new AvatarStaticValue();
					for (int l = 0; l < list.Count; l++)
					{
						avatarStaticValue.Value[l] = list[l];
					}
					List<int> list2 = field6.GetField("talk").ToList();
					if (list2.Count < 2)
					{
						list2.Add(0);
						list2.Add(0);
					}
					avatarStaticValue.talk = list2.ToArray();
					fields[i].SetValue(avatar, avatarStaticValue);
				}
			}
			else if (num <= 2935746502U)
			{
				if (num != 2711245919U)
				{
					if (num == 2935746502U)
					{
						if (name2 == "List`1")
						{
							if (fields[i].Name == "hasSkillList" || fields[i].Name == "hasStaticSkillList" || fields[i].Name == "hasJieDanSkillList" || fields[i].Name == "equipSkillList" || fields[i].Name == "equipStaticSkillList")
							{
								List<SkillItem> list3 = new List<SkillItem>();
								foreach (JSONObject jsonobject5 in jsonobject.GetField(name).list)
								{
									SkillItem skillItem2 = new SkillItem();
									skillItem2.uuid = jsonobject5.GetString("UUID", "");
									if (skillItem2.uuid == "")
									{
										skillItem2.uuid = Tools.getUUID();
									}
									skillItem2.itemId = jsonobject5.GetInt("id", 0);
									skillItem2.level = jsonobject5.GetInt("level", 0);
									skillItem2.itemIndex = jsonobject5.GetInt("index", 0);
									skillItem2.itemId = jsonobject5.GetInt("id", 0);
									skillItem2.Seid = jsonobject5.GetField("Seid");
									list3.Add(skillItem2);
								}
								fields[i].SetValue(avatar, list3);
							}
							if (fields[i].Name == "bufflist")
							{
								if (jsonobject == null || jsonobject.list == null || jsonobject.list.Count == 0)
								{
									goto IL_BD9;
								}
								List<List<int>> list4 = new List<List<int>>();
								foreach (JSONObject jsonobject6 in jsonobject.GetField(name).list)
								{
									list4.Add(jsonobject6.ToList());
								}
								fields[i].SetValue(avatar, list4);
							}
							if (fields[i].Name == "LingGeng")
							{
								JSONObject field7 = jsonobject.GetField(name);
								fields[i].SetValue(avatar, field7.ToList());
							}
						}
					}
				}
				else if (name2 == "Int32")
				{
					goto IL_36B;
				}
			}
			else if (num != 2940794790U)
			{
				if (num != 3538687084U)
				{
					if (num == 4168357374U)
					{
						if (name2 == "Int")
						{
							goto IL_36B;
						}
					}
				}
				else if (name2 == "UInt32")
				{
					goto IL_387;
				}
			}
			else if (name2 == "EmailDataMag")
			{
				EmailDataMag emailDataMag = null;
				try
				{
					string path = Paths.GetNewSavePath() + "/" + YSNewSaveSystem.NowAvatarPathPre + "/EmailDataMag.bin";
					if (File.Exists(path))
					{
						FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
						emailDataMag = (EmailDataMag)new BinaryFormatter().Deserialize(fileStream);
						fileStream.Close();
						emailDataMag.Init();
					}
					else
					{
						emailDataMag = new EmailDataMag();
					}
				}
				catch (Exception)
				{
					Debug.LogError("传音符错误,清空修复");
					emailDataMag = new EmailDataMag();
				}
				fields[i].SetValue(avatar, emailDataMag);
			}
			IL_BD9:
			i++;
			continue;
			IL_36B:
			fields[i].SetValue(avatar, jsonobject.GetInt(name, 0));
			goto IL_BD9;
			IL_387:
			int @int = jsonobject.GetInt(name, 0);
			if (fields[i].FieldType.Name == "UInt32")
			{
				fields[i].SetValue(avatar, Convert.ToUInt32(@int));
			}
			if (fields[i].FieldType.Name == "UInt16")
			{
				fields[i].SetValue(avatar, Convert.ToUInt16(@int));
			}
			if (fields[i].FieldType.Name == "Int16")
			{
				fields[i].SetValue(avatar, Convert.ToInt16(@int));
			}
			if (fields[i].FieldType.Name == "UInt64")
			{
				fields[i].SetValue(avatar, Convert.ToUInt64(@int));
				goto IL_BD9;
			}
			goto IL_BD9;
		}
	}

	// Token: 0x06001411 RID: 5137 RVA: 0x0008108C File Offset: 0x0007F28C
	public static void LoadStreamData(Avatar avatar)
	{
		string path = Paths.GetNewSavePath() + "/" + YSNewSaveSystem.NowAvatarPathPre + "/StreamData.bin";
		if (File.Exists(path))
		{
			FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			avatar.StreamData = (StreamData)binaryFormatter.Deserialize(fileStream);
			fileStream.Close();
		}
		else
		{
			avatar.StreamData = new StreamData();
		}
		avatar.StreamData.FangAnData.LoadHandle();
	}

	// Token: 0x06001412 RID: 5138 RVA: 0x00081100 File Offset: 0x0007F300
	public static void NewInitAvatarFace(int id, int index, int startIndex = 1)
	{
		jsonData.instance.AvatarRandomJsonData = YSNewSaveSystem.LoadJSONObject("AvatarRandomJsonData.json", true);
		if (YSNewSaveSystem.LoadInt("FirstSetAvatarRandomJsonData.txt", true) == 0 || jsonData.instance.reloadRandomAvatarFace)
		{
			foreach (JSONObject jsonobject in jsonData.instance.AvatarJsonData.list)
			{
				if (jsonobject["id"].I != 1 && jsonobject["id"].I >= startIndex)
				{
					if (jsonobject["id"].I >= 20000)
					{
						break;
					}
					JSONObject jsonobject2 = jsonData.instance.randomAvatarFace(jsonobject, jsonData.instance.AvatarRandomJsonData.HasField(string.Concat(jsonobject["id"].I)) ? jsonData.instance.AvatarRandomJsonData[jsonobject["id"].I.ToString()] : null);
					jsonData.instance.AvatarRandomJsonData.SetField(string.Concat(jsonobject["id"].I), jsonobject2.Copy());
				}
			}
			if (jsonData.instance.AvatarRandomJsonData.HasField("1"))
			{
				jsonData.instance.AvatarRandomJsonData.SetField("10000", jsonData.instance.AvatarRandomJsonData["1"]);
			}
			YSNewSaveSystem.Save("AvatarRandomJsonData.json", jsonData.instance.AvatarRandomJsonData, true);
			YSNewSaveSystem.Save("FirstSetAvatarRandomJsonData.txt", 1, true);
			YSNewSaveSystem.NewRandomAvatarBackpack(id, index);
		}
	}

	// Token: 0x06001413 RID: 5139 RVA: 0x000812CC File Offset: 0x0007F4CC
	public static void NewRandomAvatarBackpack(int id, int index)
	{
		JSONObject jsonobject = new JSONObject();
		Avatar avatar = Tools.instance.getPlayer();
		List<JToken> list = Tools.FindAllJTokens(jsonData.instance.ResetAvatarBackpackBanBen, (JToken aa) => (int)aa["BanBenID"] > avatar.BanBenHao);
		foreach (JSONObject jsonobject2 in jsonData.instance.BackpackJsonData.list)
		{
			int avatarID = jsonobject2["AvatrID"].I;
			if (list.Find((JToken aa) => (int)aa["avatar"] == avatarID) == null && jsonData.instance.AvatarBackpackJsonData != null && jsonData.instance.AvatarBackpackJsonData.HasField(string.Concat(avatarID)))
			{
				jsonobject.SetField(string.Concat(avatarID), jsonData.instance.AvatarBackpackJsonData[string.Concat(avatarID)]);
			}
			else
			{
				if (!jsonobject.HasField(string.Concat(avatarID)))
				{
					jsonData.instance.InitAvatarBackpack(ref jsonobject, avatarID);
				}
				jsonData.instance.AvatarAddBackpackByInfo(ref jsonobject, jsonobject2);
			}
		}
		YSNewSaveSystem.Save("AvatarBackpackJsonData.json", jsonobject, true);
		jsonData.instance.AvatarBackpackJsonData = jsonobject;
	}

	// Token: 0x06001414 RID: 5140 RVA: 0x0008144C File Offset: 0x0007F64C
	public static void SaveAvatar(object avatar)
	{
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		FieldInfo[] fields = avatar.GetType().GetFields();
		JSONObject jsonobject = new JSONObject(JSONObject.Type.OBJECT);
		int i = 0;
		while (i < fields.Length)
		{
			string name = fields[i].Name;
			string name2 = fields[i].FieldType.Name;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(name2);
			if (num <= 1615808600U)
			{
				if (num <= 1108380682U)
				{
					if (num <= 765439473U)
					{
						if (num != 697196164U)
						{
							if (num == 765439473U)
							{
								if (name2 == "Int16")
								{
									goto IL_36F;
								}
							}
						}
						else if (name2 == "Int64")
						{
							goto IL_36F;
						}
					}
					else if (num != 815609665U)
					{
						if (num != 851515688U)
						{
							if (num == 1108380682U)
							{
								if (name2 == "WorldTime")
								{
									WorldTime worldTime = (WorldTime)fields[i].GetValue(avatar);
									jsonobject.AddField(name, worldTime.nowTime);
								}
							}
						}
						else if (name2 == "ITEM_INFO_LIST")
						{
							JSONObject jsonobject2 = new JSONObject(JSONObject.Type.ARRAY);
							foreach (ITEM_INFO item_INFO in ((ITEM_INFO_LIST)fields[i].GetValue(avatar)).values)
							{
								JSONObject jsonobject3 = new JSONObject(JSONObject.Type.OBJECT);
								jsonobject3.SetField("UUID", item_INFO.uuid);
								jsonobject3.SetField("id", item_INFO.itemId);
								jsonobject3.SetField("count", item_INFO.itemCount);
								jsonobject3.SetField("index", item_INFO.itemIndex);
								jsonobject3.SetField("Seid", item_INFO.Seid);
								jsonobject2.Add(jsonobject3);
							}
							jsonobject.AddField(name, jsonobject2);
						}
					}
					else if (name2 == "uInt")
					{
						goto IL_36F;
					}
				}
				else if (num <= 1323747186U)
				{
					if (num != 1283547685U)
					{
						if (num == 1323747186U)
						{
							if (name2 == "UInt16")
							{
								goto IL_36F;
							}
						}
					}
					else if (name2 == "Float")
					{
						jsonobject.AddField(name, (float)fields[i].GetValue(avatar));
					}
				}
				else if (num != 1324880019U)
				{
					if (num != 1438686222U)
					{
						if (num == 1615808600U)
						{
							if (name2 == "String")
							{
								jsonobject.AddField(name, fields[i].GetValue(avatar).ToString());
							}
						}
					}
					else if (name2 == "List`1[]")
					{
						if (fields[i].Name == "configEquipSkill" || fields[i].Name == "configEquipStaticSkill")
						{
							JSONObject jsonobject4 = new JSONObject(JSONObject.Type.ARRAY);
							foreach (List<SkillItem> list in (List<SkillItem>[])fields[i].GetValue(avatar))
							{
								JSONObject jsonobject5 = new JSONObject(JSONObject.Type.ARRAY);
								foreach (SkillItem skillItem in list)
								{
									JSONObject jsonobject6 = new JSONObject(JSONObject.Type.OBJECT);
									jsonobject6.SetField("UUID", skillItem.uuid);
									jsonobject6.SetField("id", skillItem.itemId);
									jsonobject6.SetField("level", skillItem.level);
									jsonobject6.SetField("index", skillItem.itemIndex);
									jsonobject6.SetField("Seid", skillItem.Seid);
									jsonobject5.Add(jsonobject6);
								}
								jsonobject4.Add(jsonobject5);
							}
							jsonobject.AddField(name, jsonobject4);
						}
					}
				}
				else if (name2 == "UInt64")
				{
					goto IL_36F;
				}
			}
			else if (num <= 2388225411U)
			{
				if (num <= 1907276658U)
				{
					if (num != 1731900476U)
					{
						if (num == 1907276658U)
						{
							if (name2 == "JObject")
							{
								JSONObject obj = new JSONObject(((JObject)fields[i].GetValue(avatar)).ToString(), -2, false, false);
								jsonobject.AddField(name, obj);
							}
						}
					}
					else if (name2 == "ITEM_INFO_LIST[]")
					{
						JSONObject jsonobject7 = new JSONObject(JSONObject.Type.ARRAY);
						foreach (ITEM_INFO_LIST item_INFO_LIST in (ITEM_INFO_LIST[])fields[i].GetValue(avatar))
						{
							JSONObject jsonobject8 = new JSONObject(JSONObject.Type.ARRAY);
							foreach (ITEM_INFO item_INFO2 in item_INFO_LIST.values)
							{
								JSONObject jsonobject9 = new JSONObject(JSONObject.Type.OBJECT);
								jsonobject9.SetField("UUID", item_INFO2.uuid);
								jsonobject9.SetField("id", item_INFO2.itemId);
								jsonobject9.SetField("count", item_INFO2.itemCount);
								jsonobject9.SetField("index", item_INFO2.itemIndex);
								jsonobject9.SetField("Seid", item_INFO2.Seid);
								jsonobject8.Add(jsonobject9);
							}
							jsonobject7.Add(jsonobject8);
						}
						jsonobject.AddField(name, jsonobject7);
					}
				}
				else if (num != 1926157539U)
				{
					if (num != 1966515832U)
					{
						if (num == 2388225411U)
						{
							if (name2 == "TaskMag")
							{
								TaskMag taskMag = (TaskMag)fields[i].GetValue(avatar);
								jsonobject.AddField(name, taskMag._TaskData);
							}
						}
					}
					else if (name2 == "JSONObject")
					{
						JSONObject obj2 = (JSONObject)fields[i].GetValue(avatar);
						jsonobject.AddField(name, obj2);
					}
				}
				else if (name2 == "AvatarStaticValue")
				{
					AvatarStaticValue avatarStaticValue = (AvatarStaticValue)fields[i].GetValue(avatar);
					JSONObject jsonobject10 = new JSONObject(JSONObject.Type.OBJECT);
					JSONObject jsonobject11 = new JSONObject(JSONObject.Type.ARRAY);
					for (int k = 0; k < 2500; k++)
					{
						jsonobject11.Add(avatarStaticValue.Value[k]);
					}
					jsonobject10.SetField("value", jsonobject11);
					JSONObject jsonobject12 = new JSONObject(JSONObject.Type.ARRAY);
					for (int l = 0; l < avatarStaticValue.talk.Length; l++)
					{
						jsonobject12.Add(avatarStaticValue.talk[l]);
					}
					jsonobject10.SetField("talk", jsonobject12);
					jsonobject.AddField(name, jsonobject10);
				}
			}
			else if (num <= 2935746502U)
			{
				if (num != 2711245919U)
				{
					if (num == 2935746502U)
					{
						if (name2 == "List`1")
						{
							if (fields[i].Name == "hasSkillList" || fields[i].Name == "hasStaticSkillList" || fields[i].Name == "hasJieDanSkillList" || fields[i].Name == "equipSkillList" || fields[i].Name == "equipStaticSkillList")
							{
								JSONObject jsonobject13 = new JSONObject(JSONObject.Type.ARRAY);
								foreach (SkillItem skillItem2 in ((List<SkillItem>)fields[i].GetValue(avatar)))
								{
									JSONObject jsonobject14 = new JSONObject(JSONObject.Type.OBJECT);
									jsonobject14.SetField("UUID", skillItem2.uuid);
									jsonobject14.SetField("id", skillItem2.itemId);
									jsonobject14.SetField("level", skillItem2.level);
									jsonobject14.SetField("index", skillItem2.itemIndex);
									jsonobject14.SetField("Seid", skillItem2.Seid);
									jsonobject13.Add(jsonobject14);
								}
								jsonobject.AddField(name, jsonobject13);
							}
							if (fields[i].Name == "bufflist")
							{
								JSONObject jsonobject15 = new JSONObject(JSONObject.Type.ARRAY);
								foreach (List<int> list2 in ((List<List<int>>)fields[i].GetValue(avatar)))
								{
									JSONObject jsonobject16 = new JSONObject(JSONObject.Type.ARRAY);
									foreach (int val in list2)
									{
										jsonobject16.Add(val);
									}
									jsonobject15.Add(jsonobject16);
								}
								jsonobject.AddField(name, jsonobject15);
							}
							if (fields[i].Name == "LingGeng")
							{
								JSONObject jsonobject17 = new JSONObject(JSONObject.Type.ARRAY);
								foreach (int val2 in ((List<int>)fields[i].GetValue(avatar)))
								{
									jsonobject17.Add(val2);
								}
								jsonobject.AddField(name, jsonobject17);
							}
						}
					}
				}
				else if (name2 == "Int32")
				{
					goto IL_36F;
				}
			}
			else if (num != 2940794790U)
			{
				if (num != 3538687084U)
				{
					if (num == 4168357374U)
					{
						if (name2 == "Int")
						{
							goto IL_36F;
						}
					}
				}
				else if (name2 == "UInt32")
				{
					goto IL_36F;
				}
			}
			else if (name2 == "EmailDataMag")
			{
				EmailDataMag graph = (EmailDataMag)fields[i].GetValue(avatar);
				FileStream fileStream = new FileStream(Paths.GetNewSavePath() + "/" + YSNewSaveSystem.NowAvatarPathPre + "/EmailDataMag.bin", FileMode.Create);
				new BinaryFormatter().Serialize(fileStream, graph);
				fileStream.Close();
			}
			IL_A39:
			i++;
			continue;
			IL_36F:
			long val3 = Convert.ToInt64(fields[i].GetValue(avatar));
			jsonobject.AddField(name, val3);
			goto IL_A39;
		}
		YSNewSaveSystem.Save("Avatar.json", jsonobject, true);
		stopwatch.Stop();
		Debug.Log(string.Format("SaveAvatar耗时{0}ms", stopwatch.ElapsedMilliseconds));
	}

	// Token: 0x06001415 RID: 5141 RVA: 0x00081F24 File Offset: 0x00080124
	public static void SaveStreamData(Avatar avatar)
	{
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		string path = Paths.GetNewSavePath() + "/" + YSNewSaveSystem.NowAvatarPathPre + "/StreamData.bin";
		StreamData streamData = avatar.StreamData;
		streamData.FangAnData.SaveHandle();
		FileStream fileStream = new FileStream(path, FileMode.Create);
		new BinaryFormatter().Serialize(fileStream, streamData);
		fileStream.Close();
		stopwatch.Stop();
		Debug.Log(string.Format("SaveStreamData耗时{0}ms", stopwatch.ElapsedMilliseconds));
	}

	// Token: 0x06001416 RID: 5142 RVA: 0x00081FA4 File Offset: 0x000801A4
	public static void SaveGame(int avatarIndex, int slot, Avatar _avatar = null, bool ignoreSlot0Time = false)
	{
		if (jsonData.instance.saveState == 1)
		{
			UIPopTip.Inst.Pop("存档未完成,请稍等", PopTipIconType.叹号);
			return;
		}
		if (!NpcJieSuanManager.inst.isCanJieSuan)
		{
			UIPopTip.Inst.Pop("正在结算中不能存档", PopTipIconType.叹号);
			return;
		}
		if (FpUIMag.inst != null || TpUIMag.inst != null || UINPCJiaoHu.Inst.NowIsJiaoHu2 || SetFaceUI.Inst != null)
		{
			UIPopTip.Inst.Pop("当前状态不能存档", PopTipIconType.叹号);
			return;
		}
		if (!jsonData.instance.SaveLock)
		{
			if (slot == 0 && !ignoreSlot0Time && SystemConfig.Inst.GetSaveTimes() != 0)
			{
				if (SystemConfig.Inst.GetSaveTimes() == -1)
				{
					return;
				}
				DateTime now = DateTime.Now;
				if (!(now >= Tools.instance.NextSaveTime))
				{
					return;
				}
				Tools.instance.NextSaveTime = Tools.instance.NextSaveTime.AddMinutes((double)Tools.instance.GetAddTime());
				if (Tools.instance.NextSaveTime < now)
				{
					Tools.instance.NextSaveTime = now.AddMinutes((double)Tools.instance.GetAddTime());
				}
			}
			YSNewSaveSystem.startSaveTime = Time.realtimeSinceStartup;
			YSNewSaveSystem.NowUsingAvatarIndex = avatarIndex;
			YSNewSaveSystem.NowUsingSlot = slot;
			YSNewSaveSystem.NowAvatarPathPre = (YSNewSaveSystem.GetAvatarSavePathPre(avatarIndex, slot) ?? "");
			Avatar avatar = PlayerEx.Player;
			if (_avatar != null)
			{
				avatar = _avatar;
			}
			YSNewSaveSystem.NowUsingAvatarIndex = avatarIndex;
			YSNewSaveSystem.NowUsingSlot = slot;
			Paths.GetNewSavePath();
			GameVersion.inst.GetGameVersion();
			JSONObject arr = JSONObject.arr;
			JSONObject jsonobject = new JSONObject();
			int level = (int)avatar.level;
			jsonobject.SetField("firstName", avatar.firstName);
			jsonobject.SetField("lastName", avatar.lastName);
			jsonobject.SetField("gameTime", avatar.worldTimeMag.nowTime);
			jsonobject.SetField("avatarLevel", (int)avatar.level);
			jsonobject.SetField("face", jsonData.instance.AvatarRandomJsonData["1"]);
			YSNewSaveSystem.Save("AvatarInfo.json", jsonobject, true);
			avatar.StreamData.FungusSaveMgr.SaveData();
			YSNewSaveSystem.SaveAvatar(avatar);
			string AvatarBackpackJsonDataClone = jsonData.instance.AvatarBackpackJsonData.ToString();
			string AvatarRandomJsonDataClone = jsonData.instance.AvatarRandomJsonData.ToString();
			string deathNPCJsonData = NpcJieSuanManager.inst.npcDeath.npcDeathJson.ToString();
			string npcOnlyChenghHao = NpcJieSuanManager.inst.npcChengHao.npcOnlyChengHao.ToString();
			JSONObject JieSuanData = new JSONObject();
			JieSuanData.SetField("JieSuanTimes", NpcJieSuanManager.inst.JieSuanTimes);
			JieSuanData.SetField("JieSuanTime", NpcJieSuanManager.inst.JieSuanTime);
			Dictionary<int, List<int>> npcBigMapDictionary = new Dictionary<int, List<int>>(NpcJieSuanManager.inst.npcMap.bigMapNPCDictionary);
			Dictionary<string, List<int>> npcThreeSenceDictionary = new Dictionary<string, List<int>>(NpcJieSuanManager.inst.npcMap.threeSenceNPCDictionary);
			Dictionary<string, Dictionary<int, List<int>>> npcFuBenDictionary = new Dictionary<string, Dictionary<int, List<int>>>(NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary);
			avatar.StreamData.NpcJieSuanData.SaveData();
			YSNewSaveSystem.SaveStreamData(avatar);
			jsonData.instance.saveState = 1;
			Action<object> <>9__1;
			Loom.RunAsync(delegate
			{
				jsonData.instance.SaveLock = true;
				YSNewSaveSystem.Save("GameVersion.txt", GameVersion.inst.GetGameVersion(), true);
				YSNewSaveSystem.Save("AvatarBackpackJsonData.json", AvatarBackpackJsonDataClone, true);
				YSNewSaveSystem.Save("AvatarRandomJsonData.json", AvatarRandomJsonDataClone, true);
				YSNewSaveSystem.Save("NpcBackpack.json", FactoryManager.inst.loadPlayerDateFactory.GetRemoveJSONObjectLess20000keys(jsonData.instance.AvatarBackpackJsonData, AvatarBackpackJsonDataClone), true);
				YSNewSaveSystem.Save("NpcJsonData.json", FactoryManager.inst.loadPlayerDateFactory.GetRemoveJSONObjectLess20000keys(jsonData.instance.AvatarJsonData, null), true);
				YSNewSaveSystem.Save("DeathNpcJsonData.json", deathNPCJsonData, true);
				YSNewSaveSystem.Save("OnlyChengHao.json", npcOnlyChenghHao, true);
				YSNewSaveSystem.Save("AvatarSavetime.txt", DateTime.Now.ToString(), true);
				JSONObject json = FactoryManager.inst.loadPlayerDateFactory.SaveJieSuanData(npcBigMapDictionary, npcThreeSenceDictionary, npcFuBenDictionary, JieSuanData);
				YSNewSaveSystem.Save("JieSuanData.json", json, true);
				YSNewSaveSystem.Save("TuJianSave.json", TuJianManager.Inst.TuJianSave, false);
				YSNewSaveSystem.Save("IsComplete.txt", "true", true);
				jsonData.instance.SaveLock = false;
				jsonData.instance.saveState = 0;
				Action<object> taction;
				if ((taction = <>9__1) == null)
				{
					taction = (<>9__1 = delegate(object obj)
					{
						int @int = YSNewSaveSystem.GetInt("MaxLevelJson.txt", 0);
						if (level > @int)
						{
							YSNewSaveSystem.Save("MaxLevelJson.txt", level, false);
						}
						else
						{
							YSNewSaveSystem.Save("MaxLevelJson.txt", @int, false);
						}
						if (global::SaveManager.inst != null)
						{
							global::SaveManager.inst.updateState();
						}
						jsonData.instance.saveState = -1;
						UIPopTip.Inst.Pop("存档完成", PopTipIconType.叹号);
						if (Tools.instance.IsNeedLaterCheck)
						{
							Tools.instance.IsNeedLaterCheck = false;
							Tools.instance.getPlayer().worldTimeMag.CheckNeedJieSuan();
						}
						Debug.Log(string.Format("新存档系统共计耗时{0}秒", Time.realtimeSinceStartup - YSNewSaveSystem.startSaveTime));
					});
				}
				Loom.QueueOnMainThread(taction, null);
			});
		}
	}

	// Token: 0x04000ED8 RID: 3800
	public static int CloudSaveSlotCountLimit = 4;

	// Token: 0x04000ED9 RID: 3801
	public static int NowUsingAvatarIndex;

	// Token: 0x04000EDA RID: 3802
	public static int NowUsingSlot;

	// Token: 0x04000EDB RID: 3803
	public static string NowAvatarPathPre;

	// Token: 0x04000EDC RID: 3804
	public static float startSaveTime;

	// Token: 0x04000EDD RID: 3805
	public static Dictionary<string, int> saveInt = new Dictionary<string, int>();

	// Token: 0x04000EDE RID: 3806
	public static Dictionary<string, string> saveString = new Dictionary<string, string>();

	// Token: 0x04000EDF RID: 3807
	public static Dictionary<string, JSONObject> saveJSONObject = new Dictionary<string, JSONObject>();

	// Token: 0x04000EE0 RID: 3808
	public static JSONObject SaveJsonData = new JSONObject(JSONObject.Type.OBJECT);

	// Token: 0x04000EE1 RID: 3809
	private static StreamWriter writer;

	// Token: 0x04000EE2 RID: 3810
	private static StreamReader reader;

	// Token: 0x04000EE3 RID: 3811
	public static char huanHangChar = 'þ';
}
