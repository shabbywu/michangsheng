using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200143D RID: 5181
	[CommandInfo("YSTools", "LearningSkill", "给主角学习技能", 0)]
	[AddComponentMenu("")]
	public class LearningSkill : Command
	{
		// Token: 0x06007D3E RID: 32062 RVA: 0x00054B1F File Offset: 0x00052D1F
		public override void OnEnter()
		{
			LearningSkill.Study(this.skillID);
			this.Continue();
		}

		// Token: 0x06007D3F RID: 32063 RVA: 0x00054B32 File Offset: 0x00052D32
		public static void Study(int id)
		{
			Tools.instance.getPlayer().addHasSkillList(id);
		}

		// Token: 0x06007D40 RID: 32064 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007D41 RID: 32065 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006AD5 RID: 27349
		[Tooltip("给主角学习技能")]
		[SerializeField]
		protected int skillID;
	}
}
