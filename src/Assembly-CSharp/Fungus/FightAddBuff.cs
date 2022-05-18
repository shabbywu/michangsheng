using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013BE RID: 5054
	[CommandInfo("YSFight", "FightAddBuff", "增加Buff", 0)]
	[AddComponentMenu("")]
	public class FightAddBuff : Command
	{
		// Token: 0x06007B3C RID: 31548 RVA: 0x002C36B4 File Offset: 0x002C18B4
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			if (this.type == 1)
			{
				player.OtherAvatar.spell.addDBuff(this.BuffID, this.BuffSum);
			}
			else
			{
				player.spell.addDBuff(this.BuffID, this.BuffSum);
			}
			this.Continue();
		}

		// Token: 0x06007B3D RID: 31549 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007B3E RID: 31550 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x040069E7 RID: 27111
		[Tooltip("增加的角色(0主角,1敌人)")]
		[SerializeField]
		protected int type;

		// Token: 0x040069E8 RID: 27112
		[Tooltip("增加的BuffID")]
		[SerializeField]
		protected int BuffID;

		// Token: 0x040069E9 RID: 27113
		[Tooltip("增加的Buff层数")]
		[SerializeField]
		protected int BuffSum;
	}
}
