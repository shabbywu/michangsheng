using System;
using script.ExchangeMeeting.UI.Interface;
using UnityEngine;

namespace script.ExchangeMeeting.UI.UI
{
	// Token: 0x02000A26 RID: 2598
	public class PublishUI : IPublishUI
	{
		// Token: 0x060047AA RID: 18346 RVA: 0x001E504C File Offset: 0x001E324C
		public PublishUI(GameObject gameObject)
		{
			this._go = gameObject;
			this.PublishDataUI = new PublishDataUI(base.Get("发布", true));
			this.ExchangePrefab = base.Get("任务列表/Viewport/Content/已发布", true);
			this.ExchangeParent = base.Get("任务列表/Viewport/Content/", true).transform;
			this.BackBtn = base.Get<FpBtn>("返回交易按钮");
		}

		// Token: 0x060047AB RID: 18347 RVA: 0x001E50BE File Offset: 0x001E32BE
		public override void Hide()
		{
			IPublishDataUI publishDataUI = this.PublishDataUI;
			if (publishDataUI != null)
			{
				publishDataUI.Clear();
			}
			base.Hide();
		}

		// Token: 0x04004898 RID: 18584
		private int giveItemCount = 4;
	}
}
