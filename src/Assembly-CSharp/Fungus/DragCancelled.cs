using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E96 RID: 3734
	[EventHandlerInfo("Sprite", "Drag Cancelled", "The block will execute when the player drags an object and releases it without dropping it on a target object.")]
	[AddComponentMenu("")]
	public class DragCancelled : EventHandler
	{
		// Token: 0x060069DB RID: 27099 RVA: 0x002921F6 File Offset: 0x002903F6
		protected virtual void OnEnable()
		{
			this.eventDispatcher = FungusManager.Instance.EventDispatcher;
			this.eventDispatcher.AddListener<DragCancelled.DragCancelledEvent>(new EventDispatcher.TypedDelegate<DragCancelled.DragCancelledEvent>(this.OnDragCancelledEvent));
		}

		// Token: 0x060069DC RID: 27100 RVA: 0x00292220 File Offset: 0x00290420
		protected virtual void OnDisable()
		{
			this.eventDispatcher.RemoveListener<DragCancelled.DragCancelledEvent>(new EventDispatcher.TypedDelegate<DragCancelled.DragCancelledEvent>(this.OnDragCancelledEvent));
			this.eventDispatcher = null;
		}

		// Token: 0x060069DD RID: 27101 RVA: 0x00292241 File Offset: 0x00290441
		protected virtual void OnDragCancelledEvent(DragCancelled.DragCancelledEvent evt)
		{
			this.OnDragCancelled(evt.DraggableObject);
		}

		// Token: 0x060069DE RID: 27102 RVA: 0x0029224F File Offset: 0x0029044F
		public virtual void OnDragCancelled(Draggable2D draggableObject)
		{
			if (draggableObject == this.draggableObject)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x060069DF RID: 27103 RVA: 0x00292266 File Offset: 0x00290466
		public override string GetSummary()
		{
			if (this.draggableObject != null)
			{
				return this.draggableObject.name;
			}
			return "None";
		}

		// Token: 0x040059C2 RID: 22978
		[Tooltip("Draggable object to listen for drag events on")]
		[SerializeField]
		protected Draggable2D draggableObject;

		// Token: 0x040059C3 RID: 22979
		protected EventDispatcher eventDispatcher;

		// Token: 0x020016ED RID: 5869
		public class DragCancelledEvent
		{
			// Token: 0x06008895 RID: 34965 RVA: 0x002E9934 File Offset: 0x002E7B34
			public DragCancelledEvent(Draggable2D draggableObject)
			{
				this.DraggableObject = draggableObject;
			}

			// Token: 0x0400746A RID: 29802
			public Draggable2D DraggableObject;
		}
	}
}
