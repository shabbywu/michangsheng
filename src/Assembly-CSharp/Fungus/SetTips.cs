using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001450 RID: 5200
	[CommandInfo("YSTools", "SetTips", "提示", 0)]
	[AddComponentMenu("")]
	public class SetTips : Command
	{
		// Token: 0x06007D8C RID: 32140 RVA: 0x00054E89 File Offset: 0x00053089
		public override void OnEnter()
		{
			UIPopTip.Inst.Pop(this.Content, PopTipIconType.叹号);
			this.Continue();
		}

		// Token: 0x06007D8D RID: 32141 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006AF1 RID: 27377
		[Tooltip("提示内容")]
		[SerializeField]
		protected string Content;
	}
}
