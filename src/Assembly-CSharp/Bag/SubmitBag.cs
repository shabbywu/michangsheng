using System;
using script.Submit;
using SuperScrollView;

namespace Bag
{
	// Token: 0x020009C3 RID: 2499
	public class SubmitBag : BaseBag2
	{
		// Token: 0x06004585 RID: 17797 RVA: 0x001D79C7 File Offset: 0x001D5BC7
		public void Open()
		{
			this.Init(0, true);
			this.UpdateItem(true);
		}

		// Token: 0x06004586 RID: 17798 RVA: 0x001D79D8 File Offset: 0x001D5BD8
		public override void Init(int npcId, bool isPlayer = false)
		{
			this._player = Tools.instance.getPlayer();
			this.NpcId = npcId;
			this.IsPlayer = isPlayer;
			this.MLoopListView.InitListView(base.GetCount(this.MItemTotalCount), new Func<LoopListView2, int, LoopListViewItem2>(base.OnGetItemByIndex), null);
			this.CreateTempList();
		}

		// Token: 0x06004587 RID: 17799 RVA: 0x001D7A2D File Offset: 0x001D5C2D
		protected override bool FiddlerItem(BaseItem baseItem)
		{
			return base.FiddlerItem(baseItem) && SubmitUIMag.Inst.CanPut(baseItem);
		}
	}
}
