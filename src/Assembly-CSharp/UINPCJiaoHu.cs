using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x0200038A RID: 906
public class UINPCJiaoHu : MonoBehaviour
{
	// Token: 0x17000282 RID: 642
	// (get) Token: 0x0600195B RID: 6491 RVA: 0x000E2564 File Offset: 0x000E0764
	[HideInInspector]
	public bool NowIsJiaoHu
	{
		get
		{
			return this.JiaoHuPop.gameObject.activeInHierarchy || this.LiaoTian.gameObject.activeInHierarchy || this.QingJiao.gameObject.activeInHierarchy || this.TanCha.gameObject.activeInHierarchy || this.InfoPanel.gameObject.activeInHierarchy || this.CommonMask.gameObject.activeInHierarchy || (this.NPCList.gameObject.activeInHierarchy && this.NPCList.IsMouseInUI) || this.ZengLi.gameObject.activeInHierarchy;
		}
	}

	// Token: 0x17000283 RID: 643
	// (get) Token: 0x0600195C RID: 6492 RVA: 0x000E2620 File Offset: 0x000E0820
	public bool NowIsJiaoHu2
	{
		get
		{
			return this.JiaoHuPop.gameObject.activeInHierarchy || this.LiaoTian.gameObject.activeInHierarchy || this.QingJiao.gameObject.activeInHierarchy || this.TanCha.gameObject.activeInHierarchy || this.InfoPanel.gameObject.activeInHierarchy || this.CommonMask.gameObject.activeInHierarchy || this.ZengLi.gameObject.activeInHierarchy;
		}
	}

	// Token: 0x0600195D RID: 6493 RVA: 0x00015B20 File Offset: 0x00013D20
	private void Awake()
	{
		UINPCJiaoHu.Inst = this;
		SceneManager.activeSceneChanged += delegate(Scene s1, Scene s2)
		{
			UINPCLeftList.ShoudHide = false;
		};
		Object.Instantiate<GameObject>(this.NPCTalkPrefab, base.transform);
	}

	// Token: 0x0600195E RID: 6494 RVA: 0x000E26BC File Offset: 0x000E08BC
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

	// Token: 0x0600195F RID: 6495 RVA: 0x000E2754 File Offset: 0x000E0954
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

	// Token: 0x06001960 RID: 6496 RVA: 0x00015B5E File Offset: 0x00013D5E
	public void ShowNowJiaoHuJSON()
	{
		this.NowNPCJson = this.NowJiaoHuNPC.json.ToString();
	}

	// Token: 0x06001961 RID: 6497 RVA: 0x00015B76 File Offset: 0x00013D76
	private void AutoShowNPCList()
	{
		if (!this.NPCList.gameObject.activeInHierarchy && this.NPCList.CanShow())
		{
			this.NPCList.gameObject.SetActive(true);
			this.NPCList.needRefresh = true;
		}
	}

	// Token: 0x06001962 RID: 6498 RVA: 0x00015BB4 File Offset: 0x00013DB4
	public void ShowNPCList()
	{
		this.NPCList.gameObject.SetActive(true);
	}

	// Token: 0x06001963 RID: 6499 RVA: 0x00015BC7 File Offset: 0x00013DC7
	public void HideNPCList()
	{
		this.NPCList.gameObject.SetActive(false);
	}

	// Token: 0x06001964 RID: 6500 RVA: 0x000E2BB8 File Offset: 0x000E0DB8
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

	// Token: 0x06001965 RID: 6501 RVA: 0x000E2C3C File Offset: 0x000E0E3C
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

	// Token: 0x06001966 RID: 6502 RVA: 0x00015BDA File Offset: 0x00013DDA
	public void ShowNPCInfoPanel(UINPCData npc = null)
	{
		this.InfoPanel.gameObject.SetActive(true);
		this.CommonMask.gameObject.SetActive(true);
		this.InfoPanel.RefreshUI(npc);
		ESCCloseManager.Inst.RegisterClose(this.InfoPanel);
	}

	// Token: 0x06001967 RID: 6503 RVA: 0x000E2C98 File Offset: 0x000E0E98
	public void HideNPCInfoPanel()
	{
		this.InfoPanel.TabGroup.UnHideTab();
		this.InfoPanel.gameObject.SetActive(false);
		this.CommonMask.gameObject.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this.InfoPanel);
	}

	// Token: 0x06001968 RID: 6504 RVA: 0x00015C1A File Offset: 0x00013E1A
	public void ShowNPCTanChaPanel()
	{
		this.TanCha.gameObject.SetActive(true);
		this.CommonMask.gameObject.SetActive(true);
		this.TanCha.RefreshUI();
	}

	// Token: 0x06001969 RID: 6505 RVA: 0x00015C49 File Offset: 0x00013E49
	public void HideNPCTanChaPanel()
	{
		this.TanCha.gameObject.SetActive(false);
		this.CommonMask.gameObject.SetActive(false);
	}

	// Token: 0x0600196A RID: 6506 RVA: 0x00015C6D File Offset: 0x00013E6D
	public void ShowNPCQingJiaoPanel()
	{
		this.QingJiao.gameObject.SetActive(true);
		this.CommonMask.SetActive(true);
		this.QingJiao.RefreshUI();
		ESCCloseManager.Inst.RegisterClose(this.QingJiao);
	}

	// Token: 0x0600196B RID: 6507 RVA: 0x00015CA7 File Offset: 0x00013EA7
	public void HideNPCQingJiaoPanel()
	{
		this.QingJiao.gameObject.SetActive(false);
		this.CommonMask.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this.QingJiao);
	}

	// Token: 0x0600196C RID: 6508 RVA: 0x00015CD6 File Offset: 0x00013ED6
	public void ShowNPCZengLi()
	{
		this.ZengLi.gameObject.SetActive(true);
		this.CommonMask.SetActive(true);
		this.ZengLi.RefreshUI();
		ESCCloseManager.Inst.RegisterClose(this.ZengLi);
	}

	// Token: 0x0600196D RID: 6509 RVA: 0x00015D10 File Offset: 0x00013F10
	public void HideNPCZengLi()
	{
		this.ZengLi.gameObject.SetActive(false);
		this.CommonMask.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this.ZengLi);
	}

	// Token: 0x0600196E RID: 6510 RVA: 0x00015D3F File Offset: 0x00013F3F
	public void ShowNPCSuoQu()
	{
		this.SuoQu.gameObject.SetActive(true);
		this.CommonMask.SetActive(true);
		this.SuoQu.RefreshUI();
		ESCCloseManager.Inst.RegisterClose(this.SuoQu);
	}

	// Token: 0x0600196F RID: 6511 RVA: 0x00015D79 File Offset: 0x00013F79
	public void HideNPCSuoQu()
	{
		this.SuoQu.gameObject.SetActive(false);
		this.CommonMask.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this.SuoQu);
	}

	// Token: 0x06001970 RID: 6512 RVA: 0x00015DA8 File Offset: 0x00013FA8
	public void ShowNPCShuangXiuSelect()
	{
		this.HideJiaoHuPop();
		this.ShuangXiuSelect.gameObject.SetActive(true);
		this.CommonMask.SetActive(true);
		this.ShuangXiuSelect.RefreshUI();
		ESCCloseManager.Inst.RegisterClose(this.ShuangXiuSelect);
	}

	// Token: 0x06001971 RID: 6513 RVA: 0x00015DE8 File Offset: 0x00013FE8
	public void HideNPCShuangXiuSelect()
	{
		this.ShuangXiuSelect.gameObject.SetActive(false);
		this.CommonMask.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this.ShuangXiuSelect);
	}

	// Token: 0x06001972 RID: 6514 RVA: 0x000E2CE8 File Offset: 0x000E0EE8
	public void ShuangXiuTest()
	{
		for (int i = 2; i <= 6; i++)
		{
			PlayerEx.StudyShuangXiuSkill(i);
		}
	}

	// Token: 0x06001973 RID: 6515 RVA: 0x00015E17 File Offset: 0x00014017
	public void ShowNPCShuangXiuAnim()
	{
		this.ShuangXiuAnim.gameObject.SetActive(true);
		this.CommonMask.SetActive(true);
		this.ShuangXiuAnim.RefreshUI();
	}

	// Token: 0x06001974 RID: 6516 RVA: 0x00015E41 File Offset: 0x00014041
	public void HideNPCShuangXiuAnim()
	{
		this.ShuangXiuAnim.gameObject.SetActive(false);
		this.CommonMask.SetActive(false);
	}

	// Token: 0x06001975 RID: 6517 RVA: 0x000E2D08 File Offset: 0x000E0F08
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

	// Token: 0x06001976 RID: 6518 RVA: 0x000E2D58 File Offset: 0x000E0F58
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

	// Token: 0x06001977 RID: 6519 RVA: 0x000E2DA8 File Offset: 0x000E0FA8
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

	// Token: 0x0400147B RID: 5243
	private NPCMap TestNPCMap;

	// Token: 0x0400147C RID: 5244
	private List<JSONObject> npcjsonlist = new List<JSONObject>();

	// Token: 0x0400147D RID: 5245
	private List<SeaAvatarObjBase> seaList = new List<SeaAvatarObjBase>();

	// Token: 0x0400147E RID: 5246
	public static bool isDebugMode = false;

	// Token: 0x0400147F RID: 5247
	public static UINPCJiaoHu Inst;

	// Token: 0x04001480 RID: 5248
	public static bool AllShouldHide;

	// Token: 0x04001481 RID: 5249
	public GameObject NPCTalkPrefab;

	// Token: 0x04001482 RID: 5250
	public UINPCLeftList NPCList;

	// Token: 0x04001483 RID: 5251
	public UINPCJiaoHuPop JiaoHuPop;

	// Token: 0x04001484 RID: 5252
	public UINPCLiaoTian LiaoTian;

	// Token: 0x04001485 RID: 5253
	public UINPCQingJiao QingJiao;

	// Token: 0x04001486 RID: 5254
	public UINPCTanCha TanCha;

	// Token: 0x04001487 RID: 5255
	public UINPCInfoPanel InfoPanel;

	// Token: 0x04001488 RID: 5256
	public UINPCZengLi ZengLi;

	// Token: 0x04001489 RID: 5257
	public UINPCSuoQu SuoQu;

	// Token: 0x0400148A RID: 5258
	public UINPCShuangXiuSelect ShuangXiuSelect;

	// Token: 0x0400148B RID: 5259
	public UINPCShuangXiuAnim ShuangXiuAnim;

	// Token: 0x0400148C RID: 5260
	public GameObject CommonMask;

	// Token: 0x0400148D RID: 5261
	public List<int> TNPCIDList = new List<int>();

	// Token: 0x0400148E RID: 5262
	public List<int> NPCIDList = new List<int>();

	// Token: 0x0400148F RID: 5263
	public List<int> SeaNPCIDList = new List<int>();

	// Token: 0x04001490 RID: 5264
	public List<string> SeaNPCUUIDList = new List<string>();

	// Token: 0x04001491 RID: 5265
	public List<int> SeaNPCEventIDList = new List<int>();

	// Token: 0x04001492 RID: 5266
	private DateTime nowTime;

	// Token: 0x04001493 RID: 5267
	private DateTime lastTime;

	// Token: 0x04001494 RID: 5268
	public UINPCData NowJiaoHuNPC;

	// Token: 0x04001495 RID: 5269
	public UINPCData NowJiaoHuEnemy;

	// Token: 0x04001496 RID: 5270
	public Material GreyMat;

	// Token: 0x04001497 RID: 5271
	public bool IsLiaoTianClicked;

	// Token: 0x04001498 RID: 5272
	public bool IsQieCuoClicked;

	// Token: 0x04001499 RID: 5273
	public bool IsTanChaShiBaiOrFaXian;

	// Token: 0x0400149A RID: 5274
	public bool IsGuDingNPCClicked;

	// Token: 0x0400149B RID: 5275
	public bool IsJiaoYiClicked;

	// Token: 0x0400149C RID: 5276
	public bool IsJieShaClicked;

	// Token: 0x0400149D RID: 5277
	public bool IsQingJiaoChengGong;

	// Token: 0x0400149E RID: 5278
	public bool IsQingJiaoShiBaiQF;

	// Token: 0x0400149F RID: 5279
	public bool IsQingJiaoShiBaiSW;

	// Token: 0x040014A0 RID: 5280
	public bool IsLunDaoClicked;

	// Token: 0x040014A1 RID: 5281
	public bool IsZengLiFinished;

	// Token: 0x040014A2 RID: 5282
	public bool IsSuoQuFinished;

	// Token: 0x040014A3 RID: 5283
	public bool IsWeiXieFinished;

	// Token: 0x040014A4 RID: 5284
	public ZengLiArg ZengLiArg;

	// Token: 0x040014A5 RID: 5285
	public WeiXieArg WeiXieArg;

	// Token: 0x040014A6 RID: 5286
	public string QingJiaoName;

	// Token: 0x040014A7 RID: 5287
	public int JiaoHuItemID;

	// Token: 0x040014A8 RID: 5288
	public string NowNPCJson = "";

	// Token: 0x040014A9 RID: 5289
	public static Color NormalColor = new Color(0.5254902f, 0.83137256f, 0.73333335f);

	// Token: 0x040014AA RID: 5290
	public static Color DangerColor = new Color(0.827451f, 0.62352943f, 0.5372549f);

	// Token: 0x040014AB RID: 5291
	public static Color GreyColor = new Color(0.6509804f, 0.6509804f, 0.6509804f);
}
