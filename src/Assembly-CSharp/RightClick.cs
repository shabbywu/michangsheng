using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x020001FC RID: 508
public class RightClick : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	// Token: 0x06001027 RID: 4135 RVA: 0x000A3C04 File Offset: 0x000A1E04
	public void OnPointerDown(PointerEventData data)
	{
		if (this.craftSystem == null)
		{
			this.craftSystem = base.transform.parent.GetComponent<CraftSystem>();
			this.resultScript = base.transform.parent.GetChild(3).GetComponent<CraftResultSlot>();
		}
		if (this.resultScript.temp < this.craftSystem.possibleItems.Count - 1)
		{
			this.resultScript.temp++;
			return;
		}
		this.resultScript.temp = 0;
	}

	// Token: 0x04000C9C RID: 3228
	private CraftResultSlot resultScript;

	// Token: 0x04000C9D RID: 3229
	private CraftSystem craftSystem;
}
