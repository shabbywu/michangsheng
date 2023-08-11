using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YSGame;
using YSGame.Fight;
using script.world_script;

public class RoundManager : MonoBehaviour
{
	public enum entityID
	{
		EntityPlayer = 10,
		EntityMonster
	}

	public GameObject weapon;

	public GameObject weapon2;

	public NewNpcFightManager newNpcFightManager;

	public Dictionary<string, int> WeaponSkillList = new Dictionary<string, int>();

	public int curRemoveBuffId;

	public GUIPackage.Skill CurSkill;

	public Text PlayerCardText;

	public Text NpcCardText;

	public SkillCheck PlayerSkillCheck;

	public SkillCheck NpcSkillCheck;

	public List<string> SkillList = new List<string>();

	public int PlayerCurRoundDrawCardNum;

	public int NpcCurRoundDrawCardNum;

	public List<int> PlayerUseSkillList;

	public List<int> NpcUseSkillList;

	public bool IsVirtual;

	public int VirtualSkillDamage;

	private Avatar gameStartAvatar;

	public IFightEventProcessor PlayerFightEventProcessor;

	public int StaticRoundNum;

	public GameObject playerHoldCard;

	public static RoundManager instance;

	public GUIPackage.Skill ChoiceSkill;

	public TooltipSkill ToolitpSkill;

	public TooltipSkill ItemToolitpSkill;

	public Flowchart FightTalk;

	public FightStaticDrawCard FightDrawCard;

	public GameObject FightInfoScrew;

	public GameObject FightInfoTemp;

	public SpriteRenderer BackGroud;

	public BackGroundImage BackGroundImage;

	public int gameOverSwitch;

	public static List<StartFight.FightEnumType> TuPoTypeList = new List<StartFight.FightEnumType>
	{
		StartFight.FightEnumType.ZhuJi,
		StartFight.FightEnumType.JieDan,
		StartFight.FightEnumType.JieYing,
		StartFight.FightEnumType.HuaShen,
		StartFight.FightEnumType.FeiSheng
	};

	private readonly List<StartFight.FightEnumType> TouXiangTypes = new List<StartFight.FightEnumType>
	{
		StartFight.FightEnumType.LeiTai,
		StartFight.FightEnumType.QieCuo,
		StartFight.FightEnumType.DouFa,
		StartFight.FightEnumType.无装备无丹药擂台
	};

	private int clickSkillChangeLingQiIndex;

	private static Dictionary<int, List<int[]>> lingQiKeNengXingZuHe;

	private static Dictionary<int, List<int[]>> lingQiKeNengXingPaiLie;

	private List<int> choiceSkillCanUseLingQiIndexList;

	public int NowSkillUsedLingQiSum = -1;

	public static float KeyHideCD;

	[HideInInspector]
	public UseLingQiType NowUseLingQiType;

	private void Awake()
	{
		instance = this;
		InitLingQiKeNengXing();
		BindData.Bind("FightBeforeHpMax", Tools.instance.getPlayer().HP_Max);
		Event.registerOut("endRound", this, "endRound");
		Event.registerOut("startRound", this, "startRound");
		YSFuncList.Ints.Clear();
		if (!TuPoTypeList.Contains(Tools.instance.monstarMag.FightType) && (Object)(object)BackGroundImage != (Object)null)
		{
			if (Tools.instance.monstarMag.FightImageID != 0)
			{
				BackGroundImage.BGName = Tools.instance.monstarMag.FightImageID.ToString();
			}
			else
			{
				BackGroundImage.BGName = "1";
			}
		}
		MessageMag.Instance.Register("Fight_CardChange", UpdateCard);
	}

	public void MoveLingQiToCacheFromPlayer(Dictionary<int, int> skillCost, LingQiCacheType cacheType)
	{
		if (cacheType == LingQiCacheType.DontMove)
		{
			UIFightPanel.Inst.CacheLingQiController.NowMoveSame = true;
			foreach (KeyValuePair<int, int> item in skillCost)
			{
				UIFightLingQiCacheSlot targetLingQiSlot = UIFightPanel.Inst.CacheLingQiController.GetTargetLingQiSlot((LingQiType)item.Key);
				UIFightPanel.Inst.PlayerLingQiController.SlotList[item.Key].LingQiCount -= item.Value;
				targetLingQiSlot.LingQiCount += item.Value;
			}
		}
		else
		{
			UIFightPanel.Inst.CacheLingQiController.NowMoveSame = false;
			foreach (KeyValuePair<int, int> item2 in skillCost)
			{
				UIFightLingQiCacheSlot targetTongLingQiSlotWithLimit = UIFightPanel.Inst.CacheLingQiController.GetTargetTongLingQiSlotWithLimit(item2.Value);
				targetTongLingQiSlotWithLimit.LingQiType = (LingQiType)item2.Key;
				UIFightPanel.Inst.PlayerLingQiController.SlotList[item2.Key].LingQiCount -= item2.Value;
				targetTongLingQiSlotWithLimit.LingQiCount += item2.Value;
			}
		}
		UIFightPanel.Inst.CacheLingQiController.NowMoveSame = false;
	}

	public void PlayRunAway()
	{
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		try
		{
			if (!Tools.instance.monstarMag.CanRunAway())
			{
				UIPopTip.Inst.Pop(Tools.getStr("cannotRunAway" + Tools.instance.monstarMag.CanNotRunAwayEvent()));
				return;
			}
			Avatar avatar = Tools.instance.getPlayer();
			if (avatar.buffmag.HasBuffSeid(183))
			{
				UIPopTip.Inst.Pop("无法逃跑");
				return;
			}
			UnityAction onOK = (UnityAction)delegate
			{
				if (Tools.instance.monstarMag.shouldReloadSaveHp())
				{
					avatar.HP = Tools.instance.monstarMag.gameStartHP;
				}
				GlobalValue.SetTalk(1, 4, "RoundManager.PlayRunAway");
				Tools.instance.AutoSeatSeaRunAway();
				if (Tools.instance.getPlayer().NowFuBen == "" || Tools.instance.FinalScene.Contains("Sea"))
				{
					Tools.instance.CanShowFightUI = 1;
				}
				if (GlobalValue.GetTalk(0, "RoundManager.PlayRunAway") > 0 || avatar.fubenContorl.isInFuBen() || Tools.instance.FinalScene.Contains("Sea"))
				{
					Tools.instance.loadMapScenes(Tools.instance.FinalScene);
				}
				else
				{
					Tools.instance.loadMapScenes("AllMaps");
				}
			};
			if (Tools.instance.monstarMag.FightType == StartFight.FightEnumType.天劫秘术领悟)
			{
				USelectBox.Show("是否放弃本次感悟？", onOK);
			}
			else if (TouXiangTypes.Contains(Tools.instance.monstarMag.FightType))
			{
				USelectBox.Show("是否确认投降？", onOK);
			}
			else if (avatar.dunSu - (int)jsonData.instance.AvatarJsonData[string.Concat(Tools.instance.MonstarID)]["dunSu"].n > 0)
			{
				USelectBox.Show("是否确认遁走？", onOK);
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

	public void setSkillChoicOk()
	{
		ChoiceSkill = null;
		NowSkillUsedLingQiSum = -1;
	}

	public void SetChoiceSkill(ref GUIPackage.Skill skill)
	{
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Expected O, but got Unknown
		//IL_00cb: Expected O, but got Unknown
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		CardMag cardMag = avatar.cardMag;
		if (skill.CanUse(avatar, KBEngineApp.app.entities[11]) != SkillCanUseType.可以使用)
		{
			ChoiceSkill = null;
			UIFightPanel.Inst.CacheLingQiController.MoveAllLingQiToPlayer();
			UIFightPanel.Inst.CacheLingQiController.ChangeCacheSlotNumber(0);
			UIFightPanel.Inst.CancelSkillHighlight();
			avatar.onCrystalChanged(cardMag);
			return;
		}
		UIFightPanel.Inst.CacheLingQiController.MoveAllLingQiToPlayer();
		UIFightPanel.Inst.UIFightState = UIFightState.释放技能准备灵气阶段;
		UIFightPanel.Inst.FightCenterButtonController.ButtonType = UIFightCenterButtonType.OkCancel;
		_ = skill.skillSameCast;
		UIFightPanel.Inst.FightCenterButtonController.SetOkCancelEvent((UnityAction)delegate
		{
			if (UseSkill())
			{
				UIFightPanel.Inst.CacheLingQiController.ChangeCacheSlotNumber(0);
				UIFightPanel.Inst.CancelSkillHighlight();
			}
			else
			{
				UIFightPanel.Inst.UIFightState = UIFightState.释放技能准备灵气阶段;
			}
		}, (UnityAction)delegate
		{
			ChoiceSkill = null;
			UIFightPanel.Inst.CacheLingQiController.MoveAllLingQiToPlayer();
			UIFightPanel.Inst.CacheLingQiController.ChangeCacheSlotNumber(0);
			UIFightPanel.Inst.CancelSkillHighlight();
			UIFightPanel.Inst.FightCenterButtonController.ButtonType = UIFightCenterButtonType.EndRound;
			UIFightPanel.Inst.UIFightState = UIFightState.自己回合普通状态;
		});
		bool flag = true;
		if (skill == ChoiceSkill)
		{
			flag = false;
		}
		else
		{
			CalcTongLingQiKeNeng(avatar, skill);
		}
		ChoiceSkill = skill;
		Dictionary<int, int> skillCast = skill.getSkillCast(avatar);
		UIFightPanel.Inst.CacheLingQiController.ChangeCacheSlotNumber(skillCast.Count + skill.skillSameCast.Count);
		UIFightPanel.Inst.CacheLingQiController.SetLingQiLimit(skillCast, skill.skillSameCast);
		bool flag2 = false;
		if (flag && avatar.FightCostRecord.HasField(skill.skill_ID.ToString()))
		{
			JSONObject jSONObject = avatar.FightCostRecord[skill.skill_ID.ToString()];
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			for (int i = 0; i < 6; i++)
			{
				if (jSONObject[i.ToString()].I > 0)
				{
					dictionary.Add(i, jSONObject[i.ToString()].I);
				}
			}
			bool flag3 = true;
			foreach (KeyValuePair<int, int> item in dictionary)
			{
				if (skillCast.ContainsKey(item.Key))
				{
					if (avatar.cardMag.HasNoEnoughNum(item.Key, item.Value + skillCast[item.Key]))
					{
						flag3 = false;
						break;
					}
				}
				else if (avatar.cardMag.HasNoEnoughNum(item.Key, item.Value))
				{
					flag3 = false;
					break;
				}
			}
			if (flag3)
			{
				flag2 = true;
				MoveLingQiToCacheFromPlayer(skillCast, LingQiCacheType.DontMove);
				MoveLingQiToCacheFromPlayer(dictionary, LingQiCacheType.None);
			}
		}
		if (!flag2)
		{
			Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
			for (int j = 0; j < 6; j++)
			{
				dictionary2.Add(j, avatar.cardMag[j]);
			}
			foreach (KeyValuePair<int, int> item2 in skillCast)
			{
				dictionary2[item2.Key] -= item2.Value;
			}
			MoveLingQiToCacheFromPlayer(skillCast, LingQiCacheType.DontMove);
			if (skill.skillSameCast.Count > 0)
			{
				bool flag4 = true;
				for (int k = 1; k < skill.skillSameCast.Count; k++)
				{
					if (skill.skillSameCast[0] != skill.skillSameCast[k])
					{
						flag4 = false;
						break;
					}
				}
				Dictionary<int, int> dictionary3 = new Dictionary<int, int>();
				int num = 0;
				bool flag5;
				do
				{
					int[] array = ((!flag4) ? lingQiKeNengXingPaiLie[skill.skillSameCast.Count][choiceSkillCanUseLingQiIndexList[clickSkillChangeLingQiIndex]] : lingQiKeNengXingZuHe[skill.skillSameCast.Count][choiceSkillCanUseLingQiIndexList[clickSkillChangeLingQiIndex]]);
					clickSkillChangeLingQiIndex++;
					if (clickSkillChangeLingQiIndex >= choiceSkillCanUseLingQiIndexList.Count)
					{
						clickSkillChangeLingQiIndex = 0;
					}
					dictionary3.Clear();
					string text = "";
					for (int l = 0; l < array.Length; l++)
					{
						dictionary3.Add(array[l], skill.skillSameCast[l]);
						text += $"{array[l]}x{skill.skillSameCast[l]} ";
					}
					flag5 = true;
					foreach (KeyValuePair<int, int> item3 in dictionary3)
					{
						if (UIFightPanel.Inst.PlayerLingQiController.SlotList[item3.Key].LingQiCount < item3.Value)
						{
							flag5 = false;
						}
					}
					num++;
				}
				while (!flag5 && num < 100);
				if (num >= 100)
				{
					Debug.LogError((object)"在计算灵气可能性时出现异常，保底循环超过100次");
				}
				MoveLingQiToCacheFromPlayer(dictionary3, LingQiCacheType.None);
			}
		}
		avatar.onCrystalChanged(cardMag);
	}

	private static void InitLingQiKeNengXing()
	{
		if (lingQiKeNengXingZuHe == null)
		{
			lingQiKeNengXingZuHe = new Dictionary<int, List<int[]>>();
			int[] t = new int[5] { 0, 1, 2, 3, 4 };
			for (int i = 1; i <= 5; i++)
			{
				List<int[]> combination = PermutationAndCombination<int>.GetCombination(t, i);
				lingQiKeNengXingZuHe.Add(i, combination);
			}
		}
		if (lingQiKeNengXingPaiLie == null)
		{
			lingQiKeNengXingPaiLie = new Dictionary<int, List<int[]>>();
			int[] t2 = new int[5] { 0, 1, 2, 3, 4 };
			for (int j = 1; j <= 5; j++)
			{
				List<int[]> permutation = PermutationAndCombination<int>.GetPermutation(t2, j);
				lingQiKeNengXingPaiLie.Add(j, permutation);
			}
		}
	}

	public void CalcTongLingQiKeNeng(Avatar avatar, GUIPackage.Skill skill)
	{
		if (skill.skillSameCast.Count == 0)
		{
			return;
		}
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		for (int i = 0; i < 5; i++)
		{
			dictionary.Add(i, avatar.cardMag.getCardTypeNum(i));
			if (skill.skillCast.ContainsKey(i))
			{
				dictionary[i] -= skill.skillCast[i];
			}
		}
		choiceSkillCanUseLingQiIndexList = new List<int>();
		Dictionary<int, int> skillSameCast = skill.skillSameCast;
		bool flag = true;
		for (int j = 1; j < skillSameCast.Count; j++)
		{
			if (skillSameCast[0] != skillSameCast[j])
			{
				flag = false;
				break;
			}
		}
		List<int[]> list = ((!flag) ? lingQiKeNengXingPaiLie[skillSameCast.Count] : lingQiKeNengXingZuHe[skillSameCast.Count]);
		for (int k = 0; k < list.Count; k++)
		{
			int[] array = list[k];
			bool flag2 = true;
			for (int l = 0; l < skillSameCast.Count; l++)
			{
				if (dictionary[array[l]] < skillSameCast[l])
				{
					flag2 = false;
					break;
				}
			}
			if (flag2)
			{
				choiceSkillCanUseLingQiIndexList.Add(k);
			}
		}
		clickSkillChangeLingQiIndex = Random.Range(0, choiceSkillCanUseLingQiIndexList.Count);
		Debug.Log((object)$"对于技能{skill.skill_Name}，玩家有{choiceSkillCanUseLingQiIndexList.Count}种同系灵气可能性");
	}

	public int GetLingQiSum(Dictionary<int, int> a)
	{
		int num = 0;
		foreach (KeyValuePair<int, int> item in a)
		{
			num += item.Value;
		}
		return num;
	}

	public int GetLingQiSum(Dictionary<LingQiType, int> a)
	{
		int num = 0;
		foreach (KeyValuePair<LingQiType, int> item in a)
		{
			num += item.Value;
		}
		return num;
	}

	public bool UseSkill(string uuid = "", bool showTip = true)
	{
		Buff._NeiShangLoopCount = 0;
		UIFightPanel.Inst.UIFightState = UIFightState.自己回合普通状态;
		if (ChoiceSkill != null)
		{
			Dictionary<LingQiType, int> nowCacheLingQi = UIFightPanel.Inst.CacheLingQiController.GetNowCacheLingQi();
			Dictionary<int, int> skillCast = ChoiceSkill.getSkillCast(Tools.instance.getPlayer());
			int lingQiSum = GetLingQiSum(skillCast);
			int lingQiSum2 = GetLingQiSum(ChoiceSkill.skillSameCast);
			int lingQiSum3 = GetLingQiSum(nowCacheLingQi);
			if (lingQiSum + lingQiSum2 != lingQiSum3)
			{
				if (showTip)
				{
					UIPopTip.Inst.Pop("选择的灵气与技能消耗不一致");
					Debug.Log((object)"选择的灵气与技能消耗不一致1");
				}
				return false;
			}
			foreach (KeyValuePair<int, int> item in ChoiceSkill.getSkillCast(Tools.instance.getPlayer()))
			{
				LingQiType key = (LingQiType)item.Key;
				if (nowCacheLingQi.ContainsKey(key))
				{
					if (nowCacheLingQi[key] >= item.Value)
					{
						nowCacheLingQi[key] -= item.Value;
						continue;
					}
					if (showTip)
					{
						UIPopTip.Inst.Pop("选择的灵气与技能消耗不一致");
						Debug.Log((object)"选择的灵气与技能消耗不一致2");
					}
					return false;
				}
				if (showTip)
				{
					UIPopTip.Inst.Pop("选择的灵气与技能消耗不一致");
					Debug.Log((object)"选择的灵气与技能消耗不一致3");
				}
				return false;
			}
			Dictionary<LingQiType, int> dictionary = new Dictionary<LingQiType, int>();
			foreach (KeyValuePair<int, int> item2 in ChoiceSkill.skillSameCast)
			{
				bool flag = false;
				foreach (KeyValuePair<LingQiType, int> item3 in nowCacheLingQi)
				{
					if (item3.Value == item2.Value && !dictionary.ContainsKey(item3.Key))
					{
						dictionary.Add(item3.Key, item2.Value);
						flag = true;
						nowCacheLingQi[item3.Key] -= item2.Value;
						break;
					}
				}
				if (!flag)
				{
					if (showTip)
					{
						UIPopTip.Inst.Pop("选择的灵气与技能消耗不一致");
						Debug.Log((object)"选择的灵气与技能消耗不一致4");
					}
					return false;
				}
			}
			foreach (KeyValuePair<LingQiType, int> item4 in nowCacheLingQi)
			{
				if (item4.Value != 0)
				{
					if (showTip)
					{
						UIPopTip.Inst.Pop("选择的灵气与技能消耗不一致");
						Debug.Log((object)"选择的灵气与技能消耗不一致5");
					}
					return false;
				}
			}
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			if (avatar.isPlayer())
			{
				int[] array = new int[6];
				foreach (KeyValuePair<LingQiType, int> item5 in UIFightPanel.Inst.CacheLingQiController.GetNowCacheLingQi())
				{
					array[(int)item5.Key] = item5.Value;
				}
				foreach (KeyValuePair<int, int> item6 in skillCast)
				{
					array[item6.Key] -= item6.Value;
				}
				JSONObject jSONObject = new JSONObject(JSONObject.Type.OBJECT);
				for (int i = 0; i < 6; i++)
				{
					jSONObject.SetField(i.ToString(), array[i]);
				}
				avatar.FightCostRecord.SetField(ChoiceSkill.skill_ID.ToString(), jSONObject);
			}
			NowSkillUsedLingQiSum = lingQiSum3;
			avatar.spell.spellSkill(ChoiceSkill.skill_ID, uuid);
		}
		UIFightPanel.Inst.FightCenterButtonController.ButtonType = UIFightCenterButtonType.EndRound;
		setSkillChoicOk();
		return true;
	}

	private void Start()
	{
		gameStart();
	}

	private void OnDestroy()
	{
		instance = null;
		Event.deregisterOut(this);
		YSFuncList.Ints.Clear();
		if (newNpcFightManager != null)
		{
			newNpcFightManager.Dispose();
		}
		if (KBEngineApp.app != null)
		{
			KBEngineApp.app.entities.Remove(11);
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			if (avatar == null)
			{
				return;
			}
			avatar.spell.removeAllBuff();
			avatar.buffs.Clear();
			avatar.SkillSeidFlag.Clear();
			avatar.BuffSeidFlag.Clear();
			avatar.skill.Clear();
			avatar.StaticSkill.Clear();
			avatar.NowRoundUsedCard.Clear();
			avatar.UsedSkills.Clear();
			avatar.fightTemp = new FightTempValue();
			avatar.setHP(Mathf.Clamp(avatar.HP, 0, avatar.HP_Max));
		}
		MessageMag.Instance.Remove("Fight_CardChange", UpdateCard);
	}

	public void endRound(Entity _avater)
	{
		Avatar avatar = (Avatar)_avater;
		if (avatar.isPlayer())
		{
			EventFightTalk("RealClickEndRound", null);
		}
		avatar.state = 2;
		if (avatar.isPlayer())
		{
			UIFightPanel.Inst.UIFightState = UIFightState.敌人回合;
			UIFightPanel.Inst.FightCenterTip.HideTip();
			UIFightPanel.Inst.FightCenterButtonController.ButtonType = UIFightCenterButtonType.None;
		}
		avatar.spell.onBuffTickByType(1, new List<int> { 0 });
		avatar.spell.onRemoveBuffByType(7);
		avatar.spell.onRemoveBuffByType(3);
		avatar.spell.onRemoveBuffByType(4);
		avatar.spell.onRemoveBuffByType(13);
		avatar.spell.onBuffTickByType(34, new List<int> { 0 });
		if (avatar.isPlayer())
		{
			Avatar avater = (Avatar)KBEngineApp.app.entities[11];
			startRound(avater);
		}
		else
		{
			Avatar avater2 = (Avatar)KBEngineApp.app.entities[10];
			startRound(avater2);
		}
		avatar.onCrystalChanged(avatar.cardMag);
		avatar.fightTemp.ResetRound(avatar);
		if (Tools.instance.monstarMag.FightType == StartFight.FightEnumType.JieYing && avatar.isPlayer() && Tools.instance.getPlayer().buffmag.HasBuff(3101))
		{
			((Component)instance).gameObject.GetComponent<JieYingManager>().XinMoAttack();
		}
		if (Tools.instance.monstarMag.FightType != StartFight.FightEnumType.ZhuJi || !avatar.isPlayer())
		{
			return;
		}
		List<List<int>> buffByID = avatar.buffmag.getBuffByID(3978);
		if (buffByID.Count > 0)
		{
			if (buffByID[0][1] == 0)
			{
				ZhuJiManager.inst.checkState();
			}
			else
			{
				ZhuJiManager.inst.ShengYuHuiHe.text = "剩余回合 " + buffByID[0][1].ToCNNumber();
			}
		}
	}

	public int getListSum(CardMag list)
	{
		return list.getCardNum();
	}

	public void autoRemoveCard(Avatar avater)
	{
		avater.spell.onBuffTickByType(44);
		if (avater.SkillSeidFlag.ContainsKey(24) && avater.SkillSeidFlag[24][0] == 1)
		{
			avater.SkillSeidFlag[24][0] = 0;
			endRound(avater);
		}
		else
		{
			if (avater.buffmag.HasBuffSeid(99))
			{
				return;
			}
			List<int> list = new List<int>();
			avater.spell.onBuffTickByType(20, list);
			int listSum = getListSum(avater.cardMag);
			if (listSum > avater.NowCard)
			{
				int randomCount = listSum - (int)avater.NowCard;
				int[] randomRemoveLingQi = GetRandomRemoveLingQi(avater, randomCount);
				for (int i = 0; i < randomRemoveLingQi.Length; i++)
				{
					if (randomRemoveLingQi[i] > 0)
					{
						avater.AbandonCryStal(i, randomRemoveLingQi[i]);
					}
				}
			}
			list.Add(listSum - (int)avater.NowCard);
			avater.spell.onBuffTickByType(18, list);
			avater.spell.onBuffTickByType(26, list);
		}
	}

	private int getRealRemoveNum(int sum, int removeNum)
	{
		int num = 0;
		if (sum >= removeNum)
		{
			return removeNum;
		}
		return sum;
	}

	public int[] GetRandomRemoveLingQi(Avatar avater, int randomCount)
	{
		int cardNum = avater.cardMag.getCardNum();
		if (cardNum < randomCount)
		{
			Debug.LogError((object)$"玩家灵气数量{cardNum}小于要随机的数量{randomCount}");
			randomCount = cardNum;
		}
		List<int> list = avater.cardMag.ToListInt32();
		int[] array = new int[6];
		List<int> list2 = new List<int>();
		for (int i = 0; i < 6; i++)
		{
			if (list[i] > 0)
			{
				list2.Add(i);
			}
		}
		for (int j = 0; j < randomCount; j++)
		{
			int index = Random.Range(0, list2.Count);
			int num = list2[index];
			array[num]++;
			list[num]--;
			if (list[num] <= 0)
			{
				list2.Remove(num);
			}
		}
		return array;
	}

	public void removeCard(Avatar avater, int removeNum)
	{
		int realRemoveNum = getRealRemoveNum(getListSum(avater.cardMag), removeNum);
		int[] randomRemoveLingQi = GetRandomRemoveLingQi(avater, realRemoveNum);
		for (int i = 0; i < 6; i++)
		{
			if (randomRemoveLingQi[i] > 0)
			{
				RoundTimeAutoRemoveCard(avater, i, randomRemoveLingQi[i]);
			}
		}
		avater.onCrystalChanged(avater.cardMag);
	}

	public void removeCard(Avatar avater, int removeNum, int removeType)
	{
		int realRemoveNum = getRealRemoveNum(getListSum(avater.cardMag), removeNum);
		RoundTimeAutoRemoveCard(avater, removeType, realRemoveNum);
		avater.onCrystalChanged(avater.cardMag);
	}

	public void ExchengCard(Avatar avater, card _card, int type)
	{
		avater.onCrystalChanged(avater.cardMag);
	}

	public void RoundTimeAutoRemoveCard(Avatar avater, int removeType, int count = 1)
	{
		avater.AbandonCryStal(removeType, count);
		if (NowUseLingQiType == UseLingQiType.释放技能后消耗)
		{
			List<int> list = new List<int>();
			list.Add(1);
			list.Add(removeType);
			for (int i = 0; i < count; i++)
			{
				avater.spell.onBuffTickByType(19, list);
			}
		}
		if (avater.isPlayer())
		{
			UIFightPanel.Inst.PlayerLingQiController.SlotList[removeType].LingQiCount -= count;
		}
	}

	public static void EventFightTalk(string name, EventDelegate del, EventDelegate end = null)
	{
		if (Tools.instance.monstarMag.FightTalkID != 0)
		{
			Flowchart fightTalk = instance.FightTalk;
			if (fightTalk.HasBlock(name))
			{
				del?.Execute();
				fightTalk.ExecuteBlock(name);
				end?.Execute();
			}
		}
	}

	public void PlayerEndRound(bool canCancel = true)
	{
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Expected O, but got Unknown
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Expected O, but got Unknown
		//IL_017b: Expected O, but got Unknown
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		avatar.spell.onBuffTickByType(44);
		if (avatar.SkillSeidFlag.ContainsKey(24) && avatar.SkillSeidFlag[24][0] == 1)
		{
			avatar.SkillSeidFlag[24][0] = 0;
			endRound(avatar);
			return;
		}
		bool flag = false;
		EventFightTalk("ClickEndRound", null, new EventDelegate(delegate
		{
			Flowchart fightTalk = instance.FightTalk;
			if (fightTalk.HasVariable("WhetherCanEndRound"))
			{
				flag = !fightTalk.GetBooleanVariable("WhetherCanEndRound");
			}
		}));
		if (flag)
		{
			return;
		}
		UIFightPanel.Inst.FightCenterButtonController.ButtonType = UIFightCenterButtonType.None;
		if (UIFightPanel.Inst.PlayerLingQiController.GetPlayerLingQiSum() <= avatar.NowCard || avatar.buffmag.HasBuffSeid(99))
		{
			avatar.spell.onRemoveBuffByType(26);
			endRound(avatar);
			return;
		}
		UIFightPanel.Inst.UIFightState = UIFightState.回合结束弃置灵气阶段;
		if (canCancel)
		{
			UIFightPanel.Inst.FightCenterButtonController.ButtonType = UIFightCenterButtonType.OkCancel;
		}
		else
		{
			UIFightPanel.Inst.FightCenterButtonController.ButtonType = UIFightCenterButtonType.OnlyOK;
		}
		UIFightPanel.Inst.CacheLingQiController.ChangeCacheSlotNumber(6);
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		for (int i = 0; i < 6; i++)
		{
			dictionary.Add(i, int.MaxValue);
		}
		UIFightPanel.Inst.CacheLingQiController.SetLingQiLimit(new Dictionary<int, int>(), dictionary);
		if (canCancel)
		{
			UIFightPanel.Inst.FightCenterButtonController.SetOkCancelEvent(new UnityAction(OnPlayerEndRoundQiZhiLingQiOKClick), new UnityAction(OnPlayerEndRoundQiZhiLingQiCacelClick));
		}
		else
		{
			UIFightPanel.Inst.FightCenterButtonController.SetOnlyOKEvent(new UnityAction(OnPlayerEndRoundQiZhiLingQiOKClick));
		}
		UIFightPanel.Inst.FightCenterTip.ShowYiSan(UIFightPanel.Inst.NeedYiSanCount);
		UIPopTip.Inst.Pop("点击选择你要消散的灵气");
	}

	private void OnPlayerEndRoundQiZhiLingQiOKClick()
	{
		Avatar player = PlayerEx.Player;
		int num = player.cardMag.getCardNum() - (int)player.NowCard;
		int cacheLingQiSum = UIFightPanel.Inst.CacheLingQiController.GetCacheLingQiSum();
		if (num != cacheLingQiSum)
		{
			UIPopTip.Inst.Pop($"还需要消散{num - cacheLingQiSum}点灵气");
			return;
		}
		Dictionary<LingQiType, int> nowCacheLingQi = UIFightPanel.Inst.CacheLingQiController.GetNowCacheLingQi();
		bool flag = true;
		foreach (KeyValuePair<LingQiType, int> item in nowCacheLingQi)
		{
			if (player.cardMag.getCardTypeNum((int)item.Key) < item.Value)
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			List<int> list = new List<int>();
			player.spell.onBuffTickByType(20, list);
			int num2 = 0;
			foreach (KeyValuePair<LingQiType, int> item2 in nowCacheLingQi)
			{
				num2 += item2.Value;
				player.AbandonCryStal((int)item2.Key, item2.Value);
			}
			list.Add(num2);
			player.spell.onBuffTickByType(18, list);
			UIFightPanel.Inst.UIFightState = UIFightState.自己回合普通状态;
			UIFightPanel.Inst.CacheLingQiController.DestoryAllLingQi();
			UIFightPanel.Inst.PlayerLingQiController.ResetPlayerLingQiCount();
			UIFightPanel.Inst.CacheLingQiController.ChangeCacheSlotNumber(0);
			endRound(player);
		}
		else
		{
			UIPopTip.Inst.Pop("没有足够的灵气");
		}
	}

	private void OnPlayerEndRoundQiZhiLingQiCacelClick()
	{
		UIFightPanel.Inst.CacheLingQiController.MoveAllLingQiToPlayer();
		UIFightPanel.Inst.CacheLingQiController.ChangeCacheSlotNumber(0);
		UIFightPanel.Inst.FightCenterButtonController.ButtonType = UIFightCenterButtonType.EndRound;
		UIFightPanel.Inst.UIFightState = UIFightState.自己回合普通状态;
		UIFightPanel.Inst.FightCenterTip.HideTip();
	}

	public card drawCardCreatSpritAndAddCrystal(Avatar avatar, int type)
	{
		card result = avatar.addCrystal(type);
		if (avatar.isPlayer())
		{
			UIFightPanel.Inst.PlayerLingQiController.SlotList[type].LingQiCount++;
			PlayerCurRoundDrawCardNum++;
			return result;
		}
		NpcCurRoundDrawCardNum++;
		return result;
	}

	public void DrawCardCreatSpritAndAddCrystal(Avatar avatar, int type, int count = 1)
	{
		avatar.addCrystal(type, count);
		if (avatar.isPlayer())
		{
			UIFightPanel.Inst.PlayerLingQiController.SlotList[type].LingQiCount += count;
			PlayerCurRoundDrawCardNum += count;
		}
		else
		{
			NpcCurRoundDrawCardNum += count;
		}
	}

	public void RandomDrawCard(Avatar avatar, int count = 1)
	{
		int[] randomLingQiTypes = GetRandomLingQiTypes(avatar, count);
		for (int i = 0; i < 6; i++)
		{
			if (randomLingQiTypes[i] > 0)
			{
				DrawCardCreatSpritAndAddCrystal(avatar, i, randomLingQiTypes[i]);
			}
		}
	}

	private card drawCardRealize(Avatar avatar, int lingQiType)
	{
		List<int> list = new List<int>();
		list.Add(1);
		list.Add(0);
		avatar.spell.onBuffTickByType(24, list);
		return drawCardCreatSpritAndAddCrystal(avatar, lingQiType);
	}

	private void DrawCardRealize(Avatar avatar, int lingQiType)
	{
		List<int> list = new List<int>();
		list.Add(1);
		list.Add(0);
		avatar.spell.onBuffTickByType(24, list);
		DrawCardCreatSpritAndAddCrystal(avatar, lingQiType);
	}

	public card drawCard(Avatar avatar)
	{
		int randomLingQiType = GetRandomLingQiType(avatar);
		return drawCardRealize(avatar, randomLingQiType);
	}

	public void DrawCard(Avatar avatar)
	{
		int randomLingQiType = GetRandomLingQiType(avatar);
		DrawCardRealize(avatar, randomLingQiType);
	}

	public void DrawCard(Avatar avatar, int lingQiType)
	{
		DrawCardRealize(avatar, lingQiType);
	}

	public int getRemoveNum(Avatar avatar)
	{
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		foreach (KeyValuePair<int, int> item in avatar.DrawWeight)
		{
			dictionary[item.Key] = item.Value;
		}
		List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>(dictionary);
		list.Sort((KeyValuePair<int, int> s1, KeyValuePair<int, int> s2) => s1.Value.CompareTo(s2.Value));
		for (int i = 0; i < list.Count; i++)
		{
			if (avatar.cardMag[list[i].Key] - 1 >= 0)
			{
				return list[i].Key;
			}
		}
		return -1;
	}

	public int GetRandomLingQiType(Avatar avatar)
	{
		if (Tools.instance.monstarMag.FightCardID != 0 && avatar.isPlayer())
		{
			int card = instance.FightDrawCard.getCard();
			if (card != -1)
			{
				return card;
			}
		}
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		for (int i = 0; i < 6; i++)
		{
			dictionary.Add(i, 0);
		}
		int num = 0;
		foreach (int item in avatar.GetLingGeng)
		{
			if (num > 5)
			{
				break;
			}
			dictionary[num] += item;
			num++;
		}
		foreach (KeyValuePair<int, int> item2 in avatar.DrawWeight)
		{
			dictionary[item2.Key] += item2.Value;
		}
		if (avatar.SkillSeidFlag.ContainsKey(13))
		{
			foreach (KeyValuePair<int, int> item3 in avatar.SkillSeidFlag[13])
			{
				dictionary[item3.Key] += item3.Value;
			}
		}
		int lingQiSum = GetLingQiSum(dictionary);
		int num2 = Random.Range(0, int.MaxValue) % lingQiSum;
		int result = 0;
		int num3 = 0;
		foreach (KeyValuePair<int, int> item4 in dictionary)
		{
			num3 += item4.Value;
			if (num3 > num2)
			{
				result = item4.Key;
				break;
			}
		}
		return result;
	}

	public int[] GetRandomLingQiTypes(Avatar avatar, int count = 1)
	{
		int[] array = new int[6];
		int num = 0;
		if (Tools.instance.monstarMag.FightCardID != 0 && avatar.isPlayer())
		{
			int num2 = 0;
			while (num2 != -1 && num < count)
			{
				num2 = instance.FightDrawCard.getCard();
				if (num2 != -1)
				{
					array[num2]++;
					num++;
				}
			}
		}
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		for (int i = 0; i < 6; i++)
		{
			dictionary.Add(i, 0);
		}
		int num3 = 0;
		foreach (int item in avatar.GetLingGeng)
		{
			if (num3 > 5)
			{
				break;
			}
			dictionary[num3] += item;
			num3++;
		}
		foreach (KeyValuePair<int, int> item2 in avatar.DrawWeight)
		{
			dictionary[item2.Key] += item2.Value;
		}
		if (avatar.SkillSeidFlag.ContainsKey(13))
		{
			foreach (KeyValuePair<int, int> item3 in avatar.SkillSeidFlag[13])
			{
				dictionary[item3.Key] += item3.Value;
			}
		}
		int lingQiSum = GetLingQiSum(dictionary);
		while (num < count)
		{
			int num4 = Random.Range(0, int.MaxValue) % lingQiSum;
			int num5 = 0;
			foreach (KeyValuePair<int, int> item4 in dictionary)
			{
				num5 += item4.Value;
				if (num5 > num4)
				{
					array[item4.Key]++;
					num++;
					break;
				}
			}
		}
		return array;
	}

	public void startRound(Entity _avater)
	{
		try
		{
			Avatar avatar = (Avatar)_avater;
			Debug.Log((object)(avatar.name + " 回合开始"));
			if (avatar.isPlayer())
			{
				UIFightPanel.Inst.UIFightState = UIFightState.自己回合普通状态;
				UIFightPanel.Inst.FightCenterButtonController.ButtonType = UIFightCenterButtonType.EndRound;
				PlayerCurRoundDrawCardNum = 0;
			}
			else
			{
				UIFightPanel.Inst.FightCenterButtonController.ButtonType = UIFightCenterButtonType.None;
				UIFightPanel.Inst.UIFightState = UIFightState.敌人回合;
				NpcCurRoundDrawCardNum = 0;
			}
			if (avatar.state == 1)
			{
				return;
			}
			avatar.NowRoundUsedCard.Clear();
			avatar.state = 3;
			if (avatar.isPlayer())
			{
				StaticRoundNum++;
				if (PlayerFightEventProcessor != null)
				{
					PlayerFightEventProcessor.OnUpdateRound();
				}
				UIFightPanel.Inst.FightRoundCount.ShowRuond(StaticRoundNum);
			}
			avatar.spell.onBuffTickByType(2, new List<int>());
			avatar.spell.onRemoveBuffByType(5);
			avatar.spell.onRemoveBuffByType(6);
			avatar.spell.onRemoveBuffByType(14);
			int nowDrawCardNum = avatar.NowDrawCardNum;
			List<int> list = new List<int>();
			list.Add(nowDrawCardNum);
			list.Add(0);
			list.Add(-123);
			list.Add(0);
			avatar.spell.onBuffTickByType(3, list);
			nowDrawCardNum = list[0];
			if (avatar.buffmag.HasBuffSeid(8))
			{
				nowDrawCardNum = (int)Math.Ceiling(Convert.ToDouble((float)nowDrawCardNum / 2f));
				avatar.spell.removeBuff(avatar.buffmag.getBuffBySeid(8)[0]);
			}
			if (list[3] == 1)
			{
				nowDrawCardNum = 0;
			}
			Debug.Log((object)$"{avatar.name} 回合开始吸收{nowDrawCardNum}点灵气");
			RandomDrawCard(avatar, nowDrawCardNum);
			avatar.onCrystalChanged(avatar.cardMag);
			avatar.spell.onBuffTickByType(4, list);
			if (list[1] == 1)
			{
				if (avatar.isPlayer())
				{
					instance.PlayerEndRound(canCancel: false);
				}
				else
				{
					avatar.AvatarEndRound();
				}
			}
			foreach (GUIPackage.Skill item in avatar.skill)
			{
				if (avatar.SkillSeidFlag.ContainsKey(5) && avatar.SkillSeidFlag[5].ContainsKey(item.skill_ID) && avatar.SkillSeidFlag[5][item.skill_ID] == 1)
				{
					item.CurCD = 50000f;
				}
				else if (item.weaponuuid != null && item.weaponuuid != "" && avatar.isPlayer())
				{
					if (WeaponSkillList.ContainsKey(item.weaponuuid))
					{
						WeaponSkillList[item.weaponuuid]--;
						if (WeaponSkillList[item.weaponuuid] > 0)
						{
							item.CurCD = 50000f;
						}
						else
						{
							item.CurCD = 0f;
						}
					}
					else
					{
						item.CurCD = 0f;
					}
				}
				else if (avatar.SkillSeidFlag.ContainsKey(29) && avatar.SkillSeidFlag[29].ContainsKey(item.skill_ID) && avatar.SkillSeidFlag[29][item.skill_ID] >= 1)
				{
					avatar.SkillSeidFlag[29][item.skill_ID]--;
					if (avatar.SkillSeidFlag[29][item.skill_ID] > 0)
					{
						item.CurCD = 50000f;
					}
					else
					{
						item.CurCD = 0f;
					}
				}
				else
				{
					item.CurCD = 0f;
				}
			}
			UIFightPanel.Inst.RefreshCD();
		}
		catch (Exception arg)
		{
			Debug.LogError((object)$"startRound:\n{arg}");
		}
	}

	public void chengeCrystal()
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		avatar.onCrystalChanged(avatar.cardMag);
		avatar.OtherAvatar.onCrystalChanged(avatar.OtherAvatar.cardMag);
	}

	public void eventChengeCrystal()
	{
		((MonoBehaviour)this).Invoke("chengeCrystal", 0.05f);
	}

	public Avatar GetMonstar()
	{
		return (Avatar)KBEngineApp.app.entities[11];
	}

	public void initMonstar(int __monstarID)
	{
		Avatar avatar = (Avatar)KBEngineApp.app.entities[11];
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[string.Concat(__monstarID)];
		foreach (JSONObject item2 in jSONObject["skills"].list)
		{
			SkillItem skillItem = new SkillItem();
			skillItem.itemId = (int)item2.n;
			avatar.equipSkillList.Add(skillItem);
		}
		foreach (JSONObject item3 in jSONObject["staticSkills"].list)
		{
			SkillItem skillItem2 = new SkillItem();
			skillItem2.itemId = (int)item3.n;
			avatar.equipStaticSkillList.Add(skillItem2);
		}
		if (jSONObject.HasField("yuanying"))
		{
			int i = jSONObject["yuanying"].I;
			if (i != 0)
			{
				SkillItem skillItem3 = new SkillItem();
				skillItem3.itemId = i;
				skillItem3.itemIndex = 6;
				avatar.equipStaticSkillList.Add(skillItem3);
			}
		}
		if (jSONObject.HasField("HuaShenLingYu") && jSONObject["HuaShenLingYu"].I > 0)
		{
			SkillItem skillItem4 = new SkillItem();
			skillItem4.itemId = jSONObject["HuaShenLingYu"].I;
			avatar.equipSkillList.Add(skillItem4);
			avatar.HuaShenLingYuSkill = new JSONObject(jSONObject["HuaShenLingYu"].I);
		}
		for (int j = 0; j < jSONObject["LingGen"].Count; j++)
		{
			int item = (int)jSONObject["LingGen"][j].n;
			avatar.LingGeng.Add(item);
		}
		if (jSONObject["id"].I < 20000)
		{
			if ((int)jSONObject["equipWeapon"].n > 0)
			{
				avatar.YSequipItem((int)jSONObject["equipWeapon"].n);
			}
			if ((int)jSONObject["equipClothing"].n > 0)
			{
				avatar.YSequipItem((int)jSONObject["equipClothing"].n, 1);
			}
			if ((int)jSONObject["equipRing"].n > 0)
			{
				avatar.YSequipItem((int)jSONObject["equipRing"].n, 2);
			}
		}
		avatar.ZiZhi = (int)jSONObject["ziZhi"].n;
		avatar.dunSu = (int)jSONObject["dunSu"].n;
		avatar.wuXin = (uint)jSONObject["wuXin"].n;
		avatar.shengShi = (int)jSONObject["shengShi"].n;
		avatar.shaQi = (uint)jSONObject["shaQi"].n;
		avatar.shouYuan = (uint)jSONObject["shouYuan"].n;
		avatar.age = (uint)jSONObject["age"].n;
		avatar.HP_Max = (int)jSONObject["HP"].n;
		avatar.HP = (int)jSONObject["HP"].n;
		avatar.level = (ushort)jSONObject["Level"].n;
		avatar.AvatarType = (ushort)jSONObject["AvatarType"].n;
		avatar.name = Tools.instance.Code64ToString(jsonData.instance.AvatarRandomJsonData[string.Concat(__monstarID)]["Name"].str);
		avatar.roleTypeCell = (uint)jSONObject["fightFace"].n;
		avatar.roleType = (uint)jSONObject["face"].n;
		avatar.fightTemp.MonstarID = __monstarID;
		avatar.fightTemp.useAI = true;
		avatar.Sex = (int)jSONObject["SexType"].n;
	}

	public void InitGanYingMonstar(int monstarID)
	{
		initMonstar(monstarID);
		Avatar player = PlayerEx.Player;
		JSONObject jSONObject = jsonData.instance.AvatarRandomJsonData[string.Concat(1)];
		_ = jsonData.instance.AvatarRandomJsonData[string.Concat(monstarID)];
		jsonData.instance.AvatarRandomJsonData.SetField(string.Concat(monstarID), jSONObject.Clone());
		Avatar obj = (Avatar)KBEngineApp.app.entities[11];
		obj.roleTypeCell = player.roleTypeCell;
		obj.roleType = player.roleType;
		obj.Sex = player.Sex;
	}

	public void initXinMoMonstar()
	{
		Tools.instance.MonstarID = 10000;
		Avatar avatar = (Avatar)KBEngineApp.app.entities[11];
		int num = 10000;
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[string.Concat(num)];
		Avatar player = Tools.instance.getPlayer();
		foreach (SkillItem equipSkill in player.equipSkillList)
		{
			SkillItem skillItem = new SkillItem();
			skillItem.itemId = equipSkill.itemId;
			avatar.equipSkillList.Add(skillItem);
		}
		foreach (SkillItem equipStaticSkill in player.equipStaticSkillList)
		{
			SkillItem skillItem2 = new SkillItem();
			skillItem2.itemId = equipStaticSkill.itemId;
			avatar.equipStaticSkillList.Add(skillItem2);
		}
		for (int i = 0; i < jSONObject["LingGen"].Count; i++)
		{
			int item = (int)jSONObject["LingGen"][i].n;
			avatar.LingGeng.Add(item);
		}
		foreach (ITEM_INFO value in player.equipItemList.values)
		{
			avatar.YSequipItem(value.itemId, value.itemIndex);
		}
		avatar.ZiZhi = (int)jSONObject["ziZhi"].n;
		avatar.dunSu = (int)jSONObject["dunSu"].n;
		avatar.wuXin = (uint)jSONObject["wuXin"].n;
		avatar.shengShi = player.shengShi;
		avatar.shaQi = (uint)jSONObject["shaQi"].n;
		avatar.shouYuan = (uint)jSONObject["shouYuan"].n;
		avatar.age = (uint)jSONObject["age"].n;
		avatar.HP_Max = (int)jSONObject["HP"].n;
		avatar.HP = (int)jSONObject["HP"].n;
		JSONObject jSONObject2 = jsonData.instance.AvatarRandomJsonData[string.Concat(1)];
		_ = jsonData.instance.AvatarRandomJsonData[string.Concat(num)];
		jsonData.instance.AvatarRandomJsonData.SetField(string.Concat(num), jSONObject2.Clone());
		avatar.level = (ushort)jSONObject["Level"].n;
		avatar.AvatarType = (ushort)jSONObject["AvatarType"].n;
		avatar.name = player.name;
		avatar.roleTypeCell = player.roleTypeCell;
		avatar.roleType = player.roleType;
		avatar.Sex = player.Sex;
		avatar.fightTemp.MonstarID = num;
		avatar.fightTemp.useAI = true;
	}

	public void gameStart()
	{
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0848: Unknown result type (might be due to invalid IL or missing references)
		//IL_086c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0949: Unknown result type (might be due to invalid IL or missing references)
		//IL_094e: Unknown result type (might be due to invalid IL or missing references)
		//IL_082a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0834: Expected O, but got Unknown
		UIFightPanel.Inst.Clear();
		PlayerUseSkillList = new List<int>();
		NpcUseSkillList = new List<int>();
		UIFightPanel.Inst.FightCenterButtonController.ButtonType = UIFightCenterButtonType.None;
		Tools.instance.getPlayer().fightTemp.LianQiBuffEquipDictionary = new Dictionary<int, JSONObject>();
		Tools.instance.getPlayer().fightTemp.LianQiEquipDictionary = new Dictionary<int, JSONObject>();
		UpdateWeaponCellSum();
		if (Tools.instance.monstarMag.FightTalkID != 0)
		{
			Object obj = Resources.Load("talkPrefab/FightPrefab/FightTalk" + Tools.instance.monstarMag.FightTalkID);
			GameObject val = (GameObject)(object)((obj is GameObject) ? obj : null);
			if ((Object)(object)val != (Object)null)
			{
				GameObject val2 = Object.Instantiate<GameObject>(val);
				FightTalk = val2.GetComponentInChildren<Flowchart>();
			}
		}
		if (Tools.instance.monstarMag.FightCardID != 0)
		{
			Object obj2 = Resources.Load("talkPrefab/FightPrefab/FightCard" + Tools.instance.monstarMag.FightCardID);
			GameObject val3 = (GameObject)(object)((obj2 is GameObject) ? obj2 : null);
			if ((Object)(object)val3 != (Object)null)
			{
				GameObject val4 = Object.Instantiate<GameObject>(val3);
				FightDrawCard = val4.GetComponentInChildren<FightStaticDrawCard>();
			}
		}
		creatAvatar(11, 52, 100, new Vector3(5f, -1.4f, -1f), new Vector3(0f, 0f, -90f));
		KBEngineApp.app.entity_id = 10;
		if (Tools.instance.monstarMag.FightType == StartFight.FightEnumType.XingMo)
		{
			initXinMoMonstar();
		}
		else if (Tools.instance.monstarMag.FightType == StartFight.FightEnumType.天劫秘术领悟)
		{
			InitGanYingMonstar(Tools.instance.MonstarID);
			GlobalValue.SetTalk(0, 4032);
		}
		else
		{
			initMonstar(Tools.instance.MonstarID);
		}
		Avatar player = (Avatar)KBEngineApp.app.player();
		initCrystal(player);
		player.position = new Vector3(-5f, -1.4f, -1f);
		player.direction = new Vector3(0f, 0f, 90f);
		Avatar avatar = (Avatar)KBEngineApp.app.entities[11];
		player.OtherAvatar = avatar;
		avatar.OtherAvatar = player;
		if (Tools.instance.MonstarID > 20000 && NpcJieSuanManager.inst.npcStatus.IsInTargetStatus(Tools.instance.MonstarID, 4))
		{
			avatar.spell.addDBuff(10015);
			avatar.spell.addDBuff(10016);
		}
		SkillBox.inst.initSkillDisplay();
		UIFightLingQiSlot.IgnoreEffect = true;
		foreach (KeyValuePair<int, Entity> entity in KBEngineApp.app.entities)
		{
			Avatar avatar2 = (Avatar)entity.Value;
			initAvatarInfo(avatar2);
			avatar2.fightTemp.showNowHp = avatar2.HP;
			Debug.Log((object)$"{avatar2.name} 游戏开始吸收{avatar2.NowStartCardNum}点灵气");
			RandomDrawCard(avatar2, avatar2.NowStartCardNum);
			if (!avatar2.isPlayer())
			{
				avatar2.onCrystalChanged(avatar2.cardMag);
			}
		}
		UIFightLingQiSlot.IgnoreEffect = false;
		UIFightPanel.Inst.RefreshLingQiCount(show: true);
		if (Tools.instance.monstarMag.FightType != StartFight.FightEnumType.无装备无丹药擂台 && Tools.instance.monstarMag.FightType != StartFight.FightEnumType.天劫秘术领悟)
		{
			Dictionary<int, int> danYaoBuFFDict = player.StreamData.DanYaoBuFFDict;
			if (danYaoBuFFDict.Count > 0)
			{
				foreach (int key in danYaoBuFFDict.Keys)
				{
					player.spell.addBuff(key, danYaoBuFFDict[key]);
				}
				player.StreamData.DanYaoBuFFDict = new Dictionary<int, int>();
			}
		}
		foreach (KeyValuePair<int, int> item in Tools.instance.monstarMag.monstarAddBuff)
		{
			avatar.spell.addDBuff(item.Key, item.Value);
		}
		foreach (KeyValuePair<int, int> item2 in Tools.instance.monstarMag.HeroAddBuff)
		{
			player.spell.addDBuff(item2.Key, item2.Value);
		}
		int danDuLevel = player.GetDanDuLevel();
		if (danDuLevel >= 2)
		{
			player.spell.addDBuff(9999 + danDuLevel);
		}
		if (player.TianFuID.HasField(string.Concat(16)))
		{
			foreach (JSONObject item3 in player.TianFuID["16"].list)
			{
				player.spell.addDBuff((int)item3.n);
			}
		}
		avatar.spell.addDBuff(10000);
		player.spell.addDBuff(10000);
		Tools.instance.monstarMag.monstarAddBuff.Clear();
		Tools.instance.monstarMag.HeroAddBuff.Clear();
		UIFightPanel.Inst.PlayerStatus.Init(player);
		UIFightPanel.Inst.DiRenStatus.Init(avatar);
		GameObject.Find("Canvas_playerHead");
		GameObject.Find("Canvas_target");
		if (Tools.instance.monstarMag.FightType == StartFight.FightEnumType.JieDan)
		{
			setJieDan();
		}
		else if (Tools.instance.monstarMag.FightType == StartFight.FightEnumType.JieYing)
		{
			SetJieYing();
		}
		else if (Tools.instance.monstarMag.FightType == StartFight.FightEnumType.ZhuJi)
		{
			setZhuJi();
		}
		else if (Tools.instance.monstarMag.FightType == StartFight.FightEnumType.HuaShen)
		{
			SetHuaShen();
		}
		else if (Tools.instance.monstarMag.FightType == StartFight.FightEnumType.天劫秘术领悟)
		{
			List<List<int>> buffs2 = new List<List<int>>();
			player.bufflist.ForEach(delegate(List<int> buff)
			{
				if (_BuffJsonData.DataDict[buff[2]].bufftype == 10)
				{
					buffs2.Add(buff);
				}
			});
			buffs2.ForEach(delegate(List<int> buff)
			{
				player.spell.removeBuff(buff);
			});
			PlayerFightEventProcessor = new TianJieMiShuLingWuFightEventProcessor();
			PlayerFightEventProcessor.SetAvatar(player, avatar);
		}
		else if (Tools.instance.monstarMag.FightType == StartFight.FightEnumType.FeiSheng)
		{
			SetFeiSheng();
		}
		else
		{
			List<List<int>> buffs = new List<List<int>>();
			player.bufflist.ForEach(delegate(List<int> buff)
			{
				if (_BuffJsonData.DataDict[buff[2]].bufftype == 10)
				{
					buffs.Add(buff);
				}
			});
			buffs.ForEach(delegate(List<int> buff)
			{
				player.spell.removeBuff(buff);
			});
			if (player.getLevelType() >= 5)
			{
				int i = player.HuaShenLingYuSkill.I;
				GUIPackage.Skill skill = SkillDatebase.instence.Dict[i][1];
				GUIPackage.Skill newSkill = new GUIPackage.Skill(skill.skill_ID, 0, 10);
				((Component)UIFightPanel.Inst.HuaShenLingYuBtn).GetComponent<UTooltipSkillTrigger>().SkillID = i;
				((Component)((Component)UIFightPanel.Inst.HuaShenLingYuBtn).transform.parent).gameObject.SetActive(true);
				((UnityEventBase)UIFightPanel.Inst.HuaShenLingYuBtn.mouseUpEvent).RemoveAllListeners();
				UIFightPanel.Inst.HuaShenLingYuBtn.mouseUpEvent.AddListener((UnityAction)delegate
				{
					if (UIFightPanel.Inst.UIFightState == UIFightState.敌人回合)
					{
						UIPopTip.Inst.Pop("敌方回合，无法使用");
					}
					else if (player.TianJie.HasField("ShengYuTimeValue") && player.TianJie["ShengYuTimeValue"].I >= 100)
					{
						player.skill.Add(newSkill);
						player.spell.spellSkill(newSkill.skill_ID);
						TianJieManager.TianJieJiaSu(100);
						((Component)((Component)UIFightPanel.Inst.HuaShenLingYuBtn).transform.parent).gameObject.SetActive(false);
					}
					else
					{
						UIPopTip.Inst.Pop("天劫临近，无法使用");
					}
				});
			}
			avatar.getLevelType();
			_ = 5;
		}
		((Component)((GameObject)player.renderObj).transform.GetChild(0)).transform.eulerAngles = new Vector3(0f, -90f, 0f);
		player.spell.onBuffTickByType(23, new List<int>());
		avatar.spell.onBuffTickByType(23, new List<int>());
		gameStartAvatar = ((player.dunSu >= avatar.dunSu) ? player : avatar);
		player.onCrystalChanged(player.cardMag);
		if (PlayerFightEventProcessor != null)
		{
			PlayerFightEventProcessor.OnStartFight();
		}
		if (Tools.instance.monstarMag.FightType == StartFight.FightEnumType.FeiSheng)
		{
			gameStartAvatar = avatar;
			TianJieManager.Inst.InitTianJieData();
			object renderObj = player.renderObj;
			AvatarShowHpDamage component = ((GameObject)((renderObj is GameObject) ? renderObj : null)).GetComponent<AvatarShowHpDamage>();
			component.ShowPointTransform = TianJieEffectManager.Inst.PlayerTransform;
			component.UseCustomOffset = true;
			component.CustomOffset = new Vector3(0f, -1.5f, 0f);
			avatar.spell.removeAllBuff();
		}
		((MonoBehaviour)this).StartCoroutine(DelayStart(gameStartAvatar));
	}

	private IEnumerator DelayStart(Avatar gameStartAvatar)
	{
		yield return (object)new WaitForSeconds(0.1f);
		startRound(gameStartAvatar);
	}

	private void TuPoPreDo(int saveBuffType)
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		Avatar player = Tools.instance.getPlayer();
		foreach (KeyValuePair<int, Entity> entity in KBEngineApp.app.entities)
		{
			((GameObject)entity.Value.renderObj).transform.localPosition = new Vector3(-5f, 10f, 0f);
		}
		List<List<int>> removeBuffList = new List<List<int>>();
		player.bufflist.ForEach(delegate(List<int> buff)
		{
			if (_BuffJsonData.DataDict[buff[2]].bufftype != saveBuffType)
			{
				removeBuffList.Add(buff);
			}
		});
		removeBuffList.ForEach(delegate(List<int> buff)
		{
			player.spell.removeBuff(buff);
		});
		player.skill.Clear();
		foreach (UIFightSkillItem fightSkill in UIFightPanel.Inst.FightSkills)
		{
			fightSkill.Clear();
		}
	}

	private void setZhuJi()
	{
		TuPoPreDo(14);
		Avatar player = Tools.instance.getPlayer();
		for (int i = 0; i < ZhuJiManager.inst.ZhuJiSkillList.Count; i++)
		{
			int skillKeyByID = Tools.instance.getSkillKeyByID(ZhuJiManager.inst.ZhuJiSkillList[i], player);
			if (skillKeyByID != -1)
			{
				if (i > 1)
				{
					player.FightAddSkill(skillKeyByID, 0, 6);
				}
				else
				{
					player.FightAddSkill(skillKeyByID, 6, 12);
				}
			}
		}
		if (PlayerEx.GetGameDifficulty() == 游戏难度.极简)
		{
			player.spell.addDBuff(11021);
		}
	}

	private void setJieDan()
	{
		TuPoPreDo(10);
		Avatar player = Tools.instance.getPlayer();
		foreach (SkillItem hasSkill in player.hasSkillList)
		{
			int skillKeyByID = Tools.instance.getSkillKeyByID(hasSkill.itemId, player);
			if (skillKeyByID != -1 && _skillJsonData.DataDict[skillKeyByID].AttackType.Contains(13))
			{
				player.FightAddSkill(skillKeyByID, 6, 12);
			}
		}
		player.spell.addDBuff(4011);
		if (PlayerEx.GetGameDifficulty() == 游戏难度.极简)
		{
			player.spell.addDBuff(11022);
		}
	}

	private void SetJieYing()
	{
		TuPoPreDo(9);
		Avatar player = Tools.instance.getPlayer();
		player.FightAddSkill(11047, 0, 6);
		player.FightAddSkill(11097, 6, 12);
		player.FightAddSkill(11102, 6, 12);
		foreach (SkillItem hasSkill in player.hasSkillList)
		{
			int skillKeyByID = Tools.instance.getSkillKeyByID(hasSkill.itemId, player);
			if (skillKeyByID != -1)
			{
				_skillJsonData skillJsonData = _skillJsonData.DataDict[skillKeyByID];
				if (skillJsonData.AttackType.Contains(16))
				{
					player.FightAddSkill(skillKeyByID, 6, 12);
				}
				if (skillJsonData.AttackType.Contains(14))
				{
					player.FightAddSkill(skillKeyByID, 0, 6);
				}
			}
		}
		player.spell.addDBuff(3097);
		player.spell.addDBuff(3098);
		player.spell.addDBuff(3099);
		player.spell.addDBuff(3100);
		if (PlayerEx.GetGameDifficulty() == 游戏难度.极简)
		{
			player.spell.addDBuff(11023);
		}
	}

	private void SetHuaShen()
	{
		TuPoPreDo(15);
		Avatar player = Tools.instance.getPlayer();
		List<int> list = new List<int> { 1141, 1142, 1143, 1144, 1149 };
		for (int i = 0; i < list.Count; i++)
		{
			int skillKeyByID = Tools.instance.getSkillKeyByID(list[i], player);
			if (skillKeyByID != -1)
			{
				player.FightAddSkill(skillKeyByID, 0, 6);
			}
		}
		List<int> list2 = new List<int> { 1145, 1146, 1147, 1148, 1150 };
		for (int j = 0; j < list2.Count; j++)
		{
			int skillKeyByID2 = Tools.instance.getSkillKeyByID(list2[j], player);
			if (skillKeyByID2 != -1 && PlayerEx.HasSkill(list2[j]))
			{
				player.FightAddSkill(skillKeyByID2, 6, 12);
			}
		}
		if (player.SelectTianFuID.ToList().Contains(314))
		{
			player.spell.addDBuff(9286);
			player.spell.addDBuff(3133, 2);
		}
		if (PlayerEx.GetGameDifficulty() == 游戏难度.极简)
		{
			player.spell.addDBuff(11024);
		}
	}

	private void SetFeiSheng()
	{
		TuPoPreDo(16);
		Avatar player = PlayerEx.Player;
		foreach (JSONObject item in player.TianJieEquipedSkills.list)
		{
			string str = item.Str;
			if (!string.IsNullOrWhiteSpace(str))
			{
				TianJieMiShuData tianJieMiShuData = TianJieMiShuData.DataDict[str];
				int skillKeyByID = Tools.instance.getSkillKeyByID(tianJieMiShuData.Skill_ID, player);
				player.FightAddSkill(skillKeyByID, 0, 12);
			}
		}
		if (PlayerEx.GetGameDifficulty() == 游戏难度.极简)
		{
			player.spell.addDBuff(11025);
		}
		ulong i = (ulong)jsonData.instance.LevelUpDataJsonData[player.level.ToString()]["MaxExp"].i;
		if (player.level == 15)
		{
			if (player.exp < i)
			{
				player.spell.addDBuff(3151);
			}
			if (player.exp == i)
			{
				player.spell.addDBuff(3152);
			}
		}
		int buffid = player.HuaShenWuDao.I + 11025;
		player.spell.addDBuff(buffid);
		if (GlobalValue.Get(1690) == 1)
		{
			player.spell.addDBuff(3153);
			List<int> list = player.spell.addDBuff(3153);
			if (list != null)
			{
				list[1] = 3;
			}
			List<int> list2 = player.spell.addDBuff(3153);
			if (list2 != null)
			{
				list2[1] = 6;
			}
			List<int> list3 = player.spell.addDBuff(3153);
			if (list3 != null)
			{
				list3[1] = 9;
			}
			List<int> list4 = player.spell.addDBuff(3153);
			if (list4 != null)
			{
				list4[1] = 12;
			}
			List<int> list5 = player.spell.addDBuff(3153);
			if (list5 != null)
			{
				list5[1] = 15;
			}
			List<int> list6 = player.spell.addDBuff(3153);
			if (list6 != null)
			{
				list6[1] = 18;
			}
		}
	}

	public void initAvatarInfo(Avatar avatar)
	{
		avatar.onCrystalChanged(avatar.cardMag);
		avatar.state = 4;
		World.instance.onEnterWorld(avatar);
		avatar.addSkill();
		if (avatar.isPlayer() || Tools.instance.monstarMag.FightType == StartFight.FightEnumType.XingMo)
		{
			if (Tools.instance.monstarMag.FightType != StartFight.FightEnumType.ZhuJi && Tools.instance.monstarMag.FightType != StartFight.FightEnumType.JieDan && Tools.instance.monstarMag.FightType != StartFight.FightEnumType.JieYing && Tools.instance.monstarMag.FightType != StartFight.FightEnumType.HuaShen)
			{
				avatar.addStaticSkill();
			}
		}
		else
		{
			avatar.MonstarAddStaticSkill();
			foreach (SkillItem equipStaticSkill in avatar.equipStaticSkillList)
			{
				new StaticSkill(equipStaticSkill.itemId, 0, 5).Puting(avatar, avatar, 2);
			}
			avatar.wuDaoMag.MonstarAddWuDaoList(Tools.instance.MonstarID);
		}
		avatar.addWuDaoSeid();
		avatar.addJieDanSeid();
		if (Tools.instance.monstarMag.FightType != StartFight.FightEnumType.无装备无丹药擂台 && Tools.instance.monstarMag.FightType != StartFight.FightEnumType.天劫秘术领悟)
		{
			if (avatar.isPlayer() || Tools.instance.MonstarID < 20000)
			{
				if (avatar.isPlayer())
				{
					if (Tools.instance.monstarMag.FightType != StartFight.FightEnumType.ZhuJi && Tools.instance.monstarMag.FightType != StartFight.FightEnumType.JieDan && Tools.instance.monstarMag.FightType != StartFight.FightEnumType.JieYing && Tools.instance.monstarMag.FightType != StartFight.FightEnumType.HuaShen)
					{
						avatar.addEquipSeid();
					}
				}
				else
				{
					avatar.addEquipSeid();
				}
			}
			else
			{
				newNpcFightManager = new NewNpcFightManager();
				newNpcFightManager.addNpcEquipSeid(Tools.instance.MonstarID, avatar);
			}
		}
		else
		{
			foreach (UIFightWeaponItem item in UIFightPanel.Inst.FightWeapon)
			{
				_ = item;
				UIFightPanel.Inst.FightWeapon[0].SetLock(isLock: true);
			}
		}
		avatar.WorldsetRandomFace();
	}

	public void initUI_Target(UI_Target target, Avatar avatar)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		GameEntity gameEntity = (target.GE_target = ((GameObject)avatar.renderObj).GetComponent<GameEntity>());
		target.avatar = avatar;
		gameEntity.entity_name = avatar.name;
	}

	public void creatAvatar(int avaterID, int roleType, int HP_Max, Vector3 position, Vector3 direction)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		KBEngineApp.app.Client_onCreatedProxies((ulong)avaterID, avaterID, "Avatar");
		Avatar avatar = (Avatar)KBEngineApp.app.entities[avaterID];
		avatar.position = position;
		avatar.direction = direction;
		initCrystal(avatar);
	}

	public void initCrystal(Avatar avatar)
	{
		avatar.cardMag.Clear();
	}

	private void UpdateWeaponCellSum()
	{
		Tools.instance.getPlayer();
		if ((Object)(object)weapon2 != (Object)null)
		{
			if (PlayerEx.Player.checkHasStudyWuDaoSkillByID(2231))
			{
				((Component)UIFightPanel.Inst.FightWeapon[1]).gameObject.SetActive(true);
			}
			else
			{
				((Component)UIFightPanel.Inst.FightWeapon[1]).gameObject.SetActive(false);
			}
		}
	}

	public bool checkCanHasWeaponKey()
	{
		if ((Object)(object)weapon2 == (Object)null || (Object)(object)weapon == (Object)null)
		{
			return false;
		}
		return true;
	}

	private void Update()
	{
		KeyHideCD -= Time.deltaTime;
	}

	public void UpdateCard(MessageData data)
	{
		Avatar player = Tools.instance.getPlayer();
		Avatar otherAvatar = player.OtherAvatar;
		if ((Object)(object)PlayerCardText != (Object)null)
		{
			PlayerCardText.text = $"{player.cardMag.getCardNum()}/{player.NowCard}";
		}
		if ((Object)(object)NpcCardText != (Object)null)
		{
			NpcCardText.text = $"{otherAvatar.cardMag.getCardNum()}/{otherAvatar.NowCard}";
		}
		if (PlayerFightEventProcessor != null)
		{
			PlayerFightEventProcessor.OnUpdateLingQi();
		}
		UIFightPanel.Inst.RefreshCD();
	}
}
