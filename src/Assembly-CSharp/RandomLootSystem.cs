using System;
using UnityEngine;

// Token: 0x0200012F RID: 303
public class RandomLootSystem : MonoBehaviour
{
	// Token: 0x06000E2F RID: 3631 RVA: 0x0005441C File Offset: 0x0005261C
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

	// Token: 0x04000A35 RID: 2613
	public int amountOfLoot = 10;

	// Token: 0x04000A36 RID: 2614
	private static ItemDataBaseList inventoryItemList;

	// Token: 0x04000A37 RID: 2615
	private int counter;
}
