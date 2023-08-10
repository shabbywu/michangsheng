using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
	public Item item;

	private EquipmentSystem eS;

	private Inventory inventory;

	public TooltipType tooltipType;

	public GameObject btn_equip;

	public GameObject btn_unEquip;

	public GameObject btn_use;

	public GameObject btn_drop;

	public GameObject btn_hotBar;

	public float tooltipHeight;

	private Image tooltipImageIcon;

	private Text tooltipNameText;

	private Text tooltipDescText;

	private Text tooltipAttrText;

	private void Start()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Expected O, but got Unknown
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		setVariables();
		Rect rect = ((Component)this).GetComponent<RectTransform>().rect;
		tooltipHeight = ((Rect)(ref rect)).height;
		if (Object.op_Implicit((Object)(GameObject)KBEngineApp.app.player().renderObj))
		{
			eS = ((GameObject)KBEngineApp.app.player().renderObj).GetComponent<PlayerInventory>().characterSystem.GetComponent<EquipmentSystem>();
		}
		if (Object.op_Implicit((Object)(GameObject)KBEngineApp.app.player().renderObj))
		{
			inventory = ((GameObject)KBEngineApp.app.player().renderObj).GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>();
		}
	}

	public void setVariables()
	{
	}

	public void activateTooltip()
	{
		((Component)((Component)this).transform).gameObject.SetActive(true);
		setOperateByType(tooltipType);
		tooltipImageIcon.sprite = item.itemIcon;
		tooltipNameText.text = item.itemName;
		tooltipDescText.text = item.itemDesc;
		tooltipAttrText.text = "";
		foreach (ItemAttribute itemAttribute in item.itemAttributes)
		{
			Text val = tooltipAttrText;
			val.text = val.text + itemAttribute.attributeName + ": " + itemAttribute.attributeValue + "\n";
		}
	}

	private void setOperateByType(TooltipType ttype)
	{
		btn_equip.SetActive(false);
		btn_unEquip.SetActive(false);
		btn_use.SetActive(false);
		btn_drop.SetActive(false);
		btn_hotBar.SetActive(false);
		switch (ttype)
		{
		case TooltipType.Inventory:
			btn_drop.SetActive(true);
			if (item.isEquipItem())
			{
				btn_equip.SetActive(true);
			}
			if (item.isConsumeItem())
			{
				btn_use.SetActive(true);
				btn_hotBar.SetActive(true);
			}
			break;
		case TooltipType.Equipment:
			btn_unEquip.SetActive(true);
			break;
		}
	}

	public void deactivateTooltip()
	{
		((Component)((Component)this).transform).gameObject.SetActive(false);
	}

	public void equipTooltip()
	{
	}

	public void dropItem()
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		if (avatar != null)
		{
			avatar.dropRequest(item.itemUUID);
			deactivateTooltip();
		}
	}

	public void equipItem()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)eS == (Object)null)
		{
			eS = ((GameObject)KBEngineApp.app.player().renderObj).GetComponent<PlayerInventory>().characterSystem.GetComponent<EquipmentSystem>();
		}
		if (!((Object)(object)eS != (Object)null))
		{
			return;
		}
		for (int i = 0; i < eS.slotsInTotal; i++)
		{
			if (eS.itemTypeOfSlots[i].Equals(item.itemType))
			{
				Avatar avatar = (Avatar)KBEngineApp.app.player();
				if (avatar != null)
				{
					avatar.equipItemRequest(item.itemUUID);
					deactivateTooltip();
				}
				break;
			}
		}
	}

	public void unEquipItem()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)inventory == (Object)null)
		{
			inventory = ((GameObject)KBEngineApp.app.player().renderObj).GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>();
		}
		if (inventory.getFirstEmptyItemIndex() >= 0)
		{
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			if (avatar != null)
			{
				avatar.UnEquipItemRequest(item.itemUUID);
				deactivateTooltip();
			}
		}
	}

	public void useItem()
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		if (avatar != null)
		{
			avatar.useItemRequest((ulong)item.itemIndex);
			deactivateTooltip();
		}
	}

	public void hotBarItem()
	{
		HotBarProcess._instance.upItem(item.itemID);
	}
}
