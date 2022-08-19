using System;
using GUIPackage;

// Token: 0x02000438 RID: 1080
public class ItemCellLinWu : ItemCell
{
	// Token: 0x06002261 RID: 8801 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06002262 RID: 8802 RVA: 0x000ECB41 File Offset: 0x000EAD41
	private void OnPress()
	{
		if (this.Item.itemID == -1)
		{
			return;
		}
		this.keyCell.keyItem = this.Item;
	}

	// Token: 0x06002263 RID: 8803 RVA: 0x0000280F File Offset: 0x00000A0F
	public override int getItemPrice()
	{
		return 0;
	}

	// Token: 0x04001BD0 RID: 7120
	public KeyCell keyCell;
}
