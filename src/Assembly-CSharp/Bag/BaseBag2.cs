using System;
using System.Collections.Generic;
using JiaoYi;
using JSONClass;
using KBEngine;
using SuperScrollView;
using UnityEngine;
using UnityEngine.UI;

namespace Bag
{
	// Token: 0x02000996 RID: 2454
	public class BaseBag2 : MonoBehaviour
	{
		// Token: 0x0600446A RID: 17514 RVA: 0x001D1F94 File Offset: 0x001D0194
		public virtual void Init(int npcId, bool isPlayer = false)
		{
			this._player = Tools.instance.getPlayer();
			this.NpcId = npcId;
			this.IsPlayer = isPlayer;
			this.CreateTempList();
			this.MLoopListView.InitListView(this.GetCount(this.MItemTotalCount), new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndex), null);
			this.ItemType = ItemType.全部;
			this.ItemQuality = ItemQuality.全部;
			this.JiaoYiSkillType = JiaoYiSkillType.全部;
			this.LianQiCaiLiaoYinYang = LianQiCaiLiaoYinYang.全部;
			this.LianQiCaiLiaoType = LianQiCaiLiaoType.全部;
			this.InitLeftFilter();
			this.UpdateLeftFilter(this.LeftList[0]);
		}

		// Token: 0x0600446B RID: 17515 RVA: 0x001D2024 File Offset: 0x001D0224
		public virtual void CreateTempList()
		{
			this.TempBagList = new List<ITEM_INFO>();
			List<ITEM_INFO> list = new List<ITEM_INFO>();
			if (this.IsPlayer)
			{
				using (List<ITEM_INFO>.Enumerator enumerator = this._player.itemList.values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ITEM_INFO item_INFO = enumerator.Current;
						if (item_INFO.itemCount <= 0U)
						{
							list.Add(item_INFO);
						}
						else if (_ItemJsonData.DataDict.ContainsKey(item_INFO.itemId))
						{
							ITEM_INFO item_INFO2 = new ITEM_INFO();
							item_INFO2.itemId = item_INFO.itemId;
							item_INFO2.itemCount = item_INFO.itemCount;
							item_INFO2.uuid = item_INFO.uuid;
							if (item_INFO.Seid != null)
							{
								item_INFO2.Seid = item_INFO.Seid.Copy();
							}
							this.TempBagList.Add(item_INFO2);
						}
					}
					goto IL_1F0;
				}
			}
			JSONObject jsonobject = jsonData.instance.AvatarBackpackJsonData[this.NpcId.ToString()]["Backpack"];
			if (jsonobject.Count < 1)
			{
				return;
			}
			foreach (JSONObject jsonobject2 in jsonobject.list)
			{
				if (jsonobject2["Num"].I > 0 && _ItemJsonData.DataDict.ContainsKey(jsonobject2["ItemID"].I))
				{
					ITEM_INFO item_INFO3 = new ITEM_INFO();
					item_INFO3.itemId = jsonobject2["ItemID"].I;
					item_INFO3.itemCount = (uint)jsonobject2["Num"].I;
					item_INFO3.uuid = jsonobject2["UUID"].Str;
					if (jsonobject2.HasField("Seid"))
					{
						item_INFO3.Seid = jsonobject2["Seid"].Copy();
					}
					this.TempBagList.Add(item_INFO3);
				}
			}
			IL_1F0:
			foreach (ITEM_INFO item in list)
			{
				this._player.itemList.values.Remove(item);
			}
		}

		// Token: 0x0600446C RID: 17516 RVA: 0x001D228C File Offset: 0x001D048C
		public virtual void UpdateItem(bool flag = false)
		{
			this.ItemList = new List<ITEM_INFO>();
			foreach (ITEM_INFO item_INFO in this.TempBagList)
			{
				BaseItem baseItem = BaseItem.Create(item_INFO.itemId, (int)item_INFO.itemCount, item_INFO.uuid, item_INFO.Seid);
				if (this.FiddlerItem(baseItem))
				{
					this.ItemList.Add(item_INFO);
				}
			}
			this.MItemTotalCount = this.ItemList.Count;
			this.MLoopListView.SetListItemCount(this.GetCount(this.MItemTotalCount), flag);
			this.MLoopListView.RefreshAllShownItem();
		}

		// Token: 0x0600446D RID: 17517 RVA: 0x001D234C File Offset: 0x001D054C
		public void UpdateMoney()
		{
			if (this.IsPlayer)
			{
				this.Money.SetText(Tools.instance.getPlayer().money);
				return;
			}
			this.Money.SetText(jsonData.instance.AvatarBackpackJsonData[this.NpcId.ToString()]["money"].I);
		}

		// Token: 0x0600446E RID: 17518 RVA: 0x001D23BC File Offset: 0x001D05BC
		public int GetMoney()
		{
			if (this.IsPlayer)
			{
				return (int)Tools.instance.getPlayer().money;
			}
			return jsonData.instance.AvatarBackpackJsonData[this.NpcId.ToString()]["money"].I;
		}

		// Token: 0x0600446F RID: 17519 RVA: 0x001D240C File Offset: 0x001D060C
		public void InitLeftFilter()
		{
			if (this.LeftList.Count < 1)
			{
				return;
			}
			using (List<BaseFilterLeft>.Enumerator enumerator = this.LeftList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					BaseFilterLeft filterLeft = enumerator.Current;
					filterLeft.Add(delegate
					{
						if (!filterLeft.IsSelect)
						{
							this.UpdateLeftFilter(filterLeft);
						}
					});
				}
			}
		}

		// Token: 0x06004470 RID: 17520 RVA: 0x001D2490 File Offset: 0x001D0690
		private void UpdateLeftFilter(BaseFilterLeft filterLeft)
		{
			foreach (BaseFilterLeft baseFilterLeft in this.LeftList)
			{
				if (baseFilterLeft == filterLeft)
				{
					baseFilterLeft.IsSelect = true;
					this.ItemType = baseFilterLeft.ItemType;
					this.UpdateItem(false);
					this.UpdateTopFilter();
				}
				else
				{
					baseFilterLeft.IsSelect = false;
				}
				baseFilterLeft.UpdateState();
			}
		}

		// Token: 0x06004471 RID: 17521 RVA: 0x001D2514 File Offset: 0x001D0714
		public void CloseAllTopFilter()
		{
			foreach (JiaoYiFilterTop jiaoYiFilterTop in this.TopList)
			{
				jiaoYiFilterTop.Select.SetActive(false);
				jiaoYiFilterTop.Unselect.gameObject.SetActive(true);
			}
		}

		// Token: 0x06004472 RID: 17522 RVA: 0x001D257C File Offset: 0x001D077C
		public virtual void UpdateTopFilter()
		{
			this.OpenQuality();
			this.CloseType();
			this.CloseShuXing();
			if (this.ItemType == ItemType.法宝)
			{
				this.OpenType();
				return;
			}
			if (this.ItemType == ItemType.材料)
			{
				this.OpenType();
				this.OpenShuXing();
				return;
			}
			if (this.ItemType == ItemType.秘籍)
			{
				this.OpenType();
			}
		}

		// Token: 0x06004473 RID: 17523 RVA: 0x001D25D0 File Offset: 0x001D07D0
		public void OpenQuality()
		{
			this.TopList[0].Clear();
			string title = this.ItemQuality.ToString();
			this.TopList[0].Init(this, FilterType.品阶, title);
		}

		// Token: 0x06004474 RID: 17524 RVA: 0x001D2614 File Offset: 0x001D0814
		public virtual void OpenType()
		{
			this.TopList[1].Clear();
			if (this.ItemType == ItemType.材料)
			{
				string title = this.LianQiCaiLiaoYinYang.ToString();
				this.JiaoYiSkillType = JiaoYiSkillType.全部;
				this.EquipType = EquipType.全部;
				this.TopList[1].Init(this, FilterType.类型, title);
				return;
			}
			if (this.ItemType == ItemType.法宝)
			{
				string title = this.EquipType.ToString();
				this.LianQiCaiLiaoYinYang = LianQiCaiLiaoYinYang.全部;
				this.JiaoYiSkillType = JiaoYiSkillType.全部;
				this.TopList[1].Init(this, FilterType.类型, title);
				return;
			}
			if (this.ItemType == ItemType.秘籍)
			{
				this.EquipType = EquipType.全部;
				this.LianQiCaiLiaoYinYang = LianQiCaiLiaoYinYang.全部;
				string title = this.JiaoYiSkillType.ToString();
				this.TopList[1].Init(this, FilterType.类型, title);
			}
		}

		// Token: 0x06004475 RID: 17525 RVA: 0x001D26F4 File Offset: 0x001D08F4
		public void OpenShuXing()
		{
			string title = "";
			if (this.ItemType == ItemType.材料)
			{
				this.SkIllType = SkIllType.全部;
				this.StaticSkIllType = StaticSkIllType.全部;
				title = this.LianQiCaiLiaoType.ToString();
			}
			else if (this.JiaoYiSkillType == JiaoYiSkillType.功法)
			{
				this.SkIllType = SkIllType.全部;
				this.LianQiCaiLiaoType = LianQiCaiLiaoType.全部;
				title = this.StaticSkIllType.ToString();
			}
			else if (this.JiaoYiSkillType == JiaoYiSkillType.神通)
			{
				this.StaticSkIllType = StaticSkIllType.全部;
				this.LianQiCaiLiaoType = LianQiCaiLiaoType.全部;
				title = this.SkIllType.ToString();
			}
			this.TopList[2].Clear();
			this.TopList[2].Init(this, FilterType.属性, title);
		}

		// Token: 0x06004476 RID: 17526 RVA: 0x001D27AB File Offset: 0x001D09AB
		public void CloseType()
		{
			this.TopList[1].Clear();
			this.JiaoYiSkillType = JiaoYiSkillType.全部;
			this.EquipType = EquipType.全部;
			this.LianQiCaiLiaoYinYang = LianQiCaiLiaoYinYang.全部;
		}

		// Token: 0x06004477 RID: 17527 RVA: 0x001D27D3 File Offset: 0x001D09D3
		public void CloseShuXing()
		{
			this.TopList[2].Clear();
			this.SkIllType = SkIllType.全部;
			this.StaticSkIllType = StaticSkIllType.全部;
			this.LianQiCaiLiaoType = LianQiCaiLiaoType.全部;
		}

		// Token: 0x06004478 RID: 17528 RVA: 0x001D27FC File Offset: 0x001D09FC
		protected int GetCount(int itemCout)
		{
			int num = itemCout / this.mItemCountPerRow;
			if (itemCout % this.mItemCountPerRow > 0)
			{
				num++;
			}
			return num + 1;
		}

		// Token: 0x06004479 RID: 17529 RVA: 0x001D2828 File Offset: 0x001D0A28
		protected LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int rowIndex)
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
			for (int i = 0; i < this.mItemCountPerRow; i++)
			{
				int num = rowIndex * this.mItemCountPerRow + i;
				component.mItemList[i].SetAccptType(CanSlotType.全部物品);
				if (num >= this.MItemTotalCount)
				{
					component.mItemList[i].SetNull();
				}
				else
				{
					BaseItem slotData = BaseItem.Create(this.ItemList[num].itemId, (int)this.ItemList[num].itemCount, this.ItemList[num].uuid, this.ItemList[num].Seid);
					component.mItemList[i].SetSlotData(slotData);
				}
			}
			return loopListViewItem;
		}

		// Token: 0x0600447A RID: 17530 RVA: 0x001D2914 File Offset: 0x001D0B14
		protected virtual bool FiddlerItem(BaseItem baseItem)
		{
			if (this.ItemQuality != ItemQuality.全部 && baseItem.GetImgQuality() != (int)this.ItemQuality)
			{
				return false;
			}
			if (this.ItemType != ItemType.全部 && baseItem.ItemType != this.ItemType)
			{
				return false;
			}
			if (this.ItemType == ItemType.材料)
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
			else if (this.ItemType == ItemType.法宝)
			{
				EquipItem equipItem = (EquipItem)baseItem;
				if (this.EquipType != EquipType.全部 && !equipItem.EquipTypeIsEqual(this.EquipType))
				{
					return false;
				}
			}
			else if (this.ItemType == ItemType.秘籍)
			{
				JiaoYiMiJi jiaoYiMiJi = new JiaoYiMiJi();
				jiaoYiMiJi.SetItem(baseItem.Id, baseItem.Count);
				if (this.JiaoYiSkillType != JiaoYiSkillType.全部 && jiaoYiMiJi.GetJiaoYiType() != this.JiaoYiSkillType)
				{
					return false;
				}
				if (this.JiaoYiSkillType == JiaoYiSkillType.神通)
				{
					if (this.SkIllType != SkIllType.全部 && !jiaoYiMiJi.SkillTypeIsEqual((int)this.SkIllType))
					{
						return false;
					}
				}
				else if (this.JiaoYiSkillType == JiaoYiSkillType.功法 && this.StaticSkIllType != StaticSkIllType.全部 && !jiaoYiMiJi.SkillTypeIsEqual((int)this.StaticSkIllType))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600447B RID: 17531 RVA: 0x001D2A40 File Offset: 0x001D0C40
		public void RemoveTempItem(string uuid, int num)
		{
			ITEM_INFO item_INFO = null;
			foreach (ITEM_INFO item_INFO2 in this.TempBagList)
			{
				if (item_INFO2.uuid == uuid && (ulong)item_INFO2.itemCount >= (ulong)((long)num))
				{
					item_INFO2.itemCount -= (uint)num;
					if (item_INFO2.itemCount == 0U)
					{
						item_INFO = item_INFO2;
						break;
					}
					break;
				}
			}
			if (item_INFO != null)
			{
				this.TempBagList.Remove(item_INFO);
				this.ItemList.Remove(item_INFO);
				this.MItemTotalCount = this.ItemList.Count;
			}
		}

		// Token: 0x0600447C RID: 17532 RVA: 0x001D2AF0 File Offset: 0x001D0CF0
		public void AddTempItem(BaseItem baseItem, int num)
		{
			int num2 = 0;
			foreach (ITEM_INFO item_INFO in this.TempBagList)
			{
				if (item_INFO.uuid == baseItem.Uid)
				{
					item_INFO.itemCount += (uint)num;
					num2 = 1;
					break;
				}
			}
			if (num2 == 0)
			{
				ITEM_INFO item_INFO2 = new ITEM_INFO();
				item_INFO2.itemId = baseItem.Id;
				item_INFO2.itemCount = (uint)num;
				item_INFO2.uuid = baseItem.Uid;
				if (baseItem.Seid != null)
				{
					item_INFO2.Seid = baseItem.Seid.Copy();
				}
				this.TempBagList.Add(item_INFO2);
				this.ItemList.Add(item_INFO2);
				this.MItemTotalCount = this.ItemList.Count;
			}
		}

		// Token: 0x0600447D RID: 17533 RVA: 0x001D2BD0 File Offset: 0x001D0DD0
		public SlotBase GetNullBagSlot(string uuid)
		{
			foreach (LoopListViewItem2 loopListViewItem in this.MLoopListView.ItemList)
			{
				foreach (ISlot slot in loopListViewItem.GetComponent<SlotList>().mItemList)
				{
					SlotBase slotBase = (SlotBase)slot;
					if (slotBase.transform.parent.gameObject.activeSelf)
					{
						if (slotBase.IsNull())
						{
							return slotBase;
						}
						if (slotBase.Item.Uid == uuid && slotBase.Item.MaxNum > 1)
						{
							return slotBase;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x04004621 RID: 17953
		public Text Money;

		// Token: 0x04004622 RID: 17954
		public int NpcId;

		// Token: 0x04004623 RID: 17955
		public bool IsPlayer;

		// Token: 0x04004624 RID: 17956
		public List<ITEM_INFO> TempBagList = new List<ITEM_INFO>();

		// Token: 0x04004625 RID: 17957
		public List<BaseFilterLeft> LeftList;

		// Token: 0x04004626 RID: 17958
		public List<JiaoYiFilterTop> TopList;

		// Token: 0x04004627 RID: 17959
		public ItemType ItemType;

		// Token: 0x04004628 RID: 17960
		public ItemQuality ItemQuality;

		// Token: 0x04004629 RID: 17961
		public JiaoYiSkillType JiaoYiSkillType;

		// Token: 0x0400462A RID: 17962
		public SkIllType SkIllType = SkIllType.全部;

		// Token: 0x0400462B RID: 17963
		public StaticSkIllType StaticSkIllType = StaticSkIllType.全部;

		// Token: 0x0400462C RID: 17964
		public LianQiCaiLiaoYinYang LianQiCaiLiaoYinYang;

		// Token: 0x0400462D RID: 17965
		public LianQiCaiLiaoType LianQiCaiLiaoType;

		// Token: 0x0400462E RID: 17966
		public EquipType EquipType;

		// Token: 0x0400462F RID: 17967
		public int MItemTotalCount;

		// Token: 0x04004630 RID: 17968
		public List<ITEM_INFO> ItemList = new List<ITEM_INFO>();

		// Token: 0x04004631 RID: 17969
		public LoopListView2 MLoopListView;

		// Token: 0x04004632 RID: 17970
		public int mItemCountPerRow = 3;

		// Token: 0x04004633 RID: 17971
		protected Avatar _player;
	}
}
