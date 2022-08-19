using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SuperScrollView
{
	// Token: 0x020006BB RID: 1723
	public class ClickEventListener : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerDownHandler, IPointerUpHandler
	{
		// Token: 0x0600369B RID: 13979 RVA: 0x00175DC4 File Offset: 0x00173FC4
		public static ClickEventListener Get(GameObject obj)
		{
			ClickEventListener clickEventListener = obj.GetComponent<ClickEventListener>();
			if (clickEventListener == null)
			{
				clickEventListener = obj.AddComponent<ClickEventListener>();
			}
			return clickEventListener;
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x0600369C RID: 13980 RVA: 0x00175DE9 File Offset: 0x00173FE9
		public bool IsPressd
		{
			get
			{
				return this.mIsPressed;
			}
		}

		// Token: 0x0600369D RID: 13981 RVA: 0x00175DF1 File Offset: 0x00173FF1
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

		// Token: 0x0600369E RID: 13982 RVA: 0x00175E2F File Offset: 0x0017402F
		public void SetClickEventHandler(Action<GameObject> handler)
		{
			this.mClickedHandler = handler;
		}

		// Token: 0x0600369F RID: 13983 RVA: 0x00175E38 File Offset: 0x00174038
		public void SetDoubleClickEventHandler(Action<GameObject> handler)
		{
			this.mDoubleClickedHandler = handler;
		}

		// Token: 0x060036A0 RID: 13984 RVA: 0x00175E41 File Offset: 0x00174041
		public void SetPointerDownHandler(Action<GameObject> handler)
		{
			this.mOnPointerDownHandler = handler;
		}

		// Token: 0x060036A1 RID: 13985 RVA: 0x00175E4A File Offset: 0x0017404A
		public void SetPointerUpHandler(Action<GameObject> handler)
		{
			this.mOnPointerUpHandler = handler;
		}

		// Token: 0x060036A2 RID: 13986 RVA: 0x00175E53 File Offset: 0x00174053
		public void OnPointerDown(PointerEventData eventData)
		{
			this.mIsPressed = true;
			if (this.mOnPointerDownHandler != null)
			{
				this.mOnPointerDownHandler(base.gameObject);
			}
		}

		// Token: 0x060036A3 RID: 13987 RVA: 0x00175E75 File Offset: 0x00174075
		public void OnPointerUp(PointerEventData eventData)
		{
			this.mIsPressed = false;
			if (this.mOnPointerUpHandler != null)
			{
				this.mOnPointerUpHandler(base.gameObject);
			}
		}

		// Token: 0x04002F92 RID: 12178
		private Action<GameObject> mClickedHandler;

		// Token: 0x04002F93 RID: 12179
		private Action<GameObject> mDoubleClickedHandler;

		// Token: 0x04002F94 RID: 12180
		private Action<GameObject> mOnPointerDownHandler;

		// Token: 0x04002F95 RID: 12181
		private Action<GameObject> mOnPointerUpHandler;

		// Token: 0x04002F96 RID: 12182
		private bool mIsPressed;
	}
}
