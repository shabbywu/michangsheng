using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using YSGame;

namespace Fungus;

[CommandInfo("YSTools", "StartFight", "开始战斗模块", 0)]
[AddComponentMenu("")]
public class StartFight : Command
{
	public enum FightEnumType
	{
		Normal = 1,
		XingMo,
		DuJie,
		LeiTai,
		HuanJin,
		BossZhan,
		DiFangTaoLi,
		QieCuo,
		XinShouYinDao,
		DouFa,
		JieDan,
		ZhangLaoZhan,
		BuShaDuiShou,
		JieYing,
		ZhuJi,
		古树根须,
		生死比试,
		HuaShen,
		FeiSheng,
		无装备无丹药擂台,
		天劫秘术领悟
	}

	public enum MonstarType
	{
		Normal,
		FungusVariable
	}

	[Tooltip("设置怪物ID的方式")]
	[SerializeField]
	protected MonstarType MonstarSetType;

	[Tooltip("怪物的ID")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable MonstarFungusID;

	[Tooltip("怪物的ID")]
	[SerializeField]
	public int MonstarID;

	[Tooltip("给怪物加buff")]
	[SerializeField]
	protected List<StarttFightAddBuff> MonstarAddBuffList = new List<StarttFightAddBuff>();

	[Tooltip("给主角加buff")]
	[SerializeField]
	protected List<StarttFightAddBuff> HeroAddBuffList = new List<StarttFightAddBuff>();

	[Tooltip("战斗的类型")]
	[SerializeField]
	protected FightEnumType FightType = FightEnumType.Normal;

	[Tooltip("设置战场背景的方式")]
	[SerializeField]
	protected MonstarType BackgroundSetType;

	[Tooltip("背景的ID")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable BackgroundFungusID;

	[Tooltip("背景的ID")]
	[SerializeField]
	protected int BackgroundID;

	[Tooltip("开启战场对话")]
	[SerializeField]
	protected int FightTalk;

	[Tooltip("开启固定抽排")]
	[SerializeField]
	protected int FightCard;

	[Tooltip("是否能战前逃跑")]
	[SerializeField]
	protected int CanFpRun = 1;

	[Tooltip("战场音乐")]
	[SerializeField]
	protected string FightMusic = "战斗1";

	[Tooltip("是否开启海域移除NPC")]
	[SerializeField]
	protected bool SeaRemoveNPCFlag;

	[Tooltip("海域移除NPC的编号UUID")]
	[VariableProperty(new Type[] { typeof(StringVariable) })]
	[SerializeField]
	protected StringVariable SeaRemoveNPCUUID;

	public MonstarType _MonstarSetType => MonstarSetType;

	public MonstarType _BackgroundSetType => BackgroundSetType;

	public override void OnEnter()
	{
		int num = 0;
		if ((Object)(object)MonstarFungusID != (Object)null)
		{
			num = MonstarFungusID.Value;
		}
		string seaRemoveNPCUUID = "";
		if ((Object)(object)SeaRemoveNPCUUID != (Object)null)
		{
			seaRemoveNPCUUID = SeaRemoveNPCUUID.Value;
		}
		try
		{
			Do(MonstarID, CanFpRun, MonstarSetType, FightType, num, FightTalk, FightCard, BackgroundID, FightMusic, SeaRemoveNPCFlag, seaRemoveNPCUUID, MonstarAddBuffList, HeroAddBuffList);
		}
		catch (Exception ex)
		{
			if ((Object)(object)FpUIMag.inst != (Object)null)
			{
				FpUIMag.inst.Close();
			}
			Debug.LogError((object)ex);
			Debug.LogError((object)("错误NPCId：" + num));
			UIPopTip.Inst.Pop("获取NPC错误，请排查是否有mod冲突等问题");
		}
		Continue();
	}

	public static void Do(int MonstarID, int CanFpRun, MonstarType MonstarSetType, FightEnumType FightType, int MonstarFungusID, int FightTalk, int FightCard, int BackgroundID, string FightMusic, bool SeaRemoveNPCFlag, string SeaRemoveNPCUUID, List<StarttFightAddBuff> MonstarAddBuffList, List<StarttFightAddBuff> HeroAddBuffList)
	{
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		Avatar player = Tools.instance.getPlayer();
		Tools.instance.CanFpRun = CanFpRun;
		if (MonstarID > 0 && NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(MonstarID))
		{
			MonstarID = NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[MonstarID];
		}
		else if (MonstarSetType == MonstarType.FungusVariable && NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(MonstarFungusID))
		{
			MonstarFungusID = NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[MonstarFungusID];
		}
		foreach (StarttFightAddBuff MonstarAddBuff in MonstarAddBuffList)
		{
			if (!Tools.instance.monstarMag.monstarAddBuff.ContainsKey(MonstarAddBuff.buffID))
			{
				Tools.instance.monstarMag.monstarAddBuff.Add(MonstarAddBuff.buffID, MonstarAddBuff.BuffNum);
			}
		}
		foreach (StarttFightAddBuff HeroAddBuff in HeroAddBuffList)
		{
			if (!Tools.instance.monstarMag.HeroAddBuff.ContainsKey(HeroAddBuff.buffID))
			{
				Tools.instance.monstarMag.HeroAddBuff.Add(HeroAddBuff.buffID, HeroAddBuff.BuffNum);
			}
		}
		if (FightType > (FightEnumType)0)
		{
			Tools.instance.monstarMag.FightType = FightType;
		}
		else
		{
			Tools.instance.monstarMag.FightType = FightEnumType.Normal;
		}
		switch (FightType)
		{
		case FightEnumType.JieDan:
			LoadTuPo("YSNewJieDanFight", MonstarID);
			break;
		case FightEnumType.JieYing:
			LoadTuPo("YSNewYuanYingFight", MonstarID);
			break;
		case FightEnumType.ZhuJi:
			LoadTuPo("YSNewZhuJi", MonstarID);
			break;
		case FightEnumType.HuaShen:
			LoadTuPo("YSNewHuaShen", MonstarID);
			break;
		case FightEnumType.FeiSheng:
			LoadTuPo("YSNewTianJieFight", MonstarID);
			break;
		case FightEnumType.天劫秘术领悟:
		{
			GlobalValue.SetTalk(0, 7001, "StartFight 天劫秘术领悟");
			Tools.instance.MonstarID = MonstarID;
			Tools instance = Tools.instance;
			Scene activeScene = SceneManager.GetActiveScene();
			instance.FinalScene = ((Scene)(ref activeScene)).name;
			Tools.instance.loadOtherScenes("YSNewFight");
			UINPCJiaoHu.AllShouldHide = false;
			break;
		}
		default:
			if (MonstarSetType == MonstarType.Normal)
			{
				Tools.instance.startFight(MonstarID);
				break;
			}
			if (NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(MonstarFungusID))
			{
				MonstarFungusID = NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[MonstarFungusID];
			}
			Tools.instance.startFight(MonstarFungusID);
			break;
		}
		Tools.instance.monstarMag.gameStartHP = player.HP;
		Tools.instance.monstarMag.FightTalkID = FightTalk;
		Tools.instance.monstarMag.FightCardID = FightCard;
		Tools.instance.monstarMag.FightImageID = BackgroundID;
		MusicMag.instance.playMusic(FightMusic);
		if (SeaRemoveNPCFlag)
		{
			Tools.instance.StartRemoveSeaMonstarFight(SeaRemoveNPCUUID);
		}
	}

	public static void LoadTuPo(string tuPoSceneName, int MonstarID)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Expected O, but got Unknown
		Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("TpPanel"));
		TpUIMag.inst.call = (UnityAction)delegate
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			Tools.instance.MonstarID = MonstarID;
			Tools instance = Tools.instance;
			Scene activeScene = SceneManager.GetActiveScene();
			instance.FinalScene = ((Scene)(ref activeScene)).name;
			Tools.instance.loadOtherScenes(tuPoSceneName);
		};
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
