using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000215 RID: 533
[Serializable]
public class Item
{
	// Token: 0x060010C4 RID: 4292 RVA: 0x000106F1 File Offset: 0x0000E8F1
	public Item()
	{
	}

	// Token: 0x060010C5 RID: 4293 RVA: 0x000A9BE4 File Offset: 0x000A7DE4
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

	// Token: 0x060010C6 RID: 4294 RVA: 0x0001071D File Offset: 0x0000E91D
	public Item getCopy()
	{
		return (Item)base.MemberwiseClone();
	}

	// Token: 0x060010C7 RID: 4295 RVA: 0x000A9C58 File Offset: 0x000A7E58
	public bool isEquipItem()
	{
		return this.itemType == ItemType.Weapon || this.itemType == ItemType.Head || this.itemType == ItemType.Shoe || this.itemType == ItemType.Chest || this.itemType == ItemType.HeadEquipment || this.itemType == ItemType.TorsoEquipment || this.itemType == ItemType.LegsEquipment || this.itemType == ItemType.FeetEquipment;
	}

	// Token: 0x060010C8 RID: 4296 RVA: 0x0001072A File Offset: 0x0000E92A
	public bool isConsumeItem()
	{
		return this.itemType == ItemType.Consumable;
	}

	// Token: 0x04000D48 RID: 3400
	public string itemName;

	// Token: 0x04000D49 RID: 3401
	public int itemID;

	// Token: 0x04000D4A RID: 3402
	public ulong itemUUID;

	// Token: 0x04000D4B RID: 3403
	public string itemDesc;

	// Token: 0x04000D4C RID: 3404
	public Sprite itemIcon;

	// Token: 0x04000D4D RID: 3405
	public GameObject itemModel;

	// Token: 0x04000D4E RID: 3406
	public int itemValue = 1;

	// Token: 0x04000D4F RID: 3407
	public ItemType itemType;

	// Token: 0x04000D50 RID: 3408
	public float itemWeight;

	// Token: 0x04000D51 RID: 3409
	public int maxStack = 1;

	// Token: 0x04000D52 RID: 3410
	public int indexItemInList = 999;

	// Token: 0x04000D53 RID: 3411
	public int rarity;

	// Token: 0x04000D54 RID: 3412
	public int itemIndex;

	// Token: 0x04000D55 RID: 3413
	[SerializeField]
	public List<ItemAttribute> itemAttributes = new List<ItemAttribute>();
}
