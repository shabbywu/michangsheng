using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseDropdown : Dropdown
{
	public override void OnPointerUp(PointerEventData eventData)
	{
		((Selectable)this).OnPointerUp(eventData);
		((Selectable)this).image.overrideSprite = null;
	}

	public override void OnPointerExit(PointerEventData eventData)
	{
		((Selectable)this).OnPointerExit(eventData);
	}
}
