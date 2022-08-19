using System;
using KBEngine;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000A59 RID: 2649
	public class EquipCell : MonoBehaviour
	{
		// Token: 0x060049F5 RID: 18933 RVA: 0x001F59FF File Offset: 0x001F3BFF
		private void Start()
		{
			this.setItem();
		}

		// Token: 0x060049F6 RID: 18934 RVA: 0x001F5A07 File Offset: 0x001F3C07
		public void setItem()
		{
			if (this.IsPlayer)
			{
				this.PlayerSetEquipe();
				return;
			}
			this.MonstarSetEquipe();
		}

		// Token: 0x060049F7 RID: 18935 RVA: 0x001F5A1E File Offset: 0x001F3C1E
		public void PlayerSetEquipe()
		{
			this.Item = Singleton.equip.Equip[PlayerBeiBaoManager.GetEquipIndex(base.name)];
		}

		// Token: 0x060049F8 RID: 18936 RVA: 0x00004095 File Offset: 0x00002295
		public void MonstarSetEquipe()
		{
		}

		// Token: 0x060049F9 RID: 18937 RVA: 0x001F5A40 File Offset: 0x001F3C40
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

		// Token: 0x060049FA RID: 18938 RVA: 0x001F5CB4 File Offset: 0x001F3EB4
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

		// Token: 0x060049FB RID: 18939 RVA: 0x001F5DC0 File Offset: 0x001F3FC0
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

		// Token: 0x060049FC RID: 18940 RVA: 0x001F5FA9 File Offset: 0x001F41A9
		private void OnPress()
		{
			this.PCOnPress();
		}

		// Token: 0x060049FD RID: 18941 RVA: 0x001F5FB4 File Offset: 0x001F41B4
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

		// Token: 0x060049FE RID: 18942 RVA: 0x001F600C File Offset: 0x001F420C
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

		// Token: 0x060049FF RID: 18943 RVA: 0x001F605C File Offset: 0x001F425C
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

		// Token: 0x06004A00 RID: 18944 RVA: 0x001F6115 File Offset: 0x001F4315
		private void OnDrop(GameObject obj)
		{
			if (Input.GetMouseButtonUp(0) && this.CanClick && Singleton.inventory.draggingItem)
			{
				this.chengeItem();
			}
		}

		// Token: 0x06004A01 RID: 18945 RVA: 0x001F613C File Offset: 0x001F433C
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

		// Token: 0x06004A02 RID: 18946 RVA: 0x001F6209 File Offset: 0x001F4409
		private void OnHover(bool isOver)
		{
			this.PCOnHover(isOver);
		}

		// Token: 0x0400495C RID: 18780
		public GameObject Icon;

		// Token: 0x0400495D RID: 18781
		public GameObject Num;

		// Token: 0x0400495E RID: 18782
		public GameObject PingZhi;

		// Token: 0x0400495F RID: 18783
		public item Item = new item();

		// Token: 0x04004960 RID: 18784
		public GameObject BackGroud;

		// Token: 0x04004961 RID: 18785
		public GameObject PingZhiUI;

		// Token: 0x04004962 RID: 18786
		public ItemDatebase itemDatebase;

		// Token: 0x04004963 RID: 18787
		public bool CanClick = true;

		// Token: 0x04004964 RID: 18788
		public bool IsPlayer = true;

		// Token: 0x04004965 RID: 18789
		public int equipBuWei;

		// Token: 0x04004966 RID: 18790
		public string EquipCellName;

		// Token: 0x04004967 RID: 18791
		public UILabel KeyName;

		// Token: 0x04004968 RID: 18792
		public GameObject KeyObject;
	}
}
