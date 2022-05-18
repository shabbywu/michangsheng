using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001317 RID: 4887
	[EventHandlerInfo("Sprite", "Drag Started", "The block will execute when the player starts dragging an object.")]
	[AddComponentMenu("")]
	public class DragStarted : EventHandler
	{
		// Token: 0x06007728 RID: 30504 RVA: 0x000512C8 File Offset: 0x0004F4C8
		protected virtual void OnEnable()
		{
			this.eventDispatcher = FungusManager.Instance.EventDispatcher;
			this.eventDispatcher.AddListener<DragStarted.DragStartedEvent>(new EventDispatcher.TypedDelegate<DragStarted.DragStartedEvent>(this.OnDragStartedEvent));
		}

		// Token: 0x06007729 RID: 30505 RVA: 0x000512F1 File Offset: 0x0004F4F1
		protected virtual void OnDisable()
		{
			this.eventDispatcher.RemoveListener<DragStarted.DragStartedEvent>(new EventDispatcher.TypedDelegate<DragStarted.DragStartedEvent>(this.OnDragStartedEvent));
			this.eventDispatcher = null;
		}

		// Token: 0x0600772A RID: 30506 RVA: 0x00051311 File Offset: 0x0004F511
		private void OnDragStartedEvent(DragStarted.DragStartedEvent evt)
		{
			this.OnDragStarted(evt.DraggableObject);
		}

		// Token: 0x0600772B RID: 30507 RVA: 0x0005131F File Offset: 0x0004F51F
		public virtual void OnDragStarted(Draggable2D draggableObject)
		{
			if (draggableObject == this.draggableObject)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x0600772C RID: 30508 RVA: 0x00051336 File Offset: 0x0004F536
		public override string GetSummary()
		{
			if (this.draggableObject != null)
			{
				return this.draggableObject.name;
			}
			return "None";
		}

		// Token: 0x040067E3 RID: 26595
		[SerializeField]
		protected Draggable2D draggableObject;

		// Token: 0x040067E4 RID: 26596
		protected EventDispatcher eventDispatcher;

		// Token: 0x02001318 RID: 4888
		public class DragStartedEvent
		{
			// Token: 0x0600772E RID: 30510 RVA: 0x00051357 File Offset: 0x0004F557
			public DragStartedEvent(Draggable2D draggableObject)
			{
				this.DraggableObject = draggableObject;
			}

			// Token: 0x040067E5 RID: 26597
			public Draggable2D DraggableObject;
		}
	}
}
