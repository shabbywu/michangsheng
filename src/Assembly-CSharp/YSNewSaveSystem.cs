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
using Steamworks;
using Tab;
using UnityEngine;
using YSGame;
using YSGame.TuJian;
using script.ItemSource.Interface;
using script.NewLianDan;
using script.Submit;

public static class YSNewSaveSystem
{
	public static int CloudSaveSlotCountLimit = 4;

	public static int NowUsingAvatarIndex;

	public static int NowUsingSlot;

	public static string NowAvatarPathPre;

	public static float startSaveTime;

	public static Dictionary<string, int> saveInt = new Dictionary<string, int>();

	public static Dictionary<string, string> saveString = new Dictionary<string, string>();

	public static Dictionary<string, JSONObject> saveJSONObject = new Dictionary<string, JSONObject>();

	public static JSONObject SaveJsonData = new JSONObject(JSONObject.Type.OBJECT);

	private static StreamWriter writer;

	private static StreamReader reader;

	public static char huanHangChar = 'þ';

	public static void UploadCloudSaveData(int slot = 0)
	{
		string text = $"{Paths.GetCloudSavePath()}/CloudSave_{slot}.zip";
		if (File.Exists(text))
		{
			try
			{
				byte[] array = File.ReadAllBytes(text);
				int num = array.Length;
				bool num2 = SteamRemoteStorage.FileWrite($"CloudSave_{slot}.zip", array, num);
				SteamRemoteStorage.SetSyncPlatforms(text, (ERemoteStoragePlatform)(-1));
				if (num2)
				{
					Debug.Log((object)"上传成功");
				}
				else
				{
					Debug.LogError((object)"上传失败");
				}
				return;
			}
			catch (Exception arg)
			{
				Debug.LogError((object)$"上传云存档失败 异常:{arg}");
				return;
			}
		}
		Debug.LogError((object)("上传失败，找不到云存档压缩文件 " + text));
	}

	public static void UploadCloudSaveDataDesc(int slot = 0)
	{
		string text = $"{Paths.GetCloudSavePath()}/CloudSave_{slot}_Desc.txt";
		if (File.Exists(text))
		{
			try
			{
				byte[] array = File.ReadAllBytes(text);
				int num = array.Length;
				bool num2 = SteamRemoteStorage.FileWrite($"CloudSave_{slot}_Desc.txt", array, num);
				SteamRemoteStorage.SetSyncPlatforms(text, (ERemoteStoragePlatform)(-1));
				if (num2)
				{
					Debug.Log((object)"上传备注成功");
				}
				else
				{
					Debug.LogError((object)"上传备注失败");
				}
				return;
			}
			catch (Exception arg)
			{
				Debug.LogError((object)$"上传云存档备注失败 异常:{arg}");
				return;
			}
		}
		Debug.LogError((object)("上传备注失败，找不到备注文件 " + text));
	}

	public static void ZipAndUploadCloudSaveData(int slot = 0, string desc = "")
	{
		YSZip.ZipFile(Paths.GetNewSavePath(), $"{Paths.GetCloudSavePath()}/CloudSave_{slot}.zip");
		if (string.IsNullOrEmpty(desc))
		{
			desc = " ";
		}
		File.WriteAllText($"{Paths.GetCloudSavePath()}/CloudSave_{slot}_Desc.txt", desc);
		UploadCloudSaveData(slot);
		UploadCloudSaveDataDesc(slot);
	}

	public static void DownloadCloudSave(int slot = 0)
	{
		string text = $"{Paths.GetCloudSavePath()}/CloudSave_{slot}.zip";
		string text2 = $"CloudSave_{slot}.zip";
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
				Debug.Log((object)("下载云存档 " + text2 + " 到 " + text + " 完毕"));
			}
			else
			{
				Debug.LogError((object)("下载云存档失败，steam云上不存在文件 " + text2));
			}
		}
		catch (Exception arg)
		{
			Debug.LogError((object)$"下载云存档失败 异常:{arg}");
		}
	}

	public static void DownloadCloudSaveDesc(int slot = 0)
	{
		string text = $"{Paths.GetCloudSavePath()}/CloudSave_{slot}_Desc.txt";
		string text2 = $"CloudSave_{slot}_Desc.txt";
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
				Debug.Log((object)("下载云存档备注 " + text2 + " 到 " + text + " 完毕"));
			}
			else
			{
				Debug.LogError((object)("下载云存档备注失败，steam云上不存在文件 " + text2));
			}
		}
		catch (Exception arg)
		{
			Debug.LogError((object)$"下载云存档备注失败 异常:{arg}");
		}
	}

	public static List<CloudSaveFileData> GetCloudSaveData()
	{
		List<CloudSaveFileData> list = new List<CloudSaveFileData>();
		for (int i = 0; i < CloudSaveSlotCountLimit; i++)
		{
			CloudSaveFileData cloudSaveFileData = new CloudSaveFileData();
			cloudSaveFileData.FileName = $"CloudSave_{i}.zip";
			try
			{
				if (SteamRemoteStorage.FileExists(cloudSaveFileData.FileName))
				{
					long fileTimestamp = SteamRemoteStorage.GetFileTimestamp(cloudSaveFileData.FileName);
					DateTime fileTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)).AddSeconds(fileTimestamp);
					cloudSaveFileData.FileTime = fileTime;
					cloudSaveFileData.FileSize = SteamRemoteStorage.GetFileSize(cloudSaveFileData.FileName);
					cloudSaveFileData.HasFile = true;
				}
				if (SteamRemoteStorage.FileExists($"CloudSave_{i}_Desc.txt"))
				{
					DownloadCloudSaveDesc(i);
					string fileDesc = File.ReadAllText($"{Paths.GetCloudSavePath()}/CloudSave_{i}_Desc.txt");
					cloudSaveFileData.FileDesc = fileDesc;
				}
			}
			catch (Exception ex)
			{
				Debug.LogError((object)$"获取云存档文件信息时出现异常: {ex}");
				cloudSaveFileData.HasFile = false;
				throw ex;
			}
			cloudSaveFileData.Slot = i;
			list.Add(cloudSaveFileData);
		}
		return list;
	}

	public static void DeleteCloudSave(int slot)
	{
		if (SteamRemoteStorage.FileExists($"CloudSave_{slot}.zip"))
		{
			SteamRemoteStorage.FileDelete($"CloudSave_{slot}.zip");
		}
		if (SteamRemoteStorage.FileExists($"CloudSave_{slot}_Desc.txt"))
		{
			SteamRemoteStorage.FileDelete($"CloudSave_{slot}_Desc.txt");
		}
	}

	public static void LogCloudFiles()
	{
		int fileCount = SteamRemoteStorage.GetFileCount();
		Debug.Log((object)$"云上共有{fileCount}个文件");
		int num = default(int);
		for (int i = 0; i < fileCount; i++)
		{
			string fileNameAndSize = SteamRemoteStorage.GetFileNameAndSize(i, ref num);
			long fileTimestamp = SteamRemoteStorage.GetFileTimestamp(fileNameAndSize);
			int num2 = num / 1024;
			DateTime dateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)).AddSeconds(fileTimestamp);
			Debug.Log((object)$"index:{i} size:{num2}kb time:{dateTime} file:{fileNameAndSize}");
		}
	}

	public static void Reset()
	{
		saveInt = new Dictionary<string, int>();
		saveString = new Dictionary<string, string>();
		saveJSONObject = new Dictionary<string, JSONObject>();
		SaveJsonData = new JSONObject(JSONObject.Type.OBJECT);
		jsonData.instance.AvatarBackpackJsonData = null;
	}

	public static string GetAvatarSavePathPre(int avatarIndex, int slot)
	{
		return $"Avatar{avatarIndex}/Slot{slot}";
	}

	public static void Save(string fileName, JSONObject json, bool autoPath = true)
	{
		JSONObject value = new JSONObject(json.ToString());
		saveJSONObject[fileName] = value;
		WriteIntoTxt(fileName, saveJSONObject[fileName].ToString(), autoPath);
	}

	public static void Save(string fileName, string value, bool autoPath = true)
	{
		saveString[fileName] = value;
		WriteIntoTxt(fileName, saveString[fileName], autoPath);
	}

	public static void Save(string fileName, int value, bool autoPath = true)
	{
		saveInt[fileName] = value;
		WriteIntoTxt(fileName, saveInt[fileName].ToString(), autoPath);
	}

	public static JSONObject GetJsonObject(string name, JSONObject json = null)
	{
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		JSONObject jSONObject = json;
		jSONObject = ((!saveJSONObject.ContainsKey(name)) ? new JSONObject(ReadText(name)) : saveJSONObject[name]);
		stopwatch.Stop();
		return jSONObject;
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
				string text = ReadText(name);
				if (string.IsNullOrWhiteSpace(text))
				{
					return result;
				}
				saveInt[name] = int.Parse(text);
				SaveJsonData.SetField(name, int.Parse(text));
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

	public static int LoadInt(string fileName, bool autoPath = true)
	{
		try
		{
			string fileName2 = fileName;
			if (autoPath)
			{
				fileName2 = NowAvatarPathPre + "/" + fileName;
			}
			string text = ReadText(fileName2);
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

	public static JSONObject LoadJSONObject(string fileName, bool autoPath = true)
	{
		try
		{
			string fileName2 = fileName;
			if (autoPath)
			{
				fileName2 = NowAvatarPathPre + "/" + fileName;
			}
			string text = ReadText(fileName2);
			if (text != null && text != "")
			{
				return new JSONObject(text);
			}
		}
		catch (Exception)
		{
			return new JSONObject();
		}
		return new JSONObject();
	}

	public static bool HasFile(string path)
	{
		if (!new FileInfo(path).Exists)
		{
			return false;
		}
		return true;
	}

	public static void WriteIntoTxt(string fileName, string text, bool autoPath = true)
	{
		try
		{
			string text2 = "";
			text2 = ((!autoPath) ? (Paths.GetNewSavePath() + "/" + fileName) : (Paths.GetNewSavePath() + "/" + NowAvatarPathPre + "/" + fileName));
			string text3 = (text3 = text.Replace('\n', huanHangChar).ToCN());
			FileInfo fileInfo = new FileInfo(text2);
			if (!fileInfo.Directory.Exists)
			{
				fileInfo.Directory.Create();
				Debug.Log((object)("创建" + fileInfo.Directory.FullName));
			}
			Debug.Log((object)("开始写入文件" + fileInfo.FullName));
			writer = fileInfo.CreateText();
			writer.Write(text3);
			writer.Flush();
			writer.Dispose();
			writer.Close();
		}
		catch (Exception ex)
		{
			Debug.LogError((object)"在写入存档文件时发生异常:");
			Debug.LogException(ex);
		}
	}

	public static string ReadText(string fileName)
	{
		string text = "";
		try
		{
			string path = Paths.GetNewSavePath() + "/" + fileName;
			if (File.Exists(path))
			{
				reader = new StreamReader(path, Encoding.UTF8);
				text = reader.ReadToEnd();
				text = text.Replace(huanHangChar, '\n');
				reader.Dispose();
				reader.Close();
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)"在读取存档文件时发生异常:");
			Debug.LogException(ex);
		}
		return text;
	}

	public static void AutoLoad()
	{
		LoadSave(PlayerPrefs.GetInt("NowPlayerFileAvatar"), 1);
	}

	public static void AutoSave()
	{
		if ((Object)(object)SingletonMono<TabUIMag>.Instance != (Object)null)
		{
			SingletonMono<TabUIMag>.Instance.TryEscClose();
		}
		SaveGame(PlayerPrefs.GetInt("NowPlayerFileAvatar"), 1);
	}

	public static void DeleteSave(int avatarIndex)
	{
		string path = $"{Paths.GetNewSavePath()}/Avatar{avatarIndex}";
		if (Directory.Exists(path))
		{
			try
			{
				Directory.Delete(path, recursive: true);
				Debug.Log((object)$"删除了存档{avatarIndex}");
			}
			catch (Exception arg)
			{
				Debug.LogError((object)$"删除存档{avatarIndex}时出现异常，{arg}");
			}
		}
	}

	public static void CloseUI()
	{
		if ((Object)(object)((Component)SayDialog.GetSayDialog()).gameObject != (Object)null)
		{
			Object.Destroy((Object)(object)((Component)SayDialog.GetSayDialog()).gameObject);
		}
		if ((Object)(object)SetFaceUI.Inst != (Object)null)
		{
			Object.Destroy((Object)(object)((Component)SetFaceUI.Inst).gameObject);
		}
		if ((Object)(object)FpUIMag.inst != (Object)null)
		{
			Object.Destroy((Object)(object)((Component)FpUIMag.inst).gameObject);
		}
		if ((Object)(object)LianDanUIMag.Instance != (Object)null)
		{
			Object.Destroy((Object)(object)((Component)LianDanUIMag.Instance).gameObject);
		}
		if ((Object)(object)TpUIMag.inst != (Object)null)
		{
			Object.Destroy((Object)(object)((Component)TpUIMag.inst).gameObject);
		}
		if ((Object)(object)SubmitUIMag.Inst != (Object)null)
		{
			SubmitUIMag.Inst.Close();
		}
		if ((Object)(object)QiYuUIMag.Inst != (Object)null)
		{
			Object.Destroy((Object)(object)((Component)QiYuUIMag.Inst).gameObject);
		}
		if ((Object)(object)CaiYaoUIMag.Inst != (Object)null)
		{
			Object.Destroy((Object)(object)((Component)CaiYaoUIMag.Inst).gameObject);
		}
		if ((Object)(object)PanelMamager.inst.UISceneGameObject != (Object)null)
		{
			PanelMamager.inst.UISceneGameObject.SetActive(false);
		}
		if ((Object)(object)SingletonMono<TabUIMag>.Instance != (Object)null)
		{
			SingletonMono<TabUIMag>.Instance.TryEscClose();
		}
		if ((Object)(object)LianQiTotalManager.inst != (Object)null)
		{
			Object.Destroy((Object)(object)((Component)LianQiTotalManager.inst).gameObject);
		}
		if ((Object)(object)SingletonMono<PaiMaiUiMag>.Instance != (Object)null)
		{
			Object.Destroy((Object)(object)((Component)SingletonMono<PaiMaiUiMag>.Instance).gameObject);
			Time.timeScale = 1f;
		}
		ESCCloseManager.Inst.CloseAll();
	}

	public static void LoadOldSave(int Id, int Index)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		CloseUI();
		YSGame.YSSaveGame.Reset();
		KBEngineApp.app.entities[10] = null;
		KBEngineApp.app.entities.Remove(10);
		GameObject val = new GameObject();
		val.AddComponent<StartGame>();
		val.GetComponent<StartGame>().startGame(Id, Index);
	}

	public static SaveSlotData GetAvatarSaveData(int avatarIndex, int slot)
	{
		SaveSlotData saveSlotData = new SaveSlotData();
		if (CheckHasNewSaveAvatarInfo(avatarIndex, slot))
		{
			saveSlotData.HasSave = true;
			saveSlotData.IsNewSaveSystem = true;
		}
		else
		{
			saveSlotData.IsNewSaveSystem = false;
			if (CheckHasOldSaveAvatarInfo(avatarIndex, slot))
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
			JSONObject jSONObject = null;
			bool flag = false;
			if (saveSlotData.IsNewSaveSystem)
			{
				try
				{
					string avatarSavePathPre = GetAvatarSavePathPre(avatarIndex, slot);
					jSONObject = GetJsonObject(avatarSavePathPre + "/AvatarInfo.json");
					saveSlotData.RealSaveTime = ReadText(avatarSavePathPre + "/AvatarSavetime.txt");
				}
				catch
				{
				}
			}
			else
			{
				try
				{
					jSONObject = YSGame.YSSaveGame.GetJsonObject("AvatarInfo" + Tools.instance.getSaveID(avatarIndex, slot));
					saveSlotData.RealSaveTime = YSGame.YSSaveGame.GetTextNameData("AvatarSavetime" + Tools.instance.getSaveID(avatarIndex, slot));
				}
				catch
				{
				}
			}
			try
			{
				saveSlotData.AvatarLevel = jSONObject["avatarLevel"].I;
				JSONObject jSONObject2 = jsonData.instance.LevelUpDataJsonData[saveSlotData.AvatarLevel.ToString()];
				saveSlotData.AvatarLevelText = jSONObject2["Name"].Str;
				saveSlotData.AvatarLevelSprite = ResManager.inst.LoadSprite($"NewUI/Fight/LevelIcon/icon_{saveSlotData.AvatarLevel}");
				DateTime dateTime = DateTime.Parse(jSONObject["gameTime"].Str);
				saveSlotData.GameTime = $"{dateTime.Year}年{dateTime.Month}月{dateTime.Day}日";
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

	private static bool CheckHasNewSaveAvatarInfo(int avatarIndex, int slot)
	{
		string avatarSavePathPre = GetAvatarSavePathPre(avatarIndex, slot);
		bool flag = false;
		if (HasFile(Paths.GetNewSavePath() + "/" + avatarSavePathPre + "/AvatarInfo.json"))
		{
			if (GetJsonObject(avatarSavePathPre + "/AvatarInfo.json").IsNull)
			{
				flag = false;
			}
			else
			{
				flag = true;
				if (LoadInt(avatarSavePathPre + "/GameVersion.txt") > 4 && !HasFile(Paths.GetNewSavePath() + "/" + avatarSavePathPre + "/IsComplete.txt"))
				{
					flag = false;
				}
			}
		}
		else
		{
			flag = false;
		}
		return flag;
	}

	private static bool CheckHasOldSaveAvatarInfo(int avatarIndex, int slot)
	{
		bool flag = false;
		if (YSGame.YSSaveGame.HasFile(Paths.GetSavePath(), "AvatarInfo" + Tools.instance.getSaveID(avatarIndex, slot)))
		{
			if (YSGame.YSSaveGame.GetJsonObject("AvatarInfo" + Tools.instance.getSaveID(avatarIndex, slot)).IsNull)
			{
				flag = false;
			}
			else
			{
				flag = true;
				if (FactoryManager.inst.SaveLoadFactory.GetInt("GameVersion" + Tools.instance.getSaveID(avatarIndex, slot)) > 4 && !YSGame.YSSaveGame.HasFile(Paths.GetSavePath(), "IsComplete" + Tools.instance.getSaveID(avatarIndex, slot)))
				{
					flag = false;
				}
			}
		}
		else
		{
			flag = false;
		}
		return flag;
	}

	public static void LoadSave(int avatarIndex, int slot, int DFIndex = -1)
	{
		ABItemSourceMag.SetNull();
		if (DFIndex < 0)
		{
			Tools.instance.IsInDF = false;
		}
		CloseUI();
		Reset();
		KBEngineApp.app.entities[10] = null;
		KBEngineApp.app.entities.Remove(10);
		NowUsingAvatarIndex = avatarIndex;
		NowUsingSlot = slot;
		NowAvatarPathPre = GetAvatarSavePathPre(avatarIndex, slot) ?? "";
		Tools.instance.IsCanLoadSetTalk = false;
		MusicMag.instance.stopMusic();
		AddAvatar(avatarIndex, slot);
		Avatar player = PlayerEx.Player;
		LoadStreamData(player);
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
		if ((Object)(object)fader == (Object)null)
		{
			Tools.instance.loadOtherScenes("LoadingScreen");
		}
		else
		{
			fader.FadeIntoLevel("LoadingScreen");
		}
	}

	public static void AddAvatar(int id, int index)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		CreatAvatar(10, 51, 40, new Vector3(-5f, -1.7f, -1f), new Vector3(0f, 0f, 80f));
		KBEngineApp.app.entity_id = 10;
		Avatar obj = (Avatar)KBEngineApp.app.player();
		LoadAvatar(obj);
		InitSkill();
		LoadAvatarFace(id, index);
		StaticSkill.resetSeid(obj);
		WuDaoStaticSkill.resetWuDaoSeid(obj);
		JieDanSkill.resetJieDanSeid(obj);
		PlayerPrefs.SetInt("NowPlayerFileAvatar", id);
		obj.seaNodeMag.INITSEA();
	}

	public static void LoadAvatarFace(int id, int index)
	{
		jsonData instance = jsonData.instance;
		instance.AvatarRandomJsonData = LoadJSONObject("AvatarRandomJsonData.json");
		instance.AvatarBackpackJsonData = LoadJSONObject("AvatarBackpackJsonData.json");
		if (instance.AvatarRandomJsonData.Count == instance.AvatarJsonData.Count && !instance.reloadRandomAvatarFace)
		{
			return;
		}
		Save("FirstSetAvatarRandomJsonData.txt", 0);
		NewInitAvatarFace(id, index);
		instance.IsResetAvatarFace = true;
		List<int> list = new List<int>();
		foreach (KeyValuePair<string, JToken> item in instance.ResetAvatarBackpackBanBen)
		{
			if (!list.Contains((int)item.Value[(object)"BanBenID"]))
			{
				list.Add((int)item.Value[(object)"BanBenID"]);
			}
		}
		Tools.instance.getPlayer().BanBenHao = list.Max();
	}

	public static void InitSkill()
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		avatar.equipSkillList = avatar.configEquipSkill[avatar.nowConfigEquipSkill];
		avatar.equipStaticSkillList = avatar.configEquipStaticSkill[avatar.nowConfigEquipStaticSkill];
	}

	public static void SetAvatar(int avaterID, int roleType, int HP_Max, Vector3 position, Vector3 direction, Avatar avatar, int AvatarID = 1)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		avatar.position = position;
		avatar.direction = direction;
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[string.Concat(AvatarID)];
		int num = 0;
		foreach (JSONObject item2 in jSONObject["skills"].list)
		{
			avatar.addHasSkillList((int)item2.n);
			avatar.equipSkill((int)item2.n, num);
			num++;
		}
		int num2 = 0;
		foreach (JSONObject item3 in jSONObject["staticSkills"].list)
		{
			avatar.addHasStaticSkillList((int)item3.n);
			avatar.equipStaticSkill((int)item3.n, num2);
			num2++;
		}
		for (int j = 0; j < jSONObject["LingGen"].Count; j++)
		{
			int item = (int)jSONObject["LingGen"][j].n;
			avatar.LingGeng.Add(item);
		}
		avatar.ZiZhi = (int)jSONObject["ziZhi"].n;
		avatar.dunSu = (int)jSONObject["dunSu"].n;
		avatar.wuXin = (uint)jSONObject["wuXin"].n;
		avatar.shengShi = (int)jSONObject["shengShi"].n;
		avatar.shaQi = (uint)jSONObject["shaQi"].n;
		avatar.shouYuan = (uint)jSONObject["shouYuan"].n;
		avatar.age = (uint)jSONObject["age"].n;
		avatar.HP_Max = (int)jSONObject["HP"].n;
		avatar.HP = (int)jSONObject["HP"].n;
		avatar.money = (uint)jSONObject["MoneyType"].n;
		avatar.level = (ushort)jSONObject["Level"].n;
		avatar.AvatarType = (ushort)jSONObject["AvatarType"].n;
		avatar.roleTypeCell = (uint)jSONObject["fightFace"].n;
		avatar.roleType = (uint)jSONObject["face"].n;
		avatar.Sex = (int)jSONObject["SexType"].n;
		avatar.configEquipSkill[0] = avatar.equipSkillList;
		avatar.configEquipStaticSkill[0] = avatar.equipStaticSkillList;
		avatar.equipItemList.values.ForEach(delegate(ITEM_INFO i)
		{
			avatar.configEquipItem[0].values.Add(i);
		});
	}

	public static void CreatAvatar(int avaterID, int roleType, int HP_Max, Vector3 position, Vector3 direction, int AvatarID = 1)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		KBEngineApp.app.Client_onCreatedProxies((ulong)avaterID, avaterID, "Avatar");
		Avatar avatar = (Avatar)KBEngineApp.app.entities[avaterID];
		SetAvatar(avaterID, roleType, HP_Max, position, direction, avatar, AvatarID);
	}

	public static void LoadAvatar(Avatar avatar)
	{
		JSONObject jSONObject = LoadJSONObject("Avatar.json");
		FieldInfo[] fields = typeof(Avatar).GetFields();
		for (int i = 0; i < fields.Length; i++)
		{
			string name = fields[i].Name;
			switch (fields[i].FieldType.Name)
			{
			case "String":
				fields[i].SetValue(avatar, jSONObject.GetString(name));
				break;
			case "Int32":
			case "Int64":
			case "Int":
			case "uInt":
				fields[i].SetValue(avatar, jSONObject.GetInt(name));
				break;
			case "UInt32":
			case "UInt16":
			case "Int16":
			case "UInt64":
			{
				int @int = jSONObject.GetInt(name);
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
				}
				break;
			}
			case "Float":
				fields[i].SetValue(avatar, jSONObject.GetFloat(name));
				break;
			case "List`1":
				if (fields[i].Name == "hasSkillList" || fields[i].Name == "hasStaticSkillList" || fields[i].Name == "hasJieDanSkillList" || fields[i].Name == "equipSkillList" || fields[i].Name == "equipStaticSkillList")
				{
					List<SkillItem> list3 = new List<SkillItem>();
					foreach (JSONObject item in jSONObject.GetField(name).list)
					{
						SkillItem skillItem = new SkillItem();
						skillItem.uuid = item.GetString("UUID");
						if (skillItem.uuid == "")
						{
							skillItem.uuid = Tools.getUUID();
						}
						skillItem.itemId = item.GetInt("id");
						skillItem.level = item.GetInt("level");
						skillItem.itemIndex = item.GetInt("index");
						skillItem.itemId = item.GetInt("id");
						skillItem.Seid = item.GetField("Seid");
						list3.Add(skillItem);
					}
					fields[i].SetValue(avatar, list3);
				}
				if (fields[i].Name == "bufflist")
				{
					if (jSONObject == null || jSONObject.list == null || jSONObject.list.Count == 0)
					{
						break;
					}
					List<List<int>> list4 = new List<List<int>>();
					foreach (JSONObject item2 in jSONObject.GetField(name).list)
					{
						list4.Add(item2.ToList());
					}
					fields[i].SetValue(avatar, list4);
				}
				if (fields[i].Name == "LingGeng")
				{
					JSONObject field5 = jSONObject.GetField(name);
					fields[i].SetValue(avatar, field5.ToList());
				}
				break;
			case "List`1[]":
			{
				if (!(fields[i].Name == "configEquipSkill") && !(fields[i].Name == "configEquipStaticSkill"))
				{
					break;
				}
				List<SkillItem>[] array = new List<SkillItem>[5]
				{
					new List<SkillItem>(),
					new List<SkillItem>(),
					new List<SkillItem>(),
					new List<SkillItem>(),
					new List<SkillItem>()
				};
				JSONObject field6 = jSONObject.GetField(name);
				for (int k = 0; k < 5; k++)
				{
					foreach (JSONObject item3 in field6.list[k].list)
					{
						SkillItem skillItem2 = new SkillItem();
						skillItem2.uuid = item3.GetString("uuid");
						skillItem2.itemId = item3.GetInt("id");
						skillItem2.level = item3.GetInt("level");
						skillItem2.itemIndex = item3.GetInt("index");
						skillItem2.Seid = item3.GetField("Seid");
						array[k].Add(skillItem2);
					}
				}
				fields[i].SetValue(avatar, array);
				break;
			}
			case "ITEM_INFO_LIST":
			{
				ITEM_INFO_LIST iTEM_INFO_LIST = new ITEM_INFO_LIST();
				foreach (JSONObject item4 in jSONObject.GetField(name).list)
				{
					ITEM_INFO iTEM_INFO = new ITEM_INFO();
					iTEM_INFO.uuid = item4.GetString("UUID");
					if (iTEM_INFO.uuid == "")
					{
						iTEM_INFO.uuid = Tools.getUUID();
					}
					iTEM_INFO.itemId = item4.GetInt("id");
					iTEM_INFO.itemCount = (uint)item4.GetInt("count");
					iTEM_INFO.itemIndex = item4.GetInt("index");
					iTEM_INFO.Seid = item4.GetField("Seid");
					iTEM_INFO_LIST.values.Add(iTEM_INFO);
				}
				fields[i].SetValue(avatar, iTEM_INFO_LIST);
				break;
			}
			case "ITEM_INFO_LIST[]":
			{
				ITEM_INFO_LIST[] array2 = new ITEM_INFO_LIST[5]
				{
					new ITEM_INFO_LIST(),
					new ITEM_INFO_LIST(),
					new ITEM_INFO_LIST(),
					new ITEM_INFO_LIST(),
					new ITEM_INFO_LIST()
				};
				JSONObject field7 = jSONObject.GetField(name);
				for (int l = 0; l < 5; l++)
				{
					foreach (JSONObject item5 in field7.list[l].list)
					{
						ITEM_INFO iTEM_INFO2 = new ITEM_INFO();
						iTEM_INFO2.uuid = item5.GetString("uuid");
						if (iTEM_INFO2.uuid == "")
						{
							iTEM_INFO2.uuid = Tools.getUUID();
						}
						iTEM_INFO2.itemId = item5.GetInt("id");
						iTEM_INFO2.itemCount = (uint)item5.GetInt("count");
						iTEM_INFO2.itemIndex = item5.GetInt("index");
						iTEM_INFO2.Seid = item5.GetField("Seid");
						array2[l].values.Add(iTEM_INFO2);
					}
				}
				fields[i].SetValue(avatar, array2);
				break;
			}
			case "AvatarStaticValue":
			{
				JSONObject field4 = jSONObject.GetField(name);
				List<int> list = field4.GetField("value").ToList();
				AvatarStaticValue avatarStaticValue = new AvatarStaticValue();
				for (int j = 0; j < list.Count; j++)
				{
					avatarStaticValue.Value[j] = list[j];
				}
				List<int> list2 = field4.GetField("talk").ToList();
				if (list2.Count < 2)
				{
					list2.Add(0);
					list2.Add(0);
				}
				avatarStaticValue.talk = list2.ToArray();
				fields[i].SetValue(avatar, avatarStaticValue);
				break;
			}
			case "WorldTime":
			{
				WorldTime worldTime = new WorldTime();
				worldTime.isLoadDate = true;
				jSONObject.GetString("worldTimeMag", "0001-1-1");
				worldTime.nowTime = jSONObject.GetString("worldTimeMag", "0001-1-1");
				fields[i].SetValue(avatar, worldTime);
				break;
			}
			case "TaskMag":
			{
				JSONObject field3 = jSONObject.GetField(name);
				TaskMag taskMag = new TaskMag(avatar);
				taskMag._TaskData = field3;
				fields[i].SetValue(avatar, taskMag);
				break;
			}
			case "EmailDataMag":
			{
				EmailDataMag emailDataMag = null;
				try
				{
					string path = Paths.GetNewSavePath() + "/" + NowAvatarPathPre + "/EmailDataMag.bin";
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
					Debug.LogError((object)"传音符错误,清空修复");
					emailDataMag = new EmailDataMag();
				}
				fields[i].SetValue(avatar, emailDataMag);
				break;
			}
			case "JSONObject":
			{
				JSONObject field2 = jSONObject.GetField(name);
				fields[i].SetValue(avatar, field2);
				break;
			}
			case "JObject":
			{
				JSONObject field = jSONObject.GetField(name);
				if (field != null)
				{
					JObject value = JObject.Parse(field.ToString());
					fields[i].SetValue(avatar, value);
				}
				break;
			}
			}
		}
	}

	public static void LoadStreamData(Avatar avatar)
	{
		string path = Paths.GetNewSavePath() + "/" + NowAvatarPathPre + "/StreamData.bin";
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

	public static void NewInitAvatarFace(int id, int index, int startIndex = 1)
	{
		jsonData.instance.AvatarRandomJsonData = LoadJSONObject("AvatarRandomJsonData.json");
		if (LoadInt("FirstSetAvatarRandomJsonData.txt") != 0 && !jsonData.instance.reloadRandomAvatarFace)
		{
			return;
		}
		foreach (JSONObject item in jsonData.instance.AvatarJsonData.list)
		{
			if (item["id"].I != 1 && item["id"].I >= startIndex)
			{
				if (item["id"].I >= 20000)
				{
					break;
				}
				JSONObject jSONObject = jsonData.instance.randomAvatarFace(item, jsonData.instance.AvatarRandomJsonData.HasField(string.Concat(item["id"].I)) ? jsonData.instance.AvatarRandomJsonData[item["id"].I.ToString()] : null);
				jsonData.instance.AvatarRandomJsonData.SetField(string.Concat(item["id"].I), jSONObject.Copy());
			}
		}
		if (jsonData.instance.AvatarRandomJsonData.HasField("1"))
		{
			jsonData.instance.AvatarRandomJsonData.SetField("10000", jsonData.instance.AvatarRandomJsonData["1"]);
		}
		Save("AvatarRandomJsonData.json", jsonData.instance.AvatarRandomJsonData);
		Save("FirstSetAvatarRandomJsonData.txt", 1);
		NewRandomAvatarBackpack(id, index);
	}

	public static void NewRandomAvatarBackpack(int id, int index)
	{
		JSONObject jsondata = new JSONObject();
		Avatar avatar = Tools.instance.getPlayer();
		List<JToken> list = Tools.FindAllJTokens((JToken)(object)jsonData.instance.ResetAvatarBackpackBanBen, (JToken aa) => (int)aa[(object)"BanBenID"] > avatar.BanBenHao);
		foreach (JSONObject item in jsonData.instance.BackpackJsonData.list)
		{
			int avatarID = item["AvatrID"].I;
			if (list.Find((JToken aa) => (int)aa[(object)"avatar"] == avatarID) == null && jsonData.instance.AvatarBackpackJsonData != null && jsonData.instance.AvatarBackpackJsonData.HasField(string.Concat(avatarID)))
			{
				jsondata.SetField(string.Concat(avatarID), jsonData.instance.AvatarBackpackJsonData[string.Concat(avatarID)]);
				continue;
			}
			if (!jsondata.HasField(string.Concat(avatarID)))
			{
				jsonData.instance.InitAvatarBackpack(ref jsondata, avatarID);
			}
			jsonData.instance.AvatarAddBackpackByInfo(ref jsondata, item);
		}
		Save("AvatarBackpackJsonData.json", jsondata);
		jsonData.instance.AvatarBackpackJsonData = jsondata;
	}

	public static void SaveAvatar(object avatar)
	{
		//IL_0a1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a24: Expected O, but got Unknown
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		FieldInfo[] fields = avatar.GetType().GetFields();
		JSONObject jSONObject = new JSONObject(JSONObject.Type.OBJECT);
		for (int i = 0; i < fields.Length; i++)
		{
			string name = fields[i].Name;
			switch (fields[i].FieldType.Name)
			{
			case "String":
				jSONObject.AddField(name, fields[i].GetValue(avatar).ToString());
				break;
			case "Int32":
			case "Int64":
			case "Int16":
			case "UInt32":
			case "UInt16":
			case "UInt64":
			case "Int":
			case "uInt":
			{
				long val = Convert.ToInt64(fields[i].GetValue(avatar));
				jSONObject.AddField(name, val);
				break;
			}
			case "Float":
				jSONObject.AddField(name, (float)fields[i].GetValue(avatar));
				break;
			case "List`1":
			{
				if (fields[i].Name == "hasSkillList" || fields[i].Name == "hasStaticSkillList" || fields[i].Name == "hasJieDanSkillList" || fields[i].Name == "equipSkillList" || fields[i].Name == "equipStaticSkillList")
				{
					JSONObject jSONObject13 = new JSONObject(JSONObject.Type.ARRAY);
					foreach (SkillItem item in (List<SkillItem>)fields[i].GetValue(avatar))
					{
						JSONObject jSONObject14 = new JSONObject(JSONObject.Type.OBJECT);
						jSONObject14.SetField("UUID", item.uuid);
						jSONObject14.SetField("id", item.itemId);
						jSONObject14.SetField("level", item.level);
						jSONObject14.SetField("index", item.itemIndex);
						jSONObject14.SetField("Seid", item.Seid);
						jSONObject13.Add(jSONObject14);
					}
					jSONObject.AddField(name, jSONObject13);
				}
				if (fields[i].Name == "bufflist")
				{
					JSONObject jSONObject15 = new JSONObject(JSONObject.Type.ARRAY);
					foreach (List<int> item2 in (List<List<int>>)fields[i].GetValue(avatar))
					{
						JSONObject jSONObject16 = new JSONObject(JSONObject.Type.ARRAY);
						foreach (int item3 in item2)
						{
							jSONObject16.Add(item3);
						}
						jSONObject15.Add(jSONObject16);
					}
					jSONObject.AddField(name, jSONObject15);
				}
				if (!(fields[i].Name == "LingGeng"))
				{
					break;
				}
				JSONObject jSONObject17 = new JSONObject(JSONObject.Type.ARRAY);
				foreach (int item4 in (List<int>)fields[i].GetValue(avatar))
				{
					jSONObject17.Add(item4);
				}
				jSONObject.AddField(name, jSONObject17);
				break;
			}
			case "List`1[]":
			{
				if (!(fields[i].Name == "configEquipSkill") && !(fields[i].Name == "configEquipStaticSkill"))
				{
					break;
				}
				JSONObject jSONObject7 = new JSONObject(JSONObject.Type.ARRAY);
				List<SkillItem>[] array = (List<SkillItem>[])fields[i].GetValue(avatar);
				foreach (List<SkillItem> obj3 in array)
				{
					JSONObject jSONObject8 = new JSONObject(JSONObject.Type.ARRAY);
					foreach (SkillItem item5 in obj3)
					{
						JSONObject jSONObject9 = new JSONObject(JSONObject.Type.OBJECT);
						jSONObject9.SetField("UUID", item5.uuid);
						jSONObject9.SetField("id", item5.itemId);
						jSONObject9.SetField("level", item5.level);
						jSONObject9.SetField("index", item5.itemIndex);
						jSONObject9.SetField("Seid", item5.Seid);
						jSONObject8.Add(jSONObject9);
					}
					jSONObject7.Add(jSONObject8);
				}
				jSONObject.AddField(name, jSONObject7);
				break;
			}
			case "ITEM_INFO_LIST":
			{
				JSONObject jSONObject5 = new JSONObject(JSONObject.Type.ARRAY);
				foreach (ITEM_INFO value in ((ITEM_INFO_LIST)fields[i].GetValue(avatar)).values)
				{
					JSONObject jSONObject6 = new JSONObject(JSONObject.Type.OBJECT);
					jSONObject6.SetField("UUID", value.uuid);
					jSONObject6.SetField("id", value.itemId);
					jSONObject6.SetField("count", value.itemCount);
					jSONObject6.SetField("index", value.itemIndex);
					jSONObject6.SetField("Seid", value.Seid);
					jSONObject5.Add(jSONObject6);
				}
				jSONObject.AddField(name, jSONObject5);
				break;
			}
			case "ITEM_INFO_LIST[]":
			{
				JSONObject jSONObject10 = new JSONObject(JSONObject.Type.ARRAY);
				ITEM_INFO_LIST[] array2 = (ITEM_INFO_LIST[])fields[i].GetValue(avatar);
				foreach (ITEM_INFO_LIST obj4 in array2)
				{
					JSONObject jSONObject11 = new JSONObject(JSONObject.Type.ARRAY);
					foreach (ITEM_INFO value2 in obj4.values)
					{
						JSONObject jSONObject12 = new JSONObject(JSONObject.Type.OBJECT);
						jSONObject12.SetField("UUID", value2.uuid);
						jSONObject12.SetField("id", value2.itemId);
						jSONObject12.SetField("count", value2.itemCount);
						jSONObject12.SetField("index", value2.itemIndex);
						jSONObject12.SetField("Seid", value2.Seid);
						jSONObject11.Add(jSONObject12);
					}
					jSONObject10.Add(jSONObject11);
				}
				jSONObject.AddField(name, jSONObject10);
				break;
			}
			case "AvatarStaticValue":
			{
				AvatarStaticValue avatarStaticValue = (AvatarStaticValue)fields[i].GetValue(avatar);
				JSONObject jSONObject2 = new JSONObject(JSONObject.Type.OBJECT);
				JSONObject jSONObject3 = new JSONObject(JSONObject.Type.ARRAY);
				for (int j = 0; j < 2500; j++)
				{
					jSONObject3.Add(avatarStaticValue.Value[j]);
				}
				jSONObject2.SetField("value", jSONObject3);
				JSONObject jSONObject4 = new JSONObject(JSONObject.Type.ARRAY);
				for (int k = 0; k < avatarStaticValue.talk.Length; k++)
				{
					jSONObject4.Add(avatarStaticValue.talk[k]);
				}
				jSONObject2.SetField("talk", jSONObject4);
				jSONObject.AddField(name, jSONObject2);
				break;
			}
			case "WorldTime":
			{
				WorldTime worldTime = (WorldTime)fields[i].GetValue(avatar);
				jSONObject.AddField(name, worldTime.nowTime);
				break;
			}
			case "EmailDataMag":
			{
				EmailDataMag graph = (EmailDataMag)fields[i].GetValue(avatar);
				FileStream fileStream = new FileStream(Paths.GetNewSavePath() + "/" + NowAvatarPathPre + "/EmailDataMag.bin", FileMode.Create);
				new BinaryFormatter().Serialize(fileStream, graph);
				fileStream.Close();
				break;
			}
			case "TaskMag":
			{
				TaskMag taskMag = (TaskMag)fields[i].GetValue(avatar);
				jSONObject.AddField(name, taskMag._TaskData);
				break;
			}
			case "JSONObject":
			{
				JSONObject obj2 = (JSONObject)fields[i].GetValue(avatar);
				jSONObject.AddField(name, obj2);
				break;
			}
			case "JObject":
			{
				JSONObject obj = new JSONObject(((object)(JObject)fields[i].GetValue(avatar)).ToString());
				jSONObject.AddField(name, obj);
				break;
			}
			}
		}
		Save("Avatar.json", jSONObject);
		stopwatch.Stop();
		Debug.Log((object)$"SaveAvatar耗时{stopwatch.ElapsedMilliseconds}ms");
	}

	public static void SaveStreamData(Avatar avatar)
	{
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		string path = Paths.GetNewSavePath() + "/" + NowAvatarPathPre + "/StreamData.bin";
		StreamData streamData = avatar.StreamData;
		streamData.FangAnData.SaveHandle();
		FileStream fileStream = new FileStream(path, FileMode.Create);
		new BinaryFormatter().Serialize(fileStream, streamData);
		fileStream.Close();
		stopwatch.Stop();
		Debug.Log((object)$"SaveStreamData耗时{stopwatch.ElapsedMilliseconds}ms");
	}

	public static void SaveGame(int avatarIndex, int slot, Avatar _avatar = null, bool ignoreSlot0Time = false)
	{
		if (jsonData.instance.saveState == 1)
		{
			UIPopTip.Inst.Pop("存档未完成,请稍等");
		}
		else if (!NpcJieSuanManager.inst.isCanJieSuan)
		{
			UIPopTip.Inst.Pop("正在结算中不能存档");
		}
		else if ((Object)(object)FpUIMag.inst != (Object)null || (Object)(object)TpUIMag.inst != (Object)null || UINPCJiaoHu.Inst.NowIsJiaoHu2 || (Object)(object)SetFaceUI.Inst != (Object)null)
		{
			UIPopTip.Inst.Pop("当前状态不能存档");
		}
		else
		{
			if (jsonData.instance.SaveLock)
			{
				return;
			}
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
				Tools.instance.NextSaveTime = Tools.instance.NextSaveTime.AddMinutes(Tools.instance.GetAddTime());
				if (Tools.instance.NextSaveTime < now)
				{
					Tools.instance.NextSaveTime = now.AddMinutes(Tools.instance.GetAddTime());
				}
			}
			startSaveTime = Time.realtimeSinceStartup;
			NowUsingAvatarIndex = avatarIndex;
			NowUsingSlot = slot;
			NowAvatarPathPre = GetAvatarSavePathPre(avatarIndex, slot) ?? "";
			Avatar avatar = PlayerEx.Player;
			if (_avatar != null)
			{
				avatar = _avatar;
			}
			NowUsingAvatarIndex = avatarIndex;
			NowUsingSlot = slot;
			Paths.GetNewSavePath();
			GameVersion.inst.GetGameVersion();
			_ = JSONObject.arr;
			JSONObject jSONObject = new JSONObject();
			int level = avatar.level;
			jSONObject.SetField("firstName", avatar.firstName);
			jSONObject.SetField("lastName", avatar.lastName);
			jSONObject.SetField("gameTime", avatar.worldTimeMag.nowTime);
			jSONObject.SetField("avatarLevel", avatar.level);
			jSONObject.SetField("face", jsonData.instance.AvatarRandomJsonData["1"]);
			Save("AvatarInfo.json", jSONObject);
			avatar.StreamData.FungusSaveMgr.SaveData();
			SaveAvatar(avatar);
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
			SaveStreamData(avatar);
			jsonData.instance.saveState = 1;
			Loom.RunAsync(delegate
			{
				jsonData.instance.SaveLock = true;
				Save("GameVersion.txt", GameVersion.inst.GetGameVersion());
				Save("AvatarBackpackJsonData.json", AvatarBackpackJsonDataClone);
				Save("AvatarRandomJsonData.json", AvatarRandomJsonDataClone);
				Save("NpcBackpack.json", FactoryManager.inst.loadPlayerDateFactory.GetRemoveJSONObjectLess20000keys(jsonData.instance.AvatarBackpackJsonData, AvatarBackpackJsonDataClone));
				Save("NpcJsonData.json", FactoryManager.inst.loadPlayerDateFactory.GetRemoveJSONObjectLess20000keys(jsonData.instance.AvatarJsonData));
				Save("DeathNpcJsonData.json", deathNPCJsonData);
				Save("OnlyChengHao.json", npcOnlyChenghHao);
				Save("AvatarSavetime.txt", DateTime.Now.ToString());
				JSONObject json = FactoryManager.inst.loadPlayerDateFactory.SaveJieSuanData(npcBigMapDictionary, npcThreeSenceDictionary, npcFuBenDictionary, JieSuanData);
				Save("JieSuanData.json", json);
				Save("TuJianSave.json", TuJianManager.Inst.TuJianSave, autoPath: false);
				Save("IsComplete.txt", "true");
				jsonData.instance.SaveLock = false;
				jsonData.instance.saveState = 0;
				Loom.QueueOnMainThread(delegate
				{
					int @int = GetInt("MaxLevelJson.txt");
					if (level > @int)
					{
						Save("MaxLevelJson.txt", level, autoPath: false);
					}
					else
					{
						Save("MaxLevelJson.txt", @int, autoPath: false);
					}
					if ((Object)(object)SaveManager.inst != (Object)null)
					{
						SaveManager.inst.updateState();
					}
					jsonData.instance.saveState = -1;
					UIPopTip.Inst.Pop("存档完成");
					if (Tools.instance.IsNeedLaterCheck)
					{
						Tools.instance.IsNeedLaterCheck = false;
						Tools.instance.getPlayer().worldTimeMag.CheckNeedJieSuan();
					}
					Debug.Log((object)$"新存档系统共计耗时{Time.realtimeSinceStartup - startSaveTime}秒");
				}, null);
			});
		}
	}
}
