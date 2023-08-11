using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Fungus;

public class Draggable2D : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
	[Tooltip("Is object dragging enabled")]
	[SerializeField]
	protected bool dragEnabled = true;

	[Tooltip("Move object back to its starting position when drag is cancelled")]
	[FormerlySerializedAs("returnToStartPos")]
	[SerializeField]
	protected bool returnOnCancelled = true;

	[Tooltip("Move object back to its starting position when drag is completed")]
	[SerializeField]
	protected bool returnOnCompleted = true;

	[Tooltip("Time object takes to return to its starting position")]
	[SerializeField]
	protected float returnDuration = 1f;

	[Tooltip("Mouse texture to use when hovering mouse over object")]
	[SerializeField]
	protected Texture2D hoverCursor;

	[Tooltip("Use the UI Event System to check for drag events. Clicks that hit an overlapping UI object will be ignored. Camera must have a PhysicsRaycaster component, or a Physics2DRaycaster for 2D colliders.")]
	[SerializeField]
	protected bool useEventSystem;

	protected Vector3 startingPosition;

	protected bool updatePosition;

	protected Vector3 newPosition;

	protected Vector3 delta = Vector3.zero;

	protected List<DragCompleted> dragCompletedHandlers = new List<DragCompleted>();

	public virtual bool DragEnabled
	{
		get
		{
			return dragEnabled;
		}
		set
		{
			dragEnabled = value;
		}
	}

	public void RegisterHandler(DragCompleted handler)
	{
		dragCompletedHandlers.Add(handler);
	}

	public void UnregisterHandler(DragCompleted handler)
	{
		if (dragCompletedHandlers.Contains(handler))
		{
			dragCompletedHandlers.Remove(handler);
		}
	}

	protected virtual void LateUpdate()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (updatePosition)
		{
			((Component)this).transform.position = newPosition;
			updatePosition = false;
		}
	}

	protected virtual void OnTriggerEnter2D(Collider2D other)
	{
		if (dragEnabled)
		{
			FungusManager.Instance.EventDispatcher.Raise(new DragEntered.DragEnteredEvent(this, other));
		}
	}

	protected virtual void OnTriggerExit2D(Collider2D other)
	{
		if (dragEnabled)
		{
			FungusManager.Instance.EventDispatcher.Raise(new DragExited.DragExitedEvent(this, other));
		}
	}

	protected virtual void DoBeginDrag()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		float x = Input.mousePosition.x;
		float y = Input.mousePosition.y;
		delta = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 10f)) - ((Component)this).transform.position;
		delta.z = 0f;
		startingPosition = ((Component)this).transform.position;
		FungusManager.Instance.EventDispatcher.Raise(new DragStarted.DragStartedEvent(this));
	}

	protected virtual void DoDrag()
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		if (dragEnabled)
		{
			float x = Input.mousePosition.x;
			float y = Input.mousePosition.y;
			float z = ((Component)this).transform.position.z;
			newPosition = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 10f)) - delta;
			newPosition.z = z;
			updatePosition = true;
		}
	}

	protected virtual void DoEndDrag()
	{
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		if (!dragEnabled)
		{
			return;
		}
		EventDispatcher eventDispatcher = FungusManager.Instance.EventDispatcher;
		bool flag = false;
		for (int i = 0; i < dragCompletedHandlers.Count; i++)
		{
			DragCompleted dragCompleted = dragCompletedHandlers[i];
			if ((Object)(object)dragCompleted != (Object)null && (Object)(object)dragCompleted.DraggableObject == (Object)(object)this && dragCompleted.IsOverTarget())
			{
				flag = true;
				eventDispatcher.Raise(new DragCompleted.DragCompletedEvent(this));
			}
		}
		if (!flag)
		{
			eventDispatcher.Raise(new DragCancelled.DragCancelledEvent(this));
			if (returnOnCancelled)
			{
				LeanTween.move(((Component)this).gameObject, startingPosition, returnDuration).setEase(LeanTweenType.easeOutExpo);
			}
		}
		else if (returnOnCompleted)
		{
			LeanTween.move(((Component)this).gameObject, startingPosition, returnDuration).setEase(LeanTweenType.easeOutExpo);
		}
	}

	protected virtual void DoPointerEnter()
	{
		ChangeCursor(hoverCursor);
	}

	protected virtual void DoPointerExit()
	{
		SetMouseCursor.ResetMouseCursor();
	}

	protected virtual void ChangeCursor(Texture2D cursorTexture)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		if (dragEnabled)
		{
			Cursor.SetCursor(cursorTexture, Vector2.zero, (CursorMode)0);
		}
	}

	protected virtual void OnMouseDown()
	{
		if (!useEventSystem)
		{
			DoBeginDrag();
		}
	}

	protected virtual void OnMouseDrag()
	{
		if (!useEventSystem)
		{
			DoDrag();
		}
	}

	protected virtual void OnMouseUp()
	{
		if (!useEventSystem)
		{
			DoEndDrag();
		}
	}

	protected virtual void OnMouseEnter()
	{
		if (!useEventSystem)
		{
			DoPointerEnter();
		}
	}

	protected virtual void OnMouseExit()
	{
		if (!useEventSystem)
		{
			DoPointerExit();
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (useEventSystem)
		{
			DoBeginDrag();
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (useEventSystem)
		{
			DoDrag();
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (useEventSystem)
		{
			DoEndDrag();
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (useEventSystem)
		{
			DoPointerEnter();
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (useEventSystem)
		{
			DoPointerExit();
		}
	}
}
