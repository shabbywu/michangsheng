using System;
using KBEngine;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000D79 RID: 3449
	public class EquipCell : MonoBehaviour
	{
		// Token: 0x060052E0 RID: 21216 RVA: 0x0003B539 File Offset: 0x00039739
		private void Start()
		{
			this.setItem();
		}

		// Token: 0x060052E1 RID: 21217 RVA: 0x0003B541 File Offset: 0x00039741
		public void setItem()
		{
			if (this.IsPlayer)
			{
				this.PlayerSetEquipe();
				return;
			}
			this.MonstarSetEquipe();
		}

		// Token: 0x060052E2 RID: 21218 RVA: 0x0003B558 File Offset: 0x00039758
		public void PlayerSetEquipe()
		{
			this.Item = Singleton.equip.Equip[PlayerBeiBaoManager.GetEquipIndex(base.name)];
		}

		// Token: 0x060052E3 RID: 21219 RVA: 0x000042DD File Offset: 0x000024DD
		public void MonstarSetEquipe()
		{
		}

		// Token: 0x060052E4 RID: 21220 RVA: 0x002283E8 File Offset: 0x002265E8
		private void Update()
		{
			UITexture component;
			UITexture component2;
			if (!this.IsPlayer)
			{
				component = this.Icon.GetComponent<UITexture>();
				if (component != null)
				{
					component.mainTexture = this.Item.itemIcon;
				}
				component2 = this.PingZhi.GetComponent<UITexture>();
				if (component2 != null)
				{
					component2.mainTexture = this.Item.itemPingZhi;
				}
				return;
			}
			component = this.Icon.GetComponent<UITexture>();
			if (component != null)
			{
				component.mainTexture = Singleton.equip.Equip[PlayerBeiBaoManager.GetEquipIndex(base.name)].itemIcon;
			}
			component2 = this.PingZhi.GetComponent<UITexture>();
			if (component2 != null)
			{
				if (this.Item.Seid != null && this.Item.Seid.HasField("quality"))
				{
					int i = this.Item.Seid["quality"].I;
					if (this.itemDatebase == null)
					{
						this.itemDatebase = jsonData.instance.gameObject.GetComponent<ItemDatebase>();
					}
					component2.mainTexture = this.itemDatebase.PingZhi[i];
					if (this.PingZhiUI != null)
					{
						UI2DSprite component3 = this.PingZhiUI.GetComponent<UI2DSprite>();
						if (component3 != null)
						{
							component3.sprite2D = this.itemDatebase.PingZhiUp[i];
						}
					}
				}
				else
				{
					component2.mainTexture = Singleton.equip.Equip[PlayerBeiBaoManager.GetEquipIndex(base.name)].itemPingZhi;
					if (this.PingZhiUI != null)
					{
						UI2DSprite component3 = this.PingZhiUI.GetComponent<UI2DSprite>();
						if (component3 != null)
						{
							component3.sprite2D = Singleton.equip.Equip[PlayerBeiBaoManager.GetEquipIndex(base.name)].itemPingZhiUP;
						}
					}
				}
			}
			if (Singleton.equip.Equip[PlayerBeiBaoManager.GetEquipIndex(base.name)].UUID != "" && Tools.instance.getPlayer().itemList.values.Find((ITEM_INFO aa) => aa.uuid == Singleton.equip.Equip[PlayerBeiBaoManager.GetEquipIndex(base.name)].UUID) == null)
			{
				Singleton.equip.Equip[PlayerBeiBaoManager.GetEquipIndex(base.name)] = new item();
				PlayerBeiBaoManager.inst.updateEquipd();
			}
			if (this.CanClick)
			{
				this.setItem();
			}
			this.ShowName();
		}

		// Token: 0x060052E5 RID: 21221 RVA: 0x0022865C File Offset: 0x0022685C
		public void ShowName()
		{
			if (this.Item.itemID != -1 && this.KeyName != null && this.KeyObject != null)
			{
				JSONObject jsonobject = jsonData.instance.ItemJsonData[this.Item.itemID.ToString()];
				this.KeyName.text = string.Concat(new string[]
				{
					"[",
					jsonData.instance.NameColor[Inventory2.GetItemQuality(this.Item, jsonobject["quality"].I) - 1],
					"]",
					Inventory2.GetItemName(this.Item, Tools.instance.Code64ToString(jsonobject["name"].str)),
					"[-]"
				});
				this.KeyObject.SetActive(true);
				return;
			}
			if (this.KeyObject != null)
			{
				this.KeyObject.SetActive(false);
			}
		}

		// Token: 0x060052E6 RID: 21222 RVA: 0x00228768 File Offset: 0x00226968
		private void chengeItem()
		{
			if (Singleton.inventory.dragedItem.itemType.ToString() == base.name || Singleton.inventory.dragedItem.itemType.ToString() + "2" == base.name || !Singleton.inventory.draggingItem)
			{
				if (this.Item.itemName != null)
				{
					if (!Singleton.inventory.draggingItem)
					{
						Singleton.inventory.dragedID = PlayerBeiBaoManager.GetEquipIndex(base.name);
						Singleton.inventory.draggingItem = true;
						Singleton.inventory.ChangeItem(ref this.Item, ref Singleton.inventory.dragedItem);
						Singleton.equip.Equip[PlayerBeiBaoManager.GetEquipIndex(base.name)] = this.Item;
						PlayerBeiBaoManager.inst.updateEquipd();
						Singleton.equip.is_draged = true;
						return;
					}
					string uuid = Singleton.inventory.dragedItem.UUID;
					Singleton.inventory.draggingItem = true;
					Singleton.inventory.ChangeItem(ref this.Item, ref Singleton.inventory.dragedItem);
					Singleton.equip.Equip[PlayerBeiBaoManager.GetEquipIndex(base.name)] = this.Item;
					PlayerBeiBaoManager.inst.updateEquipd();
					return;
				}
				else if (Singleton.inventory.draggingItem)
				{
					Singleton.inventory.ChangeItem(ref this.Item, ref Singleton.inventory.dragedItem);
					Singleton.equip.Equip[PlayerBeiBaoManager.GetEquipIndex(base.name)] = this.Item;
					Singleton.inventory.Temp.GetComponent<UITexture>().mainTexture = Singleton.inventory.dragedItem.itemIcon;
					Singleton.inventory.draggingItem = false;
					Singleton.equip.is_draged = false;
					PlayerBeiBaoManager.inst.updateEquipd();
				}
			}
		}

		// Token: 0x060052E7 RID: 21223 RVA: 0x0003B57A File Offset: 0x0003977A
		private void OnPress()
		{
			this.PCOnPress();
		}

		// Token: 0x060052E8 RID: 21224 RVA: 0x00228954 File Offset: 0x00226B54
		public virtual void MobilePress()
		{
			if (this.Item.itemID == -1)
			{
				return;
			}
			this.PCOnHover(true);
			Singleton.ToolTipsBackGround.openTooltips();
			TooltipsBackgroundi toolTipsBackGround = Singleton.ToolTipsBackGround;
			toolTipsBackGround.CloseAction = delegate()
			{
				this.PCOnHover(false);
			};
			toolTipsBackGround.UseAction = delegate()
			{
				this.ClickMouseBtn1();
			};
		}

		// Token: 0x060052E9 RID: 21225 RVA: 0x002289AC File Offset: 0x00226BAC
		public void PCOnPress()
		{
			if (!this.CanClick)
			{
				return;
			}
			this.Item = Singleton.equip.Equip[PlayerBeiBaoManager.GetEquipIndex(base.name)];
			if (Input.GetMouseButtonDown(1) && !Singleton.inventory.draggingItem)
			{
				this.ClickMouseBtn1();
			}
		}

		// Token: 0x060052EA RID: 21226 RVA: 0x002289FC File Offset: 0x00226BFC
		public void ClickMouseBtn1()
		{
			if (!this.CanClick)
			{
				return;
			}
			if (this.Item.itemName != null)
			{
				for (int i = 0; i < Singleton.inventory.inventory.Count; i++)
				{
					if (Singleton.inventory.inventory[i].itemName == null)
					{
						item value = Singleton.inventory.inventory[i];
						Singleton.inventory.ChangeItem(ref this.Item, ref value);
						Singleton.equip.Equip[PlayerBeiBaoManager.GetEquipIndex(base.name)] = this.Item;
						PlayerBeiBaoManager.inst.updateEquipd();
						Singleton.inventory.inventory[i] = value;
						return;
					}
				}
			}
		}

		// Token: 0x060052EB RID: 21227 RVA: 0x0003B582 File Offset: 0x00039782
		private void OnDrop(GameObject obj)
		{
			if (Input.GetMouseButtonUp(0) && this.CanClick && Singleton.inventory.draggingItem)
			{
				this.chengeItem();
			}
		}

		// Token: 0x060052EC RID: 21228 RVA: 0x00228AB8 File Offset: 0x00226CB8
		public void PCOnHover(bool isOver)
		{
			if (this.IsPlayer)
			{
				if (isOver && Singleton.equip.Equip[PlayerBeiBaoManager.GetEquipIndex(base.name)].itemName != null)
				{
					if (this.IsPlayer)
					{
						Singleton.inventory.Show_Tooltip(Singleton.equip.Equip[PlayerBeiBaoManager.GetEquipIndex(base.name)], 0, 0);
					}
					else
					{
						Singleton.inventory.Show_Tooltip(this.Item, 0, 0);
					}
					Singleton.inventory.showTooltip = true;
					return;
				}
				Singleton.inventory.showTooltip = false;
				return;
			}
			else
			{
				if (isOver && this.Item.itemID != -1)
				{
					Singleton.inventory.Show_Tooltip(this.Item, 0, 0);
					Singleton.inventory.showTooltip = true;
					return;
				}
				Singleton.inventory.showTooltip = false;
				return;
			}
		}

		// Token: 0x060052ED RID: 21229 RVA: 0x0003B5A6 File Offset: 0x000397A6
		private void OnHover(bool isOver)
		{
			this.PCOnHover(isOver);
		}

		// Token: 0x040052E1 RID: 21217
		public GameObject Icon;

		// Token: 0x040052E2 RID: 21218
		public GameObject Num;

		// Token: 0x040052E3 RID: 21219
		public GameObject PingZhi;

		// Token: 0x040052E4 RID: 21220
		public item Item = new item();

		// Token: 0x040052E5 RID: 21221
		public GameObject BackGroud;

		// Token: 0x040052E6 RID: 21222
		public GameObject PingZhiUI;

		// Token: 0x040052E7 RID: 21223
		public ItemDatebase itemDatebase;

		// Token: 0x040052E8 RID: 21224
		public bool CanClick = true;

		// Token: 0x040052E9 RID: 21225
		public bool IsPlayer = true;

		// Token: 0x040052EA RID: 21226
		public int equipBuWei;

		// Token: 0x040052EB RID: 21227
		public string EquipCellName;

		// Token: 0x040052EC RID: 21228
		public UILabel KeyName;

		// Token: 0x040052ED RID: 21229
		public GameObject KeyObject;
	}
}
