using KBEngine;
using UnityEngine;
using UnityEngine.Events;

namespace GUIPackage;

public class itemCellShop : ItemCell
{
	public UILabel ItemName;

	public UILabel ItemMoney;

	public int pricePercent;

	private void Start()
	{
	}

	public override int getItemPrice()
	{
		return 0;
	}

	public override void MobilePress()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		base.MobilePress();
		TooltipsBackgroundi toolTipsBackGround = Singleton.ToolTipsBackGround;
		toolTipsBackGround.UseAction = (UnityAction)delegate
		{
			PCOnPress();
		};
		toolTipsBackGround.SetBtnText("购买");
	}

	public override void PCOnPress()
	{
		Item = inventory.inventory[int.Parse(((Object)this).name)];
		if (Item.itemID <= 0)
		{
			return;
		}
		selectBox.instence.setChoice("是否购买该物品", new EventDelegate(delegate
		{
			Avatar player = Tools.instance.getPlayer();
			if (player.money < (ulong)Item.itemPrice)
			{
				UIPopTip.Inst.Pop("灵石不足");
			}
			else
			{
				player.money -= (ulong)Item.itemPrice;
				player.addItem(Item.itemID, 1, Tools.CreateItemSeid(Item.itemID), ShowText: true);
			}
		}), null);
	}

	private void Update()
	{
		Icon.GetComponent<UITexture>().mainTexture = (Texture)(object)inventory.inventory[int.Parse(((Object)this).name)].itemIcon;
		ItemMoney.text = string.Concat(inventory.inventory[int.Parse(((Object)this).name)].itemPrice);
		ItemName.text = inventory.inventory[int.Parse(((Object)this).name)].itemNameCN;
		PingZhi.GetComponent<UITexture>().mainTexture = (Texture)(object)inventory.inventory[int.Parse(((Object)this).name)].itemPingZhi;
		Item = inventory.inventory[int.Parse(((Object)this).name)];
		if (inventory.inventory[int.Parse(((Object)this).name)].itemNum > 1)
		{
			Num.GetComponent<UILabel>().text = inventory.inventory[int.Parse(((Object)this).name)].itemNum.ToString();
		}
		else
		{
			Num.GetComponent<UILabel>().text = "";
		}
		showYiWu();
	}
}
