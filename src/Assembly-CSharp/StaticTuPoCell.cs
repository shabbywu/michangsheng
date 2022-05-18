using System;
using GUIPackage;

// Token: 0x020005F2 RID: 1522
public class StaticTuPoCell : SkillStaticCell
{
	// Token: 0x06002634 RID: 9780 RVA: 0x0012E038 File Offset: 0x0012C238
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

	// Token: 0x06002635 RID: 9781 RVA: 0x0001E7AF File Offset: 0x0001C9AF
	public override void SetShow_Tooltip()
	{
		this.skill_UIST.Show_Tooltip(this.skill_UIST.skill[this.skillID], 1);
	}
}
