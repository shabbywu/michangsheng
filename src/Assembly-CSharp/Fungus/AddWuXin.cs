using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013EE RID: 5102
	[CommandInfo("YSNew/Add", "AddWuXin", "增加悟性", 0)]
	[AddComponentMenu("")]
	public class AddWuXin : Command
	{
		// Token: 0x06007C0E RID: 31758 RVA: 0x0005460C File Offset: 0x0005280C
		public override void OnEnter()
		{
			Tools.instance.getPlayer().addWuXin(this.AddWuXinNum);
			this.Continue();
		}

		// Token: 0x06007C0F RID: 31759 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007C10 RID: 31760 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A56 RID: 27222
		[Tooltip("增加悟性的数量")]
		[SerializeField]
		protected int AddWuXinNum;
	}
}
