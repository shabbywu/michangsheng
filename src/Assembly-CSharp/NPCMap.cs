using System.Collections.Generic;

public class NPCMap
{
	public Dictionary<int, List<int>> bigMapNPCDictionary;

	public Dictionary<string, List<int>> threeSenceNPCDictionary;

	public Dictionary<string, Dictionary<int, List<int>>> fuBenNPCDictionary;

	private Dictionary<int, List<int>> bigMapNpcTypeIndexDictionary;

	private List<int> allMapIndexList = new List<int>();

	public NPCMap()
	{
		RestartMap();
	}

	public void RestartMap()
	{
		bigMapNPCDictionary = new Dictionary<int, List<int>>();
		bigMapNpcTypeIndexDictionary = new Dictionary<int, List<int>>();
		threeSenceNPCDictionary = new Dictionary<string, List<int>>();
		fuBenNPCDictionary = new Dictionary<string, Dictionary<int, List<int>>>();
		allMapIndexList = new List<int>();
	}

	public void AddNpcToBigMap(int npcId, int type, bool isCanJieSha = true)
	{
		List<int> list = new List<int>();
		if (type == 1)
		{
			if (allMapIndexList.Count < 1)
			{
				foreach (JSONObject item in jsonData.instance.NpcBigMapBingDate.list)
				{
					if (item["MapType"].I == 1)
					{
						allMapIndexList.Add(item["MapD"].I);
					}
				}
			}
			list = allMapIndexList;
		}
		else
		{
			int i = jsonData.instance.AvatarJsonData[npcId.ToString()]["Type"].I;
			if (!bigMapNpcTypeIndexDictionary.ContainsKey(i))
			{
				foreach (JSONObject item2 in jsonData.instance.NpcBigMapBingDate.list)
				{
					if (item2["MapType"].I == 2)
					{
						if (bigMapNpcTypeIndexDictionary.ContainsKey(item2["NPCType"].I))
						{
							bigMapNpcTypeIndexDictionary[item2["NPCType"].I].Add(item2["MapD"].I);
							continue;
						}
						bigMapNpcTypeIndexDictionary.Add(item2["NPCType"].I, new List<int> { item2["MapD"].I });
					}
				}
			}
			list = bigMapNpcTypeIndexDictionary[i];
		}
		int key = list[NpcJieSuanManager.inst.getRandomInt(0, list.Count - 1)];
		if (bigMapNPCDictionary.ContainsKey(key))
		{
			bigMapNPCDictionary[key].Add(npcId);
		}
		else
		{
			bigMapNPCDictionary.Add(key, new List<int> { npcId });
		}
		if (isCanJieSha && NPCEx.GetFavor(npcId) < 200)
		{
			NpcJieSuanManager.inst.allBigMapNpcList.Add(npcId);
		}
	}

	public void AddNpcToThreeScene(int npcId, int sceneId)
	{
		string key = "S" + sceneId;
		if (threeSenceNPCDictionary.ContainsKey(key))
		{
			threeSenceNPCDictionary[key].Add(npcId);
			return;
		}
		threeSenceNPCDictionary.Add(key, new List<int> { npcId });
	}

	public void AddNpcToThreeScene(int npcId, string sceneName)
	{
		if (threeSenceNPCDictionary.ContainsKey(sceneName))
		{
			threeSenceNPCDictionary[sceneName].Add(npcId);
			return;
		}
		threeSenceNPCDictionary.Add(sceneName, new List<int> { npcId });
	}

	public void AddNpcToFuBen(int npcId, int sceneId, int fuBenIndex)
	{
		string key = "F" + sceneId;
		if (fuBenNPCDictionary.ContainsKey(key))
		{
			if (fuBenNPCDictionary[key].ContainsKey(fuBenIndex))
			{
				fuBenNPCDictionary[key][fuBenIndex].Add(npcId);
				return;
			}
			fuBenNPCDictionary[key].Add(fuBenIndex, new List<int> { npcId });
		}
		else
		{
			Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
			dictionary.Add(fuBenIndex, new List<int> { npcId });
			fuBenNPCDictionary.Add(key, dictionary);
		}
	}

	public void RemoveNpcByList(int npcId)
	{
		int num = -1;
		string text = "";
		foreach (int key in bigMapNPCDictionary.Keys)
		{
			if (bigMapNPCDictionary[key].Contains(npcId))
			{
				num = key;
				break;
			}
		}
		if (num != -1)
		{
			bigMapNPCDictionary[num].Remove(npcId);
			return;
		}
		foreach (string key2 in threeSenceNPCDictionary.Keys)
		{
			if (threeSenceNPCDictionary[key2].Contains(npcId))
			{
				text = key2;
				break;
			}
		}
		if (text != "")
		{
			threeSenceNPCDictionary[text].Remove(npcId);
			return;
		}
		foreach (string key3 in fuBenNPCDictionary.Keys)
		{
			foreach (int key4 in fuBenNPCDictionary[key3].Keys)
			{
				if (fuBenNPCDictionary[key3][key4].Contains(npcId))
				{
					fuBenNPCDictionary[key3][key4].Remove(npcId);
					return;
				}
			}
		}
	}

	public string GetNpcSceneName(int npcId)
	{
		foreach (int key in bigMapNPCDictionary.Keys)
		{
			if (bigMapNPCDictionary[key].Contains(npcId))
			{
				return jsonData.instance.AllMapLuDainType[key.ToString()]["LuDianName"].Str;
			}
		}
		foreach (string key2 in threeSenceNPCDictionary.Keys)
		{
			if (threeSenceNPCDictionary[key2].Contains(npcId))
			{
				return jsonData.instance.SceneNameJsonData[key2]["MapName"].Str;
			}
		}
		foreach (string key3 in fuBenNPCDictionary.Keys)
		{
			foreach (int key4 in fuBenNPCDictionary[key3].Keys)
			{
				if (fuBenNPCDictionary[key3][key4].Contains(npcId))
				{
					return jsonData.instance.SceneNameJsonData[key3]["MapName"].Str;
				}
			}
		}
		return "null";
	}
}
