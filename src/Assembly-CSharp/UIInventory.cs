using System;
using System.Collections.Generic;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200040E RID: 1038
public class UIInventory : MonoBehaviour
{
	// Token: 0x1700028E RID: 654
	// (get) Token: 0x06001C04 RID: 7172 RVA: 0x000176E1 File Offset: 0x000158E1
	// (set) Token: 0x06001C05 RID: 7173 RVA: 0x000176E9 File Offset: 0x000158E9
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

	// Token: 0x06001C06 RID: 7174 RVA: 0x000F9808 File Offset: 0x000F7A08
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

	// Token: 0x06001C07 RID: 7175 RVA: 0x00017707 File Offset: 0x00015907
	public void HideFilter()
	{
		this.LianZiAnim.Play("Hide");
		this.RightFilterAnim.Play("Hide");
		base.Invoke("CloseFilter", 0.5f);
	}

	// Token: 0x06001C08 RID: 7176 RVA: 0x00017739 File Offset: 0x00015939
	public void CloseFilter()
	{
		this.FilterGO.SetActive(false);
		this.LianZiAnim.gameObject.SetActive(false);
	}

	// Token: 0x06001C09 RID: 7177 RVA: 0x000F9904 File Offset: 0x000F7B04
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

	// Token: 0x1700028F RID: 655
	// (get) Token: 0x06001C0A RID: 7178 RVA: 0x00017758 File Offset: 0x00015958
	// (set) Token: 0x06001C0B RID: 7179 RVA: 0x000F9C10 File Offset: 0x000F7E10
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

	// Token: 0x06001C0C RID: 7180 RVA: 0x00017760 File Offset: 0x00015960
	public void OnSVSliderBeginDrag(PointerEventData eventData)
	{
		this.sliderBeginDragY = this.SVSliderDragAera.RT.anchoredPosition.y;
		this.sliderBeginDragMouseY = eventData.position.y;
		this.isSVSliderDragging = true;
	}

	// Token: 0x06001C0D RID: 7181 RVA: 0x000F9C68 File Offset: 0x000F7E68
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

	// Token: 0x06001C0E RID: 7182 RVA: 0x00017795 File Offset: 0x00015995
	public void OnSVSliderEndDrag(PointerEventData eventData)
	{
		this.isSVSliderDragging = false;
	}

	// Token: 0x06001C0F RID: 7183 RVA: 0x0001779E File Offset: 0x0001599E
	public void OnSVBarValueChanged(float value)
	{
		if (!this.isSVSliderDragging && this.SVBar.gameObject.activeInHierarchy)
		{
			this.SliderPer = value;
		}
	}

	// Token: 0x17000290 RID: 656
	// (get) Token: 0x06001C10 RID: 7184 RVA: 0x000177C1 File Offset: 0x000159C1
	[HideInInspector]
	public Avatar Player
	{
		get
		{
			return PlayerEx.Player;
		}
	}

	// Token: 0x06001C11 RID: 7185 RVA: 0x000F9D1C File Offset: 0x000F7F1C
	private void Awake()
	{
		this.SVSliderDragAera.OnBeginDragAction = new UnityAction<PointerEventData>(this.OnSVSliderBeginDrag);
		this.SVSliderDragAera.OnDragAction = new UnityAction<PointerEventData>(this.OnSVSliderDrag);
		this.SVSliderDragAera.OnEndDragAction = new UnityAction<PointerEventData>(this.OnSVSliderEndDrag);
		this.SVSliderDragAera.RT.anchoredPosition = this.SVSliderButtomPos.anchoredPosition;
		this.SVBar.onValueChanged.RemoveAllListeners();
		this.SVBar.onValueChanged.AddListener(new UnityAction<float>(this.OnSVBarValueChanged));
	}

	// Token: 0x06001C12 RID: 7186 RVA: 0x000177C8 File Offset: 0x000159C8
	private void Start()
	{
		this.InventoryLayout.RT.anchoredPosition = new Vector2(this.InventoryLayout.RT.anchoredPosition.x, 0f);
	}

	// Token: 0x06001C13 RID: 7187 RVA: 0x000177F9 File Offset: 0x000159F9
	private void Update()
	{
		if (Mathf.Abs(this.InventoryLayout.RTY - this.lastY) > 0.1f)
		{
			this.lastY = this.InventoryLayout.RTY;
			this.InventoryLayout.RefreshUI();
		}
	}

	// Token: 0x06001C14 RID: 7188 RVA: 0x000F9DB8 File Offset: 0x000F7FB8
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

	// Token: 0x06001C15 RID: 7189 RVA: 0x000F9F50 File Offset: 0x000F8150
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
					if (this.FilterQingJiao || item_INFO2.itemId < 100000)
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

	// Token: 0x06001C16 RID: 7190 RVA: 0x000FA248 File Offset: 0x000F8448
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
					if (this.FilterQingJiao || item_INFO2.itemId < 100000)
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

	// Token: 0x06001C17 RID: 7191 RVA: 0x000FA4D4 File Offset: 0x000F86D4
	public void LoadNPCItems()
	{
		int num = 0;
		foreach (item item in this.NPC.Inventory)
		{
			_ItemJsonData itemJsonData = _ItemJsonData.DataDict[item.itemID];
			if (this.FilterQingJiao || item.itemID < 100000)
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

	// Token: 0x06001C18 RID: 7192 RVA: 0x00017835 File Offset: 0x00015A35
	public void Sort()
	{
		if (this.IsPlayer)
		{
			PlayerEx.Player.SortItem();
			this.RefreshUI();
		}
	}

	// Token: 0x040017E7 RID: 6119
	public PlayerSetRandomFace Face;

	// Token: 0x040017E8 RID: 6120
	public Text TitleText;

	// Token: 0x040017E9 RID: 6121
	public Text MoneyText;

	// Token: 0x040017EA RID: 6122
	public RectTransform SlotSVRT;

	// Token: 0x040017EB RID: 6123
	public GameObject FilterGO;

	// Token: 0x040017EC RID: 6124
	public Animator LianZiAnim;

	// Token: 0x040017ED RID: 6125
	public Animator RightFilterAnim;

	// Token: 0x040017EE RID: 6126
	private bool filterActive;

	// Token: 0x040017EF RID: 6127
	[HideInInspector]
	public int UITmpValue;

	// Token: 0x040017F0 RID: 6128
	public UIInvFilterBtn FilterA;

	// Token: 0x040017F1 RID: 6129
	public UIInvFilterBtn FilterB;

	// Token: 0x040017F2 RID: 6130
	public List<UIInvFilterBtn> FilterBtns = new List<UIInvFilterBtn>();

	// Token: 0x040017F3 RID: 6131
	public UIIconShow.ShowPriceType ShowPriceType;

	// Token: 0x040017F4 RID: 6132
	public DragAera SVSliderDragAera;

	// Token: 0x040017F5 RID: 6133
	public RectTransform SVSliderButtomPos;

	// Token: 0x040017F6 RID: 6134
	public Scrollbar SVBar;

	// Token: 0x040017F7 RID: 6135
	public Button SortBtn;

	// Token: 0x040017F8 RID: 6136
	public UIInventoryLayout InventoryLayout;

	// Token: 0x040017F9 RID: 6137
	public bool CanDrag;

	// Token: 0x040017FA RID: 6138
	public int NPCID;

	// Token: 0x040017FB RID: 6139
	private bool isSVSliderDragging;

	// Token: 0x040017FC RID: 6140
	private float sliderBeginDragY;

	// Token: 0x040017FD RID: 6141
	private float sliderBeginDragMouseY;

	// Token: 0x040017FE RID: 6142
	private float sliderPer;

	// Token: 0x040017FF RID: 6143
	[HideInInspector]
	public bool IsGuDing;

	// Token: 0x04001800 RID: 6144
	public InventoryType InventoryType;

	// Token: 0x04001801 RID: 6145
	public SpecialInventoryMode SpecialInventoryMode;

	// Token: 0x04001802 RID: 6146
	[HideInInspector]
	public Inventory2 oldInventory;

	// Token: 0x04001803 RID: 6147
	public bool IsPlayer;

	// Token: 0x04001804 RID: 6148
	public int ID;

	// Token: 0x04001805 RID: 6149
	public bool FilterCanSell = true;

	// Token: 0x04001806 RID: 6150
	public bool FilterNum = true;

	// Token: 0x04001807 RID: 6151
	public bool FilterQingJiao = true;

	// Token: 0x04001808 RID: 6152
	public int FilterQuality;

	// Token: 0x04001809 RID: 6153
	public int FilterType;

	// Token: 0x0400180A RID: 6154
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

	// Token: 0x0400180B RID: 6155
	public UIInventory dragTargetInventory;

	// Token: 0x0400180C RID: 6156
	public UIIconShow dragTargetSlot;

	// Token: 0x0400180D RID: 6157
	public UINPCData NPC;

	// Token: 0x0400180E RID: 6158
	private float lastY;
}
