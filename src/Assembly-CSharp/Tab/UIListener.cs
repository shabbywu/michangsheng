using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Tab;

public class UIListener : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	public UnityEvent mouseUpEvent = new UnityEvent();

	public UnityEvent mouseDownEvent = new UnityEvent();

	public UnityEvent mouseEnterEvent = new UnityEvent();

	public UnityEvent mouseOutEvent = new UnityEvent();

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!eventData.dragging && mouseEnterEvent != null)
		{
			mouseEnterEvent.Invoke();
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (mouseOutEvent != null)
		{
			mouseOutEvent.Invoke();
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (mouseDownEvent != null)
		{
			mouseDownEvent.Invoke();
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (!eventData.dragging && mouseUpEvent != null)
		{
			mouseUpEvent.Invoke();
		}
	}
}
