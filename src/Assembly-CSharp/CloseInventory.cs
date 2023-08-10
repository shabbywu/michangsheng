using UnityEngine;
using UnityEngine.EventSystems;

public class CloseInventory : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	private Inventory inv;

	private void Start()
	{
		inv = ((Component)((Component)this).transform.parent).GetComponent<Inventory>();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if ((int)eventData.button == 0)
		{
			inv.closeInventory();
		}
	}
}
