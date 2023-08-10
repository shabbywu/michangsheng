using System.Collections.Generic;
using System.Reflection;
using KBEngine;

namespace GUIPackage;

public class Equips : StaticSkill
{
	public enum EquipSeidAll
	{
		EquiSEID4 = 4,
		EquiSEID3 = 5,
		EquiSEID5 = 5,
		EquiSEID6 = 6,
		EquiSEID7 = 7
	}

	public JSONObject ItemAddSeid;

	public Equips(int id, int level, int max)
	{
		skill_ID = id;
		skill_level = level;
	}

	public override Dictionary<int, Dictionary<int, int>> getSeidFlag(Avatar attaker)
	{
		return attaker.EquipSeidFlag;
	}

	public override void realizeSeid(int seid, List<int> flag, Entity _attaker, Entity _receiver, int type)
	{
		Avatar avatar = (Avatar)_attaker;
		Avatar avatar2 = (Avatar)_receiver;
		for (int i = 0; i < 500; i++)
		{
			if (i == seid)
			{
				MethodInfo method = GetType().GetMethod(getMethodName() + seid);
				if (method != null)
				{
					method.Invoke(this, new object[5] { seid, flag, avatar, avatar2, type });
				}
				break;
			}
		}
	}

	public override JSONObject getJsonData()
	{
		return jsonData.instance._ItemJsonData;
	}

	public static void resetEquipSeid(Avatar attaker)
	{
		attaker.EquipSeidFlag.Clear();
		for (int num = attaker.equipItemList.values.Count - 1; num >= 0; num--)
		{
			if (attaker.equipItemList.values[num] == null)
			{
				attaker.equipItemList.values.RemoveAt(num);
			}
		}
		foreach (ITEM_INFO value in attaker.equipItemList.values)
		{
			if (value.Seid != null && value.Seid.HasField("ItemSeids"))
			{
				foreach (JSONObject item in value.Seid["ItemSeids"].list)
				{
					Equips equips = new Equips(value.itemId, 0, 5);
					equips.ItemAddSeid = item;
					equips.Puting(attaker, attaker, 2);
				}
			}
			else
			{
				new Equips(value.itemId, 0, 5).Puting(attaker, attaker, 2);
			}
		}
	}

	public override string getMethodName()
	{
		return "realizeEquipSeid";
	}

	public override JSONObject getSeidJson(int seid)
	{
		if (ItemAddSeid != null)
		{
			foreach (JSONObject item in ItemAddSeid.list)
			{
				if (item["id"].I == seid)
				{
					return item;
				}
			}
		}
		return jsonData.instance.EquipSeidJsonData[seid][string.Concat(skill_ID)];
	}

	public void realizeEquipSeid3(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		realizeSeid3(seid, damage, attaker, receiver, type);
	}

	public void realizeEquipSeid4(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		resetSeidFlag(seid, attaker);
	}

	public void realizeEquipSeid8(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		realizeSeid8(seid, damage, attaker, receiver, type);
	}
}
