using System;
using UnityEngine;

// Token: 0x02000207 RID: 519
public class EquipmentSystem : MonoBehaviour
{
	// Token: 0x06001055 RID: 4181 RVA: 0x000104C5 File Offset: 0x0000E6C5
	private void Start()
	{
		ConsumeItem.eS = base.GetComponent<EquipmentSystem>();
	}

	// Token: 0x06001056 RID: 4182 RVA: 0x000A5184 File Offset: 0x000A3384
	public void getSlotsInTotal()
	{
		Inventory component = base.GetComponent<Inventory>();
		this.slotsInTotal = component.width * component.height;
	}

	// Token: 0x04000CE6 RID: 3302
	[SerializeField]
	public int slotsInTotal;

	// Token: 0x04000CE7 RID: 3303
	[SerializeField]
	public ItemType[] itemTypeOfSlots = new ItemType[999];
}
