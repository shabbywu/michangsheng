using System;
using System.Collections.Generic;

// Token: 0x02000213 RID: 531
public class NPCMap
{
	// Token: 0x0600152D RID: 5421 RVA: 0x00088310 File Offset: 0x00086510
	public NPCMap()
	{
		this.RestartMap();
	}

	// Token: 0x0600152E RID: 5422 RVA: 0x00088329 File Offset: 0x00086529
	public void RestartMap()
	{
		this.bigMapNPCDictionary = new Dictionary<int, List<int>>();
		this.bigMapNpcTypeIndexDictionary = new Dictionary<int, List<int>>();
		this.threeSenceNPCDictionary = new Dictionary<string, List<int>>();
		this.fuBenNPCDictionary = new Dictionary<string, Dictionary<int, List<int>>>();
		this.allMapIndexList = new List<int>();
	}

	// Token: 0x0600152F RID: 5423 RVA: 0x00088364 File Offset: 0x00086564
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

	// Token: 0x06001530 RID: 5424 RVA: 0x000885B8 File Offset: 0x000867B8
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

	// Token: 0x06001531 RID: 5425 RVA: 0x0008860F File Offset: 0x0008680F
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

	// Token: 0x06001532 RID: 5426 RVA: 0x0008864C File Offset: 0x0008684C
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

	// Token: 0x06001533 RID: 5427 RVA: 0x000886EC File Offset: 0x000868EC
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

	// Token: 0x06001534 RID: 5428 RVA: 0x000888B8 File Offset: 0x00086AB8
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

	// Token: 0x04000FE4 RID: 4068
	public Dictionary<int, List<int>> bigMapNPCDictionary;

	// Token: 0x04000FE5 RID: 4069
	public Dictionary<string, List<int>> threeSenceNPCDictionary;

	// Token: 0x04000FE6 RID: 4070
	public Dictionary<string, Dictionary<int, List<int>>> fuBenNPCDictionary;

	// Token: 0x04000FE7 RID: 4071
	private Dictionary<int, List<int>> bigMapNpcTypeIndexDictionary;

	// Token: 0x04000FE8 RID: 4072
	private List<int> allMapIndexList = new List<int>();
}
