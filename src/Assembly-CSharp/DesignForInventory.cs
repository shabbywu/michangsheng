using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000134 RID: 308
public class DesignForInventory : MonoBehaviour
{
	// Token: 0x06000E40 RID: 3648 RVA: 0x00054A3C File Offset: 0x00052C3C
	private void Start()
	{
		this.inventoryTitle = base.transform.GetChild(0).GetComponent<Text>();
		this.backgroundInventory = base.GetComponent<Image>();
		this.backgroundSlot = base.transform.GetChild(1).GetChild(0).GetComponent<Image>();
		this.amountSlot = this.getTextAmountOfItem();
	}

	// Token: 0x06000E41 RID: 3649 RVA: 0x00054A98 File Offset: 0x00052C98
	public Text getTextAmountOfItem()
	{
		for (int i = 0; i < base.transform.GetChild(1).childCount; i++)
		{
			if (base.transform.GetChild(1).GetChild(i).childCount != 0)
			{
				return base.transform.GetChild(1).GetChild(i).GetChild(0).GetChild(1).GetComponent<Text>();
			}
		}
		return null;
	}

	// Token: 0x04000A47 RID: 2631
	public Text inventoryTitle;

	// Token: 0x04000A48 RID: 2632
	public Image backgroundInventory;

	// Token: 0x04000A49 RID: 2633
	public Image backgroundSlot;

	// Token: 0x04000A4A RID: 2634
	public Text amountSlot;
}
