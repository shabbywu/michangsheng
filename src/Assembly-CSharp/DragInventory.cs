using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000135 RID: 309
public class DragInventory : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IDragHandler
{
	// Token: 0x06000E43 RID: 3651 RVA: 0x00054B00 File Offset: 0x00052D00
	private void Awake()
	{
		Canvas componentInParent = base.GetComponentInParent<Canvas>();
		if (componentInParent != null)
		{
			this.canvasRectTransform = (componentInParent.transform as RectTransform);
			this.panelRectTransform = (base.transform.parent as RectTransform);
		}
	}

	// Token: 0x06000E44 RID: 3652 RVA: 0x00054B44 File Offset: 0x00052D44
	public void OnPointerDown(PointerEventData data)
	{
		RectTransformUtility.ScreenPointToLocalPointInRectangle(this.panelRectTransform, data.position, data.pressEventCamera, ref this.pointerOffset);
	}

	// Token: 0x06000E45 RID: 3653 RVA: 0x00054B64 File Offset: 0x00052D64
	public void OnDrag(PointerEventData data)
	{
		if (this.panelRectTransform == null)
		{
			return;
		}
		Vector2 vector;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(this.canvasRectTransform, Input.mousePosition, data.pressEventCamera, ref vector))
		{
			this.panelRectTransform.localPosition = vector - this.pointerOffset;
		}
	}

	// Token: 0x04000A4B RID: 2635
	private Vector2 pointerOffset;

	// Token: 0x04000A4C RID: 2636
	private RectTransform canvasRectTransform;

	// Token: 0x04000A4D RID: 2637
	private RectTransform panelRectTransform;
}
