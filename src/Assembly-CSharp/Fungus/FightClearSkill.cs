using System;
using UnityEngine;
using YSGame.Fight;

namespace Fungus
{
	// Token: 0x020013C0 RID: 5056
	[CommandInfo("YSFight", "FightClearSkill", "清空所有当前技能", 0)]
	[AddComponentMenu("")]
	public class FightClearSkill : Command
	{
		// Token: 0x06007B43 RID: 31555 RVA: 0x002C3710 File Offset: 0x002C1910
		public override void OnEnter()
		{
			Tools.instance.getPlayer().skill.Clear();
			foreach (UIFightSkillItem uifightSkillItem in UIFightPanel.Inst.FightSkills)
			{
				uifightSkillItem.Clear();
			}
			this.Continue();
		}

		// Token: 0x06007B44 RID: 31556 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x040069EB RID: 27115
		[Tooltip("描述")]
		[SerializeField]
		protected string desc = "清空所有当前技能";
	}
}
