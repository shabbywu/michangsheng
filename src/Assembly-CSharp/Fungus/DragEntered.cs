using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001313 RID: 4883
	[EventHandlerInfo("Sprite", "Drag Entered", "The block will execute when the player is dragging an object which starts touching the target object.")]
	[AddComponentMenu("")]
	public class DragEntered : EventHandler
	{
		// Token: 0x0600771A RID: 30490 RVA: 0x00051198 File Offset: 0x0004F398
		protected virtual void OnEnable()
		{
			this.eventDispatcher = FungusManager.Instance.EventDispatcher;
			this.eventDispatcher.AddListener<DragEntered.DragEnteredEvent>(new EventDispatcher.TypedDelegate<DragEntered.DragEnteredEvent>(this.OnDragEnteredEvent));
		}

		// Token: 0x0600771B RID: 30491 RVA: 0x000511C1 File Offset: 0x0004F3C1
		protected virtual void OnDisable()
		{
			this.eventDispatcher.RemoveListener<DragEntered.DragEnteredEvent>(new EventDispatcher.TypedDelegate<DragEntered.DragEnteredEvent>(this.OnDragEnteredEvent));
			this.eventDispatcher = null;
		}

		// Token: 0x0600771C RID: 30492 RVA: 0x000511E1 File Offset: 0x0004F3E1
		private void OnDragEnteredEvent(DragEntered.DragEnteredEvent evt)
		{
			this.OnDragEntered(evt.DraggableObject, evt.TargetCollider);
		}

		// Token: 0x0600771D RID: 30493 RVA: 0x000511F5 File Offset: 0x0004F3F5
		public virtual void OnDragEntered(Draggable2D draggableObject, Collider2D targetObject)
		{
			if (draggableObject == this.draggableObject && targetObject == this.targetObject)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x0600771E RID: 30494 RVA: 0x002B4EC8 File Offset: 0x002B30C8
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

		// Token: 0x040067D9 RID: 26585
		[Tooltip("Draggable object to listen for drag events on")]
		[SerializeField]
		protected Draggable2D draggableObject;

		// Token: 0x040067DA RID: 26586
		[Tooltip("Drag target object to listen for drag events on")]
		[SerializeField]
		protected Collider2D targetObject;

		// Token: 0x040067DB RID: 26587
		protected EventDispatcher eventDispatcher;

		// Token: 0x02001314 RID: 4884
		public class DragEnteredEvent
		{
			// Token: 0x06007720 RID: 30496 RVA: 0x0005121A File Offset: 0x0004F41A
			public DragEnteredEvent(Draggable2D draggableObject, Collider2D targetCollider)
			{
				this.DraggableObject = draggableObject;
				this.TargetCollider = targetCollider;
			}

			// Token: 0x040067DC RID: 26588
			public Draggable2D DraggableObject;

			// Token: 0x040067DD RID: 26589
			public Collider2D TargetCollider;
		}
	}
}
