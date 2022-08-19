using System;
using System.Collections.Generic;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;
using YSGame.Fight;

// Token: 0x020002B6 RID: 694
public class NewNpcFightManager : IDisposable
{
	// Token: 0x0600187E RID: 6270 RVA: 0x000AF764 File Offset: 0x000AD964
	public NewNpcFightManager()
	{
		this.npcDate = jsonData.instance.AvatarJsonData;
	}

	// Token: 0x0600187F RID: 6271 RVA: 0x000AF7A0 File Offset: 0x000AD9A0
	public void addNpcEquipSeid(int npcID, Avatar avatar)
	{
		JSONObject jsonobject = this.npcDate[npcID.ToString()]["equipList"];
		if (jsonobject.Count < 0 || jsonobject == null)
		{
			return;
		}
		if (jsonobject.HasField("Weapon1"))
		{
			JSONObject jsonobject2 = jsonobject["Weapon1"];
			if (jsonobject2.HasField("NomalID"))
			{
				GUIPackage.Skill item = new GUIPackage.Skill(jsonobject2["NomalID"].I, 0, 10);
				avatar.skill.Add(item);
			}
			else
			{
				GUIPackage.Skill skill = new GUIPackage.Skill(18011, 0, 10);
				jsonData.instance.skillJsonData[18011.ToString()].SetField("AttackType", jsonobject2["AttackType"]);
				_skillJsonData.DataDict[18011].AttackType = jsonobject2["AttackType"].ToList();
				if (jsonobject2.HasField("SkillSeids"))
				{
					skill.ItemAddSeid = jsonobject2["SkillSeids"];
				}
				if (jsonobject2.HasField("Damage"))
				{
					skill.Damage = jsonobject2["Damage"].I;
				}
				if (jsonobject2.HasField("Name"))
				{
					skill.skill_Name = jsonobject2["Name"].str;
				}
				if (jsonobject2.HasField("SeidDesc"))
				{
					skill.skill_Desc = jsonobject2["SeidDesc"].str;
				}
				if (jsonobject2.HasField("ItemIcon"))
				{
					skill.skill_Icon = ResManager.inst.LoadTexture2D(jsonobject2["ItemIcon"].str);
				}
				avatar.skill.Add(skill);
			}
			FightFaBaoShow componentInChildren = (avatar.renderObj as GameObject).GetComponentInChildren<FightFaBaoShow>();
			if (componentInChildren != null)
			{
				componentInChildren.SetNPCWeapon(avatar, jsonobject2);
			}
			else
			{
				Debug.LogError("没有查找到法宝显示组件，需要程序检查");
			}
		}
		if (jsonobject.HasField("Weapon2"))
		{
			JSONObject jsonobject3 = jsonobject["Weapon2"];
			if (jsonobject3.HasField("NomalID"))
			{
				GUIPackage.Skill item2 = new GUIPackage.Skill(jsonobject3["NomalID"].I, 0, 10);
				avatar.skill.Add(item2);
			}
			else
			{
				GUIPackage.Skill skill2 = new GUIPackage.Skill(18012, 0, 10);
				jsonData.instance.skillJsonData[18012.ToString()].SetField("AttackType", jsonobject3["AttackType"]);
				_skillJsonData.DataDict[18012].AttackType = jsonobject3["AttackType"].ToList();
				if (jsonobject3.HasField("SkillSeids"))
				{
					skill2.ItemAddSeid = jsonobject3["SkillSeids"];
				}
				if (jsonobject3.HasField("Damage"))
				{
					skill2.Damage = jsonobject3["Damage"].I;
				}
				if (jsonobject3.HasField("Name"))
				{
					skill2.skill_Name = jsonobject3["Name"].str;
				}
				if (jsonobject3.HasField("SeidDesc"))
				{
					skill2.skill_Desc = jsonobject3["SeidDesc"].str;
				}
				if (jsonobject3.HasField("ItemIcon"))
				{
					skill2.skill_Icon = ResManager.inst.LoadTexture2D(jsonobject3["ItemIcon"].str);
				}
				avatar.skill.Add(skill2);
			}
		}
		if (jsonobject.HasField("Clothing"))
		{
			this.addFangJuSeid(jsonobject["Clothing"], avatar);
		}
		if (jsonobject.HasField("Ring"))
		{
			this.addFangJuSeid(jsonobject["Ring"], avatar);
		}
	}

	// Token: 0x06001880 RID: 6272 RVA: 0x000AFB50 File Offset: 0x000ADD50
	private void addFangJuSeid(JSONObject Seid, Avatar avatar)
	{
		if (Seid.HasField("NomalID"))
		{
			avatar.spell.addDBuff(Seid["NomalID"].I);
			return;
		}
		if (Seid.HasField("ItemSeids") && Seid["ItemSeids"].list.Count > 0)
		{
			int num = Seid["ItemID"].I + 5;
			if (!this.LianQiBuffEquipDictionary.ContainsKey(num))
			{
				this.LianQiBuffEquipDictionary.Add(num, Seid["ItemSeids"]);
				this.LianQiEquipDictionary.Add(num, Seid);
			}
			List<JSONObject> list = Seid["ItemSeids"].list;
			bool flag = true;
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i]["id"].I == 62 && (float)avatar.HP / (float)avatar.HP_Max * 100f > list[i]["value1"][0].n)
				{
					flag = false;
				}
			}
			if (flag)
			{
				for (int j = 0; j < list.Count; j++)
				{
					if (list[j]["id"].I == 64)
					{
						for (int k = 0; k < list[j]["value1"].Count; k++)
						{
							avatar.spell.addDBuff(list[j]["value1"][k].I, list[j]["value2"][k].I);
						}
					}
				}
			}
			avatar.spell.addDBuff(num);
		}
	}

	// Token: 0x06001881 RID: 6273 RVA: 0x000AFD20 File Offset: 0x000ADF20
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

	// Token: 0x06001882 RID: 6274 RVA: 0x000AFE64 File Offset: 0x000AE064
	private void dropEquip(ref JSONObject addItemList, int NPCID)
	{
		if (NPCID >= 20000)
		{
			JSONObject jsonobject = this.npcDate[NPCID.ToString()]["equipList"];
			if (jsonobject.keys.Count == 0)
			{
				return;
			}
			string index = jsonobject.keys[this.getRandom(0, jsonobject.keys.Count)];
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
			this.buidTempItem(ref addItemList, list[this.getRandom(0, list.Count)], 1, null);
			return;
		}
	}

	// Token: 0x06001883 RID: 6275 RVA: 0x000B002A File Offset: 0x000AE22A
	private int getRandom(int min, int max)
	{
		return this.random.Next(min, max + 1);
	}

	// Token: 0x06001884 RID: 6276 RVA: 0x000B003C File Offset: 0x000AE23C
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

	// Token: 0x06001885 RID: 6277 RVA: 0x00004095 File Offset: 0x00002295
	public void Dispose()
	{
	}

	// Token: 0x04001376 RID: 4982
	private Random random = new Random();

	// Token: 0x04001377 RID: 4983
	private JSONObject npcDate;

	// Token: 0x04001378 RID: 4984
	public Dictionary<int, JSONObject> LianQiBuffEquipDictionary = new Dictionary<int, JSONObject>();

	// Token: 0x04001379 RID: 4985
	public Dictionary<int, JSONObject> LianQiEquipDictionary = new Dictionary<int, JSONObject>();
}
