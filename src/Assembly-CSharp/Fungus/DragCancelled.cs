using UnityEngine;

namespace Fungus;

[EventHandlerInfo("Sprite", "Drag Cancelled", "The block will execute when the player drags an object and releases it without dropping it on a target object.")]
[AddComponentMenu("")]
public class DragCancelled : EventHandler
{
	public class DragCancelledEvent
	{
		public Draggable2D DraggableObject;

		public DragCancelledEvent(Draggable2D draggableObject)
		{
			DraggableObject = draggableObject;
		}
	}

	[Tooltip("Draggable object to listen for drag events on")]
	[SerializeField]
	protected Draggable2D draggableObject;

	protected EventDispatcher eventDispatcher;

	protected virtual void OnEnable()
	{
		eventDispatcher = FungusManager.Instance.EventDispatcher;
		eventDispatcher.AddListener<DragCancelledEvent>(OnDragCancelledEvent);
	}

	protected virtual void OnDisable()
	{
		eventDispatcher.RemoveListener<DragCancelledEvent>(OnDragCancelledEvent);
		eventDispatcher = null;
	}

	protected virtual void OnDragCancelledEvent(DragCancelledEvent evt)
	{
		OnDragCancelled(evt.DraggableObject);
	}

	public virtual void OnDragCancelled(Draggable2D draggableObject)
	{
		if ((Object)(object)draggableObject == (Object)(object)this.draggableObject)
		{
			ExecuteBlock();
		}
	}

	public override string GetSummary()
	{
		if ((Object)(object)draggableObject != (Object)null)
		{
			return ((Object)draggableObject).name;
		}
		return "None";
	}
}
