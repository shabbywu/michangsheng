using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F91 RID: 3985
	[CommandInfo("YSTools", "OpenEventOption", "打开事件选择界面", 0)]
	[AddComponentMenu("")]
	public class OpenEventOption : Command
	{
		// Token: 0x06006F78 RID: 28536 RVA: 0x002A6FEE File Offset: 0x002A51EE
		public override void OnEnter()
		{
			new AddOption().addOption(this.ShiJianID);
			this.Continue();
		}

		// Token: 0x06006F79 RID: 28537 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006F7A RID: 28538 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005C10 RID: 23568
		[Tooltip("需要打开的事件ID")]
		[SerializeField]
		protected int ShiJianID = 1;
	}
}
