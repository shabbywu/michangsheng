using UnityEngine;

namespace GUIPackage;

public class KeyCellExchenge : KeyCell
{
	public bool isPlayer = true;

	public Key key;

	public Inventory2 inventory;

	private void Start()
	{
		Icon = ((Component)((Component)this).transform.Find("Icon")).gameObject;
		Num = ((Component)((Component)this).transform.Find("num")).gameObject;
		key = ((Component)((Component)this).transform.parent).GetComponent<Key>();
		if (isPlayer)
		{
			inventory = ((Component)((Component)this).transform.parent.parent.parent.Find("Inventory2")).GetComponent<Inventory2>();
		}
		else
		{
			inventory = ((Component)((Component)this).transform.parent.parent.parent.Find("Inventory3")).GetComponent<Inventory2>();
		}
	}

	private void Update()
	{
		Show_Date();
	}

	protected new void OnHover(bool isOver)
	{
		if (keyItem.itemID != -1)
		{
			if (isOver)
			{
				inventory.Show_Tooltip(keyItem);
				inventory.showTooltip = true;
			}
			else
			{
				inventory.showTooltip = false;
			}
		}
	}

	protected new void OnPress()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (inventory.draggingItem)
			{
				key.Clear_ItemKey(inventory.dragedItem);
				keyItem = inventory.dragedItem;
				KeyItemID = inventory.dragedID;
				inventory.BackItem();
				keySkill = new Skill();
			}
			else if (key.draggingKey)
			{
				keyItem = inventory.dragedItem;
				KeyItemID = inventory.dragedID;
				inventory.Clear_dragedItem();
				keySkill = new Skill();
			}
			else if (keyItem.itemID != -1)
			{
				inventory.dragedItem = keyItem;
				inventory.dragedID = KeyItemID;
				KeyItemID = -1;
				keyItem = new item();
				key.draggingKey = true;
			}
		}
	}
}
