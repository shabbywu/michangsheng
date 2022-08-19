using System;
using script.ExchangeMeeting.UI.Interface;
using UnityEngine;
using UnityEngine.Events;

namespace script.ExchangeMeeting.UI.UI
{
	// Token: 0x02000A21 RID: 2593
	public class ExchangeUI : IExchangeUI
	{
		// Token: 0x06004790 RID: 18320 RVA: 0x001E4274 File Offset: 0x001E2474
		public ExchangeUI(GameObject gameObject, UnityAction action)
		{
			this._go = gameObject;
			this.UpdateAction = action;
			this.BackBtn = base.Get<FpBtn>("寄换物品按钮");
			this.SysEvent = base.Get("事件列表/Mask/Content/系统事件", true);
			this.PlayerEvent = base.Get("事件列表/Mask/Content/玩家事件", true);
			this.EventParent = base.Get("事件列表/Mask/Content", true).transform;
		}

		// Token: 0x06004791 RID: 18321 RVA: 0x001E42E1 File Offset: 0x001E24E1
		public override void Show()
		{
			UnityAction updateAction = this.UpdateAction;
			if (updateAction != null)
			{
				updateAction.Invoke();
			}
			base.Show();
		}

		// Token: 0x0400488F RID: 18575
		public UnityAction UpdateAction;
	}
}
