using System;
using script.ExchangeMeeting.UI.Base;
using UnityEngine;

namespace script.ExchangeMeeting.UI.Interface
{
	// Token: 0x02000A2D RID: 2605
	public abstract class IPublishUI : BasePanelB
	{
		// Token: 0x040048B1 RID: 18609
		public IPublishDataUI PublishDataUI;

		// Token: 0x040048B2 RID: 18610
		public GameObject ExchangePrefab;

		// Token: 0x040048B3 RID: 18611
		public Transform ExchangeParent;

		// Token: 0x040048B4 RID: 18612
		public FpBtn BackBtn;
	}
}
