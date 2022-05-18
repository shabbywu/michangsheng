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

// Token: 0x020003F4 RID: 1012
public class FpUIMag : MonoBehaviour
{
	// Token: 0x06001B7A RID: 7034 RVA: 0x000F6814 File Offset: 0x000F4A14
	private void Awake()
	{
		FpUIMag.inst = this;
		base.transform.SetParent(NewUICanvas.Inst.gameObject.transform);
		base.transform.SetAsFirstSibling();
		base.transform.localPosition = Vector3.zero;
		base.transform.localScale = Vector3.one;
	}

	// Token: 0x06001B7B RID: 7035 RVA: 0x000F686C File Offset: 0x000F4A6C
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
		UINPCJiaoHu.AllShouldHide = false;
	}

	// Token: 0x06001B7C RID: 7036 RVA: 0x000F69FC File Offset: 0x000F4BFC
	private void StartFight()
	{
		Tools.instance.FinalScene = SceneManager.GetActiveScene().name;
		Tools.instance.loadOtherScenes("YSNewFight");
		UINPCJiaoHu.AllShouldHide = false;
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06001B7D RID: 7037 RVA: 0x000F6A40 File Offset: 0x000F4C40
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

	// Token: 0x06001B7E RID: 7038 RVA: 0x000171A8 File Offset: 0x000153A8
	private void OpenBag()
	{
		TabUIMag.OpenTab2(2);
	}

	// Token: 0x06001B7F RID: 7039 RVA: 0x000F6AAC File Offset: 0x000F4CAC
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

	// Token: 0x06001B80 RID: 7040 RVA: 0x000171B0 File Offset: 0x000153B0
	public void Close()
	{
		UI_Manager.inst.checkTool.Init();
		UINPCJiaoHu.AllShouldHide = false;
		Object.Destroy(base.gameObject);
	}

	// Token: 0x0400172F RID: 5935
	public static FpUIMag inst;

	// Token: 0x04001730 RID: 5936
	[SerializeField]
	private PlayerSetRandomFace playerFace;

	// Token: 0x04001731 RID: 5937
	[SerializeField]
	private Text playerName;

	// Token: 0x04001732 RID: 5938
	[SerializeField]
	private PlayerSetRandomFace npcFace;

	// Token: 0x04001733 RID: 5939
	[SerializeField]
	private Text npcName;

	// Token: 0x04001734 RID: 5940
	public int npcId;

	// Token: 0x04001735 RID: 5941
	public FpBtn startFightBtn;

	// Token: 0x04001736 RID: 5942
	public FpBtn tanCai1Btn;

	// Token: 0x04001737 RID: 5943
	public FpBtn tanCai2Btn;

	// Token: 0x04001738 RID: 5944
	public FpBtn fighthPrepareBtn;

	// Token: 0x04001739 RID: 5945
	public FpBtn taoPaoBtn;

	// Token: 0x0400173A RID: 5946
	private readonly List<StartFight.FightEnumType> TouXiangTypes = new List<StartFight.FightEnumType>
	{
		Fungus.StartFight.FightEnumType.LeiTai,
		Fungus.StartFight.FightEnumType.QieCuo,
		Fungus.StartFight.FightEnumType.DouFa,
		Fungus.StartFight.FightEnumType.无装备无丹药擂台
	};
}
