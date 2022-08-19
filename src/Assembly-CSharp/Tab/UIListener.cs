using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Tab
{
	// Token: 0x020006EF RID: 1775
	public class UIListener : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
	{
		// Token: 0x06003917 RID: 14615 RVA: 0x00185C06 File Offset: 0x00183E06
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (eventData.dragging)
			{
				return;
			}
			if (this.mouseEnterEvent != null)
			{
				this.mouseEnterEvent.Invoke();
			}
		}

		// Token: 0x06003918 RID: 14616 RVA: 0x00185C24 File Offset: 0x00183E24
		public void OnPointerExit(PointerEventData eventData)
		{
			if (this.mouseOutEvent != null)
			{
				this.mouseOutEvent.Invoke();
			}
		}

		// Token: 0x06003919 RID: 14617 RVA: 0x00185C39 File Offset: 0x00183E39
		public void OnPointerDown(PointerEventData eventData)
		{
			if (this.mouseDownEvent != null)
			{
				this.mouseDownEvent.Invoke();
			}
		}

		// Token: 0x0600391A RID: 14618 RVA: 0x00185C4E File Offset: 0x00183E4E
		public void OnPointerUp(PointerEventData eventData)
		{
			if (eventData.dragging)
			{
				return;
			}
			if (this.mouseUpEvent != null)
			{
				this.mouseUpEvent.Invoke();
			}
		}

		// Token: 0x0400311D RID: 12573
		public UnityEvent mouseUpEvent = new UnityEvent();

		// Token: 0x0400311E RID: 12574
		public UnityEvent mouseDownEvent = new UnityEvent();

		// Token: 0x0400311F RID: 12575
		public UnityEvent mouseEnterEvent = new UnityEvent();

		// Token: 0x04003120 RID: 12576
		public UnityEvent mouseOutEvent = new UnityEvent();
	}
}
