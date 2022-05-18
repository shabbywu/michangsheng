using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000205 RID: 517
public class DesignForInventory : MonoBehaviour
{
	// Token: 0x0600104E RID: 4174 RVA: 0x000A5024 File Offset: 0x000A3224
	private void Start()
	{
		this.inventoryTitle = base.transform.GetChild(0).GetComponent<Text>();
		this.backgroundInventory = base.GetComponent<Image>();
		this.backgroundSlot = base.transform.GetChild(1).GetChild(0).GetComponent<Image>();
		this.amountSlot = this.getTextAmountOfItem();
	}

	// Token: 0x0600104F RID: 4175 RVA: 0x000A5080 File Offset: 0x000A3280
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

	// Token: 0x04000CDF RID: 3295
	public Text inventoryTitle;

	// Token: 0x04000CE0 RID: 3296
	public Image backgroundInventory;

	// Token: 0x04000CE1 RID: 3297
	public Image backgroundSlot;

	// Token: 0x04000CE2 RID: 3298
	public Text amountSlot;
}
