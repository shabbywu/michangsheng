using System.Collections.Generic;
using KBEngine;

namespace GUIPackage;

public class WuDaoStaticSkill : StaticSkill
{
	public WuDaoStaticSkill(int id, int level, int max)
	{
		skill_ID = id;
	}

	public override Dictionary<int, Dictionary<int, int>> getSeidFlag(Avatar attaker)
	{
		return attaker.wuDaoMag.WuDaoSkillSeidFlag;
	}

	public override JSONObject getSeidJson(int seid)
	{
		return jsonData.instance.WuDaoSeidJsonData[seid][string.Concat(skill_ID)];
	}

	public static void resetWuDaoSeid(Avatar attaker)
	{
		attaker.wuDaoMag.WuDaoSkillSeidFlag.Clear();
		foreach (SkillItem allWuDaoSkill in attaker.wuDaoMag.GetAllWuDaoSkills())
		{
			new WuDaoStaticSkill(allWuDaoSkill.itemId, 0, 5).Puting(attaker, attaker, 2);
		}
	}

	public override JSONObject getJsonData()
	{
		return jsonData.instance.WuDaoJson;
	}

	public void StudtRealizeSeid3(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		attaker.setHP(attaker.HP + (int)getSeidJson(seid)["value1"].n);
	}
}
