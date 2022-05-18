using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SuperScrollView
{
	// Token: 0x020009E6 RID: 2534
	public class ClickEventListener : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerDownHandler, IPointerUpHandler
	{
		// Token: 0x060040B8 RID: 16568 RVA: 0x001BE7C8 File Offset: 0x001BC9C8
		public static ClickEventListener Get(GameObject obj)
		{
			ClickEventListener clickEventListener = obj.GetComponent<ClickEventListener>();
			if (clickEventListener == null)
			{
				clickEventListener = obj.AddComponent<ClickEventListener>();
			}
			return clickEventListener;
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x060040B9 RID: 16569 RVA: 0x0002E833 File Offset: 0x0002CA33
		public bool IsPressd
		{
			get
			{
				return this.mIsPressed;
			}
		}

		// Token: 0x060040BA RID: 16570 RVA: 0x0002E83B File Offset: 0x0002CA3B
		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.clickCount == 2)
			{
				if (this.mDoubleClickedHandler != null)
				{
					this.mDoubleClickedHandler(base.gameObject);
					return;
				}
			}
			else if (this.mClickedHandler != null)
			{
				this.mClickedHandler(base.gameObject);
			}
		}

		// Token: 0x060040BB RID: 16571 RVA: 0x0002E879 File Offset: 0x0002CA79
		public void SetClickEventHandler(Action<GameObject> handler)
		{
			this.mClickedHandler = handler;
		}

		// Token: 0x060040BC RID: 16572 RVA: 0x0002E882 File Offset: 0x0002CA82
		public void SetDoubleClickEventHandler(Action<GameObject> handler)
		{
			this.mDoubleClickedHandler = handler;
		}

		// Token: 0x060040BD RID: 16573 RVA: 0x0002E88B File Offset: 0x0002CA8B
		public void SetPointerDownHandler(Action<GameObject> handler)
		{
			this.mOnPointerDownHandler = handler;
		}

		// Token: 0x060040BE RID: 16574 RVA: 0x0002E894 File Offset: 0x0002CA94
		public void SetPointerUpHandler(Action<GameObject> handler)
		{
			this.mOnPointerUpHandler = handler;
		}

		// Token: 0x060040BF RID: 16575 RVA: 0x0002E89D File Offset: 0x0002CA9D
		public void OnPointerDown(PointerEventData eventData)
		{
			this.mIsPressed = true;
			if (this.mOnPointerDownHandler != null)
			{
				this.mOnPointerDownHandler(base.gameObject);
			}
		}

		// Token: 0x060040C0 RID: 16576 RVA: 0x0002E8BF File Offset: 0x0002CABF
		public void OnPointerUp(PointerEventData eventData)
		{
			this.mIsPressed = false;
			if (this.mOnPointerUpHandler != null)
			{
				this.mOnPointerUpHandler(base.gameObject);
			}
		}

		// Token: 0x04003997 RID: 14743
		private Action<GameObject> mClickedHandler;

		// Token: 0x04003998 RID: 14744
		private Action<GameObject> mDoubleClickedHandler;

		// Token: 0x04003999 RID: 14745
		private Action<GameObject> mOnPointerDownHandler;

		// Token: 0x0400399A RID: 14746
		private Action<GameObject> mOnPointerUpHandler;

		// Token: 0x0400399B RID: 14747
		private bool mIsPressed;
	}
}
