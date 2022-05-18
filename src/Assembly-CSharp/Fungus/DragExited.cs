using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001315 RID: 4885
	[EventHandlerInfo("Sprite", "Drag Exited", "The block will execute when the player is dragging an object which stops touching the target object.")]
	[AddComponentMenu("")]
	public class DragExited : EventHandler
	{
		// Token: 0x06007721 RID: 30497 RVA: 0x00051230 File Offset: 0x0004F430
		protected virtual void OnEnable()
		{
			this.eventDispatcher = FungusManager.Instance.EventDispatcher;
			this.eventDispatcher.AddListener<DragExited.DragExitedEvent>(new EventDispatcher.TypedDelegate<DragExited.DragExitedEvent>(this.OnDragEnteredEvent));
		}

		// Token: 0x06007722 RID: 30498 RVA: 0x00051259 File Offset: 0x0004F459
		protected virtual void OnDisable()
		{
			this.eventDispatcher.RemoveListener<DragExited.DragExitedEvent>(new EventDispatcher.TypedDelegate<DragExited.DragExitedEvent>(this.OnDragEnteredEvent));
			this.eventDispatcher = null;
		}

		// Token: 0x06007723 RID: 30499 RVA: 0x00051279 File Offset: 0x0004F479
		private void OnDragEnteredEvent(DragExited.DragExitedEvent evt)
		{
			this.OnDragExited(evt.DraggableObject, evt.TargetCollider);
		}

		// Token: 0x06007724 RID: 30500 RVA: 0x0005128D File Offset: 0x0004F48D
		public virtual void OnDragExited(Draggable2D draggableObject, Collider2D targetObject)
		{
			if (draggableObject == this.draggableObject && targetObject == this.targetObject)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x06007725 RID: 30501 RVA: 0x002B4F34 File Offset: 0x002B3134
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

		// Token: 0x040067DE RID: 26590
		[Tooltip("Draggable object to listen for drag events on")]
		[SerializeField]
		protected Draggable2D draggableObject;

		// Token: 0x040067DF RID: 26591
		[Tooltip("Drag target object to listen for drag events on")]
		[SerializeField]
		protected Collider2D targetObject;

		// Token: 0x040067E0 RID: 26592
		protected EventDispatcher eventDispatcher;

		// Token: 0x02001316 RID: 4886
		public class DragExitedEvent
		{
			// Token: 0x06007727 RID: 30503 RVA: 0x000512B2 File Offset: 0x0004F4B2
			public DragExitedEvent(Draggable2D draggableObject, Collider2D targetCollider)
			{
				this.DraggableObject = draggableObject;
				this.TargetCollider = targetCollider;
			}

			// Token: 0x040067E1 RID: 26593
			public Draggable2D DraggableObject;

			// Token: 0x040067E2 RID: 26594
			public Collider2D TargetCollider;
		}
	}
}
