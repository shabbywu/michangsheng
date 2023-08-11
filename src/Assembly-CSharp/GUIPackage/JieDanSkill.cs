using System.Collections.Generic;
using KBEngine;

namespace GUIPackage;

public class JieDanSkill : StaticSkill
{
	public JieDanSkill(int id, int level, int max)
	{
		skill_ID = id;
	}

	public override Dictionary<int, Dictionary<int, int>> getSeidFlag(Avatar attaker)
	{
		return attaker.JieDanSkillSeidFlag;
	}

	public override JSONObject getSeidJson(int seid)
	{
		return jsonData.instance.JieDanSeidJsonData[seid][string.Concat(skill_ID)];
	}

	public static void resetJieDanSeid(Avatar attaker)
	{
		attaker.JieDanSkillSeidFlag.Clear();
		foreach (SkillItem hasJieDanSkill in attaker.hasJieDanSkillList)
		{
			new JieDanSkill(hasJieDanSkill.itemId, 0, 5).Puting(attaker, attaker, 2);
		}
	}

	public override JSONObject getJsonData()
	{
		return jsonData.instance.JieDanBiao;
	}
}
