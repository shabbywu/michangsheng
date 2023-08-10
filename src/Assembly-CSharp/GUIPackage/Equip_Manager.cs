using System.Collections.Generic;
using KBEngine;
using UnityEngine;

namespace GUIPackage;

public class Equip_Manager : MonoBehaviour
{
	public GameObject EquipUI;

	public GameObject Temp;

	private bool showEquipment = true;

	private List<item> _Equip = new List<item>();

	public bool is_draged;

	public List<item> Equip
	{
		get
		{
			return _Equip;
		}
		set
		{
			_Equip = value;
		}
	}

	private void Awake()
	{
		is_draged = false;
		initEuip();
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetKeyDown((KeyCode)101))
		{
			Show();
		}
	}

	private void Show()
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		showEquipment = !showEquipment;
		if (!showEquipment)
		{
			Singleton.inventory.showTooltip = false;
		}
		if (showEquipment)
		{
			EquipUI.transform.Find("Win").position = EquipUI.transform.position;
		}
		EquipUI.SetActive(showEquipment);
		Singleton.UI.UI_Top(EquipUI.transform);
	}

	private void initEuip()
	{
		for (int i = 0; i < 15; i++)
		{
			Equip.Add(new item());
		}
		EquipUI.SetActive(showEquipment);
	}

	public void SaveEquipment()
	{
	}

	public void LoadEquipment()
	{
		for (int i = 0; i < Equip.Count; i++)
		{
			Equip[i] = new item();
		}
		foreach (ITEM_INFO value in Tools.instance.getPlayer().equipItemList.values)
		{
			for (int j = 0; j < Equip.Count; j++)
			{
				if (j == value.itemIndex)
				{
					Equip[j] = Singleton.inventory.datebase.items[value.itemId].Clone();
					Equip[j].UUID = value.uuid;
					Equip[j].Seid = value.Seid;
					break;
				}
			}
		}
	}

	public int GetEquipID(string name)
	{
		int num = 0;
		return name switch
		{
			"Weapon" => 0, 
			"Weapon2" => 6, 
			"Clothing" => 1, 
			"Trousers" => 5, 
			"Shoes" => 3, 
			"LinZhou" => 14, 
			"Ring" => 2, 
			_ => -1, 
		};
	}

	public void addEquip(string UUID, int key = 0)
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		ITEM_INFO iTEM_INFO = avatar.FindItemByUUID(UUID);
		if (iTEM_INFO != null && iTEM_INFO.itemId >= 0)
		{
			avatar.YSequipItem(UUID, (int)jsonData.instance.ItemJsonData[string.Concat(iTEM_INFO.itemId)]["type"].n, key);
		}
	}

	public void UnEquip(string UUID, int index = 0)
	{
		((Avatar)KBEngineApp.app.player()).YSUnequipItem(UUID, index);
	}

	private void OnDestroy()
	{
	}
}
