using System;
using System.Collections.Generic;

public class NpcDrop
{
	private JSONObject npcDate;

	private Random random = new Random();

	public NpcDrop()
	{
		npcDate = jsonData.instance.AvatarJsonData;
	}

	public JSONObject dropReward(float equipLv, float packLv, int NPCID)
	{
		JSONObject addItemList = new JSONObject(JSONObject.Type.ARRAY);
		if (equipLv > 0f)
		{
			dropEquip(ref addItemList, NPCID);
		}
		if (packLv > 0f)
		{
			foreach (JSONObject item in jsonData.instance.AvatarBackpackJsonData[string.Concat(NPCID)]["Backpack"].list)
			{
				if ((int)item["CanDrop"].n == 0)
				{
					continue;
				}
				if (item["Num"].I < 5)
				{
					float num = (float)getRandom(0, 100) / 100f;
					if (packLv >= num)
					{
						buidTempItem(ref addItemList, item["ItemID"].I, item["Num"].I, item["Seid"]);
					}
				}
				else
				{
					buidTempItem(ref addItemList, item["ItemID"].I, (int)((float)item["Num"].I * packLv), item["Seid"]);
				}
			}
		}
		return addItemList;
	}

	private void dropEquip(ref JSONObject addItemList, int NPCID)
	{
		if (NPCID >= 20000)
		{
			JSONObject jSONObject = npcDate[NPCID.ToString()]["equipList"];
			if (jSONObject.keys.Count != 0)
			{
				string index = jSONObject.keys[getRandom(0, jSONObject.keys.Count - 1)];
				if (jSONObject[index].HasField("NomalID"))
				{
					buidTempItem(ref addItemList, jSONObject[index]["NomalID"].I, 1);
				}
				else
				{
					buidTempItem(ref addItemList, jSONObject[index]["ItemID"].I, 1, jSONObject[index]);
				}
			}
			return;
		}
		List<int> list = new List<int>();
		if (npcDate[NPCID.ToString()]["equipWeapon"].I > 0)
		{
			list.Add(npcDate[NPCID.ToString()]["equipWeapon"].I);
		}
		if (npcDate[NPCID.ToString()]["equipRing"].I > 0)
		{
			list.Add(npcDate[NPCID.ToString()]["equipRing"].I);
		}
		if (npcDate[NPCID.ToString()]["equipClothing"].I > 0)
		{
			list.Add(npcDate[NPCID.ToString()]["equipClothing"].I);
		}
		if (list.Count != 0)
		{
			buidTempItem(ref addItemList, list[getRandom(0, list.Count - 1)], 1);
		}
	}

	private int getRandom(int min, int max)
	{
		return random.Next(min, max + 1);
	}

	private void buidTempItem(ref JSONObject addItemList, int ItemID, int ItemNum, JSONObject seid = null)
	{
		JSONObject jSONObject = new JSONObject();
		jSONObject.AddField("UUID", Tools.getUUID());
		jSONObject.AddField("ID", ItemID);
		jSONObject.AddField("Num", ItemNum);
		if (seid != null)
		{
			jSONObject.AddField("seid", seid);
		}
		else
		{
			jSONObject.AddField("seid", Tools.CreateItemSeid(ItemID));
		}
		addItemList.Add(jSONObject);
	}
}
