using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013DF RID: 5087
	[CommandInfo("YSNew/Add", "AddHaoGanDu", "增加好感度", 0)]
	[AddComponentMenu("")]
	public class AddHaoGanDu : Command
	{
		// Token: 0x06007BD8 RID: 31704 RVA: 0x00054490 File Offset: 0x00052690
		public override void OnEnter()
		{
			NPCEx.AddFavor(this.AvatarID, this.AddHaoGanduNum, true, true);
			this.Continue();
		}

		// Token: 0x06007BD9 RID: 31705 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006A37 RID: 27191
		[Tooltip("增加好感度")]
		[SerializeField]
		protected int AddHaoGanduNum;

		// Token: 0x04006A38 RID: 27192
		[Tooltip("增加好感度的武将")]
		[SerializeField]
		protected int AvatarID = 1;
	}
}
