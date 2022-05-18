using System;
using System.Collections.Generic;
using JiaoYi;
using KBEngine;
using SuperScrollView;
using UnityEngine;
using UnityEngine.UI;

namespace Bag
{
	// Token: 0x02000D16 RID: 3350
	public class BaseBag2 : MonoBehaviour
	{
		// Token: 0x06004FC0 RID: 20416 RVA: 0x00216CD4 File Offset: 0x00214ED4
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

		// Token: 0x06004FC1 RID: 20417 RVA: 0x00216D64 File Offset: 0x00214F64
		public void CreateTempList()
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
						else
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
					goto IL_1BB;
				}
			}
			JSONObject jsonobject = jsonData.instance.AvatarBackpackJsonData[this.NpcId.ToString()]["Backpack"];
			if (jsonobject.Count < 1)
			{
				return;
			}
			foreach (JSONObject jsonobject2 in jsonobject.list)
			{
				if (jsonobject2["Num"].I > 0)
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
			IL_1BB:
			foreach (ITEM_INFO item in list)
			{
				this._player.itemList.values.Remove(item);
			}
		}

		// Token: 0x06004FC2 RID: 20418 RVA: 0x00216F98 File Offset: 0x00215198
		public void UpdateItem(bool flag = false)
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

		// Token: 0x06004FC3 RID: 20419 RVA: 0x00217058 File Offset: 0x00215258
		public void UpdateMoney()
		{
			if (this.IsPlayer)
			{
				this.Money.SetText(Tools.instance.getPlayer().money);
				return;
			}
			this.Money.SetText(jsonData.instance.AvatarBackpackJsonData[this.NpcId.ToString()]["money"].I);
		}

		// Token: 0x06004FC4 RID: 20420 RVA: 0x002170C8 File Offset: 0x002152C8
		public int GetMoney()
		{
			if (this.IsPlayer)
			{
				return (int)Tools.instance.getPlayer().money;
			}
			return jsonData.instance.AvatarBackpackJsonData[this.NpcId.ToString()]["money"].I;
		}

		// Token: 0x06004FC5 RID: 20421 RVA: 0x00217118 File Offset: 0x00215318
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

		// Token: 0x06004FC6 RID: 20422 RVA: 0x001DA980 File Offset: 0x001D8B80
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

		// Token: 0x06004FC7 RID: 20423 RVA: 0x0021719C File Offset: 0x0021539C
		public void CloseAllTopFilter()
		{
			foreach (JiaoYiFilterTop jiaoYiFilterTop in this.TopList)
			{
				jiaoYiFilterTop.Select.SetActive(false);
				jiaoYiFilterTop.Unselect.gameObject.SetActive(true);
			}
		}

		// Token: 0x06004FC8 RID: 20424 RVA: 0x00217204 File Offset: 0x00215404
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

		// Token: 0x06004FC9 RID: 20425 RVA: 0x00217258 File Offset: 0x00215458
		public void OpenQuality()
		{
			this.TopList[0].Clear();
			string title = this.ItemQuality.ToString();
			this.TopList[0].Init(this, FilterType.品阶, title);
		}

		// Token: 0x06004FCA RID: 20426 RVA: 0x0021729C File Offset: 0x0021549C
		public void OpenType()
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

		// Token: 0x06004FCB RID: 20427 RVA: 0x0021737C File Offset: 0x0021557C
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

		// Token: 0x06004FCC RID: 20428 RVA: 0x000396CD File Offset: 0x000378CD
		public void CloseType()
		{
			this.TopList[1].Clear();
			this.JiaoYiSkillType = JiaoYiSkillType.全部;
			this.EquipType = EquipType.全部;
			this.LianQiCaiLiaoYinYang = LianQiCaiLiaoYinYang.全部;
		}

		// Token: 0x06004FCD RID: 20429 RVA: 0x000396F5 File Offset: 0x000378F5
		public void CloseShuXing()
		{
			this.TopList[2].Clear();
			this.SkIllType = SkIllType.全部;
			this.StaticSkIllType = StaticSkIllType.全部;
			this.LianQiCaiLiaoType = LianQiCaiLiaoType.全部;
		}

		// Token: 0x06004FCE RID: 20430 RVA: 0x00217434 File Offset: 0x00215634
		protected int GetCount(int itemCout)
		{
			int num = itemCout / this.mItemCountPerRow;
			if (itemCout % this.mItemCountPerRow > 0)
			{
				num++;
			}
			return num + 1;
		}

		// Token: 0x06004FCF RID: 20431 RVA: 0x00217460 File Offset: 0x00215660
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

		// Token: 0x06004FD0 RID: 20432 RVA: 0x0021754C File Offset: 0x0021574C
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

		// Token: 0x06004FD1 RID: 20433 RVA: 0x00217678 File Offset: 0x00215878
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

		// Token: 0x06004FD2 RID: 20434 RVA: 0x00217728 File Offset: 0x00215928
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

		// Token: 0x06004FD3 RID: 20435 RVA: 0x00217808 File Offset: 0x00215A08
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

		// Token: 0x04005115 RID: 20757
		public Text Money;

		// Token: 0x04005116 RID: 20758
		public int NpcId;

		// Token: 0x04005117 RID: 20759
		public bool IsPlayer;

		// Token: 0x04005118 RID: 20760
		public List<ITEM_INFO> TempBagList = new List<ITEM_INFO>();

		// Token: 0x04005119 RID: 20761
		public List<BaseFilterLeft> LeftList;

		// Token: 0x0400511A RID: 20762
		public List<JiaoYiFilterTop> TopList;

		// Token: 0x0400511B RID: 20763
		public ItemType ItemType;

		// Token: 0x0400511C RID: 20764
		public ItemQuality ItemQuality;

		// Token: 0x0400511D RID: 20765
		public JiaoYiSkillType JiaoYiSkillType;

		// Token: 0x0400511E RID: 20766
		public SkIllType SkIllType = SkIllType.全部;

		// Token: 0x0400511F RID: 20767
		public StaticSkIllType StaticSkIllType = StaticSkIllType.全部;

		// Token: 0x04005120 RID: 20768
		public LianQiCaiLiaoYinYang LianQiCaiLiaoYinYang;

		// Token: 0x04005121 RID: 20769
		public LianQiCaiLiaoType LianQiCaiLiaoType;

		// Token: 0x04005122 RID: 20770
		public EquipType EquipType;

		// Token: 0x04005123 RID: 20771
		public int MItemTotalCount;

		// Token: 0x04005124 RID: 20772
		public List<ITEM_INFO> ItemList = new List<ITEM_INFO>();

		// Token: 0x04005125 RID: 20773
		public LoopListView2 MLoopListView;

		// Token: 0x04005126 RID: 20774
		public int mItemCountPerRow = 3;

		// Token: 0x04005127 RID: 20775
		protected Avatar _player;
	}
}
