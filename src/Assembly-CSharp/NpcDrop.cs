using System;
using System.Collections.Generic;

// Token: 0x02000327 RID: 807
public class NpcDrop
{
	// Token: 0x060017C9 RID: 6089 RVA: 0x00014FD3 File Offset: 0x000131D3
	public NpcDrop()
	{
		this.npcDate = jsonData.instance.AvatarJsonData;
	}

	// Token: 0x060017CA RID: 6090 RVA: 0x000CF500 File Offset: 0x000CD700
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

	// Token: 0x060017CB RID: 6091 RVA: 0x000CF644 File Offset: 0x000CD844
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

	// Token: 0x060017CC RID: 6092 RVA: 0x00014FF6 File Offset: 0x000131F6
	private int getRandom(int min, int max)
	{
		return this.random.Next(min, max + 1);
	}

	// Token: 0x060017CD RID: 6093 RVA: 0x000CF810 File Offset: 0x000CDA10
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

	// Token: 0x0400132D RID: 4909
	private JSONObject npcDate;

	// Token: 0x0400132E RID: 4910
	private Random random = new Random();
}
