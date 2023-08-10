using JSONClass;
using UnityEngine;
using UnityEngine.UI;
using script.YarnEditor.Manager;

public class UINPCJiaoHuPop : MonoBehaviour, IESCClose
{
	private UINPCData npc;

	public Text NameText;

	public PlayerSetRandomFace Face;

	public Button JiaoTanBtn;

	public Button LunDaoBtn;

	public Button JiaoYiBtn;

	public Button QieCuoBtn;

	public Button ZengLiBtn;

	public Button QingJiaoBtn;

	public Button SuoQuBtn;

	public Button ChaKanBtn;

	public Button TanChaBtn;

	public Button JieShaBtn;

	public Button LiKaiBtn;

	private void Update()
	{
		AutoHide();
	}

	public bool CanShow()
	{
		if (UINPCJiaoHu.AllShouldHide)
		{
			return false;
		}
		if ((Object)(object)PanelMamager.inst != (Object)null && (Object)(object)PanelMamager.inst.UISceneGameObject == (Object)null)
		{
			return false;
		}
		if ((Object)(object)PanelMamager.inst != (Object)null && PanelMamager.inst.nowPanel != PanelMamager.PanelType.空)
		{
			return false;
		}
		return true;
	}

	private void AutoHide()
	{
		if (!CanShow())
		{
			UINPCJiaoHu.Inst.HideJiaoHuPop();
		}
	}

	public void RefreshUI()
	{
		npc = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		npc.RefreshData();
		NameText.text = npc.Name;
		Face.SetNPCFace(npc.ID);
		if (GlobalValue.Get(1600) == 1)
		{
			DuJieCloseBtn();
			return;
		}
		OpenAllBtn();
		if (npc.FavorLevel >= 3)
		{
			SetBtnNormal(QingJiaoBtn);
		}
		else
		{
			SetBtnGrey(QingJiaoBtn);
		}
		if (npc.FavorLevel > 3)
		{
			SetBtnNormal(LunDaoBtn);
			SetBtnNormal(QieCuoBtn);
		}
		else
		{
			SetBtnGrey(LunDaoBtn);
			SetBtnGrey(QieCuoBtn);
		}
		if (npc.FavorLevel != 3 || npc.IsKnowPlayer)
		{
			SetBtnNormal(ZengLiBtn);
		}
		else
		{
			SetBtnGrey(ZengLiBtn);
		}
		if (npc.FavorLevel >= 5)
		{
			((Component)ChaKanBtn).gameObject.SetActive(true);
			((Component)TanChaBtn).gameObject.SetActive(false);
			SetBtnNormal(ChaKanBtn);
		}
		else
		{
			((Component)ChaKanBtn).gameObject.SetActive(false);
			((Component)TanChaBtn).gameObject.SetActive(true);
			((Behaviour)TanChaBtn).enabled = true;
			UINPCJiaoHu.Inst.SetBtnDangerColor(((Component)TanChaBtn).transform);
		}
	}

	private void OpenAllBtn()
	{
		SetBtnNormal(JiaoTanBtn);
		SetBtnNormal(LiKaiBtn);
		SetBtnNormal(QieCuoBtn);
		SetBtnNormal(LunDaoBtn);
		SetBtnNormal(JiaoYiBtn);
		SetBtnNormal(ZengLiBtn);
		SetBtnNormal(QingJiaoBtn);
		SetDangerBtnNormal(SuoQuBtn);
		SetBtnNormal(ChaKanBtn);
		SetBtnNormal(TanChaBtn);
		SetDangerBtnNormal(JieShaBtn);
	}

	private void DuJieCloseBtn()
	{
		SetBtnClose(LunDaoBtn);
		SetBtnClose(JiaoYiBtn);
		SetBtnClose(QieCuoBtn);
		SetBtnClose(ZengLiBtn);
		SetBtnClose(QingJiaoBtn);
		SetBtnClose(SuoQuBtn);
		SetBtnClose(ChaKanBtn);
		SetBtnClose(TanChaBtn);
		SetBtnClose(JieShaBtn);
	}

	private void SpecialNPC1()
	{
		SetBtnNormal(JiaoTanBtn);
		SetBtnNormal(LiKaiBtn);
		SetBtnClose(QieCuoBtn);
		SetBtnClose(LunDaoBtn);
		SetBtnClose(JiaoYiBtn);
		SetBtnClose(ZengLiBtn);
		SetBtnClose(QingJiaoBtn);
		SetBtnClose(SuoQuBtn);
		SetBtnClose(ChaKanBtn);
		SetBtnClose(TanChaBtn);
		SetBtnClose(JieShaBtn);
	}

	private void SpecialNPC2()
	{
		SetBtnNormal(JiaoTanBtn);
		SetBtnNormal(LiKaiBtn);
		SetBtnNormal(JiaoYiBtn);
		SetBtnClose(QieCuoBtn);
		SetBtnClose(LunDaoBtn);
		SetBtnClose(ZengLiBtn);
		SetBtnClose(QingJiaoBtn);
		SetBtnClose(SuoQuBtn);
		SetBtnClose(ChaKanBtn);
		SetBtnClose(TanChaBtn);
		SetBtnClose(JieShaBtn);
	}

	private void SetBtnNormal(Button btn)
	{
		UINPCJiaoHu.Inst.SetBtnNormalColor(((Component)btn).transform);
		((Behaviour)btn).enabled = true;
	}

	private void SetDangerBtnNormal(Button btn)
	{
		UINPCJiaoHu.Inst.SetBtnDangerColor(((Component)btn).transform);
		((Behaviour)btn).enabled = true;
	}

	private void SetBtnGrey(Button btn)
	{
		UINPCJiaoHu.Inst.SetBtnGreyColor(((Component)btn).transform);
		((Behaviour)btn).enabled = true;
	}

	private void SetBtnClose(Button btn)
	{
		UINPCJiaoHu.Inst.SetBtnGreyColor(((Component)btn).transform);
		((Behaviour)btn).enabled = false;
	}

	public void OnJiaoTanBtnClick()
	{
		if (StoryManager.Inst.CheckTrigger(npc.ID))
		{
			UINPCJiaoHu.Inst.HideJiaoHuPop();
		}
		else if ((npc.ActionID == 101 || npc.ActionID == 102 || npc.ActionID == 103) && SceneNameJsonData.DataDict.ContainsKey(SceneEx.NowSceneName) && SceneNameJsonData.DataDict[SceneEx.NowSceneName].MapType == 3)
		{
			if (npc.IsZhongYaoNPC)
			{
				if (!NPCEx.OpenAction101102103GuDingTalk(npc.ZhongYaoNPCID, npc.ActionID))
				{
					NPCEx.OpenTalk(npc.ActionID + 3902);
				}
			}
			else
			{
				NPCEx.OpenTalk(npc.ActionID + 3902);
			}
			UINPCJiaoHu.Inst.HideJiaoHuPop();
		}
		else if (npc.IsSeaNPC)
		{
			OnSeaNPCClick(npc.SeaEventID);
			UINPCJiaoHu.Inst.HideJiaoHuPop();
		}
		else if (npc.IsZhongYaoNPC && UINPCData.ThreeSceneZhongYaoNPCTalkCache.ContainsKey(npc.ZhongYaoNPCID))
		{
			UINPCData.ThreeSceneZhongYaoNPCTalkCache[npc.ZhongYaoNPCID].Invoke();
			UINPCJiaoHu.Inst.HideJiaoHuPop();
		}
		else
		{
			UINPCJiaoHu.Inst.IsLiaoTianClicked = true;
			UINPCJiaoHu.Inst.HideJiaoHuPop();
		}
	}

	public void OnLunDaoBtnClick()
	{
		if (npc.FavorLevel > 3)
		{
			UINPCJiaoHu.Inst.IsLunDaoClicked = true;
			UINPCJiaoHu.Inst.HideJiaoHuPop();
		}
		else
		{
			UIPopTip.Inst.Pop("好感度不足");
		}
	}

	public void OnJiaoYiBtnClick()
	{
		UINPCJiaoHu.Inst.IsJiaoYiClicked = true;
	}

	public void OnQieCuoBtnClick()
	{
		if (npc.FavorLevel > 3)
		{
			UINPCJiaoHu.Inst.IsQieCuoClicked = true;
		}
		else
		{
			UIPopTip.Inst.Pop("好感度不足");
		}
	}

	public void OnZengLiBtnClick()
	{
		if (npc.FavorLevel != 3 || npc.IsKnowPlayer)
		{
			UINPCJiaoHu.Inst.ShowNPCZengLi();
			UINPCJiaoHu.Inst.HideJiaoHuPop();
		}
		else
		{
			UIPopTip.Inst.Pop("对方还不认识你");
		}
	}

	public void OnQingJiaoBtnClick()
	{
		if (npc.FavorLevel >= 3)
		{
			UINPCJiaoHu.Inst.ShowNPCQingJiaoPanel();
		}
		else
		{
			UIPopTip.Inst.Pop("好感度不足");
		}
	}

	public void OnSuoQuBtnClick()
	{
		UINPCJiaoHu.Inst.ShowNPCSuoQu();
		UINPCJiaoHu.Inst.HideJiaoHuPop();
	}

	public void OnTanChaBtnClick()
	{
		if (npc.FavorLevel >= 5 || npc.IsTanChaUnlock)
		{
			UINPCJiaoHu.Inst.ShowNPCInfoPanel();
			return;
		}
		UINPCJiaoHu.Inst.HideJiaoHuPop();
		UINPCJiaoHu.Inst.ShowNPCTanChaPanel();
	}

	public void FightTanCha()
	{
		UINPCJiaoHu.Inst.ShowNPCInfoPanel(UINPCJiaoHu.Inst.NowJiaoHuEnemy);
	}

	public void OnJieShaBtnClick()
	{
		UINPCJiaoHu.Inst.IsJieShaClicked = true;
	}

	public void OnLiKaiBtnClick()
	{
		UINPCJiaoHu.Inst.HideJiaoHuPop();
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	public void OnSeaNPCClick(int eventId)
	{
		bool flag = true;
		foreach (SeaAvatarObjBase monstar in EndlessSeaMag.Inst.MonstarList)
		{
			if (monstar._EventId == eventId)
			{
				monstar.EventShiJian();
				flag = false;
				break;
			}
		}
		if (flag)
		{
			int num = (int)jsonData.instance.EndlessSeaNPCData[string.Concat(eventId)][(object)"EventList"];
			Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + num));
		}
	}

	public bool TryEscClose()
	{
		OnLiKaiBtnClick();
		return true;
	}
}
