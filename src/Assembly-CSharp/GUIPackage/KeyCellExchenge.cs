using System;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000D53 RID: 3411
	public class KeyCellExchenge : KeyCell
	{
		// Token: 0x06005132 RID: 20786 RVA: 0x0021D564 File Offset: 0x0021B764
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

		// Token: 0x06005133 RID: 20787 RVA: 0x0003A76F File Offset: 0x0003896F
		private void Update()
		{
			base.Show_Date();
		}

		// Token: 0x06005134 RID: 20788 RVA: 0x0003A777 File Offset: 0x00038977
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

		// Token: 0x06005135 RID: 20789 RVA: 0x0021D61C File Offset: 0x0021B81C
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

		// Token: 0x04005235 RID: 21045
		public bool isPlayer = true;

		// Token: 0x04005236 RID: 21046
		public Key key;

		// Token: 0x04005237 RID: 21047
		public Inventory2 inventory;
	}
}
