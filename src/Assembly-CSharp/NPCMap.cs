using System;
using System.Collections.Generic;

// Token: 0x0200032B RID: 811
public class NPCMap
{
	// Token: 0x060017DE RID: 6110 RVA: 0x00015064 File Offset: 0x00013264
	public NPCMap()
	{
		this.RestartMap();
	}

	// Token: 0x060017DF RID: 6111 RVA: 0x0001507D File Offset: 0x0001327D
	public void RestartMap()
	{
		this.bigMapNPCDictionary = new Dictionary<int, List<int>>();
		this.bigMapNpcTypeIndexDictionary = new Dictionary<int, List<int>>();
		this.threeSenceNPCDictionary = new Dictionary<string, List<int>>();
		this.fuBenNPCDictionary = new Dictionary<string, Dictionary<int, List<int>>>();
		this.allMapIndexList = new List<int>();
	}

	// Token: 0x060017E0 RID: 6112 RVA: 0x000D0930 File Offset: 0x000CEB30
	public void AddNpcToBigMap(int npcId, int type, bool isCanJieSha = true)
	{
		List<int> list = new List<int>();
		if (type == 1)
		{
			if (this.allMapIndexList.Count < 1)
			{
				foreach (JSONObject jsonobject in jsonData.instance.NpcBigMapBingDate.list)
				{
					if (jsonobject["MapType"].I == 1)
					{
						this.allMapIndexList.Add(jsonobject["MapD"].I);
					}
				}
			}
			list = this.allMapIndexList;
		}
		else
		{
			int i = jsonData.instance.AvatarJsonData[npcId.ToString()]["Type"].I;
			if (!this.bigMapNpcTypeIndexDictionary.ContainsKey(i))
			{
				foreach (JSONObject jsonobject2 in jsonData.instance.NpcBigMapBingDate.list)
				{
					if (jsonobject2["MapType"].I == 2)
					{
						if (this.bigMapNpcTypeIndexDictionary.ContainsKey(jsonobject2["NPCType"].I))
						{
							this.bigMapNpcTypeIndexDictionary[jsonobject2["NPCType"].I].Add(jsonobject2["MapD"].I);
						}
						else
						{
							this.bigMapNpcTypeIndexDictionary.Add(jsonobject2["NPCType"].I, new List<int>
							{
								jsonobject2["MapD"].I
							});
						}
					}
				}
			}
			list = this.bigMapNpcTypeIndexDictionary[i];
		}
		int key = list[NpcJieSuanManager.inst.getRandomInt(0, list.Count - 1)];
		if (this.bigMapNPCDictionary.ContainsKey(key))
		{
			this.bigMapNPCDictionary[key].Add(npcId);
		}
		else
		{
			this.bigMapNPCDictionary.Add(key, new List<int>
			{
				npcId
			});
		}
		if (isCanJieSha && NPCEx.GetFavor(npcId) < 200)
		{
			NpcJieSuanManager.inst.allBigMapNpcList.Add(npcId);
		}
	}

	// Token: 0x060017E1 RID: 6113 RVA: 0x000D0B84 File Offset: 0x000CED84
	public void AddNpcToThreeScene(int npcId, int sceneId)
	{
		string key = "S" + sceneId;
		if (this.threeSenceNPCDictionary.ContainsKey(key))
		{
			this.threeSenceNPCDictionary[key].Add(npcId);
			return;
		}
		this.threeSenceNPCDictionary.Add(key, new List<int>
		{
			npcId
		});
	}

	// Token: 0x060017E2 RID: 6114 RVA: 0x000150B6 File Offset: 0x000132B6
	public void AddNpcToThreeScene(int npcId, string sceneName)
	{
		if (this.threeSenceNPCDictionary.ContainsKey(sceneName))
		{
			this.threeSenceNPCDictionary[sceneName].Add(npcId);
			return;
		}
		this.threeSenceNPCDictionary.Add(sceneName, new List<int>
		{
			npcId
		});
	}

	// Token: 0x060017E3 RID: 6115 RVA: 0x000D0BDC File Offset: 0x000CEDDC
	public void AddNpcToFuBen(int npcId, int sceneId, int fuBenIndex)
	{
		string key = "F" + sceneId;
		if (!this.fuBenNPCDictionary.ContainsKey(key))
		{
			Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
			dictionary.Add(fuBenIndex, new List<int>
			{
				npcId
			});
			this.fuBenNPCDictionary.Add(key, dictionary);
			return;
		}
		if (this.fuBenNPCDictionary[key].ContainsKey(fuBenIndex))
		{
			this.fuBenNPCDictionary[key][fuBenIndex].Add(npcId);
			return;
		}
		this.fuBenNPCDictionary[key].Add(fuBenIndex, new List<int>
		{
			npcId
		});
	}

	// Token: 0x060017E4 RID: 6116 RVA: 0x000D0C7C File Offset: 0x000CEE7C
	public void RemoveNpcByList(int npcId)
	{
		int num = -1;
		string text = "";
		foreach (int num2 in this.bigMapNPCDictionary.Keys)
		{
			if (this.bigMapNPCDictionary[num2].Contains(npcId))
			{
				num = num2;
				break;
			}
		}
		if (num != -1)
		{
			this.bigMapNPCDictionary[num].Remove(npcId);
			return;
		}
		foreach (string text2 in this.threeSenceNPCDictionary.Keys)
		{
			if (this.threeSenceNPCDictionary[text2].Contains(npcId))
			{
				text = text2;
				break;
			}
		}
		if (text != "")
		{
			this.threeSenceNPCDictionary[text].Remove(npcId);
			return;
		}
		foreach (string key in this.fuBenNPCDictionary.Keys)
		{
			foreach (int key2 in this.fuBenNPCDictionary[key].Keys)
			{
				if (this.fuBenNPCDictionary[key][key2].Contains(npcId))
				{
					this.fuBenNPCDictionary[key][key2].Remove(npcId);
					return;
				}
			}
		}
	}

	// Token: 0x060017E5 RID: 6117 RVA: 0x000D0E48 File Offset: 0x000CF048
	public string GetNpcSceneName(int npcId)
	{
		foreach (int key in this.bigMapNPCDictionary.Keys)
		{
			if (this.bigMapNPCDictionary[key].Contains(npcId))
			{
				return jsonData.instance.AllMapLuDainType[key.ToString()]["LuDianName"].Str;
			}
		}
		foreach (string text in this.threeSenceNPCDictionary.Keys)
		{
			if (this.threeSenceNPCDictionary[text].Contains(npcId))
			{
				return jsonData.instance.SceneNameJsonData[text]["MapName"].Str;
			}
		}
		foreach (string text2 in this.fuBenNPCDictionary.Keys)
		{
			foreach (int key2 in this.fuBenNPCDictionary[text2].Keys)
			{
				if (this.fuBenNPCDictionary[text2][key2].Contains(npcId))
				{
					return jsonData.instance.SceneNameJsonData[text2]["MapName"].Str;
				}
			}
		}
		return "null";
	}

	// Token: 0x04001334 RID: 4916
	public Dictionary<int, List<int>> bigMapNPCDictionary;

	// Token: 0x04001335 RID: 4917
	public Dictionary<string, List<int>> threeSenceNPCDictionary;

	// Token: 0x04001336 RID: 4918
	public Dictionary<string, Dictionary<int, List<int>>> fuBenNPCDictionary;

	// Token: 0x04001337 RID: 4919
	private Dictionary<int, List<int>> bigMapNpcTypeIndexDictionary;

	// Token: 0x04001338 RID: 4920
	private List<int> allMapIndexList = new List<int>();
}
