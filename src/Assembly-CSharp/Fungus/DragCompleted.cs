using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E97 RID: 3735
	[EventHandlerInfo("Sprite", "Drag Completed", "The block will execute when the player drags an object and successfully drops it on a target object.")]
	[AddComponentMenu("")]
	public class DragCompleted : EventHandler
	{
		// Token: 0x060069E1 RID: 27105 RVA: 0x00292288 File Offset: 0x00290488
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

		// Token: 0x060069E2 RID: 27106 RVA: 0x00292304 File Offset: 0x00290504
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

		// Token: 0x060069E3 RID: 27107 RVA: 0x00292377 File Offset: 0x00290577
		private void OnDragCompletedEvent(DragCompleted.DragCompletedEvent evt)
		{
			this.OnDragCompleted(evt.DraggableObject);
		}

		// Token: 0x060069E4 RID: 27108 RVA: 0x00292385 File Offset: 0x00290585
		private void OnDragEnteredEvent(DragEntered.DragEnteredEvent evt)
		{
			this.OnDragEntered(evt.DraggableObject, evt.TargetCollider);
		}

		// Token: 0x060069E5 RID: 27109 RVA: 0x00292399 File Offset: 0x00290599
		private void OnDragExitedEvent(DragExited.DragExitedEvent evt)
		{
			this.OnDragExited(evt.DraggableObject, evt.TargetCollider);
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x060069E6 RID: 27110 RVA: 0x002923AD File Offset: 0x002905AD
		public virtual Draggable2D DraggableObject
		{
			get
			{
				return this.draggableObject;
			}
		}

		// Token: 0x060069E7 RID: 27111 RVA: 0x002923B5 File Offset: 0x002905B5
		public virtual bool IsOverTarget()
		{
			return this.overTarget;
		}

		// Token: 0x060069E8 RID: 27112 RVA: 0x002923BD File Offset: 0x002905BD
		public virtual void OnDragEntered(Draggable2D draggableObject, Collider2D targetObject)
		{
			if (this.targetObject != null && draggableObject == this.draggableObject && targetObject == this.targetObject)
			{
				this.overTarget = true;
			}
		}

		// Token: 0x060069E9 RID: 27113 RVA: 0x002923F0 File Offset: 0x002905F0
		public virtual void OnDragExited(Draggable2D draggableObject, Collider2D targetObject)
		{
			if (this.targetObject != null && draggableObject == this.draggableObject && targetObject == this.targetObject)
			{
				this.overTarget = false;
			}
		}

		// Token: 0x060069EA RID: 27114 RVA: 0x00292423 File Offset: 0x00290623
		public virtual void OnDragCompleted(Draggable2D draggableObject)
		{
			if (draggableObject == this.draggableObject && this.overTarget)
			{
				this.overTarget = false;
				this.ExecuteBlock();
			}
		}

		// Token: 0x060069EB RID: 27115 RVA: 0x0029244C File Offset: 0x0029064C
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

		// Token: 0x040059C4 RID: 22980
		[Tooltip("Draggable object to listen for drag events on")]
		[SerializeField]
		protected Draggable2D draggableObject;

		// Token: 0x040059C5 RID: 22981
		[Tooltip("Drag target object to listen for drag events on")]
		[SerializeField]
		protected Collider2D targetObject;

		// Token: 0x040059C6 RID: 22982
		protected bool overTarget;

		// Token: 0x040059C7 RID: 22983
		protected EventDispatcher eventDispatcher;

		// Token: 0x020016EE RID: 5870
		public class DragCompletedEvent
		{
			// Token: 0x06008896 RID: 34966 RVA: 0x002E9943 File Offset: 0x002E7B43
			public DragCompletedEvent(Draggable2D draggableObject)
			{
				this.DraggableObject = draggableObject;
			}

			// Token: 0x0400746B RID: 29803
			public Draggable2D DraggableObject;
		}
	}
}
