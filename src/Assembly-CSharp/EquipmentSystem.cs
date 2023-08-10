using UnityEngine;

public class EquipmentSystem : MonoBehaviour
{
	[SerializeField]
	public int slotsInTotal;

	[SerializeField]
	public ItemType[] itemTypeOfSlots = new ItemType[999];

	private void Start()
	{
		ConsumeItem.eS = ((Component)this).GetComponent<EquipmentSystem>();
	}

	public void getSlotsInTotal()
	{
		Inventory component = ((Component)this).GetComponent<Inventory>();
		slotsInTotal = component.width * component.height;
	}
}
