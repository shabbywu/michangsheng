using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x020001FB RID: 507
public class LeftClick : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	// Token: 0x06001025 RID: 4133 RVA: 0x000A3B64 File Offset: 0x000A1D64
	public void OnPointerDown(PointerEventData data)
	{
		if (this.craftSystem == null)
		{
			this.craftSystem = base.transform.parent.GetComponent<CraftSystem>();
			this.resultScript = base.transform.parent.GetChild(3).GetComponent<CraftResultSlot>();
		}
		if (this.craftSystem.possibleItems.Count > 1 && this.resultScript.temp >= 1)
		{
			this.resultScript.temp--;
			return;
		}
		this.resultScript.temp = this.craftSystem.possibleItems.Count - 1;
	}

	// Token: 0x04000C9A RID: 3226
	private CraftResultSlot resultScript;

	// Token: 0x04000C9B RID: 3227
	private CraftSystem craftSystem;
}
