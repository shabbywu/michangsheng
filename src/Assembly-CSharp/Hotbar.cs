using System;
using UnityEngine;

// Token: 0x02000202 RID: 514
public class Hotbar : MonoBehaviour
{
	// Token: 0x06001042 RID: 4162 RVA: 0x000A4C98 File Offset: 0x000A2E98
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

	// Token: 0x06001043 RID: 4163 RVA: 0x000A4DB8 File Offset: 0x000A2FB8
	public int getSlotsInTotal()
	{
		Inventory component = base.GetComponent<Inventory>();
		return this.slotsInTotal = component.width * component.height;
	}

	// Token: 0x04000CD6 RID: 3286
	[SerializeField]
	public KeyCode[] keyCodesForSlots = new KeyCode[999];

	// Token: 0x04000CD7 RID: 3287
	[SerializeField]
	public int slotsInTotal;
}
