using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
	public string itemName;

	public int itemID;

	public ulong itemUUID;

	public string itemDesc;

	public Sprite itemIcon;

	public GameObject itemModel;

	public int itemValue = 1;

	public ItemType itemType;

	public float itemWeight;

	public int maxStack = 1;

	public int indexItemInList = 999;

	public int rarity;

	public int itemIndex;

	[SerializeField]
	public List<ItemAttribute> itemAttributes = new List<ItemAttribute>();

	public Item()
	{
	}

	public Item(string name, int id, string desc, Sprite icon, GameObject model, int maxStack, ItemType type, string sendmessagetext, List<ItemAttribute> itemAttributes)
	{
		itemName = name;
		itemID = id;
		itemDesc = desc;
		itemIcon = icon;
		itemModel = model;
		itemType = type;
		this.maxStack = maxStack;
		this.itemAttributes = itemAttributes;
	}

	public Item getCopy()
	{
		return (Item)MemberwiseClone();
	}

	public bool isEquipItem()
	{
		if (itemType != ItemType.Weapon && itemType != ItemType.Head && itemType != ItemType.Shoe && itemType != ItemType.Chest && itemType != ItemType.HeadEquipment && itemType != ItemType.TorsoEquipment && itemType != ItemType.LegsEquipment)
		{
			return itemType == ItemType.FeetEquipment;
		}
		return true;
	}

	public bool isConsumeItem()
	{
		return itemType == ItemType.Consumable;
	}
}
