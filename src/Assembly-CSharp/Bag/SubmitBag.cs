using System;
using script.Submit;
using SuperScrollView;

namespace Bag
{
	// Token: 0x02000D4C RID: 3404
	public class SubmitBag : BaseBag2
	{
		// Token: 0x060050EA RID: 20714 RVA: 0x0003A3E9 File Offset: 0x000385E9
		public void Open()
		{
			this.Init(0, true);
			base.UpdateItem(true);
		}

		// Token: 0x060050EB RID: 20715 RVA: 0x0021B854 File Offset: 0x00219A54
		public override void Init(int npcId, bool isPlayer = false)
		{
			this._player = Tools.instance.getPlayer();
			this.NpcId = npcId;
			this.IsPlayer = isPlayer;
			this.MLoopListView.InitListView(base.GetCount(this.MItemTotalCount), new Func<LoopListView2, int, LoopListViewItem2>(base.OnGetItemByIndex), null);
			base.CreateTempList();
		}

		// Token: 0x060050EC RID: 20716 RVA: 0x0003A3FA File Offset: 0x000385FA
		protected override bool FiddlerItem(BaseItem baseItem)
		{
			return base.FiddlerItem(baseItem) && SubmitUIMag.Inst.CanPut(baseItem);
		}
	}
}
