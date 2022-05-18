using System;
using System.Collections.Generic;
using Bag;
using SuperScrollView;

namespace JiaoYi
{
	// Token: 0x02000A8F RID: 2703
	public class JiaoBag : BaseBag2
	{
		// Token: 0x06004554 RID: 17748 RVA: 0x001DA8A4 File Offset: 0x001D8AA4
		public override void Init(int npcId, bool isPlayer = false)
		{
			this._player = Tools.instance.getPlayer();
			this.ItemType = Bag.ItemType.丹药;
			this.NpcId = npcId;
			this.IsPlayer = isPlayer;
			base.CreateTempList();
			this.MLoopListView.InitListView(base.GetCount(this.MItemTotalCount), new Func<LoopListView2, int, LoopListViewItem2>(base.OnGetItemByIndex), null);
			this.ItemType = Bag.ItemType.全部;
			this.ItemQuality = ItemQuality.全部;
			this.JiaoYiSkillType = JiaoYiSkillType.全部;
			this.LianQiCaiLiaoYinYang = LianQiCaiLiaoYinYang.全部;
			this.LianQiCaiLiaoType = LianQiCaiLiaoType.全部;
			base.InitLeftFilter();
			foreach (JiaoYiSlot jiaoYiSlot in this.SellList)
			{
				jiaoYiSlot.SetNull();
			}
			this.UpdateLeftFilter(this.LeftList[0]);
		}

		// Token: 0x06004555 RID: 17749 RVA: 0x001DA980 File Offset: 0x001D8B80
		private void UpdateLeftFilter(BaseFilterLeft filterLeft)
		{
			foreach (BaseFilterLeft baseFilterLeft in this.LeftList)
			{
				if (baseFilterLeft == filterLeft)
				{
					baseFilterLeft.IsSelect = true;
					this.ItemType = baseFilterLeft.ItemType;
					base.UpdateItem(false);
					this.UpdateTopFilter();
				}
				else
				{
					baseFilterLeft.IsSelect = false;
				}
				baseFilterLeft.UpdateState();
			}
		}

		// Token: 0x06004556 RID: 17750 RVA: 0x001DAA04 File Offset: 0x001D8C04
		public JiaoYiSlot GetNullSellList(string uuid)
		{
			foreach (JiaoYiSlot jiaoYiSlot in this.SellList)
			{
				if (jiaoYiSlot.IsNull())
				{
					return jiaoYiSlot;
				}
				if (jiaoYiSlot.Item.Uid == uuid && jiaoYiSlot.Item.MaxNum > 1)
				{
					return jiaoYiSlot;
				}
			}
			return null;
		}

		// Token: 0x06004557 RID: 17751 RVA: 0x001DAA84 File Offset: 0x001D8C84
		public void JiaoYiCallBack()
		{
			foreach (JiaoYiSlot jiaoYiSlot in this.SellList)
			{
				jiaoYiSlot.SetNull();
			}
			base.UpdateMoney();
			base.CreateTempList();
			base.UpdateItem(false);
		}

		// Token: 0x04003D80 RID: 15744
		public List<JiaoYiSlot> SellList;
	}
}
