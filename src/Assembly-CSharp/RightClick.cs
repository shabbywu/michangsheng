using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200012B RID: 299
public class RightClick : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	// Token: 0x06000E19 RID: 3609 RVA: 0x000534B4 File Offset: 0x000516B4
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

	// Token: 0x04000A04 RID: 2564
	private CraftResultSlot resultScript;

	// Token: 0x04000A05 RID: 2565
	private CraftSystem craftSystem;
}
