using KBEngine;
using UnityEngine;
using UnityEngine.Events;

namespace GUIPackage;

public class EquipCell : MonoBehaviour
{
	public GameObject Icon;

	public GameObject Num;

	public GameObject PingZhi;

	public item Item = new item();

	public GameObject BackGroud;

	public GameObject PingZhiUI;

	public ItemDatebase itemDatebase;

	public bool CanClick = true;

	public bool IsPlayer = true;

	public int equipBuWei;

	public string EquipCellName;

	public UILabel KeyName;

	public GameObject KeyObject;

	private void Start()
	{
		setItem();
	}

	public void setItem()
	{
		if (IsPlayer)
		{
			PlayerSetEquipe();
		}
		else
		{
			MonstarSetEquipe();
		}
	}

	public void PlayerSetEquipe()
	{
		Item = Singleton.equip.Equip[PlayerBeiBaoManager.GetEquipIndex(((Object)this).name)];
	}

	public void MonstarSetEquipe()
	{
	}

	private void Update()
	{
		UITexture component;
		UITexture component2;
		if (!IsPlayer)
		{
			component = Icon.GetComponent<UITexture>();
			if ((Object)(object)component != (Object)null)
			{
				component.mainTexture = (Texture)(object)Item.itemIcon;
			}
			component2 = PingZhi.GetComponent<UITexture>();
			if ((Object)(object)component2 != (Object)null)
			{
				component2.mainTexture = (Texture)(object)Item.itemPingZhi;
			}
			return;
		}
		component = Icon.GetComponent<UITexture>();
		if ((Object)(object)component != (Object)null)
		{
			component.mainTexture = (Texture)(object)Singleton.equip.Equip[PlayerBeiBaoManager.GetEquipIndex(((Object)this).name)].itemIcon;
		}
		component2 = PingZhi.GetComponent<UITexture>();
		if ((Object)(object)component2 != (Object)null)
		{
			if (Item.Seid != null && Item.Seid.HasField("quality"))
			{
				int i = Item.Seid["quality"].I;
				if ((Object)(object)itemDatebase == (Object)null)
				{
					itemDatebase = ((Component)jsonData.instance).gameObject.GetComponent<ItemDatebase>();
				}
				component2.mainTexture = (Texture)(object)itemDatebase.PingZhi[i];
				if ((Object)(object)PingZhiUI != (Object)null)
				{
					UI2DSprite component3 = PingZhiUI.GetComponent<UI2DSprite>();
					if ((Object)(object)component3 != (Object)null)
					{
						component3.sprite2D = itemDatebase.PingZhiUp[i];
					}
				}
			}
			else
			{
				component2.mainTexture = (Texture)(object)Singleton.equip.Equip[PlayerBeiBaoManager.GetEquipIndex(((Object)this).name)].itemPingZhi;
				if ((Object)(object)PingZhiUI != (Object)null)
				{
					UI2DSprite component3 = PingZhiUI.GetComponent<UI2DSprite>();
					if ((Object)(object)component3 != (Object)null)
					{
						component3.sprite2D = Singleton.equip.Equip[PlayerBeiBaoManager.GetEquipIndex(((Object)this).name)].itemPingZhiUP;
					}
				}
			}
		}
		if (Singleton.equip.Equip[PlayerBeiBaoManager.GetEquipIndex(((Object)this).name)].UUID != "" && Tools.instance.getPlayer().itemList.values.Find((ITEM_INFO aa) => aa.uuid == Singleton.equip.Equip[PlayerBeiBaoManager.GetEquipIndex(((Object)this).name)].UUID) == null)
		{
			Singleton.equip.Equip[PlayerBeiBaoManager.GetEquipIndex(((Object)this).name)] = new item();
			PlayerBeiBaoManager.inst.updateEquipd();
		}
		if (CanClick)
		{
			setItem();
		}
		ShowName();
	}

	public void ShowName()
	{
		if (Item.itemID != -1 && (Object)(object)KeyName != (Object)null && (Object)(object)KeyObject != (Object)null)
		{
			JSONObject jSONObject = jsonData.instance.ItemJsonData[Item.itemID.ToString()];
			KeyName.text = "[" + jsonData.instance.NameColor[Inventory2.GetItemQuality(Item, jSONObject["quality"].I) - 1] + "]" + Inventory2.GetItemName(Item, Tools.instance.Code64ToString(jSONObject["name"].str)) + "[-]";
			KeyObject.SetActive(true);
		}
		else if ((Object)(object)KeyObject != (Object)null)
		{
			KeyObject.SetActive(false);
		}
	}

	private void chengeItem()
	{
		if (!(Singleton.inventory.dragedItem.itemType.ToString() == ((Object)this).name) && !(Singleton.inventory.dragedItem.itemType.ToString() + "2" == ((Object)this).name) && Singleton.inventory.draggingItem)
		{
			return;
		}
		if (Item.itemName != null)
		{
			if (!Singleton.inventory.draggingItem)
			{
				Singleton.inventory.dragedID = PlayerBeiBaoManager.GetEquipIndex(((Object)this).name);
				Singleton.inventory.draggingItem = true;
				Singleton.inventory.ChangeItem(ref Item, ref Singleton.inventory.dragedItem);
				Singleton.equip.Equip[PlayerBeiBaoManager.GetEquipIndex(((Object)this).name)] = Item;
				PlayerBeiBaoManager.inst.updateEquipd();
				Singleton.equip.is_draged = true;
			}
			else
			{
				_ = Singleton.inventory.dragedItem.UUID;
				Singleton.inventory.draggingItem = true;
				Singleton.inventory.ChangeItem(ref Item, ref Singleton.inventory.dragedItem);
				Singleton.equip.Equip[PlayerBeiBaoManager.GetEquipIndex(((Object)this).name)] = Item;
				PlayerBeiBaoManager.inst.updateEquipd();
			}
		}
		else if (Singleton.inventory.draggingItem)
		{
			Singleton.inventory.ChangeItem(ref Item, ref Singleton.inventory.dragedItem);
			Singleton.equip.Equip[PlayerBeiBaoManager.GetEquipIndex(((Object)this).name)] = Item;
			Singleton.inventory.Temp.GetComponent<UITexture>().mainTexture = (Texture)(object)Singleton.inventory.dragedItem.itemIcon;
			Singleton.inventory.draggingItem = false;
			Singleton.equip.is_draged = false;
			PlayerBeiBaoManager.inst.updateEquipd();
		}
	}

	private void OnPress()
	{
		PCOnPress();
	}

	public virtual void MobilePress()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Expected O, but got Unknown
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Expected O, but got Unknown
		if (Item.itemID != -1)
		{
			PCOnHover(isOver: true);
			Singleton.ToolTipsBackGround.openTooltips();
			TooltipsBackgroundi toolTipsBackGround = Singleton.ToolTipsBackGround;
			toolTipsBackGround.CloseAction = (UnityAction)delegate
			{
				PCOnHover(isOver: false);
			};
			toolTipsBackGround.UseAction = (UnityAction)delegate
			{
				ClickMouseBtn1();
			};
		}
	}

	public void PCOnPress()
	{
		if (CanClick)
		{
			Item = Singleton.equip.Equip[PlayerBeiBaoManager.GetEquipIndex(((Object)this).name)];
			if (Input.GetMouseButtonDown(1) && !Singleton.inventory.draggingItem)
			{
				ClickMouseBtn1();
			}
		}
	}

	public void ClickMouseBtn1()
	{
		if (!CanClick || this.Item.itemName == null)
		{
			return;
		}
		for (int i = 0; i < Singleton.inventory.inventory.Count; i++)
		{
			if (Singleton.inventory.inventory[i].itemName == null)
			{
				item Item = Singleton.inventory.inventory[i];
				Singleton.inventory.ChangeItem(ref this.Item, ref Item);
				Singleton.equip.Equip[PlayerBeiBaoManager.GetEquipIndex(((Object)this).name)] = this.Item;
				PlayerBeiBaoManager.inst.updateEquipd();
				Singleton.inventory.inventory[i] = Item;
				break;
			}
		}
	}

	private void OnDrop(GameObject obj)
	{
		if (Input.GetMouseButtonUp(0) && CanClick && Singleton.inventory.draggingItem)
		{
			chengeItem();
		}
	}

	public void PCOnHover(bool isOver)
	{
		if (IsPlayer)
		{
			if (isOver && Singleton.equip.Equip[PlayerBeiBaoManager.GetEquipIndex(((Object)this).name)].itemName != null)
			{
				if (IsPlayer)
				{
					Singleton.inventory.Show_Tooltip(Singleton.equip.Equip[PlayerBeiBaoManager.GetEquipIndex(((Object)this).name)]);
				}
				else
				{
					Singleton.inventory.Show_Tooltip(Item);
				}
				Singleton.inventory.showTooltip = true;
			}
			else
			{
				Singleton.inventory.showTooltip = false;
			}
		}
		else if (isOver && Item.itemID != -1)
		{
			Singleton.inventory.Show_Tooltip(Item);
			Singleton.inventory.showTooltip = true;
		}
		else
		{
			Singleton.inventory.showTooltip = false;
		}
	}

	private void OnHover(bool isOver)
	{
		PCOnHover(isOver);
	}
}
