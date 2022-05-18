using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013DE RID: 5086
	[CommandInfo("YSNew/Add", "AddEXP", "增加经验", 0)]
	[AddComponentMenu("")]
	public class AddEXP : Command
	{
		// Token: 0x06007BD4 RID: 31700 RVA: 0x00054453 File Offset: 0x00052653
		public override void OnEnter()
		{
			UIPopTip.Inst.Pop("你的修为提升了" + this.AddEXPNum, PopTipIconType.上箭头);
			Tools.instance.getPlayer().addEXP(this.AddEXPNum);
			this.Continue();
		}

		// Token: 0x06007BD5 RID: 31701 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007BD6 RID: 31702 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A36 RID: 27190
		[Tooltip("增加经验的数量")]
		[SerializeField]
		public int AddEXPNum;
	}
}
