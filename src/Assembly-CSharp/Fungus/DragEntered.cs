using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E98 RID: 3736
	[EventHandlerInfo("Sprite", "Drag Entered", "The block will execute when the player is dragging an object which starts touching the target object.")]
	[AddComponentMenu("")]
	public class DragEntered : EventHandler
	{
		// Token: 0x060069ED RID: 27117 RVA: 0x002924B8 File Offset: 0x002906B8
		protected virtual void OnEnable()
		{
			this.eventDispatcher = FungusManager.Instance.EventDispatcher;
			this.eventDispatcher.AddListener<DragEntered.DragEnteredEvent>(new EventDispatcher.TypedDelegate<DragEntered.DragEnteredEvent>(this.OnDragEnteredEvent));
		}

		// Token: 0x060069EE RID: 27118 RVA: 0x002924E1 File Offset: 0x002906E1
		protected virtual void OnDisable()
		{
			this.eventDispatcher.RemoveListener<DragEntered.DragEnteredEvent>(new EventDispatcher.TypedDelegate<DragEntered.DragEnteredEvent>(this.OnDragEnteredEvent));
			this.eventDispatcher = null;
		}

		// Token: 0x060069EF RID: 27119 RVA: 0x00292501 File Offset: 0x00290701
		private void OnDragEnteredEvent(DragEntered.DragEnteredEvent evt)
		{
			this.OnDragEntered(evt.DraggableObject, evt.TargetCollider);
		}

		// Token: 0x060069F0 RID: 27120 RVA: 0x00292515 File Offset: 0x00290715
		public virtual void OnDragEntered(Draggable2D draggableObject, Collider2D targetObject)
		{
			if (draggableObject == this.draggableObject && targetObject == this.targetObject)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x060069F1 RID: 27121 RVA: 0x0029253C File Offset: 0x0029073C
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

		// Token: 0x040059C8 RID: 22984
		[Tooltip("Draggable object to listen for drag events on")]
		[SerializeField]
		protected Draggable2D draggableObject;

		// Token: 0x040059C9 RID: 22985
		[Tooltip("Drag target object to listen for drag events on")]
		[SerializeField]
		protected Collider2D targetObject;

		// Token: 0x040059CA RID: 22986
		protected EventDispatcher eventDispatcher;

		// Token: 0x020016EF RID: 5871
		public class DragEnteredEvent
		{
			// Token: 0x06008897 RID: 34967 RVA: 0x002E9952 File Offset: 0x002E7B52
			public DragEnteredEvent(Draggable2D draggableObject, Collider2D targetCollider)
			{
				this.DraggableObject = draggableObject;
				this.TargetCollider = targetCollider;
			}

			// Token: 0x0400746C RID: 29804
			public Draggable2D DraggableObject;

			// Token: 0x0400746D RID: 29805
			public Collider2D TargetCollider;
		}
	}
}
