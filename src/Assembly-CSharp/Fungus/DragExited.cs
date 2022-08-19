using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E99 RID: 3737
	[EventHandlerInfo("Sprite", "Drag Exited", "The block will execute when the player is dragging an object which stops touching the target object.")]
	[AddComponentMenu("")]
	public class DragExited : EventHandler
	{
		// Token: 0x060069F3 RID: 27123 RVA: 0x002925A8 File Offset: 0x002907A8
		protected virtual void OnEnable()
		{
			this.eventDispatcher = FungusManager.Instance.EventDispatcher;
			this.eventDispatcher.AddListener<DragExited.DragExitedEvent>(new EventDispatcher.TypedDelegate<DragExited.DragExitedEvent>(this.OnDragEnteredEvent));
		}

		// Token: 0x060069F4 RID: 27124 RVA: 0x002925D1 File Offset: 0x002907D1
		protected virtual void OnDisable()
		{
			this.eventDispatcher.RemoveListener<DragExited.DragExitedEvent>(new EventDispatcher.TypedDelegate<DragExited.DragExitedEvent>(this.OnDragEnteredEvent));
			this.eventDispatcher = null;
		}

		// Token: 0x060069F5 RID: 27125 RVA: 0x002925F1 File Offset: 0x002907F1
		private void OnDragEnteredEvent(DragExited.DragExitedEvent evt)
		{
			this.OnDragExited(evt.DraggableObject, evt.TargetCollider);
		}

		// Token: 0x060069F6 RID: 27126 RVA: 0x00292605 File Offset: 0x00290805
		public virtual void OnDragExited(Draggable2D draggableObject, Collider2D targetObject)
		{
			if (draggableObject == this.draggableObject && targetObject == this.targetObject)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x060069F7 RID: 27127 RVA: 0x0029262C File Offset: 0x0029082C
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

		// Token: 0x040059CB RID: 22987
		[Tooltip("Draggable object to listen for drag events on")]
		[SerializeField]
		protected Draggable2D draggableObject;

		// Token: 0x040059CC RID: 22988
		[Tooltip("Drag target object to listen for drag events on")]
		[SerializeField]
		protected Collider2D targetObject;

		// Token: 0x040059CD RID: 22989
		protected EventDispatcher eventDispatcher;

		// Token: 0x020016F0 RID: 5872
		public class DragExitedEvent
		{
			// Token: 0x06008898 RID: 34968 RVA: 0x002E9968 File Offset: 0x002E7B68
			public DragExitedEvent(Draggable2D draggableObject, Collider2D targetCollider)
			{
				this.DraggableObject = draggableObject;
				this.TargetCollider = targetCollider;
			}

			// Token: 0x0400746E RID: 29806
			public Draggable2D DraggableObject;

			// Token: 0x0400746F RID: 29807
			public Collider2D TargetCollider;
		}
	}
}
