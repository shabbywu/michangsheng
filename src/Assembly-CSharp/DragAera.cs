using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// Token: 0x020004CC RID: 1228
public class DragAera : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
{
	// Token: 0x06002037 RID: 8247 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06002038 RID: 8248 RVA: 0x0001A6D8 File Offset: 0x000188D8
	public void OnBeginDrag(PointerEventData eventData)
	{
		if (this.OnBeginDragAction != null)
		{
			this.OnBeginDragAction.Invoke(eventData);
		}
	}

	// Token: 0x06002039 RID: 8249 RVA: 0x0001A6EE File Offset: 0x000188EE
	public void OnDrag(PointerEventData eventData)
	{
		if (this.OnDragAction != null)
		{
			this.OnDragAction.Invoke(eventData);
		}
	}

	// Token: 0x0600203A RID: 8250 RVA: 0x0001A704 File Offset: 0x00018904
	public void OnEndDrag(PointerEventData eventData)
	{
		if (this.OnEndDragAction != null)
		{
			this.OnEndDragAction.Invoke(eventData);
		}
	}

	// Token: 0x04001BB9 RID: 7097
	public UnityAction<PointerEventData> OnBeginDragAction;

	// Token: 0x04001BBA RID: 7098
	public UnityAction<PointerEventData> OnDragAction;

	// Token: 0x04001BBB RID: 7099
	public UnityAction<PointerEventData> OnEndDragAction;

	// Token: 0x04001BBC RID: 7100
	public RectTransform RT;
}
