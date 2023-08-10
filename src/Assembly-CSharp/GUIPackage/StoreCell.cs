using UnityEngine;

namespace GUIPackage;

public class StoreCell : MonoBehaviour
{
	public GameObject Icon;

	public GameObject Price;

	public GameObject Name;

	public int storeID;

	private void Update()
	{
		if (Singleton.store.store[storeID].itemID != -1)
		{
			Icon.GetComponent<UITexture>().mainTexture = (Texture)(object)Singleton.store.store[storeID].itemIcon;
			Price.GetComponent<UILabel>().text = Singleton.store.store[storeID].itemPrice.ToString();
			Name.GetComponent<UILabel>().text = Singleton.store.store[storeID].itemNameCN;
		}
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			Singleton.inventory.Show_Tooltip(Singleton.store.store[storeID]);
			Singleton.inventory.showTooltip = true;
		}
		else
		{
			Singleton.inventory.showTooltip = false;
		}
	}
}
