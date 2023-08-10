using System.Collections.Generic;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
	public PlayerSetRandomFace Face;

	public Text TitleText;

	public Text MoneyText;

	public RectTransform SlotSVRT;

	public GameObject FilterGO;

	public Animator LianZiAnim;

	public Animator RightFilterAnim;

	private bool filterActive;

	[HideInInspector]
	public int UITmpValue;

	public UIInvFilterBtn FilterA;

	public UIInvFilterBtn FilterB;

	public List<UIInvFilterBtn> FilterBtns = new List<UIInvFilterBtn>();

	public UIIconShow.ShowPriceType ShowPriceType;

	public DragAera SVSliderDragAera;

	public RectTransform SVSliderButtomPos;

	public Scrollbar SVBar;

	public Button SortBtn;

	public UIInventoryLayout InventoryLayout;

	public bool CanDrag;

	public int NPCID;

	private bool isSVSliderDragging;

	private float sliderBeginDragY;

	private float sliderBeginDragMouseY;

	private float sliderPer;

	[HideInInspector]
	public bool IsGuDing;

	public InventoryType InventoryType;

	public SpecialInventoryMode SpecialInventoryMode;

	[HideInInspector]
	public Inventory2 oldInventory;

	public bool IsPlayer;

	public int ID;

	public bool FilterCanSell = true;

	public bool FilterNum = true;

	public bool FilterQingJiao = true;

	public int FilterQuality;

	public int FilterType;

	private static Dictionary<int, List<int>> FilterTypeDict = new Dictionary<int, List<int>>
	{
		{
			0,
			new List<int>()
		},
		{
			1,
			new List<int> { 0, 1, 2 }
		},
		{
			2,
			new List<int> { 5, 15 }
		},
		{
			3,
			new List<int> { 3, 4, 10, 12, 13 }
		},
		{
			4,
			new List<int> { 8 }
		},
		{
			5,
			new List<int> { 6 }
		},
		{
			6,
			new List<int> { 7, 9, 14, 16 }
		}
	};

	public UIInventory dragTargetInventory;

	public UIIconShow dragTargetSlot;

	public UINPCData NPC;

	private float lastY;

	[HideInInspector]
	public bool FilterActive
	{
		get
		{
			return filterActive;
		}
		set
		{
			filterActive = value;
			if (filterActive)
			{
				ShowFilter();
			}
			else
			{
				HideFilter();
			}
		}
	}

	private float SliderPer
	{
		get
		{
			return sliderPer;
		}
		set
		{
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			sliderPer = value;
			SVSliderDragAera.RT.anchoredPosition = new Vector2(SVSliderDragAera.RT.anchoredPosition.x, sliderPer * SVSliderButtomPos.anchoredPosition.y);
		}
	}

	[HideInInspector]
	public Avatar Player => PlayerEx.Player;

	public void ShowFilter()
	{
		FilterGO.SetActive(true);
		((Component)LianZiAnim).gameObject.SetActive(true);
		if (FilterA.State == UIInvFilterBtn.BtnState.Active)
		{
			foreach (UIInvFilterBtn filterBtn in FilterBtns)
			{
				if (filterBtn.BtnData == FilterQuality)
				{
					filterBtn.State = UIInvFilterBtn.BtnState.Active;
				}
				else
				{
					filterBtn.State = UIInvFilterBtn.BtnState.Normal;
				}
			}
		}
		if (FilterB.State != UIInvFilterBtn.BtnState.Active)
		{
			return;
		}
		foreach (UIInvFilterBtn filterBtn2 in FilterBtns)
		{
			if (filterBtn2.BtnData == FilterType)
			{
				filterBtn2.State = UIInvFilterBtn.BtnState.Active;
			}
			else
			{
				filterBtn2.State = UIInvFilterBtn.BtnState.Normal;
			}
		}
	}

	public void HideFilter()
	{
		LianZiAnim.Play("Hide");
		RightFilterAnim.Play("Hide");
		((MonoBehaviour)this).Invoke("CloseFilter", 0.5f);
	}

	public void CloseFilter()
	{
		FilterGO.SetActive(false);
		((Component)LianZiAnim).gameObject.SetActive(false);
	}

	public void OnFilterBtnClick(UIInvFilterBtn btn)
	{
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		if (UITmpValue == -1)
		{
			if (FilterActive)
			{
				FilterA.State = UIInvFilterBtn.BtnState.Normal;
				FilterB.State = UIInvFilterBtn.BtnState.Normal;
				FilterActive = false;
				return;
			}
			FilterBtns[0].ShowText.text = "全部";
			FilterBtns[1].ShowText.text = "一品";
			FilterBtns[2].ShowText.text = "二品";
			FilterBtns[3].ShowText.text = "三品";
			FilterBtns[4].ShowText.text = "四品";
			FilterBtns[5].ShowText.text = "五品";
			FilterBtns[6].ShowText.text = "六品";
			btn.State = UIInvFilterBtn.BtnState.Active;
			FilterActive = true;
			return;
		}
		if (UITmpValue == -2)
		{
			if (FilterActive)
			{
				FilterA.State = UIInvFilterBtn.BtnState.Normal;
				FilterB.State = UIInvFilterBtn.BtnState.Normal;
				FilterActive = false;
				return;
			}
			FilterBtns[0].ShowText.text = "全部";
			FilterBtns[1].ShowText.text = "装备";
			FilterBtns[2].ShowText.text = "丹药";
			FilterBtns[3].ShowText.text = "秘籍";
			FilterBtns[4].ShowText.text = "材料";
			FilterBtns[5].ShowText.text = "草药";
			FilterBtns[6].ShowText.text = "其他";
			btn.State = UIInvFilterBtn.BtnState.Active;
			FilterActive = true;
			return;
		}
		if (FilterA.State == UIInvFilterBtn.BtnState.Active)
		{
			FilterQuality = btn.BtnData;
			if (FilterQuality == 0)
			{
				FilterA.ShowText.text = "品阶";
			}
			else
			{
				FilterA.ShowText.text = btn.ShowText.text;
			}
		}
		else if (FilterB.State == UIInvFilterBtn.BtnState.Active)
		{
			FilterType = btn.BtnData;
			if (FilterType == 0)
			{
				FilterB.ShowText.text = "类型";
			}
			else
			{
				FilterB.ShowText.text = btn.ShowText.text;
			}
		}
		FilterB.State = UIInvFilterBtn.BtnState.Normal;
		FilterA.State = UIInvFilterBtn.BtnState.Normal;
		FilterActive = false;
		InventoryLayout.RT.anchoredPosition = new Vector2(InventoryLayout.RT.anchoredPosition.x, 0f);
		RefreshUI();
	}

	public void OnSVSliderBeginDrag(PointerEventData eventData)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		sliderBeginDragY = SVSliderDragAera.RT.anchoredPosition.y;
		sliderBeginDragMouseY = eventData.position.y;
		isSVSliderDragging = true;
	}

	public void OnSVSliderDrag(PointerEventData eventData)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		isSVSliderDragging = true;
		float num = 1080f / (float)Screen.height * (eventData.position.y - sliderBeginDragMouseY);
		float num2 = sliderBeginDragY + num / 0.821f;
		num2 = Mathf.Clamp(num2, 0f, SVSliderButtomPos.anchoredPosition.y);
		SVSliderDragAera.RT.anchoredPosition = new Vector2(SVSliderDragAera.RT.anchoredPosition.x, num2);
		sliderPer = num2 / SVSliderButtomPos.anchoredPosition.y;
		SVBar.value = sliderPer;
	}

	public void OnSVSliderEndDrag(PointerEventData eventData)
	{
		isSVSliderDragging = false;
	}

	public void OnSVBarValueChanged(float value)
	{
		if (!isSVSliderDragging && ((Component)SVBar).gameObject.activeInHierarchy)
		{
			SliderPer = value;
		}
	}

	private void Awake()
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		SVSliderDragAera.OnBeginDragAction = OnSVSliderBeginDrag;
		SVSliderDragAera.OnDragAction = OnSVSliderDrag;
		SVSliderDragAera.OnEndDragAction = OnSVSliderEndDrag;
		SVSliderDragAera.RT.anchoredPosition = SVSliderButtomPos.anchoredPosition;
		((UnityEventBase)SVBar.onValueChanged).RemoveAllListeners();
		((UnityEvent<float>)(object)SVBar.onValueChanged).AddListener((UnityAction<float>)OnSVBarValueChanged);
	}

	private void Start()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		InventoryLayout.RT.anchoredPosition = new Vector2(InventoryLayout.RT.anchoredPosition.x, 0f);
	}

	private void Update()
	{
		if (Mathf.Abs(InventoryLayout.RTY - lastY) > 0.1f)
		{
			lastY = InventoryLayout.RTY;
			InventoryLayout.RefreshUI();
		}
	}

	public void RefreshUI()
	{
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		if (IsPlayer)
		{
			ID = 1;
		}
		((Component)Face).gameObject.SetActive(true);
		TitleText.text = Tools.getMonstarTitle(ID);
		if (IsPlayer)
		{
			Face.faceID = ID;
			Face.setFace();
			MoneyText.text = Player.money.ToString();
			if ((Object)(object)SortBtn != (Object)null)
			{
				((Component)SortBtn).gameObject.SetActive(true);
			}
		}
		else
		{
			Face.SetNPCFace(ID);
			MoneyText.text = NPC.BackpackJson["money"].I.ToString();
			if ((Object)(object)SortBtn != (Object)null)
			{
				((Component)SortBtn).gameObject.SetActive(false);
			}
		}
		InventoryLayout.Init();
		if (InventoryType == InventoryType.Item)
		{
			if (IsPlayer)
			{
				if (SpecialInventoryMode == SpecialInventoryMode.LingTian)
				{
					LoadPlayerLingTianItems();
				}
				else
				{
					LoadPlayerItems();
				}
			}
			else
			{
				LoadNPCItems();
			}
		}
		InventoryLayout.CalcGridPos();
		InventoryLayout.RefreshUI();
		int count = InventoryLayout.GridDataList.Count;
		SlotSVRT.sizeDelta = new Vector2(SlotSVRT.sizeDelta.x, (float)((count - 1) / 3 + 1) * 144.5f + 19f);
	}

	public void LoadPlayerItems()
	{
		List<string> list = new List<string>();
		foreach (ITEM_INFO value in Player.equipItemList.values)
		{
			list.Add(value.uuid);
		}
		int num = 0;
		foreach (ITEM_INFO value2 in Player.itemList.values)
		{
			if (list.Contains(value2.uuid))
			{
				continue;
			}
			if (!_ItemJsonData.DataDict.ContainsKey(value2.itemId))
			{
				Debug.LogError((object)("找不到物品" + value2.itemId));
				continue;
			}
			_ItemJsonData itemJsonData = _ItemJsonData.DataDict[value2.itemId];
			if (!FilterQingJiao && value2.itemId >= jsonData.QingJiaoItemIDSegment)
			{
				continue;
			}
			int num2 = itemJsonData.quality;
			if (value2.Seid != null && value2.Seid.HasField("quality"))
			{
				num2 = value2.Seid["quality"].I;
			}
			bool flag = FilterQuality == 0 || num2 == FilterQuality;
			if (itemJsonData.type == 3 || itemJsonData.type == 4)
			{
				flag = FilterQuality == 0 || num2 * 2 == FilterQuality;
			}
			if (itemJsonData.type == 0 || itemJsonData.type == 1 || itemJsonData.type == 2)
			{
				flag = FilterQuality == 0 || num2 + 1 == FilterQuality;
			}
			if (flag && (FilterType == 0 || FilterTypeDict[FilterType].Contains(itemJsonData.type)) && (!FilterCanSell || itemJsonData.CanSale == 0) && (!FilterNum || value2.itemCount != 0))
			{
				item item = new item(value2.itemId);
				item.Seid = value2.Seid;
				item.UUID = value2.uuid;
				UIInventoryGridData gridData = new UIInventoryGridData();
				gridData.Item = item;
				gridData.Index = num;
				gridData.ItemCount = (int)value2.itemCount;
				gridData.IconShowInitAction = delegate(UIIconShow s)
				{
					s.Inventory = this;
					s.isPlayer = IsPlayer;
					s.NPCID = NPCID;
					s.CanDrag = CanDrag;
					s.ShowPrice = UIIconShow.ShowPriceType.PlayerSell;
					s.isShowStudy = true;
					s.SetItem(gridData.Item);
					s.Count = gridData.ItemCount;
				};
				InventoryLayout.GridDataList.Add(gridData);
				num++;
			}
		}
	}

	public void LoadPlayerLingTianItems()
	{
		List<string> list = new List<string>();
		foreach (ITEM_INFO value in Player.equipItemList.values)
		{
			list.Add(value.uuid);
		}
		int num = 0;
		foreach (ITEM_INFO value2 in Player.itemList.values)
		{
			if (DFBuKeZhongZhi.DataDict.ContainsKey(value2.itemId) || list.Contains(value2.uuid))
			{
				continue;
			}
			if (!_ItemJsonData.DataDict.ContainsKey(value2.itemId))
			{
				Debug.LogError((object)("找不到物品" + value2.itemId));
				continue;
			}
			_ItemJsonData itemJsonData = _ItemJsonData.DataDict[value2.itemId];
			if (!FilterQingJiao && value2.itemId >= jsonData.QingJiaoItemIDSegment)
			{
				continue;
			}
			int num2 = itemJsonData.quality;
			if (value2.Seid != null && value2.Seid.HasField("quality"))
			{
				num2 = value2.Seid["quality"].I;
			}
			if (itemJsonData.type == 6 && (FilterQuality == 0 || num2 == FilterQuality) && (!FilterCanSell || itemJsonData.CanSale == 0) && (!FilterNum || value2.itemCount != 0))
			{
				item item = new item(value2.itemId);
				item.Seid = value2.Seid;
				item.UUID = value2.uuid;
				UIInventoryGridData gridData = new UIInventoryGridData();
				gridData.Item = item;
				gridData.Index = num;
				gridData.ItemCount = (int)value2.itemCount;
				gridData.IconShowInitAction = delegate(UIIconShow s)
				{
					s.Inventory = this;
					s.isPlayer = IsPlayer;
					s.NPCID = NPCID;
					s.CanDrag = CanDrag;
					s.ShowPrice = UIIconShow.ShowPriceType.PlayerSell;
					s.isShowStudy = true;
					s.SetItem(gridData.Item);
					s.Count = gridData.ItemCount;
				};
				InventoryLayout.GridDataList.Add(gridData);
				num++;
			}
		}
	}

	public void LoadNPCItems()
	{
		int num = 0;
		foreach (item item in NPC.Inventory)
		{
			_ItemJsonData itemJsonData = _ItemJsonData.DataDict[item.itemID];
			if (!FilterQingJiao && item.itemID >= jsonData.QingJiaoItemIDSegment)
			{
				continue;
			}
			int quality = item.quality;
			int type = itemJsonData.type;
			bool flag = FilterQuality == 0 || quality == FilterQuality;
			if (type == 3 || type == 4)
			{
				flag = FilterQuality == 0 || quality * 2 == FilterQuality;
			}
			if (itemJsonData.type == 0 || itemJsonData.type == 1 || itemJsonData.type == 2)
			{
				flag = FilterQuality == 0 || quality + 1 == FilterQuality;
			}
			if (flag && (FilterType == 0 || FilterTypeDict[FilterType].Contains(item.itemtype)) && (!FilterCanSell || itemJsonData.CanSale == 0) && (!FilterNum || item.itemNum > 0))
			{
				UIInventoryGridData gridData = new UIInventoryGridData();
				gridData.Item = item;
				gridData.Index = num;
				gridData.ItemCount = item.itemNum;
				gridData.IconShowInitAction = delegate(UIIconShow s)
				{
					s.Inventory = this;
					s.isPlayer = IsPlayer;
					s.NPCID = NPCID;
					s.CanDrag = CanDrag;
					s.ShowPrice = UIIconShow.ShowPriceType.PlayerSell;
					s.SetItem(gridData.Item);
					s.Count = gridData.ItemCount;
				};
				InventoryLayout.GridDataList.Add(gridData);
				num++;
			}
		}
	}

	public void Sort()
	{
		if (IsPlayer)
		{
			PlayerEx.Player.SortItem();
			RefreshUI();
		}
	}
}
