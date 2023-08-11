using System.Collections.Generic;
using GUIPackage;
using UnityEngine;

public class InitLinWu : MonoBehaviour
{
	public Inventory2 inventory;

	public KeyCell keyCell;

	public UIPopupList mList;

	private void Start()
	{
		updateItem();
	}

	public void updateItem()
	{
		for (int i = 0; i < (int)inventory.count; i++)
		{
			inventory.inventory.Add(new item());
		}
		inventory = ((Component)this).GetComponent<Inventory2>();
		setItem1();
		if (inventory.inventory[0].itemID != -1)
		{
			keyCell.keyItem = inventory.inventory[0];
		}
	}

	public void setItem1()
	{
		inventory.setItemLeiXin(new List<int> { 3, 4, 13 });
	}

	public void setItem2()
	{
		inventory.setItemLeiXin(new List<int> { 3 });
	}

	public void setItem3()
	{
		inventory.setItemLeiXin(new List<int> { 4 });
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

	public void chengeList()
	{
		Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
		dictionary[0] = new List<int> { 3, 4 };
		dictionary[1] = new List<int> { 3 };
		dictionary[2] = new List<int> { 4 };
		inventory.setItemLeiXin(dictionary[getInputID(mList.value)]);
	}

	private void Update()
	{
	}
}
