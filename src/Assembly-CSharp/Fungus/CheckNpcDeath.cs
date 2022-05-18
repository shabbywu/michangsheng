using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001434 RID: 5172
	[CommandInfo("YSTools", "检查npc是否死亡", "检查是否能截杀", 0)]
	[AddComponentMenu("")]
	public class CheckNpcDeath : Command
	{
		// Token: 0x06007D1A RID: 32026 RVA: 0x00054A53 File Offset: 0x00052C53
		public override void OnEnter()
		{
			if (NpcJieSuanManager.inst.IsDeath(this.npcId.Value))
			{
				this.IsDeath.Value = true;
			}
			else
			{
				this.IsDeath.Value = false;
			}
			this.Continue();
		}

		// Token: 0x06007D1B RID: 32027 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007D1C RID: 32028 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006AC6 RID: 27334
		[Tooltip("npcId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable npcId;

		// Token: 0x04006AC7 RID: 27335
		[Tooltip("是否死亡")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable IsDeath;
	}
}
