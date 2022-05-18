using System;
using Fungus;
using UnityEngine;

// Token: 0x0200042F RID: 1071
[CommandInfo("YSTool", "打开迷你商店", "打开迷你商店", 0)]
[AddComponentMenu("")]
public class CmdOpenMiniShop : Command, INoCommand
{
	// Token: 0x06001C8D RID: 7309 RVA: 0x00017DA1 File Offset: 0x00015FA1
	public override void OnEnter()
	{
		UIMiniShop.Show(this.ItemID, this.ItemPrice, this.MaxSellCount, this);
	}

	// Token: 0x04001886 RID: 6278
	[Tooltip("商品ID")]
	[SerializeField]
	protected int ItemID;

	// Token: 0x04001887 RID: 6279
	[Tooltip("商品价格，如果填0则使用物品原价")]
	[SerializeField]
	protected int ItemPrice;

	// Token: 0x04001888 RID: 6280
	[Tooltip("最大售卖数量，如果填0则不限制")]
	[SerializeField]
	protected int MaxSellCount;
}
