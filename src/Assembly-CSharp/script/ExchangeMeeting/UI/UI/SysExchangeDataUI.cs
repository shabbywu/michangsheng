using System;
using Bag;
using JSONClass;
using script.ExchangeMeeting.Logic;
using script.ExchangeMeeting.Logic.Interface;
using script.ExchangeMeeting.UI.Base;
using script.ExchangeMeeting.UI.Interface;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace script.ExchangeMeeting.UI.UI
{
	// Token: 0x02000A23 RID: 2595
	public sealed class SysExchangeDataUI : IExchangeDataUI
	{
		// Token: 0x06004796 RID: 18326 RVA: 0x001E42FA File Offset: 0x001E24FA
		public SysExchangeDataUI(GameObject gameObject, IExchangeData data)
		{
			this._go = gameObject;
			this._go.SetActive(true);
			this._data = data;
			this.Init();
		}

		// Token: 0x06004797 RID: 18327 RVA: 0x001E4460 File Offset: 0x001E2660
		protected override void Init()
		{
			this.i = 1;
			this.data = (this._data as SysExchangeData);
			this.InitGiveItem();
			this.InitNeedTag();
			this.InitNeedItem();
			this.InitSubmitItem();
		}

		// Token: 0x06004798 RID: 18328 RVA: 0x001E4494 File Offset: 0x001E2694
		private void InitGiveItem()
		{
			if (this.data == null || this.data.GiveItems.Count < 1 || this.data.GiveItems[0] == null)
			{
				Debug.LogError("初始化系统交易会数据失败，已自动隐藏");
				this._go.SetActive(false);
				return;
			}
			base.Get<BaseSlot>("GiveList/1").SetSlotData(this.data.GiveItems[0].Clone());
		}

		// Token: 0x06004799 RID: 18329 RVA: 0x001E450C File Offset: 0x001E270C
		private void InitNeedItem()
		{
			if (this.IsNeedError())
			{
				Debug.LogError("需求物品数量异常，已自动隐藏");
				this._go.SetActive(false);
				return;
			}
			foreach (BaseItem baseItem in this.data.NeedItems)
			{
				base.Get("NeedList/" + this.i, true).SetActive(true);
				base.Get<Text>("NeedList/" + this.i).SetText(string.Format("{0}x{1}", baseItem.GetName(), baseItem.Count));
				this.i++;
			}
			if (this.i < 4)
			{
				base.Get(string.Format("NeedList/{0}/或者", this.i - 1), true).SetActive(false);
			}
		}

		// Token: 0x0600479A RID: 18330 RVA: 0x001E4614 File Offset: 0x001E2814
		private void InitNeedTag()
		{
			if (this.IsNeedError())
			{
				Debug.LogError("需求物品数量异常，已自动隐藏");
				this._go.SetActive(false);
				return;
			}
			foreach (int num in this.data.NeedTags.Keys)
			{
				if (!ItemFlagData.DataDict.ContainsKey(num))
				{
					Debug.LogError("不存在物品标签id：" + num + "，已跳过");
				}
				else
				{
					base.Get("NeedList/" + this.i, true).SetActive(true);
					base.Get<Text>("NeedList/" + this.i).SetText(string.Format("{0}x{1}", ItemFlagData.DataDict[num].name, this.data.NeedTags[num]));
					this.i++;
				}
			}
		}

		// Token: 0x0600479B RID: 18331 RVA: 0x001E473C File Offset: 0x001E293C
		private void InitSubmitItem()
		{
			this.submitBtn = base.Get<FpBtn>("提交/提交按钮");
			this.submitBtn.mouseUpEvent.AddListener(new UnityAction(this.Submit));
			this.disable = base.Get("提交/不可点击", true);
			this.SubSlot = base.Get<ExchangeSlotC>("提交/1");
			this.SubSlot.SetNull();
			this.SubSlot.Init(this);
		}

		// Token: 0x0600479C RID: 18332 RVA: 0x001E47B0 File Offset: 0x001E29B0
		public void UpdateCanSubmit()
		{
			if (this.SubSlot.IsNull() || !this.IsNeedItem(this.SubSlot.Item))
			{
				this.disable.SetActive(true);
				this.submitBtn.gameObject.SetActive(false);
				return;
			}
			this.disable.SetActive(false);
			this.submitBtn.gameObject.SetActive(true);
		}

		// Token: 0x0600479D RID: 18333 RVA: 0x001E4818 File Offset: 0x001E2A18
		private void Submit()
		{
			if (this.IsNeedItem(this.SubSlot.Item))
			{
				PlayerEx.Player.removeItem(this.SubSlot.Item.Uid, this.SubSlot.Item.Count);
				foreach (BaseItem baseItem in this.data.GiveItems)
				{
					PlayerEx.Player.addItem(baseItem.Id, baseItem.Count, Tools.CreateItemSeid(baseItem.Id), true);
				}
				if (this.data.IsGuDing && GuDingExchangeData.DataDict.ContainsKey(this.data.Id))
				{
					IExchangeMag.Inst.ExchangeIO.SaveGuDingId(this.data.Id);
				}
				IExchangeMag.Inst.ExchangeIO.Remove(this._data);
				IExchangeUIMag.Inst.SubmitBag.CreateTempList();
				Object.Destroy(this._go);
				return;
			}
			UIPopTip.Inst.Pop("提交物品异常，请反馈", PopTipIconType.叹号);
			Debug.LogError("提交物品异常");
		}

		// Token: 0x0600479E RID: 18334 RVA: 0x001E4954 File Offset: 0x001E2B54
		public bool IsNeedItem(BaseItem baseItem)
		{
			if (baseItem == null || !_ItemJsonData.DataDict.ContainsKey(baseItem.Id))
			{
				Debug.LogError("物品为空或id异常");
				return false;
			}
			if (this.data.NeedTags.Count > 0)
			{
				foreach (int key in _ItemJsonData.DataDict[baseItem.Id].ItemFlag)
				{
					if (this.data.NeedTags.ContainsKey(key) && this.data.NeedTags[key] <= baseItem.Count)
					{
						return true;
					}
				}
			}
			if (this.data.NeedItems.Count > 0)
			{
				foreach (BaseItem baseItem2 in this.data.NeedItems)
				{
					if (baseItem.Id == baseItem2.Id)
					{
						if (baseItem.Count >= baseItem2.Count)
						{
							return true;
						}
						return false;
					}
				}
			}
			return false;
		}

		// Token: 0x0600479F RID: 18335 RVA: 0x001E4A94 File Offset: 0x001E2C94
		public int GetPutNum(BaseItem baseItem)
		{
			if (this.data.NeedTags.Count > 0)
			{
				foreach (int key in _ItemJsonData.DataDict[baseItem.Id].ItemFlag)
				{
					if (this.data.NeedTags.ContainsKey(key) && this.data.NeedTags[key] <= baseItem.Count)
					{
						return this.data.NeedTags[key];
					}
				}
			}
			if (this.data.NeedItems.Count > 0)
			{
				foreach (BaseItem baseItem2 in this.data.NeedItems)
				{
					if (baseItem.Id == baseItem2.Id && baseItem.Count >= baseItem2.Count)
					{
						return baseItem2.Count;
					}
				}
			}
			Debug.LogError("获取放入数目异常");
			return -1;
		}

		// Token: 0x060047A0 RID: 18336 RVA: 0x001E4BD4 File Offset: 0x001E2DD4
		private bool IsNeedError()
		{
			if ((this.data.NeedTags.Count < 1 && this.data.NeedItems.Count < 1) || this.data.NeedTags.Count + this.data.NeedItems.Count > 3)
			{
				Debug.LogError("需求物品数量异常，已自动隐藏");
				this._go.SetActive(false);
				return true;
			}
			return false;
		}

		// Token: 0x04004891 RID: 18577
		public ExchangeSlotC SubSlot;

		// Token: 0x04004892 RID: 18578
		private SysExchangeData data;

		// Token: 0x04004893 RID: 18579
		private FpBtn submitBtn;

		// Token: 0x04004894 RID: 18580
		private GameObject disable;

		// Token: 0x04004895 RID: 18581
		private int i;
	}
}
