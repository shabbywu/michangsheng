using System;
using KBEngine;

namespace GUIPackage
{
	// Token: 0x02000A5E RID: 2654
	public class itemcellShopEx : itemCellShop
	{
		// Token: 0x06004A7E RID: 19070 RVA: 0x001FA4AD File Offset: 0x001F86AD
		public override void MobilePress()
		{
			base.MobilePress();
		}

		// Token: 0x06004A7F RID: 19071 RVA: 0x001FA4B8 File Offset: 0x001F86B8
		public override void PCOnPress()
		{
			this.inventory.showTooltip = false;
			this.Item = this.inventory.inventory[int.Parse(base.name)];
			if (this.Item.itemID > 0)
			{
				this.EXsetUI_chifen();
				selectNum.instence.setChoice(new EventDelegate(delegate()
				{
					Avatar player = Tools.instance.getPlayer();
					int num = int.Parse(selectNum.instence.gameObject.GetComponent<UI_chaifen>().inputNum.value);
					if (this.Item.ExGoodsID == 10035)
					{
						if (player.money < (ulong)((long)(this.Item.itemPrice * num)))
						{
							UIPopTip.Inst.Pop("灵石不足", PopTipIconType.叹号);
							return;
						}
						player.money -= (ulong)((long)(this.Item.itemPrice * num));
					}
					else
					{
						if (player.getItemNum(this.Item.ExGoodsID) < this.Item.itemPrice * num)
						{
							UIPopTip.Inst.Pop("物品不足", PopTipIconType.叹号);
							return;
						}
						player.removeItem(this.Item.ExGoodsID, this.Item.itemPrice * num);
					}
					player.addItem(this.Item.itemID, num, this.Item.Seid, true);
				}), null, "选择兑换数量");
			}
		}

		// Token: 0x06004A80 RID: 19072 RVA: 0x001FA528 File Offset: 0x001F8728
		public void EXsetUI_chifen()
		{
			selectNum.instence.gameObject.GetComponent<UI_chaifen>().Item = this.inventory.inventory[int.Parse(base.name)].Clone();
			selectNum.instence.gameObject.GetComponent<UI_chaifen>().Item.itemNum = 9999;
			selectNum.instence.gameObject.GetComponent<UI_chaifen>().inputNum.value = string.Concat(1);
		}

		// Token: 0x06004A81 RID: 19073 RVA: 0x001FA5AC File Offset: 0x001F87AC
		private void Update()
		{
			this.Icon.GetComponent<UITexture>().mainTexture = this.inventory.inventory[int.Parse(base.name)].itemIcon;
			this.ItemMoney.text = string.Concat(this.inventory.inventory[int.Parse(base.name)].itemPrice);
			this.ItemName.text = this.inventory.inventory[int.Parse(base.name)].itemNameCN;
			this.PingZhi.GetComponent<UITexture>().mainTexture = this.inventory.inventory[int.Parse(base.name)].itemPingZhi;
			this.EXGoodsIcon.mainTexture = this.inventory.inventory[int.Parse(base.name)].ExItemIcon;
			this.Item = this.inventory.inventory[int.Parse(base.name)];
			if (this.inventory.inventory[int.Parse(base.name)].itemNum > 1)
			{
				this.Num.GetComponent<UILabel>().text = this.inventory.inventory[int.Parse(base.name)].itemNum.ToString();
			}
			else
			{
				this.Num.GetComponent<UILabel>().text = "";
			}
			base.showYiWu();
		}

		// Token: 0x040049B0 RID: 18864
		public UITexture EXGoodsIcon;
	}
}
