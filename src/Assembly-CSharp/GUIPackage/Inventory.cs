using System;
using System.Collections.Generic;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000D7A RID: 3450
	public class Inventory : MonoBehaviour
	{
		// Token: 0x060052F2 RID: 21234 RVA: 0x00228B88 File Offset: 0x00226D88
		private void Start()
		{
			for (int i = 0; i < this.slotsX * this.slotsY; i++)
			{
				this.slots.Add(new item());
				this.inventory.Add(new item());
			}
			this.datebase = GameObject.FindGameObjectWithTag("item_datebase").GetComponent<ItemDatebase>();
			this.AddItem(0);
			this.AddItem(0);
			this.AddItem(1);
		}

		// Token: 0x060052F3 RID: 21235 RVA: 0x0003B60D File Offset: 0x0003980D
		private void Update()
		{
			if (Input.GetKeyDown(105))
			{
				this.showInventory = !this.showInventory;
			}
		}

		// Token: 0x060052F4 RID: 21236 RVA: 0x00228BF8 File Offset: 0x00226DF8
		private void OnGUI()
		{
			this.tooltip = "";
			GUI.skin = this.skin;
			if (this.showInventory)
			{
				this.DrawInventory();
				if (this.showTooltip)
				{
					GUI.Box(new Rect(Event.current.mousePosition.x + 15f, Event.current.mousePosition.y + 15f, 200f, 200f), this.tooltip, this.skin.GetStyle("Tooltip"));
				}
			}
			if (this.draggingItem)
			{
				GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 100f, 100f), this.dragedItem.itemIcon);
			}
		}

		// Token: 0x060052F5 RID: 21237 RVA: 0x00228CCC File Offset: 0x00226ECC
		private void DrawInventory()
		{
			Event current = Event.current;
			int num = 0;
			for (int i = 0; i < this.slotsY; i++)
			{
				for (int j = 0; j < this.slotsX; j++)
				{
					Rect rect;
					rect..ctor((float)(j * 105), (float)(i * 105), 100f, 100f);
					GUI.Box(rect, "", this.skin.GetStyle("Slot"));
					this.slots[num] = this.inventory[num];
					item value = this.slots[num];
					if (this.slots[num].itemName != null)
					{
						GUI.DrawTexture(rect, this.slots[num].itemIcon);
						if (rect.Contains(Event.current.mousePosition))
						{
							if (current.isMouse && current.button == 0 && current.type == 3 && !this.draggingItem)
							{
								this.draggingItem = true;
								this.prevIndex = num;
								this.dragedItem = value;
								this.inventory[num] = new item();
							}
							if (current.isMouse && current.type == 1 && this.draggingItem)
							{
								this.inventory[this.prevIndex] = value;
								this.inventory[num] = this.dragedItem;
								this.draggingItem = false;
								this.dragedItem = null;
							}
							if (!this.draggingItem)
							{
								this.CreateTooltip(this.slots[num]);
								this.showTooltip = true;
							}
						}
						if (this.tooltip == "")
						{
							this.showTooltip = false;
						}
					}
					else if (current.isMouse && current.type == 1 && this.draggingItem && rect.Contains(Event.current.mousePosition))
					{
						this.slots[this.prevIndex] = value;
						this.inventory[num] = this.dragedItem;
						this.draggingItem = false;
						this.dragedItem = null;
					}
					if (this.tooltip == "")
					{
						this.showTooltip = false;
					}
					num++;
				}
			}
		}

		// Token: 0x060052F6 RID: 21238 RVA: 0x00228F00 File Offset: 0x00227100
		private string CreateTooltip(item Item)
		{
			this.tooltip = string.Concat(new string[]
			{
				"<color=#4DA4BF>",
				Item.itemName,
				"</color>\n\n<color=#f2f2f2>",
				Item.itemDesc,
				"</color>"
			});
			return this.tooltip;
		}

		// Token: 0x060052F7 RID: 21239 RVA: 0x00228F50 File Offset: 0x00227150
		private void AddItem(int id)
		{
			for (int i = 0; i < this.inventory.Count; i++)
			{
				if (this.inventory[i].itemName == null)
				{
					using (Dictionary<int, item>.Enumerator enumerator = this.datebase.items.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<int, item> keyValuePair = enumerator.Current;
							if (keyValuePair.Value.itemID == id)
							{
								this.inventory[i] = keyValuePair.Value.Clone();
							}
						}
						break;
					}
				}
			}
		}

		// Token: 0x060052F8 RID: 21240 RVA: 0x00228FF4 File Offset: 0x002271F4
		private void RemoveItem(int id)
		{
			for (int i = 0; i < this.inventory.Count; i++)
			{
				if (this.inventory[i].itemID == id)
				{
					this.inventory[i] = new item();
					return;
				}
			}
		}

		// Token: 0x060052F9 RID: 21241 RVA: 0x00229040 File Offset: 0x00227240
		private bool InventoryContains(int id)
		{
			bool flag = false;
			for (int i = 0; i < this.inventory.Count; i++)
			{
				flag = (this.inventory[i].itemID == id);
				if (flag)
				{
					break;
				}
			}
			return flag;
		}

		// Token: 0x060052FA RID: 21242 RVA: 0x00229080 File Offset: 0x00227280
		private void SaveInventory()
		{
			for (int i = 0; i < this.inventory.Count; i++)
			{
				PlayerPrefs.SetInt("Inventory" + i, this.inventory[i].itemID);
			}
		}

		// Token: 0x060052FB RID: 21243 RVA: 0x002290CC File Offset: 0x002272CC
		private void LoadInventory()
		{
			for (int i = 0; i < this.inventory.Count; i++)
			{
				this.inventory[i] = ((PlayerPrefs.GetInt("Inventory" + i, -1) >= 0) ? this.datebase.items[PlayerPrefs.GetInt("Inventory" + i)] : new item());
			}
		}

		// Token: 0x040052EE RID: 21230
		public int slotsX;

		// Token: 0x040052EF RID: 21231
		public int slotsY;

		// Token: 0x040052F0 RID: 21232
		public GUISkin skin;

		// Token: 0x040052F1 RID: 21233
		public List<item> inventory = new List<item>();

		// Token: 0x040052F2 RID: 21234
		public List<item> slots = new List<item>();

		// Token: 0x040052F3 RID: 21235
		private ItemDatebase datebase;

		// Token: 0x040052F4 RID: 21236
		private bool showInventory;

		// Token: 0x040052F5 RID: 21237
		private bool showTooltip;

		// Token: 0x040052F6 RID: 21238
		private string tooltip;

		// Token: 0x040052F7 RID: 21239
		private bool draggingItem;

		// Token: 0x040052F8 RID: 21240
		private item dragedItem;

		// Token: 0x040052F9 RID: 21241
		private int prevIndex;
	}
}
