using System;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200014B RID: 331
public class Tooltip : MonoBehaviour
{
	// Token: 0x06000EC3 RID: 3779 RVA: 0x0005A07C File Offset: 0x0005827C
	private void Start()
	{
		this.setVariables();
		this.tooltipHeight = base.GetComponent<RectTransform>().rect.height;
		if ((GameObject)KBEngineApp.app.player().renderObj)
		{
			this.eS = ((GameObject)KBEngineApp.app.player().renderObj).GetComponent<PlayerInventory>().characterSystem.GetComponent<EquipmentSystem>();
		}
		if ((GameObject)KBEngineApp.app.player().renderObj)
		{
			this.inventory = ((GameObject)KBEngineApp.app.player().renderObj).GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>();
		}
	}

	// Token: 0x06000EC4 RID: 3780 RVA: 0x00004095 File Offset: 0x00002295
	public void setVariables()
	{
	}

	// Token: 0x06000EC5 RID: 3781 RVA: 0x0005A130 File Offset: 0x00058330
	public void activateTooltip()
	{
		base.transform.gameObject.SetActive(true);
		this.setOperateByType(this.tooltipType);
		this.tooltipImageIcon.sprite = this.item.itemIcon;
		this.tooltipNameText.text = this.item.itemName;
		this.tooltipDescText.text = this.item.itemDesc;
		this.tooltipAttrText.text = "";
		foreach (ItemAttribute itemAttribute in this.item.itemAttributes)
		{
			Text text = this.tooltipAttrText;
			text.text = string.Concat(new object[]
			{
				text.text,
				itemAttribute.attributeName,
				": ",
				itemAttribute.attributeValue,
				"\n"
			});
		}
	}

	// Token: 0x06000EC6 RID: 3782 RVA: 0x0005A238 File Offset: 0x00058438
	private void setOperateByType(TooltipType ttype)
	{
		this.btn_equip.SetActive(false);
		this.btn_unEquip.SetActive(false);
		this.btn_use.SetActive(false);
		this.btn_drop.SetActive(false);
		this.btn_hotBar.SetActive(false);
		if (ttype == TooltipType.Inventory)
		{
			this.btn_drop.SetActive(true);
			if (this.item.isEquipItem())
			{
				this.btn_equip.SetActive(true);
			}
			if (this.item.isConsumeItem())
			{
				this.btn_use.SetActive(true);
				this.btn_hotBar.SetActive(true);
				return;
			}
		}
		else if (ttype == TooltipType.Equipment)
		{
			this.btn_unEquip.SetActive(true);
		}
	}

	// Token: 0x06000EC7 RID: 3783 RVA: 0x0005A2DF File Offset: 0x000584DF
	public void deactivateTooltip()
	{
		base.transform.gameObject.SetActive(false);
	}

	// Token: 0x06000EC8 RID: 3784 RVA: 0x00004095 File Offset: 0x00002295
	public void equipTooltip()
	{
	}

	// Token: 0x06000EC9 RID: 3785 RVA: 0x0005A2F4 File Offset: 0x000584F4
	public void dropItem()
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		if (avatar != null)
		{
			avatar.dropRequest(this.item.itemUUID);
			this.deactivateTooltip();
		}
	}

	// Token: 0x06000ECA RID: 3786 RVA: 0x0005A32C File Offset: 0x0005852C
	public void equipItem()
	{
		if (this.eS == null)
		{
			this.eS = ((GameObject)KBEngineApp.app.player().renderObj).GetComponent<PlayerInventory>().characterSystem.GetComponent<EquipmentSystem>();
		}
		if (this.eS != null)
		{
			int i = 0;
			while (i < this.eS.slotsInTotal)
			{
				if (this.eS.itemTypeOfSlots[i].Equals(this.item.itemType))
				{
					Avatar avatar = (Avatar)KBEngineApp.app.player();
					if (avatar != null)
					{
						avatar.equipItemRequest(this.item.itemUUID);
						this.deactivateTooltip();
						return;
					}
					break;
				}
				else
				{
					i++;
				}
			}
		}
	}

	// Token: 0x06000ECB RID: 3787 RVA: 0x0005A3F0 File Offset: 0x000585F0
	public void unEquipItem()
	{
		if (this.inventory == null)
		{
			this.inventory = ((GameObject)KBEngineApp.app.player().renderObj).GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>();
		}
		if (this.inventory.getFirstEmptyItemIndex() >= 0)
		{
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			if (avatar != null)
			{
				avatar.UnEquipItemRequest(this.item.itemUUID);
				this.deactivateTooltip();
			}
		}
	}

	// Token: 0x06000ECC RID: 3788 RVA: 0x0005A46C File Offset: 0x0005866C
	public void useItem()
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		if (avatar != null)
		{
			avatar.useItemRequest((ulong)((long)this.item.itemIndex));
			this.deactivateTooltip();
		}
	}

	// Token: 0x06000ECD RID: 3789 RVA: 0x0005A4A4 File Offset: 0x000586A4
	public void hotBarItem()
	{
		HotBarProcess._instance.upItem(this.item.itemID);
	}

	// Token: 0x04000AF6 RID: 2806
	public Item item;

	// Token: 0x04000AF7 RID: 2807
	private EquipmentSystem eS;

	// Token: 0x04000AF8 RID: 2808
	private Inventory inventory;

	// Token: 0x04000AF9 RID: 2809
	public TooltipType tooltipType;

	// Token: 0x04000AFA RID: 2810
	public GameObject btn_equip;

	// Token: 0x04000AFB RID: 2811
	public GameObject btn_unEquip;

	// Token: 0x04000AFC RID: 2812
	public GameObject btn_use;

	// Token: 0x04000AFD RID: 2813
	public GameObject btn_drop;

	// Token: 0x04000AFE RID: 2814
	public GameObject btn_hotBar;

	// Token: 0x04000AFF RID: 2815
	public float tooltipHeight;

	// Token: 0x04000B00 RID: 2816
	private Image tooltipImageIcon;

	// Token: 0x04000B01 RID: 2817
	private Text tooltipNameText;

	// Token: 0x04000B02 RID: 2818
	private Text tooltipDescText;

	// Token: 0x04000B03 RID: 2819
	private Text tooltipAttrText;
}
