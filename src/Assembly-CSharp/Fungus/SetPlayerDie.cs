using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F61 RID: 3937
	[CommandInfo("YSNew/Set", "SetPlayerDie", "让玩家直接死亡", 0)]
	[AddComponentMenu("")]
	public class SetPlayerDie : Command
	{
		// Token: 0x06006EC2 RID: 28354 RVA: 0x002A5729 File Offset: 0x002A3929
		public override void OnEnter()
		{
			UIDeath.Inst.Show(DeathType.身死道消);
			this.Continue();
		}

		// Token: 0x06006EC3 RID: 28355 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005BC8 RID: 23496
		[Tooltip("描述")]
		[SerializeField]
		protected string Desc = "让玩家直接死亡";
	}
}
