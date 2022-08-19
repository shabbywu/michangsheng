using System;
using script.ExchangeMeeting.UI.Base;
using UnityEngine;

namespace script.ExchangeMeeting.UI.Interface
{
	// Token: 0x02000A2B RID: 2603
	public abstract class IExchangeUI : BasePanelB
	{
		// Token: 0x040048A6 RID: 18598
		public FpBtn BackBtn;

		// Token: 0x040048A7 RID: 18599
		public GameObject SysEvent;

		// Token: 0x040048A8 RID: 18600
		public GameObject PlayerEvent;

		// Token: 0x040048A9 RID: 18601
		public Transform EventParent;
	}
}
