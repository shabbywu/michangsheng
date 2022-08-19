using System;
using System.Collections.Generic;
using Fungus;
using GUIPackage;
using KBEngine;
using Tab;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x020002B8 RID: 696
public class FpUIMag : MonoBehaviour
{
	// Token: 0x06001886 RID: 6278 RVA: 0x000B00A4 File Offset: 0x000AE2A4
	private void Awake()
	{
		Tools.instance.FinalScene = SceneManager.GetActiveScene().name;
		FpUIMag.inst = this;
		base.transform.SetParent(NewUICanvas.Inst.gameObject.transform);
		base.transform.SetAsFirstSibling();
		base.transform.localPosition = Vector3.zero;
		base.transform.localScale = Vector3.one;
	}

	// Token: 0x06001887 RID: 6279 RVA: 0x000B0114 File Offset: 0x000AE314
	public void Init()
	{
		this.playerFace.SetNPCFace(1);
		this.playerName.text = Tools.instance.getPlayer().name;
		this.npcId = Tools.instance.MonstarID;
		this.npcFace.SetNPCFace(this.npcId);
		this.npcName.text = jsonData.instance.AvatarRandomJsonData[this.npcId.ToString()]["Name"].Str;
		int i = jsonData.instance.AvatarRandomJsonData[this.npcId.ToString()]["HaoGanDu"].I;
		this.startFightBtn.mouseUpEvent.AddListener(new UnityAction(this.StartFight));
		if (i >= 50)
		{
			this.tanCai1Btn.gameObject.SetActive(false);
			this.tanCai2Btn.gameObject.SetActive(true);
			this.tanCai2Btn.mouseUpEvent.AddListener(new UnityAction(this.TanCai));
		}
		else
		{
			this.tanCai1Btn.gameObject.SetActive(true);
			this.tanCai2Btn.gameObject.SetActive(false);
			this.tanCai1Btn.mouseUpEvent.AddListener(new UnityAction(this.TanCai));
		}
		this.fighthPrepareBtn.mouseUpEvent.AddListener(new UnityAction(this.OpenBag));
		this.taoPaoBtn.mouseUpEvent.AddListener(new UnityAction(this.PlayRunAway));
		string tips = this.GetTips();
		if (tips == "无")
		{
			this.DisableSkipFight.gameObject.SetActive(false);
			this.SkipFight.gameObject.SetActive(true);
			this.SkipFight.mouseUpEvent.AddListener(delegate()
			{
				ResManager.inst.LoadPrefab("VictoryPanel").Inst(null);
				this.Close();
			});
		}
		else
		{
			this.DisableSkipFight.gameObject.SetActive(true);
			this.SkipFight.gameObject.SetActive(false);
			this.TipsText.text = tips;
		}
		UINPCJiaoHu.AllShouldHide = false;
	}

	// Token: 0x06001888 RID: 6280 RVA: 0x000B0324 File Offset: 0x000AE524
	private void StartFight()
	{
		Tools.instance.FinalScene = SceneManager.GetActiveScene().name;
		Tools.instance.loadOtherScenes("YSNewFight");
		UINPCJiaoHu.AllShouldHide = false;
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06001889 RID: 6281 RVA: 0x000B0368 File Offset: 0x000AE568
	private void TanCai()
	{
		UINPCData uinpcdata = new UINPCData(this.npcId, false);
		if (this.npcId < 20000)
		{
			uinpcdata.RefreshOldNpcData();
		}
		else
		{
			uinpcdata.RefreshData();
		}
		uinpcdata.IsFight = true;
		UINPCJiaoHu.Inst.NowJiaoHuEnemy = uinpcdata;
		UINPCJiaoHu.Inst.JiaoHuPop.FightTanCha();
		UINPCJiaoHu.Inst.InfoPanel.TabGroup.HideTab();
	}

	// Token: 0x0600188A RID: 6282 RVA: 0x000B03D2 File Offset: 0x000AE5D2
	private void OpenBag()
	{
		TabUIMag.OpenTab2(2);
	}

	// Token: 0x0600188B RID: 6283 RVA: 0x000B03DC File Offset: 0x000AE5DC
	private void PlayRunAway()
	{
		try
		{
			if (Tools.instance.CanFpRun == 0)
			{
				UIPopTip.Inst.Pop("此战斗战前无法逃跑", PopTipIconType.叹号);
			}
			else if (!Tools.instance.monstarMag.CanRunAway())
			{
				string str = Tools.getStr("cannotRunAway" + Tools.instance.monstarMag.CanNotRunAwayEvent());
				UIPopTip.Inst.Pop(str, PopTipIconType.叹号);
			}
			else
			{
				Avatar avatar = Tools.instance.getPlayer();
				if (this.TouXiangTypes.Contains(Tools.instance.monstarMag.FightType))
				{
					USelectBox.Show("是否确认投降?", delegate
					{
						GlobalValue.SetTalk(1, 4, "FpUIMag.PlayRunAway");
						Tools.instance.FinalScene = SceneManager.GetActiveScene().name;
						Tools.instance.AutoSeatSeaRunAway(false);
						if (Tools.instance.getPlayer().NowFuBen == "" || Tools.instance.FinalScene.Contains("Sea"))
						{
							Tools.instance.CanShowFightUI = 1;
						}
						if (GlobalValue.GetTalk(0, "FpUIMag.PlayRunAway") > 0 || avatar.fubenContorl.isInFuBen() || Tools.instance.FinalScene.Contains("Sea"))
						{
							UINPCJiaoHu.AllShouldHide = false;
							Object.Destroy(this.gameObject);
							Tools.instance.loadMapScenes(Tools.instance.FinalScene, true);
							Tools.instance.monstarMag.ClearBuff();
							return;
						}
						this.Close();
					}, null);
				}
				else if (avatar.dunSu - (int)jsonData.instance.AvatarJsonData[string.Concat(Tools.instance.MonstarID)]["dunSu"].n > 0)
				{
					USelectBox.Show("是否确认遁走？", delegate
					{
						GlobalValue.SetTalk(1, 4, "FpUIMag.PlayRunAway");
						Tools.instance.FinalScene = SceneManager.GetActiveScene().name;
						Tools.instance.AutoSeatSeaRunAway(false);
						if (Tools.instance.getPlayer().NowFuBen == "" || Tools.instance.FinalScene.Contains("Sea"))
						{
							Tools.instance.CanShowFightUI = 1;
						}
						if (GlobalValue.GetTalk(0, "FpUIMag.PlayRunAway") > 0 || avatar.fubenContorl.isInFuBen() || Tools.instance.FinalScene.Contains("Sea"))
						{
							UINPCJiaoHu.AllShouldHide = false;
							Object.Destroy(this.gameObject);
							Tools.instance.loadMapScenes(Tools.instance.FinalScene, true);
							Tools.instance.monstarMag.ClearBuff();
							return;
						}
						this.Close();
					}, null);
				}
				else
				{
					UIPopTip.Inst.Pop(Tools.getStr("cannotRunAway0"), PopTipIconType.叹号);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogError(ex);
		}
	}

	// Token: 0x0600188C RID: 6284 RVA: 0x000B0538 File Offset: 0x000AE738
	public void Close()
	{
		UI_Manager.inst.checkTool.Init();
		UINPCJiaoHu.AllShouldHide = false;
		Object.Destroy(base.gameObject);
	}

	// Token: 0x0600188D RID: 6285 RVA: 0x000B055C File Offset: 0x000AE75C
	private string GetTips()
	{
		if (Tools.instance.monstarMag.FightType != Fungus.StartFight.FightEnumType.Normal)
		{
			return "该战斗类型无法跳过";
		}
		if ((int)PlayerEx.Player.level <= jsonData.instance.AvatarJsonData[this.npcId.ToString()]["Level"].I)
		{
			return "境界未高于此对手";
		}
		if (!PlayerEx.Player.HasDefeatNpcList.Contains(this.npcId))
		{
			return "未曾战胜过此对手";
		}
		return "无";
	}

	// Token: 0x04001389 RID: 5001
	public static FpUIMag inst;

	// Token: 0x0400138A RID: 5002
	[SerializeField]
	private PlayerSetRandomFace playerFace;

	// Token: 0x0400138B RID: 5003
	[SerializeField]
	private Text playerName;

	// Token: 0x0400138C RID: 5004
	[SerializeField]
	private PlayerSetRandomFace npcFace;

	// Token: 0x0400138D RID: 5005
	[SerializeField]
	private Text npcName;

	// Token: 0x0400138E RID: 5006
	public int npcId;

	// Token: 0x0400138F RID: 5007
	public FpBtn startFightBtn;

	// Token: 0x04001390 RID: 5008
	public FpBtn tanCai1Btn;

	// Token: 0x04001391 RID: 5009
	public FpBtn SkipFight;

	// Token: 0x04001392 RID: 5010
	public FpBtn DisableSkipFight;

	// Token: 0x04001393 RID: 5011
	public Text TipsText;

	// Token: 0x04001394 RID: 5012
	public FpBtn tanCai2Btn;

	// Token: 0x04001395 RID: 5013
	public FpBtn fighthPrepareBtn;

	// Token: 0x04001396 RID: 5014
	public FpBtn taoPaoBtn;

	// Token: 0x04001397 RID: 5015
	private readonly List<StartFight.FightEnumType> TouXiangTypes = new List<StartFight.FightEnumType>
	{
		Fungus.StartFight.FightEnumType.LeiTai,
		Fungus.StartFight.FightEnumType.QieCuo,
		Fungus.StartFight.FightEnumType.DouFa,
		Fungus.StartFight.FightEnumType.无装备无丹药擂台
	};
}
