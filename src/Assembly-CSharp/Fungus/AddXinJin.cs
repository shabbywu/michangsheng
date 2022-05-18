using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013EF RID: 5103
	[CommandInfo("YSNew/Add", "AddXinJin", "增加心境", 0)]
	[AddComponentMenu("")]
	public class AddXinJin : Command
	{
		// Token: 0x06007C12 RID: 31762 RVA: 0x002C463C File Offset: 0x002C283C
		public override void OnEnter()
		{
			string str = (this.AddXinjinNum >= 0) ? ("提升了" + Math.Abs(this.AddXinjinNum)) : ("降低了" + Math.Abs(this.AddXinjinNum));
			PopTipIconType iconType = (this.AddXinjinNum >= 0) ? PopTipIconType.上箭头 : PopTipIconType.下箭头;
			UIPopTip.Inst.Pop("你的心境" + str, iconType);
			Tools.instance.getPlayer().xinjin += this.AddXinjinNum;
			this.Continue();
		}

		// Token: 0x06007C13 RID: 31763 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006A57 RID: 27223
		[Tooltip("增加心境的数量")]
		[SerializeField]
		public int AddXinjinNum;
	}
}
