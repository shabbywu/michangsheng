using System;
using KBEngine;
using SuperScrollView;

namespace Bag
{
	// Token: 0x020009C2 RID: 2498
	public class LianQiBag : BaseBag2, IESCClose
	{
		// Token: 0x0600457E RID: 17790 RVA: 0x001D785B File Offset: 0x001D5A5B
		public void Open()
		{
			if (!this._init)
			{
				this.Init(1, true);
				this._init = true;
			}
			ESCCloseManager.Inst.RegisterClose(this);
			this.UpdateItem(false);
			base.gameObject.SetActive(true);
		}

		// Token: 0x0600457F RID: 17791 RVA: 0x001D7892 File Offset: 0x001D5A92
		public void Close()
		{
			LianQiTotalManager.inst.ToSlot = null;
			ESCCloseManager.Inst.UnRegisterClose(this);
			base.gameObject.SetActive(false);
		}

		// Token: 0x06004580 RID: 17792 RVA: 0x001D78B8 File Offset: 0x001D5AB8
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

		// Token: 0x06004581 RID: 17793 RVA: 0x001D792C File Offset: 0x001D5B2C
		public override void Init(int npcId, bool isPlayer = false)
		{
			this._player = Tools.instance.getPlayer();
			this.NpcId = npcId;
			this.IsPlayer = isPlayer;
			this.CreateTempList();
			this.MLoopListView.InitListView(base.GetCount(this.MItemTotalCount), new Func<LoopListView2, int, LoopListViewItem2>(base.OnGetItemByIndex), null);
			this.ItemType = ItemType.材料;
			this.ItemQuality = ItemQuality.全部;
			this.LianQiCaiLiaoYinYang = LianQiCaiLiaoYinYang.全部;
			this.LianQiCaiLiaoType = LianQiCaiLiaoType.全部;
			this.UpdateTopFilter();
			this.UpdateItem(false);
		}

		// Token: 0x06004582 RID: 17794 RVA: 0x001D79AA File Offset: 0x001D5BAA
		public override void UpdateTopFilter()
		{
			base.OpenQuality();
			this.OpenType();
			base.OpenShuXing();
		}

		// Token: 0x06004583 RID: 17795 RVA: 0x001D79BE File Offset: 0x001D5BBE
		public bool TryEscClose()
		{
			this.Close();
			return true;
		}

		// Token: 0x04004708 RID: 18184
		private bool _init;
	}
}
