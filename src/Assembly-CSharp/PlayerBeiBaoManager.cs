using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;

// Token: 0x02000373 RID: 883
public class PlayerBeiBaoManager : MonoBehaviour
{
	// Token: 0x06001D7F RID: 7551 RVA: 0x000D055C File Offset: 0x000CE75C
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

	// Token: 0x06001D80 RID: 7552 RVA: 0x000D061E File Offset: 0x000CE81E
	private void Awake()
	{
		PlayerBeiBaoManager.inst = this;
	}

	// Token: 0x06001D81 RID: 7553 RVA: 0x000D061E File Offset: 0x000CE81E
	private void Start()
	{
		PlayerBeiBaoManager.inst = this;
	}

	// Token: 0x06001D82 RID: 7554 RVA: 0x000D0626 File Offset: 0x000CE826
	public void openBackpack()
	{
		if (this.uIToggle.value)
		{
			this.updateEquipCellSum();
		}
	}

	// Token: 0x06001D83 RID: 7555 RVA: 0x000D063C File Offset: 0x000CE83C
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

	// Token: 0x06001D84 RID: 7556 RVA: 0x000D06A8 File Offset: 0x000CE8A8
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

	// Token: 0x06001D85 RID: 7557 RVA: 0x000D07E4 File Offset: 0x000CE9E4
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

	// Token: 0x06001D86 RID: 7558 RVA: 0x000D08F4 File Offset: 0x000CEAF4
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

	// Token: 0x06001D87 RID: 7559 RVA: 0x000D0AE4 File Offset: 0x000CECE4
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

	// Token: 0x04001812 RID: 6162
	public Inventory2 inventory2;

	// Token: 0x04001813 RID: 6163
	[SerializeField]
	private UIToggle uIToggle;

	// Token: 0x04001814 RID: 6164
	[SerializeField]
	private GameObject Weapon;

	// Token: 0x04001815 RID: 6165
	[SerializeField]
	private GameObject Weapon2;

	// Token: 0x04001816 RID: 6166
	[SerializeField]
	private GameObject Clothing;

	// Token: 0x04001817 RID: 6167
	[SerializeField]
	private GameObject Ring;

	// Token: 0x04001818 RID: 6168
	[SerializeField]
	private GameObject LinZhou;

	// Token: 0x04001819 RID: 6169
	public static PlayerBeiBaoManager inst;

	// Token: 0x0400181A RID: 6170
	public List<EquipCell> equiplist;
}
