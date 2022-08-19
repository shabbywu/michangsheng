using System;
using System.Collections.Generic;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000A5A RID: 2650
	public class Inventory : MonoBehaviour
	{
		// Token: 0x06004A07 RID: 18951 RVA: 0x001F6270 File Offset: 0x001F4470
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

		// Token: 0x06004A08 RID: 18952 RVA: 0x001F62DF File Offset: 0x001F44DF
		private void Update()
		{
			if (Input.GetKeyDown(105))
			{
				this.showInventory = !this.showInventory;
			}
		}

		// Token: 0x06004A09 RID: 18953 RVA: 0x001F62FC File Offset: 0x001F44FC
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

		// Token: 0x06004A0A RID: 18954 RVA: 0x001F63D0 File Offset: 0x001F45D0
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

		// Token: 0x06004A0B RID: 18955 RVA: 0x001F6604 File Offset: 0x001F4804
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

		// Token: 0x06004A0C RID: 18956 RVA: 0x001F6654 File Offset: 0x001F4854
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

		// Token: 0x06004A0D RID: 18957 RVA: 0x001F66F8 File Offset: 0x001F48F8
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

		// Token: 0x06004A0E RID: 18958 RVA: 0x001F6744 File Offset: 0x001F4944
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

		// Token: 0x06004A0F RID: 18959 RVA: 0x001F6784 File Offset: 0x001F4984
		private void SaveInventory()
		{
			for (int i = 0; i < this.inventory.Count; i++)
			{
				PlayerPrefs.SetInt("Inventory" + i, this.inventory[i].itemID);
			}
		}

		// Token: 0x06004A10 RID: 18960 RVA: 0x001F67D0 File Offset: 0x001F49D0
		private void LoadInventory()
		{
			for (int i = 0; i < this.inventory.Count; i++)
			{
				this.inventory[i] = ((PlayerPrefs.GetInt("Inventory" + i, -1) >= 0) ? this.datebase.items[PlayerPrefs.GetInt("Inventory" + i)] : new item());
			}
		}

		// Token: 0x04004969 RID: 18793
		public int slotsX;

		// Token: 0x0400496A RID: 18794
		public int slotsY;

		// Token: 0x0400496B RID: 18795
		public GUISkin skin;

		// Token: 0x0400496C RID: 18796
		public List<item> inventory = new List<item>();

		// Token: 0x0400496D RID: 18797
		public List<item> slots = new List<item>();

		// Token: 0x0400496E RID: 18798
		private ItemDatebase datebase;

		// Token: 0x0400496F RID: 18799
		private bool showInventory;

		// Token: 0x04004970 RID: 18800
		private bool showTooltip;

		// Token: 0x04004971 RID: 18801
		private string tooltip;

		// Token: 0x04004972 RID: 18802
		private bool draggingItem;

		// Token: 0x04004973 RID: 18803
		private item dragedItem;

		// Token: 0x04004974 RID: 18804
		private int prevIndex;
	}
}
