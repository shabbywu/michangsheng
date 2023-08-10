using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;

public class PlayerBeiBaoManager : MonoBehaviour
{
	public Inventory2 inventory2;

	[SerializeField]
	private UIToggle uIToggle;

	[SerializeField]
	private GameObject Weapon;

	[SerializeField]
	private GameObject Weapon2;

	[SerializeField]
	private GameObject Clothing;

	[SerializeField]
	private GameObject Ring;

	[SerializeField]
	private GameObject LinZhou;

	public static PlayerBeiBaoManager inst;

	public List<EquipCell> equiplist;

	public void updateEquipd()
	{
		Avatar player = Tools.instance.getPlayer();
		player.equipItemList.values = new List<ITEM_INFO>();
		for (int i = 0; i < equiplist.Count; i++)
		{
			equiplist[i].Item = Singleton.equip.Equip[GetEquipIndex(((Object)equiplist[i]).name)];
			if (equiplist[i].Item.itemID != -1)
			{
				player.equipItemList.values.Add(player.FindItemByUUID(equiplist[i].Item.UUID));
			}
		}
		Equips.resetEquipSeid(player);
	}

	private void Awake()
	{
		inst = this;
	}

	private void Start()
	{
		inst = this;
	}

	public void openBackpack()
	{
		if (uIToggle.value)
		{
			updateEquipCellSum();
		}
	}

	public static int GetEquipIndex(string name)
	{
		int num = 0;
		return name switch
		{
			"Weapon" => 0, 
			"Weapon2" => 1, 
			"Clothing" => 2, 
			"Ring" => 3, 
			"LinZhou" => 4, 
			_ => -1, 
		};
	}

	private void updateEquipCellSum()
	{
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		if (Tools.instance.getPlayer().checkHasStudyWuDaoSkillByID(2231))
		{
			Weapon.transform.localPosition = new Vector3(-66f, 137f, 0f);
			Weapon2.transform.localPosition = new Vector3(-6f, -81f, 0f);
			Clothing.transform.localPosition = new Vector3(-2f, -299f, 0f);
			Ring.transform.localPosition = new Vector3(-59f, -525f, 0f);
			Weapon2.SetActive(true);
		}
		else
		{
			Weapon.transform.localPosition = new Vector3(-51f, 75f, 0f);
			Clothing.transform.localPosition = new Vector3(-2f, -207f, 0f);
			Ring.transform.localPosition = new Vector3(-57f, -485f, 0f);
			Weapon2.SetActive(false);
		}
	}

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
				addEquip(values[k].itemId, values[k].uuid, values[k].Seid);
			}
		}
		Equips.resetEquipSeid(player);
	}

	public void addEquip(int id, string uuid, JSONObject Seid)
	{
		int i = jsonData.instance.ItemJsonData[id.ToString()]["type"].I;
		List<item> equip = Singleton.equip.Equip;
		switch (i)
		{
		case 0:
			if (equip[0].itemID == -1)
			{
				equip[0] = Singleton.inventory.datebase.items[id].Clone();
				equip[0].UUID = uuid;
				equip[0].Seid = Seid;
			}
			else if (equip[1].itemID == -1 && Tools.instance.getPlayer().checkHasStudyWuDaoSkillByID(2231))
			{
				equip[1] = Singleton.inventory.datebase.items[id].Clone();
				equip[1].UUID = uuid;
				equip[1].Seid = Seid;
			}
			else
			{
				equip[0] = Singleton.inventory.datebase.items[id].Clone();
				equip[0].UUID = uuid;
				equip[0].Seid = Seid;
			}
			break;
		case 1:
			equip[2] = Singleton.inventory.datebase.items[id].Clone();
			equip[2].UUID = uuid;
			equip[2].Seid = Seid;
			break;
		case 2:
			equip[3] = Singleton.inventory.datebase.items[id].Clone();
			equip[3].UUID = uuid;
			equip[3].Seid = Seid;
			break;
		case 14:
			equip[4] = Singleton.inventory.datebase.items[id].Clone();
			equip[4].UUID = uuid;
			equip[4].Seid = Seid;
			break;
		}
	}

	public int getEquipIndexByType(int type)
	{
		int result = -1;
		List<item> equip = Singleton.equip.Equip;
		switch (type)
		{
		case 0:
			result = ((equip[0].itemID != -1) ? ((equip[1].itemID == -1 && Tools.instance.getPlayer().checkHasStudyWuDaoSkillByID(2231)) ? 1 : 0) : 0);
			break;
		case 1:
			result = 2;
			break;
		case 2:
			result = 3;
			break;
		case 14:
			result = 4;
			break;
		}
		return result;
	}
}
