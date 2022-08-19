using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F0B RID: 3851
	[CommandInfo("YSFight", "FightAddBuff", "增加Buff", 0)]
	[AddComponentMenu("")]
	public class FightAddBuff : Command
	{
		// Token: 0x06006D53 RID: 27987 RVA: 0x002A3004 File Offset: 0x002A1204
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

		// Token: 0x06006D54 RID: 27988 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006D55 RID: 27989 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B20 RID: 23328
		[Tooltip("增加的角色(0主角,1敌人)")]
		[SerializeField]
		protected int type;

		// Token: 0x04005B21 RID: 23329
		[Tooltip("增加的BuffID")]
		[SerializeField]
		protected int BuffID;

		// Token: 0x04005B22 RID: 23330
		[Tooltip("增加的Buff层数")]
		[SerializeField]
		protected int BuffSum;
	}
}
