using UnityEngine;
using UnityEngine.EventSystems;

public class LeftClick : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	private CraftResultSlot resultScript;

	private CraftSystem craftSystem;

	public void OnPointerDown(PointerEventData data)
	{
		if ((Object)(object)craftSystem == (Object)null)
		{
			craftSystem = ((Component)((Component)this).transform.parent).GetComponent<CraftSystem>();
			resultScript = ((Component)((Component)this).transform.parent.GetChild(3)).GetComponent<CraftResultSlot>();
		}
		if (craftSystem.possibleItems.Count > 1 && resultScript.temp >= 1)
		{
			resultScript.temp--;
		}
		else
		{
			resultScript.temp = craftSystem.possibleItems.Count - 1;
		}
	}
}
