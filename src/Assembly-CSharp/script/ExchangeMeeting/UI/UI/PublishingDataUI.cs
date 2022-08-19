using System;
using Bag;
using Boo.Lang.Runtime;
using script.ExchangeMeeting.Logic.Interface;
using script.ExchangeMeeting.UI.Interface;
using UnityEngine;
using UnityEngine.Events;

namespace script.ExchangeMeeting.UI.UI
{
	// Token: 0x02000A25 RID: 2597
	public sealed class PublishingDataUI : IPublishDataUI
	{
		// Token: 0x060047A5 RID: 18341 RVA: 0x001E4E0A File Offset: 0x001E300A
		public PublishingDataUI(GameObject gameObject, IExchangeData data)
		{
			this._go = gameObject;
			this._action = new UnityAction(this.Cancel);
			this.data = (data as PlayerExchangeData);
			this.Init();
		}

		// Token: 0x060047A6 RID: 18342 RVA: 0x001E4E40 File Offset: 0x001E3040
		protected override void Init()
		{
			base.Init();
			if (this.IsErrorData())
			{
				throw new RuntimeException("exchangeData数据异常！");
			}
			this.NeedItem.SetSlotData(this.data.NeedItems[0]);
			for (int i = 0; i < this.data.GiveItems.Count; i++)
			{
				try
				{
					this.GiveItems[i].SetSlotData(this.data.GiveItems[i].Clone());
				}
				catch (Exception)
				{
					this._go.SetActive(false);
				}
				this.GiveItems[i].gameObject.SetActive(true);
			}
			this.抽成.SetText(this.data.CostMoney);
			this._go.SetActive(true);
		}

		// Token: 0x060047A7 RID: 18343 RVA: 0x001E4F24 File Offset: 0x001E3124
		private void Cancel()
		{
			if (this.IsErrorData())
			{
				throw new RuntimeException("exchangeData数据异常！");
			}
			USelectBox.Show("确定要放弃吗？", delegate
			{
				foreach (BaseItem baseItem in this.data.GiveItems)
				{
					PlayerEx.Player.addItem(baseItem.Id, baseItem.Count, baseItem.Seid, true);
				}
				PlayerEx.Player.AddMoney(this.data.CostMoney);
				IExchangeMag.Inst.ExchangeIO.Remove(this.data);
				Object.Destroy(this._go);
			}, null);
		}

		// Token: 0x060047A8 RID: 18344 RVA: 0x001E4F50 File Offset: 0x001E3150
		private bool IsErrorData()
		{
			return this.data == null || (this.data.NeedItems.Count < 1 || this.data.GiveItems.Count < 1 || this.data.GiveItems.Count > this.giveItemCount);
		}

		// Token: 0x04004897 RID: 18583
		private readonly PlayerExchangeData data;
	}
}
