using System;
using System.Collections.Generic;
using Bag;
using UnityEngine.Events;
using UnityEngine.UI;

namespace script.ExchangeMeeting.UI.Interface
{
	// Token: 0x02000A2C RID: 2604
	public abstract class IPublishDataUI : UIBase
	{
		// Token: 0x060047C2 RID: 18370 RVA: 0x001E5180 File Offset: 0x001E3380
		protected virtual void Init()
		{
			this.NeedItem = base.Get<BaseSlot>("需求物品/1");
			this.NeedItem.SetNull();
			this.GiveItems = new List<BaseSlot>();
			for (int i = 1; i <= this.giveItemCount; i++)
			{
				this.GiveItems.Add(base.Get<BaseSlot>(string.Format("兑换物品/{0}", i)));
				this.GiveItems[i - 1].SetNull();
			}
			this.抽成 = base.Get<Text>("抽成/Value");
			this.Btn = base.Get<FpBtn>("按钮/按钮");
			this.Btn.mouseUpEvent.AddListener(delegate()
			{
				UnityAction action = this._action;
				if (action == null)
				{
					return;
				}
				action.Invoke();
			});
		}

		// Token: 0x060047C3 RID: 18371 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void Clear()
		{
		}

		// Token: 0x060047C4 RID: 18372 RVA: 0x001E5237 File Offset: 0x001E3437
		public virtual void SetClickAction(UnityAction action)
		{
			this._action = action;
		}

		// Token: 0x060047C5 RID: 18373 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void UpdateUI()
		{
		}

		// Token: 0x040048AA RID: 18602
		public List<BaseSlot> GiveItems;

		// Token: 0x040048AB RID: 18603
		public BaseSlot NeedItem;

		// Token: 0x040048AC RID: 18604
		public int DrawMoney;

		// Token: 0x040048AD RID: 18605
		public Text 抽成;

		// Token: 0x040048AE RID: 18606
		public FpBtn Btn;

		// Token: 0x040048AF RID: 18607
		protected UnityAction _action;

		// Token: 0x040048B0 RID: 18608
		protected int giveItemCount = 4;
	}
}
