using System;
using JSONClass;
using script.YarnEditor.Manager;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200038C RID: 908
public class UINPCJiaoHuPop : MonoBehaviour, IESCClose
{
	// Token: 0x0600197D RID: 6525 RVA: 0x00015E74 File Offset: 0x00014074
	private void Update()
	{
		this.AutoHide();
	}

	// Token: 0x0600197E RID: 6526 RVA: 0x000E2284 File Offset: 0x000E0484
	public bool CanShow()
	{
		return !UINPCJiaoHu.AllShouldHide && (!(PanelMamager.inst != null) || !(PanelMamager.inst.UISceneGameObject == null)) && (!(PanelMamager.inst != null) || PanelMamager.inst.nowPanel == PanelMamager.PanelType.空);
	}

	// Token: 0x0600197F RID: 6527 RVA: 0x00015E7C File Offset: 0x0001407C
	private void AutoHide()
	{
		if (!this.CanShow())
		{
			UINPCJiaoHu.Inst.HideJiaoHuPop();
		}
	}

	// Token: 0x06001980 RID: 6528 RVA: 0x000E2EC8 File Offset: 0x000E10C8
	public void RefreshUI()
	{
		ModResources.ClearSpriteCache();
		ModResources.ClearTextureCache();
		this.npc = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		this.npc.RefreshData();
		this.NameText.text = this.npc.Name;
		this.Face.SetNPCFace(this.npc.ID);
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

	// Token: 0x06001981 RID: 6529 RVA: 0x000E3044 File Offset: 0x000E1244
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

	// Token: 0x06001982 RID: 6530 RVA: 0x000E30D8 File Offset: 0x000E12D8
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

	// Token: 0x06001983 RID: 6531 RVA: 0x00015E90 File Offset: 0x00014090
	private void SetBtnNormal(Button btn)
	{
		UINPCJiaoHu.Inst.SetBtnNormalColor(btn.transform);
		btn.enabled = true;
	}

	// Token: 0x06001984 RID: 6532 RVA: 0x00015EA9 File Offset: 0x000140A9
	private void SetBtnGrey(Button btn)
	{
		UINPCJiaoHu.Inst.SetBtnGreyColor(btn.transform);
		btn.enabled = true;
	}

	// Token: 0x06001985 RID: 6533 RVA: 0x00015EC2 File Offset: 0x000140C2
	private void SetBtnClose(Button btn)
	{
		UINPCJiaoHu.Inst.SetBtnGreyColor(btn.transform);
		btn.enabled = false;
	}

	// Token: 0x06001986 RID: 6534 RVA: 0x000E316C File Offset: 0x000E136C
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

	// Token: 0x06001987 RID: 6535 RVA: 0x00015EDB File Offset: 0x000140DB
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

	// Token: 0x06001988 RID: 6536 RVA: 0x00015F11 File Offset: 0x00014111
	public void OnJiaoYiBtnClick()
	{
		UINPCJiaoHu.Inst.IsJiaoYiClicked = true;
	}

	// Token: 0x06001989 RID: 6537 RVA: 0x00015F1E File Offset: 0x0001411E
	public void OnQieCuoBtnClick()
	{
		if (this.npc.FavorLevel > 3)
		{
			UINPCJiaoHu.Inst.IsQieCuoClicked = true;
			return;
		}
		UIPopTip.Inst.Pop("好感度不足", PopTipIconType.叹号);
	}

	// Token: 0x0600198A RID: 6538 RVA: 0x000E32E0 File Offset: 0x000E14E0
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

	// Token: 0x0600198B RID: 6539 RVA: 0x00015F4A File Offset: 0x0001414A
	public void OnQingJiaoBtnClick()
	{
		if (this.npc.FavorLevel >= 3)
		{
			UINPCJiaoHu.Inst.ShowNPCQingJiaoPanel();
			return;
		}
		UIPopTip.Inst.Pop("好感度不足", PopTipIconType.叹号);
	}

	// Token: 0x0600198C RID: 6540 RVA: 0x00015F75 File Offset: 0x00014175
	public void OnSuoQuBtnClick()
	{
		UINPCJiaoHu.Inst.ShowNPCSuoQu();
		UINPCJiaoHu.Inst.HideJiaoHuPop();
	}

	// Token: 0x0600198D RID: 6541 RVA: 0x00015F8B File Offset: 0x0001418B
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

	// Token: 0x0600198E RID: 6542 RVA: 0x00015FC8 File Offset: 0x000141C8
	public void FightTanCha()
	{
		UINPCJiaoHu.Inst.ShowNPCInfoPanel(UINPCJiaoHu.Inst.NowJiaoHuEnemy);
	}

	// Token: 0x0600198F RID: 6543 RVA: 0x00015FDE File Offset: 0x000141DE
	public void OnJieShaBtnClick()
	{
		UINPCJiaoHu.Inst.IsJieShaClicked = true;
	}

	// Token: 0x06001990 RID: 6544 RVA: 0x00015FEB File Offset: 0x000141EB
	public void OnLiKaiBtnClick()
	{
		UINPCJiaoHu.Inst.HideJiaoHuPop();
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	// Token: 0x06001991 RID: 6545 RVA: 0x000E3330 File Offset: 0x000E1530
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

	// Token: 0x06001992 RID: 6546 RVA: 0x00016002 File Offset: 0x00014202
	public bool TryEscClose()
	{
		this.OnLiKaiBtnClick();
		return true;
	}

	// Token: 0x040014AE RID: 5294
	private UINPCData npc;

	// Token: 0x040014AF RID: 5295
	public Text NameText;

	// Token: 0x040014B0 RID: 5296
	public PlayerSetRandomFace Face;

	// Token: 0x040014B1 RID: 5297
	public Button JiaoTanBtn;

	// Token: 0x040014B2 RID: 5298
	public Button LunDaoBtn;

	// Token: 0x040014B3 RID: 5299
	public Button JiaoYiBtn;

	// Token: 0x040014B4 RID: 5300
	public Button QieCuoBtn;

	// Token: 0x040014B5 RID: 5301
	public Button ZengLiBtn;

	// Token: 0x040014B6 RID: 5302
	public Button QingJiaoBtn;

	// Token: 0x040014B7 RID: 5303
	public Button SuoQuBtn;

	// Token: 0x040014B8 RID: 5304
	public Button ChaKanBtn;

	// Token: 0x040014B9 RID: 5305
	public Button TanChaBtn;

	// Token: 0x040014BA RID: 5306
	public Button JieShaBtn;

	// Token: 0x040014BB RID: 5307
	public Button LiKaiBtn;
}
