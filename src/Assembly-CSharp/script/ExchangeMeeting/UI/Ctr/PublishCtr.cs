using System;
using System.Collections.Generic;
using Bag;
using JSONClass;
using script.ExchangeMeeting.Logic.Interface;
using script.ExchangeMeeting.UI.Base;
using script.ExchangeMeeting.UI.Factory;
using script.ExchangeMeeting.UI.Interface;
using script.ExchangeMeeting.UI.UI;
using UnityEngine;
using UnityEngine.Events;

namespace script.ExchangeMeeting.UI.Ctr
{
	// Token: 0x02000A32 RID: 2610
	public class PublishCtr : IPublishCtr
	{
		// Token: 0x060047D2 RID: 18386 RVA: 0x001E5448 File Offset: 0x001E3648
		public PublishCtr(GameObject gameObject)
		{
			this.UI = new PublishUI(gameObject);
			this.Factory = new PlayerExchangeFactory();
			this.Init();
		}

		// Token: 0x060047D3 RID: 18387 RVA: 0x001E5470 File Offset: 0x001E3670
		private void Init()
		{
			this.UI.PublishDataUI.SetClickAction(new UnityAction(this.Publish));
			this.UI.BackBtn.mouseUpEvent.AddListener(new UnityAction(IExchangeUIMag.Inst.OpenExchange));
			this.CreatePlayerList();
		}

		// Token: 0x060047D4 RID: 18388 RVA: 0x001E54C8 File Offset: 0x001E36C8
		public override void Publish()
		{
			if (this.CheckCanPublish())
			{
				List<BaseItem> list = new List<BaseItem>();
				PlayerEx.Player.AddMoney(-this.UI.PublishDataUI.DrawMoney);
				foreach (BaseSlot baseSlot in this.UI.PublishDataUI.GiveItems)
				{
					if (!baseSlot.IsNull())
					{
						list.Add(baseSlot.Item.Clone());
						PlayerEx.Player.removeItem(baseSlot.Item.Uid, baseSlot.Item.Count);
					}
				}
				List<BaseItem> list2 = new List<BaseItem>();
				list2.Add(this.UI.PublishDataUI.NeedItem.Item);
				IExchangeMag.Inst.ExchangeIO.CreatePlayerExchange(list2, list);
				this.UpdatePlayerList();
				this.UI.PublishDataUI.Clear();
				IExchangeUIMag.Inst.SubmitBag.CreateTempList();
			}
		}

		// Token: 0x060047D5 RID: 18389 RVA: 0x001E55DC File Offset: 0x001E37DC
		private bool CheckCanPublish()
		{
			int num = 0;
			int num2 = 0;
			Dictionary<int, _ItemJsonData> dataDict = _ItemJsonData.DataDict;
			foreach (BaseSlot baseSlot in this.UI.PublishDataUI.GiveItems)
			{
				if (!baseSlot.IsNull() && dataDict.ContainsKey(baseSlot.Item.Id))
				{
					int num3 = baseSlot.Item.GetPrice() * baseSlot.Item.Count;
					if (dataDict[baseSlot.Item.Id].ItemFlag.Contains(52))
					{
						num3 = num3 * 13 / 10;
					}
					if (dataDict[baseSlot.Item.Id].ItemFlag.Contains(53))
					{
						num3 = num3 * 13 / 10;
					}
					num += num3;
				}
			}
			if (!this.UI.PublishDataUI.NeedItem.IsNull() && dataDict.ContainsKey(this.UI.PublishDataUI.NeedItem.Item.Id))
			{
				int num4 = this.UI.PublishDataUI.NeedItem.Item.GetPrice();
				if (dataDict[this.UI.PublishDataUI.NeedItem.Item.Id].ItemFlag.Contains(52))
				{
					num4 = num4 * 13 / 10;
				}
				if (dataDict[this.UI.PublishDataUI.NeedItem.Item.Id].ItemFlag.Contains(53))
				{
					num4 = num4 * 13 / 10;
				}
				num2 = num4;
			}
			if (num2 <= 0 || num <= 0)
			{
				return false;
			}
			int needSay = this.GetNeedSay(num2, num);
			if (needSay <= 3)
			{
				IExchangeUIMag.Inst.Say(needSay);
				return false;
			}
			IExchangeUIMag.Inst.Say(needSay);
			return true;
		}

		// Token: 0x060047D6 RID: 18390 RVA: 0x001E57D8 File Offset: 0x001E39D8
		private int GetNeedSay(int need, int give)
		{
			if (need * 95 / 100 > give)
			{
				return 1;
			}
			if (this.UI.PublishDataUI.DrawMoney == 0 || this.UI.PublishDataUI.DrawMoney > (int)PlayerEx.Player.money)
			{
				return 2;
			}
			foreach (BaseSlot baseSlot in this.UI.PublishDataUI.GiveItems)
			{
				if (!baseSlot.IsNull() && this.UI.PublishDataUI.NeedItem.Item.Id == baseSlot.Item.Id)
				{
					return 3;
				}
			}
			int id = this.UI.PublishDataUI.NeedItem.Item.Id;
			if (_ItemJsonData.DataDict[id].ItemFlag.Contains(53))
			{
				return 11;
			}
			return 12;
		}

		// Token: 0x060047D7 RID: 18391 RVA: 0x001E58DC File Offset: 0x001E3ADC
		public override void CreatePlayerList()
		{
			List<IExchangeData> playerList = IExchangeMag.Inst.ExchangeIO.GetPlayerList();
			Tools.ClearChild(this.UI.ExchangeParent);
			foreach (IExchangeData data in playerList)
			{
				this.Factory.Create(this.UI.ExchangePrefab, this.UI.ExchangeParent, data);
			}
		}

		// Token: 0x060047D8 RID: 18392 RVA: 0x001E5964 File Offset: 0x001E3B64
		public override void UpdatePlayerList()
		{
			this.CreatePlayerList();
		}

		// Token: 0x060047D9 RID: 18393 RVA: 0x001E596C File Offset: 0x001E3B6C
		public override void PutNeedItem(BaseItem baseItem)
		{
			if (baseItem == null)
			{
				Debug.LogError("PutNeedItem,物品数据异常 baseItem=null");
				UIPopTip.Inst.Pop("物品数据异常", PopTipIconType.叹号);
				return;
			}
			if (!_ItemJsonData.DataDict.ContainsKey(baseItem.Id))
			{
				Debug.LogError(string.Format("PutNeedItem,物品数据异常 baseItem.Id={0}", baseItem.Id));
				UIPopTip.Inst.Pop("物品数据异常", PopTipIconType.叹号);
				return;
			}
			this.UI.PublishDataUI.NeedItem.SetSlotData(baseItem.Clone());
			this.UI.PublishDataUI.UpdateUI();
		}

		// Token: 0x060047DA RID: 18394 RVA: 0x001E59FF File Offset: 0x001E3BFF
		public override void BackNeedItem()
		{
			this.UI.PublishDataUI.NeedItem.SetNull();
			this.UI.PublishDataUI.UpdateUI();
		}

		// Token: 0x060047DB RID: 18395 RVA: 0x001E5A28 File Offset: 0x001E3C28
		public override void PutGiveItem(BaseSlot slot)
		{
			if (slot == null || slot.IsNull())
			{
				Debug.LogError("数据异常 slot=null 或者 slot.IsNull");
			}
			if (IExchangeUIMag.Inst.PlayerBag.ToSlot == null)
			{
				Debug.LogError("数据异常 IExchangeUIMag.Inst.PlayerBag.ToSlot=null");
				return;
			}
			BaseSlot toSlot = IExchangeUIMag.Inst.PlayerBag.ToSlot;
			ExchangeBag bag = IExchangeUIMag.Inst.PlayerBag;
			if (slot.Item.IsEqual(toSlot.Item) && slot.Item.MaxNum > 1)
			{
				UnityAction<int> unityAction = delegate(int num)
				{
					toSlot.Item.Count += num;
					toSlot.UpdateUI();
					bag.Hide();
					bag.RemoveTempItem(slot.Item.Uid, num);
					this.UI.PublishDataUI.UpdateUI();
				};
				if (slot.Item.Count == 1)
				{
					unityAction.Invoke(1);
					return;
				}
				USelectNum.Show(slot.Item.GetName() + " x{num}", 1, slot.Item.Count, unityAction, null);
				return;
			}
			else
			{
				UnityAction<int> unityAction2 = delegate(int num)
				{
					if (!toSlot.IsNull())
					{
						bag.AddTempItem(toSlot.Item.Clone(), toSlot.Item.Count);
					}
					BaseItem baseItem = slot.Item.Clone();
					baseItem.Count = num;
					toSlot.SetSlotData(baseItem);
					bag.Hide();
					bag.RemoveTempItem(slot.Item.Uid, num);
					this.UI.PublishDataUI.UpdateUI();
				};
				if (slot.Item.Count == 1)
				{
					unityAction2.Invoke(1);
					return;
				}
				USelectNum.Show(slot.Item.GetName() + " x{num}", 1, slot.Item.Count, unityAction2, null);
				return;
			}
		}

		// Token: 0x060047DC RID: 18396 RVA: 0x001E5B9C File Offset: 0x001E3D9C
		public override void BackGiveItem(BaseSlot slot)
		{
			if (slot == null || slot.IsNull())
			{
				Debug.LogError("数据异常 slot=null 或者 slot.IsNull");
			}
			ExchangeBag bag = IExchangeUIMag.Inst.PlayerBag;
			UnityAction<int> unityAction = delegate(int num)
			{
				bag.AddTempItem(slot.Item.Clone(), num);
				if (slot.Item.Count <= num)
				{
					slot.SetNull();
				}
				else
				{
					slot.Item.Count -= num;
					slot.UpdateUI();
				}
				this.UI.PublishDataUI.UpdateUI();
			};
			if (slot.Item.Count == 1)
			{
				unityAction.Invoke(1);
				return;
			}
			USelectNum.Show(slot.Item.GetName() + " x{num}", 1, slot.Item.Count, unityAction, null);
		}

		// Token: 0x060047DD RID: 18397 RVA: 0x001E5C4C File Offset: 0x001E3E4C
		public override bool CheckCanClickPublish()
		{
			if (this.UI == null)
			{
				Debug.LogError("CheckCanPublish UI=null");
				return false;
			}
			if (this.UI.PublishDataUI == null)
			{
				Debug.LogError("CheckCanPublish UI.PublishDataUI=null");
				return false;
			}
			if (this.UI.PublishDataUI.NeedItem == null)
			{
				Debug.LogError("CheckCanPublish UI.PublishDataUI.NeedItem=null");
				return false;
			}
			if (this.UI.PublishDataUI.NeedItem.IsNull())
			{
				return false;
			}
			if (this.UI.PublishDataUI.GiveItems == null)
			{
				Debug.LogError("CheckCanPublish UI.PublishDataUI.GiveItems=null");
				return false;
			}
			using (List<BaseSlot>.Enumerator enumerator = this.UI.PublishDataUI.GiveItems.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.IsNull())
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
