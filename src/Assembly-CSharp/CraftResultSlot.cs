using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000127 RID: 295
public class CraftResultSlot : MonoBehaviour
{
	// Token: 0x06000E08 RID: 3592 RVA: 0x00052C98 File Offset: 0x00050E98
	private void Start()
	{
		this.craftSystem = base.transform.parent.GetComponent<CraftSystem>();
		this.itemGameObject = Object.Instantiate<GameObject>(Resources.Load("Prefabs/Item") as GameObject);
		this.itemGameObject.transform.SetParent(base.gameObject.transform);
		this.itemGameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
		this.itemGameObject.GetComponent<DragItem>().enabled = false;
		this.itemGameObject.SetActive(false);
		this.itemGameObject.transform.GetChild(1).GetComponent<Text>().enabled = true;
		this.itemGameObject.transform.GetChild(1).GetComponent<RectTransform>().localPosition = new Vector2((float)GameObject.FindGameObjectWithTag("MainInventory").GetComponent<Inventory>().positionNumberX, (float)GameObject.FindGameObjectWithTag("MainInventory").GetComponent<Inventory>().positionNumberY);
	}

	// Token: 0x06000E09 RID: 3593 RVA: 0x00052D90 File Offset: 0x00050F90
	private void Update()
	{
		if (this.craftSystem.possibleItems.Count != 0)
		{
			this.itemGameObject.GetComponent<ItemOnObject>().item = this.craftSystem.possibleItems[this.temp];
			this.itemGameObject.SetActive(true);
			return;
		}
		this.itemGameObject.SetActive(false);
	}

	// Token: 0x040009EF RID: 2543
	private CraftSystem craftSystem;

	// Token: 0x040009F0 RID: 2544
	public int temp;

	// Token: 0x040009F1 RID: 2545
	private GameObject itemGameObject;
}
