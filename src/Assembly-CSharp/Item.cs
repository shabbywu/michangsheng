using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000140 RID: 320
[Serializable]
public class Item
{
	// Token: 0x06000EA4 RID: 3748 RVA: 0x000597CB File Offset: 0x000579CB
	public Item()
	{
	}

	// Token: 0x06000EA5 RID: 3749 RVA: 0x000597F8 File Offset: 0x000579F8
	public Item(string name, int id, string desc, Sprite icon, GameObject model, int maxStack, ItemType type, string sendmessagetext, List<ItemAttribute> itemAttributes)
	{
		this.itemName = name;
		this.itemID = id;
		this.itemDesc = desc;
		this.itemIcon = icon;
		this.itemModel = model;
		this.itemType = type;
		this.maxStack = maxStack;
		this.itemAttributes = itemAttributes;
	}

	// Token: 0x06000EA6 RID: 3750 RVA: 0x0005986C File Offset: 0x00057A6C
	public Item getCopy()
	{
		return (Item)base.MemberwiseClone();
	}

	// Token: 0x06000EA7 RID: 3751 RVA: 0x0005987C File Offset: 0x00057A7C
	public bool isEquipItem()
	{
		return this.itemType == ItemType.Weapon || this.itemType == ItemType.Head || this.itemType == ItemType.Shoe || this.itemType == ItemType.Chest || this.itemType == ItemType.HeadEquipment || this.itemType == ItemType.TorsoEquipment || this.itemType == ItemType.LegsEquipment || this.itemType == ItemType.FeetEquipment;
	}

	// Token: 0x06000EA8 RID: 3752 RVA: 0x000598D7 File Offset: 0x00057AD7
	public bool isConsumeItem()
	{
		return this.itemType == ItemType.Consumable;
	}

	// Token: 0x04000AAD RID: 2733
	public string itemName;

	// Token: 0x04000AAE RID: 2734
	public int itemID;

	// Token: 0x04000AAF RID: 2735
	public ulong itemUUID;

	// Token: 0x04000AB0 RID: 2736
	public string itemDesc;

	// Token: 0x04000AB1 RID: 2737
	public Sprite itemIcon;

	// Token: 0x04000AB2 RID: 2738
	public GameObject itemModel;

	// Token: 0x04000AB3 RID: 2739
	public int itemValue = 1;

	// Token: 0x04000AB4 RID: 2740
	public ItemType itemType;

	// Token: 0x04000AB5 RID: 2741
	public float itemWeight;

	// Token: 0x04000AB6 RID: 2742
	public int maxStack = 1;

	// Token: 0x04000AB7 RID: 2743
	public int indexItemInList = 999;

	// Token: 0x04000AB8 RID: 2744
	public int rarity;

	// Token: 0x04000AB9 RID: 2745
	public int itemIndex;

	// Token: 0x04000ABA RID: 2746
	[SerializeField]
	public List<ItemAttribute> itemAttributes = new List<ItemAttribute>();
}
