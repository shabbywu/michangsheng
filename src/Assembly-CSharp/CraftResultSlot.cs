using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001F8 RID: 504
public class CraftResultSlot : MonoBehaviour
{
	// Token: 0x06001016 RID: 4118 RVA: 0x000A3494 File Offset: 0x000A1694
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

	// Token: 0x06001017 RID: 4119 RVA: 0x000A358C File Offset: 0x000A178C
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

	// Token: 0x04000C87 RID: 3207
	private CraftSystem craftSystem;

	// Token: 0x04000C88 RID: 3208
	public int temp;

	// Token: 0x04000C89 RID: 3209
	private GameObject itemGameObject;
}
