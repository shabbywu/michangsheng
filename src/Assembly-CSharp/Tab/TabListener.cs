using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Tab
{
	// Token: 0x02000A30 RID: 2608
	public class TabListener : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
	{
		// Token: 0x06004382 RID: 17282 RVA: 0x000303CD File Offset: 0x0002E5CD
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

		// Token: 0x06004383 RID: 17283 RVA: 0x000303EB File Offset: 0x0002E5EB
		public void OnPointerExit(PointerEventData eventData)
		{
			if (this.mouseOutEvent != null)
			{
				this.mouseOutEvent.Invoke();
			}
		}

		// Token: 0x06004384 RID: 17284 RVA: 0x00030400 File Offset: 0x0002E600
		public void OnPointerDown(PointerEventData eventData)
		{
			if (this.mouseDownEvent != null)
			{
				this.mouseDownEvent.Invoke();
			}
		}

		// Token: 0x06004385 RID: 17285 RVA: 0x00030415 File Offset: 0x0002E615
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

		// Token: 0x04003B82 RID: 15234
		public UnityEvent mouseUpEvent = new UnityEvent();

		// Token: 0x04003B83 RID: 15235
		public UnityEvent mouseDownEvent = new UnityEvent();

		// Token: 0x04003B84 RID: 15236
		public UnityEvent mouseEnterEvent = new UnityEvent();

		// Token: 0x04003B85 RID: 15237
		public UnityEvent mouseOutEvent = new UnityEvent();
	}
}
