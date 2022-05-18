using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001311 RID: 4881
	[EventHandlerInfo("Sprite", "Drag Completed", "The block will execute when the player drags an object and successfully drops it on a target object.")]
	[AddComponentMenu("")]
	public class DragCompleted : EventHandler
	{
		// Token: 0x0600770D RID: 30477 RVA: 0x002B4D6C File Offset: 0x002B2F6C
		protected virtual void OnEnable()
		{
			this.eventDispatcher = FungusManager.Instance.EventDispatcher;
			this.eventDispatcher.AddListener<DragCompleted.DragCompletedEvent>(new EventDispatcher.TypedDelegate<DragCompleted.DragCompletedEvent>(this.OnDragCompletedEvent));
			this.eventDispatcher.AddListener<DragEntered.DragEnteredEvent>(new EventDispatcher.TypedDelegate<DragEntered.DragEnteredEvent>(this.OnDragEnteredEvent));
			this.eventDispatcher.AddListener<DragExited.DragExitedEvent>(new EventDispatcher.TypedDelegate<DragExited.DragExitedEvent>(this.OnDragExitedEvent));
			if (this.draggableObject != null)
			{
				this.draggableObject.RegisterHandler(this);
			}
		}

		// Token: 0x0600770E RID: 30478 RVA: 0x002B4DE8 File Offset: 0x002B2FE8
		protected virtual void OnDisable()
		{
			this.eventDispatcher.RemoveListener<DragCompleted.DragCompletedEvent>(new EventDispatcher.TypedDelegate<DragCompleted.DragCompletedEvent>(this.OnDragCompletedEvent));
			this.eventDispatcher.RemoveListener<DragEntered.DragEnteredEvent>(new EventDispatcher.TypedDelegate<DragEntered.DragEnteredEvent>(this.OnDragEnteredEvent));
			this.eventDispatcher.RemoveListener<DragExited.DragExitedEvent>(new EventDispatcher.TypedDelegate<DragExited.DragExitedEvent>(this.OnDragExitedEvent));
			if (this.draggableObject != null)
			{
				this.draggableObject.UnregisterHandler(this);
			}
			this.eventDispatcher = null;
		}

		// Token: 0x0600770F RID: 30479 RVA: 0x000510B7 File Offset: 0x0004F2B7
		private void OnDragCompletedEvent(DragCompleted.DragCompletedEvent evt)
		{
			this.OnDragCompleted(evt.DraggableObject);
		}

		// Token: 0x06007710 RID: 30480 RVA: 0x000510C5 File Offset: 0x0004F2C5
		private void OnDragEnteredEvent(DragEntered.DragEnteredEvent evt)
		{
			this.OnDragEntered(evt.DraggableObject, evt.TargetCollider);
		}

		// Token: 0x06007711 RID: 30481 RVA: 0x000510D9 File Offset: 0x0004F2D9
		private void OnDragExitedEvent(DragExited.DragExitedEvent evt)
		{
			this.OnDragExited(evt.DraggableObject, evt.TargetCollider);
		}

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06007712 RID: 30482 RVA: 0x000510ED File Offset: 0x0004F2ED
		public virtual Draggable2D DraggableObject
		{
			get
			{
				return this.draggableObject;
			}
		}

		// Token: 0x06007713 RID: 30483 RVA: 0x000510F5 File Offset: 0x0004F2F5
		public virtual bool IsOverTarget()
		{
			return this.overTarget;
		}

		// Token: 0x06007714 RID: 30484 RVA: 0x000510FD File Offset: 0x0004F2FD
		public virtual void OnDragEntered(Draggable2D draggableObject, Collider2D targetObject)
		{
			if (this.targetObject != null && draggableObject == this.draggableObject && targetObject == this.targetObject)
			{
				this.overTarget = true;
			}
		}

		// Token: 0x06007715 RID: 30485 RVA: 0x00051130 File Offset: 0x0004F330
		public virtual void OnDragExited(Draggable2D draggableObject, Collider2D targetObject)
		{
			if (this.targetObject != null && draggableObject == this.draggableObject && targetObject == this.targetObject)
			{
				this.overTarget = false;
			}
		}

		// Token: 0x06007716 RID: 30486 RVA: 0x00051163 File Offset: 0x0004F363
		public virtual void OnDragCompleted(Draggable2D draggableObject)
		{
			if (draggableObject == this.draggableObject && this.overTarget)
			{
				this.overTarget = false;
				this.ExecuteBlock();
			}
		}

		// Token: 0x06007717 RID: 30487 RVA: 0x002B4E5C File Offset: 0x002B305C
		public override string GetSummary()
		{
			string text = "";
			if (this.draggableObject != null)
			{
				text = text + "\nDraggable: " + this.draggableObject.name;
			}
			if (this.targetObject != null)
			{
				text = text + "\nTarget: " + this.targetObject.name;
			}
			if (text.Length == 0)
			{
				return "None";
			}
			return text;
		}

		// Token: 0x040067D4 RID: 26580
		[Tooltip("Draggable object to listen for drag events on")]
		[SerializeField]
		protected Draggable2D draggableObject;

		// Token: 0x040067D5 RID: 26581
		[Tooltip("Drag target object to listen for drag events on")]
		[SerializeField]
		protected Collider2D targetObject;

		// Token: 0x040067D6 RID: 26582
		protected bool overTarget;

		// Token: 0x040067D7 RID: 26583
		protected EventDispatcher eventDispatcher;

		// Token: 0x02001312 RID: 4882
		public class DragCompletedEvent
		{
			// Token: 0x06007719 RID: 30489 RVA: 0x00051189 File Offset: 0x0004F389
			public DragCompletedEvent(Draggable2D draggableObject)
			{
				this.DraggableObject = draggableObject;
			}

			// Token: 0x040067D8 RID: 26584
			public Draggable2D DraggableObject;
		}
	}
}
