using System;
using UnityEngine;

// Token: 0x02000131 RID: 305
public class Hotbar : MonoBehaviour
{
	// Token: 0x06000E34 RID: 3636 RVA: 0x00054648 File Offset: 0x00052848
	private void Update()
	{
		for (int i = 0; i < this.slotsInTotal; i++)
		{
			if (Input.GetKeyDown(this.keyCodesForSlots[i]) && base.transform.GetChild(1).GetChild(i).childCount != 0 && base.transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<ItemOnObject>().item.itemType != ItemType.UFPS_Ammo)
			{
				if (base.transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<ConsumeItem>().duplication != null && base.transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<ItemOnObject>().item.maxStack == 1)
				{
					Object.Destroy(base.transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<ConsumeItem>().duplication);
				}
				base.transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<ConsumeItem>().consumeIt();
			}
		}
	}

	// Token: 0x06000E35 RID: 3637 RVA: 0x00054768 File Offset: 0x00052968
	public int getSlotsInTotal()
	{
		Inventory component = base.GetComponent<Inventory>();
		return this.slotsInTotal = component.width * component.height;
	}

	// Token: 0x04000A3E RID: 2622
	[SerializeField]
	public KeyCode[] keyCodesForSlots = new KeyCode[999];

	// Token: 0x04000A3F RID: 2623
	[SerializeField]
	public int slotsInTotal;
}
