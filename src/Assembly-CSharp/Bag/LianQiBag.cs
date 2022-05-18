using System;
using KBEngine;
using SuperScrollView;

namespace Bag
{
	// Token: 0x02000D4B RID: 3403
	public class LianQiBag : BaseBag2, IESCClose
	{
		// Token: 0x060050E3 RID: 20707 RVA: 0x0003A371 File Offset: 0x00038571
		public void Open()
		{
			if (!this._init)
			{
				this.Init(1, true);
				this._init = true;
			}
			ESCCloseManager.Inst.RegisterClose(this);
			base.UpdateItem(false);
			base.gameObject.SetActive(true);
		}

		// Token: 0x060050E4 RID: 20708 RVA: 0x0003A3A8 File Offset: 0x000385A8
		public void Close()
		{
			LianQiTotalManager.inst.ToSlot = null;
			ESCCloseManager.Inst.UnRegisterClose(this);
			base.gameObject.SetActive(false);
		}

		// Token: 0x060050E5 RID: 20709 RVA: 0x0021B760 File Offset: 0x00219960
		public BaseItem GetItem(int id)
		{
			foreach (ITEM_INFO item_INFO in this.TempBagList)
			{
				if (item_INFO.itemId == id)
				{
					return BaseItem.Create(id, (int)item_INFO.itemCount, item_INFO.uuid, item_INFO.Seid);
				}
			}
			return null;
		}

		// Token: 0x060050E6 RID: 20710 RVA: 0x0021B7D4 File Offset: 0x002199D4
		public override void Init(int npcId, bool isPlayer = false)
		{
			this._player = Tools.instance.getPlayer();
			this.NpcId = npcId;
			this.IsPlayer = isPlayer;
			base.CreateTempList();
			this.MLoopListView.InitListView(base.GetCount(this.MItemTotalCount), new Func<LoopListView2, int, LoopListViewItem2>(base.OnGetItemByIndex), null);
			this.ItemType = ItemType.材料;
			this.ItemQuality = ItemQuality.全部;
			this.LianQiCaiLiaoYinYang = LianQiCaiLiaoYinYang.全部;
			this.LianQiCaiLiaoType = LianQiCaiLiaoType.全部;
			this.UpdateTopFilter();
			base.UpdateItem(false);
		}

		// Token: 0x060050E7 RID: 20711 RVA: 0x0003A3CC File Offset: 0x000385CC
		public override void UpdateTopFilter()
		{
			base.OpenQuality();
			base.OpenType();
			base.OpenShuXing();
		}

		// Token: 0x060050E8 RID: 20712 RVA: 0x0003A3E0 File Offset: 0x000385E0
		public bool TryEscClose()
		{
			this.Close();
			return true;
		}

		// Token: 0x0400520C RID: 21004
		private bool _init;
	}
}
