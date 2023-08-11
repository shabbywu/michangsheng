using UnityEngine;

namespace Fungus;

[EventHandlerInfo("Sprite", "Drag Started", "The block will execute when the player starts dragging an object.")]
[AddComponentMenu("")]
public class DragStarted : EventHandler
{
	public class DragStartedEvent
	{
		public Draggable2D DraggableObject;

		public DragStartedEvent(Draggable2D draggableObject)
		{
			DraggableObject = draggableObject;
		}
	}

	[SerializeField]
	protected Draggable2D draggableObject;

	protected EventDispatcher eventDispatcher;

	protected virtual void OnEnable()
	{
		eventDispatcher = FungusManager.Instance.EventDispatcher;
		eventDispatcher.AddListener<DragStartedEvent>(OnDragStartedEvent);
	}

	protected virtual void OnDisable()
	{
		eventDispatcher.RemoveListener<DragStartedEvent>(OnDragStartedEvent);
		eventDispatcher = null;
	}

	private void OnDragStartedEvent(DragStartedEvent evt)
	{
		OnDragStarted(evt.DraggableObject);
	}

	public virtual void OnDragStarted(Draggable2D draggableObject)
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
