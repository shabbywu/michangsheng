using UnityEngine;
using UnityEngine.EventSystems;

public class BtnISShow : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public bool isIn;

	public void OnPointerEnter(PointerEventData eventData)
	{
		isIn = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		isIn = false;
	}
}
