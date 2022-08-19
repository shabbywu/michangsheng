using System;
using System.Collections;
using System.Collections.Generic;
using Bag;
using JSONClass;
using KBEngine;
using SuperScrollView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tab
{
	// Token: 0x020006F5 RID: 1781
	[Serializable]
	public class TabBag : UIBase
	{
		// Token: 0x06003935 RID: 14645 RVA: 0x00186BB8 File Offset: 0x00184DB8
		public TabBag(GameObject go)
		{
			this._go = go;
			this.mLoopListView = base.Get<LoopListView2>("ItemList");
			this.BagFilter = go.GetComponent<BagFilter>();
			base.Get<FpBtn>("工具/ZhengLiBtn").mouseUpEvent.AddListener(delegate()
			{
				this.BagFilter.Sort(new UnityAction(this.UpdateItem));
			});
			this._player = Tools.instance.getPlayer();
			this.CanSort = true;
		}

		// Token: 0x06003936 RID: 14646 RVA: 0x00186C64 File Offset: 0x00184E64
		private void Init()
		{
			this._isInit = true;
			this.MoneyText = base.Get<Text>("工具/MoneyText");
			this.MoneyIcon = base.Get<Image>("工具/MoneyText/MoneyIcon");
			this.UtilsPanel = base.Get("工具", true);
			this.mLoopListView.InitListView(this.GetCount(this.mItemTotalCount), new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndex), null);
		}

		// Token: 0x06003937 RID: 14647 RVA: 0x00186CD0 File Offset: 0x00184ED0
		public void OpenBag(BagType bagType)
		{
			this.ItemType = Bag.ItemType.全部;
			this.ItemQuality = ItemQuality.全部;
			this.LianQiCaiLiaoYinYang = LianQiCaiLiaoYinYang.全部;
			this.LianQiCaiLiaoType = LianQiCaiLiaoType.全部;
			this._bagType = bagType;
			switch (bagType)
			{
			case BagType.功法:
				this.PassiveSkillList = new List<SkillItem>(this._player.hasStaticSkillList);
				this.mItemTotalCount = this.PassiveSkillList.Count;
				break;
			case BagType.技能:
				this.ActiveSkillList = new List<SkillItem>(this._player.hasSkillList);
				this.mItemTotalCount = this.ActiveSkillList.Count;
				break;
			case BagType.背包:
				this.ItemList = new List<ITEM_INFO>(this._player.itemList.values);
				this.mItemTotalCount = this.ItemList.Count;
				break;
			}
			this.SlotList = new List<ISlot>();
			if (!this._isInit)
			{
				this.Init();
				MessageMag.Instance.Register(MessageName.MSG_PLAYER_USE_ITEM, new Action<MessageData>(this.UseItemCallBack));
				this.UpdateMoney();
			}
			else
			{
				this.UpdateItem();
			}
			if (bagType == BagType.背包)
			{
				this.BagFilter.AddBigTypeBtn(delegate
				{
					using (IEnumerator enumerator = Enum.GetValues(typeof(ItemQuality)).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ItemQuality itemQuality = (ItemQuality)enumerator.Current;
							this.BagFilter.AddSmallTypeBtn(delegate
							{
								this.ItemQuality = itemQuality;
								this.BagFilter.CloseSmallSelect();
								this.UpdateItem();
							}, itemQuality.ToString());
						}
					}
				}, (this.ItemQuality == ItemQuality.全部) ? "品阶" : this.ItemQuality.ToString());
				this.BagFilter.AddBigTypeBtn(delegate
				{
					using (IEnumerator enumerator = Enum.GetValues(typeof(Bag.ItemType)).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Bag.ItemType itemType = (Bag.ItemType)enumerator.Current;
							this.BagFilter.AddSmallTypeBtn(delegate
							{
								this.ItemType = itemType;
								this.BagFilter.CloseSmallSelect();
								this.UpdateItem();
								if (itemType != Bag.ItemType.材料)
								{
									if (this.BagFilter.BigTypeIndex >= 2)
									{
										this.BagFilter.BigFilterBtnList[2].gameObject.SetActive(false);
										this.BagFilter.BigFilterBtnList[3].gameObject.SetActive(false);
									}
									return;
								}
								if (this.BagFilter.BigTypeIndex >= 3)
								{
									this.BagFilter.BigFilterBtnList[2].gameObject.SetActive(true);
									this.BagFilter.BigFilterBtnList[3].gameObject.SetActive(true);
									return;
								}
								this.BagFilter.AddBigTypeBtn(delegate
								{
									using (IEnumerator enumerator2 = Enum.GetValues(typeof(LianQiCaiLiaoYinYang)).GetEnumerator())
									{
										while (enumerator2.MoveNext())
										{
											LianQiCaiLiaoYinYang yinYang = (LianQiCaiLiaoYinYang)enumerator2.Current;
											this.BagFilter.AddSmallTypeBtn(delegate
											{
												this.LianQiCaiLiaoYinYang = yinYang;
												this.BagFilter.CloseSmallSelect();
												this.UpdateItem();
											}, yinYang.ToString());
										}
									}
								}, (this.LianQiCaiLiaoYinYang == LianQiCaiLiaoYinYang.全部) ? "阴阳" : this.LianQiCaiLiaoYinYang.ToString());
								this.BagFilter.AddBigTypeBtn(delegate
								{
									using (IEnumerator enumerator2 = Enum.GetValues(typeof(LianQiCaiLiaoType)).GetEnumerator())
									{
										while (enumerator2.MoveNext())
										{
											LianQiCaiLiaoType lianQiCaiLiaoType = (LianQiCaiLiaoType)enumerator2.Current;
											this.BagFilter.AddSmallTypeBtn(delegate
											{
												this.LianQiCaiLiaoType = lianQiCaiLiaoType;
												this.BagFilter.CloseSmallSelect();
												this.UpdateItem();
											}, lianQiCaiLiaoType.ToString());
										}
									}
								}, (this.LianQiCaiLiaoType == LianQiCaiLiaoType.全部) ? "属性" : this.LianQiCaiLiaoType.ToString());
							}, itemType.ToString());
						}
					}
				}, (this.ItemType == Bag.ItemType.全部) ? "类型" : this.ItemType.ToString());
			}
			else if (bagType == BagType.技能 || bagType == BagType.功法)
			{
				this.BagFilter.AddBigTypeBtn(delegate
				{
					using (IEnumerator enumerator = Enum.GetValues(typeof(SkillQuality)).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							SkillQuality skillQuality = (SkillQuality)enumerator.Current;
							this.BagFilter.AddSmallTypeBtn(delegate
							{
								this.SkillQuality = skillQuality;
								this.BagFilter.CloseSmallSelect();
								this.UpdateItem();
							}, skillQuality.ToString());
						}
					}
				}, (this.SkillQuality == SkillQuality.全部) ? "品阶" : this.SkillQuality.ToString());
				if (bagType == BagType.功法)
				{
					this.BagFilter.AddBigTypeBtn(delegate
					{
						List<StaticSkIllType> list = new List<StaticSkIllType>();
						list.Add(StaticSkIllType.全部);
						foreach (object obj in Enum.GetValues(typeof(SkIllType)))
						{
							StaticSkIllType staticSkIllType2 = (StaticSkIllType)obj;
							if (staticSkIllType2 != StaticSkIllType.全部)
							{
								list.Add(staticSkIllType2);
							}
						}
						using (List<StaticSkIllType>.Enumerator enumerator2 = list.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								StaticSkIllType staticSkIllType = enumerator2.Current;
								this.BagFilter.AddSmallTypeBtn(delegate
								{
									this.StaticSkIllType = staticSkIllType;
									this.BagFilter.CloseSmallSelect();
									this.UpdateItem();
								}, staticSkIllType.ToString());
							}
						}
					}, (this.StaticSkIllType == StaticSkIllType.全部) ? "属性" : this.StaticSkIllType.ToString());
				}
				else
				{
					this.BagFilter.AddBigTypeBtn(delegate
					{
						List<SkIllType> list = new List<SkIllType>();
						list.Add(SkIllType.全部);
						foreach (object obj in Enum.GetValues(typeof(SkIllType)))
						{
							SkIllType skIllType2 = (SkIllType)obj;
							if (skIllType2 != SkIllType.全部)
							{
								list.Add(skIllType2);
							}
						}
						using (List<SkIllType>.Enumerator enumerator2 = list.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								SkIllType skIllType = enumerator2.Current;
								this.BagFilter.AddSmallTypeBtn(delegate
								{
									this.SkIllType = skIllType;
									this.BagFilter.CloseSmallSelect();
									this.UpdateItem();
								}, skIllType.ToString());
							}
						}
					}, (this.SkIllType == SkIllType.全部) ? "属性" : this.SkIllType.ToString());
				}
			}
			this._go.SetActive(true);
			SingletonMono<TabUIMag>.Instance.TabBag.BagFilter.PlayHideAn();
			SingletonMono<TabUIMag>.Instance.TabFangAnPanel.Show();
		}

		// Token: 0x06003938 RID: 14648 RVA: 0x00186F34 File Offset: 0x00185134
		public void UpdateMoney()
		{
			if (this._bagType == BagType.背包)
			{
				this.MoneyText.SetText(Tools.instance.getPlayer().money);
				this.UtilsPanel.gameObject.SetActive(true);
				return;
			}
			this.UtilsPanel.gameObject.SetActive(false);
		}

		// Token: 0x06003939 RID: 14649 RVA: 0x00186F8C File Offset: 0x0018518C
		public void Close()
		{
			this.BagFilter.ResetData();
			this._go.SetActive(false);
		}

		// Token: 0x0600393A RID: 14650 RVA: 0x00186FA8 File Offset: 0x001851A8
		private LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int rowIndex)
		{
			if (rowIndex < 0)
			{
				return null;
			}
			LoopListViewItem2 loopListViewItem = listView.NewListViewItem("Prefab");
			SlotList component = loopListViewItem.GetComponent<SlotList>();
			if (!loopListViewItem.IsInitHandlerCalled)
			{
				loopListViewItem.IsInitHandlerCalled = true;
				component.Init();
			}
			for (int i = 0; i < 5; i++)
			{
				int num = rowIndex * 5 + i;
				switch (this._bagType)
				{
				case BagType.功法:
					component.mItemList[i].SetAccptType(CanSlotType.功法);
					break;
				case BagType.技能:
					component.mItemList[i].SetAccptType(CanSlotType.技能);
					break;
				case BagType.背包:
					component.mItemList[i].SetAccptType(CanSlotType.全部物品);
					break;
				}
				if (num >= this.mItemTotalCount)
				{
					component.mItemList[i].SetNull();
					if (!this.SlotList.Contains((SlotBase)component.mItemList[i]))
					{
						this.SlotList.Add((SlotBase)component.mItemList[i]);
					}
				}
				else
				{
					switch (this._bagType)
					{
					case BagType.功法:
					{
						PassiveSkill passiveSkill = new PassiveSkill();
						passiveSkill.SetSkill(this.PassiveSkillList[num].itemId, this.PassiveSkillList[num].level);
						component.mItemList[i].SetSlotData(passiveSkill);
						break;
					}
					case BagType.技能:
					{
						ActiveSkill activeSkill = new ActiveSkill();
						activeSkill.SetSkill(this.ActiveSkillList[num].itemId, Tools.instance.getPlayer().getLevelType());
						component.mItemList[i].SetSlotData(activeSkill);
						break;
					}
					case BagType.背包:
					{
						BaseItem slotData = BaseItem.Create(this.ItemList[num].itemId, (int)this.ItemList[num].itemCount, this.ItemList[num].uuid, this.ItemList[num].Seid);
						component.mItemList[i].SetSlotData(slotData);
						break;
					}
					}
					if (!this.SlotList.Contains(component.mItemList[i]))
					{
						this.SlotList.Add(component.mItemList[i]);
					}
				}
			}
			return loopListViewItem;
		}

		// Token: 0x0600393B RID: 14651 RVA: 0x001871EC File Offset: 0x001853EC
		public int GetCount(int itemCout)
		{
			int num = itemCout / 5;
			if (itemCout % 5 > 0)
			{
				num++;
			}
			return num + 1;
		}

		// Token: 0x0600393C RID: 14652 RVA: 0x0018720C File Offset: 0x0018540C
		public bool FiddlerItem(BaseItem baseItem)
		{
			if (this.ItemQuality != ItemQuality.全部 && baseItem.GetImgQuality() != (int)this.ItemQuality)
			{
				return false;
			}
			if (this.ItemType != Bag.ItemType.全部 && baseItem.ItemType != this.ItemType)
			{
				return false;
			}
			if (this.ItemType == Bag.ItemType.材料)
			{
				CaiLiaoItem caiLiaoItem = (CaiLiaoItem)baseItem;
				if (this.LianQiCaiLiaoYinYang != LianQiCaiLiaoYinYang.全部 && caiLiaoItem.GetYinYang() != this.LianQiCaiLiaoYinYang)
				{
					return false;
				}
				if (this.LianQiCaiLiaoType != LianQiCaiLiaoType.全部 && caiLiaoItem.GetLianQiCaiLiaoType() != this.LianQiCaiLiaoType)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600393D RID: 14653 RVA: 0x0018728A File Offset: 0x0018548A
		public bool FiddlerSkill(BaseSkill baseSkill)
		{
			return (this.SkillQuality == SkillQuality.全部 || baseSkill.GetImgQuality() == (int)this.SkillQuality) && (this.SkIllType == SkIllType.全部 || baseSkill.SkillTypeIsEqual((int)this.SkIllType));
		}

		// Token: 0x0600393E RID: 14654 RVA: 0x001872BE File Offset: 0x001854BE
		public bool FiddlerStaticSkill(BaseSkill baseSkill)
		{
			return (this.SkillQuality == SkillQuality.全部 || baseSkill.GetImgQuality() == (int)this.SkillQuality) && (this.StaticSkIllType == StaticSkIllType.全部 || baseSkill.SkillTypeIsEqual((int)this.StaticSkIllType));
		}

		// Token: 0x0600393F RID: 14655 RVA: 0x001872F4 File Offset: 0x001854F4
		public void UpdateItem()
		{
			if (this._bagType == BagType.背包)
			{
				this.ItemList = new List<ITEM_INFO>();
				foreach (ITEM_INFO item_INFO in this._player.itemList.values)
				{
					if (_ItemJsonData.DataDict.ContainsKey(item_INFO.itemId))
					{
						BaseItem baseItem = BaseItem.Create(item_INFO.itemId, (int)item_INFO.itemCount, item_INFO.uuid, item_INFO.Seid);
						if (this.FiddlerItem(baseItem))
						{
							this.ItemList.Add(item_INFO);
						}
					}
				}
				this.mItemTotalCount = this.ItemList.Count;
			}
			else if (this._bagType == BagType.技能)
			{
				this.ActiveSkillList = new List<SkillItem>();
				foreach (SkillItem skillItem in this._player.hasSkillList)
				{
					BaseSkill baseSkill = new ActiveSkill();
					baseSkill.SetSkill(skillItem.itemId, Tools.instance.getPlayer().getLevelType());
					if (this.FiddlerSkill(baseSkill))
					{
						this.ActiveSkillList.Add(skillItem);
					}
				}
				this.mItemTotalCount = this.ActiveSkillList.Count;
			}
			else if (this._bagType == BagType.功法)
			{
				this.PassiveSkillList = new List<SkillItem>();
				foreach (SkillItem skillItem2 in this._player.hasStaticSkillList)
				{
					BaseSkill baseSkill2 = new PassiveSkill();
					baseSkill2.SetSkill(skillItem2.itemId, skillItem2.level);
					if (this.FiddlerStaticSkill(baseSkill2))
					{
						this.PassiveSkillList.Add(skillItem2);
					}
				}
				this.mItemTotalCount = this.PassiveSkillList.Count;
			}
			this.mLoopListView.SetListItemCount(this.GetCount(this.mItemTotalCount), true);
			this.mLoopListView.RefreshAllShownItem();
			this.UpdateMoney();
		}

		// Token: 0x06003940 RID: 14656 RVA: 0x0018752C File Offset: 0x0018572C
		public BagType GetCurBagType()
		{
			return this._bagType;
		}

		// Token: 0x06003941 RID: 14657 RVA: 0x00187534 File Offset: 0x00185734
		public void UpDateSlotList()
		{
			if (this._bagType == BagType.背包)
			{
				this.ItemList = new List<ITEM_INFO>();
				foreach (ITEM_INFO item_INFO in this._player.itemList.values)
				{
					BaseItem baseItem = BaseItem.Create(item_INFO.itemId, (int)item_INFO.itemCount, item_INFO.uuid, item_INFO.Seid);
					if (this.FiddlerItem(baseItem))
					{
						this.ItemList.Add(item_INFO);
					}
				}
				this.mItemTotalCount = this.ItemList.Count;
			}
			else if (this._bagType == BagType.技能)
			{
				this.ActiveSkillList = new List<SkillItem>();
				foreach (SkillItem skillItem in this._player.hasSkillList)
				{
					BaseSkill baseSkill = new ActiveSkill();
					baseSkill.SetSkill(skillItem.itemId, skillItem.level);
					if (this.FiddlerSkill(baseSkill))
					{
						this.ActiveSkillList.Add(skillItem);
					}
				}
				this.mItemTotalCount = this.ActiveSkillList.Count;
			}
			else if (this._bagType == BagType.功法)
			{
				this.PassiveSkillList = new List<SkillItem>();
				foreach (SkillItem skillItem2 in this._player.hasStaticSkillList)
				{
					BaseSkill baseSkill2 = new PassiveSkill();
					baseSkill2.SetSkill(skillItem2.itemId, skillItem2.level);
					if (this.FiddlerStaticSkill(baseSkill2))
					{
						this.PassiveSkillList.Add(skillItem2);
					}
				}
				this.mItemTotalCount = this.PassiveSkillList.Count;
			}
			this.mLoopListView.SetListItemCount(this.GetCount(this.mItemTotalCount), false);
			this.mLoopListView.RefreshAllShownItem();
		}

		// Token: 0x06003942 RID: 14658 RVA: 0x0018774C File Offset: 0x0018594C
		public SlotBase GetNullSlot()
		{
			foreach (ISlot slot in this.SlotList)
			{
				SlotBase slotBase = (SlotBase)slot;
				if (slotBase.transform.parent.gameObject.activeSelf && slotBase.IsNull())
				{
					return slotBase;
				}
			}
			return null;
		}

		// Token: 0x06003943 RID: 14659 RVA: 0x001877C8 File Offset: 0x001859C8
		public void UseItemCallBack(MessageData messageData)
		{
			this.UpDateSlotList();
		}

		// Token: 0x04003143 RID: 12611
		private Avatar _player;

		// Token: 0x04003144 RID: 12612
		private BagType _bagType;

		// Token: 0x04003145 RID: 12613
		public Bag.ItemType ItemType;

		// Token: 0x04003146 RID: 12614
		public ItemQuality ItemQuality;

		// Token: 0x04003147 RID: 12615
		public LianQiCaiLiaoYinYang LianQiCaiLiaoYinYang;

		// Token: 0x04003148 RID: 12616
		public LianQiCaiLiaoType LianQiCaiLiaoType;

		// Token: 0x04003149 RID: 12617
		public SkIllType SkIllType = SkIllType.全部;

		// Token: 0x0400314A RID: 12618
		public SkillQuality SkillQuality;

		// Token: 0x0400314B RID: 12619
		public StaticSkIllType StaticSkIllType = StaticSkIllType.全部;

		// Token: 0x0400314C RID: 12620
		public BagFilter BagFilter;

		// Token: 0x0400314D RID: 12621
		public Text MoneyText;

		// Token: 0x0400314E RID: 12622
		public Image MoneyIcon;

		// Token: 0x0400314F RID: 12623
		public GameObject UtilsPanel;

		// Token: 0x04003150 RID: 12624
		public List<ITEM_INFO> ItemList = new List<ITEM_INFO>();

		// Token: 0x04003151 RID: 12625
		public List<SkillItem> ActiveSkillList = new List<SkillItem>();

		// Token: 0x04003152 RID: 12626
		public List<SkillItem> PassiveSkillList = new List<SkillItem>();

		// Token: 0x04003153 RID: 12627
		public bool CanSort;

		// Token: 0x04003154 RID: 12628
		public LoopListView2 mLoopListView;

		// Token: 0x04003155 RID: 12629
		public List<ISlot> SlotList = new List<ISlot>();

		// Token: 0x04003156 RID: 12630
		private bool _isInit;

		// Token: 0x04003157 RID: 12631
		private const int mItemCountPerRow = 5;

		// Token: 0x04003158 RID: 12632
		private int mItemTotalCount;
	}
}
