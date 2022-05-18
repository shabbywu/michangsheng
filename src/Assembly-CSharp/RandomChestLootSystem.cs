using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001FF RID: 511
public class RandomChestLootSystem : MonoBehaviour
{
	// Token: 0x0600103B RID: 4155 RVA: 0x000A48C0 File Offset: 0x000A2AC0
	private void Start()
	{
		RandomChestLootSystem.inventoryItemList = (ItemDataBaseList)Resources.Load("ItemDatabase");
		while (this.counter < this.amountOfChest)
		{
			this.counter++;
			this.creatingItemsForChest = 0;
			int num = Random.Range(this.minItemInChest, this.maxItemInChest);
			List<Item> list = new List<Item>();
			while (this.creatingItemsForChest < num)
			{
				this.randomItemNumber = Random.Range(1, RandomChestLootSystem.inventoryItemList.itemList.Count - 1);
				if (Random.Range(1, 100) <= RandomChestLootSystem.inventoryItemList.itemList[this.randomItemNumber].rarity)
				{
					list.Add(RandomChestLootSystem.inventoryItemList.itemList[this.randomItemNumber].getCopy());
					this.creatingItemsForChest++;
				}
			}
			Terrain activeTerrain = Terrain.activeTerrain;
			float num2 = Random.Range(5f, activeTerrain.terrainData.size.x - 5f);
			float num3 = Random.Range(5f, activeTerrain.terrainData.size.z - 5f);
			float height = activeTerrain.terrainData.GetHeight((int)num2, (int)num3);
			GameObject gameObject = Object.Instantiate<GameObject>(this.storageBox);
			StorageInventory component = gameObject.GetComponent<StorageInventory>();
			component.inventory = GameObject.FindGameObjectWithTag("Storage");
			for (int i = 0; i < list.Count; i++)
			{
				component.storageItems.Add(RandomChestLootSystem.inventoryItemList.getItemByID(list[i].itemID));
				int itemValue = Random.Range(1, component.storageItems[component.storageItems.Count - 1].maxStack);
				component.storageItems[component.storageItems.Count - 1].itemValue = itemValue;
			}
			gameObject.transform.localPosition = new Vector3(num2, height + 2f, num3);
		}
	}

	// Token: 0x04000CC5 RID: 3269
	public int amountOfChest = 10;

	// Token: 0x04000CC6 RID: 3270
	public int minItemInChest = 2;

	// Token: 0x04000CC7 RID: 3271
	public int maxItemInChest = 10;

	// Token: 0x04000CC8 RID: 3272
	private static ItemDataBaseList inventoryItemList;

	// Token: 0x04000CC9 RID: 3273
	public GameObject storageBox;

	// Token: 0x04000CCA RID: 3274
	private int counter;

	// Token: 0x04000CCB RID: 3275
	private int creatingItemsForChest;

	// Token: 0x04000CCC RID: 3276
	private int randomItemNumber;
}
