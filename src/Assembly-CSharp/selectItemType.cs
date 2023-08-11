using GUIPackage;
using UnityEngine;

public class selectItemType : MonoBehaviour
{
	private UIPopupList mList;

	public Inventory2 inventory;

	public CaiLiaoInventory caiLiaoInventory;

	private void Start()
	{
		mList = ((Component)this).GetComponent<UIPopupList>();
		EventDelegate.Add(mList.onChange, OnChange);
	}

	public int getInputID(string name)
	{
		int num = 0;
		foreach (string item in mList.items)
		{
			if (name == item)
			{
				break;
			}
			num++;
		}
		return num;
	}

	private void OnChange()
	{
		inventory.inventoryItemType = getInputID(mList.value);
		if (inventory.ISPlayer)
		{
			if ((Object)(object)caiLiaoInventory != (Object)null)
			{
				caiLiaoInventory.Quaily = getInputID(mList.value);
				caiLiaoInventory.LoadCaiLiaoInventory();
			}
			else
			{
				inventory.LoadInventory();
			}
		}
		else
		{
			ExchangePlan component = GameObject.Find("UI Root (2D)/exchangePlan").GetComponent<ExchangePlan>();
			inventory.MonstarLoadInventory(component.MonstarID);
		}
	}
}
