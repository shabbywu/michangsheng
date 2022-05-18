using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001445 RID: 5189
	[CommandInfo("YSTools", "OpenEventOption", "打开事件选择界面", 0)]
	[AddComponentMenu("")]
	public class OpenEventOption : Command
	{
		// Token: 0x06007D62 RID: 32098 RVA: 0x00054D05 File Offset: 0x00052F05
		public override void OnEnter()
		{
			new AddOption().addOption(this.ShiJianID);
			this.Continue();
		}

		// Token: 0x06007D63 RID: 32099 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007D64 RID: 32100 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006ADF RID: 27359
		[Tooltip("需要打开的事件ID")]
		[SerializeField]
		protected int ShiJianID = 1;
	}
}
