using UnityEngine;

[AddComponentMenu("NGUI/Examples/UI Equipment Slot")]
public class UIEquipmentSlot : UIItemSlot
{
	public InvEquipment equipment;

	public InvBaseItem.Slot slot;

	protected override InvGameItem observedItem
	{
		get
		{
			if (!((Object)(object)equipment != (Object)null))
			{
				return null;
			}
			return equipment.GetItem(slot);
		}
	}

	protected override InvGameItem Replace(InvGameItem item)
	{
		if (!((Object)(object)equipment != (Object)null))
		{
			return item;
		}
		return equipment.Replace(slot, item);
	}
}
