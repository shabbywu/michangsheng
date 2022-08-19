using System;
using System.Collections.Generic;
using Bag;
using Fungus;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020002AD RID: 685
public class UIDuJieZhunBei : MonoBehaviour, IESCClose
{
	// Token: 0x06001830 RID: 6192 RVA: 0x000A8CE4 File Offset: 0x000A6EE4
	private void Start()
	{
		UIDuJieZhunBei.Inst = this;
		ESCCloseManager.Inst.RegisterClose(this);
		this.BindBtnsEvent();
		this.RefreshUI();
	}

	// Token: 0x06001831 RID: 6193 RVA: 0x000A8D03 File Offset: 0x000A6F03
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

	// Token: 0x06001832 RID: 6194 RVA: 0x000A8D44 File Offset: 0x000A6F44
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

	// Token: 0x06001833 RID: 6195 RVA: 0x000A8E5C File Offset: 0x000A705C
	private void OnLeftSlotClick(UITianJieSkillSlot slot)
	{
		if (this.State == UIDuJieZhunBeiState.感悟)
		{
			this.ClearSlotHighlight();
			this.ShowSlotGanWuData(slot);
		}
	}

	// Token: 0x06001834 RID: 6196 RVA: 0x000A8E73 File Offset: 0x000A7073
	private void OnRightSlotRightClick(UITianJieSkillSlot slot)
	{
		if (this.State == UIDuJieZhunBeiState.调整 && !slot.IsNull())
		{
			slot.SetNull();
		}
	}

	// Token: 0x06001835 RID: 6197 RVA: 0x000A8E8C File Offset: 0x000A708C
	public void ClearSlotHighlight()
	{
		foreach (UITianJieSkillSlot uitianJieSkillSlot in this.InvSlots)
		{
			uitianJieSkillSlot.SetSelected(false);
		}
	}

	// Token: 0x06001836 RID: 6198 RVA: 0x000A8EE0 File Offset: 0x000A70E0
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

	// Token: 0x06001837 RID: 6199 RVA: 0x000A912C File Offset: 0x000A732C
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

	// Token: 0x06001838 RID: 6200 RVA: 0x000A91F0 File Offset: 0x000A73F0
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

	// Token: 0x06001839 RID: 6201 RVA: 0x000A9384 File Offset: 0x000A7584
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

	// Token: 0x0600183A RID: 6202 RVA: 0x000A93F8 File Offset: 0x000A75F8
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

	// Token: 0x0600183B RID: 6203 RVA: 0x000A9465 File Offset: 0x000A7665
	public static void OpenPanel(bool isDuJie)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(Resources.Load<GameObject>("NewUI/DuJie/UIDuJieZhunBei"), NewUICanvas.Inst.Canvas.transform);
		gameObject.transform.SetAsLastSibling();
		gameObject.GetComponent<UIDuJieZhunBei>().IsOpenByDuJie = isDuJie;
	}

	// Token: 0x0600183C RID: 6204 RVA: 0x000A949C File Offset: 0x000A769C
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

	// Token: 0x0600183D RID: 6205 RVA: 0x000A9570 File Offset: 0x000A7770
	public void HideRightBtns()
	{
		this.GanYingBtn2.transform.parent.gameObject.SetActive(false);
		this.LingWuBtn.transform.parent.gameObject.SetActive(false);
		this.DuJieBtn.transform.parent.gameObject.SetActive(false);
	}

	// Token: 0x0600183E RID: 6206 RVA: 0x000A95D0 File Offset: 0x000A77D0
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

	// Token: 0x0600183F RID: 6207 RVA: 0x000A9644 File Offset: 0x000A7844
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

	// Token: 0x06001840 RID: 6208 RVA: 0x000A96D8 File Offset: 0x000A78D8
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

	// Token: 0x06001841 RID: 6209 RVA: 0x000A97A0 File Offset: 0x000A79A0
	public void OnDuJieBtnClick()
	{
		UIPopTip.Inst.Pop("点击了渡劫按钮", PopTipIconType.叹号);
	}

	// Token: 0x06001842 RID: 6210 RVA: 0x000A97B2 File Offset: 0x000A79B2
	bool IESCClose.TryEscClose()
	{
		if (this.IsOpenByDuJie)
		{
			return false;
		}
		this.Close();
		return true;
	}

	// Token: 0x06001843 RID: 6211 RVA: 0x000A97C5 File Offset: 0x000A79C5
	public void Close()
	{
		ESCCloseManager.Inst.UnRegisterClose(this);
		UIDuJieZhunBei.Inst = null;
		Object.Destroy(base.gameObject);
	}

	// Token: 0x0400133E RID: 4926
	public static UIDuJieZhunBei Inst;

	// Token: 0x0400133F RID: 4927
	public GameObject LeftBtnObj;

	// Token: 0x04001340 RID: 4928
	public FpBtn GanWuBtnNoActive;

	// Token: 0x04001341 RID: 4929
	public GameObject GanWuBtnActive;

	// Token: 0x04001342 RID: 4930
	public FpBtn TiaoZhengBtnNoActive;

	// Token: 0x04001343 RID: 4931
	public GameObject TiaoZhengBtnActive;

	// Token: 0x04001344 RID: 4932
	public RectTransform SkillContentRT;

	// Token: 0x04001345 RID: 4933
	public GameObject SkillPrefab;

	// Token: 0x04001346 RID: 4934
	public GameObject GanWuObj;

	// Token: 0x04001347 RID: 4935
	public Text GanWuShuoMingText;

	// Token: 0x04001348 RID: 4936
	public Text GanWuTimeText;

	// Token: 0x04001349 RID: 4937
	public Text GanWuTiaoJianText;

	// Token: 0x0400134A RID: 4938
	public GameObject GanWuRoundLimitObj;

	// Token: 0x0400134B RID: 4939
	public Text GanWuRoundLimitText;

	// Token: 0x0400134C RID: 4940
	public GameObject TiaoZhengObj;

	// Token: 0x0400134D RID: 4941
	public List<UITianJieSkillSlot> TiaoZhengSlots;

	// Token: 0x0400134E RID: 4942
	public FpBtn GanYingBtn2;

	// Token: 0x0400134F RID: 4943
	public FpBtn LingWuBtn2;

	// Token: 0x04001350 RID: 4944
	public FpBtn LingWuBtn;

	// Token: 0x04001351 RID: 4945
	public Sprite LingWuGraySprite;

	// Token: 0x04001352 RID: 4946
	public FpBtn DuJieBtn;

	// Token: 0x04001353 RID: 4947
	public Button CloseBtn;

	// Token: 0x04001354 RID: 4948
	[HideInInspector]
	public List<UITianJieSkillSlot> InvSlots;

	// Token: 0x04001355 RID: 4949
	[HideInInspector]
	public UIDuJieZhunBeiState State;

	// Token: 0x04001356 RID: 4950
	private UITianJieSkillSlot nowSelectedSlot;

	// Token: 0x04001357 RID: 4951
	[HideInInspector]
	public bool IsOpenByDuJie;

	// Token: 0x04001358 RID: 4952
	private int lingWuTime;
}
