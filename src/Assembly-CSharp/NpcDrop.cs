using System;
using System.Collections.Generic;

// Token: 0x0200020F RID: 527
public class NpcDrop
{
	// Token: 0x06001518 RID: 5400 RVA: 0x00086E28 File Offset: 0x00085028
	public NpcDrop()
	{
		this.npcDate = jsonData.instance.AvatarJsonData;
	}

	// Token: 0x06001519 RID: 5401 RVA: 0x00086E4C File Offset: 0x0008504C
	public JSONObject dropReward(float equipLv, float packLv, int NPCID)
	{
		JSONObject result = new JSONObject(JSONObject.Type.ARRAY);
		if (equipLv > 0f)
		{
			this.dropEquip(ref result, NPCID);
		}
		if (packLv > 0f)
		{
			foreach (JSONObject jsonobject in jsonData.instance.AvatarBackpackJsonData[string.Concat(NPCID)]["Backpack"].list)
			{
				if ((int)jsonobject["CanDrop"].n != 0)
				{
					if (jsonobject["Num"].I < 5)
					{
						float num = (float)this.getRandom(0, 100) / 100f;
						if (packLv >= num)
						{
							this.buidTempItem(ref result, jsonobject["ItemID"].I, jsonobject["Num"].I, jsonobject["Seid"]);
						}
					}
					else
					{
						this.buidTempItem(ref result, jsonobject["ItemID"].I, (int)((float)jsonobject["Num"].I * packLv), jsonobject["Seid"]);
					}
				}
			}
		}
		return result;
	}

	// Token: 0x0600151A RID: 5402 RVA: 0x00086F90 File Offset: 0x00085190
	private void dropEquip(ref JSONObject addItemList, int NPCID)
	{
		if (NPCID >= 20000)
		{
			JSONObject jsonobject = this.npcDate[NPCID.ToString()]["equipList"];
			if (jsonobject.keys.Count == 0)
			{
				return;
			}
			string index = jsonobject.keys[this.getRandom(0, jsonobject.keys.Count - 1)];
			if (jsonobject[index].HasField("NomalID"))
			{
				this.buidTempItem(ref addItemList, jsonobject[index]["NomalID"].I, 1, null);
				return;
			}
			this.buidTempItem(ref addItemList, jsonobject[index]["ItemID"].I, 1, jsonobject[index]);
			return;
		}
		else
		{
			List<int> list = new List<int>();
			if (this.npcDate[NPCID.ToString()]["equipWeapon"].I > 0)
			{
				list.Add(this.npcDate[NPCID.ToString()]["equipWeapon"].I);
			}
			if (this.npcDate[NPCID.ToString()]["equipRing"].I > 0)
			{
				list.Add(this.npcDate[NPCID.ToString()]["equipRing"].I);
			}
			if (this.npcDate[NPCID.ToString()]["equipClothing"].I > 0)
			{
				list.Add(this.npcDate[NPCID.ToString()]["equipClothing"].I);
			}
			if (list.Count == 0)
			{
				return;
			}
			this.buidTempItem(ref addItemList, list[this.getRandom(0, list.Count - 1)], 1, null);
			return;
		}
	}

	// Token: 0x0600151B RID: 5403 RVA: 0x0008715A File Offset: 0x0008535A
	private int getRandom(int min, int max)
	{
		return this.random.Next(min, max + 1);
	}

	// Token: 0x0600151C RID: 5404 RVA: 0x0008716C File Offset: 0x0008536C
	private void buidTempItem(ref JSONObject addItemList, int ItemID, int ItemNum, JSONObject seid = null)
	{
		JSONObject jsonobject = new JSONObject();
		jsonobject.AddField("UUID", Tools.getUUID());
		jsonobject.AddField("ID", ItemID);
		jsonobject.AddField("Num", ItemNum);
		if (seid != null)
		{
			jsonobject.AddField("seid", seid);
		}
		else
		{
			jsonobject.AddField("seid", Tools.CreateItemSeid(ItemID));
		}
		addItemList.Add(jsonobject);
	}

	// Token: 0x04000FDD RID: 4061
	private JSONObject npcDate;

	// Token: 0x04000FDE RID: 4062
	private Random random = new Random();
}
