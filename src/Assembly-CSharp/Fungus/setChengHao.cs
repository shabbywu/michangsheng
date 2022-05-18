using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001410 RID: 5136
	[CommandInfo("YSNew/Set", "setChengHao", "设置称号id", 0)]
	[AddComponentMenu("")]
	public class setChengHao : Command
	{
		// Token: 0x06007C9B RID: 31899 RVA: 0x00054784 File Offset: 0x00052984
		public override void OnEnter()
		{
			Tools.instance.getPlayer().SetChengHaoId(this.chengHaoID);
			this.Continue();
		}

		// Token: 0x06007C9C RID: 31900 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007C9D RID: 31901 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A8F RID: 27279
		[Tooltip("设置称号的ID")]
		[SerializeField]
		protected int chengHaoID;
	}
}
