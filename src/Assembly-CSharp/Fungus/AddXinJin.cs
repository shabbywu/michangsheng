using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F39 RID: 3897
	[CommandInfo("YSNew/Add", "AddXinJin", "增加心境", 0)]
	[AddComponentMenu("")]
	public class AddXinJin : Command
	{
		// Token: 0x06006E27 RID: 28199 RVA: 0x002A45DC File Offset: 0x002A27DC
		public override void OnEnter()
		{
			string str = (this.AddXinjinNum >= 0) ? ("提升了" + Math.Abs(this.AddXinjinNum)) : ("降低了" + Math.Abs(this.AddXinjinNum));
			PopTipIconType iconType = (this.AddXinjinNum >= 0) ? PopTipIconType.上箭头 : PopTipIconType.下箭头;
			UIPopTip.Inst.Pop("你的心境" + str, iconType);
			Tools.instance.getPlayer().xinjin += this.AddXinjinNum;
			this.Continue();
		}

		// Token: 0x06006E28 RID: 28200 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005B85 RID: 23429
		[Tooltip("增加心境的数量")]
		[SerializeField]
		public int AddXinjinNum;
	}
}
