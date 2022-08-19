using System;
using GUIPackage;

// Token: 0x0200043B RID: 1083
public class StaticTuPoCell : SkillStaticCell
{
	// Token: 0x06002275 RID: 8821 RVA: 0x000ED0D4 File Offset: 0x000EB2D4
	protected override void OnPress()
	{
		if (this.skillID == -1)
		{
			return;
		}
		KeyCell componentInChildren = base.transform.parent.parent.parent.parent.GetComponentInChildren<KeyCell>();
		componentInChildren.keySkill = this.skill_UIST.skill[this.skillID];
		componentInChildren.transform.GetComponent<TuPoGongFa>().NowIndex = this.skillID;
	}

	// Token: 0x06002276 RID: 8822 RVA: 0x000ED13B File Offset: 0x000EB33B
	public override void SetShow_Tooltip()
	{
		this.skill_UIST.Show_Tooltip(this.skill_UIST.skill[this.skillID], 1);
	}
}
