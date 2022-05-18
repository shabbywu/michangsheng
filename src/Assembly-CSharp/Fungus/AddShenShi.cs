using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013E9 RID: 5097
	[CommandInfo("YSNew/Add", "AddShenShi", "增加神识", 0)]
	[AddComponentMenu("")]
	public class AddShenShi : Command
	{
		// Token: 0x06007BFB RID: 31739 RVA: 0x00054558 File Offset: 0x00052758
		public override void OnEnter()
		{
			Tools.instance.getPlayer().addShenShi(this.AddShenShiNum);
			this.Continue();
		}

		// Token: 0x06007BFC RID: 31740 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007BFD RID: 31741 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A4C RID: 27212
		[Tooltip("增加神识的数量")]
		[SerializeField]
		protected int AddShenShiNum;
	}
}
