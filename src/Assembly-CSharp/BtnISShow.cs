using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x020003C5 RID: 965
public class BtnISShow : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x06001AAF RID: 6831 RVA: 0x00016AA6 File Offset: 0x00014CA6
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.isIn = true;
	}

	// Token: 0x06001AB0 RID: 6832 RVA: 0x00016AAF File Offset: 0x00014CAF
	public void OnPointerExit(PointerEventData eventData)
	{
		this.isIn = false;
	}

	// Token: 0x0400160F RID: 5647
	public bool isIn;
}
