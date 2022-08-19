using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000FA1 RID: 4001
	[CommandInfo("YSTools", "SetTips", "提示", 0)]
	[AddComponentMenu("")]
	public class SetTips : Command
	{
		// Token: 0x06006FAE RID: 28590 RVA: 0x002A74A3 File Offset: 0x002A56A3
		public override void OnEnter()
		{
			UIPopTip.Inst.Pop(this.Content, PopTipIconType.叹号);
			this.Continue();
		}

		// Token: 0x06006FAF RID: 28591 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005C22 RID: 23586
		[Tooltip("提示内容")]
		[SerializeField]
		protected string Content;
	}
}
