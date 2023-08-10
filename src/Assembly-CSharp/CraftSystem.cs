using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public class CraftSystem : MonoBehaviour
{
	[SerializeField]
	public int finalSlotPositionX;

	[SerializeField]
	public int finalSlotPositionY;

	[SerializeField]
	public int leftArrowPositionX;

	[SerializeField]
	public int leftArrowPositionY;

	[SerializeField]
	public int rightArrowPositionX;

	[SerializeField]
	public int rightArrowPositionY;

	[SerializeField]
	public int leftArrowRotation;

	[SerializeField]
	public int rightArrowRotation;

	public Image finalSlotImage;

	public Image arrowImage;

	public List<Item> itemInCraftSystem = new List<Item>();

	public List<GameObject> itemInCraftSystemGameObject = new List<GameObject>();

	private BlueprintDatabase blueprintDatabase;

	public List<Item> possibleItems = new List<Item>();

	public List<bool> possibletoCreate = new List<bool>();

	private void Start()
	{
		blueprintDatabase = (BlueprintDatabase)(object)Resources.Load("BlueprintDatabase");
	}

	private void Update()
	{
		ListWithItem();
	}

	public void setImages()
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		finalSlotImage = ((Component)((Component)this).transform.GetChild(3)).GetComponent<Image>();
		arrowImage = ((Component)((Component)this).transform.GetChild(4)).GetComponent<Image>();
		Image component = ((Component)((Component)this).transform.GetChild(5)).GetComponent<Image>();
		component.sprite = arrowImage.sprite;
		((Graphic)component).color = ((Graphic)arrowImage).color;
		((Graphic)component).material = ((Graphic)arrowImage).material;
		component.type = arrowImage.type;
		component.fillCenter = arrowImage.fillCenter;
	}

	public void setArrowSettings()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		RectTransform component = ((Component)((Component)this).transform.GetChild(4)).GetComponent<RectTransform>();
		RectTransform component2 = ((Component)((Component)this).transform.GetChild(5)).GetComponent<RectTransform>();
		((Transform)component).localPosition = new Vector3((float)leftArrowPositionX, (float)leftArrowPositionY, 0f);
		((Transform)component2).localPosition = new Vector3((float)rightArrowPositionX, (float)rightArrowPositionY, 0f);
		((Transform)component).eulerAngles = new Vector3(0f, 0f, (float)leftArrowRotation);
		((Transform)component2).eulerAngles = new Vector3(0f, 0f, (float)rightArrowRotation);
	}

	public void setPositionFinalSlot()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		((Transform)((Component)((Component)this).transform.GetChild(3)).GetComponent<RectTransform>()).localPosition = new Vector3((float)finalSlotPositionX, (float)finalSlotPositionY, 0f);
	}

	public int getSizeX()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		return (int)((Component)this).GetComponent<RectTransform>().sizeDelta.x;
	}

	public int getSizeY()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		return (int)((Component)this).GetComponent<RectTransform>().sizeDelta.y;
	}

	public void backToInventory()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		int count = itemInCraftSystem.Count;
		for (int i = 0; i < count; i++)
		{
			((GameObject)KBEngineApp.app.player().renderObj).GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>().addItemToInventory(itemInCraftSystem[i].itemID, itemInCraftSystem[i].itemValue);
			Object.Destroy((Object)(object)itemInCraftSystemGameObject[i]);
		}
		itemInCraftSystem.Clear();
		itemInCraftSystemGameObject.Clear();
	}

	public void ListWithItem()
	{
		itemInCraftSystem.Clear();
		possibleItems.Clear();
		possibletoCreate.Clear();
		itemInCraftSystemGameObject.Clear();
		for (int i = 0; i < ((Component)this).transform.GetChild(1).childCount; i++)
		{
			Transform child = ((Component)this).transform.GetChild(1).GetChild(i);
			if (child.childCount != 0)
			{
				itemInCraftSystem.Add(((Component)child.GetChild(0)).GetComponent<ItemOnObject>().item);
				itemInCraftSystemGameObject.Add(((Component)child.GetChild(0)).gameObject);
			}
		}
		for (int j = 0; j < blueprintDatabase.blueprints.Count; j++)
		{
			int num = 0;
			for (int k = 0; k < blueprintDatabase.blueprints[j].ingredients.Count; k++)
			{
				for (int l = 0; l < itemInCraftSystem.Count; l++)
				{
					if (blueprintDatabase.blueprints[j].ingredients[k] == itemInCraftSystem[l].itemID && blueprintDatabase.blueprints[j].amount[k] <= itemInCraftSystem[l].itemValue)
					{
						num++;
						break;
					}
				}
				if (num == blueprintDatabase.blueprints[j].ingredients.Count)
				{
					possibleItems.Add(blueprintDatabase.blueprints[j].finalItem);
					possibleItems[possibleItems.Count - 1].itemValue = blueprintDatabase.blueprints[j].amountOfFinalItem;
					possibletoCreate.Add(item: true);
				}
			}
		}
	}

	public void deleteItems(Item item)
	{
		for (int i = 0; i < blueprintDatabase.blueprints.Count; i++)
		{
			if (!blueprintDatabase.blueprints[i].finalItem.Equals(item))
			{
				continue;
			}
			for (int j = 0; j < blueprintDatabase.blueprints[i].ingredients.Count; j++)
			{
				for (int k = 0; k < itemInCraftSystem.Count; k++)
				{
					if (itemInCraftSystem[k].itemID == blueprintDatabase.blueprints[i].ingredients[j])
					{
						if (itemInCraftSystem[k].itemValue == blueprintDatabase.blueprints[i].amount[j])
						{
							itemInCraftSystem.RemoveAt(k);
							Object.Destroy((Object)(object)itemInCraftSystemGameObject[k]);
							itemInCraftSystemGameObject.RemoveAt(k);
							ListWithItem();
							break;
						}
						if (itemInCraftSystem[k].itemValue >= blueprintDatabase.blueprints[i].amount[j])
						{
							itemInCraftSystem[k].itemValue = itemInCraftSystem[k].itemValue - blueprintDatabase.blueprints[i].amount[j];
							ListWithItem();
							break;
						}
					}
				}
			}
		}
	}
}
