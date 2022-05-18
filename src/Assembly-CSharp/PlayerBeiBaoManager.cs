using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;

// Token: 0x020004F0 RID: 1264
public class PlayerBeiBaoManager : MonoBehaviour
{
	// Token: 0x060020E4 RID: 8420 RVA: 0x00114634 File Offset: 0x00112834
	public void updateEquipd()
	{
		Avatar player = Tools.instance.getPlayer();
		player.equipItemList.values = new List<ITEM_INFO>();
		for (int i = 0; i < this.equiplist.Count; i++)
		{
			this.equiplist[i].Item = Singleton.equip.Equip[PlayerBeiBaoManager.GetEquipIndex(this.equiplist[i].name)];
			if (this.equiplist[i].Item.itemID != -1)
			{
				player.equipItemList.values.Add(player.FindItemByUUID(this.equiplist[i].Item.UUID));
			}
		}
		Equips.resetEquipSeid(player);
	}

	// Token: 0x060020E5 RID: 8421 RVA: 0x0001B1F8 File Offset: 0x000193F8
	private void Awake()
	{
		PlayerBeiBaoManager.inst = this;
	}

	// Token: 0x060020E6 RID: 8422 RVA: 0x0001B1F8 File Offset: 0x000193F8
	private void Start()
	{
		PlayerBeiBaoManager.inst = this;
	}

	// Token: 0x060020E7 RID: 8423 RVA: 0x0001B200 File Offset: 0x00019400
	public void openBackpack()
	{
		if (this.uIToggle.value)
		{
			this.updateEquipCellSum();
		}
	}

	// Token: 0x060020E8 RID: 8424 RVA: 0x001146F8 File Offset: 0x001128F8
	public static int GetEquipIndex(string name)
	{
		int result;
		if (!(name == "Weapon"))
		{
			if (!(name == "Weapon2"))
			{
				if (!(name == "Clothing"))
				{
					if (!(name == "Ring"))
					{
						if (!(name == "LinZhou"))
						{
							result = -1;
						}
						else
						{
							result = 4;
						}
					}
					else
					{
						result = 3;
					}
				}
				else
				{
					result = 2;
				}
			}
			else
			{
				result = 1;
			}
		}
		else
		{
			result = 0;
		}
		return result;
	}

	// Token: 0x060020E9 RID: 8425 RVA: 0x00114764 File Offset: 0x00112964
	private void updateEquipCellSum()
	{
		if (Tools.instance.getPlayer().checkHasStudyWuDaoSkillByID(2231))
		{
			this.Weapon.transform.localPosition = new Vector3(-66f, 137f, 0f);
			this.Weapon2.transform.localPosition = new Vector3(-6f, -81f, 0f);
			this.Clothing.transform.localPosition = new Vector3(-2f, -299f, 0f);
			this.Ring.transform.localPosition = new Vector3(-59f, -525f, 0f);
			this.Weapon2.SetActive(true);
			return;
		}
		this.Weapon.transform.localPosition = new Vector3(-51f, 75f, 0f);
		this.Clothing.transform.localPosition = new Vector3(-2f, -207f, 0f);
		this.Ring.transform.localPosition = new Vector3(-57f, -485f, 0f);
		this.Weapon2.SetActive(false);
	}

	// Token: 0x060020EA RID: 8426 RVA: 0x001148A0 File Offset: 0x00112AA0
	public void restartEquips()
	{
		Avatar player = Tools.instance.getPlayer();
		List<ITEM_INFO> values = player.equipItemList.values;
		for (int i = 0; i < Singleton.equip.Equip.Count; i++)
		{
			Singleton.equip.Equip[i] = new item();
		}
		List<ITEM_INFO> list = new List<ITEM_INFO>();
		for (int j = 0; j < values.Count; j++)
		{
			if (values[j] != null && values[j].itemId != -1)
			{
				list.Add(values[j]);
			}
		}
		player.equipItemList.values = list;
		for (int k = 0; k < player.equipItemList.values.Count; k++)
		{
			if (values[k] != null && values[k].itemId != -1)
			{
				this.addEquip(values[k].itemId, values[k].uuid, values[k].Seid);
			}
		}
		Equips.resetEquipSeid(player);
	}

	// Token: 0x060020EB RID: 8427 RVA: 0x001149B0 File Offset: 0x00112BB0
	public void addEquip(int id, string uuid, JSONObject Seid)
	{
		int i = jsonData.instance.ItemJsonData[id.ToString()]["type"].I;
		List<item> equip = Singleton.equip.Equip;
		if (i == 0)
		{
			if (equip[0].itemID == -1)
			{
				equip[0] = Singleton.inventory.datebase.items[id].Clone();
				equip[0].UUID = uuid;
				equip[0].Seid = Seid;
				return;
			}
			if (equip[1].itemID == -1 && Tools.instance.getPlayer().checkHasStudyWuDaoSkillByID(2231))
			{
				equip[1] = Singleton.inventory.datebase.items[id].Clone();
				equip[1].UUID = uuid;
				equip[1].Seid = Seid;
				return;
			}
			equip[0] = Singleton.inventory.datebase.items[id].Clone();
			equip[0].UUID = uuid;
			equip[0].Seid = Seid;
			return;
		}
		else
		{
			if (i == 1)
			{
				equip[2] = Singleton.inventory.datebase.items[id].Clone();
				equip[2].UUID = uuid;
				equip[2].Seid = Seid;
				return;
			}
			if (i == 2)
			{
				equip[3] = Singleton.inventory.datebase.items[id].Clone();
				equip[3].UUID = uuid;
				equip[3].Seid = Seid;
				return;
			}
			if (i == 14)
			{
				equip[4] = Singleton.inventory.datebase.items[id].Clone();
				equip[4].UUID = uuid;
				equip[4].Seid = Seid;
				return;
			}
			return;
		}
	}

	// Token: 0x060020EC RID: 8428 RVA: 0x00114BA0 File Offset: 0x00112DA0
	public int getEquipIndexByType(int type)
	{
		int result = -1;
		List<item> equip = Singleton.equip.Equip;
		switch (type)
		{
		case 0:
			if (equip[0].itemID == -1)
			{
				result = 0;
			}
			else if (equip[1].itemID == -1 && Tools.instance.getPlayer().checkHasStudyWuDaoSkillByID(2231))
			{
				result = 1;
			}
			else
			{
				result = 0;
			}
			break;
		case 1:
			result = 2;
			break;
		case 2:
			result = 3;
			break;
		default:
			if (type == 14)
			{
				result = 4;
			}
			break;
		}
		return result;
	}

	// Token: 0x04001C5F RID: 7263
	public Inventory2 inventory2;

	// Token: 0x04001C60 RID: 7264
	[SerializeField]
	private UIToggle uIToggle;

	// Token: 0x04001C61 RID: 7265
	[SerializeField]
	private GameObject Weapon;

	// Token: 0x04001C62 RID: 7266
	[SerializeField]
	private GameObject Weapon2;

	// Token: 0x04001C63 RID: 7267
	[SerializeField]
	private GameObject Clothing;

	// Token: 0x04001C64 RID: 7268
	[SerializeField]
	private GameObject Ring;

	// Token: 0x04001C65 RID: 7269
	[SerializeField]
	private GameObject LinZhou;

	// Token: 0x04001C66 RID: 7270
	public static PlayerBeiBaoManager inst;

	// Token: 0x04001C67 RID: 7271
	public List<EquipCell> equiplist;
}
