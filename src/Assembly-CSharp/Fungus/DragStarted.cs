using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E9A RID: 3738
	[EventHandlerInfo("Sprite", "Drag Started", "The block will execute when the player starts dragging an object.")]
	[AddComponentMenu("")]
	public class DragStarted : EventHandler
	{
		// Token: 0x060069F9 RID: 27129 RVA: 0x00292698 File Offset: 0x00290898
		protected virtual void OnEnable()
		{
			this.eventDispatcher = FungusManager.Instance.EventDispatcher;
			this.eventDispatcher.AddListener<DragStarted.DragStartedEvent>(new EventDispatcher.TypedDelegate<DragStarted.DragStartedEvent>(this.OnDragStartedEvent));
		}

		// Token: 0x060069FA RID: 27130 RVA: 0x002926C1 File Offset: 0x002908C1
		protected virtual void OnDisable()
		{
			this.eventDispatcher.RemoveListener<DragStarted.DragStartedEvent>(new EventDispatcher.TypedDelegate<DragStarted.DragStartedEvent>(this.OnDragStartedEvent));
			this.eventDispatcher = null;
		}

		// Token: 0x060069FB RID: 27131 RVA: 0x002926E1 File Offset: 0x002908E1
		private void OnDragStartedEvent(DragStarted.DragStartedEvent evt)
		{
			this.OnDragStarted(evt.DraggableObject);
		}

		// Token: 0x060069FC RID: 27132 RVA: 0x002926EF File Offset: 0x002908EF
		public virtual void OnDragStarted(Draggable2D draggableObject)
		{
			if (draggableObject == this.draggableObject)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x060069FD RID: 27133 RVA: 0x00292706 File Offset: 0x00290906
		public override string GetSummary()
		{
			if (this.draggableObject != null)
			{
				return this.draggableObject.name;
			}
			return "None";
		}

		// Token: 0x040059CE RID: 22990
		[SerializeField]
		protected Draggable2D draggableObject;

		// Token: 0x040059CF RID: 22991
		protected EventDispatcher eventDispatcher;

		// Token: 0x020016F1 RID: 5873
		public class DragStartedEvent
		{
			// Token: 0x06008899 RID: 34969 RVA: 0x002E997E File Offset: 0x002E7B7E
			public DragStartedEvent(Draggable2D draggableObject)
			{
				this.DraggableObject = draggableObject;
			}

			// Token: 0x04007470 RID: 29808
			public Draggable2D DraggableObject;
		}
	}
}
