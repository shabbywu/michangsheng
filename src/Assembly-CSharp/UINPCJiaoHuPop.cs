using System;
using JSONClass;
using script.YarnEditor.Manager;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000273 RID: 627
public class UINPCJiaoHuPop : MonoBehaviour, IESCClose
{
	// Token: 0x060016C8 RID: 5832 RVA: 0x0009B376 File Offset: 0x00099576
	private void Update()
	{
		this.AutoHide();
	}

	// Token: 0x060016C9 RID: 5833 RVA: 0x0009B380 File Offset: 0x00099580
	public bool CanShow()
	{
		return !UINPCJiaoHu.AllShouldHide && (!(PanelMamager.inst != null) || !(PanelMamager.inst.UISceneGameObject == null)) && (!(PanelMamager.inst != null) || PanelMamager.inst.nowPanel == PanelMamager.PanelType.空);
	}

	// Token: 0x060016CA RID: 5834 RVA: 0x0009B3D4 File Offset: 0x000995D4
	private void AutoHide()
	{
		if (!this.CanShow())
		{
			UINPCJiaoHu.Inst.HideJiaoHuPop();
		}
	}

	// Token: 0x060016CB RID: 5835 RVA: 0x0009B3E8 File Offset: 0x000995E8
	public void RefreshUI()
	{
		this.npc = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		this.npc.RefreshData();
		this.NameText.text = this.npc.Name;
		this.Face.SetNPCFace(this.npc.ID);
		if (GlobalValue.Get(1600, "unknow") == 1)
		{
			this.DuJieCloseBtn();
			return;
		}
		this.OpenAllBtn();
		if (this.npc.FavorLevel >= 3)
		{
			this.SetBtnNormal(this.QingJiaoBtn);
		}
		else
		{
			this.SetBtnGrey(this.QingJiaoBtn);
		}
		if (this.npc.FavorLevel > 3)
		{
			this.SetBtnNormal(this.LunDaoBtn);
			this.SetBtnNormal(this.QieCuoBtn);
		}
		else
		{
			this.SetBtnGrey(this.LunDaoBtn);
			this.SetBtnGrey(this.QieCuoBtn);
		}
		if (this.npc.FavorLevel != 3 || this.npc.IsKnowPlayer)
		{
			this.SetBtnNormal(this.ZengLiBtn);
		}
		else
		{
			this.SetBtnGrey(this.ZengLiBtn);
		}
		if (this.npc.FavorLevel >= 5)
		{
			this.ChaKanBtn.gameObject.SetActive(true);
			this.TanChaBtn.gameObject.SetActive(false);
			this.SetBtnNormal(this.ChaKanBtn);
			return;
		}
		this.ChaKanBtn.gameObject.SetActive(false);
		this.TanChaBtn.gameObject.SetActive(true);
		this.TanChaBtn.enabled = true;
		UINPCJiaoHu.Inst.SetBtnDangerColor(this.TanChaBtn.transform);
	}

	// Token: 0x060016CC RID: 5836 RVA: 0x0009B578 File Offset: 0x00099778
	private void OpenAllBtn()
	{
		this.SetBtnNormal(this.JiaoTanBtn);
		this.SetBtnNormal(this.LiKaiBtn);
		this.SetBtnNormal(this.QieCuoBtn);
		this.SetBtnNormal(this.LunDaoBtn);
		this.SetBtnNormal(this.JiaoYiBtn);
		this.SetBtnNormal(this.ZengLiBtn);
		this.SetBtnNormal(this.QingJiaoBtn);
		this.SetDangerBtnNormal(this.SuoQuBtn);
		this.SetBtnNormal(this.ChaKanBtn);
		this.SetBtnNormal(this.TanChaBtn);
		this.SetDangerBtnNormal(this.JieShaBtn);
	}

	// Token: 0x060016CD RID: 5837 RVA: 0x0009B60C File Offset: 0x0009980C
	private void DuJieCloseBtn()
	{
		this.SetBtnClose(this.LunDaoBtn);
		this.SetBtnClose(this.JiaoYiBtn);
		this.SetBtnClose(this.QieCuoBtn);
		this.SetBtnClose(this.ZengLiBtn);
		this.SetBtnClose(this.QingJiaoBtn);
		this.SetBtnClose(this.SuoQuBtn);
		this.SetBtnClose(this.ChaKanBtn);
		this.SetBtnClose(this.TanChaBtn);
		this.SetBtnClose(this.JieShaBtn);
	}

	// Token: 0x060016CE RID: 5838 RVA: 0x0009B688 File Offset: 0x00099888
	private void SpecialNPC1()
	{
		this.SetBtnNormal(this.JiaoTanBtn);
		this.SetBtnNormal(this.LiKaiBtn);
		this.SetBtnClose(this.QieCuoBtn);
		this.SetBtnClose(this.LunDaoBtn);
		this.SetBtnClose(this.JiaoYiBtn);
		this.SetBtnClose(this.ZengLiBtn);
		this.SetBtnClose(this.QingJiaoBtn);
		this.SetBtnClose(this.SuoQuBtn);
		this.SetBtnClose(this.ChaKanBtn);
		this.SetBtnClose(this.TanChaBtn);
		this.SetBtnClose(this.JieShaBtn);
	}

	// Token: 0x060016CF RID: 5839 RVA: 0x0009B71C File Offset: 0x0009991C
	private void SpecialNPC2()
	{
		this.SetBtnNormal(this.JiaoTanBtn);
		this.SetBtnNormal(this.LiKaiBtn);
		this.SetBtnNormal(this.JiaoYiBtn);
		this.SetBtnClose(this.QieCuoBtn);
		this.SetBtnClose(this.LunDaoBtn);
		this.SetBtnClose(this.ZengLiBtn);
		this.SetBtnClose(this.QingJiaoBtn);
		this.SetBtnClose(this.SuoQuBtn);
		this.SetBtnClose(this.ChaKanBtn);
		this.SetBtnClose(this.TanChaBtn);
		this.SetBtnClose(this.JieShaBtn);
	}

	// Token: 0x060016D0 RID: 5840 RVA: 0x0009B7AD File Offset: 0x000999AD
	private void SetBtnNormal(Button btn)
	{
		UINPCJiaoHu.Inst.SetBtnNormalColor(btn.transform);
		btn.enabled = true;
	}

	// Token: 0x060016D1 RID: 5841 RVA: 0x0009B7C6 File Offset: 0x000999C6
	private void SetDangerBtnNormal(Button btn)
	{
		UINPCJiaoHu.Inst.SetBtnDangerColor(btn.transform);
		btn.enabled = true;
	}

	// Token: 0x060016D2 RID: 5842 RVA: 0x0009B7DF File Offset: 0x000999DF
	private void SetBtnGrey(Button btn)
	{
		UINPCJiaoHu.Inst.SetBtnGreyColor(btn.transform);
		btn.enabled = true;
	}

	// Token: 0x060016D3 RID: 5843 RVA: 0x0009B7F8 File Offset: 0x000999F8
	private void SetBtnClose(Button btn)
	{
		UINPCJiaoHu.Inst.SetBtnGreyColor(btn.transform);
		btn.enabled = false;
	}

	// Token: 0x060016D4 RID: 5844 RVA: 0x0009B814 File Offset: 0x00099A14
	public void OnJiaoTanBtnClick()
	{
		if (StoryManager.Inst.CheckTrigger(this.npc.ID))
		{
			UINPCJiaoHu.Inst.HideJiaoHuPop();
			return;
		}
		if ((this.npc.ActionID == 101 || this.npc.ActionID == 102 || this.npc.ActionID == 103) && SceneNameJsonData.DataDict.ContainsKey(SceneEx.NowSceneName) && SceneNameJsonData.DataDict[SceneEx.NowSceneName].MapType == 3)
		{
			if (this.npc.IsZhongYaoNPC)
			{
				if (!NPCEx.OpenAction101102103GuDingTalk(this.npc.ZhongYaoNPCID, this.npc.ActionID))
				{
					NPCEx.OpenTalk(this.npc.ActionID + 3902);
				}
			}
			else
			{
				NPCEx.OpenTalk(this.npc.ActionID + 3902);
			}
			UINPCJiaoHu.Inst.HideJiaoHuPop();
			return;
		}
		if (this.npc.IsSeaNPC)
		{
			this.OnSeaNPCClick(this.npc.SeaEventID);
			UINPCJiaoHu.Inst.HideJiaoHuPop();
			return;
		}
		if (this.npc.IsZhongYaoNPC && UINPCData.ThreeSceneZhongYaoNPCTalkCache.ContainsKey(this.npc.ZhongYaoNPCID))
		{
			UINPCData.ThreeSceneZhongYaoNPCTalkCache[this.npc.ZhongYaoNPCID].Invoke();
			UINPCJiaoHu.Inst.HideJiaoHuPop();
			return;
		}
		UINPCJiaoHu.Inst.IsLiaoTianClicked = true;
		UINPCJiaoHu.Inst.HideJiaoHuPop();
	}

	// Token: 0x060016D5 RID: 5845 RVA: 0x0009B985 File Offset: 0x00099B85
	public void OnLunDaoBtnClick()
	{
		if (this.npc.FavorLevel > 3)
		{
			UINPCJiaoHu.Inst.IsLunDaoClicked = true;
			UINPCJiaoHu.Inst.HideJiaoHuPop();
			return;
		}
		UIPopTip.Inst.Pop("好感度不足", PopTipIconType.叹号);
	}

	// Token: 0x060016D6 RID: 5846 RVA: 0x0009B9BB File Offset: 0x00099BBB
	public void OnJiaoYiBtnClick()
	{
		UINPCJiaoHu.Inst.IsJiaoYiClicked = true;
	}

	// Token: 0x060016D7 RID: 5847 RVA: 0x0009B9C8 File Offset: 0x00099BC8
	public void OnQieCuoBtnClick()
	{
		if (this.npc.FavorLevel > 3)
		{
			UINPCJiaoHu.Inst.IsQieCuoClicked = true;
			return;
		}
		UIPopTip.Inst.Pop("好感度不足", PopTipIconType.叹号);
	}

	// Token: 0x060016D8 RID: 5848 RVA: 0x0009B9F4 File Offset: 0x00099BF4
	public void OnZengLiBtnClick()
	{
		if (this.npc.FavorLevel != 3 || this.npc.IsKnowPlayer)
		{
			UINPCJiaoHu.Inst.ShowNPCZengLi();
			UINPCJiaoHu.Inst.HideJiaoHuPop();
			return;
		}
		UIPopTip.Inst.Pop("对方还不认识你", PopTipIconType.叹号);
	}

	// Token: 0x060016D9 RID: 5849 RVA: 0x0009BA41 File Offset: 0x00099C41
	public void OnQingJiaoBtnClick()
	{
		if (this.npc.FavorLevel >= 3)
		{
			UINPCJiaoHu.Inst.ShowNPCQingJiaoPanel();
			return;
		}
		UIPopTip.Inst.Pop("好感度不足", PopTipIconType.叹号);
	}

	// Token: 0x060016DA RID: 5850 RVA: 0x0009BA6C File Offset: 0x00099C6C
	public void OnSuoQuBtnClick()
	{
		UINPCJiaoHu.Inst.ShowNPCSuoQu();
		UINPCJiaoHu.Inst.HideJiaoHuPop();
	}

	// Token: 0x060016DB RID: 5851 RVA: 0x0009BA82 File Offset: 0x00099C82
	public void OnTanChaBtnClick()
	{
		if (this.npc.FavorLevel >= 5 || this.npc.IsTanChaUnlock)
		{
			UINPCJiaoHu.Inst.ShowNPCInfoPanel(null);
			return;
		}
		UINPCJiaoHu.Inst.HideJiaoHuPop();
		UINPCJiaoHu.Inst.ShowNPCTanChaPanel();
	}

	// Token: 0x060016DC RID: 5852 RVA: 0x0009BABF File Offset: 0x00099CBF
	public void FightTanCha()
	{
		UINPCJiaoHu.Inst.ShowNPCInfoPanel(UINPCJiaoHu.Inst.NowJiaoHuEnemy);
	}

	// Token: 0x060016DD RID: 5853 RVA: 0x0009BAD5 File Offset: 0x00099CD5
	public void OnJieShaBtnClick()
	{
		UINPCJiaoHu.Inst.IsJieShaClicked = true;
	}

	// Token: 0x060016DE RID: 5854 RVA: 0x0009BAE2 File Offset: 0x00099CE2
	public void OnLiKaiBtnClick()
	{
		UINPCJiaoHu.Inst.HideJiaoHuPop();
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	// Token: 0x060016DF RID: 5855 RVA: 0x0009BAFC File Offset: 0x00099CFC
	public void OnSeaNPCClick(int eventId)
	{
		bool flag = true;
		foreach (SeaAvatarObjBase seaAvatarObjBase in EndlessSeaMag.Inst.MonstarList)
		{
			if (seaAvatarObjBase._EventId == eventId)
			{
				seaAvatarObjBase.EventShiJian();
				flag = false;
				break;
			}
		}
		if (flag)
		{
			int num = (int)jsonData.instance.EndlessSeaNPCData[string.Concat(eventId)]["EventList"];
			Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + num));
		}
	}

	// Token: 0x060016E0 RID: 5856 RVA: 0x0009BBAC File Offset: 0x00099DAC
	public bool TryEscClose()
	{
		this.OnLiKaiBtnClick();
		return true;
	}

	// Token: 0x0400115D RID: 4445
	private UINPCData npc;

	// Token: 0x0400115E RID: 4446
	public Text NameText;

	// Token: 0x0400115F RID: 4447
	public PlayerSetRandomFace Face;

	// Token: 0x04001160 RID: 4448
	public Button JiaoTanBtn;

	// Token: 0x04001161 RID: 4449
	public Button LunDaoBtn;

	// Token: 0x04001162 RID: 4450
	public Button JiaoYiBtn;

	// Token: 0x04001163 RID: 4451
	public Button QieCuoBtn;

	// Token: 0x04001164 RID: 4452
	public Button ZengLiBtn;

	// Token: 0x04001165 RID: 4453
	public Button QingJiaoBtn;

	// Token: 0x04001166 RID: 4454
	public Button SuoQuBtn;

	// Token: 0x04001167 RID: 4455
	public Button ChaKanBtn;

	// Token: 0x04001168 RID: 4456
	public Button TanChaBtn;

	// Token: 0x04001169 RID: 4457
	public Button JieShaBtn;

	// Token: 0x0400116A RID: 4458
	public Button LiKaiBtn;
}
