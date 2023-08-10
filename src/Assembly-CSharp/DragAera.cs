using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DragAera : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
{
	public UnityAction<PointerEventData> OnBeginDragAction;

	public UnityAction<PointerEventData> OnDragAction;

	public UnityAction<PointerEventData> OnEndDragAction;

	public RectTransform RT;

	private void Start()
	{
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (OnBeginDragAction != null)
		{
			OnBeginDragAction.Invoke(eventData);
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (OnDragAction != null)
		{
			OnDragAction.Invoke(eventData);
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (OnEndDragAction != null)
		{
			OnEndDragAction.Invoke(eventData);
		}
	}
}
