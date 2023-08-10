using UnityEngine;

public class RandomLootSystem : MonoBehaviour
{
	public int amountOfLoot = 10;

	private static ItemDataBaseList inventoryItemList;

	private int counter;

	private void Start()
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		inventoryItemList = (ItemDataBaseList)(object)Resources.Load("ItemDatabase");
		while (counter < amountOfLoot)
		{
			counter++;
			int index = Random.Range(1, inventoryItemList.itemList.Count - 1);
			Terrain activeTerrain = Terrain.activeTerrain;
			float num = Random.Range(5f, activeTerrain.terrainData.size.x - 5f);
			float num2 = Random.Range(5f, activeTerrain.terrainData.size.z - 5f);
			if ((Object)(object)inventoryItemList.itemList[index].itemModel == (Object)null)
			{
				counter--;
				continue;
			}
			GameObject obj = Object.Instantiate<GameObject>(inventoryItemList.itemList[index].itemModel);
			obj.AddComponent<PickUpItem>().item = inventoryItemList.itemList[index];
			obj.transform.localPosition = new Vector3(num, 0f, num2);
		}
	}
}
