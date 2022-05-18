using System;
using UnityEngine;

// Token: 0x02000200 RID: 512
public class RandomLootSystem : MonoBehaviour
{
	// Token: 0x0600103D RID: 4157 RVA: 0x000A4ABC File Offset: 0x000A2CBC
	private void Start()
	{
		RandomLootSystem.inventoryItemList = (ItemDataBaseList)Resources.Load("ItemDatabase");
		while (this.counter < this.amountOfLoot)
		{
			this.counter++;
			int index = Random.Range(1, RandomLootSystem.inventoryItemList.itemList.Count - 1);
			Terrain activeTerrain = Terrain.activeTerrain;
			float num = Random.Range(5f, activeTerrain.terrainData.size.x - 5f);
			float num2 = Random.Range(5f, activeTerrain.terrainData.size.z - 5f);
			if (RandomLootSystem.inventoryItemList.itemList[index].itemModel == null)
			{
				this.counter--;
			}
			else
			{
				GameObject gameObject = Object.Instantiate<GameObject>(RandomLootSystem.inventoryItemList.itemList[index].itemModel);
				gameObject.AddComponent<PickUpItem>().item = RandomLootSystem.inventoryItemList.itemList[index];
				gameObject.transform.localPosition = new Vector3(num, 0f, num2);
			}
		}
	}

	// Token: 0x04000CCD RID: 3277
	public int amountOfLoot = 10;

	// Token: 0x04000CCE RID: 3278
	private static ItemDataBaseList inventoryItemList;

	// Token: 0x04000CCF RID: 3279
	private int counter;
}
