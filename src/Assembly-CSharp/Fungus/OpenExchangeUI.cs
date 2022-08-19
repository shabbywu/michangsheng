using System;
using script.ExchangeMeeting.UI.Interface;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F92 RID: 3986
	[CommandInfo("YSTools", "打开交易会", "打开交易会", 0)]
	[AddComponentMenu("")]
	public class OpenExchangeUI : Command, INoCommand
	{
		// Token: 0x06006F7C RID: 28540 RVA: 0x002A7015 File Offset: 0x002A5215
		public override void OnEnter()
		{
			IExchangeUIMag.Open();
			this.Continue();
		}

		// Token: 0x06006F7D RID: 28541 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}
	}
}
