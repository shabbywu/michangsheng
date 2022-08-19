using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F7D RID: 3965
	[CommandInfo("YSTools", "检查npc是否死亡", "检查是否能截杀", 0)]
	[AddComponentMenu("")]
	public class CheckNpcDeath : Command
	{
		// Token: 0x06006F2A RID: 28458 RVA: 0x002A6818 File Offset: 0x002A4A18
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

		// Token: 0x06006F2B RID: 28459 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006F2C RID: 28460 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005BF2 RID: 23538
		[Tooltip("npcId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable npcId;

		// Token: 0x04005BF3 RID: 23539
		[Tooltip("是否死亡")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable IsDeath;
	}
}
