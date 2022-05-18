using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200130F RID: 4879
	[EventHandlerInfo("Sprite", "Drag Cancelled", "The block will execute when the player drags an object and releases it without dropping it on a target object.")]
	[AddComponentMenu("")]
	public class DragCancelled : EventHandler
	{
		// Token: 0x06007706 RID: 30470 RVA: 0x00051017 File Offset: 0x0004F217
		protected virtual void OnEnable()
		{
			this.eventDispatcher = FungusManager.Instance.EventDispatcher;
			this.eventDispatcher.AddListener<DragCancelled.DragCancelledEvent>(new EventDispatcher.TypedDelegate<DragCancelled.DragCancelledEvent>(this.OnDragCancelledEvent));
		}

		// Token: 0x06007707 RID: 30471 RVA: 0x00051041 File Offset: 0x0004F241
		protected virtual void OnDisable()
		{
			this.eventDispatcher.RemoveListener<DragCancelled.DragCancelledEvent>(new EventDispatcher.TypedDelegate<DragCancelled.DragCancelledEvent>(this.OnDragCancelledEvent));
			this.eventDispatcher = null;
		}

		// Token: 0x06007708 RID: 30472 RVA: 0x00051062 File Offset: 0x0004F262
		protected virtual void OnDragCancelledEvent(DragCancelled.DragCancelledEvent evt)
		{
			this.OnDragCancelled(evt.DraggableObject);
		}

		// Token: 0x06007709 RID: 30473 RVA: 0x00051070 File Offset: 0x0004F270
		public virtual void OnDragCancelled(Draggable2D draggableObject)
		{
			if (draggableObject == this.draggableObject)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x0600770A RID: 30474 RVA: 0x00051087 File Offset: 0x0004F287
		public override string GetSummary()
		{
			if (this.draggableObject != null)
			{
				return this.draggableObject.name;
			}
			return "None";
		}

		// Token: 0x040067D1 RID: 26577
		[Tooltip("Draggable object to listen for drag events on")]
		[SerializeField]
		protected Draggable2D draggableObject;

		// Token: 0x040067D2 RID: 26578
		protected EventDispatcher eventDispatcher;

		// Token: 0x02001310 RID: 4880
		public class DragCancelledEvent
		{
			// Token: 0x0600770C RID: 30476 RVA: 0x000510A8 File Offset: 0x0004F2A8
			public DragCancelledEvent(Draggable2D draggableObject)
			{
				this.DraggableObject = draggableObject;
			}

			// Token: 0x040067D3 RID: 26579
			public Draggable2D DraggableObject;
		}
	}
}
