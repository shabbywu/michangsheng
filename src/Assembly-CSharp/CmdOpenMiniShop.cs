using Fungus;
using UnityEngine;

[CommandInfo("YSTool", "打开迷你商店", "打开迷你商店", 0)]
[AddComponentMenu("")]
public class CmdOpenMiniShop : Command, INoCommand
{
	[Tooltip("商品ID")]
	[SerializeField]
	protected int ItemID;

	[Tooltip("商品价格，如果填0则使用物品原价")]
	[SerializeField]
	protected int ItemPrice;

	[Tooltip("最大售卖数量，如果填0则不限制")]
	[SerializeField]
	protected int MaxSellCount;

	public override void OnEnter()
	{
		UIMiniShop.Show(ItemID, ItemPrice, MaxSellCount, this);
	}
}
