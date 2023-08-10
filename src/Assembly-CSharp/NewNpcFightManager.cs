using System;
using System.Collections.Generic;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;
using YSGame.Fight;

public class NewNpcFightManager : IDisposable
{
	private Random random = new Random();

	private JSONObject npcDate;

	public Dictionary<int, JSONObject> LianQiBuffEquipDictionary = new Dictionary<int, JSONObject>();

	public Dictionary<int, JSONObject> LianQiEquipDictionary = new Dictionary<int, JSONObject>();

	public NewNpcFightManager()
	{
		npcDate = jsonData.instance.AvatarJsonData;
	}

	public void addNpcEquipSeid(int npcID, Avatar avatar)
	{
		JSONObject jSONObject = npcDate[npcID.ToString()]["equipList"];
		if (jSONObject.Count < 0 || jSONObject == null)
		{
			return;
		}
		if (jSONObject.HasField("Weapon1"))
		{
			JSONObject jSONObject2 = jSONObject["Weapon1"];
			if (jSONObject2.HasField("NomalID"))
			{
				GUIPackage.Skill item = new GUIPackage.Skill(jSONObject2["NomalID"].I, 0, 10);
				avatar.skill.Add(item);
			}
			else
			{
				GUIPackage.Skill skill = new GUIPackage.Skill(18011, 0, 10);
				jsonData.instance.skillJsonData[18011.ToString()].SetField("AttackType", jSONObject2["AttackType"]);
				_skillJsonData.DataDict[18011].AttackType = jSONObject2["AttackType"].ToList();
				if (jSONObject2.HasField("SkillSeids"))
				{
					skill.ItemAddSeid = jSONObject2["SkillSeids"];
				}
				if (jSONObject2.HasField("Damage"))
				{
					skill.Damage = jSONObject2["Damage"].I;
				}
				if (jSONObject2.HasField("Name"))
				{
					skill.skill_Name = jSONObject2["Name"].str;
				}
				if (jSONObject2.HasField("SeidDesc"))
				{
					skill.skill_Desc = jSONObject2["SeidDesc"].str;
				}
				if (jSONObject2.HasField("ItemIcon"))
				{
					skill.skill_Icon = ResManager.inst.LoadTexture2D(jSONObject2["ItemIcon"].str);
				}
				avatar.skill.Add(skill);
			}
			object renderObj = avatar.renderObj;
			FightFaBaoShow componentInChildren = ((GameObject)((renderObj is GameObject) ? renderObj : null)).GetComponentInChildren<FightFaBaoShow>();
			if ((Object)(object)componentInChildren != (Object)null)
			{
				componentInChildren.SetNPCWeapon(avatar, jSONObject2);
			}
			else
			{
				Debug.LogError((object)"没有查找到法宝显示组件，需要程序检查");
			}
		}
		if (jSONObject.HasField("Weapon2"))
		{
			JSONObject jSONObject3 = jSONObject["Weapon2"];
			if (jSONObject3.HasField("NomalID"))
			{
				GUIPackage.Skill item2 = new GUIPackage.Skill(jSONObject3["NomalID"].I, 0, 10);
				avatar.skill.Add(item2);
			}
			else
			{
				GUIPackage.Skill skill2 = new GUIPackage.Skill(18012, 0, 10);
				jsonData.instance.skillJsonData[18012.ToString()].SetField("AttackType", jSONObject3["AttackType"]);
				_skillJsonData.DataDict[18012].AttackType = jSONObject3["AttackType"].ToList();
				if (jSONObject3.HasField("SkillSeids"))
				{
					skill2.ItemAddSeid = jSONObject3["SkillSeids"];
				}
				if (jSONObject3.HasField("Damage"))
				{
					skill2.Damage = jSONObject3["Damage"].I;
				}
				if (jSONObject3.HasField("Name"))
				{
					skill2.skill_Name = jSONObject3["Name"].str;
				}
				if (jSONObject3.HasField("SeidDesc"))
				{
					skill2.skill_Desc = jSONObject3["SeidDesc"].str;
				}
				if (jSONObject3.HasField("ItemIcon"))
				{
					skill2.skill_Icon = ResManager.inst.LoadTexture2D(jSONObject3["ItemIcon"].str);
				}
				avatar.skill.Add(skill2);
			}
		}
		if (jSONObject.HasField("Clothing"))
		{
			addFangJuSeid(jSONObject["Clothing"], avatar);
		}
		if (jSONObject.HasField("Ring"))
		{
			addFangJuSeid(jSONObject["Ring"], avatar);
		}
	}

	private void addFangJuSeid(JSONObject Seid, Avatar avatar)
	{
		if (Seid.HasField("NomalID"))
		{
			avatar.spell.addDBuff(Seid["NomalID"].I);
		}
		else
		{
			if (!Seid.HasField("ItemSeids") || Seid["ItemSeids"].list.Count <= 0)
			{
				return;
			}
			int num = Seid["ItemID"].I + 5;
			if (!LianQiBuffEquipDictionary.ContainsKey(num))
			{
				LianQiBuffEquipDictionary.Add(num, Seid["ItemSeids"]);
				LianQiEquipDictionary.Add(num, Seid);
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
				string index = jSONObject.keys[getRandom(0, jSONObject.keys.Count)];
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
			buidTempItem(ref addItemList, list[getRandom(0, list.Count)], 1);
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

	public void Dispose()
	{
	}
}
