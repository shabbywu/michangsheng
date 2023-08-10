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

public class FpUIMag : MonoBehaviour
{
	public static FpUIMag inst;

	[SerializeField]
	private PlayerSetRandomFace playerFace;

	[SerializeField]
	private Text playerName;

	[SerializeField]
	private PlayerSetRandomFace npcFace;

	[SerializeField]
	private Text npcName;

	public int npcId;

	public FpBtn startFightBtn;

	public FpBtn tanCai1Btn;

	public FpBtn SkipFight;

	public FpBtn DisableSkipFight;

	public Text TipsText;

	public FpBtn tanCai2Btn;

	public FpBtn fighthPrepareBtn;

	public FpBtn taoPaoBtn;

	private readonly List<StartFight.FightEnumType> TouXiangTypes = new List<StartFight.FightEnumType>
	{
		Fungus.StartFight.FightEnumType.LeiTai,
		Fungus.StartFight.FightEnumType.QieCuo,
		Fungus.StartFight.FightEnumType.DouFa,
		Fungus.StartFight.FightEnumType.无装备无丹药擂台
	};

	private void Awake()
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		Tools instance = Tools.instance;
		Scene activeScene = SceneManager.GetActiveScene();
		instance.FinalScene = ((Scene)(ref activeScene)).name;
		inst = this;
		((Component)this).transform.SetParent(((Component)NewUICanvas.Inst).gameObject.transform);
		((Component)this).transform.SetAsFirstSibling();
		((Component)this).transform.localPosition = Vector3.zero;
		((Component)this).transform.localScale = Vector3.one;
	}

	public void Init()
	{
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Expected O, but got Unknown
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Expected O, but got Unknown
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Expected O, but got Unknown
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Expected O, but got Unknown
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Expected O, but got Unknown
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Expected O, but got Unknown
		playerFace.SetNPCFace(1);
		playerName.text = Tools.instance.getPlayer().name;
		npcId = Tools.instance.MonstarID;
		npcFace.SetNPCFace(npcId);
		npcName.text = jsonData.instance.AvatarRandomJsonData[npcId.ToString()]["Name"].Str;
		int i = jsonData.instance.AvatarRandomJsonData[npcId.ToString()]["HaoGanDu"].I;
		startFightBtn.mouseUpEvent.AddListener(new UnityAction(StartFight));
		if (i >= 50)
		{
			((Component)tanCai1Btn).gameObject.SetActive(false);
			((Component)tanCai2Btn).gameObject.SetActive(true);
			tanCai2Btn.mouseUpEvent.AddListener(new UnityAction(TanCai));
		}
		else
		{
			((Component)tanCai1Btn).gameObject.SetActive(true);
			((Component)tanCai2Btn).gameObject.SetActive(false);
			tanCai1Btn.mouseUpEvent.AddListener(new UnityAction(TanCai));
		}
		fighthPrepareBtn.mouseUpEvent.AddListener(new UnityAction(OpenBag));
		taoPaoBtn.mouseUpEvent.AddListener(new UnityAction(PlayRunAway));
		string tips = GetTips();
		if (tips == "无")
		{
			((Component)DisableSkipFight).gameObject.SetActive(false);
			((Component)SkipFight).gameObject.SetActive(true);
			SkipFight.mouseUpEvent.AddListener((UnityAction)delegate
			{
				ResManager.inst.LoadPrefab("VictoryPanel").Inst();
				Close();
			});
		}
		else
		{
			((Component)DisableSkipFight).gameObject.SetActive(true);
			((Component)SkipFight).gameObject.SetActive(false);
			TipsText.text = tips;
		}
		UINPCJiaoHu.AllShouldHide = false;
	}

	private void StartFight()
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		Tools instance = Tools.instance;
		Scene activeScene = SceneManager.GetActiveScene();
		instance.FinalScene = ((Scene)(ref activeScene)).name;
		Tools.instance.loadOtherScenes("YSNewFight");
		UINPCJiaoHu.AllShouldHide = false;
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	private void TanCai()
	{
		UINPCData uINPCData = new UINPCData(npcId);
		if (npcId < 20000)
		{
			uINPCData.RefreshOldNpcData();
		}
		else
		{
			uINPCData.RefreshData();
		}
		uINPCData.IsFight = true;
		UINPCJiaoHu.Inst.NowJiaoHuEnemy = uINPCData;
		UINPCJiaoHu.Inst.JiaoHuPop.FightTanCha();
		UINPCJiaoHu.Inst.InfoPanel.TabGroup.HideTab();
	}

	private void OpenBag()
	{
		TabUIMag.OpenTab2(2);
	}

	private void PlayRunAway()
	{
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Expected O, but got Unknown
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Expected O, but got Unknown
		try
		{
			if (Tools.instance.CanFpRun == 0)
			{
				UIPopTip.Inst.Pop("此战斗战前无法逃跑");
				return;
			}
			if (!Tools.instance.monstarMag.CanRunAway())
			{
				string str = Tools.getStr("cannotRunAway" + Tools.instance.monstarMag.CanNotRunAwayEvent());
				UIPopTip.Inst.Pop(str);
				return;
			}
			Avatar avatar = Tools.instance.getPlayer();
			if (TouXiangTypes.Contains(Tools.instance.monstarMag.FightType))
			{
				USelectBox.Show("是否确认投降?", (UnityAction)delegate
				{
					//IL_0011: Unknown result type (might be due to invalid IL or missing references)
					//IL_0016: Unknown result type (might be due to invalid IL or missing references)
					GlobalValue.SetTalk(1, 4, "FpUIMag.PlayRunAway");
					Tools instance2 = Tools.instance;
					Scene activeScene2 = SceneManager.GetActiveScene();
					instance2.FinalScene = ((Scene)(ref activeScene2)).name;
					Tools.instance.AutoSeatSeaRunAway();
					if (Tools.instance.getPlayer().NowFuBen == "" || Tools.instance.FinalScene.Contains("Sea"))
					{
						Tools.instance.CanShowFightUI = 1;
					}
					if (GlobalValue.GetTalk(0, "FpUIMag.PlayRunAway") > 0 || avatar.fubenContorl.isInFuBen() || Tools.instance.FinalScene.Contains("Sea"))
					{
						UINPCJiaoHu.AllShouldHide = false;
						Object.Destroy((Object)(object)((Component)this).gameObject);
						Tools.instance.loadMapScenes(Tools.instance.FinalScene);
						Tools.instance.monstarMag.ClearBuff();
					}
					else
					{
						Close();
					}
				});
			}
			else if (avatar.dunSu - (int)jsonData.instance.AvatarJsonData[string.Concat(Tools.instance.MonstarID)]["dunSu"].n > 0)
			{
				USelectBox.Show("是否确认遁走？", (UnityAction)delegate
				{
					//IL_0011: Unknown result type (might be due to invalid IL or missing references)
					//IL_0016: Unknown result type (might be due to invalid IL or missing references)
					GlobalValue.SetTalk(1, 4, "FpUIMag.PlayRunAway");
					Tools instance = Tools.instance;
					Scene activeScene = SceneManager.GetActiveScene();
					instance.FinalScene = ((Scene)(ref activeScene)).name;
					Tools.instance.AutoSeatSeaRunAway();
					if (Tools.instance.getPlayer().NowFuBen == "" || Tools.instance.FinalScene.Contains("Sea"))
					{
						Tools.instance.CanShowFightUI = 1;
					}
					if (GlobalValue.GetTalk(0, "FpUIMag.PlayRunAway") > 0 || avatar.fubenContorl.isInFuBen() || Tools.instance.FinalScene.Contains("Sea"))
					{
						UINPCJiaoHu.AllShouldHide = false;
						Object.Destroy((Object)(object)((Component)this).gameObject);
						Tools.instance.loadMapScenes(Tools.instance.FinalScene);
						Tools.instance.monstarMag.ClearBuff();
					}
					else
					{
						Close();
					}
				});
			}
			else
			{
				UIPopTip.Inst.Pop(Tools.getStr("cannotRunAway0"));
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
	}

	public void Close()
	{
		UI_Manager.inst.checkTool.Init();
		UINPCJiaoHu.AllShouldHide = false;
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	private string GetTips()
	{
		if (Tools.instance.monstarMag.FightType != Fungus.StartFight.FightEnumType.Normal)
		{
			return "该战斗类型无法跳过";
		}
		if (PlayerEx.Player.level <= jsonData.instance.AvatarJsonData[npcId.ToString()]["Level"].I)
		{
			return "境界未高于此对手";
		}
		if (!PlayerEx.Player.HasDefeatNpcList.Contains(npcId))
		{
			return "未曾战胜过此对手";
		}
		return "无";
	}
}
