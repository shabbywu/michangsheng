using System;
using GUIPackage;

// Token: 0x020005EF RID: 1519
public class ItemCellLinWu : ItemCell
{
	// Token: 0x06002620 RID: 9760 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06002621 RID: 9761 RVA: 0x0001E6F9 File Offset: 0x0001C8F9
	private void OnPress()
	{
		if (this.Item.itemID == -1)
		{
			return;
		}
		this.keyCell.keyItem = this.Item;
	}

	// Token: 0x06002622 RID: 9762 RVA: 0x00004050 File Offset: 0x00002250
	public override int getItemPrice()
	{
		return 0;
	}

	// Token: 0x0400209C RID: 8348
	public KeyCell keyCell;
}
