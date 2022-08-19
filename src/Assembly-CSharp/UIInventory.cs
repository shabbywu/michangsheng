using System;
using System.Collections.Generic;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020002C6 RID: 710
public class UIInventory : MonoBehaviour
{
	// Token: 0x17000244 RID: 580
	// (get) Token: 0x06001902 RID: 6402 RVA: 0x000B36D7 File Offset: 0x000B18D7
	// (set) Token: 0x06001903 RID: 6403 RVA: 0x000B36DF File Offset: 0x000B18DF
	[HideInInspector]
	public bool FilterActive
	{
		get
		{
			return this.filterActive;
		}
		set
		{
			this.filterActive = value;
			if (this.filterActive)
			{
				this.ShowFilter();
				return;
			}
			this.HideFilter();
		}
	}

	// Token: 0x06001904 RID: 6404 RVA: 0x000B3700 File Offset: 0x000B1900
	public void ShowFilter()
	{
		this.FilterGO.SetActive(true);
		this.LianZiAnim.gameObject.SetActive(true);
		if (this.FilterA.State == UIInvFilterBtn.BtnState.Active)
		{
			foreach (UIInvFilterBtn uiinvFilterBtn in this.FilterBtns)
			{
				if (uiinvFilterBtn.BtnData == this.FilterQuality)
				{
					uiinvFilterBtn.State = UIInvFilterBtn.BtnState.Active;
				}
				else
				{
					uiinvFilterBtn.State = UIInvFilterBtn.BtnState.Normal;
				}
			}
		}
		if (this.FilterB.State == UIInvFilterBtn.BtnState.Active)
		{
			foreach (UIInvFilterBtn uiinvFilterBtn2 in this.FilterBtns)
			{
				if (uiinvFilterBtn2.BtnData == this.FilterType)
				{
					uiinvFilterBtn2.State = UIInvFilterBtn.BtnState.Active;
				}
				else
				{
					uiinvFilterBtn2.State = UIInvFilterBtn.BtnState.Normal;
				}
			}
		}
	}

	// Token: 0x06001905 RID: 6405 RVA: 0x000B37FC File Offset: 0x000B19FC
	public void HideFilter()
	{
		this.LianZiAnim.Play("Hide");
		this.RightFilterAnim.Play("Hide");
		base.Invoke("CloseFilter", 0.5f);
	}

	// Token: 0x06001906 RID: 6406 RVA: 0x000B382E File Offset: 0x000B1A2E
	public void CloseFilter()
	{
		this.FilterGO.SetActive(false);
		this.LianZiAnim.gameObject.SetActive(false);
	}

	// Token: 0x06001907 RID: 6407 RVA: 0x000B3850 File Offset: 0x000B1A50
	public void OnFilterBtnClick(UIInvFilterBtn btn)
	{
		if (this.UITmpValue == -1)
		{
			if (this.FilterActive)
			{
				this.FilterA.State = UIInvFilterBtn.BtnState.Normal;
				this.FilterB.State = UIInvFilterBtn.BtnState.Normal;
				this.FilterActive = false;
				return;
			}
			this.FilterBtns[0].ShowText.text = "全部";
			this.FilterBtns[1].ShowText.text = "一品";
			this.FilterBtns[2].ShowText.text = "二品";
			this.FilterBtns[3].ShowText.text = "三品";
			this.FilterBtns[4].ShowText.text = "四品";
			this.FilterBtns[5].ShowText.text = "五品";
			this.FilterBtns[6].ShowText.text = "六品";
			btn.State = UIInvFilterBtn.BtnState.Active;
			this.FilterActive = true;
			return;
		}
		else
		{
			if (this.UITmpValue != -2)
			{
				if (this.FilterA.State == UIInvFilterBtn.BtnState.Active)
				{
					this.FilterQuality = btn.BtnData;
					if (this.FilterQuality == 0)
					{
						this.FilterA.ShowText.text = "品阶";
					}
					else
					{
						this.FilterA.ShowText.text = btn.ShowText.text;
					}
				}
				else if (this.FilterB.State == UIInvFilterBtn.BtnState.Active)
				{
					this.FilterType = btn.BtnData;
					if (this.FilterType == 0)
					{
						this.FilterB.ShowText.text = "类型";
					}
					else
					{
						this.FilterB.ShowText.text = btn.ShowText.text;
					}
				}
				this.FilterB.State = UIInvFilterBtn.BtnState.Normal;
				this.FilterA.State = UIInvFilterBtn.BtnState.Normal;
				this.FilterActive = false;
				this.InventoryLayout.RT.anchoredPosition = new Vector2(this.InventoryLayout.RT.anchoredPosition.x, 0f);
				this.RefreshUI();
				return;
			}
			if (this.FilterActive)
			{
				this.FilterA.State = UIInvFilterBtn.BtnState.Normal;
				this.FilterB.State = UIInvFilterBtn.BtnState.Normal;
				this.FilterActive = false;
				return;
			}
			this.FilterBtns[0].ShowText.text = "全部";
			this.FilterBtns[1].ShowText.text = "装备";
			this.FilterBtns[2].ShowText.text = "丹药";
			this.FilterBtns[3].ShowText.text = "秘籍";
			this.FilterBtns[4].ShowText.text = "材料";
			this.FilterBtns[5].ShowText.text = "草药";
			this.FilterBtns[6].ShowText.text = "其他";
			btn.State = UIInvFilterBtn.BtnState.Active;
			this.FilterActive = true;
			return;
		}
	}

	// Token: 0x17000245 RID: 581
	// (get) Token: 0x06001908 RID: 6408 RVA: 0x000B3B5C File Offset: 0x000B1D5C
	// (set) Token: 0x06001909 RID: 6409 RVA: 0x000B3B64 File Offset: 0x000B1D64
	private float SliderPer
	{
		get
		{
			return this.sliderPer;
		}
		set
		{
			this.sliderPer = value;
			this.SVSliderDragAera.RT.anchoredPosition = new Vector2(this.SVSliderDragAera.RT.anchoredPosition.x, this.sliderPer * this.SVSliderButtomPos.anchoredPosition.y);
		}
	}

	// Token: 0x0600190A RID: 6410 RVA: 0x000B3BB9 File Offset: 0x000B1DB9
	public void OnSVSliderBeginDrag(PointerEventData eventData)
	{
		this.sliderBeginDragY = this.SVSliderDragAera.RT.anchoredPosition.y;
		this.sliderBeginDragMouseY = eventData.position.y;
		this.isSVSliderDragging = true;
	}

	// Token: 0x0600190B RID: 6411 RVA: 0x000B3BF0 File Offset: 0x000B1DF0
	public void OnSVSliderDrag(PointerEventData eventData)
	{
		this.isSVSliderDragging = true;
		float num = 1080f / (float)Screen.height * (eventData.position.y - this.sliderBeginDragMouseY);
		float num2 = this.sliderBeginDragY + num / 0.821f;
		num2 = Mathf.Clamp(num2, 0f, this.SVSliderButtomPos.anchoredPosition.y);
		this.SVSliderDragAera.RT.anchoredPosition = new Vector2(this.SVSliderDragAera.RT.anchoredPosition.x, num2);
		this.sliderPer = num2 / this.SVSliderButtomPos.anchoredPosition.y;
		this.SVBar.value = this.sliderPer;
	}

	// Token: 0x0600190C RID: 6412 RVA: 0x000B3CA3 File Offset: 0x000B1EA3
	public void OnSVSliderEndDrag(PointerEventData eventData)
	{
		this.isSVSliderDragging = false;
	}

	// Token: 0x0600190D RID: 6413 RVA: 0x000B3CAC File Offset: 0x000B1EAC
	public void OnSVBarValueChanged(float value)
	{
		if (!this.isSVSliderDragging && this.SVBar.gameObject.activeInHierarchy)
		{
			this.SliderPer = value;
		}
	}

	// Token: 0x17000246 RID: 582
	// (get) Token: 0x0600190E RID: 6414 RVA: 0x000B3CCF File Offset: 0x000B1ECF
	[HideInInspector]
	public Avatar Player
	{
		get
		{
			return PlayerEx.Player;
		}
	}

	// Token: 0x0600190F RID: 6415 RVA: 0x000B3CD8 File Offset: 0x000B1ED8
	private void Awake()
	{
		this.SVSliderDragAera.OnBeginDragAction = new UnityAction<PointerEventData>(this.OnSVSliderBeginDrag);
		this.SVSliderDragAera.OnDragAction = new UnityAction<PointerEventData>(this.OnSVSliderDrag);
		this.SVSliderDragAera.OnEndDragAction = new UnityAction<PointerEventData>(this.OnSVSliderEndDrag);
		this.SVSliderDragAera.RT.anchoredPosition = this.SVSliderButtomPos.anchoredPosition;
		this.SVBar.onValueChanged.RemoveAllListeners();
		this.SVBar.onValueChanged.AddListener(new UnityAction<float>(this.OnSVBarValueChanged));
	}

	// Token: 0x06001910 RID: 6416 RVA: 0x000B3D71 File Offset: 0x000B1F71
	private void Start()
	{
		this.InventoryLayout.RT.anchoredPosition = new Vector2(this.InventoryLayout.RT.anchoredPosition.x, 0f);
	}

	// Token: 0x06001911 RID: 6417 RVA: 0x000B3DA2 File Offset: 0x000B1FA2
	private void Update()
	{
		if (Mathf.Abs(this.InventoryLayout.RTY - this.lastY) > 0.1f)
		{
			this.lastY = this.InventoryLayout.RTY;
			this.InventoryLayout.RefreshUI();
		}
	}

	// Token: 0x06001912 RID: 6418 RVA: 0x000B3DE0 File Offset: 0x000B1FE0
	public void RefreshUI()
	{
		if (this.IsPlayer)
		{
			this.ID = 1;
		}
		this.Face.gameObject.SetActive(true);
		this.TitleText.text = Tools.getMonstarTitle(this.ID);
		if (this.IsPlayer)
		{
			this.Face.faceID = this.ID;
			this.Face.setFace();
			this.MoneyText.text = this.Player.money.ToString();
			if (this.SortBtn != null)
			{
				this.SortBtn.gameObject.SetActive(true);
			}
		}
		else
		{
			this.Face.SetNPCFace(this.ID);
			this.MoneyText.text = this.NPC.BackpackJson["money"].I.ToString();
			if (this.SortBtn != null)
			{
				this.SortBtn.gameObject.SetActive(false);
			}
		}
		this.InventoryLayout.Init();
		if (this.InventoryType == InventoryType.Item)
		{
			if (this.IsPlayer)
			{
				if (this.SpecialInventoryMode == SpecialInventoryMode.LingTian)
				{
					this.LoadPlayerLingTianItems();
				}
				else
				{
					this.LoadPlayerItems();
				}
			}
			else
			{
				this.LoadNPCItems();
			}
		}
		this.InventoryLayout.CalcGridPos();
		this.InventoryLayout.RefreshUI();
		int count = this.InventoryLayout.GridDataList.Count;
		this.SlotSVRT.sizeDelta = new Vector2(this.SlotSVRT.sizeDelta.x, (float)((count - 1) / 3 + 1) * 144.5f + 19f);
	}

	// Token: 0x06001913 RID: 6419 RVA: 0x000B3F78 File Offset: 0x000B2178
	public void LoadPlayerItems()
	{
		List<string> list = new List<string>();
		foreach (ITEM_INFO item_INFO in this.Player.equipItemList.values)
		{
			list.Add(item_INFO.uuid);
		}
		int num = 0;
		foreach (ITEM_INFO item_INFO2 in this.Player.itemList.values)
		{
			if (!list.Contains(item_INFO2.uuid))
			{
				if (!_ItemJsonData.DataDict.ContainsKey(item_INFO2.itemId))
				{
					Debug.LogError("找不到物品" + item_INFO2.itemId);
				}
				else
				{
					_ItemJsonData itemJsonData = _ItemJsonData.DataDict[item_INFO2.itemId];
					if (this.FilterQingJiao || item_INFO2.itemId < jsonData.QingJiaoItemIDSegment)
					{
						int num2 = itemJsonData.quality;
						if (item_INFO2.Seid != null && item_INFO2.Seid.HasField("quality"))
						{
							num2 = item_INFO2.Seid["quality"].I;
						}
						bool flag = this.FilterQuality == 0 || num2 == this.FilterQuality;
						if (itemJsonData.type == 3 || itemJsonData.type == 4)
						{
							flag = (this.FilterQuality == 0 || num2 * 2 == this.FilterQuality);
						}
						if (itemJsonData.type == 0 || itemJsonData.type == 1 || itemJsonData.type == 2)
						{
							flag = (this.FilterQuality == 0 || num2 + 1 == this.FilterQuality);
						}
						if (flag && (this.FilterType == 0 || UIInventory.FilterTypeDict[this.FilterType].Contains(itemJsonData.type)) && (!this.FilterCanSell || itemJsonData.CanSale == 0) && (!this.FilterNum || item_INFO2.itemCount > 0U))
						{
							item item = new item(item_INFO2.itemId);
							item.Seid = item_INFO2.Seid;
							item.UUID = item_INFO2.uuid;
							UIInventoryGridData gridData = new UIInventoryGridData();
							gridData.Item = item;
							gridData.Index = num;
							gridData.ItemCount = (int)item_INFO2.itemCount;
							gridData.IconShowInitAction = delegate(UIIconShow s)
							{
								s.Inventory = this;
								s.isPlayer = this.IsPlayer;
								s.NPCID = this.NPCID;
								s.CanDrag = this.CanDrag;
								s.ShowPrice = UIIconShow.ShowPriceType.PlayerSell;
								s.isShowStudy = true;
								s.SetItem(gridData.Item);
								s.Count = gridData.ItemCount;
							};
							this.InventoryLayout.GridDataList.Add(gridData);
							num++;
						}
					}
				}
			}
		}
	}

	// Token: 0x06001914 RID: 6420 RVA: 0x000B4270 File Offset: 0x000B2470
	public void LoadPlayerLingTianItems()
	{
		List<string> list = new List<string>();
		foreach (ITEM_INFO item_INFO in this.Player.equipItemList.values)
		{
			list.Add(item_INFO.uuid);
		}
		int num = 0;
		foreach (ITEM_INFO item_INFO2 in this.Player.itemList.values)
		{
			if (!DFBuKeZhongZhi.DataDict.ContainsKey(item_INFO2.itemId) && !list.Contains(item_INFO2.uuid))
			{
				if (!_ItemJsonData.DataDict.ContainsKey(item_INFO2.itemId))
				{
					Debug.LogError("找不到物品" + item_INFO2.itemId);
				}
				else
				{
					_ItemJsonData itemJsonData = _ItemJsonData.DataDict[item_INFO2.itemId];
					if (this.FilterQingJiao || item_INFO2.itemId < jsonData.QingJiaoItemIDSegment)
					{
						int num2 = itemJsonData.quality;
						if (item_INFO2.Seid != null && item_INFO2.Seid.HasField("quality"))
						{
							num2 = item_INFO2.Seid["quality"].I;
						}
						if (itemJsonData.type == 6 && (this.FilterQuality == 0 || num2 == this.FilterQuality) && (!this.FilterCanSell || itemJsonData.CanSale == 0) && (!this.FilterNum || item_INFO2.itemCount > 0U))
						{
							item item = new item(item_INFO2.itemId);
							item.Seid = item_INFO2.Seid;
							item.UUID = item_INFO2.uuid;
							UIInventoryGridData gridData = new UIInventoryGridData();
							gridData.Item = item;
							gridData.Index = num;
							gridData.ItemCount = (int)item_INFO2.itemCount;
							gridData.IconShowInitAction = delegate(UIIconShow s)
							{
								s.Inventory = this;
								s.isPlayer = this.IsPlayer;
								s.NPCID = this.NPCID;
								s.CanDrag = this.CanDrag;
								s.ShowPrice = UIIconShow.ShowPriceType.PlayerSell;
								s.isShowStudy = true;
								s.SetItem(gridData.Item);
								s.Count = gridData.ItemCount;
							};
							this.InventoryLayout.GridDataList.Add(gridData);
							num++;
						}
					}
				}
			}
		}
	}

	// Token: 0x06001915 RID: 6421 RVA: 0x000B44FC File Offset: 0x000B26FC
	public void LoadNPCItems()
	{
		int num = 0;
		foreach (item item in this.NPC.Inventory)
		{
			_ItemJsonData itemJsonData = _ItemJsonData.DataDict[item.itemID];
			if (this.FilterQingJiao || item.itemID < jsonData.QingJiaoItemIDSegment)
			{
				int quality = item.quality;
				int type = itemJsonData.type;
				bool flag = this.FilterQuality == 0 || quality == this.FilterQuality;
				if (type == 3 || type == 4)
				{
					flag = (this.FilterQuality == 0 || quality * 2 == this.FilterQuality);
				}
				if (itemJsonData.type == 0 || itemJsonData.type == 1 || itemJsonData.type == 2)
				{
					flag = (this.FilterQuality == 0 || quality + 1 == this.FilterQuality);
				}
				if (flag && (this.FilterType == 0 || UIInventory.FilterTypeDict[this.FilterType].Contains(item.itemtype)) && (!this.FilterCanSell || itemJsonData.CanSale == 0) && (!this.FilterNum || item.itemNum > 0))
				{
					UIInventoryGridData gridData = new UIInventoryGridData();
					gridData.Item = item;
					gridData.Index = num;
					gridData.ItemCount = item.itemNum;
					gridData.IconShowInitAction = delegate(UIIconShow s)
					{
						s.Inventory = this;
						s.isPlayer = this.IsPlayer;
						s.NPCID = this.NPCID;
						s.CanDrag = this.CanDrag;
						s.ShowPrice = UIIconShow.ShowPriceType.PlayerSell;
						s.SetItem(gridData.Item);
						s.Count = gridData.ItemCount;
					};
					this.InventoryLayout.GridDataList.Add(gridData);
					num++;
				}
			}
		}
	}

	// Token: 0x06001916 RID: 6422 RVA: 0x000B46D8 File Offset: 0x000B28D8
	public void Sort()
	{
		if (this.IsPlayer)
		{
			PlayerEx.Player.SortItem();
			this.RefreshUI();
		}
	}

	// Token: 0x04001423 RID: 5155
	public PlayerSetRandomFace Face;

	// Token: 0x04001424 RID: 5156
	public Text TitleText;

	// Token: 0x04001425 RID: 5157
	public Text MoneyText;

	// Token: 0x04001426 RID: 5158
	public RectTransform SlotSVRT;

	// Token: 0x04001427 RID: 5159
	public GameObject FilterGO;

	// Token: 0x04001428 RID: 5160
	public Animator LianZiAnim;

	// Token: 0x04001429 RID: 5161
	public Animator RightFilterAnim;

	// Token: 0x0400142A RID: 5162
	private bool filterActive;

	// Token: 0x0400142B RID: 5163
	[HideInInspector]
	public int UITmpValue;

	// Token: 0x0400142C RID: 5164
	public UIInvFilterBtn FilterA;

	// Token: 0x0400142D RID: 5165
	public UIInvFilterBtn FilterB;

	// Token: 0x0400142E RID: 5166
	public List<UIInvFilterBtn> FilterBtns = new List<UIInvFilterBtn>();

	// Token: 0x0400142F RID: 5167
	public UIIconShow.ShowPriceType ShowPriceType;

	// Token: 0x04001430 RID: 5168
	public DragAera SVSliderDragAera;

	// Token: 0x04001431 RID: 5169
	public RectTransform SVSliderButtomPos;

	// Token: 0x04001432 RID: 5170
	public Scrollbar SVBar;

	// Token: 0x04001433 RID: 5171
	public Button SortBtn;

	// Token: 0x04001434 RID: 5172
	public UIInventoryLayout InventoryLayout;

	// Token: 0x04001435 RID: 5173
	public bool CanDrag;

	// Token: 0x04001436 RID: 5174
	public int NPCID;

	// Token: 0x04001437 RID: 5175
	private bool isSVSliderDragging;

	// Token: 0x04001438 RID: 5176
	private float sliderBeginDragY;

	// Token: 0x04001439 RID: 5177
	private float sliderBeginDragMouseY;

	// Token: 0x0400143A RID: 5178
	private float sliderPer;

	// Token: 0x0400143B RID: 5179
	[HideInInspector]
	public bool IsGuDing;

	// Token: 0x0400143C RID: 5180
	public InventoryType InventoryType;

	// Token: 0x0400143D RID: 5181
	public SpecialInventoryMode SpecialInventoryMode;

	// Token: 0x0400143E RID: 5182
	[HideInInspector]
	public Inventory2 oldInventory;

	// Token: 0x0400143F RID: 5183
	public bool IsPlayer;

	// Token: 0x04001440 RID: 5184
	public int ID;

	// Token: 0x04001441 RID: 5185
	public bool FilterCanSell = true;

	// Token: 0x04001442 RID: 5186
	public bool FilterNum = true;

	// Token: 0x04001443 RID: 5187
	public bool FilterQingJiao = true;

	// Token: 0x04001444 RID: 5188
	public int FilterQuality;

	// Token: 0x04001445 RID: 5189
	public int FilterType;

	// Token: 0x04001446 RID: 5190
	private static Dictionary<int, List<int>> FilterTypeDict = new Dictionary<int, List<int>>
	{
		{
			0,
			new List<int>()
		},
		{
			1,
			new List<int>
			{
				0,
				1,
				2
			}
		},
		{
			2,
			new List<int>
			{
				5,
				15
			}
		},
		{
			3,
			new List<int>
			{
				3,
				4,
				10,
				12,
				13
			}
		},
		{
			4,
			new List<int>
			{
				8
			}
		},
		{
			5,
			new List<int>
			{
				6
			}
		},
		{
			6,
			new List<int>
			{
				7,
				9,
				14,
				16
			}
		}
	};

	// Token: 0x04001447 RID: 5191
	public UIInventory dragTargetInventory;

	// Token: 0x04001448 RID: 5192
	public UIIconShow dragTargetSlot;

	// Token: 0x04001449 RID: 5193
	public UINPCData NPC;

	// Token: 0x0400144A RID: 5194
	private float lastY;
}
