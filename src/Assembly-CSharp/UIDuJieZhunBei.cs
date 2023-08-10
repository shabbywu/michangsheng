using System;
using System.Collections.Generic;
using Bag;
using Fungus;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIDuJieZhunBei : MonoBehaviour, IESCClose
{
	public static UIDuJieZhunBei Inst;

	public GameObject LeftBtnObj;

	public FpBtn GanWuBtnNoActive;

	public GameObject GanWuBtnActive;

	public FpBtn TiaoZhengBtnNoActive;

	public GameObject TiaoZhengBtnActive;

	public RectTransform SkillContentRT;

	public GameObject SkillPrefab;

	public GameObject GanWuObj;

	public Text GanWuShuoMingText;

	public Text GanWuTimeText;

	public Text GanWuTiaoJianText;

	public GameObject GanWuRoundLimitObj;

	public Text GanWuRoundLimitText;

	public GameObject TiaoZhengObj;

	public List<UITianJieSkillSlot> TiaoZhengSlots;

	public FpBtn GanYingBtn2;

	public FpBtn LingWuBtn2;

	public FpBtn LingWuBtn;

	public Sprite LingWuGraySprite;

	public FpBtn DuJieBtn;

	public Button CloseBtn;

	[HideInInspector]
	public List<UITianJieSkillSlot> InvSlots;

	[HideInInspector]
	public UIDuJieZhunBeiState State;

	private UITianJieSkillSlot nowSelectedSlot;

	[HideInInspector]
	public bool IsOpenByDuJie;

	private int lingWuTime;

	private void Start()
	{
		Inst = this;
		ESCCloseManager.Inst.RegisterClose(this);
		BindBtnsEvent();
		RefreshUI();
	}

	public void RefreshUI()
	{
		if (IsOpenByDuJie)
		{
			LeftBtnObj.SetActive(false);
			((Component)((Component)CloseBtn).transform.parent).gameObject.SetActive(false);
			SwitchToTiaoZhengPanel();
		}
		else
		{
			SwitchToGanWuPanel();
		}
	}

	public void RefreshInv(bool onlyLingWu)
	{
		SkillPrefab.SetActive(false);
		int childCount = ((Transform)SkillContentRT).childCount;
		if (childCount > 1)
		{
			for (int num = childCount - 1; num >= 1; num--)
			{
				Object.Destroy((Object)(object)((Component)((Transform)SkillContentRT).GetChild(num)).gameObject);
			}
		}
		InvSlots = new List<UITianJieSkillSlot>();
		foreach (TianJieMiShuData data in TianJieMiShuData.DataList)
		{
			BagTianJieSkill bagTianJieSkill = new BagTianJieSkill(data);
			if (!onlyLingWu || bagTianJieSkill.IsLingWu)
			{
				GameObject obj = Object.Instantiate<GameObject>(SkillPrefab, (Transform)(object)SkillContentRT);
				obj.SetActive(true);
				UITianJieSkillSlot component = obj.GetComponent<UITianJieSkillSlot>();
				component.SetSlotData(bagTianJieSkill);
				component.SetGrey(!bagTianJieSkill.IsLingWu);
				component.OnLiftClick = OnLeftSlotClick;
				component.OnRightClick = OnLeftSlotClick;
				InvSlots.Add(component);
			}
		}
	}

	private void OnLeftSlotClick(UITianJieSkillSlot slot)
	{
		if (State == UIDuJieZhunBeiState.感悟)
		{
			ClearSlotHighlight();
			ShowSlotGanWuData(slot);
		}
	}

	private void OnRightSlotRightClick(UITianJieSkillSlot slot)
	{
		if (State == UIDuJieZhunBeiState.调整 && !slot.IsNull())
		{
			slot.SetNull();
		}
	}

	public void ClearSlotHighlight()
	{
		foreach (UITianJieSkillSlot invSlot in InvSlots)
		{
			invSlot.SetSelected(value: false);
		}
	}

	public void ShowSlotGanWuData(UITianJieSkillSlot slot)
	{
		nowSelectedSlot = slot;
		slot.SetSelected(value: true);
		HideRightBtns();
		TianJieMiShuData miShu = slot.TianJieSkill.MiShu;
		GanWuTiaoJianText.text = miShu.desc;
		GanWuShuoMingText.text = miShu.ShuoMing;
		if (miShu.Type == 0)
		{
			GanWuRoundLimitObj.SetActive(true);
			((Component)GanWuRoundLimitText).gameObject.SetActive(true);
			GanWuRoundLimitText.text = miShu.RoundLimit.ToCNNumber() + "回合";
			((Component)((Component)GanYingBtn2).transform.parent).gameObject.SetActive(true);
			if (slot.TianJieSkill.IsGanYing)
			{
				int number = Tools.CalcLingWuOrTuPoTime(miShu.StuTime, new List<int>());
				GanWuTimeText.text = number.MonthToDesc();
				if (slot.TianJieSkill.IsCanLingWu)
				{
					((Component)LingWuBtn2).GetComponent<Image>().sprite = LingWuBtn2.nomalSprite;
					((Behaviour)LingWuBtn2).enabled = true;
				}
				else
				{
					((Component)LingWuBtn2).GetComponent<Image>().sprite = LingWuGraySprite;
					((Behaviour)LingWuBtn2).enabled = false;
				}
			}
			else
			{
				GanWuTimeText.text = "？？？";
				((Component)LingWuBtn2).GetComponent<Image>().sprite = LingWuGraySprite;
				((Behaviour)LingWuBtn2).enabled = false;
			}
		}
		else
		{
			GanWuRoundLimitObj.SetActive(false);
			((Component)GanWuRoundLimitText).gameObject.SetActive(false);
			((Component)((Component)LingWuBtn).transform.parent).gameObject.SetActive(true);
			if (slot.TianJieSkill.IsCanLingWu)
			{
				((Component)LingWuBtn).GetComponent<Image>().sprite = LingWuBtn.nomalSprite;
				((Behaviour)LingWuBtn).enabled = true;
			}
			else
			{
				((Component)LingWuBtn).GetComponent<Image>().sprite = LingWuGraySprite;
				((Behaviour)LingWuBtn).enabled = false;
			}
			if (slot.TianJieSkill.IsCanLingWu || slot.TianJieSkill.IsLingWu)
			{
				int number2 = Tools.CalcLingWuOrTuPoTime(miShu.StuTime, new List<int>());
				GanWuTimeText.text = number2.MonthToDesc();
			}
			else
			{
				GanWuTimeText.text = "？？？";
			}
		}
	}

	public void SwitchToGanWuPanel()
	{
		State = UIDuJieZhunBeiState.感悟;
		((Component)GanWuBtnNoActive).gameObject.SetActive(false);
		GanWuBtnActive.SetActive(true);
		((Component)TiaoZhengBtnNoActive).gameObject.SetActive(true);
		TiaoZhengBtnActive.SetActive(false);
		GanWuObj.SetActive(true);
		TiaoZhengObj.SetActive(false);
		RefreshInv(onlyLingWu: false);
		foreach (UITianJieSkillSlot invSlot in InvSlots)
		{
			invSlot.IsCanDrag = false;
		}
		ShowSlotGanWuData(InvSlots[0]);
	}

	public void SwitchToTiaoZhengPanel()
	{
		State = UIDuJieZhunBeiState.调整;
		((Component)GanWuBtnNoActive).gameObject.SetActive(true);
		GanWuBtnActive.SetActive(false);
		((Component)TiaoZhengBtnNoActive).gameObject.SetActive(false);
		TiaoZhengBtnActive.SetActive(true);
		GanWuObj.SetActive(false);
		TiaoZhengObj.SetActive(true);
		HideRightBtns();
		RefreshInv(onlyLingWu: true);
		ClearSlotHighlight();
		foreach (UITianJieSkillSlot invSlot in InvSlots)
		{
			invSlot.IsCanDrag = true;
		}
		Avatar player = PlayerEx.Player;
		if (player.TianJieEquipedSkills.Count != 12)
		{
			player.TianJieEquipedSkills.Clear();
			for (int i = 0; i < 12; i++)
			{
				player.TianJieEquipedSkills.Add("");
			}
		}
		for (int j = 0; j < 12; j++)
		{
			UITianJieSkillSlot uITianJieSkillSlot = TiaoZhengSlots[j];
			string str = player.TianJieEquipedSkills[j].Str;
			if (!string.IsNullOrWhiteSpace(str))
			{
				BagTianJieSkill slotData = new BagTianJieSkill(TianJieMiShuData.DataDict[str]);
				uITianJieSkillSlot.SetSlotData(slotData);
			}
			else
			{
				uITianJieSkillSlot.SetNull();
			}
			uITianJieSkillSlot.OnRightClick = OnRightSlotRightClick;
			uITianJieSkillSlot.IsEquipSlot = true;
		}
		if (IsOpenByDuJie)
		{
			((Component)((Component)DuJieBtn).transform.parent).gameObject.SetActive(true);
		}
	}

	public void ClearTiaoZhengSlotByID(string miShuID)
	{
		foreach (UITianJieSkillSlot tiaoZhengSlot in TiaoZhengSlots)
		{
			if (!tiaoZhengSlot.IsNull() && tiaoZhengSlot.TianJieSkill.MiShu.id == miShuID)
			{
				tiaoZhengSlot.SetNull();
			}
		}
	}

	public void SaveTiaoZheng()
	{
		Avatar player = PlayerEx.Player;
		player.TianJieEquipedSkills.Clear();
		for (int i = 0; i < 12; i++)
		{
			UITianJieSkillSlot uITianJieSkillSlot = TiaoZhengSlots[i];
			if (uITianJieSkillSlot.IsNull())
			{
				player.TianJieEquipedSkills.Add("");
			}
			else
			{
				player.TianJieEquipedSkills.Add(uITianJieSkillSlot.TianJieSkill.MiShu.id);
			}
		}
	}

	public static void OpenPanel(bool isDuJie)
	{
		GameObject obj = Object.Instantiate<GameObject>(Resources.Load<GameObject>("NewUI/DuJie/UIDuJieZhunBei"), ((Component)NewUICanvas.Inst.Canvas).transform);
		obj.transform.SetAsLastSibling();
		obj.GetComponent<UIDuJieZhunBei>().IsOpenByDuJie = isDuJie;
	}

	private void BindBtnsEvent()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Expected O, but got Unknown
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Expected O, but got Unknown
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Expected O, but got Unknown
		GanWuBtnNoActive.mouseUpEvent.AddListener(new UnityAction(SwitchToGanWuPanel));
		TiaoZhengBtnNoActive.mouseUpEvent.AddListener(new UnityAction(SwitchToTiaoZhengPanel));
		((UnityEvent)CloseBtn.onClick).AddListener(new UnityAction(Close));
		LingWuBtn.mouseUpEvent.AddListener(new UnityAction(OnLingWuBtnClick));
		GanYingBtn2.mouseUpEvent.AddListener(new UnityAction(OnGanYingBtnClick));
		LingWuBtn2.mouseUpEvent.AddListener(new UnityAction(OnLingWuBtnClick));
		DuJieBtn.mouseUpEvent.AddListener(new UnityAction(OnDuJieBtnClick));
	}

	public void HideRightBtns()
	{
		((Component)((Component)GanYingBtn2).transform.parent).gameObject.SetActive(false);
		((Component)((Component)LingWuBtn).transform.parent).gameObject.SetActive(false);
		((Component)((Component)DuJieBtn).transform.parent).gameObject.SetActive(false);
	}

	public void OnGanYingBtnClick()
	{
		Close();
		TianJieMiShuLingWuFightEventProcessor.MiShuID = nowSelectedSlot.TianJieSkill.MiShu.id;
		List<StarttFightAddBuff> list = new List<StarttFightAddBuff>();
		list.Add(new StarttFightAddBuff
		{
			buffID = 11020,
			BuffNum = 1
		});
		StartFight.Do(10010, 1, StartFight.MonstarType.Normal, StartFight.FightEnumType.天劫秘术领悟, 0, 0, 0, 39, "战斗3", SeaRemoveNPCFlag: false, "", new List<StarttFightAddBuff>(), list);
	}

	public void OnLingWuBtnClick()
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		Avatar player = PlayerEx.Player;
		string id = nowSelectedSlot.TianJieSkill.MiShu.id;
		if (!player.TianJieYiLingWuSkills.StringListContains(id))
		{
			player.TianJieYiLingWuSkills.Add(id);
		}
		Tools.instance.playFader("正在神游太虚...", (UnityAction)delegate
		{
			SwitchToGanWuPanel();
		});
		lingWuTime = Tools.CalcLingWuOrTuPoTime(nowSelectedSlot.TianJieSkill.MiShu.StuTime, new List<int>());
		((MonoBehaviour)this).Invoke("OkEvent", 0.5f);
	}

	public void OkEvent()
	{
		Avatar player = PlayerEx.Player;
		int num = lingWuTime / 12;
		int num2 = lingWuTime % 12;
		player.AddTime(0, num2, num);
		if (player.getStaticID() != 0)
		{
			string text = player.TianJieBeforeShenYouSceneName;
			if (text.StartsWith("DongFu"))
			{
				DongFuManager.NowDongFuID = int.Parse(text.Replace("DongFu", ""));
				text = "S101";
			}
			float biguanSpeed = UIBiGuanXiuLianPanel.GetBiguanSpeed(log: true, 2, text);
			player.addEXP((int)((float)(num * 12 + num2) * biguanSpeed));
		}
		UIBiGuanXiuLianPanel.CalcShuangXiu(lingWuTime);
		player.HP = player.HP_Max;
		try
		{
			player.wuDaoMag.autoAddBiGuanLingGuang(num2 + num * 12);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
	}

	public void OnDuJieBtnClick()
	{
		UIPopTip.Inst.Pop("点击了渡劫按钮");
	}

	bool IESCClose.TryEscClose()
	{
		if (IsOpenByDuJie)
		{
			return false;
		}
		Close();
		return true;
	}

	public void Close()
	{
		ESCCloseManager.Inst.UnRegisterClose(this);
		Inst = null;
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}
}
