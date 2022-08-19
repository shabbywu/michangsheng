using System;
using System.Collections.Generic;
using Bag;
using SuperScrollView;

namespace JiaoYi
{
	// Token: 0x0200072F RID: 1839
	public class JiaoBag : BaseBag2
	{
		// Token: 0x06003A9A RID: 15002 RVA: 0x00192D74 File Offset: 0x00190F74
		public override void Init(int npcId, bool isPlayer = false)
		{
			this._player = Tools.instance.getPlayer();
			this.ItemType = Bag.ItemType.丹药;
			this.NpcId = npcId;
			this.IsPlayer = isPlayer;
			this.CreateTempList();
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

		// Token: 0x06003A9B RID: 15003 RVA: 0x00192E50 File Offset: 0x00191050
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

		// Token: 0x06003A9C RID: 15004 RVA: 0x00192ED4 File Offset: 0x001910D4
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

		// Token: 0x06003A9D RID: 15005 RVA: 0x00192F54 File Offset: 0x00191154
		public void JiaoYiCallBack()
		{
			foreach (JiaoYiSlot jiaoYiSlot in this.SellList)
			{
				jiaoYiSlot.SetNull();
			}
			base.UpdateMoney();
			this.CreateTempList();
			this.UpdateItem(false);
		}

		// Token: 0x040032CF RID: 13007
		public List<JiaoYiSlot> SellList;
	}
}
