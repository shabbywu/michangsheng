using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F89 RID: 3977
	[CommandInfo("YSTools", "LearningSkill", "给主角学习技能", 0)]
	[AddComponentMenu("")]
	public class LearningSkill : Command
	{
		// Token: 0x06006F54 RID: 28500 RVA: 0x002A6C6B File Offset: 0x002A4E6B
		public override void OnEnter()
		{
			LearningSkill.Study(this.skillID);
			this.Continue();
		}

		// Token: 0x06006F55 RID: 28501 RVA: 0x002A6C7E File Offset: 0x002A4E7E
		public static void Study(int id)
		{
			Tools.instance.getPlayer().addHasSkillList(id);
		}

		// Token: 0x06006F56 RID: 28502 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006F57 RID: 28503 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005C06 RID: 23558
		[Tooltip("给主角学习技能")]
		[SerializeField]
		protected int skillID;
	}
}
