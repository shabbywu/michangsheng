using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200143E RID: 5182
	[CommandInfo("YSTools", "LearningPassivitySkill", "给主角学习功法技能", 0)]
	[AddComponentMenu("")]
	public class LearningStaticSkill : Command
	{
		// Token: 0x06007D43 RID: 32067 RVA: 0x00054B44 File Offset: 0x00052D44
		public override void OnEnter()
		{
			LearningStaticSkill.Study(this.skillID);
			this.Continue();
		}

		// Token: 0x06007D44 RID: 32068 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007D45 RID: 32069 RVA: 0x00054B57 File Offset: 0x00052D57
		public static void Study(int skillID)
		{
			Tools.instance.getPlayer().addHasStaticSkillList(skillID, 1);
		}

		// Token: 0x04006AD6 RID: 27350
		[Tooltip("给主角学习功法")]
		[SerializeField]
		protected int skillID;
	}
}
