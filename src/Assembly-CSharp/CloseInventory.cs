using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000133 RID: 307
public class CloseInventory : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	// Token: 0x06000E3D RID: 3645 RVA: 0x00054A0D File Offset: 0x00052C0D
	private void Start()
	{
		this.inv = base.transform.parent.GetComponent<Inventory>();
	}

	// Token: 0x06000E3E RID: 3646 RVA: 0x00054A25 File Offset: 0x00052C25
	public void OnPointerDown(PointerEventData eventData)
	{
		if (eventData.button == null)
		{
			this.inv.closeInventory();
		}
	}

	// Token: 0x04000A46 RID: 2630
	private Inventory inv;
}
