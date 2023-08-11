using System.Collections.Generic;
using UnityEngine;

public class RandomChestLootSystem : MonoBehaviour
{
	public int amountOfChest = 10;

	public int minItemInChest = 2;

	public int maxItemInChest = 10;

	private static ItemDataBaseList inventoryItemList;

	public GameObject storageBox;

	private int counter;

	private int creatingItemsForChest;

	private int randomItemNumber;

	private void Start()
	{
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		inventoryItemList = (ItemDataBaseList)(object)Resources.Load("ItemDatabase");
		while (counter < amountOfChest)
		{
			counter++;
			creatingItemsForChest = 0;
			int num = Random.Range(minItemInChest, maxItemInChest);
			List<Item> list = new List<Item>();
			while (creatingItemsForChest < num)
			{
				randomItemNumber = Random.Range(1, inventoryItemList.itemList.Count - 1);
				if (Random.Range(1, 100) <= inventoryItemList.itemList[randomItemNumber].rarity)
				{
					list.Add(inventoryItemList.itemList[randomItemNumber].getCopy());
					creatingItemsForChest++;
				}
			}
			Terrain activeTerrain = Terrain.activeTerrain;
			float num2 = Random.Range(5f, activeTerrain.terrainData.size.x - 5f);
			float num3 = Random.Range(5f, activeTerrain.terrainData.size.z - 5f);
			float height = activeTerrain.terrainData.GetHeight((int)num2, (int)num3);
			GameObject val = Object.Instantiate<GameObject>(storageBox);
			StorageInventory component = val.GetComponent<StorageInventory>();
			component.inventory = GameObject.FindGameObjectWithTag("Storage");
			for (int i = 0; i < list.Count; i++)
			{
				component.storageItems.Add(inventoryItemList.getItemByID(list[i].itemID));
				int itemValue = Random.Range(1, component.storageItems[component.storageItems.Count - 1].maxStack);
				component.storageItems[component.storageItems.Count - 1].itemValue = itemValue;
			}
			val.transform.localPosition = new Vector3(num2, height + 2f, num3);
		}
	}
}
