using GUIPackage;
using UnityEngine;

public class StaticTuPoCell : SkillStaticCell
{
	protected override void OnPress()
	{
		if (skillID != -1)
		{
			KeyCell componentInChildren = ((Component)((Component)this).transform.parent.parent.parent.parent).GetComponentInChildren<KeyCell>();
			componentInChildren.keySkill = skill_UIST.skill[skillID];
			((Component)((Component)componentInChildren).transform).GetComponent<TuPoGongFa>().NowIndex = skillID;
		}
	}

	public override void SetShow_Tooltip()
	{
		skill_UIST.Show_Tooltip(skill_UIST.skill[skillID], 1);
	}
}
