using UnityEngine;
using UnityEngine.UI;

public class DesignForInventory : MonoBehaviour
{
	public Text inventoryTitle;

	public Image backgroundInventory;

	public Image backgroundSlot;

	public Text amountSlot;

	private void Start()
	{
		inventoryTitle = ((Component)((Component)this).transform.GetChild(0)).GetComponent<Text>();
		backgroundInventory = ((Component)this).GetComponent<Image>();
		backgroundSlot = ((Component)((Component)this).transform.GetChild(1).GetChild(0)).GetComponent<Image>();
		amountSlot = getTextAmountOfItem();
	}

	public Text getTextAmountOfItem()
	{
		for (int i = 0; i < ((Component)this).transform.GetChild(1).childCount; i++)
		{
			if (((Component)this).transform.GetChild(1).GetChild(i).childCount != 0)
			{
				return ((Component)((Component)this).transform.GetChild(1).GetChild(i).GetChild(0)
					.GetChild(1)).GetComponent<Text>();
			}
		}
		return null;
	}
}
