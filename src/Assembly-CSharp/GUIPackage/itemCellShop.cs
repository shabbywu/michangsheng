using System;
using KBEngine;

namespace GUIPackage
{
	// Token: 0x02000A5D RID: 2653
	public class itemCellShop : ItemCell
	{
		// Token: 0x06004A76 RID: 19062 RVA: 0x00004095 File Offset: 0x00002295
		private void Start()
		{
		}

		// Token: 0x06004A77 RID: 19063 RVA: 0x0000280F File Offset: 0x00000A0F
		public override int getItemPrice()
		{
			return 0;
		}

		// Token: 0x06004A78 RID: 19064 RVA: 0x001FA247 File Offset: 0x001F8447
		public override void MobilePress()
		{
			base.MobilePress();
			TooltipsBackgroundi toolTipsBackGround = Singleton.ToolTipsBackGround;
			toolTipsBackGround.UseAction = delegate()
			{
				this.PCOnPress();
			};
			toolTipsBackGround.SetBtnText("购买");
		}

		// Token: 0x06004A79 RID: 19065 RVA: 0x001FA270 File Offset: 0x001F8470
		public override void PCOnPress()
		{
			this.Item = this.inventory.inventory[int.Parse(base.name)];
			if (this.Item.itemID > 0)
			{
				selectBox.instence.setChoice("是否购买该物品", new EventDelegate(delegate()
				{
					Avatar player = Tools.instance.getPlayer();
					if (player.money < (ulong)((long)this.Item.itemPrice))
					{
						UIPopTip.Inst.Pop("灵石不足", PopTipIconType.叹号);
						return;
					}
					player.money -= (ulong)((long)this.Item.itemPrice);
					player.addItem(this.Item.itemID, 1, Tools.CreateItemSeid(this.Item.itemID), true);
				}), null);
			}
		}

		// Token: 0x06004A7A RID: 19066 RVA: 0x001FA2D0 File Offset: 0x001F84D0
		private void Update()
		{
			this.Icon.GetComponent<UITexture>().mainTexture = this.inventory.inventory[int.Parse(base.name)].itemIcon;
			this.ItemMoney.text = string.Concat(this.inventory.inventory[int.Parse(base.name)].itemPrice);
			this.ItemName.text = this.inventory.inventory[int.Parse(base.name)].itemNameCN;
			this.PingZhi.GetComponent<UITexture>().mainTexture = this.inventory.inventory[int.Parse(base.name)].itemPingZhi;
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

		// Token: 0x040049AD RID: 18861
		public UILabel ItemName;

		// Token: 0x040049AE RID: 18862
		public UILabel ItemMoney;

		// Token: 0x040049AF RID: 18863
		public int pricePercent;
	}
}
