using System;
using System.Collections.Generic;
using Bag;
using JSONClass;
using KBEngine;
using script.ExchangeMeeting.UI.Interface;
using SuperScrollView;
using UnityEngine;

namespace script.ExchangeMeeting.UI.Base
{
	// Token: 0x02000A34 RID: 2612
	public class ExchangeBag : BaseBag2, IESCClose
	{
		// Token: 0x060047E0 RID: 18400 RVA: 0x001E5D48 File Offset: 0x001E3F48
		public bool TryEscClose()
		{
			this.Hide();
			return true;
		}

		// Token: 0x060047E1 RID: 18401 RVA: 0x001E5D51 File Offset: 0x001E3F51
		public void Open()
		{
			if (!this._init)
			{
				this.Init(0, false);
				this._init = true;
			}
			this.UpdateItem(false);
			ESCCloseManager.Inst.RegisterClose(this);
			base.gameObject.SetActive(true);
		}

		// Token: 0x060047E2 RID: 18402 RVA: 0x001E5D88 File Offset: 0x001E3F88
		public override void Init(int npcId, bool isPlayer = false)
		{
			this._player = Tools.instance.getPlayer();
			this.ItemType = Bag.ItemType.丹药;
			this.NpcId = npcId;
			this.CreateTempList();
			this.MLoopListView.InitListView(base.GetCount(this.MItemTotalCount), new Func<LoopListView2, int, LoopListViewItem2>(base.OnGetItemByIndex), null);
			this.ItemType = Bag.ItemType.全部;
			this.ItemQuality = ItemQuality.全部;
			this.JiaoYiSkillType = JiaoYiSkillType.全部;
			this.LianQiCaiLiaoYinYang = LianQiCaiLiaoYinYang.全部;
			this.LianQiCaiLiaoType = LianQiCaiLiaoType.全部;
			base.InitLeftFilter();
			this.UpdateLeftFilter(this.LeftList[0]);
		}

		// Token: 0x060047E3 RID: 18403 RVA: 0x001E5E18 File Offset: 0x001E4018
		public void UpdateLeftFilter(BaseFilterLeft filterLeft)
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

		// Token: 0x060047E4 RID: 18404 RVA: 0x001E5E9C File Offset: 0x001E409C
		public override void UpdateTopFilter()
		{
			if (this.TopList != null && this.TopList.Count > 0)
			{
				base.UpdateTopFilter();
			}
		}

		// Token: 0x060047E5 RID: 18405 RVA: 0x001E5EBC File Offset: 0x001E40BC
		public override void CreateTempList()
		{
			if (this.IsPlayer)
			{
				using (List<ITEM_INFO>.Enumerator enumerator = this._player.itemList.values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ITEM_INFO item_INFO = enumerator.Current;
						if (item_INFO.itemCount > 0U && _ItemJsonData.DataDict.ContainsKey(item_INFO.itemId) && _ItemJsonData.DataDict[item_INFO.itemId].CanSale != 1)
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
					return;
				}
			}
			foreach (int num in IExchangeUIMag.Inst.GetCanGetItemList())
			{
				try
				{
					ITEM_INFO item = new ITEM_INFO
					{
						itemCount = 1U,
						itemId = num,
						uuid = Tools.getUUID(),
						Seid = Tools.CreateItemSeid(num)
					};
					this.TempBagList.Add(item);
				}
				catch (Exception)
				{
					Debug.LogError("物品id:" + num + "不存在");
				}
			}
		}

		// Token: 0x060047E6 RID: 18406 RVA: 0x001E6050 File Offset: 0x001E4250
		public override void OpenType()
		{
			this.TopList[1].Clear();
			if (this.ItemType == Bag.ItemType.材料)
			{
				string title = this.LianQiCaiLiaoYinYang.ToString();
				this.JiaoYiSkillType = JiaoYiSkillType.全部;
				this.EquipType = EquipType.全部;
				this.TopList[1].Init(this, FilterType.类型, title);
			}
		}

		// Token: 0x060047E7 RID: 18407 RVA: 0x000D5AD3 File Offset: 0x000D3CD3
		public void Hide()
		{
			ESCCloseManager.Inst.UnRegisterClose(this);
			base.gameObject.SetActive(false);
		}

		// Token: 0x040048B5 RID: 18613
		protected bool _init;

		// Token: 0x040048B6 RID: 18614
		public ExchangeSlot ToSlot;
	}
}
