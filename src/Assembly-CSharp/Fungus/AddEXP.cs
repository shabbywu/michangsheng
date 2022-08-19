using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F28 RID: 3880
	[CommandInfo("YSNew/Add", "AddEXP", "增加经验", 0)]
	[AddComponentMenu("")]
	public class AddEXP : Command
	{
		// Token: 0x06006DE9 RID: 28137 RVA: 0x002A4032 File Offset: 0x002A2232
		public override void OnEnter()
		{
			UIPopTip.Inst.Pop("你的修为提升了" + this.AddEXPNum, PopTipIconType.上箭头);
			Tools.instance.getPlayer().addEXP(this.AddEXPNum);
			this.Continue();
		}

		// Token: 0x06006DEA RID: 28138 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006DEB RID: 28139 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B64 RID: 23396
		[Tooltip("增加经验的数量")]
		[SerializeField]
		public int AddEXPNum;
	}
}
