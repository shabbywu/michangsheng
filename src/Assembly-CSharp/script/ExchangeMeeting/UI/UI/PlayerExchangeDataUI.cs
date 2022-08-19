using System;
using Bag;
using JSONClass;
using script.ExchangeMeeting.Logic.Interface;
using script.ExchangeMeeting.UI.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace script.ExchangeMeeting.UI.UI
{
	// Token: 0x02000A22 RID: 2594
	public sealed class PlayerExchangeDataUI : IExchangeDataUI
	{
		// Token: 0x06004792 RID: 18322 RVA: 0x001E42FA File Offset: 0x001E24FA
		public PlayerExchangeDataUI(GameObject gameObject, IExchangeData data)
		{
			this._go = gameObject;
			this._go.SetActive(true);
			this._data = data;
			this.Init();
		}

		// Token: 0x06004793 RID: 18323 RVA: 0x001E4322 File Offset: 0x001E2522
		protected override void Init()
		{
			this.data = (this._data as PlayerExchangeData);
			this.InitGiveItem();
			this.InitNeedItem();
		}

		// Token: 0x06004794 RID: 18324 RVA: 0x001E4344 File Offset: 0x001E2544
		private void InitGiveItem()
		{
			int num = 1;
			foreach (BaseItem baseItem in this.data.GiveItems)
			{
				if (baseItem == null || !_ItemJsonData.DataDict.ContainsKey(baseItem.Id))
				{
					Debug.LogError("PlayerExchangeDataUI.InitGiveItem物品数据异常已跳过");
				}
				else
				{
					base.Get<Text>("GiveList/" + num).SetText(string.Format("{0}x{1}", baseItem.GetName(), baseItem.Count));
					base.Get("GiveList/" + num, true).SetActive(true);
					num++;
				}
			}
			if (num < 5)
			{
				base.Get(string.Format("GiveList/{0}/加上", num - 1), true).SetActive(false);
			}
		}

		// Token: 0x06004795 RID: 18325 RVA: 0x001E4438 File Offset: 0x001E2638
		private void InitNeedItem()
		{
			base.Get<BaseSlot>("NeedList/1").SetSlotData(this.data.NeedItems[0].Clone());
		}

		// Token: 0x04004890 RID: 18576
		private PlayerExchangeData data;
	}
}
