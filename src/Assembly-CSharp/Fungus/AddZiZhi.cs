using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013F0 RID: 5104
	[CommandInfo("YS", "AddZiZhi", "增加资质", 0)]
	[AddComponentMenu("")]
	public class AddZiZhi : Command
	{
		// Token: 0x06007C15 RID: 31765 RVA: 0x00054629 File Offset: 0x00052829
		public override void OnEnter()
		{
			Tools.instance.getPlayer().addZiZhi(this.AddZiZhiNum);
			this.Continue();
		}

		// Token: 0x06007C16 RID: 31766 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007C17 RID: 31767 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A58 RID: 27224
		[Tooltip("增加资质的数量")]
		[SerializeField]
		protected int AddZiZhiNum;
	}
}
