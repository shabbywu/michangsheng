using System;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000220 RID: 544
public class Tooltip : MonoBehaviour
{
	// Token: 0x060010E3 RID: 4323 RVA: 0x000AA328 File Offset: 0x000A8528
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

	// Token: 0x060010E4 RID: 4324 RVA: 0x000042DD File Offset: 0x000024DD
	public void setVariables()
	{
	}

	// Token: 0x060010E5 RID: 4325 RVA: 0x000AA3DC File Offset: 0x000A85DC
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

	// Token: 0x060010E6 RID: 4326 RVA: 0x000AA4E4 File Offset: 0x000A86E4
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

	// Token: 0x060010E7 RID: 4327 RVA: 0x0001085A File Offset: 0x0000EA5A
	public void deactivateTooltip()
	{
		base.transform.gameObject.SetActive(false);
	}

	// Token: 0x060010E8 RID: 4328 RVA: 0x000042DD File Offset: 0x000024DD
	public void equipTooltip()
	{
	}

	// Token: 0x060010E9 RID: 4329 RVA: 0x000AA58C File Offset: 0x000A878C
	public void dropItem()
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		if (avatar != null)
		{
			avatar.dropRequest(this.item.itemUUID);
			this.deactivateTooltip();
		}
	}

	// Token: 0x060010EA RID: 4330 RVA: 0x000AA5C4 File Offset: 0x000A87C4
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

	// Token: 0x060010EB RID: 4331 RVA: 0x000AA688 File Offset: 0x000A8888
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

	// Token: 0x060010EC RID: 4332 RVA: 0x000AA704 File Offset: 0x000A8904
	public void useItem()
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		if (avatar != null)
		{
			avatar.useItemRequest((ulong)((long)this.item.itemIndex));
			this.deactivateTooltip();
		}
	}

	// Token: 0x060010ED RID: 4333 RVA: 0x0001086D File Offset: 0x0000EA6D
	public void hotBarItem()
	{
		HotBarProcess._instance.upItem(this.item.itemID);
	}

	// Token: 0x04000D91 RID: 3473
	public Item item;

	// Token: 0x04000D92 RID: 3474
	private EquipmentSystem eS;

	// Token: 0x04000D93 RID: 3475
	private Inventory inventory;

	// Token: 0x04000D94 RID: 3476
	public TooltipType tooltipType;

	// Token: 0x04000D95 RID: 3477
	public GameObject btn_equip;

	// Token: 0x04000D96 RID: 3478
	public GameObject btn_unEquip;

	// Token: 0x04000D97 RID: 3479
	public GameObject btn_use;

	// Token: 0x04000D98 RID: 3480
	public GameObject btn_drop;

	// Token: 0x04000D99 RID: 3481
	public GameObject btn_hotBar;

	// Token: 0x04000D9A RID: 3482
	public float tooltipHeight;

	// Token: 0x04000D9B RID: 3483
	private Image tooltipImageIcon;

	// Token: 0x04000D9C RID: 3484
	private Text tooltipNameText;

	// Token: 0x04000D9D RID: 3485
	private Text tooltipDescText;

	// Token: 0x04000D9E RID: 3486
	private Text tooltipAttrText;
}
