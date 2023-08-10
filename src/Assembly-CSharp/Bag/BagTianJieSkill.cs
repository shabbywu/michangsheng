using JSONClass;
using KBEngine;
using UnityEngine;

namespace Bag;

public class BagTianJieSkill : ISkill
{
	public ActiveSkill BindSkill;

	public int Quality;

	public TianJieMiShuData MiShu;

	public bool IsLingWu;

	public bool IsGanYing;

	public bool IsCanLingWu;

	public BagTianJieSkill()
	{
	}

	public BagTianJieSkill(TianJieMiShuData miShu)
	{
		MiShu = miShu;
		BindSkill = new ActiveSkill();
		BindSkill.SetSkill(MiShu.Skill_ID, PlayerEx.Player.getLevelType());
		Avatar player = PlayerEx.Player;
		if (MiShu.Type == 0)
		{
			if (player.TianJieCanLingWuSkills.StringListContains(MiShu.id))
			{
				IsGanYing = true;
			}
			if (player.TianJieYiLingWuSkills.StringListContains(MiShu.id))
			{
				IsLingWu = true;
			}
			if (IsGanYing && !IsLingWu)
			{
				IsCanLingWu = true;
			}
			return;
		}
		int id = -1;
		if (float.TryParse(MiShu.PanDing, out var result))
		{
			id = (int)result;
		}
		else
		{
			Debug.LogError((object)("天劫秘术解析判定时出错，无法解析为数字。秘术ID:" + miShu.id + "，需要解析的文本:" + miShu.PanDing));
		}
		if (player.TianJieYiLingWuSkills.StringListContains(MiShu.id))
		{
			IsLingWu = true;
		}
		if (IsLingWu)
		{
			return;
		}
		if (MiShu.Type == 1)
		{
			if (player.checkHasStudyWuDaoSkillByID(id))
			{
				IsCanLingWu = true;
			}
		}
		else if (MiShu.Type == 2 && GlobalValue.Get(id) == 1)
		{
			IsCanLingWu = true;
		}
	}

	public Sprite GetIconSprite()
	{
		return BindSkill.GetIconSprite();
	}

	public Sprite GetQualitySprite()
	{
		return BindSkill.GetQualitySprite();
	}

	public Sprite GetQualityUpSprite()
	{
		return BindSkill.GetQualityUpSprite();
	}
}
