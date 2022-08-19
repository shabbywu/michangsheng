using System;
using System.Collections.Generic;
using Bag;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020002C5 RID: 709
public class UIIconShow : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	// Token: 0x1700023F RID: 575
	// (get) Token: 0x060018D9 RID: 6361 RVA: 0x000B2747 File Offset: 0x000B0947
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

	// Token: 0x060018DA RID: 6362 RVA: 0x000B276C File Offset: 0x000B096C
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

	// Token: 0x060018DB RID: 6363 RVA: 0x000B27E0 File Offset: 0x000B09E0
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

	// Token: 0x17000240 RID: 576
	// (get) Token: 0x060018DC RID: 6364 RVA: 0x000B2872 File Offset: 0x000B0A72
	// (set) Token: 0x060018DD RID: 6365 RVA: 0x000B287A File Offset: 0x000B0A7A
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

	// Token: 0x17000241 RID: 577
	// (get) Token: 0x060018DE RID: 6366 RVA: 0x000B2883 File Offset: 0x000B0A83
	// (set) Token: 0x060018DF RID: 6367 RVA: 0x000B288C File Offset: 0x000B0A8C
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

	// Token: 0x17000242 RID: 578
	// (get) Token: 0x060018E0 RID: 6368 RVA: 0x000B297F File Offset: 0x000B0B7F
	// (set) Token: 0x060018E1 RID: 6369 RVA: 0x000B2988 File Offset: 0x000B0B88
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

	// Token: 0x17000243 RID: 579
	// (get) Token: 0x060018E2 RID: 6370 RVA: 0x000B2AC7 File Offset: 0x000B0CC7
	// (set) Token: 0x060018E3 RID: 6371 RVA: 0x000B2AD0 File Offset: 0x000B0CD0
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

	// Token: 0x060018E4 RID: 6372 RVA: 0x000B2B26 File Offset: 0x000B0D26
	public void SetCustomCountText(string text, Color color)
	{
		this.CountText.gameObject.SetActive(true);
		this.CountText.text = text;
		this.CountText.color = color;
	}

	// Token: 0x060018E5 RID: 6373 RVA: 0x000B2B51 File Offset: 0x000B0D51
	public void SetNull()
	{
		this.NowType = UIIconShow.UIIconType.None;
	}

	// Token: 0x060018E6 RID: 6374 RVA: 0x000B2B5C File Offset: 0x000B0D5C
	public void SetItem(int id)
	{
		item item = new item(id);
		this.SetItem(item);
	}

	// Token: 0x060018E7 RID: 6375 RVA: 0x000B2B78 File Offset: 0x000B0D78
	public void SetItem(int id, JSONObject seid)
	{
		this.SetItem(new item(id)
		{
			Seid = seid
		});
	}

	// Token: 0x060018E8 RID: 6376 RVA: 0x000B2B9C File Offset: 0x000B0D9C
	public void SetItem(ITEM_INFO info)
	{
		this.SetItem(new item(info.itemId)
		{
			Seid = info.Seid,
			UUID = info.uuid
		});
	}

	// Token: 0x060018E9 RID: 6377 RVA: 0x000B2BD4 File Offset: 0x000B0DD4
	public void SetItem(JSONObject json)
	{
		this.SetItem(new item(json["ItemID"].I)
		{
			Seid = json
		});
	}

	// Token: 0x060018EA RID: 6378 RVA: 0x000B2C08 File Offset: 0x000B0E08
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

	// Token: 0x060018EB RID: 6379 RVA: 0x000B2CBC File Offset: 0x000B0EBC
	public void SetSkill(int id, bool showStudy = false, int level = 1)
	{
		this.isShowStudy = showStudy;
		GUIPackage.Skill skill = new GUIPackage.Skill(id, level, 5);
		this.SetSkill(skill);
	}

	// Token: 0x060018EC RID: 6380 RVA: 0x000B2CE0 File Offset: 0x000B0EE0
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

	// Token: 0x060018ED RID: 6381 RVA: 0x000B2DBC File Offset: 0x000B0FBC
	public void SetStaticSkill(int id, bool showStudy = false)
	{
		this.isShowStudy = showStudy;
		GUIPackage.Skill skill = SkillStaticDatebase.instence.dicSkills[id];
		this.SetStaticSkill(skill);
		base.gameObject.name = skill.skill_Name;
	}

	// Token: 0x060018EE RID: 6382 RVA: 0x000B2DFC File Offset: 0x000B0FFC
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

	// Token: 0x060018EF RID: 6383 RVA: 0x000B2EEB File Offset: 0x000B10EB
	public void CalcFontSize()
	{
		if (this.NameText.text.Length == 6)
		{
			this.NameText.fontSize = 20;
			return;
		}
		this.NameText.fontSize = 24;
	}

	// Token: 0x060018F0 RID: 6384 RVA: 0x000B2F1B File Offset: 0x000B111B
	public void SetBuChuan()
	{
		this.NowJiaoBiao = UIIconShow.JiaoBiaoType.MiChuan;
	}

	// Token: 0x060018F1 RID: 6385 RVA: 0x000B2F24 File Offset: 0x000B1124
	public void SetCount(int count)
	{
		this.CountText.text = count.ToString();
		this.CountText.gameObject.SetActive(true);
	}

	// Token: 0x060018F2 RID: 6386 RVA: 0x000B2F49 File Offset: 0x000B1149
	public int GetCount()
	{
		return int.Parse(this.CountText.text);
	}

	// Token: 0x060018F3 RID: 6387 RVA: 0x000B2F5C File Offset: 0x000B115C
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

	// Token: 0x060018F4 RID: 6388 RVA: 0x000B3123 File Offset: 0x000B1323
	public void CloseTooltip()
	{
		if (ToolTipsMag.Inst != null)
		{
			ToolTipsMag.Inst.Close();
		}
	}

	// Token: 0x060018F5 RID: 6389 RVA: 0x000B313C File Offset: 0x000B133C
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (this != UIIconShow.NowDraggingObj)
		{
			UIIconShow.NowMouseBottomObj = this;
		}
		this.OpenTooltip();
		this.isHover = true;
	}

	// Token: 0x060018F6 RID: 6390 RVA: 0x000B315E File Offset: 0x000B135E
	public void OnPointerExit(PointerEventData eventData)
	{
		this.CloseTooltip();
		this.isHover = false;
	}

	// Token: 0x060018F7 RID: 6391 RVA: 0x000B316D File Offset: 0x000B136D
	public void OnPointerClick(PointerEventData eventData)
	{
		if (this.OnClick != null)
		{
			this.OnClick.Invoke(eventData);
		}
	}

	// Token: 0x060018F8 RID: 6392 RVA: 0x000B3184 File Offset: 0x000B1384
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

	// Token: 0x060018F9 RID: 6393 RVA: 0x000B31D8 File Offset: 0x000B13D8
	public void OnDrag(PointerEventData eventData)
	{
		if (UIIconShow.NowDraggingObj != null)
		{
			float num = 1920f / (float)Screen.width * eventData.position.x;
			float num2 = 1080f / (float)Screen.height * eventData.position.y;
			UIIconShow.NowDraggingObj.RT.anchoredPosition = new Vector2(num, num2);
		}
	}

	// Token: 0x060018FA RID: 6394 RVA: 0x000B323C File Offset: 0x000B143C
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

	// Token: 0x060018FB RID: 6395 RVA: 0x000B32C0 File Offset: 0x000B14C0
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

	// Token: 0x060018FC RID: 6396 RVA: 0x000B3370 File Offset: 0x000B1570
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

	// Token: 0x060018FD RID: 6397 RVA: 0x000B34C0 File Offset: 0x000B16C0
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

	// Token: 0x060018FE RID: 6398 RVA: 0x000B3548 File Offset: 0x000B1748
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

	// Token: 0x040013F6 RID: 5110
	private static GameObject prefab;

	// Token: 0x040013F7 RID: 5111
	public UIInventory Inventory;

	// Token: 0x040013F8 RID: 5112
	public Image Quality;

	// Token: 0x040013F9 RID: 5113
	public Image Icon;

	// Token: 0x040013FA RID: 5114
	public Image QualityUp;

	// Token: 0x040013FB RID: 5115
	public Image JiaoBiao;

	// Token: 0x040013FC RID: 5116
	public Image BottomBG;

	// Token: 0x040013FD RID: 5117
	public Text NameText;

	// Token: 0x040013FE RID: 5118
	public Text CountText;

	// Token: 0x040013FF RID: 5119
	public GameObject JiaoBiaoMask;

	// Token: 0x04001400 RID: 5120
	public GameObject NameMask;

	// Token: 0x04001401 RID: 5121
	public GameObject BGMask;

	// Token: 0x04001402 RID: 5122
	public GameObject SelectedImage;

	// Token: 0x04001403 RID: 5123
	public UnityAction<PointerEventData> OnClick;

	// Token: 0x04001404 RID: 5124
	[HideInInspector]
	public bool IsLingWu;

	// Token: 0x04001405 RID: 5125
	public RectTransform RT;

	// Token: 0x04001406 RID: 5126
	public RectTransform SlotRT;

	// Token: 0x04001407 RID: 5127
	public bool isShowStudy;

	// Token: 0x04001408 RID: 5128
	public bool IsCopyDrag;

	// Token: 0x04001409 RID: 5129
	public bool IsDragSelcetNum;

	// Token: 0x0400140A RID: 5130
	public UIIconShow.ShowPriceType ShowPrice;

	// Token: 0x0400140B RID: 5131
	[HideInInspector]
	public int NPCID;

	// Token: 0x0400140C RID: 5132
	[HideInInspector]
	public bool isPlayer;

	// Token: 0x0400140D RID: 5133
	public int DragCountLimit;

	// Token: 0x0400140E RID: 5134
	public DragAera DragAera;

	// Token: 0x0400140F RID: 5135
	public bool IsLingTian;

	// Token: 0x04001410 RID: 5136
	[HideInInspector]
	public UIInventoryGridData InventoryGridData;

	// Token: 0x04001411 RID: 5137
	[HideInInspector]
	public bool IsDragging;

	// Token: 0x04001412 RID: 5138
	private bool isDraggingObj;

	// Token: 0x04001413 RID: 5139
	public bool CanDrag;

	// Token: 0x04001414 RID: 5140
	public UIIconShow.UIIconType CanAcceptDragType;

	// Token: 0x04001415 RID: 5141
	public static UIIconShow NowDraggingObj;

	// Token: 0x04001416 RID: 5142
	public static UIIconShow NowMouseBottomObj;

	// Token: 0x04001417 RID: 5143
	public static UIIconShow DragStarter;

	// Token: 0x04001418 RID: 5144
	private static List<Color> _ItemQualityColor = new List<Color>
	{
		new Color(0.84705883f, 0.84705883f, 0.7921569f),
		new Color(0.8f, 0.8862745f, 0.5058824f),
		new Color(0.6745098f, 1f, 0.99607843f),
		new Color(0.94509804f, 0.7176471f, 0.972549f),
		new Color(1f, 0.6745098f, 0.37254903f),
		new Color(1f, 0.69803923f, 0.54509807f)
	};

	// Token: 0x04001419 RID: 5145
	private UIIconShow.UIIconType nowType;

	// Token: 0x0400141A RID: 5146
	private static Sprite _YiWuSprite;

	// Token: 0x0400141B RID: 5147
	private static Sprite _NaiSprite;

	// Token: 0x0400141C RID: 5148
	private static Sprite _MiChuanSprite;

	// Token: 0x0400141D RID: 5149
	private static Sprite _MoSprite;

	// Token: 0x0400141E RID: 5150
	private UIIconShow.JiaoBiaoType nowJiaoBiao;

	// Token: 0x0400141F RID: 5151
	private int count;

	// Token: 0x04001420 RID: 5152
	private bool isHover;

	// Token: 0x04001421 RID: 5153
	[HideInInspector]
	public item tmpItem;

	// Token: 0x04001422 RID: 5154
	[HideInInspector]
	public GUIPackage.Skill tmpSkill;

	// Token: 0x02001317 RID: 4887
	public enum UIIconType
	{
		// Token: 0x04006770 RID: 26480
		None,
		// Token: 0x04006771 RID: 26481
		Item,
		// Token: 0x04006772 RID: 26482
		Skill,
		// Token: 0x04006773 RID: 26483
		StaticSkill
	}

	// Token: 0x02001318 RID: 4888
	public enum JiaoBiaoType
	{
		// Token: 0x04006775 RID: 26485
		None,
		// Token: 0x04006776 RID: 26486
		YiWu,
		// Token: 0x04006777 RID: 26487
		Nai,
		// Token: 0x04006778 RID: 26488
		MiChuan,
		// Token: 0x04006779 RID: 26489
		Mo
	}

	// Token: 0x02001319 RID: 4889
	public enum ShowPriceType
	{
		// Token: 0x0400677B RID: 26491
		None,
		// Token: 0x0400677C RID: 26492
		Normal,
		// Token: 0x0400677D RID: 26493
		PlayerSell,
		// Token: 0x0400677E RID: 26494
		NPCSell
	}
}
