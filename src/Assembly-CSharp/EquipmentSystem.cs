using System;
using UnityEngine;

// Token: 0x02000136 RID: 310
public class EquipmentSystem : MonoBehaviour
{
	// Token: 0x06000E47 RID: 3655 RVA: 0x00054BBB File Offset: 0x00052DBB
	private void Start()
	{
		ConsumeItem.eS = base.GetComponent<EquipmentSystem>();
	}

	// Token: 0x06000E48 RID: 3656 RVA: 0x00054BC8 File Offset: 0x00052DC8
	public void getSlotsInTotal()
	{
		Inventory component = base.GetComponent<Inventory>();
		this.slotsInTotal = component.width * component.height;
	}

	// Token: 0x04000A4E RID: 2638
	[SerializeField]
	public int slotsInTotal;

	// Token: 0x04000A4F RID: 2639
	[SerializeField]
	public ItemType[] itemTypeOfSlots = new ItemType[999];
}
