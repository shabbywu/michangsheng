using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013BF RID: 5055
	[CommandInfo("YSFight", "FightAddSkill", "增加技能", 0)]
	[AddComponentMenu("")]
	public class FightAddSkill : Command
	{
		// Token: 0x06007B40 RID: 31552 RVA: 0x000541BF File Offset: 0x000523BF
		public override void OnEnter()
		{
			PlayerEx.Player.FightAddSkill(this.skillID, 0, 12);
			this.Continue();
		}

		// Token: 0x06007B41 RID: 31553 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x040069EA RID: 27114
		[Tooltip("技能ID")]
		[SerializeField]
		protected int skillID;
	}
}
