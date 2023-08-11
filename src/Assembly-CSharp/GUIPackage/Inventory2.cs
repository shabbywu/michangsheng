using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using JSONClass;
using KBEngine;
using UnityEngine;

namespace GUIPackage;

public class Inventory2 : MonoBehaviour
{
	public delegate void EventsetItemCell<T>(T aa) where T : ItemCell;

	public jsonData.InventoryNUM count = jsonData.InventoryNUM.Max;

	public jsonData.InventoryNUM FanYeCount = jsonData.InventoryNUM.Max;

	public GameObject InventoryUI;

	public GameObject Temp;

	public List<item> inventory = new List<item>();

	public ItemDatebase datebase;

	private bool showInventory = true;

	public bool draggingItem;

	public item dragedItem;

	public int dragedID;

	public selectPage selectpage;

	public GameObject BaseSlotOBJ;

	public bool ResetToolTips = true;

	public GameObject exchengkey;

	private int showToolType;

	public bool JustShow;

	public string NomelBtn1Text = "";

	public bool AutoSetBtnText;

	public bool isNewJiaoYi;

	public bool isPaiMai;

	public GameObject Tooltip;

	public GameObject EquipTooltip;

	public GameObject BookTooltip;

	public GameObject skillBookToolTip;

	public GameObject DanyaoToolTip;

	public GameObject YaoCaoToolTip;

	public GameObject DanYaoToolTip;

	public GameObject DanLuToolTip;

	public int inventoryItemType;

	public List<int> inventItemLeiXing = new List<int>();

	public int nowIndex;

	public bool shouldInit = true;

	public bool ISExchengePlan;

	public bool ISPlayer = true;

	public int MonstarID;

	public bool hideLearned;

	public bool showTooltip
	{
		get
		{
			bool result = false;
			if ((Object)(object)Tooltip.GetComponent<TooltipScale>() != (Object)null)
			{
				result = Tooltip.GetComponent<TooltipScale>().showTooltip;
			}
			else if (Object.op_Implicit((Object)(object)Tooltip.GetComponent<TooltipItem>()))
			{
				result = Tooltip.GetComponent<TooltipItem>().showTooltip;
			}
			else if (Object.op_Implicit((Object)(object)Tooltip.GetComponent<TooltipSkillTab>()))
			{
				result = Tooltip.GetComponent<TooltipSkillTab>().showTooltip;
			}
			return result;
		}
		set
		{
			GameObject val = null;
			val = Tooltip;
			if ((Object)(object)val.GetComponent<TooltipScale>() != (Object)null)
			{
				val.GetComponent<TooltipScale>().showTooltip = value;
			}
			else if (Object.op_Implicit((Object)(object)val.GetComponent<TooltipItem>()))
			{
				val.GetComponent<TooltipItem>().showTooltip = value;
			}
			else if (Object.op_Implicit((Object)(object)val.GetComponent<TooltipSkillTab>()))
			{
				val.GetComponent<TooltipSkillTab>().showTooltip = value;
			}
		}
	}

	public void Awake()
	{
		nowIndex = 0;
		datebase = ((Component)jsonData.instance).gameObject.GetComponent<ItemDatebase>();
		draggingItem = false;
		dragedItem = new item();
		if (shouldInit)
		{
			for (int i = 0; i < (int)count; i++)
			{
				inventory.Add(new item());
			}
			if (ISExchengePlan)
			{
				InitInventoryEX();
			}
			else
			{
				InitInventory();
			}
		}
	}

	private void Start()
	{
		GameObject val = (EquipTooltip = GameObject.Find("NewUIAutoToolTips"));
		if (ResetToolTips)
		{
			Tooltip = val;
		}
		BookTooltip = val;
		skillBookToolTip = val;
		DanyaoToolTip = val;
		YaoCaoToolTip = val;
		DanYaoToolTip = val;
		DanLuToolTip = val;
	}

	public void ZhengLi()
	{
		PlayerEx.Player.SortItem();
		LoadInventory();
	}

	public void setItemLeiXin(List<int> leixin)
	{
		if (!((Object)(object)JiaoYiManager.inst != (Object)null) || JiaoYiManager.inst.canClick)
		{
			inventItemLeiXing = leixin;
			if ((Object)(object)selectpage != (Object)null)
			{
				selectpage.RestePageIndex();
			}
			LoadInventory();
		}
	}

	public void setMonstartLeiXin(List<int> leixin)
	{
		if (!((Object)(object)JiaoYiManager.inst != (Object)null) || JiaoYiManager.inst.canClick)
		{
			inventItemLeiXing = leixin;
			if ((Object)(object)selectpage != (Object)null)
			{
				selectpage.RestePageIndex();
			}
			ExchangePlan exchengePlan = Singleton.ints.exchengePlan;
			MonstarLoadInventory(exchengePlan.MonstarID);
		}
	}

	public void setMonstarItemLeiXin1()
	{
		setMonstartLeiXin(new List<int>());
	}

	public void setMonstarItemLeiXin2()
	{
		setMonstartLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["2"]["ItemFlag"]));
	}

	public void setMonstarItemLeiXin3()
	{
		setMonstartLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["3"]["ItemFlag"]));
	}

	public void setMonstarItemLeiXin4()
	{
		setMonstartLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["4"]["ItemFlag"]));
	}

	public void setMonstarItemLeiXin5()
	{
		setMonstartLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["5"]["ItemFlag"]));
	}

	public void setMonstarItemLeiXin6()
	{
		setMonstartLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["6"]["ItemFlag"]));
	}

	public void setMonstarItemLeiXin7()
	{
		setMonstartLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["7"]["ItemFlag"]));
	}

	public void setItemLeiXin1()
	{
		setItemLeiXin(new List<int>());
	}

	public void setItemLeiXin2()
	{
		setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["2"]["ItemFlag"]));
	}

	public void setItemLeiXin3()
	{
		setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["3"]["ItemFlag"]));
	}

	public void setItemLeiXin4()
	{
		setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["4"]["ItemFlag"]));
	}

	public void setItemLeiXin5()
	{
		setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["5"]["ItemFlag"]));
	}

	public void setItemLeiXin6()
	{
		setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["6"]["ItemFlag"]));
	}

	public void setItemLeiXin7()
	{
		setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["7"]["ItemFlag"]));
	}

	public void setExItemLeiXin2()
	{
		setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["2"]["ItemFlag"]));
	}

	public void setExItemLeiXin3()
	{
		setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["3"]["ItemFlag"]));
	}

	public void setExItemLeiXin4()
	{
		setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["4"]["ItemFlag"]));
	}

	public void setExItemLeiXin5()
	{
		setItemLeiXin(new List<int> { 6 });
	}

	public void setExItemLeiXin6()
	{
		setItemLeiXin(new List<int> { 8 });
	}

	public void setExItemLeiXin7()
	{
		setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["6"]["ItemFlag"]));
	}

	public void Update()
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)Temp == (Object)null && (Object)(object)UI_Manager.inst != (Object)null)
		{
			Temp = ((Component)UI_Manager.inst.temp).gameObject;
		}
		if (draggingItem || ((Object)(object)Singleton.key != (Object)null && Singleton.key.draggingKey))
		{
			Temp.transform.position = UICamera.currentCamera.ScreenToWorldPoint(Input.mousePosition);
			Temp.GetComponent<UITexture>().mainTexture = (Texture)(object)dragedItem.itemIcon;
			showTooltip = false;
		}
		if (Input.GetMouseButtonUp(0))
		{
			BackItem();
		}
		if ((Input.GetKeyDown((KeyCode)27) || Input.GetKeyDown((KeyCode)9)) && showTooltip)
		{
			showTooltip = false;
		}
	}

	public void ChangeItem(ref item Item1, ref item Item2)
	{
		item item2 = new item();
		item2 = Item1;
		Item1 = Item2;
		Item2 = item2;
	}

	public void Clear_dragedItem()
	{
		dragedItem = new item();
		draggingItem = false;
		Temp.GetComponent<UITexture>().mainTexture = null;
	}

	public void BackItem()
	{
		if (draggingItem)
		{
			inventory[dragedID] = dragedItem;
			Clear_dragedItem();
		}
	}

	private void Show()
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		showInventory = !showInventory;
		if (!showInventory)
		{
			showTooltip = false;
		}
		if (showInventory)
		{
			InventoryUI.transform.Find("Win").position = InventoryUI.transform.position;
		}
		InventoryUI.SetActive(showInventory);
		Singleton.UI.UI_Top(InventoryUI.transform);
	}

	private void InitInventory()
	{
		InitSlot("Slot");
		InventoryUI.SetActive(showInventory);
	}

	public void InitSlot(string SlotName, string ParentPatch = "Win/item")
	{
		InitSlot(SlotName, (int)count, ParentPatch);
	}

	public void InitSlot<T>(string SlotName, int _count, EventsetItemCell<T> eventcell, string ParentPatch = "Win/item") where T : ItemCell
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < _count; i++)
		{
			GameObject val = (GameObject)Object.Instantiate(Resources.Load(SlotName));
			val.transform.parent = ((Component)InventoryUI.transform.Find(ParentPatch)).transform;
			val.transform.localScale = new Vector3(1f, 1f, 1f);
			((Component)val.transform.parent).GetComponent<UIGrid>().repositionNow = true;
			((Object)val).name = i.ToString();
			val.GetComponent<T>().isPlayer = ISPlayer;
			val.GetComponent<T>().inventory = this;
			eventcell(val.GetComponent<T>());
			if (inventory[i].itemName != null)
			{
				val.GetComponent<T>().Icon.GetComponent<UITexture>().mainTexture = (Texture)(object)inventory[i].itemIcon;
			}
		}
	}

	public void InitSlot(string SlotName, int _count, string ParentPatch = "Win/item")
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Expected O, but got Unknown
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < _count; i++)
		{
			GameObject val = null;
			if ((Object)(object)BaseSlotOBJ != (Object)null)
			{
				val = Object.Instantiate<GameObject>(BaseSlotOBJ);
				val.SetActive(true);
			}
			else
			{
				val = (GameObject)Object.Instantiate(Resources.Load(SlotName));
			}
			val.transform.parent = ((Component)InventoryUI.transform.Find(ParentPatch)).transform;
			val.transform.localScale = new Vector3(1f, 1f, 1f);
			((Component)val.transform.parent).GetComponent<UIGrid>().repositionNow = true;
			((Object)val).name = i.ToString();
			val.GetComponent<ItemCell>().isPlayer = ISPlayer;
			val.GetComponent<ItemCell>().inventory = this;
			val.GetComponent<ItemCell>().Btn1Text = NomelBtn1Text;
			val.GetComponent<ItemCell>().AutoSetBtnText = AutoSetBtnText;
			if (inventory[i].itemName != null)
			{
				val.GetComponent<ItemCell>().Icon.GetComponent<UITexture>().mainTexture = (Texture)(object)inventory[i].itemIcon;
			}
		}
	}

	private void InitInventoryEX()
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		GameObject val = exchengkey;
		if ((Object)(object)val == (Object)null)
		{
			val = ((!ISPlayer) ? GameObject.Find("UI Root (2D)/exchangePlan/Panel/inventoryGuiAvatarEX/Win/item").gameObject : GameObject.Find("UI Root (2D)/exchangePlan/Panel/inventoryGuiEX/Win/item").gameObject);
		}
		if (!isNewJiaoYi)
		{
			for (int i = 0; i < (int)count; i++)
			{
				GameObject val2 = (GameObject)Object.Instantiate(Resources.Load("SlotEx"));
				if (i < 24)
				{
					val2.transform.parent = ((Component)InventoryUI.transform.Find("Win/item")).transform;
				}
				else
				{
					val2.transform.parent = val.transform;
				}
				val2.transform.localScale = new Vector3(1f, 1f, 1f);
				((Component)val2.transform.parent).GetComponent<UIGrid>().repositionNow = true;
				((Object)val2).name = i.ToString();
				val2.GetComponent<ItemCellEX>().isPlayer = ISPlayer;
				val2.GetComponent<ItemCellEX>().inventory = this;
				val2.GetComponent<ItemCellEX>().JustShow = JustShow;
				val2.GetComponent<ItemCellEX>().Btn1Text = NomelBtn1Text;
				val2.GetComponent<ItemCellEX>().AutoSetBtnText = AutoSetBtnText;
				if (inventory[i].itemName != null)
				{
					val2.GetComponent<ItemCellEX>().Icon.GetComponent<UITexture>().mainTexture = (Texture)(object)inventory[i].itemIcon;
				}
			}
		}
		InventoryUI.SetActive(showInventory);
	}

	public void AddItem(int id)
	{
		int i;
		for (i = 0; i < inventory.Count; i++)
		{
			if (InventoryContains(id))
			{
				if (inventory[i].itemID == id && inventory[i].itemNum < inventory[i].itemMaxNum)
				{
					inventory[i].itemNum++;
					break;
				}
			}
			else if (inventory[i].itemName == null)
			{
				inventory[i] = datebase.items[id].Clone();
				break;
			}
		}
		if (i != inventory.Count)
		{
			return;
		}
		for (i = 0; i < inventory.Count; i++)
		{
			if (inventory[i].itemName == null)
			{
				inventory[i] = datebase.items[id].Clone();
				break;
			}
		}
	}

	public bool is_Full(item Item, int num)
	{
		bool result = true;
		for (int i = 0; i < inventory.Count; i++)
		{
			if (inventory[i].itemID == Item.itemID)
			{
				if (Item.itemType == item.ItemType.Potion)
				{
					if (inventory[i].itemMaxNum - inventory[i].itemNum >= num)
					{
						result = false;
						break;
					}
				}
				else if (inventory[i].itemID == -1)
				{
					result = false;
					break;
				}
			}
			else if (inventory[i].itemID == -1)
			{
				result = false;
				break;
			}
		}
		return result;
	}

	public bool isFull(int type, string UUID)
	{
		int startIndex = 0;
		int endIndex = 24;
		if (isNewJiaoYi)
		{
			endIndex = 15;
		}
		if (type == 1)
		{
			startIndex = 24;
			if (isNewJiaoYi)
			{
				startIndex = 15;
			}
			endIndex = (int)count;
		}
		return isFull(startIndex, endIndex, UUID);
	}

	public bool isFull(int startIndex, int endIndex, string UUID)
	{
		bool result = false;
		for (int i = startIndex; i < endIndex; i++)
		{
			if (inventory[i].itemName == null)
			{
				result = true;
				break;
			}
			if (inventory[i].UUID == UUID)
			{
				result = true;
				break;
			}
		}
		return result;
	}

	public void exAddItem1(int addType, string uuid, int num = 1, int addChaifenTyep = 0, int AddToIndex = -1)
	{
		int startIndex;
		int fanYeCount;
		if (addType == 1)
		{
			startIndex = (int)FanYeCount;
			fanYeCount = (int)count;
		}
		else
		{
			startIndex = 0;
			fanYeCount = (int)FanYeCount;
		}
		exAddItem(startIndex, fanYeCount, uuid, num, addChaifenTyep, AddToIndex);
	}

	public void exAddItem(int startIndex, int endIndex, string uuid, int num = 1, int addChaifenTyep = 0, int AddToIndex = -1)
	{
		int numIndex = 0;
		jsonData.InventoryNUM num2 = ((startIndex == 0) ? FanYeCount : ((jsonData.InventoryNUM)0));
		int num3 = (int)((startIndex == 0) ? count : FanYeCount);
		for (int i = (int)num2; i < num3; i++)
		{
			if (inventory[i].UUID == uuid)
			{
				numIndex = i;
				break;
			}
		}
		if (InventoryContains(startIndex, endIndex, uuid))
		{
			for (int j = startIndex; j < endIndex; j++)
			{
				if (inventory[j].UUID == uuid)
				{
					inventory[j].itemNum += num;
					break;
				}
			}
			return;
		}
		for (int k = startIndex; k < endIndex; k++)
		{
			if (inventory[k].itemName == null)
			{
				setInventoryIndexItem(k, numIndex, num, uuid);
				break;
			}
		}
	}

	public void setindexItem(int i, int ItemID, int num, string uuid)
	{
		inventory[i] = datebase.items[ItemID].Clone();
		inventory[i].UUID = uuid;
		inventory[i].itemNum = num;
	}

	public void setInventoryIndexItem(int i, int numIndex, int num, string uuid)
	{
		inventory[i] = datebase.items[inventory[numIndex].itemID].Clone();
		inventory[i].UUID = uuid;
		inventory[i].Seid = inventory[numIndex].Seid;
		inventory[i].itemNum = num;
	}

	public void reduceItem1(int addType, string uuid, int num = 1)
	{
		int num2 = 24;
		if (isNewJiaoYi)
		{
			num2 = 15;
		}
		int startIndex;
		int endIndex;
		if (addType == 1)
		{
			startIndex = num2;
			endIndex = (int)count;
		}
		else
		{
			startIndex = 0;
			endIndex = num2;
		}
		reduceItem(startIndex, endIndex, uuid, num);
	}

	public void reduceItem(int startIndex, int endIndex, string uuid, int num = 1)
	{
		for (int i = startIndex; i < endIndex; i++)
		{
			if (!(inventory[i].UUID == uuid))
			{
				continue;
			}
			inventory[i].itemNum -= num;
			if (inventory[i].itemNum <= 0)
			{
				int itemNum = inventory[i].itemNum;
				inventory[i] = new item();
				if (itemNum < 0)
				{
					reduceItem(startIndex, endIndex, uuid, -itemNum);
				}
			}
			break;
		}
	}

	public void reduceItem(int index, int num)
	{
		inventory[index].itemNum -= num;
		if (inventory[index].itemNum <= 0)
		{
			_ = inventory[index].itemNum;
			inventory[index] = new item();
		}
	}

	public void RemoveItem(int id)
	{
		Tools.instance.getPlayer().removeItem(id);
		for (int i = 0; i < inventory.Count; i++)
		{
			if (inventory[i].itemID == id)
			{
				inventory[i] = new item();
				break;
			}
		}
	}

	private bool InventoryContains(int id)
	{
		bool flag = false;
		for (int i = 0; i < inventory.Count; i++)
		{
			flag = inventory[i].itemID == id;
			if (flag)
			{
				break;
			}
		}
		return flag;
	}

	private bool InventoryContains(int startindex, int endIndex, string UUID)
	{
		bool flag = false;
		for (int i = startindex; i < endIndex; i++)
		{
			flag = inventory[i].UUID == UUID;
			if (flag)
			{
				break;
			}
		}
		return flag;
	}

	public static string getSkillBookDesc(JSONObject info)
	{
		string text = info["desc"].Str;
		if (info["type"].I == 3)
		{
			int num = (int)float.Parse(text);
			foreach (KeyValuePair<string, JSONObject> skillJsonDatum in jsonData.instance.skillJsonData)
			{
				if (skillJsonDatum.Value["Skill_ID"].I == num && skillJsonDatum.Value["Skill_Lv"].I == Tools.instance.getPlayer().getLevelType())
				{
					text = Tools.getDescByID(Tools.instance.Code64ToString(skillJsonDatum.Value["descr"].str), skillJsonDatum.Value["id"].I);
					break;
				}
			}
		}
		else if (info["type"].I == 4)
		{
			int num2 = (int)float.Parse(text);
			foreach (JSONObject item in jsonData.instance.StaticSkillJsonData.list)
			{
				if (item["Skill_ID"].I == num2 && item["Skill_Lv"].I == 1)
				{
					text = Tools.instance.Code64ToString(item["descr"].str);
					break;
				}
			}
		}
		return text;
	}

	public static string GetSkillBookDesc(_ItemJsonData info)
	{
		string text = info.desc;
		if (info.type == 3)
		{
			int num = (int)float.Parse(text);
			int levelType = Tools.instance.getPlayer().getLevelType();
			foreach (_skillJsonData data in _skillJsonData.DataList)
			{
				if (data.Skill_ID == num && data.Skill_Lv == levelType)
				{
					text = Tools.getDescByID(data.descr, data.id);
					break;
				}
			}
		}
		else if (info.type == 4)
		{
			int num2 = (int)float.Parse(text);
			foreach (StaticSkillJsonData data2 in StaticSkillJsonData.DataList)
			{
				if (data2.Skill_ID == num2 && data2.Skill_Lv == 1)
				{
					text = data2.descr;
					break;
				}
			}
		}
		return text;
	}

	public GameObject CreatGameObjectToParent(GameObject parent, GameObject Temp)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		GameObject gameObject = ((Component)Object.Instantiate<Transform>(Temp.transform)).gameObject;
		gameObject.transform.SetParent(parent.transform);
		gameObject.SetActive(true);
		gameObject.transform.localScale = Vector3.one;
		return gameObject;
	}

	public void autoSetTooltip()
	{
		Tooltip = Singleton.inventory.Tooltip;
		EquipTooltip = Singleton.inventory.EquipTooltip;
		BookTooltip = Singleton.inventory.BookTooltip;
		skillBookToolTip = Singleton.inventory.skillBookToolTip;
		DanyaoToolTip = Singleton.inventory.DanyaoToolTip;
		YaoCaoToolTip = Singleton.inventory.YaoCaoToolTip;
		DanYaoToolTip = Singleton.inventory.DanYaoToolTip;
		DanLuToolTip = Singleton.inventory.DanLuToolTip;
	}

	public static int GetItemCD(item Item)
	{
		int value = EquipSeidJsonData2.DataDict[Item.itemID].value1;
		int oldCD = 1;
		if (SkillSeidJsonData29.DataDict.ContainsKey(value))
		{
			oldCD = SkillSeidJsonData29.DataDict[value].value1;
		}
		return GetItemCD(Item.Seid, oldCD);
	}

	public static int GetItemCD(JSONObject Seid, int oldCD)
	{
		if (Seid == null || !Seid.HasField("SkillSeids"))
		{
			return oldCD;
		}
		return Seid["SkillSeids"].list.Find((JSONObject aa) => aa["id"].I == 29)["value1"].I;
	}

	public static string GetItemName(item Item, string baseName = "")
	{
		return GetItemName(Item.Seid, baseName);
	}

	public static string GetItemName(JSONObject Seid, string baseName = "")
	{
		if (Seid == null || !Seid.HasField("Name"))
		{
			return baseName;
		}
		return Seid["Name"].str;
	}

	public static int GetItemQuality(item Item, int oldquality)
	{
		if (Item.Seid == null || !Item.Seid.HasField("quality"))
		{
			return oldquality;
		}
		return Item.Seid["quality"].I;
	}

	public static JSONObject GetItemAttackType(JSONObject Seid, JSONObject oldAttack)
	{
		if (Seid == null || !Seid.HasField("AttackType"))
		{
			return oldAttack;
		}
		return Seid["AttackType"];
	}

	public static string GetItemFirstDesc(JSONObject Seid, string oldAttack)
	{
		if (Seid == null || !Seid.HasField("SeidDesc"))
		{
			return oldAttack;
		}
		return Seid["SeidDesc"].Str;
	}

	public static string GetItemDesc(JSONObject Seid, string oldAttack)
	{
		if (Seid == null || !Seid.HasField("Desc"))
		{
			return oldAttack;
		}
		return Seid["Desc"].Str;
	}

	public void Show_Tooltip(item Item, int money = 0, int moneyPercent = 0)
	{
		try
		{
			_ItemJsonData itemJsonData = _ItemJsonData.DataDict[Item.itemID];
			JSONObject jSONObject = jsonData.instance.ItemJsonData[Item.itemID.ToString()];
			if (Item.Seid == null)
			{
				Item.Seid = new JSONObject();
			}
			string name = itemJsonData.name;
			TooltipItem component = Tooltip.GetComponent<TooltipItem>();
			component.Clear();
			string text = GetSkillBookDesc(itemJsonData);
			if (itemJsonData.type == 0)
			{
				int value = EquipSeidJsonData2.DataDict[itemJsonData.id].value1;
				JSONObject jSONObject2 = jsonData.instance.skillJsonData[value.ToString()];
				int itemCD = GetItemCD(Item);
				component.Label7.text = itemCD + "回合";
				string text2 = "";
				foreach (JSONObject item in GetItemAttackType(Item.Seid, jSONObject2["AttackType"]).list)
				{
					text2 += Tools.getStr("xibieFight" + item.I);
				}
				component.Label8.text = text2;
				component.setCenterTextTitle("【冷却】", "【属性】");
				text = "[f28125]【主动】[-] [E0DDB4]" + text.Replace("主动：", "");
				showToolType = 1;
			}
			else if (itemJsonData.type != 4 && itemJsonData.type != 13 && (itemJsonData.type != 3 || !(name != "情报玉简")))
			{
				if (itemJsonData.type == 6)
				{
					Avatar player = Tools.instance.getPlayer();
					string liDanLeiXinStr = Tools.getLiDanLeiXinStr(itemJsonData.yaoZhi2);
					string liDanLeiXinStr2 = Tools.getLiDanLeiXinStr(itemJsonData.yaoZhi3);
					string liDanLeiXinStr3 = Tools.getLiDanLeiXinStr(itemJsonData.yaoZhi1);
					component.Label7.text = (player.GetHasZhuYaoShuXin(Item.itemID, itemJsonData.quality) ? liDanLeiXinStr : "未知");
					component.Label8.text = (player.GetHasFuYaoShuXin(Item.itemID, itemJsonData.quality) ? liDanLeiXinStr2 : "未知");
					component.Label9.text = (player.GetHasYaoYinShuXin(Item.itemID, itemJsonData.quality) ? liDanLeiXinStr3 : "未知");
					component.setCenterTextTitle("【主药】", "【辅药】", "【药引】");
					showToolType = 6;
				}
				else if (itemJsonData.type == 9 || itemJsonData.type == 14)
				{
					int num = -1;
					if (!Item.Seid.HasField("NaiJiu"))
					{
						Item.Seid = Tools.CreateItemSeid(Item.itemID);
					}
					component.setCenterTextTitle("【耐久】");
					num = Item.Seid["NaiJiu"].I;
					int num2 = 100;
					if (itemJsonData.type == 14)
					{
						num2 = (int)jsonData.instance.LingZhouPinJie[itemJsonData.quality.ToString()][(object)"Naijiu"];
					}
					component.Label7.text = num + "/" + num2;
					showToolType = 9;
				}
				else if (itemJsonData.type == 5)
				{
					component.setCenterTextTitle("【耐药】", "【丹毒】");
					component.Label8.text = string.Concat(jSONObject["DanDu"].I);
					int jsonobject = Tools.getJsonobject(Tools.instance.getPlayer().NaiYaoXin, Item.itemID.ToString());
					int itemCanUseNum = GUIPackage.item.GetItemCanUseNum(itemJsonData.id);
					component.Label7.text = jsonobject + "/" + itemCanUseNum;
					component.ShowPlayerInfo();
					showToolType = 5;
				}
				else
				{
					showToolType = 0;
				}
			}
			Regex regex = new Regex("\\{STVar=\\d*\\}");
			MatchCollection matchCollection = Regex.Matches(text, "\\{STVar=\\d*\\}");
			foreach (Match item2 in matchCollection)
			{
				if (int.TryParse(item2.Value.Replace("{STVar=", "").Replace("}", ""), out var result))
				{
					text = regex.Replace(text, GlobalValue.Get(result, "Inventory2.Show_Tooltip").ToString());
				}
			}
			Regex.Matches(text, "【\\w*】");
			foreach (Match item3 in matchCollection)
			{
				text = text.Replace(item3.Value, "[42E395]" + item3.Value + "[-]");
			}
			component.Label1.text = "[e0ddb4]" + GetItemFirstDesc(Item.Seid, text);
			component.Label2.text = "[bfba7d]" + GetItemDesc(Item.Seid, itemJsonData.desc2);
			if (UINPCJiaoHu.isDebugMode)
			{
				UILabel label = component.Label2;
				label.text = label.text + "\nUUID:" + Item.UUID;
			}
			int num3 = GetItemQuality(Item, itemJsonData.quality);
			List<string> tootipItemQualityColor = jsonData.instance.TootipItemQualityColor;
			string newValue = tootipItemQualityColor[num3 - 1] + Tools.getStr("shuzi" + num3) + Tools.getStr("jiecailiao");
			if (itemJsonData.type == 0 || itemJsonData.type == 1 || itemJsonData.type == 2)
			{
				num3++;
				if (Item.Seid != null && Item.Seid.HasField("qualitydesc"))
				{
					newValue = tootipItemQualityColor[num3 - 1] + Item.Seid["qualitydesc"].Str;
				}
				else
				{
					int num4 = ((Item.Seid != null && Item.Seid.HasField("QPingZhi")) ? Item.Seid["QPingZhi"].I : itemJsonData.typePinJie);
					newValue = tootipItemQualityColor[num3 - 1] + ((num4 > 0) ? (Tools.getStr("shangzhongxia" + num4) + "品") : "") + Tools.getStr("EquipPingji" + (num3 - 1));
				}
			}
			else if (itemJsonData.type == 3 || itemJsonData.type == 4)
			{
				num3 *= 2;
				newValue = tootipItemQualityColor[num3 - 1] + Tools.getStr("pingjie" + itemJsonData.quality) + Tools.getStr("shangzhongxia" + itemJsonData.typePinJie);
			}
			else if (itemJsonData.type == 5 || itemJsonData.type == 9)
			{
				newValue = tootipItemQualityColor[num3 - 1] + Tools.getStr("shuzi" + num3) + Tools.getStr("pingdianyao");
			}
			else if (itemJsonData.type == 6 || itemJsonData.type == 7 || itemJsonData.type == 8)
			{
				newValue = tootipItemQualityColor[num3 - 1] + Tools.getStr("shuzi" + num3) + Tools.getStr("jiecailiao");
				if (itemJsonData.type == 8)
				{
					int wuWeiType = itemJsonData.WuWeiType;
					string text3 = ((wuWeiType != 0) ? LianQiWuWeiBiao.DataDict[wuWeiType].desc : "无");
					component.Label7.text = text3;
					int shuXingType = itemJsonData.ShuXingType;
					string text4 = "";
					text4 = ((shuXingType != 0) ? LianQiShuXinLeiBie.DataDict[shuXingType].desc : "无");
					component.Label8.text = text4;
					component.setCenterTextTitle("【种类】", "【属性】");
				}
			}
			component.Label3.text = Tools.getStr("pingjieCell").Replace("{X}", newValue).Replace("[333333]品级：", "");
			component.Label4.text = jsonData.instance.TootipItemNameColor[num3 - 1] + GetItemName(Item, itemJsonData.name);
			component.Label5.text = Tools.getStr("ItemType" + itemJsonData.type);
			if (money != 0)
			{
				int num5 = money;
				if (Item.Seid != null && Item.Seid.HasField("NaiJiu"))
				{
					num5 = (int)((float)num5 * ItemCellEX.getItemNaiJiuPrice(Item));
				}
				((Component)((Component)component.Label6).transform.parent).gameObject.SetActive(true);
				component.Label6.text = string.Concat(num5);
				if (moneyPercent > 0)
				{
					component.Label6.text = $"[D55D21]{num5}";
				}
				else if (moneyPercent < 0)
				{
					component.Label6.text = $"[75C0AE]{num5}";
				}
				component.ShowMoney();
			}
			else
			{
				((Component)((Component)component.Label6).transform.parent).gameObject.SetActive(false);
			}
			component.icon.mainTexture = (Texture)(object)Item.itemIcon;
			component.pingZhi.mainTexture = (Texture)(object)Item.itemPingZhi;
		}
		catch (Exception ex)
		{
			TooltipItem component2 = Tooltip.GetComponent<TooltipItem>();
			component2.Clear();
			component2.Label2.text = "[bfba7d]暂无说明[-]";
			Debug.LogError((object)("物品出错" + Item.itemID));
			Debug.LogError((object)ex);
		}
	}

	public void SaveInventory()
	{
	}

	public int addItemToNullInventory(int id, int num, string uuid, JSONObject Seid)
	{
		foreach (item item in inventory)
		{
			if (item.UUID == uuid)
			{
				if (item.itemNum >= num)
				{
					return -1;
				}
				num -= item.itemNum;
			}
		}
		if (ISPlayer && Tools.instance.getPlayer().FindEquipItemByUUID(uuid) != null)
		{
			return -2;
		}
		for (int i = 0; i < inventory.Count; i++)
		{
			if (inventory[i].itemID == -1)
			{
				inventory[i] = datebase.items[id].Clone();
				inventory[i].UUID = uuid;
				inventory[i].Seid = Seid;
				inventory[i].itemNum = num;
				return i;
			}
		}
		return -1;
	}

	public bool isInPage(int page, int nowIndex, int OnePageMaxNum)
	{
		if (nowIndex >= page * OnePageMaxNum && nowIndex < (page + 1) * OnePageMaxNum)
		{
			return true;
		}
		return false;
	}

	public void resteInventoryItem()
	{
		int num = inventory.Count;
		if (num > (int)FanYeCount && ISExchengePlan)
		{
			num = 24;
		}
		if (isNewJiaoYi)
		{
			num = 15;
		}
		for (int i = 0; i < num; i++)
		{
			inventory[i] = new item();
			inventory[i].itemNum = 1;
		}
	}

	public void resteAllInventoryItem()
	{
		for (int i = 0; i < inventory.Count; i++)
		{
			inventory[i] = new item();
			inventory[i].itemNum = 1;
		}
	}

	public void loadInventory(JSONObject json)
	{
		foreach (JSONObject item in json.list)
		{
			addItemToNullInventory(item["ID"].I, item["Num"].I, item["UUID"].str, null);
		}
	}

	public void loadNewNPCDropInventory(JSONObject json)
	{
		foreach (JSONObject item in json.list)
		{
			addItemToNullInventory(item["ID"].I, item["Num"].I, item["UUID"].str, item["seid"]);
		}
	}

	public void LoadInventory()
	{
		resteInventoryItem();
		Avatar player = Tools.instance.getPlayer();
		new List<ITEM_INFO>();
		int num = 0;
		foreach (ITEM_INFO value in player.itemList.values)
		{
			if (player.FindEquipItemByUUID(value.uuid) != null)
			{
				continue;
			}
			if (!jsonData.instance.ItemJsonData.ContainsKey(string.Concat(value.itemId)))
			{
				Debug.LogError((object)("找不到物品" + value.itemId));
				continue;
			}
			JSONObject info = jsonData.instance.ItemJsonData[value.itemId.ToString()];
			if (((int)info["quality"].n != inventoryItemType && inventoryItemType != 0) || (inventItemLeiXing.Count != 0 && inventItemLeiXing.FindAll((int a) => a == (int)info["type"].n).Count <= 0))
			{
				continue;
			}
			if (hideLearned)
			{
				if ((int)info["type"].n == 3)
				{
					int getskillID2;
					if (value.itemId > jsonData.QingJiaoItemIDSegment)
					{
						getskillID2 = jsonData.instance.ItemsSeidJsonData[1][(value.itemId - jsonData.QingJiaoItemIDSegment).ToString()]["value1"].I;
					}
					else
					{
						getskillID2 = jsonData.instance.ItemsSeidJsonData[1][value.itemId.ToString()]["value1"].I;
					}
					if (Tools.instance.getPlayer().hasSkillList.Find((SkillItem aa) => aa.itemId == getskillID2) != null)
					{
						continue;
					}
				}
				else if ((int)info["type"].n == 4)
				{
					int getskillID;
					if (value.itemId > jsonData.QingJiaoItemIDSegment)
					{
						getskillID = jsonData.instance.ItemsSeidJsonData[2][(value.itemId - jsonData.QingJiaoItemIDSegment).ToString()]["value1"].I;
					}
					else
					{
						getskillID = jsonData.instance.ItemsSeidJsonData[2][value.itemId.ToString()]["value1"].I;
					}
					if (Tools.instance.getPlayer().hasStaticSkillList.Find((SkillItem aa) => aa.itemId == getskillID) != null)
					{
						continue;
					}
				}
				else if ((int)info["type"].n == 10)
				{
					int id = (int)jsonData.instance.ItemsSeidJsonData[13][value.itemId.ToString()]["value1"].n;
					if (Tools.instance.getPlayer().ISStudyDanFan(id))
					{
						continue;
					}
				}
			}
			if (isInPage(nowIndex, num, (int)FanYeCount))
			{
				addItemToNullInventory(value.itemId, (int)value.itemCount, value.uuid, value.Seid);
			}
			num++;
		}
		if ((Object)(object)selectpage != (Object)null)
		{
			selectpage.setMaxPage(player.itemList.values.Count / (int)FanYeCount + 1);
		}
	}

	public bool HasUUIDItem(int start, int end, string uuid)
	{
		for (int i = start; i < end; i++)
		{
			if (inventory[i].UUID == uuid)
			{
				return true;
			}
		}
		return false;
	}

	public void PaiMaiMonstarLoad(int MonstarID)
	{
		this.MonstarID = MonstarID;
		resteInventoryItem();
		_ = jsonData.instance.AvatarBackpackJsonData[string.Concat(MonstarID)];
		List<JSONObject> list = jsonData.instance.AvatarBackpackJsonData[string.Concat(MonstarID)]["Backpack"].list.FindAll((JSONObject ccc) => (int)ccc["CanSell"].n == 1 && (int)ccc["Num"].n > 0);
		foreach (JSONObject item in list)
		{
			if (item.HasField("paiMaiPlayer") && (int)item["paiMaiPlayer"].n == 2)
			{
				addItemToNullInventory(item["ItemID"].I, item["Num"].I, item["UUID"].str, item["Seid"]);
			}
		}
		if ((Object)(object)selectpage != (Object)null)
		{
			selectpage.setMaxPage(list.Count / (int)FanYeCount + 1);
		}
	}

	public void MonstarLoadInventory(int MonstarID)
	{
		this.MonstarID = MonstarID;
		resteInventoryItem();
		_ = jsonData.instance.AvatarBackpackJsonData[string.Concat(MonstarID)];
		int num = 0;
		List<JSONObject> list = jsonData.instance.AvatarBackpackJsonData[string.Concat(MonstarID)]["Backpack"].list.FindAll((JSONObject ccc) => (int)ccc["CanSell"].n == 1 && (int)ccc["Num"].n > 0);
		foreach (JSONObject item2 in list)
		{
			if ((jsonData.instance.ItemJsonData[string.Concat(item2["ItemID"].I)]["quality"].I == inventoryItemType || inventoryItemType == 0) && (inventItemLeiXing.Count == 0 || inventItemLeiXing.FindAll((int a) => a == jsonData.instance.ItemJsonData[string.Concat(item2["ItemID"].I)]["type"].I).Count > 0))
			{
				if (isInPage(nowIndex, num, (int)FanYeCount))
				{
					addItemToNullInventory(item2["ItemID"].I, item2["Num"].I, item2["UUID"].str, item2["Seid"]);
				}
				num++;
			}
		}
		if ((Object)(object)selectpage != (Object)null)
		{
			selectpage.setMaxPage(list.Count / (int)FanYeCount + 1);
		}
	}

	public bool CanClick()
	{
		if ((Object)(object)GameObject.Find("CanvasChoice(Clone)") != (Object)null)
		{
			return false;
		}
		return true;
	}

	public void UseItem(int id)
	{
	}

	public int GetSoltNum()
	{
		int num = 0;
		for (int i = 0; i < inventory.Count; i++)
		{
			if (inventory[i].itemID == -1)
			{
				num++;
			}
		}
		return num;
	}
}
