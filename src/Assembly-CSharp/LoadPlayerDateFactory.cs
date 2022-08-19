using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using KBEngine;
using script.YarnEditor.Manager;
using UnityEngine;
using YSGame;

// Token: 0x020002B2 RID: 690
public class LoadPlayerDateFactory
{
	// Token: 0x06001855 RID: 6229 RVA: 0x000A9FD0 File Offset: 0x000A81D0
	public JSONObject SaveJieSuanData(Dictionary<int, List<int>> bigMap, Dictionary<string, List<int>> threeScene, Dictionary<string, Dictionary<int, List<int>>> fuBen, JSONObject data)
	{
		if (bigMap.Count > 0)
		{
			JSONObject jsonobject = new JSONObject();
			foreach (int key in bigMap.Keys)
			{
				JSONObject arr = JSONObject.arr;
				foreach (int val in bigMap[key])
				{
					arr.Add(val);
				}
				jsonobject.SetField(key.ToString(), arr);
			}
			data.SetField("bigMapJson", jsonobject);
		}
		if (threeScene.Count > 0)
		{
			JSONObject jsonobject2 = new JSONObject();
			foreach (string text in threeScene.Keys)
			{
				JSONObject arr2 = JSONObject.arr;
				foreach (int val2 in threeScene[text])
				{
					arr2.Add(val2);
				}
				jsonobject2.SetField(text.ToString(), arr2);
			}
			data.SetField("threeSceneJson", jsonobject2);
		}
		if (fuBen.Count > 0)
		{
			JSONObject jsonobject3 = new JSONObject();
			foreach (string text2 in fuBen.Keys)
			{
				JSONObject jsonobject4 = new JSONObject();
				foreach (int key2 in fuBen[text2].Keys)
				{
					JSONObject arr3 = JSONObject.arr;
					foreach (int val3 in fuBen[text2][key2])
					{
						arr3.Add(val3);
					}
					jsonobject4.SetField(key2.ToString(), arr3);
				}
				jsonobject3.SetField(text2.ToString(), jsonobject4);
			}
			data.SetField("fuBenJson", jsonobject3);
		}
		return data;
	}

	// Token: 0x06001856 RID: 6230 RVA: 0x000AA278 File Offset: 0x000A8478
	public JSONObject SavePackData(string packData)
	{
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		JSONObject jsonobject = new JSONObject();
		JSONObject jsonobject2 = new JSONObject(packData, -2, false, false);
		foreach (string text in jsonobject2.keys)
		{
			if (int.Parse(text) >= 20000)
			{
				jsonobject.SetField(text, jsonobject2[text]);
			}
		}
		stopwatch.Stop();
		Debug.Log(string.Format("SavePackData消耗{0}ms", stopwatch.ElapsedMilliseconds));
		return jsonobject;
	}

	// Token: 0x06001857 RID: 6231 RVA: 0x000AA324 File Offset: 0x000A8524
	public JSONObject SaveNpcData(string npcData)
	{
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		JSONObject jsonobject = new JSONObject();
		JSONObject jsonobject2 = new JSONObject(npcData, -2, false, false);
		foreach (string text in jsonobject2.keys)
		{
			if (int.Parse(text) >= 20000)
			{
				jsonobject.SetField(text, jsonobject2[text]);
			}
		}
		stopwatch.Stop();
		Debug.Log(string.Format("SaveNpcData消耗{0}ms", stopwatch.ElapsedMilliseconds));
		return jsonobject;
	}

	// Token: 0x06001858 RID: 6232 RVA: 0x000AA3D0 File Offset: 0x000A85D0
	public string GetRemoveJSONObjectLess20000keys(JSONObject json, string str = null)
	{
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		if (str == null)
		{
			str = json.ToString();
		}
		if (json == null || json.keys == null || json.keys == null)
		{
			return str;
		}
		string value = "";
		foreach (string s in json.keys)
		{
			int num = int.Parse(s);
			if (num >= 20000)
			{
				value = string.Format(",\"{0}\":", num);
				break;
			}
		}
		if (string.IsNullOrWhiteSpace(value))
		{
			return str;
		}
		int count = str.IndexOf(value);
		string result = str.Remove(1, count);
		stopwatch.Stop();
		Debug.Log(string.Format("处理20000以下数据耗时{0}ms", stopwatch.ElapsedMilliseconds));
		return result;
	}

	// Token: 0x06001859 RID: 6233 RVA: 0x000AA4AC File Offset: 0x000A86AC
	public void LoadPlayerData(int id, int index)
	{
		this.isLoadComplete = false;
		Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("NpcJieSuan"));
		Action<object> <>9__1;
		Loom.RunAsync(delegate
		{
			try
			{
				NpcJieSuanManager.inst.ImportantNpcBangDingDictionary = new Dictionary<int, int>();
				this.initChuanYingFu();
				JSONObject jsonobject = FactoryManager.inst.SaveLoadFactory.GetJSONObject("NpcBackpack" + Tools.instance.getSaveID(id, index));
				if (jsonobject != null && jsonobject.keys != null)
				{
					foreach (string text in jsonobject.keys)
					{
						jsonData.instance.AvatarBackpackJsonData.SetField(text, jsonobject[text]);
					}
				}
				JSONObject jsonobject2 = FactoryManager.inst.SaveLoadFactory.GetJSONObject("NpcJsonData" + Tools.instance.getSaveID(id, index));
				if (jsonobject2 != null && jsonobject2.keys != null)
				{
					List<string> keys = jsonobject2.keys;
					foreach (string text2 in jsonobject2.keys)
					{
						jsonData.instance.AvatarJsonData.SetField(text2, jsonobject2[text2]);
						if (jsonobject2[text2].HasField("isImportant") && jsonobject2[text2]["isImportant"].b)
						{
							NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.Add(jsonobject2[text2]["BindingNpcID"].I, jsonobject2[text2]["id"].I);
						}
						if (!jsonData.instance.AvatarRandomJsonData.HasField(text2))
						{
							if (jsonobject2[text2]["isImportant"].b)
							{
								jsonData.instance.AvatarRandomJsonData.SetField(jsonobject2[text2]["id"].I.ToString(), jsonData.instance.AvatarRandomJsonData[jsonobject2[text2]["BindingNpcID"].I.ToString()]);
							}
							else
							{
								JSONObject jsonobject3 = jsonData.instance.randomAvatarFace(jsonobject2[text2], null);
								jsonData.instance.AvatarRandomJsonData.SetField(string.Concat(jsonobject2[text2]["id"].I), jsonobject3.Copy());
							}
						}
						if (!jsonData.instance.AvatarBackpackJsonData.HasField(text2))
						{
							FactoryManager.inst.npcFactory.InitAutoCreateNpcBackpack(jsonData.instance.AvatarBackpackJsonData, jsonobject2[text2]["id"].I, jsonobject2[text2]);
						}
					}
				}
				NpcJieSuanManager.inst.npcDeath.npcDeathJson = FactoryManager.inst.SaveLoadFactory.GetJSONObject("DeathNpcJsonData" + Tools.instance.getSaveID(id, index));
				NpcJieSuanManager.inst.npcChengHao.npcOnlyChengHao = FactoryManager.inst.SaveLoadFactory.GetJSONObject("OnlyChengHao" + Tools.instance.getSaveID(id, index));
				List<int> list = new List<int>();
				if (NpcJieSuanManager.inst.npcDeath.npcDeathJson.HasField("deathImportantList"))
				{
					list = NpcJieSuanManager.inst.npcDeath.npcDeathJson["deathImportantList"].ToList();
				}
				foreach (string text3 in jsonData.instance.NPCImportantDate.keys)
				{
					if (!NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(int.Parse(text3)) && (list.Count <= 0 || !list.Contains(int.Parse(text3))))
					{
						FactoryManager.inst.npcFactory.AfterCreateImprotantNpc(jsonData.instance.NPCImportantDate[text3], false);
					}
				}
				JSONObject jsonobject4 = FactoryManager.inst.SaveLoadFactory.GetJSONObject("JieSuanData" + Tools.instance.getSaveID(id, index));
				if (jsonobject4 != null && jsonobject4.keys != null)
				{
					NpcJieSuanManager.inst.JieSuanTimes = jsonobject4["JieSuanTimes"].I;
					NpcJieSuanManager.inst.JieSuanTime = jsonobject4["JieSuanTime"].str;
					Tools.instance.getPlayer().StreamData.NpcJieSuanData.Init();
					NpcJieSuanManager.inst.npcMap.bigMapNPCDictionary = new Dictionary<int, List<int>>();
					if (jsonobject4.HasField("bigMapJson"))
					{
						foreach (string text4 in jsonobject4["bigMapJson"].keys)
						{
							NpcJieSuanManager.inst.npcMap.bigMapNPCDictionary.Add(int.Parse(text4), new List<int>());
							foreach (JSONObject jsonobject5 in jsonobject4["bigMapJson"][text4].list)
							{
								NpcJieSuanManager.inst.npcMap.bigMapNPCDictionary[int.Parse(text4)].Add(jsonobject5.I);
							}
						}
					}
					NpcJieSuanManager.inst.npcMap.threeSenceNPCDictionary = new Dictionary<string, List<int>>();
					if (jsonobject4.HasField("threeSceneJson"))
					{
						foreach (string text5 in jsonobject4["threeSceneJson"].keys)
						{
							NpcJieSuanManager.inst.npcMap.threeSenceNPCDictionary.Add(text5, new List<int>());
							foreach (JSONObject jsonobject6 in jsonobject4["threeSceneJson"][text5].list)
							{
								NpcJieSuanManager.inst.npcMap.threeSenceNPCDictionary[text5].Add(jsonobject6.I);
							}
						}
					}
					NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary = new Dictionary<string, Dictionary<int, List<int>>>();
					if (jsonobject4.HasField("fuBenJson"))
					{
						foreach (string text6 in jsonobject4["fuBenJson"].keys)
						{
							NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary.Add(text6, new Dictionary<int, List<int>>());
							foreach (string text7 in jsonobject4["fuBenJson"][text6].keys)
							{
								NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary[text6].Add(int.Parse(text7), new List<int>());
								foreach (JSONObject jsonobject7 in jsonobject4["fuBenJson"][text6][text7].list)
								{
									NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary[text6][int.Parse(text7)].Add(jsonobject7.I);
								}
							}
						}
					}
				}
				Action<object> taction;
				if ((taction = <>9__1) == null)
				{
					taction = (<>9__1 = delegate(object obj)
					{
						int @int = FactoryManager.inst.SaveLoadFactory.GetInt("GameVersion" + Tools.instance.getSaveID(id, index));
						int gameVersion = GameVersion.inst.GetGameVersion();
						this.UpdateVersionData(gameVersion, @int);
						VersionMag.Inst.UpdateVersion(gameVersion, @int);
						StoryManager.Inst.ReInit();
					});
				}
				Loom.QueueOnMainThread(taction, null);
				Tools.instance.NextSaveTime = DateTime.Now;
				this.isLoadComplete = true;
			}
			catch (Exception ex)
			{
				Debug.LogError("读档失败");
				Debug.LogError(ex);
				UIPopTip.Inst.Pop("存档可能因云存档已损坏，无法读取", PopTipIconType.叹号);
			}
		});
	}

	// Token: 0x0600185A RID: 6234 RVA: 0x000AA500 File Offset: 0x000A8700
	public void NewLoadPlayerData(int avatarIndex, int slot)
	{
		this.isLoadComplete = false;
		Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("NpcJieSuan"));
		Loom.RunAsync(delegate
		{
			try
			{
				NpcJieSuanManager.inst.ImportantNpcBangDingDictionary = new Dictionary<int, int>();
				this.initChuanYingFu();
				JSONObject jsonobject = YSNewSaveSystem.LoadJSONObject("NpcBackpack.json", true);
				if (jsonobject != null && jsonobject.keys != null)
				{
					foreach (string text in jsonobject.keys)
					{
						jsonData.instance.AvatarBackpackJsonData.SetField(text, jsonobject[text]);
					}
				}
				JSONObject jsonobject2 = YSNewSaveSystem.LoadJSONObject("NpcJsonData.json", true);
				if (jsonobject2 != null && jsonobject2.keys != null)
				{
					List<string> keys = jsonobject2.keys;
					foreach (string text2 in jsonobject2.keys)
					{
						jsonData.instance.AvatarJsonData.SetField(text2, jsonobject2[text2]);
						if (jsonobject2[text2].HasField("isImportant") && jsonobject2[text2]["isImportant"].b)
						{
							NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.Add(jsonobject2[text2]["BindingNpcID"].I, jsonobject2[text2]["id"].I);
						}
						if (!jsonData.instance.AvatarRandomJsonData.HasField(text2))
						{
							if (jsonobject2[text2]["isImportant"].b)
							{
								jsonData.instance.AvatarRandomJsonData.SetField(jsonobject2[text2]["id"].I.ToString(), jsonData.instance.AvatarRandomJsonData[jsonobject2[text2]["BindingNpcID"].I.ToString()]);
							}
							else
							{
								JSONObject jsonobject3 = jsonData.instance.randomAvatarFace(jsonobject2[text2], null);
								jsonData.instance.AvatarRandomJsonData.SetField(string.Concat(jsonobject2[text2]["id"].I), jsonobject3.Copy());
							}
						}
						if (!jsonData.instance.AvatarBackpackJsonData.HasField(text2))
						{
							FactoryManager.inst.npcFactory.InitAutoCreateNpcBackpack(jsonData.instance.AvatarBackpackJsonData, jsonobject2[text2]["id"].I, jsonobject2[text2]);
						}
					}
				}
				NpcJieSuanManager.inst.npcDeath.npcDeathJson = YSNewSaveSystem.LoadJSONObject("DeathNpcJsonData.json", true);
				NpcJieSuanManager.inst.npcChengHao.npcOnlyChengHao = YSNewSaveSystem.LoadJSONObject("OnlyChengHao.json", true);
				List<int> list = new List<int>();
				if (NpcJieSuanManager.inst.npcDeath.npcDeathJson.HasField("deathImportantList"))
				{
					list = NpcJieSuanManager.inst.npcDeath.npcDeathJson["deathImportantList"].ToList();
				}
				foreach (string text3 in jsonData.instance.NPCImportantDate.keys)
				{
					if (!NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(int.Parse(text3)) && (list.Count <= 0 || !list.Contains(int.Parse(text3))))
					{
						FactoryManager.inst.npcFactory.AfterCreateImprotantNpc(jsonData.instance.NPCImportantDate[text3], false);
					}
				}
				JSONObject jsonobject4 = YSNewSaveSystem.LoadJSONObject("JieSuanData.json", true);
				if (jsonobject4 != null && jsonobject4.keys != null)
				{
					NpcJieSuanManager.inst.JieSuanTimes = jsonobject4["JieSuanTimes"].I;
					NpcJieSuanManager.inst.JieSuanTime = jsonobject4["JieSuanTime"].str;
					Tools.instance.getPlayer().StreamData.NpcJieSuanData.Init();
					NpcJieSuanManager.inst.npcMap.bigMapNPCDictionary = new Dictionary<int, List<int>>();
					if (jsonobject4.HasField("bigMapJson"))
					{
						foreach (string text4 in jsonobject4["bigMapJson"].keys)
						{
							NpcJieSuanManager.inst.npcMap.bigMapNPCDictionary.Add(int.Parse(text4), new List<int>());
							foreach (JSONObject jsonobject5 in jsonobject4["bigMapJson"][text4].list)
							{
								NpcJieSuanManager.inst.npcMap.bigMapNPCDictionary[int.Parse(text4)].Add(jsonobject5.I);
							}
						}
					}
					NpcJieSuanManager.inst.npcMap.threeSenceNPCDictionary = new Dictionary<string, List<int>>();
					if (jsonobject4.HasField("threeSceneJson"))
					{
						foreach (string text5 in jsonobject4["threeSceneJson"].keys)
						{
							NpcJieSuanManager.inst.npcMap.threeSenceNPCDictionary.Add(text5, new List<int>());
							foreach (JSONObject jsonobject6 in jsonobject4["threeSceneJson"][text5].list)
							{
								NpcJieSuanManager.inst.npcMap.threeSenceNPCDictionary[text5].Add(jsonobject6.I);
							}
						}
					}
					NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary = new Dictionary<string, Dictionary<int, List<int>>>();
					if (jsonobject4.HasField("fuBenJson"))
					{
						foreach (string text6 in jsonobject4["fuBenJson"].keys)
						{
							NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary.Add(text6, new Dictionary<int, List<int>>());
							foreach (string text7 in jsonobject4["fuBenJson"][text6].keys)
							{
								NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary[text6].Add(int.Parse(text7), new List<int>());
								foreach (JSONObject jsonobject7 in jsonobject4["fuBenJson"][text6][text7].list)
								{
									NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary[text6][int.Parse(text7)].Add(jsonobject7.I);
								}
							}
						}
					}
				}
				Loom.QueueOnMainThread(delegate(object obj)
				{
					int oldVersion = YSNewSaveSystem.LoadInt("GameVersion.txt", true);
					int gameVersion = GameVersion.inst.GetGameVersion();
					this.UpdateVersionData(gameVersion, oldVersion);
					VersionMag.Inst.UpdateVersion(gameVersion, oldVersion);
					StoryManager.Inst.ReInit();
				}, null);
				Tools.instance.NextSaveTime = DateTime.Now;
				this.isLoadComplete = true;
			}
			catch (Exception ex)
			{
				Debug.LogError("读档失败");
				Debug.LogError(ex);
				UIPopTip.Inst.Pop("存档可能已损坏，无法读取", PopTipIconType.叹号);
			}
		});
	}

	// Token: 0x0600185B RID: 6235 RVA: 0x000AA530 File Offset: 0x000A8730
	public void UpdateVersionData(int nowVersion, int oldVersion)
	{
		string str = string.Format("LoadPlayerDateFactory.UpdateVersionData({0}, {1}) ", nowVersion, oldVersion);
		if (oldVersion < nowVersion)
		{
			if (oldVersion == 1 && GlobalValue.Get(568, str + "百里奇事件结束") == 1)
			{
				PlayerEx.AddSeaTanSuoDu(17, 50);
				PlayerEx.AddShengWang(23, 50, false);
				oldVersion = 2;
			}
			if (oldVersion == 2)
			{
				int num = GlobalValue.Get(1711, str + "进入气眼的次数");
				if (num == 1)
				{
					Tools.instance.getPlayer().addItem(10055, 1, Tools.CreateItemSeid(10055), false);
				}
				if (num >= 2)
				{
					Tools.instance.getPlayer().addItem(10055, 1, Tools.CreateItemSeid(10055), false);
					Tools.instance.getPlayer().addItem(10056, 1, Tools.CreateItemSeid(10056), false);
				}
				if (GlobalValue.Get(1703, str + "敖灵的求助接取任务") >= 3 && GlobalValue.Get(1702, str + "敖灵的求助") == 2)
				{
					GlobalValue.Set(1703, 0, str + "敖灵的求助接取任务");
					GlobalValue.Set(1702, 0, str + "敖灵的求助");
				}
				oldVersion = 5;
			}
			if (oldVersion == 5)
			{
				Avatar player = Tools.instance.getPlayer();
				int chengHao = player.chengHao;
				if (chengHao >= 6 && chengHao <= 10)
				{
					player.chengHao = chengHao - 1;
				}
				int num2 = player.StaticValue.Value[998];
				int num3 = player.StaticValue.Value[1825];
				int num4 = player.StaticValue.Value[853];
				int num5 = player.StaticValue.Value[531];
				int num6 = player.StaticValue.Value[586];
				if (num2 > 0 && num2 < 4)
				{
					player.jianLingManager.UnlockXianSuo("神秘铁剑杀戮剑灵");
					player.jianLingManager.UnlockXianSuo("昔日身份异常剑灵");
				}
				else if (num2 == 4)
				{
					player.jianLingManager.UnlockXianSuo("神秘铁剑杀戮剑灵");
					player.jianLingManager.UnlockXianSuo("神秘铁剑天魔眼");
					player.jianLingManager.UnlockXianSuo("昔日身份异常剑灵");
					player.jianLingManager.UnlockXianSuo("昔日身份御剑门");
				}
				else if (num2 == 5)
				{
					player.jianLingManager.UnlockXianSuo("神秘铁剑杀戮剑灵");
					player.jianLingManager.UnlockXianSuo("神秘铁剑天魔眼");
					player.jianLingManager.UnlockXianSuo("昔日身份异常剑灵");
					player.jianLingManager.UnlockXianSuo("昔日身份御剑门");
					player.jianLingManager.UnlockXianSuo("御剑门传闻");
				}
				else if (num2 == 6)
				{
					player.jianLingManager.UnlockXianSuo("神秘铁剑杀戮剑灵");
					player.jianLingManager.UnlockXianSuo("神秘铁剑天魔眼");
					player.jianLingManager.UnlockXianSuo("神秘铁剑戮仙剑");
					player.jianLingManager.UnlockXianSuo("昔日身份异常剑灵");
					player.jianLingManager.UnlockXianSuo("昔日身份御剑门");
					player.jianLingManager.UnlockXianSuo("昔日身份古迹");
					player.jianLingManager.UnlockXianSuo("昔日身份玄清");
					player.jianLingManager.UnlockXianSuo("昔日身份戮仙剑");
					player.jianLingManager.UnlockXianSuo("御剑门传闻");
					player.jianLingManager.UnlockZhenXiang("神秘铁剑");
					player.jianLingManager.UnlockZhenXiang("昔日身份");
				}
				else if (num2 > 6)
				{
					player.jianLingManager.UnlockXianSuo("神秘铁剑杀戮剑灵");
					player.jianLingManager.UnlockXianSuo("神秘铁剑天魔眼");
					player.jianLingManager.UnlockXianSuo("神秘铁剑戮仙剑");
					player.jianLingManager.UnlockXianSuo("昔日身份异常剑灵");
					player.jianLingManager.UnlockXianSuo("昔日身份御剑门");
					player.jianLingManager.UnlockXianSuo("昔日身份古迹");
					player.jianLingManager.UnlockXianSuo("昔日身份玄清");
					player.jianLingManager.UnlockXianSuo("昔日身份戮仙剑");
					player.jianLingManager.UnlockXianSuo("御剑门传闻");
					player.jianLingManager.UnlockXianSuo("魔道踪影血剑宫二");
					player.jianLingManager.UnlockXianSuo("魔道踪影血煞符");
					player.jianLingManager.UnlockZhenXiang("神秘铁剑");
					player.jianLingManager.UnlockZhenXiang("昔日身份");
				}
				if (num3 > 10)
				{
					player.jianLingManager.UnlockXianSuo("魔道踪影天魔道四");
				}
				if (num4 > 0)
				{
					player.jianLingManager.UnlockXianSuo("魔道踪影古神教一");
				}
				if (num5 > 0)
				{
					player.jianLingManager.UnlockXianSuo("魔道踪影古神教二");
				}
				if (num6 > 0)
				{
					player.jianLingManager.UnlockXianSuo("魔道踪影古神教三");
				}
				oldVersion = 6;
			}
			if (oldVersion == 6)
			{
				this.UpdateGuDingNpcFace();
				Tools.instance.getPlayer().addItem(5322, 1, Tools.CreateItemSeid(5322), true);
				oldVersion = 7;
			}
			if (oldVersion == 7)
			{
				this.UpdateGuDingNpcFace();
				oldVersion = 8;
			}
			if (oldVersion == 8)
			{
				this.UpdateQingJiaoItem();
				oldVersion = 9;
			}
		}
	}

	// Token: 0x0600185C RID: 6236 RVA: 0x000AA9F8 File Offset: 0x000A8BF8
	public void UpdateQingJiaoItem()
	{
		Avatar player = PlayerEx.Player;
		int count = player.itemList.values.Count;
		for (int i = 0; i < count; i++)
		{
			ITEM_INFO item_INFO = player.itemList.values[i];
			if (item_INFO.itemId > 100000)
			{
				item_INFO.itemId = item_INFO.itemId - 100000 + 1000000000;
			}
		}
	}

	// Token: 0x0600185D RID: 6237 RVA: 0x000AAA60 File Offset: 0x000A8C60
	public void UpdateGuDingNpcFace()
	{
		foreach (StaticFaceInfo staticFaceInfo in SetAvatarFaceRandomInfo.inst.StaticRandomInfo)
		{
			string text = staticFaceInfo.AvatarScope.ToString();
			if (!jsonData.instance.MonstarIsDeath(staticFaceInfo.AvatarScope) && jsonData.instance.AvatarRandomJsonData.HasField(text))
			{
				jsonData.instance.UpdateGuDingNpcFace(staticFaceInfo.AvatarScope, jsonData.instance.AvatarRandomJsonData[text]);
				string text2 = NPCEx.NPCIDToNew(staticFaceInfo.AvatarScope).ToString();
				if (text2 != text)
				{
					jsonData.instance.UpdateGuDingNpcFace(staticFaceInfo.AvatarScope, jsonData.instance.AvatarRandomJsonData[text2]);
				}
			}
		}
	}

	// Token: 0x0600185E RID: 6238 RVA: 0x000AAB44 File Offset: 0x000A8D44
	private void initChuanYingFu()
	{
		Avatar player = Tools.instance.getPlayer();
		player.ToalChuanYingFuList = new JSONObject();
		List<JSONObject> list = jsonData.instance.ChuanYingFuBiao.list;
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i]["Type"].I != 3)
			{
				player.ToalChuanYingFuList.SetField(list[i]["id"].I.ToString(), list[i]);
			}
		}
		JSONObject hasSendChuanYingFuList = player.HasSendChuanYingFuList;
		GlobalValue.Get(63, "LoadPlayerDateFactory.initChuanYingFu");
		for (int j = 0; j < hasSendChuanYingFuList.Count; j++)
		{
			try
			{
				player.ToalChuanYingFuList.RemoveField(hasSendChuanYingFuList[j]["id"].I.ToString());
			}
			catch (Exception)
			{
			}
		}
	}

	// Token: 0x04001367 RID: 4967
	private Thread loadAutoNPCThread;

	// Token: 0x04001368 RID: 4968
	public bool isLoadComplete;
}
