using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000E6D RID: 3693
	public class Draggable2D : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
	{
		// Token: 0x060067F7 RID: 26615 RVA: 0x0028B80B File Offset: 0x00289A0B
		public void RegisterHandler(DragCompleted handler)
		{
			this.dragCompletedHandlers.Add(handler);
		}

		// Token: 0x060067F8 RID: 26616 RVA: 0x0028B819 File Offset: 0x00289A19
		public void UnregisterHandler(DragCompleted handler)
		{
			if (this.dragCompletedHandlers.Contains(handler))
			{
				this.dragCompletedHandlers.Remove(handler);
			}
		}

		// Token: 0x060067F9 RID: 26617 RVA: 0x0028B836 File Offset: 0x00289A36
		protected virtual void LateUpdate()
		{
			if (this.updatePosition)
			{
				base.transform.position = this.newPosition;
				this.updatePosition = false;
			}
		}

		// Token: 0x060067FA RID: 26618 RVA: 0x0028B858 File Offset: 0x00289A58
		protected virtual void OnTriggerEnter2D(Collider2D other)
		{
			if (!this.dragEnabled)
			{
				return;
			}
			FungusManager.Instance.EventDispatcher.Raise<DragEntered.DragEnteredEvent>(new DragEntered.DragEnteredEvent(this, other));
		}

		// Token: 0x060067FB RID: 26619 RVA: 0x0028B879 File Offset: 0x00289A79
		protected virtual void OnTriggerExit2D(Collider2D other)
		{
			if (!this.dragEnabled)
			{
				return;
			}
			FungusManager.Instance.EventDispatcher.Raise<DragExited.DragExitedEvent>(new DragExited.DragExitedEvent(this, other));
		}

		// Token: 0x060067FC RID: 26620 RVA: 0x0028B89C File Offset: 0x00289A9C
		protected virtual void DoBeginDrag()
		{
			float x = Input.mousePosition.x;
			float y = Input.mousePosition.y;
			this.delta = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 10f)) - base.transform.position;
			this.delta.z = 0f;
			this.startingPosition = base.transform.position;
			FungusManager.Instance.EventDispatcher.Raise<DragStarted.DragStartedEvent>(new DragStarted.DragStartedEvent(this));
		}

		// Token: 0x060067FD RID: 26621 RVA: 0x0028B924 File Offset: 0x00289B24
		protected virtual void DoDrag()
		{
			if (!this.dragEnabled)
			{
				return;
			}
			float x = Input.mousePosition.x;
			float y = Input.mousePosition.y;
			float z = base.transform.position.z;
			this.newPosition = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 10f)) - this.delta;
			this.newPosition.z = z;
			this.updatePosition = true;
		}

		// Token: 0x060067FE RID: 26622 RVA: 0x0028B99C File Offset: 0x00289B9C
		protected virtual void DoEndDrag()
		{
			if (!this.dragEnabled)
			{
				return;
			}
			EventDispatcher eventDispatcher = FungusManager.Instance.EventDispatcher;
			bool flag = false;
			for (int i = 0; i < this.dragCompletedHandlers.Count; i++)
			{
				DragCompleted dragCompleted = this.dragCompletedHandlers[i];
				if (dragCompleted != null && dragCompleted.DraggableObject == this && dragCompleted.IsOverTarget())
				{
					flag = true;
					eventDispatcher.Raise<DragCompleted.DragCompletedEvent>(new DragCompleted.DragCompletedEvent(this));
				}
			}
			if (!flag)
			{
				eventDispatcher.Raise<DragCancelled.DragCancelledEvent>(new DragCancelled.DragCancelledEvent(this));
				if (this.returnOnCancelled)
				{
					LeanTween.move(base.gameObject, this.startingPosition, this.returnDuration).setEase(LeanTweenType.easeOutExpo);
					return;
				}
			}
			else if (this.returnOnCompleted)
			{
				LeanTween.move(base.gameObject, this.startingPosition, this.returnDuration).setEase(LeanTweenType.easeOutExpo);
			}
		}

		// Token: 0x060067FF RID: 26623 RVA: 0x0028BA6D File Offset: 0x00289C6D
		protected virtual void DoPointerEnter()
		{
			this.ChangeCursor(this.hoverCursor);
		}

		// Token: 0x06006800 RID: 26624 RVA: 0x0028BA7B File Offset: 0x00289C7B
		protected virtual void DoPointerExit()
		{
			SetMouseCursor.ResetMouseCursor();
		}

		// Token: 0x06006801 RID: 26625 RVA: 0x0028BA82 File Offset: 0x00289C82
		protected virtual void ChangeCursor(Texture2D cursorTexture)
		{
			if (!this.dragEnabled)
			{
				return;
			}
			Cursor.SetCursor(cursorTexture, Vector2.zero, 0);
		}

		// Token: 0x06006802 RID: 26626 RVA: 0x0028BA99 File Offset: 0x00289C99
		protected virtual void OnMouseDown()
		{
			if (!this.useEventSystem)
			{
				this.DoBeginDrag();
			}
		}

		// Token: 0x06006803 RID: 26627 RVA: 0x0028BAA9 File Offset: 0x00289CA9
		protected virtual void OnMouseDrag()
		{
			if (!this.useEventSystem)
			{
				this.DoDrag();
			}
		}

		// Token: 0x06006804 RID: 26628 RVA: 0x0028BAB9 File Offset: 0x00289CB9
		protected virtual void OnMouseUp()
		{
			if (!this.useEventSystem)
			{
				this.DoEndDrag();
			}
		}

		// Token: 0x06006805 RID: 26629 RVA: 0x0028BAC9 File Offset: 0x00289CC9
		protected virtual void OnMouseEnter()
		{
			if (!this.useEventSystem)
			{
				this.DoPointerEnter();
			}
		}

		// Token: 0x06006806 RID: 26630 RVA: 0x0028BAD9 File Offset: 0x00289CD9
		protected virtual void OnMouseExit()
		{
			if (!this.useEventSystem)
			{
				this.DoPointerExit();
			}
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06006807 RID: 26631 RVA: 0x0028BAE9 File Offset: 0x00289CE9
		// (set) Token: 0x06006808 RID: 26632 RVA: 0x0028BAF1 File Offset: 0x00289CF1
		public virtual bool DragEnabled
		{
			get
			{
				return this.dragEnabled;
			}
			set
			{
				this.dragEnabled = value;
			}
		}

		// Token: 0x06006809 RID: 26633 RVA: 0x0028BAFA File Offset: 0x00289CFA
		public void OnBeginDrag(PointerEventData eventData)
		{
			if (this.useEventSystem)
			{
				this.DoBeginDrag();
			}
		}

		// Token: 0x0600680A RID: 26634 RVA: 0x0028BB0A File Offset: 0x00289D0A
		public void OnDrag(PointerEventData eventData)
		{
			if (this.useEventSystem)
			{
				this.DoDrag();
			}
		}

		// Token: 0x0600680B RID: 26635 RVA: 0x0028BB1A File Offset: 0x00289D1A
		public void OnEndDrag(PointerEventData eventData)
		{
			if (this.useEventSystem)
			{
				this.DoEndDrag();
			}
		}

		// Token: 0x0600680C RID: 26636 RVA: 0x0028BB2A File Offset: 0x00289D2A
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (this.useEventSystem)
			{
				this.DoPointerEnter();
			}
		}

		// Token: 0x0600680D RID: 26637 RVA: 0x0028BB3A File Offset: 0x00289D3A
		public void OnPointerExit(PointerEventData eventData)
		{
			if (this.useEventSystem)
			{
				this.DoPointerExit();
			}
		}

		// Token: 0x040058B6 RID: 22710
		[Tooltip("Is object dragging enabled")]
		[SerializeField]
		protected bool dragEnabled = true;

		// Token: 0x040058B7 RID: 22711
		[Tooltip("Move object back to its starting position when drag is cancelled")]
		[FormerlySerializedAs("returnToStartPos")]
		[SerializeField]
		protected bool returnOnCancelled = true;

		// Token: 0x040058B8 RID: 22712
		[Tooltip("Move object back to its starting position when drag is completed")]
		[SerializeField]
		protected bool returnOnCompleted = true;

		// Token: 0x040058B9 RID: 22713
		[Tooltip("Time object takes to return to its starting position")]
		[SerializeField]
		protected float returnDuration = 1f;

		// Token: 0x040058BA RID: 22714
		[Tooltip("Mouse texture to use when hovering mouse over object")]
		[SerializeField]
		protected Texture2D hoverCursor;

		// Token: 0x040058BB RID: 22715
		[Tooltip("Use the UI Event System to check for drag events. Clicks that hit an overlapping UI object will be ignored. Camera must have a PhysicsRaycaster component, or a Physics2DRaycaster for 2D colliders.")]
		[SerializeField]
		protected bool useEventSystem;

		// Token: 0x040058BC RID: 22716
		protected Vector3 startingPosition;

		// Token: 0x040058BD RID: 22717
		protected bool updatePosition;

		// Token: 0x040058BE RID: 22718
		protected Vector3 newPosition;

		// Token: 0x040058BF RID: 22719
		protected Vector3 delta = Vector3.zero;

		// Token: 0x040058C0 RID: 22720
		protected List<DragCompleted> dragCompletedHandlers = new List<DragCompleted>();
	}
}
