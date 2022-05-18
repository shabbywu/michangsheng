using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x020012CA RID: 4810
	public class Draggable2D : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
	{
		// Token: 0x060074A9 RID: 29865 RVA: 0x0004F9E6 File Offset: 0x0004DBE6
		public void RegisterHandler(DragCompleted handler)
		{
			this.dragCompletedHandlers.Add(handler);
		}

		// Token: 0x060074AA RID: 29866 RVA: 0x0004F9F4 File Offset: 0x0004DBF4
		public void UnregisterHandler(DragCompleted handler)
		{
			if (this.dragCompletedHandlers.Contains(handler))
			{
				this.dragCompletedHandlers.Remove(handler);
			}
		}

		// Token: 0x060074AB RID: 29867 RVA: 0x0004FA11 File Offset: 0x0004DC11
		protected virtual void LateUpdate()
		{
			if (this.updatePosition)
			{
				base.transform.position = this.newPosition;
				this.updatePosition = false;
			}
		}

		// Token: 0x060074AC RID: 29868 RVA: 0x0004FA33 File Offset: 0x0004DC33
		protected virtual void OnTriggerEnter2D(Collider2D other)
		{
			if (!this.dragEnabled)
			{
				return;
			}
			FungusManager.Instance.EventDispatcher.Raise<DragEntered.DragEnteredEvent>(new DragEntered.DragEnteredEvent(this, other));
		}

		// Token: 0x060074AD RID: 29869 RVA: 0x0004FA54 File Offset: 0x0004DC54
		protected virtual void OnTriggerExit2D(Collider2D other)
		{
			if (!this.dragEnabled)
			{
				return;
			}
			FungusManager.Instance.EventDispatcher.Raise<DragExited.DragExitedEvent>(new DragExited.DragExitedEvent(this, other));
		}

		// Token: 0x060074AE RID: 29870 RVA: 0x002AE334 File Offset: 0x002AC534
		protected virtual void DoBeginDrag()
		{
			float x = Input.mousePosition.x;
			float y = Input.mousePosition.y;
			this.delta = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 10f)) - base.transform.position;
			this.delta.z = 0f;
			this.startingPosition = base.transform.position;
			FungusManager.Instance.EventDispatcher.Raise<DragStarted.DragStartedEvent>(new DragStarted.DragStartedEvent(this));
		}

		// Token: 0x060074AF RID: 29871 RVA: 0x002AE3BC File Offset: 0x002AC5BC
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

		// Token: 0x060074B0 RID: 29872 RVA: 0x002AE434 File Offset: 0x002AC634
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

		// Token: 0x060074B1 RID: 29873 RVA: 0x0004FA75 File Offset: 0x0004DC75
		protected virtual void DoPointerEnter()
		{
			this.ChangeCursor(this.hoverCursor);
		}

		// Token: 0x060074B2 RID: 29874 RVA: 0x0004FA83 File Offset: 0x0004DC83
		protected virtual void DoPointerExit()
		{
			SetMouseCursor.ResetMouseCursor();
		}

		// Token: 0x060074B3 RID: 29875 RVA: 0x0004FA8A File Offset: 0x0004DC8A
		protected virtual void ChangeCursor(Texture2D cursorTexture)
		{
			if (!this.dragEnabled)
			{
				return;
			}
			Cursor.SetCursor(cursorTexture, Vector2.zero, 0);
		}

		// Token: 0x060074B4 RID: 29876 RVA: 0x0004FAA1 File Offset: 0x0004DCA1
		protected virtual void OnMouseDown()
		{
			if (!this.useEventSystem)
			{
				this.DoBeginDrag();
			}
		}

		// Token: 0x060074B5 RID: 29877 RVA: 0x0004FAB1 File Offset: 0x0004DCB1
		protected virtual void OnMouseDrag()
		{
			if (!this.useEventSystem)
			{
				this.DoDrag();
			}
		}

		// Token: 0x060074B6 RID: 29878 RVA: 0x0004FAC1 File Offset: 0x0004DCC1
		protected virtual void OnMouseUp()
		{
			if (!this.useEventSystem)
			{
				this.DoEndDrag();
			}
		}

		// Token: 0x060074B7 RID: 29879 RVA: 0x0004FAD1 File Offset: 0x0004DCD1
		protected virtual void OnMouseEnter()
		{
			if (!this.useEventSystem)
			{
				this.DoPointerEnter();
			}
		}

		// Token: 0x060074B8 RID: 29880 RVA: 0x0004FAE1 File Offset: 0x0004DCE1
		protected virtual void OnMouseExit()
		{
			if (!this.useEventSystem)
			{
				this.DoPointerExit();
			}
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x060074B9 RID: 29881 RVA: 0x0004FAF1 File Offset: 0x0004DCF1
		// (set) Token: 0x060074BA RID: 29882 RVA: 0x0004FAF9 File Offset: 0x0004DCF9
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

		// Token: 0x060074BB RID: 29883 RVA: 0x0004FB02 File Offset: 0x0004DD02
		public void OnBeginDrag(PointerEventData eventData)
		{
			if (this.useEventSystem)
			{
				this.DoBeginDrag();
			}
		}

		// Token: 0x060074BC RID: 29884 RVA: 0x0004FB12 File Offset: 0x0004DD12
		public void OnDrag(PointerEventData eventData)
		{
			if (this.useEventSystem)
			{
				this.DoDrag();
			}
		}

		// Token: 0x060074BD RID: 29885 RVA: 0x0004FB22 File Offset: 0x0004DD22
		public void OnEndDrag(PointerEventData eventData)
		{
			if (this.useEventSystem)
			{
				this.DoEndDrag();
			}
		}

		// Token: 0x060074BE RID: 29886 RVA: 0x0004FB32 File Offset: 0x0004DD32
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (this.useEventSystem)
			{
				this.DoPointerEnter();
			}
		}

		// Token: 0x060074BF RID: 29887 RVA: 0x0004FB42 File Offset: 0x0004DD42
		public void OnPointerExit(PointerEventData eventData)
		{
			if (this.useEventSystem)
			{
				this.DoPointerExit();
			}
		}

		// Token: 0x0400664E RID: 26190
		[Tooltip("Is object dragging enabled")]
		[SerializeField]
		protected bool dragEnabled = true;

		// Token: 0x0400664F RID: 26191
		[Tooltip("Move object back to its starting position when drag is cancelled")]
		[FormerlySerializedAs("returnToStartPos")]
		[SerializeField]
		protected bool returnOnCancelled = true;

		// Token: 0x04006650 RID: 26192
		[Tooltip("Move object back to its starting position when drag is completed")]
		[SerializeField]
		protected bool returnOnCompleted = true;

		// Token: 0x04006651 RID: 26193
		[Tooltip("Time object takes to return to its starting position")]
		[SerializeField]
		protected float returnDuration = 1f;

		// Token: 0x04006652 RID: 26194
		[Tooltip("Mouse texture to use when hovering mouse over object")]
		[SerializeField]
		protected Texture2D hoverCursor;

		// Token: 0x04006653 RID: 26195
		[Tooltip("Use the UI Event System to check for drag events. Clicks that hit an overlapping UI object will be ignored. Camera must have a PhysicsRaycaster component, or a Physics2DRaycaster for 2D colliders.")]
		[SerializeField]
		protected bool useEventSystem;

		// Token: 0x04006654 RID: 26196
		protected Vector3 startingPosition;

		// Token: 0x04006655 RID: 26197
		protected bool updatePosition;

		// Token: 0x04006656 RID: 26198
		protected Vector3 newPosition;

		// Token: 0x04006657 RID: 26199
		protected Vector3 delta = Vector3.zero;

		// Token: 0x04006658 RID: 26200
		protected List<DragCompleted> dragCompletedHandlers = new List<DragCompleted>();
	}
}
