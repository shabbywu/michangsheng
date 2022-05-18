using System;
using System.Collections.Generic;
using Bag;
using Fungus;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020003E5 RID: 997
public class UIDuJieZhunBei : MonoBehaviour, IESCClose
{
	// Token: 0x06001B22 RID: 6946 RVA: 0x00016F10 File Offset: 0x00015110
	private void Start()
	{
		UIDuJieZhunBei.Inst = this;
		ESCCloseManager.Inst.RegisterClose(this);
		this.BindBtnsEvent();
		this.RefreshUI();
	}

	// Token: 0x06001B23 RID: 6947 RVA: 0x00016F2F File Offset: 0x0001512F
	public void RefreshUI()
	{
		if (this.IsOpenByDuJie)
		{
			this.LeftBtnObj.SetActive(false);
			this.CloseBtn.transform.parent.gameObject.SetActive(false);
			this.SwitchToTiaoZhengPanel();
			return;
		}
		this.SwitchToGanWuPanel();
	}

	// Token: 0x06001B24 RID: 6948 RVA: 0x000EFC20 File Offset: 0x000EDE20
	public void RefreshInv(bool onlyLingWu)
	{
		this.SkillPrefab.SetActive(false);
		int childCount = this.SkillContentRT.childCount;
		if (childCount > 1)
		{
			for (int i = childCount - 1; i >= 1; i--)
			{
				Object.Destroy(this.SkillContentRT.GetChild(i).gameObject);
			}
		}
		this.InvSlots = new List<UITianJieSkillSlot>();
		foreach (TianJieMiShuData miShu in TianJieMiShuData.DataList)
		{
			BagTianJieSkill bagTianJieSkill = new BagTianJieSkill(miShu);
			if (!onlyLingWu || bagTianJieSkill.IsLingWu)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.SkillPrefab, this.SkillContentRT);
				gameObject.SetActive(true);
				UITianJieSkillSlot component = gameObject.GetComponent<UITianJieSkillSlot>();
				component.SetSlotData(bagTianJieSkill);
				component.SetGrey(!bagTianJieSkill.IsLingWu);
				component.OnLiftClick = new Action<UITianJieSkillSlot>(this.OnLeftSlotClick);
				component.OnRightClick = new Action<UITianJieSkillSlot>(this.OnLeftSlotClick);
				this.InvSlots.Add(component);
			}
		}
	}

	// Token: 0x06001B25 RID: 6949 RVA: 0x00016F6D File Offset: 0x0001516D
	private void OnLeftSlotClick(UITianJieSkillSlot slot)
	{
		if (this.State == UIDuJieZhunBeiState.感悟)
		{
			this.ClearSlotHighlight();
			this.ShowSlotGanWuData(slot);
		}
	}

	// Token: 0x06001B26 RID: 6950 RVA: 0x00016F84 File Offset: 0x00015184
	private void OnRightSlotRightClick(UITianJieSkillSlot slot)
	{
		if (this.State == UIDuJieZhunBeiState.调整 && !slot.IsNull())
		{
			slot.SetNull();
		}
	}

	// Token: 0x06001B27 RID: 6951 RVA: 0x000EFD38 File Offset: 0x000EDF38
	public void ClearSlotHighlight()
	{
		foreach (UITianJieSkillSlot uitianJieSkillSlot in this.InvSlots)
		{
			uitianJieSkillSlot.SetSelected(false);
		}
	}

	// Token: 0x06001B28 RID: 6952 RVA: 0x000EFD8C File Offset: 0x000EDF8C
	public void ShowSlotGanWuData(UITianJieSkillSlot slot)
	{
		this.nowSelectedSlot = slot;
		slot.SetSelected(true);
		this.HideRightBtns();
		TianJieMiShuData miShu = slot.TianJieSkill.MiShu;
		this.GanWuTiaoJianText.text = miShu.desc;
		this.GanWuShuoMingText.text = miShu.ShuoMing;
		if (miShu.Type == 0)
		{
			this.GanWuRoundLimitObj.SetActive(true);
			this.GanWuRoundLimitText.gameObject.SetActive(true);
			this.GanWuRoundLimitText.text = miShu.RoundLimit.ToCNNumber() + "回合";
			this.GanYingBtn2.transform.parent.gameObject.SetActive(true);
			if (!slot.TianJieSkill.IsGanYing)
			{
				this.GanWuTimeText.text = "？？？";
				this.LingWuBtn2.GetComponent<Image>().sprite = this.LingWuGraySprite;
				this.LingWuBtn2.enabled = false;
				return;
			}
			int number = Tools.CalcLingWuOrTuPoTime(miShu.StuTime, new List<int>());
			this.GanWuTimeText.text = number.MonthToDesc();
			if (slot.TianJieSkill.IsCanLingWu)
			{
				this.LingWuBtn2.GetComponent<Image>().sprite = this.LingWuBtn2.nomalSprite;
				this.LingWuBtn2.enabled = true;
				return;
			}
			this.LingWuBtn2.GetComponent<Image>().sprite = this.LingWuGraySprite;
			this.LingWuBtn2.enabled = false;
			return;
		}
		else
		{
			this.GanWuRoundLimitObj.SetActive(false);
			this.GanWuRoundLimitText.gameObject.SetActive(false);
			this.LingWuBtn.transform.parent.gameObject.SetActive(true);
			if (slot.TianJieSkill.IsCanLingWu)
			{
				this.LingWuBtn.GetComponent<Image>().sprite = this.LingWuBtn.nomalSprite;
				this.LingWuBtn.enabled = true;
			}
			else
			{
				this.LingWuBtn.GetComponent<Image>().sprite = this.LingWuGraySprite;
				this.LingWuBtn.enabled = false;
			}
			if (slot.TianJieSkill.IsCanLingWu || slot.TianJieSkill.IsLingWu)
			{
				int number2 = Tools.CalcLingWuOrTuPoTime(miShu.StuTime, new List<int>());
				this.GanWuTimeText.text = number2.MonthToDesc();
				return;
			}
			this.GanWuTimeText.text = "？？？";
			return;
		}
	}

	// Token: 0x06001B29 RID: 6953 RVA: 0x000EFFD8 File Offset: 0x000EE1D8
	public void SwitchToGanWuPanel()
	{
		this.State = UIDuJieZhunBeiState.感悟;
		this.GanWuBtnNoActive.gameObject.SetActive(false);
		this.GanWuBtnActive.SetActive(true);
		this.TiaoZhengBtnNoActive.gameObject.SetActive(true);
		this.TiaoZhengBtnActive.SetActive(false);
		this.GanWuObj.SetActive(true);
		this.TiaoZhengObj.SetActive(false);
		this.RefreshInv(false);
		foreach (UITianJieSkillSlot uitianJieSkillSlot in this.InvSlots)
		{
			uitianJieSkillSlot.IsCanDrag = false;
		}
		this.ShowSlotGanWuData(this.InvSlots[0]);
	}

	// Token: 0x06001B2A RID: 6954 RVA: 0x000F009C File Offset: 0x000EE29C
	public void SwitchToTiaoZhengPanel()
	{
		this.State = UIDuJieZhunBeiState.调整;
		this.GanWuBtnNoActive.gameObject.SetActive(true);
		this.GanWuBtnActive.SetActive(false);
		this.TiaoZhengBtnNoActive.gameObject.SetActive(false);
		this.TiaoZhengBtnActive.SetActive(true);
		this.GanWuObj.SetActive(false);
		this.TiaoZhengObj.SetActive(true);
		this.HideRightBtns();
		this.RefreshInv(true);
		this.ClearSlotHighlight();
		foreach (UITianJieSkillSlot uitianJieSkillSlot in this.InvSlots)
		{
			uitianJieSkillSlot.IsCanDrag = true;
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
			UITianJieSkillSlot uitianJieSkillSlot2 = this.TiaoZhengSlots[j];
			string str = player.TianJieEquipedSkills[j].Str;
			if (!string.IsNullOrWhiteSpace(str))
			{
				BagTianJieSkill slotData = new BagTianJieSkill(TianJieMiShuData.DataDict[str]);
				uitianJieSkillSlot2.SetSlotData(slotData);
			}
			else
			{
				uitianJieSkillSlot2.SetNull();
			}
			uitianJieSkillSlot2.OnRightClick = new Action<UITianJieSkillSlot>(this.OnRightSlotRightClick);
			uitianJieSkillSlot2.IsEquipSlot = true;
		}
		if (this.IsOpenByDuJie)
		{
			this.DuJieBtn.transform.parent.gameObject.SetActive(true);
		}
	}

	// Token: 0x06001B2B RID: 6955 RVA: 0x000F0230 File Offset: 0x000EE430
	public void ClearTiaoZhengSlotByID(string miShuID)
	{
		foreach (UITianJieSkillSlot uitianJieSkillSlot in this.TiaoZhengSlots)
		{
			if (!uitianJieSkillSlot.IsNull() && uitianJieSkillSlot.TianJieSkill.MiShu.id == miShuID)
			{
				uitianJieSkillSlot.SetNull();
			}
		}
	}

	// Token: 0x06001B2C RID: 6956 RVA: 0x000F02A4 File Offset: 0x000EE4A4
	public void SaveTiaoZheng()
	{
		Avatar player = PlayerEx.Player;
		player.TianJieEquipedSkills.Clear();
		for (int i = 0; i < 12; i++)
		{
			UITianJieSkillSlot uitianJieSkillSlot = this.TiaoZhengSlots[i];
			if (uitianJieSkillSlot.IsNull())
			{
				player.TianJieEquipedSkills.Add("");
			}
			else
			{
				player.TianJieEquipedSkills.Add(uitianJieSkillSlot.TianJieSkill.MiShu.id);
			}
		}
	}

	// Token: 0x06001B2D RID: 6957 RVA: 0x00016F9D File Offset: 0x0001519D
	public static void OpenPanel(bool isDuJie)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(Resources.Load<GameObject>("NewUI/DuJie/UIDuJieZhunBei"), NewUICanvas.Inst.Canvas.transform);
		gameObject.transform.SetAsLastSibling();
		gameObject.GetComponent<UIDuJieZhunBei>().IsOpenByDuJie = isDuJie;
	}

	// Token: 0x06001B2E RID: 6958 RVA: 0x000F0314 File Offset: 0x000EE514
	private void BindBtnsEvent()
	{
		this.GanWuBtnNoActive.mouseUpEvent.AddListener(new UnityAction(this.SwitchToGanWuPanel));
		this.TiaoZhengBtnNoActive.mouseUpEvent.AddListener(new UnityAction(this.SwitchToTiaoZhengPanel));
		this.CloseBtn.onClick.AddListener(new UnityAction(this.Close));
		this.LingWuBtn.mouseUpEvent.AddListener(new UnityAction(this.OnLingWuBtnClick));
		this.GanYingBtn2.mouseUpEvent.AddListener(new UnityAction(this.OnGanYingBtnClick));
		this.LingWuBtn2.mouseUpEvent.AddListener(new UnityAction(this.OnLingWuBtnClick));
		this.DuJieBtn.mouseUpEvent.AddListener(new UnityAction(this.OnDuJieBtnClick));
	}

	// Token: 0x06001B2F RID: 6959 RVA: 0x000F03E8 File Offset: 0x000EE5E8
	public void HideRightBtns()
	{
		this.GanYingBtn2.transform.parent.gameObject.SetActive(false);
		this.LingWuBtn.transform.parent.gameObject.SetActive(false);
		this.DuJieBtn.transform.parent.gameObject.SetActive(false);
	}

	// Token: 0x06001B30 RID: 6960 RVA: 0x000F0448 File Offset: 0x000EE648
	public void OnGanYingBtnClick()
	{
		this.Close();
		TianJieMiShuLingWuFightEventProcessor.MiShuID = this.nowSelectedSlot.TianJieSkill.MiShu.id;
		List<StarttFightAddBuff> list = new List<StarttFightAddBuff>();
		list.Add(new StarttFightAddBuff
		{
			buffID = 11020,
			BuffNum = 1
		});
		StartFight.Do(10010, 1, StartFight.MonstarType.Normal, StartFight.FightEnumType.天劫秘术领悟, 0, 0, 0, 39, "战斗3", false, "", new List<StarttFightAddBuff>(), list);
	}

	// Token: 0x06001B31 RID: 6961 RVA: 0x000F04BC File Offset: 0x000EE6BC
	public void OnLingWuBtnClick()
	{
		Avatar player = PlayerEx.Player;
		string id = this.nowSelectedSlot.TianJieSkill.MiShu.id;
		if (!player.TianJieYiLingWuSkills.StringListContains(id))
		{
			player.TianJieYiLingWuSkills.Add(id);
		}
		Tools.instance.playFader("正在神游太虚...", delegate
		{
			this.SwitchToGanWuPanel();
		});
		this.lingWuTime = Tools.CalcLingWuOrTuPoTime(this.nowSelectedSlot.TianJieSkill.MiShu.StuTime, new List<int>());
		base.Invoke("OkEvent", 0.5f);
	}

	// Token: 0x06001B32 RID: 6962 RVA: 0x000F0550 File Offset: 0x000EE750
	public void OkEvent()
	{
		Avatar player = PlayerEx.Player;
		int num = this.lingWuTime / 12;
		int num2 = this.lingWuTime % 12;
		player.AddTime(0, num2, num);
		if (player.getStaticID() != 0)
		{
			string text = player.TianJieBeforeShenYouSceneName;
			if (text.StartsWith("DongFu"))
			{
				DongFuManager.NowDongFuID = int.Parse(text.Replace("DongFu", ""));
				text = "S101";
			}
			float biguanSpeed = UIBiGuanXiuLianPanel.GetBiguanSpeed(true, 2, text);
			player.addEXP((int)((float)(num * 12 + num2) * biguanSpeed));
		}
		UIBiGuanXiuLianPanel.CalcShuangXiu(this.lingWuTime);
		player.HP = player.HP_Max;
		try
		{
			player.wuDaoMag.autoAddBiGuanLingGuang(num2 + num * 12);
		}
		catch (Exception ex)
		{
			Debug.LogError(ex);
		}
	}

	// Token: 0x06001B33 RID: 6963 RVA: 0x00016FD3 File Offset: 0x000151D3
	public void OnDuJieBtnClick()
	{
		UIPopTip.Inst.Pop("点击了渡劫按钮", PopTipIconType.叹号);
	}

	// Token: 0x06001B34 RID: 6964 RVA: 0x00016FE5 File Offset: 0x000151E5
	bool IESCClose.TryEscClose()
	{
		if (this.IsOpenByDuJie)
		{
			return false;
		}
		this.Close();
		return true;
	}

	// Token: 0x06001B35 RID: 6965 RVA: 0x00016FF8 File Offset: 0x000151F8
	public void Close()
	{
		ESCCloseManager.Inst.UnRegisterClose(this);
		UIDuJieZhunBei.Inst = null;
		Object.Destroy(base.gameObject);
	}

	// Token: 0x040016DB RID: 5851
	public static UIDuJieZhunBei Inst;

	// Token: 0x040016DC RID: 5852
	public GameObject LeftBtnObj;

	// Token: 0x040016DD RID: 5853
	public FpBtn GanWuBtnNoActive;

	// Token: 0x040016DE RID: 5854
	public GameObject GanWuBtnActive;

	// Token: 0x040016DF RID: 5855
	public FpBtn TiaoZhengBtnNoActive;

	// Token: 0x040016E0 RID: 5856
	public GameObject TiaoZhengBtnActive;

	// Token: 0x040016E1 RID: 5857
	public RectTransform SkillContentRT;

	// Token: 0x040016E2 RID: 5858
	public GameObject SkillPrefab;

	// Token: 0x040016E3 RID: 5859
	public GameObject GanWuObj;

	// Token: 0x040016E4 RID: 5860
	public Text GanWuShuoMingText;

	// Token: 0x040016E5 RID: 5861
	public Text GanWuTimeText;

	// Token: 0x040016E6 RID: 5862
	public Text GanWuTiaoJianText;

	// Token: 0x040016E7 RID: 5863
	public GameObject GanWuRoundLimitObj;

	// Token: 0x040016E8 RID: 5864
	public Text GanWuRoundLimitText;

	// Token: 0x040016E9 RID: 5865
	public GameObject TiaoZhengObj;

	// Token: 0x040016EA RID: 5866
	public List<UITianJieSkillSlot> TiaoZhengSlots;

	// Token: 0x040016EB RID: 5867
	public FpBtn GanYingBtn2;

	// Token: 0x040016EC RID: 5868
	public FpBtn LingWuBtn2;

	// Token: 0x040016ED RID: 5869
	public FpBtn LingWuBtn;

	// Token: 0x040016EE RID: 5870
	public Sprite LingWuGraySprite;

	// Token: 0x040016EF RID: 5871
	public FpBtn DuJieBtn;

	// Token: 0x040016F0 RID: 5872
	public Button CloseBtn;

	// Token: 0x040016F1 RID: 5873
	[HideInInspector]
	public List<UITianJieSkillSlot> InvSlots;

	// Token: 0x040016F2 RID: 5874
	[HideInInspector]
	public UIDuJieZhunBeiState State;

	// Token: 0x040016F3 RID: 5875
	private UITianJieSkillSlot nowSelectedSlot;

	// Token: 0x040016F4 RID: 5876
	[HideInInspector]
	public bool IsOpenByDuJie;

	// Token: 0x040016F5 RID: 5877
	private int lingWuTime;
}
