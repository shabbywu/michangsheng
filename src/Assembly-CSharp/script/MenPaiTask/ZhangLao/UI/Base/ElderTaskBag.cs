using System;
using Bag;
using KBEngine;
using SuperScrollView;
using UnityEngine;

namespace script.MenPaiTask.ZhangLao.UI.Base
{
	// Token: 0x02000A12 RID: 2578
	public class ElderTaskBag : BaseBag2, IESCClose
	{
		// Token: 0x0600475E RID: 18270 RVA: 0x001E3598 File Offset: 0x001E1798
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
			this.UpdateLeftFilter(this.LeftList[0]);
		}

		// Token: 0x0600475F RID: 18271 RVA: 0x001E3630 File Offset: 0x001E1830
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

		// Token: 0x06004760 RID: 18272 RVA: 0x001E36B4 File Offset: 0x001E18B4
		public override void CreateTempList()
		{
			foreach (int num in this._player.ElderTaskMag.GetCanNeedItemList())
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

		// Token: 0x06004761 RID: 18273 RVA: 0x001E376C File Offset: 0x001E196C
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

		// Token: 0x06004762 RID: 18274 RVA: 0x000D5AD3 File Offset: 0x000D3CD3
		public void Hide()
		{
			ESCCloseManager.Inst.UnRegisterClose(this);
			base.gameObject.SetActive(false);
		}

		// Token: 0x06004763 RID: 18275 RVA: 0x001E37A3 File Offset: 0x001E19A3
		public bool TryEscClose()
		{
			this.Hide();
			return true;
		}

		// Token: 0x0400487D RID: 18557
		private bool _init;

		// Token: 0x0400487E RID: 18558
		public ElderTaskSlot ToSlot;
	}
}
