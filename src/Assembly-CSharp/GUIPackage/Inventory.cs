using System.Collections.Generic;
using UnityEngine;

namespace GUIPackage;

public class Inventory : MonoBehaviour
{
	public int slotsX;

	public int slotsY;

	public GUISkin skin;

	public List<item> inventory = new List<item>();

	public List<item> slots = new List<item>();

	private ItemDatebase datebase;

	private bool showInventory;

	private bool showTooltip;

	private string tooltip;

	private bool draggingItem;

	private item dragedItem;

	private int prevIndex;

	private void Start()
	{
		for (int i = 0; i < slotsX * slotsY; i++)
		{
			slots.Add(new item());
			inventory.Add(new item());
		}
		datebase = GameObject.FindGameObjectWithTag("item_datebase").GetComponent<ItemDatebase>();
		AddItem(0);
		AddItem(0);
		AddItem(1);
	}

	private void Update()
	{
		if (Input.GetKeyDown((KeyCode)105))
		{
			showInventory = !showInventory;
		}
	}

	private void OnGUI()
	{
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		tooltip = "";
		GUI.skin = skin;
		if (showInventory)
		{
			DrawInventory();
			if (showTooltip)
			{
				GUI.Box(new Rect(Event.current.mousePosition.x + 15f, Event.current.mousePosition.y + 15f, 200f, 200f), tooltip, skin.GetStyle("Tooltip"));
			}
		}
		if (draggingItem)
		{
			GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 100f, 100f), (Texture)(object)dragedItem.itemIcon);
		}
	}

	private void DrawInventory()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Invalid comparison between Unknown and I4
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Invalid comparison between Unknown and I4
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Invalid comparison between Unknown and I4
		Event current = Event.current;
		int num = 0;
		Rect val = default(Rect);
		for (int i = 0; i < slotsY; i++)
		{
			for (int j = 0; j < slotsX; j++)
			{
				((Rect)(ref val))._002Ector((float)(j * 105), (float)(i * 105), 100f, 100f);
				GUI.Box(val, "", skin.GetStyle("Slot"));
				slots[num] = inventory[num];
				item value = slots[num];
				if (slots[num].itemName != null)
				{
					GUI.DrawTexture(val, (Texture)(object)slots[num].itemIcon);
					if (((Rect)(ref val)).Contains(Event.current.mousePosition))
					{
						if (current.isMouse && current.button == 0 && (int)current.type == 3 && !draggingItem)
						{
							draggingItem = true;
							prevIndex = num;
							dragedItem = value;
							inventory[num] = new item();
						}
						if (current.isMouse && (int)current.type == 1 && draggingItem)
						{
							inventory[prevIndex] = value;
							inventory[num] = dragedItem;
							draggingItem = false;
							dragedItem = null;
						}
						if (!draggingItem)
						{
							CreateTooltip(slots[num]);
							showTooltip = true;
						}
					}
					if (tooltip == "")
					{
						showTooltip = false;
					}
				}
				else if (current.isMouse && (int)current.type == 1 && draggingItem && ((Rect)(ref val)).Contains(Event.current.mousePosition))
				{
					slots[prevIndex] = value;
					inventory[num] = dragedItem;
					draggingItem = false;
					dragedItem = null;
				}
				if (tooltip == "")
				{
					showTooltip = false;
				}
				num++;
			}
		}
	}

	private string CreateTooltip(item Item)
	{
		tooltip = "<color=#4DA4BF>" + Item.itemName + "</color>\n\n<color=#f2f2f2>" + Item.itemDesc + "</color>";
		return tooltip;
	}

	private void AddItem(int id)
	{
		for (int i = 0; i < inventory.Count; i++)
		{
			if (inventory[i].itemName != null)
			{
				continue;
			}
			{
				foreach (KeyValuePair<int, item> item in datebase.items)
				{
					if (item.Value.itemID == id)
					{
						inventory[i] = item.Value.Clone();
					}
				}
				break;
			}
		}
	}

	private void RemoveItem(int id)
	{
		for (int i = 0; i < inventory.Count; i++)
		{
			if (inventory[i].itemID == id)
			{
				inventory[i] = new item();
				break;
			}
		}
	}

	private bool InventoryContains(int id)
	{
		bool flag = false;
		for (int i = 0; i < inventory.Count; i++)
		{
			flag = inventory[i].itemID == id;
			if (flag)
			{
				break;
			}
		}
		return flag;
	}

	private void SaveInventory()
	{
		for (int i = 0; i < inventory.Count; i++)
		{
			PlayerPrefs.SetInt("Inventory" + i, inventory[i].itemID);
		}
	}

	private void LoadInventory()
	{
		for (int i = 0; i < inventory.Count; i++)
		{
			inventory[i] = ((PlayerPrefs.GetInt("Inventory" + i, -1) >= 0) ? datebase.items[PlayerPrefs.GetInt("Inventory" + i)] : new item());
		}
	}
}
