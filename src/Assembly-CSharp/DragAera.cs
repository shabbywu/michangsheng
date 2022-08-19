using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// Token: 0x02000350 RID: 848
public class DragAera : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
{
	// Token: 0x06001CD0 RID: 7376 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06001CD1 RID: 7377 RVA: 0x000CDF8C File Offset: 0x000CC18C
	public void OnBeginDrag(PointerEventData eventData)
	{
		if (this.OnBeginDragAction != null)
		{
			this.OnBeginDragAction.Invoke(eventData);
		}
	}

	// Token: 0x06001CD2 RID: 7378 RVA: 0x000CDFA2 File Offset: 0x000CC1A2
	public void OnDrag(PointerEventData eventData)
	{
		if (this.OnDragAction != null)
		{
			this.OnDragAction.Invoke(eventData);
		}
	}

	// Token: 0x06001CD3 RID: 7379 RVA: 0x000CDFB8 File Offset: 0x000CC1B8
	public void OnEndDrag(PointerEventData eventData)
	{
		if (this.OnEndDragAction != null)
		{
			this.OnEndDragAction.Invoke(eventData);
		}
	}

	// Token: 0x04001763 RID: 5987
	public UnityAction<PointerEventData> OnBeginDragAction;

	// Token: 0x04001764 RID: 5988
	public UnityAction<PointerEventData> OnDragAction;

	// Token: 0x04001765 RID: 5989
	public UnityAction<PointerEventData> OnEndDragAction;

	// Token: 0x04001766 RID: 5990
	public RectTransform RT;
}
