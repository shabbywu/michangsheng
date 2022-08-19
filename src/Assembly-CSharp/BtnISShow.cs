using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000295 RID: 661
public class BtnISShow : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x060017CF RID: 6095 RVA: 0x000A506E File Offset: 0x000A326E
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.isIn = true;
	}

	// Token: 0x060017D0 RID: 6096 RVA: 0x000A5077 File Offset: 0x000A3277
	public void OnPointerExit(PointerEventData eventData)
	{
		this.isIn = false;
	}

	// Token: 0x04001286 RID: 4742
	public bool isIn;
}
