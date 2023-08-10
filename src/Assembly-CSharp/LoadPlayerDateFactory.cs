using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using KBEngine;
using UnityEngine;
using YSGame;
using script.YarnEditor.Manager;

public class LoadPlayerDateFactory
{
	private Thread loadAutoNPCThread;

	public bool isLoadComplete;

	public JSONObject SaveJieSuanData(Dictionary<int, List<int>> bigMap, Dictionary<string, List<int>> threeScene, Dictionary<string, Dictionary<int, List<int>>> fuBen, JSONObject data)
	{
		if (bigMap.Count > 0)
		{
			JSONObject jSONObject = new JSONObject();
			foreach (int key in bigMap.Keys)
			{
				JSONObject arr = JSONObject.arr;
				foreach (int item in bigMap[key])
				{
					arr.Add(item);
				}
				jSONObject.SetField(key.ToString(), arr);
			}
			data.SetField("bigMapJson", jSONObject);
		}
		if (threeScene.Count > 0)
		{
			JSONObject jSONObject2 = new JSONObject();
			foreach (string key2 in threeScene.Keys)
			{
				JSONObject arr2 = JSONObject.arr;
				foreach (int item2 in threeScene[key2])
				{
					arr2.Add(item2);
				}
				jSONObject2.SetField(key2.ToString(), arr2);
			}
			data.SetField("threeSceneJson", jSONObject2);
		}
		if (fuBen.Count > 0)
		{
			JSONObject jSONObject3 = new JSONObject();
			foreach (string key3 in fuBen.Keys)
			{
				JSONObject jSONObject4 = new JSONObject();
				foreach (int key4 in fuBen[key3].Keys)
				{
					JSONObject arr3 = JSONObject.arr;
					foreach (int item3 in fuBen[key3][key4])
					{
						arr3.Add(item3);
					}
					jSONObject4.SetField(key4.ToString(), arr3);
				}
				jSONObject3.SetField(key3.ToString(), jSONObject4);
			}
			data.SetField("fuBenJson", jSONObject3);
		}
		return data;
	}

	public JSONObject SavePackData(string packData)
	{
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		JSONObject jSONObject = new JSONObject();
		JSONObject jSONObject2 = new JSONObject(packData);
		foreach (string key in jSONObject2.keys)
		{
			if (int.Parse(key) >= 20000)
			{
				jSONObject.SetField(key, jSONObject2[key]);
			}
		}
		stopwatch.Stop();
		Debug.Log((object)$"SavePackData消耗{stopwatch.ElapsedMilliseconds}ms");
		return jSONObject;
	}

	public JSONObject SaveNpcData(string npcData)
	{
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		JSONObject jSONObject = new JSONObject();
		JSONObject jSONObject2 = new JSONObject(npcData);
		foreach (string key in jSONObject2.keys)
		{
			if (int.Parse(key) >= 20000)
			{
				jSONObject.SetField(key, jSONObject2[key]);
			}
		}
		stopwatch.Stop();
		Debug.Log((object)$"SaveNpcData消耗{stopwatch.ElapsedMilliseconds}ms");
		return jSONObject;
	}

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
		foreach (string key in json.keys)
		{
			int num = int.Parse(key);
			if (num >= 20000)
			{
				value = $",\"{num}\":";
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
		Debug.Log((object)$"处理20000以下数据耗时{stopwatch.ElapsedMilliseconds}ms");
		return result;
	}

	public void LoadPlayerData(int id, int index)
	{
		isLoadComplete = false;
		Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("NpcJieSuan"));
		Loom.RunAsync(delegate
		{
			try
			{
				NpcJieSuanManager.inst.ImportantNpcBangDingDictionary = new Dictionary<int, int>();
				initChuanYingFu();
				JSONObject jSONObject = FactoryManager.inst.SaveLoadFactory.GetJSONObject("NpcBackpack" + Tools.instance.getSaveID(id, index));
				if (jSONObject != null && jSONObject.keys != null)
				{
					foreach (string key in jSONObject.keys)
					{
						jsonData.instance.AvatarBackpackJsonData.SetField(key, jSONObject[key]);
					}
				}
				JSONObject jSONObject2 = FactoryManager.inst.SaveLoadFactory.GetJSONObject("NpcJsonData" + Tools.instance.getSaveID(id, index));
				if (jSONObject2 != null && jSONObject2.keys != null)
				{
					_ = jSONObject2.keys;
					foreach (string key2 in jSONObject2.keys)
					{
						jsonData.instance.AvatarJsonData.SetField(key2, jSONObject2[key2]);
						if (jSONObject2[key2].HasField("isImportant") && jSONObject2[key2]["isImportant"].b)
						{
							NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.Add(jSONObject2[key2]["BindingNpcID"].I, jSONObject2[key2]["id"].I);
						}
						if (!jsonData.instance.AvatarRandomJsonData.HasField(key2))
						{
							if (jSONObject2[key2]["isImportant"].b)
							{
								jsonData.instance.AvatarRandomJsonData.SetField(jSONObject2[key2]["id"].I.ToString(), jsonData.instance.AvatarRandomJsonData[jSONObject2[key2]["BindingNpcID"].I.ToString()]);
							}
							else
							{
								JSONObject jSONObject3 = jsonData.instance.randomAvatarFace(jSONObject2[key2]);
								jsonData.instance.AvatarRandomJsonData.SetField(string.Concat(jSONObject2[key2]["id"].I), jSONObject3.Copy());
							}
						}
						if (!jsonData.instance.AvatarBackpackJsonData.HasField(key2))
						{
							FactoryManager.inst.npcFactory.InitAutoCreateNpcBackpack(jsonData.instance.AvatarBackpackJsonData, jSONObject2[key2]["id"].I, jSONObject2[key2]);
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
				foreach (string key3 in jsonData.instance.NPCImportantDate.keys)
				{
					if (!NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(int.Parse(key3)) && (list.Count <= 0 || !list.Contains(int.Parse(key3))))
					{
						FactoryManager.inst.npcFactory.AfterCreateImprotantNpc(jsonData.instance.NPCImportantDate[key3], isNewPlayer: false);
					}
				}
				JSONObject jSONObject4 = FactoryManager.inst.SaveLoadFactory.GetJSONObject("JieSuanData" + Tools.instance.getSaveID(id, index));
				if (jSONObject4 != null && jSONObject4.keys != null)
				{
					NpcJieSuanManager.inst.JieSuanTimes = jSONObject4["JieSuanTimes"].I;
					NpcJieSuanManager.inst.JieSuanTime = jSONObject4["JieSuanTime"].str;
					Tools.instance.getPlayer().StreamData.NpcJieSuanData.Init();
					NpcJieSuanManager.inst.npcMap.bigMapNPCDictionary = new Dictionary<int, List<int>>();
					if (jSONObject4.HasField("bigMapJson"))
					{
						foreach (string key4 in jSONObject4["bigMapJson"].keys)
						{
							NpcJieSuanManager.inst.npcMap.bigMapNPCDictionary.Add(int.Parse(key4), new List<int>());
							foreach (JSONObject item in jSONObject4["bigMapJson"][key4].list)
							{
								NpcJieSuanManager.inst.npcMap.bigMapNPCDictionary[int.Parse(key4)].Add(item.I);
							}
						}
					}
					NpcJieSuanManager.inst.npcMap.threeSenceNPCDictionary = new Dictionary<string, List<int>>();
					if (jSONObject4.HasField("threeSceneJson"))
					{
						foreach (string key5 in jSONObject4["threeSceneJson"].keys)
						{
							NpcJieSuanManager.inst.npcMap.threeSenceNPCDictionary.Add(key5, new List<int>());
							foreach (JSONObject item2 in jSONObject4["threeSceneJson"][key5].list)
							{
								NpcJieSuanManager.inst.npcMap.threeSenceNPCDictionary[key5].Add(item2.I);
							}
						}
					}
					NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary = new Dictionary<string, Dictionary<int, List<int>>>();
					if (jSONObject4.HasField("fuBenJson"))
					{
						foreach (string key6 in jSONObject4["fuBenJson"].keys)
						{
							NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary.Add(key6, new Dictionary<int, List<int>>());
							foreach (string key7 in jSONObject4["fuBenJson"][key6].keys)
							{
								NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary[key6].Add(int.Parse(key7), new List<int>());
								foreach (JSONObject item3 in jSONObject4["fuBenJson"][key6][key7].list)
								{
									NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary[key6][int.Parse(key7)].Add(item3.I);
								}
							}
						}
					}
				}
				Loom.QueueOnMainThread(delegate
				{
					int @int = FactoryManager.inst.SaveLoadFactory.GetInt("GameVersion" + Tools.instance.getSaveID(id, index));
					int gameVersion = GameVersion.inst.GetGameVersion();
					UpdateVersionData(gameVersion, @int);
					VersionMag.Inst.UpdateVersion(gameVersion, @int);
					StoryManager.Inst.ReInit();
				}, null);
				Tools.instance.NextSaveTime = DateTime.Now;
				isLoadComplete = true;
			}
			catch (Exception ex)
			{
				Debug.LogError((object)"读档失败");
				Debug.LogError((object)ex);
				UIPopTip.Inst.Pop("存档可能因云存档已损坏，无法读取");
			}
		});
	}

	public void NewLoadPlayerData(int avatarIndex, int slot)
	{
		isLoadComplete = false;
		Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("NpcJieSuan"));
		Loom.RunAsync(delegate
		{
			try
			{
				NpcJieSuanManager.inst.ImportantNpcBangDingDictionary = new Dictionary<int, int>();
				initChuanYingFu();
				JSONObject jSONObject = YSNewSaveSystem.LoadJSONObject("NpcBackpack.json");
				if (jSONObject != null && jSONObject.keys != null)
				{
					foreach (string key in jSONObject.keys)
					{
						jsonData.instance.AvatarBackpackJsonData.SetField(key, jSONObject[key]);
					}
				}
				JSONObject jSONObject2 = YSNewSaveSystem.LoadJSONObject("NpcJsonData.json");
				if (jSONObject2 != null && jSONObject2.keys != null)
				{
					_ = jSONObject2.keys;
					foreach (string key2 in jSONObject2.keys)
					{
						jsonData.instance.AvatarJsonData.SetField(key2, jSONObject2[key2]);
						if (jSONObject2[key2].HasField("isImportant") && jSONObject2[key2]["isImportant"].b)
						{
							NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.Add(jSONObject2[key2]["BindingNpcID"].I, jSONObject2[key2]["id"].I);
						}
						if (!jsonData.instance.AvatarRandomJsonData.HasField(key2))
						{
							if (jSONObject2[key2]["isImportant"].b)
							{
								jsonData.instance.AvatarRandomJsonData.SetField(jSONObject2[key2]["id"].I.ToString(), jsonData.instance.AvatarRandomJsonData[jSONObject2[key2]["BindingNpcID"].I.ToString()]);
							}
							else
							{
								JSONObject jSONObject3 = jsonData.instance.randomAvatarFace(jSONObject2[key2]);
								jsonData.instance.AvatarRandomJsonData.SetField(string.Concat(jSONObject2[key2]["id"].I), jSONObject3.Copy());
							}
						}
						if (!jsonData.instance.AvatarBackpackJsonData.HasField(key2))
						{
							FactoryManager.inst.npcFactory.InitAutoCreateNpcBackpack(jsonData.instance.AvatarBackpackJsonData, jSONObject2[key2]["id"].I, jSONObject2[key2]);
						}
					}
				}
				NpcJieSuanManager.inst.npcDeath.npcDeathJson = YSNewSaveSystem.LoadJSONObject("DeathNpcJsonData.json");
				NpcJieSuanManager.inst.npcChengHao.npcOnlyChengHao = YSNewSaveSystem.LoadJSONObject("OnlyChengHao.json");
				List<int> list = new List<int>();
				if (NpcJieSuanManager.inst.npcDeath.npcDeathJson.HasField("deathImportantList"))
				{
					list = NpcJieSuanManager.inst.npcDeath.npcDeathJson["deathImportantList"].ToList();
				}
				foreach (string key3 in jsonData.instance.NPCImportantDate.keys)
				{
					if (!NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(int.Parse(key3)) && (list.Count <= 0 || !list.Contains(int.Parse(key3))))
					{
						FactoryManager.inst.npcFactory.AfterCreateImprotantNpc(jsonData.instance.NPCImportantDate[key3], isNewPlayer: false);
					}
				}
				JSONObject jSONObject4 = YSNewSaveSystem.LoadJSONObject("JieSuanData.json");
				if (jSONObject4 != null && jSONObject4.keys != null)
				{
					NpcJieSuanManager.inst.JieSuanTimes = jSONObject4["JieSuanTimes"].I;
					NpcJieSuanManager.inst.JieSuanTime = jSONObject4["JieSuanTime"].str;
					Tools.instance.getPlayer().StreamData.NpcJieSuanData.Init();
					NpcJieSuanManager.inst.npcMap.bigMapNPCDictionary = new Dictionary<int, List<int>>();
					if (jSONObject4.HasField("bigMapJson"))
					{
						foreach (string key4 in jSONObject4["bigMapJson"].keys)
						{
							NpcJieSuanManager.inst.npcMap.bigMapNPCDictionary.Add(int.Parse(key4), new List<int>());
							foreach (JSONObject item in jSONObject4["bigMapJson"][key4].list)
							{
								NpcJieSuanManager.inst.npcMap.bigMapNPCDictionary[int.Parse(key4)].Add(item.I);
							}
						}
					}
					NpcJieSuanManager.inst.npcMap.threeSenceNPCDictionary = new Dictionary<string, List<int>>();
					if (jSONObject4.HasField("threeSceneJson"))
					{
						foreach (string key5 in jSONObject4["threeSceneJson"].keys)
						{
							NpcJieSuanManager.inst.npcMap.threeSenceNPCDictionary.Add(key5, new List<int>());
							foreach (JSONObject item2 in jSONObject4["threeSceneJson"][key5].list)
							{
								NpcJieSuanManager.inst.npcMap.threeSenceNPCDictionary[key5].Add(item2.I);
							}
						}
					}
					NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary = new Dictionary<string, Dictionary<int, List<int>>>();
					if (jSONObject4.HasField("fuBenJson"))
					{
						foreach (string key6 in jSONObject4["fuBenJson"].keys)
						{
							NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary.Add(key6, new Dictionary<int, List<int>>());
							foreach (string key7 in jSONObject4["fuBenJson"][key6].keys)
							{
								NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary[key6].Add(int.Parse(key7), new List<int>());
								foreach (JSONObject item3 in jSONObject4["fuBenJson"][key6][key7].list)
								{
									NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary[key6][int.Parse(key7)].Add(item3.I);
								}
							}
						}
					}
				}
				Loom.QueueOnMainThread(delegate
				{
					int oldVersion = YSNewSaveSystem.LoadInt("GameVersion.txt");
					int gameVersion = GameVersion.inst.GetGameVersion();
					UpdateVersionData(gameVersion, oldVersion);
					VersionMag.Inst.UpdateVersion(gameVersion, oldVersion);
					StoryManager.Inst.ReInit();
				}, null);
				Tools.instance.NextSaveTime = DateTime.Now;
				isLoadComplete = true;
			}
			catch (Exception ex)
			{
				Debug.LogError((object)"读档失败");
				Debug.LogError((object)ex);
				UIPopTip.Inst.Pop("存档可能已损坏，无法读取");
			}
		});
	}

	public void UpdateVersionData(int nowVersion, int oldVersion)
	{
		string text = $"LoadPlayerDateFactory.UpdateVersionData({nowVersion}, {oldVersion}) ";
		if (oldVersion >= nowVersion)
		{
			return;
		}
		if (oldVersion == 1 && GlobalValue.Get(568, text + "百里奇事件结束") == 1)
		{
			PlayerEx.AddSeaTanSuoDu(17, 50);
			PlayerEx.AddShengWang(23, 50);
			oldVersion = 2;
		}
		if (oldVersion == 2)
		{
			int num = GlobalValue.Get(1711, text + "进入气眼的次数");
			if (num == 1)
			{
				Tools.instance.getPlayer().addItem(10055, 1, Tools.CreateItemSeid(10055));
			}
			if (num >= 2)
			{
				Tools.instance.getPlayer().addItem(10055, 1, Tools.CreateItemSeid(10055));
				Tools.instance.getPlayer().addItem(10056, 1, Tools.CreateItemSeid(10056));
			}
			if (GlobalValue.Get(1703, text + "敖灵的求助接取任务") >= 3 && GlobalValue.Get(1702, text + "敖灵的求助") == 2)
			{
				GlobalValue.Set(1703, 0, text + "敖灵的求助接取任务");
				GlobalValue.Set(1702, 0, text + "敖灵的求助");
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
			UpdateGuDingNpcFace();
			Tools.instance.getPlayer().addItem(5322, 1, Tools.CreateItemSeid(5322), ShowText: true);
			oldVersion = 7;
		}
		if (oldVersion == 7)
		{
			UpdateGuDingNpcFace();
			oldVersion = 8;
		}
		if (oldVersion == 8)
		{
			UpdateQingJiaoItem();
			oldVersion = 9;
		}
	}

	public void UpdateQingJiaoItem()
	{
		Avatar player = PlayerEx.Player;
		int count = player.itemList.values.Count;
		for (int i = 0; i < count; i++)
		{
			ITEM_INFO iTEM_INFO = player.itemList.values[i];
			if (iTEM_INFO.itemId > 100000)
			{
				iTEM_INFO.itemId = iTEM_INFO.itemId - 100000 + 1000000000;
			}
		}
	}

	public void UpdateGuDingNpcFace()
	{
		foreach (StaticFaceInfo item in SetAvatarFaceRandomInfo.inst.StaticRandomInfo)
		{
			string text = item.AvatarScope.ToString();
			if (!jsonData.instance.MonstarIsDeath(item.AvatarScope) && jsonData.instance.AvatarRandomJsonData.HasField(text))
			{
				jsonData.instance.UpdateGuDingNpcFace(item.AvatarScope, jsonData.instance.AvatarRandomJsonData[text]);
				string text2 = NPCEx.NPCIDToNew(item.AvatarScope).ToString();
				if (text2 != text)
				{
					jsonData.instance.UpdateGuDingNpcFace(item.AvatarScope, jsonData.instance.AvatarRandomJsonData[text2]);
				}
			}
		}
	}

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
}
