using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F8A RID: 3978
	[CommandInfo("YSTools", "LearningPassivitySkill", "给主角学习功法技能", 0)]
	[AddComponentMenu("")]
	public class LearningStaticSkill : Command
	{
		// Token: 0x06006F59 RID: 28505 RVA: 0x002A6C90 File Offset: 0x002A4E90
		public override void OnEnter()
		{
			LearningStaticSkill.Study(this.skillID);
			this.Continue();
		}

		// Token: 0x06006F5A RID: 28506 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006F5B RID: 28507 RVA: 0x002A6CA3 File Offset: 0x002A4EA3
		public static void Study(int skillID)
		{
			Tools.instance.getPlayer().addHasStaticSkillList(skillID, 1);
		}

		// Token: 0x04005C07 RID: 23559
		[Tooltip("给主角学习功法")]
		[SerializeField]
		protected int skillID;
	}
}
