using System.Collections.Generic;
using UnityEngine;

public class ItemDataBaseList : ScriptableObject
{
	[SerializeField]
	public List<Item> itemList = new List<Item>();

	public Item getItemByID(int id)
	{
		for (int i = 0; i < itemList.Count; i++)
		{
			if (itemList[i].itemID == id)
			{
				return itemList[i].getCopy();
			}
		}
		return null;
	}

	public Item getItemByName(string name)
	{
		for (int i = 0; i < itemList.Count; i++)
		{
			if (itemList[i].itemName.ToLower().Equals(name.ToLower()))
			{
				return itemList[i].getCopy();
			}
		}
		return null;
	}
}
