using System;
using UnityEngine;
using YSGame.Fight;

namespace Fungus
{
	// Token: 0x02000F0D RID: 3853
	[CommandInfo("YSFight", "FightClearSkill", "清空所有当前技能", 0)]
	[AddComponentMenu("")]
	public class FightClearSkill : Command
	{
		// Token: 0x06006D5A RID: 27994 RVA: 0x002A307C File Offset: 0x002A127C
		public override void OnEnter()
		{
			Tools.instance.getPlayer().skill.Clear();
			foreach (UIFightSkillItem uifightSkillItem in UIFightPanel.Inst.FightSkills)
			{
				uifightSkillItem.Clear();
			}
			this.Continue();
		}

		// Token: 0x06006D5B RID: 27995 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005B24 RID: 23332
		[Tooltip("描述")]
		[SerializeField]
		protected string desc = "清空所有当前技能";
	}
}
