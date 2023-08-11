using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Bag;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIIconShow : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	public enum UIIconType
	{
		None,
		Item,
		Skill,
		StaticSkill
	}

	public enum JiaoBiaoType
	{
		None,
		YiWu,
		Nai,
		MiChuan,
		Mo
	}

	public enum ShowPriceType
	{
		None,
		Normal,
		PlayerSell,
		NPCSell
	}

	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static UnityAction _003C_003E9__85_1;

		internal void _003COnDragToThis_003Eb__85_1()
		{
			((Component)DragStarter.SlotRT).gameObject.SetActive(true);
		}
	}

	private static GameObject prefab;

	public UIInventory Inventory;

	public Image Quality;

	public Image Icon;

	public Image QualityUp;

	public Image JiaoBiao;

	public Image BottomBG;

	public Text NameText;

	public Text CountText;

	public GameObject JiaoBiaoMask;

	public GameObject NameMask;

	public GameObject BGMask;

	public GameObject SelectedImage;

	public UnityAction<PointerEventData> OnClick;

	[HideInInspector]
	public bool IsLingWu;

	public RectTransform RT;

	public RectTransform SlotRT;

	public bool isShowStudy;

	public bool IsCopyDrag;

	public bool IsDragSelcetNum;

	public ShowPriceType ShowPrice;

	[HideInInspector]
	public int NPCID;

	[HideInInspector]
	public bool isPlayer;

	public int DragCountLimit;

	public DragAera DragAera;

	public bool IsLingTian;

	[HideInInspector]
	public UIInventoryGridData InventoryGridData;

	[HideInInspector]
	public bool IsDragging;

	private bool isDraggingObj;

	public bool CanDrag;

	public UIIconType CanAcceptDragType;

	public static UIIconShow NowDraggingObj;

	public static UIIconShow NowMouseBottomObj;

	public static UIIconShow DragStarter;

	private static List<Color> _ItemQualityColor = new List<Color>
	{
		new Color(72f / 85f, 72f / 85f, 0.7921569f),
		new Color(0.8f, 0.8862745f, 43f / 85f),
		new Color(0.6745098f, 1f, 0.99607843f),
		new Color(0.94509804f, 61f / 85f, 0.972549f),
		new Color(1f, 0.6745098f, 19f / 51f),
		new Color(1f, 0.69803923f, 0.54509807f)
	};

	private UIIconType nowType;

	private static Sprite _YiWuSprite;

	private static Sprite _NaiSprite;

	private static Sprite _MiChuanSprite;

	private static Sprite _MoSprite;

	private JiaoBiaoType nowJiaoBiao;

	private int count;

	private bool isHover;

	[HideInInspector]
	public item tmpItem;

	[HideInInspector]
	public GUIPackage.Skill tmpSkill;

	public static GameObject Prefab
	{
		get
		{
			if ((Object)(object)prefab == (Object)null)
			{
				prefab = Resources.Load<GameObject>("NewUI/Inventory/UIIconShow");
			}
			return prefab;
		}
	}

	[HideInInspector]
	public bool IsDraggingObj
	{
		get
		{
			return isDraggingObj;
		}
		set
		{
			isDraggingObj = value;
		}
	}

	public UIIconType NowType
	{
		get
		{
			return nowType;
		}
		set
		{
			nowType = value;
			Count = 0;
			switch (nowType)
			{
			case UIIconType.None:
				((Component)Quality).gameObject.SetActive(false);
				JiaoBiaoMask.gameObject.SetActive(false);
				BGMask.gameObject.SetActive(false);
				((Component)SlotRT).gameObject.SetActive(false);
				break;
			case UIIconType.Item:
				CanAcceptDragType = value;
				((Component)Quality).gameObject.SetActive(true);
				BGMask.gameObject.SetActive(true);
				((Component)SlotRT).gameObject.SetActive(true);
				break;
			case UIIconType.Skill:
			case UIIconType.StaticSkill:
				CanAcceptDragType = value;
				((Component)Quality).gameObject.SetActive(false);
				BGMask.gameObject.SetActive(true);
				((Component)SlotRT).gameObject.SetActive(true);
				break;
			}
		}
	}

	public JiaoBiaoType NowJiaoBiao
	{
		get
		{
			return nowJiaoBiao;
		}
		set
		{
			nowJiaoBiao = value;
			switch (nowJiaoBiao)
			{
			case JiaoBiaoType.None:
				JiaoBiaoMask.gameObject.SetActive(false);
				break;
			case JiaoBiaoType.YiWu:
				JiaoBiaoMask.gameObject.SetActive(true);
				if ((Object)(object)_YiWuSprite == (Object)null)
				{
					_YiWuSprite = Resources.Load<Sprite>("NewUI/Inventory/Icon/MCS_icon_wu");
				}
				JiaoBiao.sprite = _YiWuSprite;
				break;
			case JiaoBiaoType.Nai:
				JiaoBiaoMask.gameObject.SetActive(true);
				if ((Object)(object)_NaiSprite == (Object)null)
				{
					_NaiSprite = Resources.Load<Sprite>("NewUI/Inventory/Icon/MCS_icon_nai");
				}
				JiaoBiao.sprite = _NaiSprite;
				break;
			case JiaoBiaoType.MiChuan:
				JiaoBiaoMask.gameObject.SetActive(true);
				if ((Object)(object)_MiChuanSprite == (Object)null)
				{
					_MiChuanSprite = Resources.Load<Sprite>("NewUI/Inventory/Icon/MCS_icon_mi");
				}
				JiaoBiao.sprite = _MiChuanSprite;
				break;
			case JiaoBiaoType.Mo:
				JiaoBiaoMask.gameObject.SetActive(true);
				if ((Object)(object)_MoSprite == (Object)null)
				{
					_MoSprite = Resources.Load<Sprite>("NewUI/Inventory/Icon/MCS_icon_mo");
				}
				JiaoBiao.sprite = _MoSprite;
				break;
			}
		}
	}

	public int Count
	{
		get
		{
			return count;
		}
		set
		{
			count = value;
			if (count <= 1)
			{
				((Component)CountText).gameObject.SetActive(false);
				return;
			}
			((Component)CountText).gameObject.SetActive(true);
			CountText.text = count.ToString();
		}
	}

	private void Start()
	{
		if ((Object)(object)DragAera != (Object)null)
		{
			DragAera.OnBeginDragAction = OnBeginDrag;
			DragAera.OnDragAction = OnDrag;
			DragAera.OnEndDragAction = OnEndDrag;
			((Behaviour)DragAera).enabled = CanDrag;
		}
	}

	private void Update()
	{
		if (isHover && OnClick == null && Input.GetMouseButtonDown(1))
		{
			MoveToTarget();
			CloseTooltip();
		}
		if (!IsLingTian)
		{
			return;
		}
		if (UILingTianPanel.Inst.IsShouGe)
		{
			if (NowType == UIIconType.None)
			{
				SelectedImage.SetActive(false);
			}
			else
			{
				SelectedImage.SetActive(isHover);
			}
		}
		else if (NowType == UIIconType.None)
		{
			SelectedImage.SetActive(isHover);
		}
		else
		{
			SelectedImage.SetActive(false);
		}
	}

	public void SetCustomCountText(string text, Color color)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		((Component)CountText).gameObject.SetActive(true);
		CountText.text = text;
		((Graphic)CountText).color = color;
	}

	public void SetNull()
	{
		NowType = UIIconType.None;
	}

	public void SetItem(int id)
	{
		item item = new item(id);
		SetItem(item);
	}

	public void SetItem(int id, JSONObject seid)
	{
		item item = new item(id);
		item.Seid = seid;
		SetItem(item);
	}

	public void SetItem(ITEM_INFO info)
	{
		item item = new item(info.itemId);
		item.Seid = info.Seid;
		item.UUID = info.uuid;
		SetItem(item);
	}

	public void SetItem(JSONObject json)
	{
		int i = json["ItemID"].I;
		item item = null;
		item = new item(i);
		item.Seid = json;
		SetItem(item);
	}

	public void SetItem(item item)
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		NowType = UIIconType.Item;
		NowJiaoBiao = JiaoBiaoType.None;
		tmpItem = item;
		Quality.sprite = item.newitemPingZhiSprite;
		QualityUp.sprite = item.newitemPingZhiUP;
		Icon.sprite = item.itemIconSprite;
		NameText.text = item.itemName;
		((Graphic)NameText).color = _ItemQualityColor[item.ColorIndex];
		if (isShowStudy && PlayerEx.IsLingWuBook(item.itemID))
		{
			NowJiaoBiao = JiaoBiaoType.YiWu;
		}
		CalcFontSize();
		((Object)((Component)this).gameObject).name = item.itemName;
	}

	public void SetSkill(int id, bool showStudy = false, int level = 1)
	{
		isShowStudy = showStudy;
		GUIPackage.Skill skill = new GUIPackage.Skill(id, level, 5);
		SetSkill(skill);
	}

	public void SetSkill(GUIPackage.Skill skill)
	{
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		NowType = UIIconType.Skill;
		NowJiaoBiao = JiaoBiaoType.None;
		tmpSkill = skill;
		QualityUp.sprite = skill.newSkillPingZhiSprite;
		Icon.sprite = skill.skillIconSprite;
		NameText.text = skill.skill_Name.RemoveNumber();
		((Graphic)NameText).color = _ItemQualityColor[skill.ColorIndex];
		if (isShowStudy && Tools.instance.getPlayer().hasSkillList.Find((SkillItem aa) => aa.itemId == skill.SkillID) != null)
		{
			NowJiaoBiao = JiaoBiaoType.YiWu;
			IsLingWu = true;
		}
		CalcFontSize();
	}

	public void SetStaticSkill(int id, bool showStudy = false)
	{
		isShowStudy = showStudy;
		GUIPackage.Skill skill = SkillStaticDatebase.instence.dicSkills[id];
		SetStaticSkill(skill);
		((Object)((Component)this).gameObject).name = skill.skill_Name;
	}

	public void SetStaticSkill(GUIPackage.Skill skill)
	{
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		NowType = UIIconType.StaticSkill;
		NowJiaoBiao = JiaoBiaoType.None;
		tmpSkill = skill;
		QualityUp.sprite = skill.newSkillPingZhiSprite;
		Icon.sprite = skill.skillIconSprite;
		NameText.text = skill.skill_Name.RemoveNumber();
		((Graphic)NameText).color = _ItemQualityColor[skill.ColorIndex];
		if (isShowStudy && Tools.instance.getPlayer().hasStaticSkillList.Find((SkillItem aa) => aa.itemId == skill.SkillID) != null)
		{
			NowJiaoBiao = JiaoBiaoType.YiWu;
			IsLingWu = true;
		}
		CalcFontSize();
		((Object)((Component)this).gameObject).name = skill.skill_Name;
	}

	public void CalcFontSize()
	{
		if (NameText.text.Length == 6)
		{
			NameText.fontSize = 20;
		}
		else
		{
			NameText.fontSize = 24;
		}
	}

	public void SetBuChuan()
	{
		NowJiaoBiao = JiaoBiaoType.MiChuan;
	}

	public void SetCount(int count)
	{
		CountText.text = count.ToString();
		((Component)CountText).gameObject.SetActive(true);
	}

	public int GetCount()
	{
		return int.Parse(CountText.text);
	}

	public void OpenTooltip()
	{
		if ((Object)(object)ToolTipsMag.Inst == (Object)null)
		{
			ResManager.inst.LoadPrefab("ToolTips").Inst(((Component)NewUICanvas.Inst).transform);
		}
		switch (NowType)
		{
		case UIIconType.Item:
			switch (ShowPrice)
			{
			case ShowPriceType.None:
			case ShowPriceType.Normal:
				ToolTipsMag.Inst.Show(BaseItem.Create(tmpItem.itemID, tmpItem.itemNum, tmpItem.UUID, tmpItem.Seid));
				break;
			case ShowPriceType.PlayerSell:
				ToolTipsMag.Inst.Show(BaseItem.Create(tmpItem.itemID, tmpItem.itemNum, tmpItem.UUID, tmpItem.Seid), tmpItem.GetJiaoYiPrice(NPCID, isPlayer: true), isPlayer: true);
				break;
			case ShowPriceType.NPCSell:
				ToolTipsMag.Inst.Show(BaseItem.Create(tmpItem.itemID, tmpItem.itemNum, tmpItem.UUID, tmpItem.Seid), tmpItem.GetJiaoYiPrice(NPCID), isPlayer: false);
				break;
			}
			break;
		case UIIconType.Skill:
		{
			ActiveSkill activeSkill = new ActiveSkill();
			activeSkill.SetSkill(tmpSkill.SkillID, tmpSkill.Skill_Lv);
			ToolTipsMag.Inst.Show(activeSkill);
			break;
		}
		case UIIconType.StaticSkill:
		{
			PassiveSkill passiveSkill = new PassiveSkill();
			passiveSkill.SetSkill(tmpSkill.SkillID, tmpSkill.Skill_Lv);
			ToolTipsMag.Inst.Show(passiveSkill);
			break;
		}
		}
		UToolTip.BindObj = ((Component)this).gameObject;
	}

	public void CloseTooltip()
	{
		if ((Object)(object)ToolTipsMag.Inst != (Object)null)
		{
			ToolTipsMag.Inst.Close();
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if ((Object)(object)this != (Object)(object)NowDraggingObj)
		{
			NowMouseBottomObj = this;
		}
		OpenTooltip();
		isHover = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		CloseTooltip();
		isHover = false;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (OnClick != null)
		{
			OnClick.Invoke(eventData);
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if ((!((Object)(object)UILingTianPanel.Inst != (Object)null) || !UILingTianPanel.Inst.IsShouGe) && CanDrag && NowType != 0)
		{
			DragStarter = this;
			CreateDragObj();
			((Component)SlotRT).gameObject.SetActive(false);
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)NowDraggingObj != (Object)null)
		{
			float num = 1920f / (float)Screen.width * eventData.position.x;
			float num2 = 1080f / (float)Screen.height * eventData.position.y;
			NowDraggingObj.RT.anchoredPosition = new Vector2(num, num2);
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if ((Object)(object)NowDraggingObj != (Object)null)
		{
			bool flag = false;
			if ((Object)(object)NowMouseBottomObj != (Object)null && (Object)(object)NowMouseBottomObj != (Object)(object)DragStarter && NowMouseBottomObj.CanAcceptDragType == NowDraggingObj.NowType)
			{
				flag = true;
				NowMouseBottomObj.OnDragToThis(NowDraggingObj);
			}
			if (!flag)
			{
				((Component)SlotRT).gameObject.SetActive(true);
				Object.Destroy((Object)(object)((Component)NowDraggingObj).gameObject);
			}
		}
	}

	public void CreateDragObj()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		NowDraggingObj = Object.Instantiate<GameObject>(((Component)this).gameObject, ((Component)NewUICanvas.Inst.Canvas).transform).GetComponent<UIIconShow>();
		NowDraggingObj.AcceptObj(this);
		((Object)NowDraggingObj).name = "DraggingObj";
		NowDraggingObj.IsDraggingObj = true;
		NowDraggingObj.RT.anchorMin = Vector2.zero;
		NowDraggingObj.RT.anchorMax = Vector2.zero;
		RectTransform rT = NowDraggingObj.RT;
		((Transform)rT).localScale = ((Transform)rT).localScale * 0.821f;
		((Graphic)NowDraggingObj.BottomBG).raycastTarget = false;
	}

	public void OnDragToThis(UIIconShow draggingObj)
	{
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Expected O, but got Unknown
		if (IsLingTian)
		{
			if (NowType == UIIconType.None)
			{
				UILingTianCell component = ((Component)this).GetComponent<UILingTianCell>();
				int itemID = draggingObj.tmpItem.itemID;
				DongFuManager.ZhongZhi(DongFuManager.NowDongFuID, component.Slot, itemID);
				PlayerEx.Player.removeItem(itemID, 1);
				Object.Destroy((Object)(object)((Component)draggingObj).gameObject);
				UIDongFu.Inst.InitData();
				UILingTianPanel.Inst.RefreshUI();
			}
			else
			{
				Object.Destroy((Object)(object)((Component)draggingObj).gameObject);
				USelectBox.Show("当前灵田已经种有草药，不能继续种植");
				UILingTianPanel.Inst.RefreshUI();
			}
		}
		else
		{
			if (draggingObj.NowType != UIIconType.Item)
			{
				return;
			}
			UnityAction<int> val = delegate(int n)
			{
				if (NowType == UIIconType.None)
				{
					if (IsCopyDrag)
					{
						AcceptObj(draggingObj);
						((Component)DragStarter.SlotRT).gameObject.SetActive(true);
						Count = n;
					}
					else if (DragStarter.Count - n > 0)
					{
						AcceptObj(draggingObj);
						Count = n;
						DragStarter.Count -= n;
					}
					else
					{
						DragStarter.SetNull();
						AcceptObj(draggingObj);
					}
				}
				else if (IsCopyDrag)
				{
					AcceptObj(draggingObj);
					((Component)DragStarter.SlotRT).gameObject.SetActive(true);
					Count = n;
				}
				else
				{
					DragStarter.AcceptObj(this);
					AcceptObj(draggingObj);
				}
				Object.Destroy((Object)(object)((Component)draggingObj).gameObject);
			};
			object obj = _003C_003Ec._003C_003E9__85_1;
			if (obj == null)
			{
				UnityAction val2 = delegate
				{
					((Component)DragStarter.SlotRT).gameObject.SetActive(true);
				};
				_003C_003Ec._003C_003E9__85_1 = val2;
				obj = (object)val2;
			}
			UnityAction cancel = (UnityAction)obj;
			((Component)draggingObj).gameObject.SetActive(false);
			if (draggingObj.Count > 1)
			{
				USelectNum.Show("放入 " + draggingObj.NameText.text + " x{num}", 1, draggingObj.Count, val, cancel);
			}
			else
			{
				val.Invoke(1);
			}
		}
	}

	public void MoveToTarget()
	{
		if (!((Object)(object)Inventory != (Object)null) || (Object)(object)Inventory.dragTargetInventory != (Object)null || !((Object)(object)Inventory.dragTargetSlot != (Object)null))
		{
			return;
		}
		UnityAction<int> val = delegate(int n)
		{
			if (Inventory.dragTargetSlot.IsCopyDrag)
			{
				Inventory.dragTargetSlot.NowType = NowType;
				Inventory.dragTargetSlot.AcceptObj(this);
				Inventory.dragTargetSlot.Count = n;
			}
		};
		if (Count > 1)
		{
			USelectNum.Show("放入 " + NameText.text + " x{num}", 1, Count, val);
		}
		else
		{
			val.Invoke(1);
		}
	}

	public void AcceptObj(UIIconShow obj)
	{
		((Component)SlotRT).gameObject.SetActive(true);
		((Graphic)BottomBG).raycastTarget = true;
		switch (obj.NowType)
		{
		case UIIconType.None:
			SetNull();
			break;
		case UIIconType.Item:
			SetItem(obj.tmpItem);
			Count = obj.Count;
			break;
		case UIIconType.Skill:
			SetSkill(obj.tmpSkill);
			break;
		case UIIconType.StaticSkill:
			SetStaticSkill(obj.tmpSkill);
			break;
		}
	}
}
