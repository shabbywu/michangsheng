using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000272 RID: 626
public class UINPCJiaoHu : MonoBehaviour
{
	// Token: 0x1700023A RID: 570
	// (get) Token: 0x060016A9 RID: 5801 RVA: 0x0009A6D8 File Offset: 0x000988D8
	[HideInInspector]
	public bool NowIsJiaoHu
	{
		get
		{
			return this.JiaoHuPop.gameObject.activeInHierarchy || this.LiaoTian.gameObject.activeInHierarchy || this.QingJiao.gameObject.activeInHierarchy || this.TanCha.gameObject.activeInHierarchy || this.InfoPanel.gameObject.activeInHierarchy || this.CommonMask.gameObject.activeInHierarchy || (this.NPCList.gameObject.activeInHierarchy && this.NPCList.IsMouseInUI) || this.ZengLi.gameObject.activeInHierarchy;
		}
	}

	// Token: 0x1700023B RID: 571
	// (get) Token: 0x060016AA RID: 5802 RVA: 0x0009A794 File Offset: 0x00098994
	public bool NowIsJiaoHu2
	{
		get
		{
			return this.JiaoHuPop.gameObject.activeInHierarchy || this.LiaoTian.gameObject.activeInHierarchy || this.QingJiao.gameObject.activeInHierarchy || this.TanCha.gameObject.activeInHierarchy || this.InfoPanel.gameObject.activeInHierarchy || this.CommonMask.gameObject.activeInHierarchy || this.ZengLi.gameObject.activeInHierarchy;
		}
	}

	// Token: 0x060016AB RID: 5803 RVA: 0x0009A82E File Offset: 0x00098A2E
	private void Awake()
	{
		UINPCJiaoHu.Inst = this;
		SceneManager.activeSceneChanged += delegate(Scene s1, Scene s2)
		{
			UINPCLeftList.ShoudHide = false;
		};
		Object.Instantiate<GameObject>(this.NPCTalkPrefab, base.transform);
	}

	// Token: 0x060016AC RID: 5804 RVA: 0x0009A86C File Offset: 0x00098A6C
	private void Update()
	{
		if (NpcJieSuanManager.inst == null)
		{
			return;
		}
		if (Tools.instance.getPlayer() != null)
		{
			this.lastTime = this.nowTime;
			this.nowTime = Tools.instance.getPlayer().worldTimeMag.getNowTime();
			if (this.lastTime != this.nowTime)
			{
				NpcJieSuanManager.inst.isUpDateNpcList = true;
			}
		}
		if (NpcJieSuanManager.inst.isUpDateNpcList)
		{
			this.RefreshNowMapNPC();
		}
		if (!PanelMamager.CanOpenOrClose && !this.NowIsJiaoHu)
		{
			PanelMamager.CanOpenOrClose = true;
		}
		this.AutoShowNPCList();
	}

	// Token: 0x060016AD RID: 5805 RVA: 0x0009A904 File Offset: 0x00098B04
	public void RefreshNowMapNPC()
	{
		string name = SceneManager.GetActiveScene().name;
		Avatar player = Tools.instance.getPlayer();
		if (player == null)
		{
			return;
		}
		NPCMap npcMap = NpcJieSuanManager.inst.npcMap;
		this.TestNPCMap = NpcJieSuanManager.inst.npcMap;
		this.NPCIDList.Clear();
		this.SeaNPCIDList.Clear();
		this.SeaNPCUUIDList.Clear();
		this.SeaNPCEventIDList.Clear();
		this.npcjsonlist.Clear();
		if (npcMap != null)
		{
			if (name == "AllMaps")
			{
				int nowMapIndex = player.NowMapIndex;
				if (npcMap.bigMapNPCDictionary == null || !npcMap.bigMapNPCDictionary.ContainsKey(nowMapIndex) || npcMap.bigMapNPCDictionary[nowMapIndex].Count <= 0)
				{
					goto IL_409;
				}
				using (List<int>.Enumerator enumerator = npcMap.bigMapNPCDictionary[nowMapIndex].GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int num = enumerator.Current;
						this.NPCIDList.Add(num);
						this.npcjsonlist.Add(num.NPCJson());
					}
					goto IL_409;
				}
			}
			if (name.StartsWith("F"))
			{
				int nowIndex = player.fubenContorl[Tools.getScreenName()].NowIndex;
				if (npcMap.fuBenNPCDictionary == null || !npcMap.fuBenNPCDictionary.ContainsKey(name) || !npcMap.fuBenNPCDictionary[name].ContainsKey(nowIndex) || npcMap.fuBenNPCDictionary[name][nowIndex].Count <= 0)
				{
					goto IL_409;
				}
				using (List<int>.Enumerator enumerator = npcMap.fuBenNPCDictionary[name][nowIndex].GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int num2 = enumerator.Current;
						this.NPCIDList.Add(num2);
						this.npcjsonlist.Add(num2.NPCJson());
					}
					goto IL_409;
				}
			}
			if (name.StartsWith("Sea"))
			{
				int num3 = GlobalValue.Get(1000, "UINPCJiaoHu.RefreshNowMapNPC 海上百里奇特殊处理");
				if (num3 > 0)
				{
					int item = num3;
					int num4 = NPCEx.NPCIDToNew(615);
					this.SeaNPCIDList.Add(num4);
					this.SeaNPCUUIDList.Add("1f3c6041-c68f-4ab3-ae19-f66f541e3209");
					this.SeaNPCEventIDList.Add(item);
					this.npcjsonlist.Add(num4.NPCJson());
				}
				this.seaList = EndlessSeaMag.Inst.MonstarList;
				using (List<SeaAvatarObjBase>.Enumerator enumerator2 = EndlessSeaMag.Inst.MonstarList.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						SeaAvatarObjBase seaAvatarObjBase = enumerator2.Current;
						if (seaAvatarObjBase.IsCollect)
						{
							int seaNPCIDByEventID = NPCEx.GetSeaNPCIDByEventID(seaAvatarObjBase._EventId);
							this.SeaNPCIDList.Add(seaNPCIDByEventID);
							this.SeaNPCUUIDList.Add(seaAvatarObjBase.UUID);
							this.SeaNPCEventIDList.Add(seaAvatarObjBase._EventId);
							this.npcjsonlist.Add(seaNPCIDByEventID.NPCJson());
						}
					}
					goto IL_409;
				}
			}
			if (name.StartsWith("S"))
			{
				bool flag = false;
				if (name == "S101")
				{
					flag = true;
				}
				if (npcMap.threeSenceNPCDictionary != null && npcMap.threeSenceNPCDictionary.ContainsKey(name) && npcMap.threeSenceNPCDictionary[name].Count > 0)
				{
					foreach (int num5 in npcMap.threeSenceNPCDictionary[name])
					{
						if (flag)
						{
							if (jsonData.instance.AvatarJsonData[num5.ToString()].TryGetField("DongFuId").I == DongFuManager.NowDongFuID)
							{
								this.NPCIDList.Add(num5);
								this.npcjsonlist.Add(num5.NPCJson());
							}
						}
						else
						{
							this.NPCIDList.Add(num5);
							this.npcjsonlist.Add(num5.NPCJson());
						}
					}
				}
			}
		}
		IL_409:
		this.NPCList.needRefresh = true;
		NpcJieSuanManager.inst.isUpDateNpcList = false;
	}

	// Token: 0x060016AE RID: 5806 RVA: 0x0009AD68 File Offset: 0x00098F68
	public void ShowNowJiaoHuJSON()
	{
		this.NowNPCJson = this.NowJiaoHuNPC.json.ToString();
	}

	// Token: 0x060016AF RID: 5807 RVA: 0x0009AD80 File Offset: 0x00098F80
	private void AutoShowNPCList()
	{
		if (!this.NPCList.gameObject.activeInHierarchy && this.NPCList.CanShow())
		{
			this.NPCList.gameObject.SetActive(true);
			this.NPCList.needRefresh = true;
		}
	}

	// Token: 0x060016B0 RID: 5808 RVA: 0x0009ADBE File Offset: 0x00098FBE
	public void ShowNPCList()
	{
		this.NPCList.gameObject.SetActive(true);
	}

	// Token: 0x060016B1 RID: 5809 RVA: 0x0009ADD1 File Offset: 0x00098FD1
	public void HideNPCList()
	{
		this.NPCList.gameObject.SetActive(false);
	}

	// Token: 0x060016B2 RID: 5810 RVA: 0x0009ADE4 File Offset: 0x00098FE4
	public void ShowJiaoHuPop()
	{
		if (FpUIMag.inst != null)
		{
			return;
		}
		this.JiaoHuPop.gameObject.SetActive(true);
		PanelMamager.CanOpenOrClose = false;
		this.JiaoHuPop.RefreshUI();
		if (UINPCSVItem.NowSelectedUINPCSVItem != null)
		{
			UINPCSVItem.NowSelectedUINPCSVItem.Selected.SetActive(true);
			UINPCSVItem.NowSelectedUINPCSVItem.BG.sprite = UINPCSVItem.NowSelectedUINPCSVItem.SelectedBG;
		}
		ESCCloseManager.Inst.RegisterClose(this.JiaoHuPop);
	}

	// Token: 0x060016B3 RID: 5811 RVA: 0x0009AE68 File Offset: 0x00099068
	public void HideJiaoHuPop()
	{
		this.JiaoHuPop.gameObject.SetActive(false);
		PanelMamager.CanOpenOrClose = true;
		if (UINPCSVItem.NowSelectedUINPCSVItem != null)
		{
			UINPCSVItem.NowSelectedUINPCSVItem.Selected.SetActive(false);
			UINPCSVItem.NowSelectedUINPCSVItem.BG.sprite = UINPCSVItem.NowSelectedUINPCSVItem.NormalBG;
		}
	}

	// Token: 0x060016B4 RID: 5812 RVA: 0x0009AEC2 File Offset: 0x000990C2
	public void ShowNPCInfoPanel(UINPCData npc = null)
	{
		this.InfoPanel.gameObject.SetActive(true);
		this.CommonMask.gameObject.SetActive(true);
		this.InfoPanel.RefreshUI(npc);
		ESCCloseManager.Inst.RegisterClose(this.InfoPanel);
	}

	// Token: 0x060016B5 RID: 5813 RVA: 0x0009AF04 File Offset: 0x00099104
	public void HideNPCInfoPanel()
	{
		this.InfoPanel.TabGroup.UnHideTab();
		this.InfoPanel.gameObject.SetActive(false);
		this.CommonMask.gameObject.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this.InfoPanel);
	}

	// Token: 0x060016B6 RID: 5814 RVA: 0x0009AF53 File Offset: 0x00099153
	public void ShowNPCTanChaPanel()
	{
		this.TanCha.gameObject.SetActive(true);
		this.CommonMask.gameObject.SetActive(true);
		this.TanCha.RefreshUI();
	}

	// Token: 0x060016B7 RID: 5815 RVA: 0x0009AF82 File Offset: 0x00099182
	public void HideNPCTanChaPanel()
	{
		this.TanCha.gameObject.SetActive(false);
		this.CommonMask.gameObject.SetActive(false);
	}

	// Token: 0x060016B8 RID: 5816 RVA: 0x0009AFA6 File Offset: 0x000991A6
	public void ShowNPCQingJiaoPanel()
	{
		this.QingJiao.gameObject.SetActive(true);
		this.CommonMask.SetActive(true);
		this.QingJiao.RefreshUI();
		ESCCloseManager.Inst.RegisterClose(this.QingJiao);
	}

	// Token: 0x060016B9 RID: 5817 RVA: 0x0009AFE0 File Offset: 0x000991E0
	public void HideNPCQingJiaoPanel()
	{
		this.QingJiao.gameObject.SetActive(false);
		this.CommonMask.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this.QingJiao);
	}

	// Token: 0x060016BA RID: 5818 RVA: 0x0009B00F File Offset: 0x0009920F
	public void ShowNPCZengLi()
	{
		this.ZengLi.gameObject.SetActive(true);
		this.CommonMask.SetActive(true);
		this.ZengLi.RefreshUI();
		ESCCloseManager.Inst.RegisterClose(this.ZengLi);
	}

	// Token: 0x060016BB RID: 5819 RVA: 0x0009B049 File Offset: 0x00099249
	public void HideNPCZengLi()
	{
		this.ZengLi.gameObject.SetActive(false);
		this.CommonMask.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this.ZengLi);
	}

	// Token: 0x060016BC RID: 5820 RVA: 0x0009B078 File Offset: 0x00099278
	public void ShowNPCSuoQu()
	{
		this.SuoQu.gameObject.SetActive(true);
		this.CommonMask.SetActive(true);
		this.SuoQu.RefreshUI();
		ESCCloseManager.Inst.RegisterClose(this.SuoQu);
	}

	// Token: 0x060016BD RID: 5821 RVA: 0x0009B0B2 File Offset: 0x000992B2
	public void HideNPCSuoQu()
	{
		this.SuoQu.gameObject.SetActive(false);
		this.CommonMask.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this.SuoQu);
	}

	// Token: 0x060016BE RID: 5822 RVA: 0x0009B0E1 File Offset: 0x000992E1
	public void ShowNPCShuangXiuSelect()
	{
		this.HideJiaoHuPop();
		this.ShuangXiuSelect.gameObject.SetActive(true);
		this.CommonMask.SetActive(true);
		this.ShuangXiuSelect.RefreshUI();
		ESCCloseManager.Inst.RegisterClose(this.ShuangXiuSelect);
	}

	// Token: 0x060016BF RID: 5823 RVA: 0x0009B121 File Offset: 0x00099321
	public void HideNPCShuangXiuSelect()
	{
		this.ShuangXiuSelect.gameObject.SetActive(false);
		this.CommonMask.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this.ShuangXiuSelect);
	}

	// Token: 0x060016C0 RID: 5824 RVA: 0x0009B150 File Offset: 0x00099350
	public void ShuangXiuTest()
	{
		for (int i = 2; i <= 6; i++)
		{
			PlayerEx.StudyShuangXiuSkill(i);
		}
	}

	// Token: 0x060016C1 RID: 5825 RVA: 0x0009B16F File Offset: 0x0009936F
	public void ShowNPCShuangXiuAnim()
	{
		this.ShuangXiuAnim.gameObject.SetActive(true);
		this.CommonMask.SetActive(true);
		this.ShuangXiuAnim.RefreshUI();
	}

	// Token: 0x060016C2 RID: 5826 RVA: 0x0009B199 File Offset: 0x00099399
	public void HideNPCShuangXiuAnim()
	{
		this.ShuangXiuAnim.gameObject.SetActive(false);
		this.CommonMask.SetActive(false);
	}

	// Token: 0x060016C3 RID: 5827 RVA: 0x0009B1B8 File Offset: 0x000993B8
	public void SetBtnNormalColor(Transform btnTransform)
	{
		Image[] componentsInChildren = btnTransform.GetComponentsInChildren<Image>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].material = null;
		}
		Text[] componentsInChildren2 = btnTransform.GetComponentsInChildren<Text>();
		for (int i = 0; i < componentsInChildren2.Length; i++)
		{
			componentsInChildren2[i].color = UINPCJiaoHu.NormalColor;
		}
	}

	// Token: 0x060016C4 RID: 5828 RVA: 0x0009B208 File Offset: 0x00099408
	public void SetBtnDangerColor(Transform btnTransform)
	{
		Image[] componentsInChildren = btnTransform.GetComponentsInChildren<Image>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].material = null;
		}
		Text[] componentsInChildren2 = btnTransform.GetComponentsInChildren<Text>();
		for (int i = 0; i < componentsInChildren2.Length; i++)
		{
			componentsInChildren2[i].color = UINPCJiaoHu.DangerColor;
		}
	}

	// Token: 0x060016C5 RID: 5829 RVA: 0x0009B258 File Offset: 0x00099458
	public void SetBtnGreyColor(Transform btnTransform)
	{
		Image[] componentsInChildren = btnTransform.GetComponentsInChildren<Image>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].material = this.GreyMat;
		}
		Text[] componentsInChildren2 = btnTransform.GetComponentsInChildren<Text>();
		for (int i = 0; i < componentsInChildren2.Length; i++)
		{
			componentsInChildren2[i].color = Color.grey;
		}
	}

	// Token: 0x0400112B RID: 4395
	private NPCMap TestNPCMap;

	// Token: 0x0400112C RID: 4396
	private List<JSONObject> npcjsonlist = new List<JSONObject>();

	// Token: 0x0400112D RID: 4397
	private List<SeaAvatarObjBase> seaList = new List<SeaAvatarObjBase>();

	// Token: 0x0400112E RID: 4398
	public static bool isDebugMode = false;

	// Token: 0x0400112F RID: 4399
	public static UINPCJiaoHu Inst;

	// Token: 0x04001130 RID: 4400
	public static bool AllShouldHide;

	// Token: 0x04001131 RID: 4401
	public GameObject NPCTalkPrefab;

	// Token: 0x04001132 RID: 4402
	public UINPCLeftList NPCList;

	// Token: 0x04001133 RID: 4403
	public UINPCJiaoHuPop JiaoHuPop;

	// Token: 0x04001134 RID: 4404
	public UINPCLiaoTian LiaoTian;

	// Token: 0x04001135 RID: 4405
	public UINPCQingJiao QingJiao;

	// Token: 0x04001136 RID: 4406
	public UINPCTanCha TanCha;

	// Token: 0x04001137 RID: 4407
	public UINPCInfoPanel InfoPanel;

	// Token: 0x04001138 RID: 4408
	public UINPCZengLi ZengLi;

	// Token: 0x04001139 RID: 4409
	public UINPCSuoQu SuoQu;

	// Token: 0x0400113A RID: 4410
	public UINPCShuangXiuSelect ShuangXiuSelect;

	// Token: 0x0400113B RID: 4411
	public UINPCShuangXiuAnim ShuangXiuAnim;

	// Token: 0x0400113C RID: 4412
	public GameObject CommonMask;

	// Token: 0x0400113D RID: 4413
	public List<int> TNPCIDList = new List<int>();

	// Token: 0x0400113E RID: 4414
	public List<int> NPCIDList = new List<int>();

	// Token: 0x0400113F RID: 4415
	public List<int> SeaNPCIDList = new List<int>();

	// Token: 0x04001140 RID: 4416
	public List<string> SeaNPCUUIDList = new List<string>();

	// Token: 0x04001141 RID: 4417
	public List<int> SeaNPCEventIDList = new List<int>();

	// Token: 0x04001142 RID: 4418
	private DateTime nowTime;

	// Token: 0x04001143 RID: 4419
	private DateTime lastTime;

	// Token: 0x04001144 RID: 4420
	public UINPCData NowJiaoHuNPC;

	// Token: 0x04001145 RID: 4421
	public UINPCData NowJiaoHuEnemy;

	// Token: 0x04001146 RID: 4422
	public Material GreyMat;

	// Token: 0x04001147 RID: 4423
	public bool IsLiaoTianClicked;

	// Token: 0x04001148 RID: 4424
	public bool IsQieCuoClicked;

	// Token: 0x04001149 RID: 4425
	public bool IsTanChaShiBaiOrFaXian;

	// Token: 0x0400114A RID: 4426
	public bool IsGuDingNPCClicked;

	// Token: 0x0400114B RID: 4427
	public bool IsJiaoYiClicked;

	// Token: 0x0400114C RID: 4428
	public bool IsJieShaClicked;

	// Token: 0x0400114D RID: 4429
	public bool IsQingJiaoChengGong;

	// Token: 0x0400114E RID: 4430
	public bool IsQingJiaoShiBaiQF;

	// Token: 0x0400114F RID: 4431
	public bool IsQingJiaoShiBaiSW;

	// Token: 0x04001150 RID: 4432
	public bool IsLunDaoClicked;

	// Token: 0x04001151 RID: 4433
	public bool IsZengLiFinished;

	// Token: 0x04001152 RID: 4434
	public bool IsSuoQuFinished;

	// Token: 0x04001153 RID: 4435
	public bool IsWeiXieFinished;

	// Token: 0x04001154 RID: 4436
	public bool IsNeedWarpToNPCTalk;

	// Token: 0x04001155 RID: 4437
	public ZengLiArg ZengLiArg;

	// Token: 0x04001156 RID: 4438
	public WeiXieArg WeiXieArg;

	// Token: 0x04001157 RID: 4439
	public string QingJiaoName;

	// Token: 0x04001158 RID: 4440
	public int JiaoHuItemID;

	// Token: 0x04001159 RID: 4441
	public string NowNPCJson = "";

	// Token: 0x0400115A RID: 4442
	public static Color NormalColor = new Color(0.5254902f, 0.83137256f, 0.73333335f);

	// Token: 0x0400115B RID: 4443
	public static Color DangerColor = new Color(0.827451f, 0.62352943f, 0.5372549f);

	// Token: 0x0400115C RID: 4444
	public static Color GreyColor = new Color(0.6509804f, 0.6509804f, 0.6509804f);
}
