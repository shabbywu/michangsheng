using System;
using Fungus;
using UnityEngine;

// Token: 0x020002DD RID: 733
[CommandInfo("YSTool", "打开迷你商店", "打开迷你商店", 0)]
[AddComponentMenu("")]
public class CmdOpenMiniShop : Command, INoCommand
{
	// Token: 0x06001977 RID: 6519 RVA: 0x000B6325 File Offset: 0x000B4525
	public override void OnEnter()
	{
		UIMiniShop.Show(this.ItemID, this.ItemPrice, this.MaxSellCount, this);
	}

	// Token: 0x040014A6 RID: 5286
	[Tooltip("商品ID")]
	[SerializeField]
	protected int ItemID;

	// Token: 0x040014A7 RID: 5287
	[Tooltip("商品价格，如果填0则使用物品原价")]
	[SerializeField]
	protected int ItemPrice;

	// Token: 0x040014A8 RID: 5288
	[Tooltip("最大售卖数量，如果填0则不限制")]
	[SerializeField]
	protected int MaxSellCount;
}
