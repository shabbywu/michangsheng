using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000128 RID: 296
public class CraftSystem : MonoBehaviour
{
	// Token: 0x06000E0B RID: 3595 RVA: 0x00052DEE File Offset: 0x00050FEE
	private void Start()
	{
		this.blueprintDatabase = (BlueprintDatabase)Resources.Load("BlueprintDatabase");
	}

	// Token: 0x06000E0C RID: 3596 RVA: 0x00052E05 File Offset: 0x00051005
	private void Update()
	{
		this.ListWithItem();
	}

	// Token: 0x06000E0D RID: 3597 RVA: 0x00052E10 File Offset: 0x00051010
	public void setImages()
	{
		this.finalSlotImage = base.transform.GetChild(3).GetComponent<Image>();
		this.arrowImage = base.transform.GetChild(4).GetComponent<Image>();
		Image component = base.transform.GetChild(5).GetComponent<Image>();
		component.sprite = this.arrowImage.sprite;
		component.color = this.arrowImage.color;
		component.material = this.arrowImage.material;
		component.type = this.arrowImage.type;
		component.fillCenter = this.arrowImage.fillCenter;
	}

	// Token: 0x06000E0E RID: 3598 RVA: 0x00052EB0 File Offset: 0x000510B0
	public void setArrowSettings()
	{
		RectTransform component = base.transform.GetChild(4).GetComponent<RectTransform>();
		RectTransform component2 = base.transform.GetChild(5).GetComponent<RectTransform>();
		component.localPosition = new Vector3((float)this.leftArrowPositionX, (float)this.leftArrowPositionY, 0f);
		component2.localPosition = new Vector3((float)this.rightArrowPositionX, (float)this.rightArrowPositionY, 0f);
		component.eulerAngles = new Vector3(0f, 0f, (float)this.leftArrowRotation);
		component2.eulerAngles = new Vector3(0f, 0f, (float)this.rightArrowRotation);
	}

	// Token: 0x06000E0F RID: 3599 RVA: 0x00052F53 File Offset: 0x00051153
	public void setPositionFinalSlot()
	{
		base.transform.GetChild(3).GetComponent<RectTransform>().localPosition = new Vector3((float)this.finalSlotPositionX, (float)this.finalSlotPositionY, 0f);
	}

	// Token: 0x06000E10 RID: 3600 RVA: 0x00052F83 File Offset: 0x00051183
	public int getSizeX()
	{
		return (int)base.GetComponent<RectTransform>().sizeDelta.x;
	}

	// Token: 0x06000E11 RID: 3601 RVA: 0x00052F96 File Offset: 0x00051196
	public int getSizeY()
	{
		return (int)base.GetComponent<RectTransform>().sizeDelta.y;
	}

	// Token: 0x06000E12 RID: 3602 RVA: 0x00052FAC File Offset: 0x000511AC
	public void backToInventory()
	{
		int count = this.itemInCraftSystem.Count;
		for (int i = 0; i < count; i++)
		{
			((GameObject)KBEngineApp.app.player().renderObj).GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>().addItemToInventory(this.itemInCraftSystem[i].itemID, this.itemInCraftSystem[i].itemValue);
			Object.Destroy(this.itemInCraftSystemGameObject[i]);
		}
		this.itemInCraftSystem.Clear();
		this.itemInCraftSystemGameObject.Clear();
	}

	// Token: 0x06000E13 RID: 3603 RVA: 0x00053044 File Offset: 0x00051244
	public void ListWithItem()
	{
		this.itemInCraftSystem.Clear();
		this.possibleItems.Clear();
		this.possibletoCreate.Clear();
		this.itemInCraftSystemGameObject.Clear();
		for (int i = 0; i < base.transform.GetChild(1).childCount; i++)
		{
			Transform child = base.transform.GetChild(1).GetChild(i);
			if (child.childCount != 0)
			{
				this.itemInCraftSystem.Add(child.GetChild(0).GetComponent<ItemOnObject>().item);
				this.itemInCraftSystemGameObject.Add(child.GetChild(0).gameObject);
			}
		}
		for (int j = 0; j < this.blueprintDatabase.blueprints.Count; j++)
		{
			int num = 0;
			for (int k = 0; k < this.blueprintDatabase.blueprints[j].ingredients.Count; k++)
			{
				for (int l = 0; l < this.itemInCraftSystem.Count; l++)
				{
					if (this.blueprintDatabase.blueprints[j].ingredients[k] == this.itemInCraftSystem[l].itemID && this.blueprintDatabase.blueprints[j].amount[k] <= this.itemInCraftSystem[l].itemValue)
					{
						num++;
						break;
					}
				}
				if (num == this.blueprintDatabase.blueprints[j].ingredients.Count)
				{
					this.possibleItems.Add(this.blueprintDatabase.blueprints[j].finalItem);
					this.possibleItems[this.possibleItems.Count - 1].itemValue = this.blueprintDatabase.blueprints[j].amountOfFinalItem;
					this.possibletoCreate.Add(true);
				}
			}
		}
	}

	// Token: 0x06000E14 RID: 3604 RVA: 0x0005323C File Offset: 0x0005143C
	public void deleteItems(Item item)
	{
		for (int i = 0; i < this.blueprintDatabase.blueprints.Count; i++)
		{
			if (this.blueprintDatabase.blueprints[i].finalItem.Equals(item))
			{
				for (int j = 0; j < this.blueprintDatabase.blueprints[i].ingredients.Count; j++)
				{
					for (int k = 0; k < this.itemInCraftSystem.Count; k++)
					{
						if (this.itemInCraftSystem[k].itemID == this.blueprintDatabase.blueprints[i].ingredients[j])
						{
							if (this.itemInCraftSystem[k].itemValue == this.blueprintDatabase.blueprints[i].amount[j])
							{
								this.itemInCraftSystem.RemoveAt(k);
								Object.Destroy(this.itemInCraftSystemGameObject[k]);
								this.itemInCraftSystemGameObject.RemoveAt(k);
								this.ListWithItem();
								break;
							}
							if (this.itemInCraftSystem[k].itemValue >= this.blueprintDatabase.blueprints[i].amount[j])
							{
								this.itemInCraftSystem[k].itemValue = this.itemInCraftSystem[k].itemValue - this.blueprintDatabase.blueprints[i].amount[j];
								this.ListWithItem();
								break;
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x040009F2 RID: 2546
	[SerializeField]
	public int finalSlotPositionX;

	// Token: 0x040009F3 RID: 2547
	[SerializeField]
	public int finalSlotPositionY;

	// Token: 0x040009F4 RID: 2548
	[SerializeField]
	public int leftArrowPositionX;

	// Token: 0x040009F5 RID: 2549
	[SerializeField]
	public int leftArrowPositionY;

	// Token: 0x040009F6 RID: 2550
	[SerializeField]
	public int rightArrowPositionX;

	// Token: 0x040009F7 RID: 2551
	[SerializeField]
	public int rightArrowPositionY;

	// Token: 0x040009F8 RID: 2552
	[SerializeField]
	public int leftArrowRotation;

	// Token: 0x040009F9 RID: 2553
	[SerializeField]
	public int rightArrowRotation;

	// Token: 0x040009FA RID: 2554
	public Image finalSlotImage;

	// Token: 0x040009FB RID: 2555
	public Image arrowImage;

	// Token: 0x040009FC RID: 2556
	public List<Item> itemInCraftSystem = new List<Item>();

	// Token: 0x040009FD RID: 2557
	public List<GameObject> itemInCraftSystemGameObject = new List<GameObject>();

	// Token: 0x040009FE RID: 2558
	private BlueprintDatabase blueprintDatabase;

	// Token: 0x040009FF RID: 2559
	public List<Item> possibleItems = new List<Item>();

	// Token: 0x04000A00 RID: 2560
	public List<bool> possibletoCreate = new List<bool>();
}
