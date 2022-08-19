using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F29 RID: 3881
	[CommandInfo("YSNew/Add", "AddHaoGanDu", "增加好感度", 0)]
	[AddComponentMenu("")]
	public class AddHaoGanDu : Command
	{
		// Token: 0x06006DED RID: 28141 RVA: 0x002A406F File Offset: 0x002A226F
		public override void OnEnter()
		{
			NPCEx.AddFavor(this.AvatarID, this.AddHaoGanduNum, true, true);
			this.Continue();
		}

		// Token: 0x06006DEE RID: 28142 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005B65 RID: 23397
		[Tooltip("增加好感度")]
		[SerializeField]
		protected int AddHaoGanduNum;

		// Token: 0x04005B66 RID: 23398
		[Tooltip("增加好感度的武将")]
		[SerializeField]
		protected int AvatarID = 1;
	}
}
