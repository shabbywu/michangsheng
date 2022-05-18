using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000204 RID: 516
public class CloseInventory : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	// Token: 0x0600104B RID: 4171 RVA: 0x00010478 File Offset: 0x0000E678
	private void Start()
	{
		this.inv = base.transform.parent.GetComponent<Inventory>();
	}

	// Token: 0x0600104C RID: 4172 RVA: 0x00010490 File Offset: 0x0000E690
	public void OnPointerDown(PointerEventData eventData)
	{
		if (eventData.button == null)
		{
			this.inv.closeInventory();
		}
	}

	// Token: 0x04000CDE RID: 3294
	private Inventory inv;
}
