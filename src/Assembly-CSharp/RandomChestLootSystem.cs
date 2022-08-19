using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200012E RID: 302
public class RandomChestLootSystem : MonoBehaviour
{
	// Token: 0x06000E2D RID: 3629 RVA: 0x00054200 File Offset: 0x00052400
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

	// Token: 0x04000A2D RID: 2605
	public int amountOfChest = 10;

	// Token: 0x04000A2E RID: 2606
	public int minItemInChest = 2;

	// Token: 0x04000A2F RID: 2607
	public int maxItemInChest = 10;

	// Token: 0x04000A30 RID: 2608
	private static ItemDataBaseList inventoryItemList;

	// Token: 0x04000A31 RID: 2609
	public GameObject storageBox;

	// Token: 0x04000A32 RID: 2610
	private int counter;

	// Token: 0x04000A33 RID: 2611
	private int creatingItemsForChest;

	// Token: 0x04000A34 RID: 2612
	private int randomItemNumber;
}
