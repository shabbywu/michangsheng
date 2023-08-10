using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UINPCJiaoHu : MonoBehaviour
{
	private NPCMap TestNPCMap;

	private List<JSONObject> npcjsonlist = new List<JSONObject>();

	private List<SeaAvatarObjBase> seaList = new List<SeaAvatarObjBase>();

	public static bool isDebugMode = false;

	public static UINPCJiaoHu Inst;

	public static bool AllShouldHide;

	public GameObject NPCTalkPrefab;

	public UINPCLeftList NPCList;

	public UINPCJiaoHuPop JiaoHuPop;

	public UINPCLiaoTian LiaoTian;

	public UINPCQingJiao QingJiao;

	public UINPCTanCha TanCha;

	public UINPCInfoPanel InfoPanel;

	public UINPCZengLi ZengLi;

	public UINPCSuoQu SuoQu;

	public UINPCShuangXiuSelect ShuangXiuSelect;

	public UINPCShuangXiuAnim ShuangXiuAnim;

	public GameObject CommonMask;

	public List<int> TNPCIDList = new List<int>();

	public List<int> NPCIDList = new List<int>();

	public List<int> SeaNPCIDList = new List<int>();

	public List<string> SeaNPCUUIDList = new List<string>();

	public List<int> SeaNPCEventIDList = new List<int>();

	private DateTime nowTime;

	private DateTime lastTime;

	public UINPCData NowJiaoHuNPC;

	public UINPCData NowJiaoHuEnemy;

	public Material GreyMat;

	public bool IsLiaoTianClicked;

	public bool IsQieCuoClicked;

	public bool IsTanChaShiBaiOrFaXian;

	public bool IsGuDingNPCClicked;

	public bool IsJiaoYiClicked;

	public bool IsJieShaClicked;

	public bool IsQingJiaoChengGong;

	public bool IsQingJiaoShiBaiQF;

	public bool IsQingJiaoShiBaiSW;

	public bool IsLunDaoClicked;

	public bool IsZengLiFinished;

	public bool IsSuoQuFinished;

	public bool IsWeiXieFinished;

	public bool IsNeedWarpToNPCTalk;

	public ZengLiArg ZengLiArg;

	public WeiXieArg WeiXieArg;

	public string QingJiaoName;

	public int JiaoHuItemID;

	public string NowNPCJson = "";

	public static Color NormalColor = new Color(0.5254902f, 0.83137256f, 11f / 15f);

	public static Color DangerColor = new Color(0.827451f, 53f / 85f, 0.5372549f);

	public static Color GreyColor = new Color(0.6509804f, 0.6509804f, 0.6509804f);

	[HideInInspector]
	public bool NowIsJiaoHu
	{
		get
		{
			if (((Component)JiaoHuPop).gameObject.activeInHierarchy)
			{
				return true;
			}
			if (((Component)LiaoTian).gameObject.activeInHierarchy)
			{
				return true;
			}
			if (((Component)QingJiao).gameObject.activeInHierarchy)
			{
				return true;
			}
			if (((Component)TanCha).gameObject.activeInHierarchy)
			{
				return true;
			}
			if (((Component)InfoPanel).gameObject.activeInHierarchy)
			{
				return true;
			}
			if (CommonMask.gameObject.activeInHierarchy)
			{
				return true;
			}
			if (((Component)NPCList).gameObject.activeInHierarchy && NPCList.IsMouseInUI)
			{
				return true;
			}
			if (((Component)ZengLi).gameObject.activeInHierarchy)
			{
				return true;
			}
			return false;
		}
	}

	public bool NowIsJiaoHu2
	{
		get
		{
			if (((Component)JiaoHuPop).gameObject.activeInHierarchy)
			{
				return true;
			}
			if (((Component)LiaoTian).gameObject.activeInHierarchy)
			{
				return true;
			}
			if (((Component)QingJiao).gameObject.activeInHierarchy)
			{
				return true;
			}
			if (((Component)TanCha).gameObject.activeInHierarchy)
			{
				return true;
			}
			if (((Component)InfoPanel).gameObject.activeInHierarchy)
			{
				return true;
			}
			if (CommonMask.gameObject.activeInHierarchy)
			{
				return true;
			}
			if (((Component)ZengLi).gameObject.activeInHierarchy)
			{
				return true;
			}
			return false;
		}
	}

	private void Awake()
	{
		Inst = this;
		SceneManager.activeSceneChanged += delegate
		{
			UINPCLeftList.ShoudHide = false;
		};
		Object.Instantiate<GameObject>(NPCTalkPrefab, ((Component)this).transform);
	}

	private void Update()
	{
		if ((Object)(object)NpcJieSuanManager.inst == (Object)null)
		{
			return;
		}
		if (Tools.instance.getPlayer() != null)
		{
			lastTime = nowTime;
			nowTime = Tools.instance.getPlayer().worldTimeMag.getNowTime();
			if (lastTime != nowTime)
			{
				NpcJieSuanManager.inst.isUpDateNpcList = true;
			}
		}
		if (NpcJieSuanManager.inst.isUpDateNpcList)
		{
			RefreshNowMapNPC();
		}
		if (!PanelMamager.CanOpenOrClose && !NowIsJiaoHu)
		{
			PanelMamager.CanOpenOrClose = true;
		}
		AutoShowNPCList();
	}

	public void RefreshNowMapNPC()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		Scene activeScene = SceneManager.GetActiveScene();
		string name = ((Scene)(ref activeScene)).name;
		Avatar player = Tools.instance.getPlayer();
		if (player == null)
		{
			return;
		}
		NPCMap npcMap = NpcJieSuanManager.inst.npcMap;
		TestNPCMap = NpcJieSuanManager.inst.npcMap;
		NPCIDList.Clear();
		SeaNPCIDList.Clear();
		SeaNPCUUIDList.Clear();
		SeaNPCEventIDList.Clear();
		npcjsonlist.Clear();
		if (npcMap != null)
		{
			if (name == "AllMaps")
			{
				int nowMapIndex = player.NowMapIndex;
				if (npcMap.bigMapNPCDictionary != null && npcMap.bigMapNPCDictionary.ContainsKey(nowMapIndex) && npcMap.bigMapNPCDictionary[nowMapIndex].Count > 0)
				{
					foreach (int item2 in npcMap.bigMapNPCDictionary[nowMapIndex])
					{
						NPCIDList.Add(item2);
						npcjsonlist.Add(item2.NPCJson());
					}
				}
			}
			else if (name.StartsWith("F"))
			{
				int nowIndex = player.fubenContorl[Tools.getScreenName()].NowIndex;
				if (npcMap.fuBenNPCDictionary != null && npcMap.fuBenNPCDictionary.ContainsKey(name) && npcMap.fuBenNPCDictionary[name].ContainsKey(nowIndex) && npcMap.fuBenNPCDictionary[name][nowIndex].Count > 0)
				{
					foreach (int item3 in npcMap.fuBenNPCDictionary[name][nowIndex])
					{
						NPCIDList.Add(item3);
						npcjsonlist.Add(item3.NPCJson());
					}
				}
			}
			else if (name.StartsWith("Sea"))
			{
				int num = GlobalValue.Get(1000, "UINPCJiaoHu.RefreshNowMapNPC 海上百里奇特殊处理");
				if (num > 0)
				{
					int item = num;
					int num2 = NPCEx.NPCIDToNew(615);
					SeaNPCIDList.Add(num2);
					SeaNPCUUIDList.Add("1f3c6041-c68f-4ab3-ae19-f66f541e3209");
					SeaNPCEventIDList.Add(item);
					npcjsonlist.Add(num2.NPCJson());
				}
				seaList = EndlessSeaMag.Inst.MonstarList;
				foreach (SeaAvatarObjBase monstar in EndlessSeaMag.Inst.MonstarList)
				{
					if (monstar.IsCollect)
					{
						int seaNPCIDByEventID = NPCEx.GetSeaNPCIDByEventID(monstar._EventId);
						SeaNPCIDList.Add(seaNPCIDByEventID);
						SeaNPCUUIDList.Add(monstar.UUID);
						SeaNPCEventIDList.Add(monstar._EventId);
						npcjsonlist.Add(seaNPCIDByEventID.NPCJson());
					}
				}
			}
			else if (name.StartsWith("S"))
			{
				bool flag = false;
				if (name == "S101")
				{
					flag = true;
				}
				if (npcMap.threeSenceNPCDictionary != null && npcMap.threeSenceNPCDictionary.ContainsKey(name) && npcMap.threeSenceNPCDictionary[name].Count > 0)
				{
					foreach (int item4 in npcMap.threeSenceNPCDictionary[name])
					{
						if (flag)
						{
							if (jsonData.instance.AvatarJsonData[item4.ToString()].TryGetField("DongFuId").I == DongFuManager.NowDongFuID)
							{
								NPCIDList.Add(item4);
								npcjsonlist.Add(item4.NPCJson());
							}
						}
						else
						{
							NPCIDList.Add(item4);
							npcjsonlist.Add(item4.NPCJson());
						}
					}
				}
			}
		}
		NPCList.needRefresh = true;
		NpcJieSuanManager.inst.isUpDateNpcList = false;
	}

	public void ShowNowJiaoHuJSON()
	{
		NowNPCJson = NowJiaoHuNPC.json.ToString();
	}

	private void AutoShowNPCList()
	{
		if (!((Component)NPCList).gameObject.activeInHierarchy && NPCList.CanShow())
		{
			((Component)NPCList).gameObject.SetActive(true);
			NPCList.needRefresh = true;
		}
	}

	public void ShowNPCList()
	{
		((Component)NPCList).gameObject.SetActive(true);
	}

	public void HideNPCList()
	{
		((Component)NPCList).gameObject.SetActive(false);
	}

	public void ShowJiaoHuPop()
	{
		if (!((Object)(object)FpUIMag.inst != (Object)null))
		{
			((Component)JiaoHuPop).gameObject.SetActive(true);
			PanelMamager.CanOpenOrClose = false;
			JiaoHuPop.RefreshUI();
			if ((Object)(object)UINPCSVItem.NowSelectedUINPCSVItem != (Object)null)
			{
				UINPCSVItem.NowSelectedUINPCSVItem.Selected.SetActive(true);
				UINPCSVItem.NowSelectedUINPCSVItem.BG.sprite = UINPCSVItem.NowSelectedUINPCSVItem.SelectedBG;
			}
			ESCCloseManager.Inst.RegisterClose(JiaoHuPop);
		}
	}

	public void HideJiaoHuPop()
	{
		((Component)JiaoHuPop).gameObject.SetActive(false);
		PanelMamager.CanOpenOrClose = true;
		if ((Object)(object)UINPCSVItem.NowSelectedUINPCSVItem != (Object)null)
		{
			UINPCSVItem.NowSelectedUINPCSVItem.Selected.SetActive(false);
			UINPCSVItem.NowSelectedUINPCSVItem.BG.sprite = UINPCSVItem.NowSelectedUINPCSVItem.NormalBG;
		}
	}

	public void ShowNPCInfoPanel(UINPCData npc = null)
	{
		((Component)InfoPanel).gameObject.SetActive(true);
		CommonMask.gameObject.SetActive(true);
		InfoPanel.RefreshUI(npc);
		ESCCloseManager.Inst.RegisterClose(InfoPanel);
	}

	public void HideNPCInfoPanel()
	{
		InfoPanel.TabGroup.UnHideTab();
		((Component)InfoPanel).gameObject.SetActive(false);
		CommonMask.gameObject.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(InfoPanel);
	}

	public void ShowNPCTanChaPanel()
	{
		((Component)TanCha).gameObject.SetActive(true);
		CommonMask.gameObject.SetActive(true);
		TanCha.RefreshUI();
	}

	public void HideNPCTanChaPanel()
	{
		((Component)TanCha).gameObject.SetActive(false);
		CommonMask.gameObject.SetActive(false);
	}

	public void ShowNPCQingJiaoPanel()
	{
		((Component)QingJiao).gameObject.SetActive(true);
		CommonMask.SetActive(true);
		QingJiao.RefreshUI();
		ESCCloseManager.Inst.RegisterClose(QingJiao);
	}

	public void HideNPCQingJiaoPanel()
	{
		((Component)QingJiao).gameObject.SetActive(false);
		CommonMask.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(QingJiao);
	}

	public void ShowNPCZengLi()
	{
		((Component)ZengLi).gameObject.SetActive(true);
		CommonMask.SetActive(true);
		ZengLi.RefreshUI();
		ESCCloseManager.Inst.RegisterClose(ZengLi);
	}

	public void HideNPCZengLi()
	{
		((Component)ZengLi).gameObject.SetActive(false);
		CommonMask.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(ZengLi);
	}

	public void ShowNPCSuoQu()
	{
		((Component)SuoQu).gameObject.SetActive(true);
		CommonMask.SetActive(true);
		SuoQu.RefreshUI();
		ESCCloseManager.Inst.RegisterClose(SuoQu);
	}

	public void HideNPCSuoQu()
	{
		((Component)SuoQu).gameObject.SetActive(false);
		CommonMask.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(SuoQu);
	}

	public void ShowNPCShuangXiuSelect()
	{
		HideJiaoHuPop();
		((Component)ShuangXiuSelect).gameObject.SetActive(true);
		CommonMask.SetActive(true);
		ShuangXiuSelect.RefreshUI();
		ESCCloseManager.Inst.RegisterClose(ShuangXiuSelect);
	}

	public void HideNPCShuangXiuSelect()
	{
		((Component)ShuangXiuSelect).gameObject.SetActive(false);
		CommonMask.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(ShuangXiuSelect);
	}

	public void ShuangXiuTest()
	{
		for (int i = 2; i <= 6; i++)
		{
			PlayerEx.StudyShuangXiuSkill(i);
		}
	}

	public void ShowNPCShuangXiuAnim()
	{
		((Component)ShuangXiuAnim).gameObject.SetActive(true);
		CommonMask.SetActive(true);
		ShuangXiuAnim.RefreshUI();
	}

	public void HideNPCShuangXiuAnim()
	{
		((Component)ShuangXiuAnim).gameObject.SetActive(false);
		CommonMask.SetActive(false);
	}

	public void SetBtnNormalColor(Transform btnTransform)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		Image[] componentsInChildren = ((Component)btnTransform).GetComponentsInChildren<Image>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			((Graphic)componentsInChildren[i]).material = null;
		}
		Text[] componentsInChildren2 = ((Component)btnTransform).GetComponentsInChildren<Text>();
		for (int i = 0; i < componentsInChildren2.Length; i++)
		{
			((Graphic)componentsInChildren2[i]).color = NormalColor;
		}
	}

	public void SetBtnDangerColor(Transform btnTransform)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		Image[] componentsInChildren = ((Component)btnTransform).GetComponentsInChildren<Image>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			((Graphic)componentsInChildren[i]).material = null;
		}
		Text[] componentsInChildren2 = ((Component)btnTransform).GetComponentsInChildren<Text>();
		for (int i = 0; i < componentsInChildren2.Length; i++)
		{
			((Graphic)componentsInChildren2[i]).color = DangerColor;
		}
	}

	public void SetBtnGreyColor(Transform btnTransform)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		Image[] componentsInChildren = ((Component)btnTransform).GetComponentsInChildren<Image>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			((Graphic)componentsInChildren[i]).material = GreyMat;
		}
		Text[] componentsInChildren2 = ((Component)btnTransform).GetComponentsInChildren<Text>();
		for (int i = 0; i < componentsInChildren2.Length; i++)
		{
			((Graphic)componentsInChildren2[i]).color = Color.grey;
		}
	}
}
