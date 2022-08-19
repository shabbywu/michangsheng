using System;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000A4C RID: 2636
	public class KeyCellExchenge : KeyCell
	{
		// Token: 0x0600486D RID: 18541 RVA: 0x001E975C File Offset: 0x001E795C
		private void Start()
		{
			this.Icon = base.transform.Find("Icon").gameObject;
			this.Num = base.transform.Find("num").gameObject;
			this.key = base.transform.parent.GetComponent<Key>();
			if (this.isPlayer)
			{
				this.inventory = base.transform.parent.parent.parent.Find("Inventory2").GetComponent<Inventory2>();
				return;
			}
			this.inventory = base.transform.parent.parent.parent.Find("Inventory3").GetComponent<Inventory2>();
		}

		// Token: 0x0600486E RID: 18542 RVA: 0x001E9812 File Offset: 0x001E7A12
		private void Update()
		{
			base.Show_Date();
		}

		// Token: 0x0600486F RID: 18543 RVA: 0x001E981A File Offset: 0x001E7A1A
		protected new void OnHover(bool isOver)
		{
			if (this.keyItem.itemID != -1)
			{
				if (isOver)
				{
					this.inventory.Show_Tooltip(this.keyItem, 0, 0);
					this.inventory.showTooltip = true;
					return;
				}
				this.inventory.showTooltip = false;
			}
		}

		// Token: 0x06004870 RID: 18544 RVA: 0x001E985C File Offset: 0x001E7A5C
		protected new void OnPress()
		{
			if (Input.GetMouseButtonDown(0))
			{
				if (this.inventory.draggingItem)
				{
					this.key.Clear_ItemKey(this.inventory.dragedItem);
					this.keyItem = this.inventory.dragedItem;
					this.KeyItemID = this.inventory.dragedID;
					this.inventory.BackItem();
					this.keySkill = new Skill();
					return;
				}
				if (this.key.draggingKey)
				{
					this.keyItem = this.inventory.dragedItem;
					this.KeyItemID = this.inventory.dragedID;
					this.inventory.Clear_dragedItem();
					this.keySkill = new Skill();
					return;
				}
				if (this.keyItem.itemID != -1)
				{
					this.inventory.dragedItem = this.keyItem;
					this.inventory.dragedID = this.KeyItemID;
					this.KeyItemID = -1;
					this.keyItem = new item();
					this.key.draggingKey = true;
				}
			}
		}

		// Token: 0x040048F7 RID: 18679
		public bool isPlayer = true;

		// Token: 0x040048F8 RID: 18680
		public Key key;

		// Token: 0x040048F9 RID: 18681
		public Inventory2 inventory;
	}
}
