using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200012A RID: 298
public class LeftClick : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	// Token: 0x06000E17 RID: 3607 RVA: 0x00053414 File Offset: 0x00051614
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

	// Token: 0x04000A02 RID: 2562
	private CraftResultSlot resultScript;

	// Token: 0x04000A03 RID: 2563
	private CraftSystem craftSystem;
}
