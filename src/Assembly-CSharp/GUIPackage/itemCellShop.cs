using System;
using KBEngine;

namespace GUIPackage
{
	// Token: 0x02000D85 RID: 3461
	public class itemCellShop : ItemCell
	{
		// Token: 0x06005376 RID: 21366 RVA: 0x000042DD File Offset: 0x000024DD
		private void Start()
		{
		}

		// Token: 0x06005377 RID: 21367 RVA: 0x00004050 File Offset: 0x00002250
		public override int getItemPrice()
		{
			return 0;
		}

		// Token: 0x06005378 RID: 21368 RVA: 0x0003BB91 File Offset: 0x00039D91
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

		// Token: 0x06005379 RID: 21369 RVA: 0x0022C6C8 File Offset: 0x0022A8C8
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

		// Token: 0x0600537A RID: 21370 RVA: 0x0022C728 File Offset: 0x0022A928
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

		// Token: 0x0400533C RID: 21308
		public UILabel ItemName;

		// Token: 0x0400533D RID: 21309
		public UILabel ItemMoney;

		// Token: 0x0400533E RID: 21310
		public int pricePercent;
	}
}
