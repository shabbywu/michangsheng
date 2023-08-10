using UnityEngine;

public class Hotbar : MonoBehaviour
{
	[SerializeField]
	public KeyCode[] keyCodesForSlots = (KeyCode[])(object)new KeyCode[999];

	[SerializeField]
	public int slotsInTotal;

	private void Update()
	{
		for (int i = 0; i < slotsInTotal; i++)
		{
			if (Input.GetKeyDown(keyCodesForSlots[i]) && ((Component)this).transform.GetChild(1).GetChild(i).childCount != 0 && ((Component)((Component)this).transform.GetChild(1).GetChild(i).GetChild(0)).GetComponent<ItemOnObject>().item.itemType != ItemType.UFPS_Ammo)
			{
				if ((Object)(object)((Component)((Component)this).transform.GetChild(1).GetChild(i).GetChild(0)).GetComponent<ConsumeItem>().duplication != (Object)null && ((Component)((Component)this).transform.GetChild(1).GetChild(i).GetChild(0)).GetComponent<ItemOnObject>().item.maxStack == 1)
				{
					Object.Destroy((Object)(object)((Component)((Component)this).transform.GetChild(1).GetChild(i).GetChild(0)).GetComponent<ConsumeItem>().duplication);
				}
				((Component)((Component)this).transform.GetChild(1).GetChild(i).GetChild(0)).GetComponent<ConsumeItem>().consumeIt();
			}
		}
	}

	public int getSlotsInTotal()
	{
		Inventory component = ((Component)this).GetComponent<Inventory>();
		return slotsInTotal = component.width * component.height;
	}
}
