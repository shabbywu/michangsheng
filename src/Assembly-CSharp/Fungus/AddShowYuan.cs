using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013EA RID: 5098
	[CommandInfo("YSNew/Add", "AddShowYuan", "增加寿元", 0)]
	[AddComponentMenu("")]
	public class AddShowYuan : Command
	{
		// Token: 0x06007BFF RID: 31743 RVA: 0x00054575 File Offset: 0x00052775
		public override void OnEnter()
		{
			Tools.instance.getPlayer().addShoYuan(this.AddShouYuanNum);
			this.Continue();
		}

		// Token: 0x06007C00 RID: 31744 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007C01 RID: 31745 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A4D RID: 27213
		[Tooltip("增加经验的数量")]
		[SerializeField]
		protected int AddShouYuanNum;
	}
}
