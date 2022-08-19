using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F0C RID: 3852
	[CommandInfo("YSFight", "FightAddSkill", "增加技能", 0)]
	[AddComponentMenu("")]
	public class FightAddSkill : Command
	{
		// Token: 0x06006D57 RID: 27991 RVA: 0x002A3060 File Offset: 0x002A1260
		public override void OnEnter()
		{
			PlayerEx.Player.FightAddSkill(this.skillID, 0, 12);
			this.Continue();
		}

		// Token: 0x06006D58 RID: 27992 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005B23 RID: 23331
		[Tooltip("技能ID")]
		[SerializeField]
		protected int skillID;
	}
}
