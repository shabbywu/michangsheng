using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001F9 RID: 505
public class CraftSystem : MonoBehaviour
{
	// Token: 0x06001019 RID: 4121 RVA: 0x00010291 File Offset: 0x0000E491
	private void Start()
	{
		this.blueprintDatabase = (BlueprintDatabase)Resources.Load("BlueprintDatabase");
	}

	// Token: 0x0600101A RID: 4122 RVA: 0x000102A8 File Offset: 0x0000E4A8
	private void Update()
	{
		this.ListWithItem();
	}

	// Token: 0x0600101B RID: 4123 RVA: 0x000A35EC File Offset: 0x000A17EC
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

	// Token: 0x0600101C RID: 4124 RVA: 0x000A368C File Offset: 0x000A188C
	public void setArrowSettings()
	{
		RectTransform component = base.transform.GetChild(4).GetComponent<RectTransform>();
		RectTransform component2 = base.transform.GetChild(5).GetComponent<RectTransform>();
		component.localPosition = new Vector3((float)this.leftArrowPositionX, (float)this.leftArrowPositionY, 0f);
		component2.localPosition = new Vector3((float)this.rightArrowPositionX, (float)this.rightArrowPositionY, 0f);
		component.eulerAngles = new Vector3(0f, 0f, (float)this.leftArrowRotation);
		component2.eulerAngles = new Vector3(0f, 0f, (float)this.rightArrowRotation);
	}

	// Token: 0x0600101D RID: 4125 RVA: 0x000102B0 File Offset: 0x0000E4B0
	public void setPositionFinalSlot()
	{
		base.transform.GetChild(3).GetComponent<RectTransform>().localPosition = new Vector3((float)this.finalSlotPositionX, (float)this.finalSlotPositionY, 0f);
	}

	// Token: 0x0600101E RID: 4126 RVA: 0x000102E0 File Offset: 0x0000E4E0
	public int getSizeX()
	{
		return (int)base.GetComponent<RectTransform>().sizeDelta.x;
	}

	// Token: 0x0600101F RID: 4127 RVA: 0x000102F3 File Offset: 0x0000E4F3
	public int getSizeY()
	{
		return (int)base.GetComponent<RectTransform>().sizeDelta.y;
	}

	// Token: 0x06001020 RID: 4128 RVA: 0x000A3730 File Offset: 0x000A1930
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

	// Token: 0x06001021 RID: 4129 RVA: 0x000A37C8 File Offset: 0x000A19C8
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

	// Token: 0x06001022 RID: 4130 RVA: 0x000A39C0 File Offset: 0x000A1BC0
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

	// Token: 0x04000C8A RID: 3210
	[SerializeField]
	public int finalSlotPositionX;

	// Token: 0x04000C8B RID: 3211
	[SerializeField]
	public int finalSlotPositionY;

	// Token: 0x04000C8C RID: 3212
	[SerializeField]
	public int leftArrowPositionX;

	// Token: 0x04000C8D RID: 3213
	[SerializeField]
	public int leftArrowPositionY;

	// Token: 0x04000C8E RID: 3214
	[SerializeField]
	public int rightArrowPositionX;

	// Token: 0x04000C8F RID: 3215
	[SerializeField]
	public int rightArrowPositionY;

	// Token: 0x04000C90 RID: 3216
	[SerializeField]
	public int leftArrowRotation;

	// Token: 0x04000C91 RID: 3217
	[SerializeField]
	public int rightArrowRotation;

	// Token: 0x04000C92 RID: 3218
	public Image finalSlotImage;

	// Token: 0x04000C93 RID: 3219
	public Image arrowImage;

	// Token: 0x04000C94 RID: 3220
	public List<Item> itemInCraftSystem = new List<Item>();

	// Token: 0x04000C95 RID: 3221
	public List<GameObject> itemInCraftSystemGameObject = new List<GameObject>();

	// Token: 0x04000C96 RID: 3222
	private BlueprintDatabase blueprintDatabase;

	// Token: 0x04000C97 RID: 3223
	public List<Item> possibleItems = new List<Item>();

	// Token: 0x04000C98 RID: 3224
	public List<bool> possibletoCreate = new List<bool>();
}
