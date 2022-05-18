using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001418 RID: 5144
	[CommandInfo("YSNew/Set", "SetPlayerDie", "让玩家直接死亡", 0)]
	[AddComponentMenu("")]
	public class SetPlayerDie : Command
	{
		// Token: 0x06007CB2 RID: 31922 RVA: 0x000547F5 File Offset: 0x000529F5
		public override void OnEnter()
		{
			UIDeath.Inst.Show(DeathType.身死道消);
			this.Continue();
		}

		// Token: 0x06007CB3 RID: 31923 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006A9D RID: 27293
		[Tooltip("描述")]
		[SerializeField]
		protected string Desc = "让玩家直接死亡";
	}
}
