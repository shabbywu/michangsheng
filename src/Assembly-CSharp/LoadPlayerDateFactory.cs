using System;
using System.Collections.Generic;
using System.Threading;
using KBEngine;
using script.YarnEditor.Manager;
using UnityEngine;

// Token: 0x020003ED RID: 1005
public class LoadPlayerDateFactory
{
	// Token: 0x06001B4D RID: 6989 RVA: 0x000F0E9C File Offset: 0x000EF09C
	public JSONObject saveJieSuanData(Dictionary<int, List<int>> bigMap, Dictionary<string, List<int>> threeScene, Dictionary<string, Dictionary<int, List<int>>> fuBen, JSONObject data)
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

	// Token: 0x06001B4E RID: 6990 RVA: 0x000F1144 File Offset: 0x000EF344
	public JSONObject savePackDate(string packDate)
	{
		JSONObject jsonobject = new JSONObject();
		JSONObject jsonobject2 = new JSONObject(packDate, -2, false, false);
		foreach (string text in jsonobject2.keys)
		{
			if (int.Parse(text) >= 20000)
			{
				jsonobject.SetField(text, jsonobject2[text]);
			}
		}
		return jsonobject;
	}

	// Token: 0x06001B4F RID: 6991 RVA: 0x000F1144 File Offset: 0x000EF344
	public JSONObject savaNpcDate(string npcDate)
	{
		JSONObject jsonobject = new JSONObject();
		JSONObject jsonobject2 = new JSONObject(npcDate, -2, false, false);
		foreach (string text in jsonobject2.keys)
		{
			if (int.Parse(text) >= 20000)
			{
				jsonobject.SetField(text, jsonobject2[text]);
			}
		}
		return jsonobject;
	}

	// Token: 0x06001B50 RID: 6992 RVA: 0x000F11C0 File Offset: 0x000EF3C0
	public void LoadPlayerDate(int id, int index)
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
								JSONObject jsonobject3 = jsonData.instance.randomAvatarFace(jsonobject2[text2], jsonData.instance.AvatarRandomJsonData.HasField(string.Concat((int)jsonobject2[text2]["id"].n)) ? jsonData.instance.AvatarRandomJsonData[((int)jsonobject2[text2]["id"].n).ToString()] : null);
								jsonData.instance.AvatarRandomJsonData.SetField(string.Concat((int)jsonobject2[text2]["id"].n), jsonobject3.Clone());
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

	// Token: 0x06001B51 RID: 6993 RVA: 0x000F1214 File Offset: 0x000EF414
	public void UpdateVersionData(int nowVersion, int oldVersion)
	{
		string str = string.Format("LoadPlayerDateFactory.UpdateVersionData({0}, {1}) ", nowVersion, oldVersion);
		if (oldVersion < nowVersion)
		{
			if (oldVersion == 1 && GlobalValue.Get(568, str + "百里奇事件结束") == 1)
			{
				PlayerEx.AddSeaTanSuoDu(17, 50);
				PlayerEx.AddShengWang(23, 50, false);
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
			}
		}
	}

	// Token: 0x06001B52 RID: 6994 RVA: 0x000F1350 File Offset: 0x000EF550
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

	// Token: 0x0400170A RID: 5898
	private Thread loadAutoNPCThread;

	// Token: 0x0400170B RID: 5899
	public bool isLoadComplete;
}
