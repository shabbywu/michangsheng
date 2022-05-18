using System;
using System.Collections.Generic;
using Bag;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000406 RID: 1030
public class UIIconShow : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	// Token: 0x17000289 RID: 649
	// (get) Token: 0x06001BD2 RID: 7122 RVA: 0x00017544 File Offset: 0x00015744
	public static GameObject Prefab
	{
		get
		{
			if (UIIconShow.prefab == null)
			{
				UIIconShow.prefab = Resources.Load<GameObject>("NewUI/Inventory/UIIconShow");
			}
			return UIIconShow.prefab;
		}
	}

	// Token: 0x06001BD3 RID: 7123 RVA: 0x000F8898 File Offset: 0x000F6A98
	private void Start()
	{
		if (this.DragAera != null)
		{
			this.DragAera.OnBeginDragAction = new UnityAction<PointerEventData>(this.OnBeginDrag);
			this.DragAera.OnDragAction = new UnityAction<PointerEventData>(this.OnDrag);
			this.DragAera.OnEndDragAction = new UnityAction<PointerEventData>(this.OnEndDrag);
			this.DragAera.enabled = this.CanDrag;
		}
	}

	// Token: 0x06001BD4 RID: 7124 RVA: 0x000F890C File Offset: 0x000F6B0C
	private void Update()
	{
		if (this.isHover && this.OnClick == null && Input.GetMouseButtonDown(1))
		{
			this.MoveToTarget();
			this.CloseTooltip();
		}
		if (this.IsLingTian)
		{
			if (UILingTianPanel.Inst.IsShouGe)
			{
				if (this.NowType == UIIconShow.UIIconType.None)
				{
					this.SelectedImage.SetActive(false);
					return;
				}
				this.SelectedImage.SetActive(this.isHover);
				return;
			}
			else
			{
				if (this.NowType == UIIconShow.UIIconType.None)
				{
					this.SelectedImage.SetActive(this.isHover);
					return;
				}
				this.SelectedImage.SetActive(false);
			}
		}
	}

	// Token: 0x1700028A RID: 650
	// (get) Token: 0x06001BD5 RID: 7125 RVA: 0x00017567 File Offset: 0x00015767
	// (set) Token: 0x06001BD6 RID: 7126 RVA: 0x0001756F File Offset: 0x0001576F
	[HideInInspector]
	public bool IsDraggingObj
	{
		get
		{
			return this.isDraggingObj;
		}
		set
		{
			this.isDraggingObj = value;
		}
	}

	// Token: 0x1700028B RID: 651
	// (get) Token: 0x06001BD7 RID: 7127 RVA: 0x00017578 File Offset: 0x00015778
	// (set) Token: 0x06001BD8 RID: 7128 RVA: 0x000F89A0 File Offset: 0x000F6BA0
	public UIIconShow.UIIconType NowType
	{
		get
		{
			return this.nowType;
		}
		set
		{
			this.nowType = value;
			this.Count = 0;
			switch (this.nowType)
			{
			case UIIconShow.UIIconType.None:
				this.Quality.gameObject.SetActive(false);
				this.JiaoBiaoMask.gameObject.SetActive(false);
				this.BGMask.gameObject.SetActive(false);
				this.SlotRT.gameObject.SetActive(false);
				return;
			case UIIconShow.UIIconType.Item:
				this.CanAcceptDragType = value;
				this.Quality.gameObject.SetActive(true);
				this.BGMask.gameObject.SetActive(true);
				this.SlotRT.gameObject.SetActive(true);
				return;
			case UIIconShow.UIIconType.Skill:
			case UIIconShow.UIIconType.StaticSkill:
				this.CanAcceptDragType = value;
				this.Quality.gameObject.SetActive(false);
				this.BGMask.gameObject.SetActive(true);
				this.SlotRT.gameObject.SetActive(true);
				return;
			default:
				return;
			}
		}
	}

	// Token: 0x1700028C RID: 652
	// (get) Token: 0x06001BD9 RID: 7129 RVA: 0x00017580 File Offset: 0x00015780
	// (set) Token: 0x06001BDA RID: 7130 RVA: 0x000F8A94 File Offset: 0x000F6C94
	public UIIconShow.JiaoBiaoType NowJiaoBiao
	{
		get
		{
			return this.nowJiaoBiao;
		}
		set
		{
			this.nowJiaoBiao = value;
			switch (this.nowJiaoBiao)
			{
			case UIIconShow.JiaoBiaoType.None:
				this.JiaoBiaoMask.gameObject.SetActive(false);
				return;
			case UIIconShow.JiaoBiaoType.YiWu:
				this.JiaoBiaoMask.gameObject.SetActive(true);
				if (UIIconShow._YiWuSprite == null)
				{
					UIIconShow._YiWuSprite = Resources.Load<Sprite>("NewUI/Inventory/Icon/MCS_icon_wu");
				}
				this.JiaoBiao.sprite = UIIconShow._YiWuSprite;
				return;
			case UIIconShow.JiaoBiaoType.Nai:
				this.JiaoBiaoMask.gameObject.SetActive(true);
				if (UIIconShow._NaiSprite == null)
				{
					UIIconShow._NaiSprite = Resources.Load<Sprite>("NewUI/Inventory/Icon/MCS_icon_nai");
				}
				this.JiaoBiao.sprite = UIIconShow._NaiSprite;
				return;
			case UIIconShow.JiaoBiaoType.MiChuan:
				this.JiaoBiaoMask.gameObject.SetActive(true);
				if (UIIconShow._MiChuanSprite == null)
				{
					UIIconShow._MiChuanSprite = Resources.Load<Sprite>("NewUI/Inventory/Icon/MCS_icon_mi");
				}
				this.JiaoBiao.sprite = UIIconShow._MiChuanSprite;
				return;
			case UIIconShow.JiaoBiaoType.Mo:
				this.JiaoBiaoMask.gameObject.SetActive(true);
				if (UIIconShow._MoSprite == null)
				{
					UIIconShow._MoSprite = Resources.Load<Sprite>("NewUI/Inventory/Icon/MCS_icon_mo");
				}
				this.JiaoBiao.sprite = UIIconShow._MoSprite;
				return;
			default:
				return;
			}
		}
	}

	// Token: 0x1700028D RID: 653
	// (get) Token: 0x06001BDB RID: 7131 RVA: 0x00017588 File Offset: 0x00015788
	// (set) Token: 0x06001BDC RID: 7132 RVA: 0x000F8BD4 File Offset: 0x000F6DD4
	public int Count
	{
		get
		{
			return this.count;
		}
		set
		{
			this.count = value;
			if (this.count <= 1)
			{
				this.CountText.gameObject.SetActive(false);
				return;
			}
			this.CountText.gameObject.SetActive(true);
			this.CountText.text = this.count.ToString();
		}
	}

	// Token: 0x06001BDD RID: 7133 RVA: 0x00017590 File Offset: 0x00015790
	public void SetCustomCountText(string text, Color color)
	{
		this.CountText.gameObject.SetActive(true);
		this.CountText.text = text;
		this.CountText.color = color;
	}

	// Token: 0x06001BDE RID: 7134 RVA: 0x000175BB File Offset: 0x000157BB
	public void SetNull()
	{
		this.NowType = UIIconShow.UIIconType.None;
	}

	// Token: 0x06001BDF RID: 7135 RVA: 0x000F8C2C File Offset: 0x000F6E2C
	public void SetItem(int id)
	{
		item item = new item(id);
		this.SetItem(item);
	}

	// Token: 0x06001BE0 RID: 7136 RVA: 0x000F8C48 File Offset: 0x000F6E48
	public void SetItem(int id, JSONObject seid)
	{
		this.SetItem(new item(id)
		{
			Seid = seid
		});
	}

	// Token: 0x06001BE1 RID: 7137 RVA: 0x000F8C6C File Offset: 0x000F6E6C
	public void SetItem(ITEM_INFO info)
	{
		this.SetItem(new item(info.itemId)
		{
			Seid = info.Seid,
			UUID = info.uuid
		});
	}

	// Token: 0x06001BE2 RID: 7138 RVA: 0x000F8CA4 File Offset: 0x000F6EA4
	public void SetItem(JSONObject json)
	{
		this.SetItem(new item(json["ItemID"].I)
		{
			Seid = json
		});
	}

	// Token: 0x06001BE3 RID: 7139 RVA: 0x000F8CD8 File Offset: 0x000F6ED8
	public void SetItem(item item)
	{
		this.NowType = UIIconShow.UIIconType.Item;
		this.NowJiaoBiao = UIIconShow.JiaoBiaoType.None;
		this.tmpItem = item;
		this.Quality.sprite = item.newitemPingZhiSprite;
		this.QualityUp.sprite = item.newitemPingZhiUP;
		this.Icon.sprite = item.itemIconSprite;
		this.NameText.text = item.itemName;
		this.NameText.color = UIIconShow._ItemQualityColor[item.ColorIndex];
		if (this.isShowStudy && PlayerEx.IsLingWuBook(item.itemID))
		{
			this.NowJiaoBiao = UIIconShow.JiaoBiaoType.YiWu;
		}
		this.CalcFontSize();
		base.gameObject.name = item.itemName;
	}

	// Token: 0x06001BE4 RID: 7140 RVA: 0x000F8D8C File Offset: 0x000F6F8C
	public void SetSkill(int id, bool showStudy = false, int level = 1)
	{
		this.isShowStudy = showStudy;
		GUIPackage.Skill skill = new GUIPackage.Skill(id, level, 5);
		this.SetSkill(skill);
	}

	// Token: 0x06001BE5 RID: 7141 RVA: 0x000F8DB0 File Offset: 0x000F6FB0
	public void SetSkill(GUIPackage.Skill skill)
	{
		this.NowType = UIIconShow.UIIconType.Skill;
		this.NowJiaoBiao = UIIconShow.JiaoBiaoType.None;
		this.tmpSkill = skill;
		this.QualityUp.sprite = skill.newSkillPingZhiSprite;
		this.Icon.sprite = skill.skillIconSprite;
		this.NameText.text = skill.skill_Name.RemoveNumber();
		this.NameText.color = UIIconShow._ItemQualityColor[skill.ColorIndex];
		if (this.isShowStudy && Tools.instance.getPlayer().hasSkillList.Find((SkillItem aa) => aa.itemId == skill.SkillID) != null)
		{
			this.NowJiaoBiao = UIIconShow.JiaoBiaoType.YiWu;
			this.IsLingWu = true;
		}
		this.CalcFontSize();
	}

	// Token: 0x06001BE6 RID: 7142 RVA: 0x000F8E8C File Offset: 0x000F708C
	public void SetStaticSkill(int id, bool showStudy = false)
	{
		this.isShowStudy = showStudy;
		GUIPackage.Skill skill = SkillStaticDatebase.instence.dicSkills[id];
		this.SetStaticSkill(skill);
		base.gameObject.name = skill.skill_Name;
	}

	// Token: 0x06001BE7 RID: 7143 RVA: 0x000F8ECC File Offset: 0x000F70CC
	public void SetStaticSkill(GUIPackage.Skill skill)
	{
		this.NowType = UIIconShow.UIIconType.StaticSkill;
		this.NowJiaoBiao = UIIconShow.JiaoBiaoType.None;
		this.tmpSkill = skill;
		this.QualityUp.sprite = skill.newSkillPingZhiSprite;
		this.Icon.sprite = skill.skillIconSprite;
		this.NameText.text = skill.skill_Name.RemoveNumber();
		this.NameText.color = UIIconShow._ItemQualityColor[skill.ColorIndex];
		if (this.isShowStudy && Tools.instance.getPlayer().hasStaticSkillList.Find((SkillItem aa) => aa.itemId == skill.SkillID) != null)
		{
			this.NowJiaoBiao = UIIconShow.JiaoBiaoType.YiWu;
			this.IsLingWu = true;
		}
		this.CalcFontSize();
		base.gameObject.name = skill.skill_Name;
	}

	// Token: 0x06001BE8 RID: 7144 RVA: 0x000175C4 File Offset: 0x000157C4
	public void CalcFontSize()
	{
		if (this.NameText.text.Length == 6)
		{
			this.NameText.fontSize = 20;
			return;
		}
		this.NameText.fontSize = 24;
	}

	// Token: 0x06001BE9 RID: 7145 RVA: 0x000175F4 File Offset: 0x000157F4
	public void SetBuChuan()
	{
		this.NowJiaoBiao = UIIconShow.JiaoBiaoType.MiChuan;
	}

	// Token: 0x06001BEA RID: 7146 RVA: 0x000175FD File Offset: 0x000157FD
	public void SetCount(int count)
	{
		this.CountText.text = count.ToString();
		this.CountText.gameObject.SetActive(true);
	}

	// Token: 0x06001BEB RID: 7147 RVA: 0x00017622 File Offset: 0x00015822
	public int GetCount()
	{
		return int.Parse(this.CountText.text);
	}

	// Token: 0x06001BEC RID: 7148 RVA: 0x000F8FBC File Offset: 0x000F71BC
	public void OpenTooltip()
	{
		if (ToolTipsMag.Inst == null)
		{
			ResManager.inst.LoadPrefab("ToolTips").Inst(NewUICanvas.Inst.transform);
		}
		switch (this.NowType)
		{
		case UIIconShow.UIIconType.Item:
			switch (this.ShowPrice)
			{
			case UIIconShow.ShowPriceType.None:
			case UIIconShow.ShowPriceType.Normal:
				ToolTipsMag.Inst.Show(BaseItem.Create(this.tmpItem.itemID, this.tmpItem.itemNum, this.tmpItem.UUID, this.tmpItem.Seid));
				break;
			case UIIconShow.ShowPriceType.PlayerSell:
				ToolTipsMag.Inst.Show(BaseItem.Create(this.tmpItem.itemID, this.tmpItem.itemNum, this.tmpItem.UUID, this.tmpItem.Seid), this.tmpItem.GetJiaoYiPrice(this.NPCID, true, false), true);
				break;
			case UIIconShow.ShowPriceType.NPCSell:
				ToolTipsMag.Inst.Show(BaseItem.Create(this.tmpItem.itemID, this.tmpItem.itemNum, this.tmpItem.UUID, this.tmpItem.Seid), this.tmpItem.GetJiaoYiPrice(this.NPCID, false, false), false);
				break;
			}
			break;
		case UIIconShow.UIIconType.Skill:
		{
			ActiveSkill activeSkill = new ActiveSkill();
			activeSkill.SetSkill(this.tmpSkill.SkillID, this.tmpSkill.Skill_Lv);
			ToolTipsMag.Inst.Show(activeSkill);
			break;
		}
		case UIIconShow.UIIconType.StaticSkill:
		{
			PassiveSkill passiveSkill = new PassiveSkill();
			passiveSkill.SetSkill(this.tmpSkill.SkillID, this.tmpSkill.Skill_Lv);
			ToolTipsMag.Inst.Show(passiveSkill);
			break;
		}
		}
		UToolTip.BindObj = base.gameObject;
	}

	// Token: 0x06001BED RID: 7149 RVA: 0x00017634 File Offset: 0x00015834
	public void CloseTooltip()
	{
		if (ToolTipsMag.Inst != null)
		{
			ToolTipsMag.Inst.Close();
		}
	}

	// Token: 0x06001BEE RID: 7150 RVA: 0x0001764D File Offset: 0x0001584D
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (this != UIIconShow.NowDraggingObj)
		{
			UIIconShow.NowMouseBottomObj = this;
		}
		this.OpenTooltip();
		this.isHover = true;
	}

	// Token: 0x06001BEF RID: 7151 RVA: 0x0001766F File Offset: 0x0001586F
	public void OnPointerExit(PointerEventData eventData)
	{
		this.CloseTooltip();
		this.isHover = false;
	}

	// Token: 0x06001BF0 RID: 7152 RVA: 0x0001767E File Offset: 0x0001587E
	public void OnPointerClick(PointerEventData eventData)
	{
		if (this.OnClick != null)
		{
			this.OnClick.Invoke(eventData);
		}
	}

	// Token: 0x06001BF1 RID: 7153 RVA: 0x000F9184 File Offset: 0x000F7384
	public void OnBeginDrag(PointerEventData eventData)
	{
		if (UILingTianPanel.Inst != null && UILingTianPanel.Inst.IsShouGe)
		{
			return;
		}
		if (this.CanDrag && this.NowType != UIIconShow.UIIconType.None)
		{
			UIIconShow.DragStarter = this;
			this.CreateDragObj();
			this.SlotRT.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001BF2 RID: 7154 RVA: 0x000F91D8 File Offset: 0x000F73D8
	public void OnDrag(PointerEventData eventData)
	{
		if (UIIconShow.NowDraggingObj != null)
		{
			float num = 1920f / (float)Screen.width * eventData.position.x;
			float num2 = 1080f / (float)Screen.height * eventData.position.y;
			UIIconShow.NowDraggingObj.RT.anchoredPosition = new Vector2(num, num2);
		}
	}

	// Token: 0x06001BF3 RID: 7155 RVA: 0x000F923C File Offset: 0x000F743C
	public void OnEndDrag(PointerEventData eventData)
	{
		if (UIIconShow.NowDraggingObj != null)
		{
			bool flag = false;
			if (UIIconShow.NowMouseBottomObj != null && UIIconShow.NowMouseBottomObj != UIIconShow.DragStarter && UIIconShow.NowMouseBottomObj.CanAcceptDragType == UIIconShow.NowDraggingObj.NowType)
			{
				flag = true;
				UIIconShow.NowMouseBottomObj.OnDragToThis(UIIconShow.NowDraggingObj);
			}
			if (!flag)
			{
				this.SlotRT.gameObject.SetActive(true);
				Object.Destroy(UIIconShow.NowDraggingObj.gameObject);
			}
		}
	}

	// Token: 0x06001BF4 RID: 7156 RVA: 0x000F92C0 File Offset: 0x000F74C0
	public void CreateDragObj()
	{
		UIIconShow.NowDraggingObj = Object.Instantiate<GameObject>(base.gameObject, NewUICanvas.Inst.Canvas.transform).GetComponent<UIIconShow>();
		UIIconShow.NowDraggingObj.AcceptObj(this);
		UIIconShow.NowDraggingObj.name = "DraggingObj";
		UIIconShow.NowDraggingObj.IsDraggingObj = true;
		UIIconShow.NowDraggingObj.RT.anchorMin = Vector2.zero;
		UIIconShow.NowDraggingObj.RT.anchorMax = Vector2.zero;
		UIIconShow.NowDraggingObj.RT.localScale *= 0.821f;
		UIIconShow.NowDraggingObj.BottomBG.raycastTarget = false;
	}

	// Token: 0x06001BF5 RID: 7157 RVA: 0x000F9370 File Offset: 0x000F7570
	public void OnDragToThis(UIIconShow draggingObj)
	{
		if (!this.IsLingTian)
		{
			if (draggingObj.NowType == UIIconShow.UIIconType.Item)
			{
				UnityAction<int> unityAction = delegate(int n)
				{
					if (this.NowType == UIIconShow.UIIconType.None)
					{
						if (this.IsCopyDrag)
						{
							this.AcceptObj(draggingObj);
							UIIconShow.DragStarter.SlotRT.gameObject.SetActive(true);
							this.Count = n;
						}
						else if (UIIconShow.DragStarter.Count - n > 0)
						{
							this.AcceptObj(draggingObj);
							this.Count = n;
							UIIconShow.DragStarter.Count -= n;
						}
						else
						{
							UIIconShow.DragStarter.SetNull();
							this.AcceptObj(draggingObj);
						}
					}
					else if (this.IsCopyDrag)
					{
						this.AcceptObj(draggingObj);
						UIIconShow.DragStarter.SlotRT.gameObject.SetActive(true);
						this.Count = n;
					}
					else
					{
						UIIconShow.DragStarter.AcceptObj(this);
						this.AcceptObj(draggingObj);
					}
					Object.Destroy(draggingObj.gameObject);
				};
				UnityAction cancel = delegate()
				{
					UIIconShow.DragStarter.SlotRT.gameObject.SetActive(true);
				};
				draggingObj.gameObject.SetActive(false);
				if (draggingObj.Count > 1)
				{
					USelectNum.Show("放入 " + draggingObj.NameText.text + " x{num}", 1, draggingObj.Count, unityAction, cancel);
					return;
				}
				unityAction.Invoke(1);
			}
			return;
		}
		if (this.NowType == UIIconShow.UIIconType.None)
		{
			UILingTianCell component = base.GetComponent<UILingTianCell>();
			int itemID = draggingObj.tmpItem.itemID;
			DongFuManager.ZhongZhi(DongFuManager.NowDongFuID, component.Slot, itemID);
			PlayerEx.Player.removeItem(itemID, 1);
			Object.Destroy(draggingObj.gameObject);
			UIDongFu.Inst.InitData();
			UILingTianPanel.Inst.RefreshUI();
			return;
		}
		Object.Destroy(draggingObj.gameObject);
		USelectBox.Show("当前灵田已经种有草药，不能继续种植", null, null);
		UILingTianPanel.Inst.RefreshUI();
	}

	// Token: 0x06001BF6 RID: 7158 RVA: 0x000F94C0 File Offset: 0x000F76C0
	public void MoveToTarget()
	{
		if (this.Inventory != null && !(this.Inventory.dragTargetInventory != null) && this.Inventory.dragTargetSlot != null)
		{
			UnityAction<int> unityAction = delegate(int n)
			{
				if (this.Inventory.dragTargetSlot.IsCopyDrag)
				{
					this.Inventory.dragTargetSlot.NowType = this.NowType;
					this.Inventory.dragTargetSlot.AcceptObj(this);
					this.Inventory.dragTargetSlot.Count = n;
				}
			};
			if (this.Count > 1)
			{
				USelectNum.Show("放入 " + this.NameText.text + " x{num}", 1, this.Count, unityAction, null);
				return;
			}
			unityAction.Invoke(1);
		}
	}

	// Token: 0x06001BF7 RID: 7159 RVA: 0x000F9548 File Offset: 0x000F7748
	public void AcceptObj(UIIconShow obj)
	{
		this.SlotRT.gameObject.SetActive(true);
		this.BottomBG.raycastTarget = true;
		switch (obj.NowType)
		{
		case UIIconShow.UIIconType.None:
			this.SetNull();
			return;
		case UIIconShow.UIIconType.Item:
			this.SetItem(obj.tmpItem);
			this.Count = obj.Count;
			return;
		case UIIconShow.UIIconType.Skill:
			this.SetSkill(obj.tmpSkill);
			return;
		case UIIconShow.UIIconType.StaticSkill:
			this.SetStaticSkill(obj.tmpSkill);
			return;
		default:
			return;
		}
	}

	// Token: 0x040017A4 RID: 6052
	private static GameObject prefab;

	// Token: 0x040017A5 RID: 6053
	public UIInventory Inventory;

	// Token: 0x040017A6 RID: 6054
	public Image Quality;

	// Token: 0x040017A7 RID: 6055
	public Image Icon;

	// Token: 0x040017A8 RID: 6056
	public Image QualityUp;

	// Token: 0x040017A9 RID: 6057
	public Image JiaoBiao;

	// Token: 0x040017AA RID: 6058
	public Image BottomBG;

	// Token: 0x040017AB RID: 6059
	public Text NameText;

	// Token: 0x040017AC RID: 6060
	public Text CountText;

	// Token: 0x040017AD RID: 6061
	public GameObject JiaoBiaoMask;

	// Token: 0x040017AE RID: 6062
	public GameObject NameMask;

	// Token: 0x040017AF RID: 6063
	public GameObject BGMask;

	// Token: 0x040017B0 RID: 6064
	public GameObject SelectedImage;

	// Token: 0x040017B1 RID: 6065
	public UnityAction<PointerEventData> OnClick;

	// Token: 0x040017B2 RID: 6066
	[HideInInspector]
	public bool IsLingWu;

	// Token: 0x040017B3 RID: 6067
	public RectTransform RT;

	// Token: 0x040017B4 RID: 6068
	public RectTransform SlotRT;

	// Token: 0x040017B5 RID: 6069
	public bool isShowStudy;

	// Token: 0x040017B6 RID: 6070
	public bool IsCopyDrag;

	// Token: 0x040017B7 RID: 6071
	public bool IsDragSelcetNum;

	// Token: 0x040017B8 RID: 6072
	public UIIconShow.ShowPriceType ShowPrice;

	// Token: 0x040017B9 RID: 6073
	[HideInInspector]
	public int NPCID;

	// Token: 0x040017BA RID: 6074
	[HideInInspector]
	public bool isPlayer;

	// Token: 0x040017BB RID: 6075
	public int DragCountLimit;

	// Token: 0x040017BC RID: 6076
	public DragAera DragAera;

	// Token: 0x040017BD RID: 6077
	public bool IsLingTian;

	// Token: 0x040017BE RID: 6078
	[HideInInspector]
	public UIInventoryGridData InventoryGridData;

	// Token: 0x040017BF RID: 6079
	[HideInInspector]
	public bool IsDragging;

	// Token: 0x040017C0 RID: 6080
	private bool isDraggingObj;

	// Token: 0x040017C1 RID: 6081
	public bool CanDrag;

	// Token: 0x040017C2 RID: 6082
	public UIIconShow.UIIconType CanAcceptDragType;

	// Token: 0x040017C3 RID: 6083
	public static UIIconShow NowDraggingObj;

	// Token: 0x040017C4 RID: 6084
	public static UIIconShow NowMouseBottomObj;

	// Token: 0x040017C5 RID: 6085
	public static UIIconShow DragStarter;

	// Token: 0x040017C6 RID: 6086
	private static List<Color> _ItemQualityColor = new List<Color>
	{
		new Color(0.84705883f, 0.84705883f, 0.7921569f),
		new Color(0.8f, 0.8862745f, 0.5058824f),
		new Color(0.6745098f, 1f, 0.99607843f),
		new Color(0.94509804f, 0.7176471f, 0.972549f),
		new Color(1f, 0.6745098f, 0.37254903f),
		new Color(1f, 0.69803923f, 0.54509807f)
	};

	// Token: 0x040017C7 RID: 6087
	private UIIconShow.UIIconType nowType;

	// Token: 0x040017C8 RID: 6088
	private static Sprite _YiWuSprite;

	// Token: 0x040017C9 RID: 6089
	private static Sprite _NaiSprite;

	// Token: 0x040017CA RID: 6090
	private static Sprite _MiChuanSprite;

	// Token: 0x040017CB RID: 6091
	private static Sprite _MoSprite;

	// Token: 0x040017CC RID: 6092
	private UIIconShow.JiaoBiaoType nowJiaoBiao;

	// Token: 0x040017CD RID: 6093
	private int count;

	// Token: 0x040017CE RID: 6094
	private bool isHover;

	// Token: 0x040017CF RID: 6095
	[HideInInspector]
	public item tmpItem;

	// Token: 0x040017D0 RID: 6096
	[HideInInspector]
	public GUIPackage.Skill tmpSkill;

	// Token: 0x02000407 RID: 1031
	public enum UIIconType
	{
		// Token: 0x040017D2 RID: 6098
		None,
		// Token: 0x040017D3 RID: 6099
		Item,
		// Token: 0x040017D4 RID: 6100
		Skill,
		// Token: 0x040017D5 RID: 6101
		StaticSkill
	}

	// Token: 0x02000408 RID: 1032
	public enum JiaoBiaoType
	{
		// Token: 0x040017D7 RID: 6103
		None,
		// Token: 0x040017D8 RID: 6104
		YiWu,
		// Token: 0x040017D9 RID: 6105
		Nai,
		// Token: 0x040017DA RID: 6106
		MiChuan,
		// Token: 0x040017DB RID: 6107
		Mo
	}

	// Token: 0x02000409 RID: 1033
	public enum ShowPriceType
	{
		// Token: 0x040017DD RID: 6109
		None,
		// Token: 0x040017DE RID: 6110
		Normal,
		// Token: 0x040017DF RID: 6111
		PlayerSell,
		// Token: 0x040017E0 RID: 6112
		NPCSell
	}
}
