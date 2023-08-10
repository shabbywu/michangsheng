using UnityEngine;

namespace Fungus;

[EventHandlerInfo("Sprite", "Drag Exited", "The block will execute when the player is dragging an object which stops touching the target object.")]
[AddComponentMenu("")]
public class DragExited : EventHandler
{
	public class DragExitedEvent
	{
		public Draggable2D DraggableObject;

		public Collider2D TargetCollider;

		public DragExitedEvent(Draggable2D draggableObject, Collider2D targetCollider)
		{
			DraggableObject = draggableObject;
			TargetCollider = targetCollider;
		}
	}

	[Tooltip("Draggable object to listen for drag events on")]
	[SerializeField]
	protected Draggable2D draggableObject;

	[Tooltip("Drag target object to listen for drag events on")]
	[SerializeField]
	protected Collider2D targetObject;

	protected EventDispatcher eventDispatcher;

	protected virtual void OnEnable()
	{
		eventDispatcher = FungusManager.Instance.EventDispatcher;
		eventDispatcher.AddListener<DragExitedEvent>(OnDragEnteredEvent);
	}

	protected virtual void OnDisable()
	{
		eventDispatcher.RemoveListener<DragExitedEvent>(OnDragEnteredEvent);
		eventDispatcher = null;
	}

	private void OnDragEnteredEvent(DragExitedEvent evt)
	{
		OnDragExited(evt.DraggableObject, evt.TargetCollider);
	}

	public virtual void OnDragExited(Draggable2D draggableObject, Collider2D targetObject)
	{
		if ((Object)(object)draggableObject == (Object)(object)this.draggableObject && (Object)(object)targetObject == (Object)(object)this.targetObject)
		{
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
