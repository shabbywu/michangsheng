using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000206 RID: 518
public class DragInventory : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IDragHandler
{
	// Token: 0x06001051 RID: 4177 RVA: 0x000A50E8 File Offset: 0x000A32E8
	private void Awake()
	{
		Canvas componentInParent = base.GetComponentInParent<Canvas>();
		if (componentInParent != null)
		{
			this.canvasRectTransform = (componentInParent.transform as RectTransform);
			this.panelRectTransform = (base.transform.parent as RectTransform);
		}
	}

	// Token: 0x06001052 RID: 4178 RVA: 0x000104A5 File Offset: 0x0000E6A5
	public void OnPointerDown(PointerEventData data)
	{
		RectTransformUtility.ScreenPointToLocalPointInRectangle(this.panelRectTransform, data.position, data.pressEventCamera, ref this.pointerOffset);
	}

	// Token: 0x06001053 RID: 4179 RVA: 0x000A512C File Offset: 0x000A332C
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

	// Token: 0x04000CE3 RID: 3299
	private Vector2 pointerOffset;

	// Token: 0x04000CE4 RID: 3300
	private RectTransform canvasRectTransform;

	// Token: 0x04000CE5 RID: 3301
	private RectTransform panelRectTransform;
}
