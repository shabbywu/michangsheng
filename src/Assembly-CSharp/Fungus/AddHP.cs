using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013E0 RID: 5088
	[CommandInfo("YSNew/Add", "AddHP", "增加生命值", 0)]
	[AddComponentMenu("")]
	public class AddHP : Command
	{
		// Token: 0x06007BDB RID: 31707 RVA: 0x000544BA File Offset: 0x000526BA
		public override void OnEnter()
		{
			Tools.instance.getPlayer().AllMapAddHP(this.AddHpNum, DeathType.身死道消);
			this.Continue();
		}

		// Token: 0x06007BDC RID: 31708 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007BDD RID: 31709 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A39 RID: 27193
		[Tooltip("增加生命的数量")]
		[SerializeField]
		public int AddHpNum;
	}
}
