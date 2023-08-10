using KBEngine;
using UnityEngine;

namespace GUIPackage;

public class itemcellShopEx : itemCellShop
{
	public UITexture EXGoodsIcon;

	public override void MobilePress()
	{
		base.MobilePress();
	}

	public override void PCOnPress()
	{
		inventory.showTooltip = false;
		Item = inventory.inventory[int.Parse(((Object)this).name)];
		if (Item.itemID <= 0)
		{
			return;
		}
		EXsetUI_chifen();
		selectNum.instence.setChoice(new EventDelegate(delegate
		{
			Avatar player = Tools.instance.getPlayer();
			int num = int.Parse(((Component)selectNum.instence).gameObject.GetComponent<UI_chaifen>().inputNum.value);
			if (Item.ExGoodsID == 10035)
			{
				if (player.money < (ulong)(Item.itemPrice * num))
				{
					UIPopTip.Inst.Pop("灵石不足");
					return;
				}
				player.money -= (ulong)(Item.itemPrice * num);
			}
			else
			{
				if (player.getItemNum(Item.ExGoodsID) < Item.itemPrice * num)
				{
					UIPopTip.Inst.Pop("物品不足");
					return;
				}
				player.removeItem(Item.ExGoodsID, Item.itemPrice * num);
			}
			player.addItem(Item.itemID, num, Item.Seid, ShowText: true);
		}), null, "选择兑换数量");
	}

	public void EXsetUI_chifen()
	{
		((Component)selectNum.instence).gameObject.GetComponent<UI_chaifen>().Item = inventory.inventory[int.Parse(((Object)this).name)].Clone();
		((Component)selectNum.instence).gameObject.GetComponent<UI_chaifen>().Item.itemNum = 9999;
		((Component)selectNum.instence).gameObject.GetComponent<UI_chaifen>().inputNum.value = string.Concat(1);
	}

	private void Update()
	{
		Icon.GetComponent<UITexture>().mainTexture = (Texture)(object)inventory.inventory[int.Parse(((Object)this).name)].itemIcon;
		ItemMoney.text = string.Concat(inventory.inventory[int.Parse(((Object)this).name)].itemPrice);
		ItemName.text = inventory.inventory[int.Parse(((Object)this).name)].itemNameCN;
		PingZhi.GetComponent<UITexture>().mainTexture = (Texture)(object)inventory.inventory[int.Parse(((Object)this).name)].itemPingZhi;
		EXGoodsIcon.mainTexture = (Texture)(object)inventory.inventory[int.Parse(((Object)this).name)].ExItemIcon;
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
