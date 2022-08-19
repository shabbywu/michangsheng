using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F33 RID: 3891
	[CommandInfo("YSNew/Add", "AddShenShi", "增加神识", 0)]
	[AddComponentMenu("")]
	public class AddShenShi : Command
	{
		// Token: 0x06006E10 RID: 28176 RVA: 0x002A4438 File Offset: 0x002A2638
		public override void OnEnter()
		{
			Tools.instance.getPlayer().addShenShi(this.AddShenShiNum);
			this.Continue();
		}

		// Token: 0x06006E11 RID: 28177 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006E12 RID: 28178 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B7A RID: 23418
		[Tooltip("增加神识的数量")]
		[SerializeField]
		protected int AddShenShiNum;
	}
}
