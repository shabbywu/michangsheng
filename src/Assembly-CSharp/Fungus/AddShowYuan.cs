using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F34 RID: 3892
	[CommandInfo("YSNew/Add", "AddShowYuan", "增加寿元", 0)]
	[AddComponentMenu("")]
	public class AddShowYuan : Command
	{
		// Token: 0x06006E14 RID: 28180 RVA: 0x002A4455 File Offset: 0x002A2655
		public override void OnEnter()
		{
			Tools.instance.getPlayer().addShoYuan(this.AddShouYuanNum);
			this.Continue();
		}

		// Token: 0x06006E15 RID: 28181 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006E16 RID: 28182 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B7B RID: 23419
		[Tooltip("增加经验的数量")]
		[SerializeField]
		protected int AddShouYuanNum;
	}
}
