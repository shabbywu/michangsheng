using UnityEngine;

namespace Fungus;

[EventHandlerInfo("Sprite", "Drag Completed", "The block will execute when the player drags an object and successfully drops it on a target object.")]
[AddComponentMenu("")]
public class DragCompleted : EventHandler
{
	public class DragCompletedEvent
	{
		public Draggable2D DraggableObject;

		public DragCompletedEvent(Draggable2D draggableObject)
		{
			DraggableObject = draggableObject;
		}
	}

	[Tooltip("Draggable object to listen for drag events on")]
	[SerializeField]
	protected Draggable2D draggableObject;

	[Tooltip("Drag target object to listen for drag events on")]
	[SerializeField]
	protected Collider2D targetObject;

	protected bool overTarget;

	protected EventDispatcher eventDispatcher;

	public virtual Draggable2D DraggableObject => draggableObject;

	protected virtual void OnEnable()
	{
		eventDispatcher = FungusManager.Instance.EventDispatcher;
		eventDispatcher.AddListener<DragCompletedEvent>(OnDragCompletedEvent);
		eventDispatcher.AddListener<DragEntered.DragEnteredEvent>(OnDragEnteredEvent);
		eventDispatcher.AddListener<DragExited.DragExitedEvent>(OnDragExitedEvent);
		if ((Object)(object)draggableObject != (Object)null)
		{
			draggableObject.RegisterHandler(this);
		}
	}

	protected virtual void OnDisable()
	{
		eventDispatcher.RemoveListener<DragCompletedEvent>(OnDragCompletedEvent);
		eventDispatcher.RemoveListener<DragEntered.DragEnteredEvent>(OnDragEnteredEvent);
		eventDispatcher.RemoveListener<DragExited.DragExitedEvent>(OnDragExitedEvent);
		if ((Object)(object)draggableObject != (Object)null)
		{
			draggableObject.UnregisterHandler(this);
		}
		eventDispatcher = null;
	}

	private void OnDragCompletedEvent(DragCompletedEvent evt)
	{
		OnDragCompleted(evt.DraggableObject);
	}

	private void OnDragEnteredEvent(DragEntered.DragEnteredEvent evt)
	{
		OnDragEntered(evt.DraggableObject, evt.TargetCollider);
	}

	private void OnDragExitedEvent(DragExited.DragExitedEvent evt)
	{
		OnDragExited(evt.DraggableObject, evt.TargetCollider);
	}

	public virtual bool IsOverTarget()
	{
		return overTarget;
	}

	public virtual void OnDragEntered(Draggable2D draggableObject, Collider2D targetObject)
	{
		if ((Object)(object)this.targetObject != (Object)null && (Object)(object)draggableObject == (Object)(object)this.draggableObject && (Object)(object)targetObject == (Object)(object)this.targetObject)
		{
			overTarget = true;
		}
	}

	public virtual void OnDragExited(Draggable2D draggableObject, Collider2D targetObject)
	{
		if ((Object)(object)this.targetObject != (Object)null && (Object)(object)draggableObject == (Object)(object)this.draggableObject && (Object)(object)targetObject == (Object)(object)this.targetObject)
		{
			overTarget = false;
		}
	}

	public virtual void OnDragCompleted(Draggable2D draggableObject)
	{
		if ((Object)(object)draggableObject == (Object)(object)this.draggableObject && overTarget)
		{
			overTarget = false;
			ExecuteBlock();
		}
	}

	public override string GetSummary()
	{
		string text = "";
		if ((Object)(object)draggableObject != (Object)null)
		{
			text = text + "\nDraggable: " + ((Object)draggableObject).name;
		}
		if ((Object)(object)targetObject != (Object)null)
		{
			text = text + "\nTarget: " + ((Object)targetObject).name;
		}
		if (text.Length == 0)
		{
			return "None";
		}
		return text;
	}
}
