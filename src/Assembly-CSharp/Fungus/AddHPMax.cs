using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013E1 RID: 5089
	[CommandInfo("YSNew/Add", "AddHPMax", "增加生命最大值", 0)]
	[AddComponentMenu("")]
	public class AddHPMax : Command
	{
		// Token: 0x06007BDF RID: 31711 RVA: 0x000544D8 File Offset: 0x000526D8
		public override void OnEnter()
		{
			Tools.instance.getPlayer().AllMapAddHPMax(this.AddHPMaxNum);
			this.Continue();
		}

		// Token: 0x06007BE0 RID: 31712 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007BE1 RID: 31713 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A3A RID: 27194
		[Tooltip("增加经验的数量")]
		[SerializeField]
		protected int AddHPMaxNum;
	}
}
