using UnityEngine;
using UnityEngine.EventSystems;

public class SplitItem : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	private bool pressingButtonToSplit;

	public Inventory inv;

	private static InputManager inputManagerDatabase;

	private void Update()
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		if (Input.GetKeyDown(inputManagerDatabase.SplitItem))
		{
			pressingButtonToSplit = true;
		}
		if (Input.GetKeyUp(inputManagerDatabase.SplitItem))
		{
			pressingButtonToSplit = false;
		}
	}

	private void Start()
	{
		inputManagerDatabase = (InputManager)(object)Resources.Load("InputManager");
	}

	public void OnPointerDown(PointerEventData data)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		inv = ((Component)((Component)this).transform.parent.parent.parent).GetComponent<Inventory>();
		if (!((Object)(object)((Component)((Component)this).transform.parent.parent.parent).GetComponent<Hotbar>() == (Object)null) || (int)data.button != 0 || !pressingButtonToSplit || !inv.stackable || inv.ItemsInInventory.Count >= inv.height * inv.width)
		{
			return;
		}
		ItemOnObject component = ((Component)this).GetComponent<ItemOnObject>();
		if (component.item.itemValue > 1)
		{
			int itemValue = component.item.itemValue;
			component.item.itemValue = component.item.itemValue / 2;
			itemValue -= component.item.itemValue;
			inv.addItemToInventory(component.item.itemID, itemValue);
			inv.stackableSettings();
			if ((Object)(object)((Component)this).GetComponent<ConsumeItem>().duplication != (Object)null)
			{
				GameObject duplication = ((Component)this).GetComponent<ConsumeItem>().duplication;
				duplication.GetComponent<ItemOnObject>().item.itemValue = component.item.itemValue;
				duplication.GetComponent<SplitItem>().inv.stackableSettings();
			}
			inv.updateItemList();
		}
	}
}
