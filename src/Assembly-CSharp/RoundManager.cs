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

// Token: 0x02000643 RID: 1603
public class RoundManager : MonoBehaviour
{
	// Token: 0x060027CE RID: 10190 RVA: 0x001367FC File Offset: 0x001349FC
	private void Awake()
	{
		RoundManager.instance = this;
		RoundManager.InitLingQiKeNengXing();
		BindData.Bind("FightBeforeHpMax", Tools.instance.getPlayer().HP_Max);
		Event.registerOut("endRound", this, "endRound");
		Event.registerOut("startRound", this, "startRound");
		YSFuncList.Ints.Clear();
		if (!RoundManager.TuPoTypeList.Contains(Tools.instance.monstarMag.FightType))
		{
			if (Tools.instance.monstarMag.FightImageID != 0)
			{
				Sprite sprite = Resources.Load<Sprite>("Fightimage/" + Tools.instance.monstarMag.FightImageID);
				this.BackGroud.sprite = sprite;
			}
			else
			{
				Sprite sprite2 = Resources.Load<Sprite>("Fightimage/" + 1);
				this.BackGroud.sprite = sprite2;
			}
		}
		MessageMag.Instance.Register("Fight_CardChange", new Action<MessageData>(this.UpdateCard));
	}

	// Token: 0x060027CF RID: 10191 RVA: 0x001368FC File Offset: 0x00134AFC
	public void MoveLingQiToCacheFromPlayer(Dictionary<int, int> skillCost, LingQiCacheType cacheType)
	{
		if (cacheType == LingQiCacheType.DontMove)
		{
			UIFightPanel.Inst.CacheLingQiController.NowMoveSame = true;
			using (Dictionary<int, int>.Enumerator enumerator = skillCost.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, int> keyValuePair = enumerator.Current;
					UIFightLingQiSlot targetLingQiSlot = UIFightPanel.Inst.CacheLingQiController.GetTargetLingQiSlot((LingQiType)keyValuePair.Key);
					UIFightPanel.Inst.PlayerLingQiController.SlotList[keyValuePair.Key].LingQiCount -= keyValuePair.Value;
					targetLingQiSlot.LingQiCount += keyValuePair.Value;
				}
				goto IL_139;
			}
		}
		UIFightPanel.Inst.CacheLingQiController.NowMoveSame = false;
		foreach (KeyValuePair<int, int> keyValuePair2 in skillCost)
		{
			UIFightLingQiCacheSlot targetTongLingQiSlotWithLimit = UIFightPanel.Inst.CacheLingQiController.GetTargetTongLingQiSlotWithLimit(keyValuePair2.Value);
			targetTongLingQiSlotWithLimit.LingQiType = (LingQiType)keyValuePair2.Key;
			UIFightPanel.Inst.PlayerLingQiController.SlotList[keyValuePair2.Key].LingQiCount -= keyValuePair2.Value;
			targetTongLingQiSlotWithLimit.LingQiCount += keyValuePair2.Value;
		}
		IL_139:
		UIFightPanel.Inst.CacheLingQiController.NowMoveSame = false;
	}

	// Token: 0x060027D0 RID: 10192 RVA: 0x00136A70 File Offset: 0x00134C70
	public void PlayRunAway()
	{
		try
		{
			if (!Tools.instance.monstarMag.CanRunAway())
			{
				UIPopTip.Inst.Pop(Tools.getStr("cannotRunAway" + Tools.instance.monstarMag.CanNotRunAwayEvent()), PopTipIconType.叹号);
			}
			else
			{
				Avatar avatar = Tools.instance.getPlayer();
				if (avatar.buffmag.HasBuffSeid(183))
				{
					UIPopTip.Inst.Pop("无法逃跑", PopTipIconType.叹号);
				}
				else
				{
					UnityAction onOK = delegate()
					{
						if (Tools.instance.monstarMag.shouldReloadSaveHp())
						{
							avatar.HP = Tools.instance.monstarMag.gameStartHP;
						}
						GlobalValue.SetTalk(1, 4, "RoundManager.PlayRunAway");
						Tools.instance.AutoSeatSeaRunAway(false);
						if (Tools.instance.getPlayer().NowFuBen == "" || Tools.instance.FinalScene.Contains("Sea"))
						{
							Tools.instance.CanShowFightUI = 1;
						}
						if (GlobalValue.GetTalk(0, "RoundManager.PlayRunAway") > 0 || avatar.fubenContorl.isInFuBen() || Tools.instance.FinalScene.Contains("Sea"))
						{
							Tools.instance.loadMapScenes(Tools.instance.FinalScene, true);
							return;
						}
						Tools.instance.loadMapScenes("AllMaps", true);
					};
					if (Tools.instance.monstarMag.FightType == StartFight.FightEnumType.天劫秘术领悟)
					{
						USelectBox.Show("是否放弃本次感悟？", onOK, null);
					}
					else if (this.TouXiangTypes.Contains(Tools.instance.monstarMag.FightType))
					{
						USelectBox.Show("是否确认投降？", onOK, null);
					}
					else if (avatar.dunSu - (int)jsonData.instance.AvatarJsonData[string.Concat(Tools.instance.MonstarID)]["dunSu"].n > 0)
					{
						USelectBox.Show("是否确认遁走？", onOK, null);
					}
					else
					{
						UIPopTip.Inst.Pop(Tools.getStr("cannotRunAway0"), PopTipIconType.叹号);
					}
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogError(ex);
		}
	}

	// Token: 0x060027D1 RID: 10193 RVA: 0x0001F6B2 File Offset: 0x0001D8B2
	public void setSkillChoicOk()
	{
		this.ChoiceSkill = null;
		this.NowSkillUsedLingQiSum = -1;
	}

	// Token: 0x060027D2 RID: 10194 RVA: 0x00136BE8 File Offset: 0x00134DE8
	public void SetChoiceSkill(ref GUIPackage.Skill skill)
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		CardMag cardMag = avatar.cardMag;
		if (skill.CanUse(avatar, KBEngineApp.app.entities[11], true, "") != SkillCanUseType.可以使用)
		{
			this.ChoiceSkill = null;
			UIFightPanel.Inst.CacheLingQiController.MoveAllLingQiToPlayer();
			UIFightPanel.Inst.CacheLingQiController.ChangeCacheSlotNumber(0);
			UIFightPanel.Inst.CancelSkillHighlight();
			avatar.onCrystalChanged(cardMag);
			return;
		}
		UIFightPanel.Inst.CacheLingQiController.MoveAllLingQiToPlayer();
		UIFightPanel.Inst.UIFightState = UIFightState.释放技能准备灵气阶段;
		UIFightPanel.Inst.FightCenterButtonController.ButtonType = UIFightCenterButtonType.OkCancel;
		Dictionary<int, int> skillSameCast = skill.skillSameCast;
		UIFightPanel.Inst.FightCenterButtonController.SetOkCancelEvent(delegate
		{
			if (this.UseSkill("", true))
			{
				UIFightPanel.Inst.CacheLingQiController.ChangeCacheSlotNumber(0);
				UIFightPanel.Inst.CancelSkillHighlight();
				return;
			}
			UIFightPanel.Inst.UIFightState = UIFightState.释放技能准备灵气阶段;
		}, delegate
		{
			this.ChoiceSkill = null;
			UIFightPanel.Inst.CacheLingQiController.MoveAllLingQiToPlayer();
			UIFightPanel.Inst.CacheLingQiController.ChangeCacheSlotNumber(0);
			UIFightPanel.Inst.CancelSkillHighlight();
			UIFightPanel.Inst.FightCenterButtonController.ButtonType = UIFightCenterButtonType.EndRound;
			UIFightPanel.Inst.UIFightState = UIFightState.自己回合普通状态;
		});
		bool flag = true;
		if (skill == this.ChoiceSkill)
		{
			flag = false;
		}
		else
		{
			this.CalcTongLingQiKeNeng(avatar, skill);
		}
		this.ChoiceSkill = skill;
		Dictionary<int, int> skillCast = skill.getSkillCast(avatar);
		UIFightPanel.Inst.CacheLingQiController.ChangeCacheSlotNumber(skillCast.Count + skill.skillSameCast.Count);
		UIFightPanel.Inst.CacheLingQiController.SetLingQiLimit(skillCast, skill.skillSameCast);
		bool flag2 = false;
		if (flag && avatar.FightCostRecord.HasField(skill.skill_ID.ToString()))
		{
			JSONObject jsonobject = avatar.FightCostRecord[skill.skill_ID.ToString()];
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			for (int i = 0; i < 6; i++)
			{
				if (jsonobject[i.ToString()].I > 0)
				{
					dictionary.Add(i, jsonobject[i.ToString()].I);
				}
			}
			bool flag3 = true;
			foreach (KeyValuePair<int, int> keyValuePair in dictionary)
			{
				if (skillCast.ContainsKey(keyValuePair.Key))
				{
					if (avatar.cardMag.HasNoEnoughNum(keyValuePair.Key, keyValuePair.Value + skillCast[keyValuePair.Key]))
					{
						flag3 = false;
						break;
					}
				}
				else if (avatar.cardMag.HasNoEnoughNum(keyValuePair.Key, keyValuePair.Value))
				{
					flag3 = false;
					break;
				}
			}
			if (flag3)
			{
				flag2 = true;
				this.MoveLingQiToCacheFromPlayer(skillCast, LingQiCacheType.DontMove);
				this.MoveLingQiToCacheFromPlayer(dictionary, LingQiCacheType.None);
			}
		}
		if (!flag2)
		{
			Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
			for (int j = 0; j < 6; j++)
			{
				dictionary2.Add(j, avatar.cardMag[j]);
			}
			foreach (KeyValuePair<int, int> keyValuePair2 in skillCast)
			{
				Dictionary<int, int> dictionary3 = dictionary2;
				int key = keyValuePair2.Key;
				dictionary3[key] -= keyValuePair2.Value;
			}
			this.MoveLingQiToCacheFromPlayer(skillCast, LingQiCacheType.DontMove);
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
				Dictionary<int, int> dictionary4 = new Dictionary<int, int>();
				int num = 0;
				bool flag5;
				do
				{
					int[] array;
					if (flag4)
					{
						array = RoundManager.lingQiKeNengXingZuHe[skill.skillSameCast.Count][this.choiceSkillCanUseLingQiIndexList[this.clickSkillChangeLingQiIndex]];
					}
					else
					{
						array = RoundManager.lingQiKeNengXingPaiLie[skill.skillSameCast.Count][this.choiceSkillCanUseLingQiIndexList[this.clickSkillChangeLingQiIndex]];
					}
					this.clickSkillChangeLingQiIndex++;
					if (this.clickSkillChangeLingQiIndex >= this.choiceSkillCanUseLingQiIndexList.Count)
					{
						this.clickSkillChangeLingQiIndex = 0;
					}
					dictionary4.Clear();
					string str = "";
					for (int l = 0; l < array.Length; l++)
					{
						dictionary4.Add(array[l], skill.skillSameCast[l]);
						str += string.Format("{0}x{1} ", array[l], skill.skillSameCast[l]);
					}
					flag5 = true;
					foreach (KeyValuePair<int, int> keyValuePair3 in dictionary4)
					{
						if (UIFightPanel.Inst.PlayerLingQiController.SlotList[keyValuePair3.Key].LingQiCount < keyValuePair3.Value)
						{
							flag5 = false;
						}
					}
					num++;
				}
				while (!flag5 && num < 100);
				if (num >= 100)
				{
					Debug.LogError("在计算灵气可能性时出现异常，保底循环超过100次");
				}
				this.MoveLingQiToCacheFromPlayer(dictionary4, LingQiCacheType.None);
			}
		}
		avatar.onCrystalChanged(cardMag);
	}

	// Token: 0x060027D3 RID: 10195 RVA: 0x001370E8 File Offset: 0x001352E8
	private static void InitLingQiKeNengXing()
	{
		if (RoundManager.lingQiKeNengXingZuHe == null)
		{
			RoundManager.lingQiKeNengXingZuHe = new Dictionary<int, List<int[]>>();
			int[] t = new int[]
			{
				0,
				1,
				2,
				3,
				4
			};
			for (int i = 1; i <= 5; i++)
			{
				List<int[]> combination = PermutationAndCombination<int>.GetCombination(t, i);
				RoundManager.lingQiKeNengXingZuHe.Add(i, combination);
			}
		}
		if (RoundManager.lingQiKeNengXingPaiLie == null)
		{
			RoundManager.lingQiKeNengXingPaiLie = new Dictionary<int, List<int[]>>();
			int[] t2 = new int[]
			{
				0,
				1,
				2,
				3,
				4
			};
			for (int j = 1; j <= 5; j++)
			{
				List<int[]> permutation = PermutationAndCombination<int>.GetPermutation(t2, j);
				RoundManager.lingQiKeNengXingPaiLie.Add(j, permutation);
			}
		}
	}

	// Token: 0x060027D4 RID: 10196 RVA: 0x00137184 File Offset: 0x00135384
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
				Dictionary<int, int> dictionary2 = dictionary;
				int key = i;
				dictionary2[key] -= skill.skillCast[i];
			}
		}
		this.choiceSkillCanUseLingQiIndexList = new List<int>();
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
		List<int[]> list;
		if (flag)
		{
			list = RoundManager.lingQiKeNengXingZuHe[skillSameCast.Count];
		}
		else
		{
			list = RoundManager.lingQiKeNengXingPaiLie[skillSameCast.Count];
		}
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
				this.choiceSkillCanUseLingQiIndexList.Add(k);
			}
		}
		this.clickSkillChangeLingQiIndex = Random.Range(0, this.choiceSkillCanUseLingQiIndexList.Count);
		Debug.Log(string.Format("对于技能{0}，玩家有{1}种同系灵气可能性", skill.skill_Name, this.choiceSkillCanUseLingQiIndexList.Count));
	}

	// Token: 0x060027D5 RID: 10197 RVA: 0x000ADDE8 File Offset: 0x000ABFE8
	public int GetLingQiSum(Dictionary<int, int> a)
	{
		int num = 0;
		foreach (KeyValuePair<int, int> keyValuePair in a)
		{
			num += keyValuePair.Value;
		}
		return num;
	}

	// Token: 0x060027D6 RID: 10198 RVA: 0x00137304 File Offset: 0x00135504
	public int GetLingQiSum(Dictionary<LingQiType, int> a)
	{
		int num = 0;
		foreach (KeyValuePair<LingQiType, int> keyValuePair in a)
		{
			num += keyValuePair.Value;
		}
		return num;
	}

	// Token: 0x060027D7 RID: 10199 RVA: 0x00137358 File Offset: 0x00135558
	public bool UseSkill(string uuid = "", bool showTip = true)
	{
		Buff._NeiShangLoopCount = 0;
		UIFightPanel.Inst.UIFightState = UIFightState.自己回合普通状态;
		if (this.ChoiceSkill != null)
		{
			Dictionary<LingQiType, int> nowCacheLingQi = UIFightPanel.Inst.CacheLingQiController.GetNowCacheLingQi();
			Dictionary<int, int> skillCast = this.ChoiceSkill.getSkillCast(Tools.instance.getPlayer());
			int lingQiSum = this.GetLingQiSum(skillCast);
			int lingQiSum2 = this.GetLingQiSum(this.ChoiceSkill.skillSameCast);
			int lingQiSum3 = this.GetLingQiSum(nowCacheLingQi);
			if (lingQiSum + lingQiSum2 != lingQiSum3)
			{
				if (showTip)
				{
					UIPopTip.Inst.Pop("选择的灵气与技能消耗不一致", PopTipIconType.叹号);
					Debug.Log("选择的灵气与技能消耗不一致1");
				}
				return false;
			}
			foreach (KeyValuePair<int, int> keyValuePair in this.ChoiceSkill.getSkillCast(Tools.instance.getPlayer()))
			{
				LingQiType key = (LingQiType)keyValuePair.Key;
				if (!nowCacheLingQi.ContainsKey(key))
				{
					if (showTip)
					{
						UIPopTip.Inst.Pop("选择的灵气与技能消耗不一致", PopTipIconType.叹号);
						Debug.Log("选择的灵气与技能消耗不一致3");
					}
					return false;
				}
				if (nowCacheLingQi[key] < keyValuePair.Value)
				{
					if (showTip)
					{
						UIPopTip.Inst.Pop("选择的灵气与技能消耗不一致", PopTipIconType.叹号);
						Debug.Log("选择的灵气与技能消耗不一致2");
					}
					return false;
				}
				Dictionary<LingQiType, int> dictionary = nowCacheLingQi;
				LingQiType key2 = key;
				dictionary[key2] -= keyValuePair.Value;
			}
			Dictionary<LingQiType, int> dictionary2 = new Dictionary<LingQiType, int>();
			foreach (KeyValuePair<int, int> keyValuePair2 in this.ChoiceSkill.skillSameCast)
			{
				bool flag = false;
				foreach (KeyValuePair<LingQiType, int> keyValuePair3 in nowCacheLingQi)
				{
					if (keyValuePair3.Value == keyValuePair2.Value && !dictionary2.ContainsKey(keyValuePair3.Key))
					{
						dictionary2.Add(keyValuePair3.Key, keyValuePair2.Value);
						flag = true;
						Dictionary<LingQiType, int> dictionary = nowCacheLingQi;
						LingQiType key2 = keyValuePair3.Key;
						dictionary[key2] -= keyValuePair2.Value;
						break;
					}
				}
				if (!flag)
				{
					if (showTip)
					{
						UIPopTip.Inst.Pop("选择的灵气与技能消耗不一致", PopTipIconType.叹号);
						Debug.Log("选择的灵气与技能消耗不一致4");
					}
					return false;
				}
			}
			foreach (KeyValuePair<LingQiType, int> keyValuePair4 in nowCacheLingQi)
			{
				if (keyValuePair4.Value != 0)
				{
					if (showTip)
					{
						UIPopTip.Inst.Pop("选择的灵气与技能消耗不一致", PopTipIconType.叹号);
						Debug.Log("选择的灵气与技能消耗不一致5");
					}
					return false;
				}
			}
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			if (avatar.isPlayer())
			{
				int[] array = new int[6];
				foreach (KeyValuePair<LingQiType, int> keyValuePair5 in UIFightPanel.Inst.CacheLingQiController.GetNowCacheLingQi())
				{
					array[(int)keyValuePair5.Key] = keyValuePair5.Value;
				}
				foreach (KeyValuePair<int, int> keyValuePair6 in skillCast)
				{
					array[keyValuePair6.Key] -= keyValuePair6.Value;
				}
				JSONObject jsonobject = new JSONObject(JSONObject.Type.OBJECT);
				for (int i = 0; i < 6; i++)
				{
					jsonobject.SetField(i.ToString(), array[i]);
				}
				avatar.FightCostRecord.SetField(this.ChoiceSkill.skill_ID.ToString(), jsonobject);
			}
			this.NowSkillUsedLingQiSum = lingQiSum3;
			avatar.spell.spellSkill(this.ChoiceSkill.skill_ID, uuid);
		}
		UIFightPanel.Inst.FightCenterButtonController.ButtonType = UIFightCenterButtonType.EndRound;
		this.setSkillChoicOk();
		return true;
	}

	// Token: 0x060027D8 RID: 10200 RVA: 0x0001F6C2 File Offset: 0x0001D8C2
	private void Start()
	{
		this.gameStart();
	}

	// Token: 0x060027D9 RID: 10201 RVA: 0x0013779C File Offset: 0x0013599C
	private void OnDestroy()
	{
		RoundManager.instance = null;
		Event.deregisterOut(this);
		YSFuncList.Ints.Clear();
		if (this.newNpcFightManager != null)
		{
			this.newNpcFightManager.Dispose();
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
		MessageMag.Instance.Remove("Fight_CardChange", new Action<MessageData>(this.UpdateCard));
	}

	// Token: 0x060027DA RID: 10202 RVA: 0x0013789C File Offset: 0x00135A9C
	public void endRound(Entity _avater)
	{
		Avatar avatar = (Avatar)_avater;
		if (avatar.isPlayer())
		{
			RoundManager.EventFightTalk("RealClickEndRound", null, null);
		}
		avatar.state = 2;
		if (avatar.isPlayer())
		{
			UIFightPanel.Inst.UIFightState = UIFightState.敌人回合;
			UIFightPanel.Inst.FightCenterTip.HideTip();
			UIFightPanel.Inst.FightCenterButtonController.ButtonType = UIFightCenterButtonType.None;
		}
		avatar.spell.onBuffTickByType(1, new List<int>
		{
			0
		});
		avatar.spell.onRemoveBuffByType(7, 1);
		avatar.spell.onRemoveBuffByType(3, 1);
		avatar.spell.onRemoveBuffByType(4, 1);
		avatar.spell.onRemoveBuffByType(13, 1);
		avatar.spell.onBuffTickByType(34, new List<int>
		{
			0
		});
		if (avatar.isPlayer())
		{
			Avatar avater = (Avatar)KBEngineApp.app.entities[11];
			this.startRound(avater);
		}
		else
		{
			Avatar avater2 = (Avatar)KBEngineApp.app.entities[10];
			this.startRound(avater2);
		}
		avatar.onCrystalChanged(avatar.cardMag);
		avatar.fightTemp.ResetRound(avatar);
		if (Tools.instance.monstarMag.FightType == StartFight.FightEnumType.JieYing && avatar.isPlayer() && Tools.instance.getPlayer().buffmag.HasBuff(3101))
		{
			RoundManager.instance.gameObject.GetComponent<JieYingManager>().XinMoAttack(0);
		}
		if (Tools.instance.monstarMag.FightType == StartFight.FightEnumType.ZhuJi && avatar.isPlayer())
		{
			List<List<int>> buffByID = avatar.buffmag.getBuffByID(3978);
			if (buffByID.Count > 0)
			{
				if (buffByID[0][1] == 0)
				{
					ZhuJiManager.inst.checkState();
					return;
				}
				ZhuJiManager.inst.ShengYuHuiHe.text = "剩余回合 " + buffByID[0][1].ToCNNumber();
			}
		}
	}

	// Token: 0x060027DB RID: 10203 RVA: 0x0001F6CA File Offset: 0x0001D8CA
	public int getListSum(CardMag list)
	{
		return list.getCardNum();
	}

	// Token: 0x060027DC RID: 10204 RVA: 0x00137A88 File Offset: 0x00135C88
	public void autoRemoveCard(Avatar avater)
	{
		avater.spell.onBuffTickByType(44);
		if (avater.SkillSeidFlag.ContainsKey(24) && avater.SkillSeidFlag[24][0] == 1)
		{
			avater.SkillSeidFlag[24][0] = 0;
			this.endRound(avater);
			return;
		}
		if (avater.buffmag.HasBuffSeid(99))
		{
			return;
		}
		List<int> list = new List<int>();
		avater.spell.onBuffTickByType(20, list);
		int listSum = this.getListSum(avater.cardMag);
		if ((long)listSum > (long)((ulong)avater.NowCard))
		{
			int randomCount = listSum - (int)avater.NowCard;
			int[] randomRemoveLingQi = this.GetRandomRemoveLingQi(avater, randomCount);
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

	// Token: 0x060027DD RID: 10205 RVA: 0x00137B80 File Offset: 0x00135D80
	private int getRealRemoveNum(int sum, int removeNum)
	{
		int result;
		if (sum >= removeNum)
		{
			result = removeNum;
		}
		else
		{
			result = sum;
		}
		return result;
	}

	// Token: 0x060027DE RID: 10206 RVA: 0x00137B9C File Offset: 0x00135D9C
	public int[] GetRandomRemoveLingQi(Avatar avater, int randomCount)
	{
		int cardNum = avater.cardMag.getCardNum();
		if (cardNum < randomCount)
		{
			Debug.LogError(string.Format("玩家灵气数量{0}小于要随机的数量{1}", cardNum, randomCount));
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
			List<int> list3 = list;
			int index2 = num;
			int num2 = list3[index2];
			list3[index2] = num2 - 1;
			if (list[num] <= 0)
			{
				list2.Remove(num);
			}
		}
		return array;
	}

	// Token: 0x060027DF RID: 10207 RVA: 0x00137C78 File Offset: 0x00135E78
	public void removeCard(Avatar avater, int removeNum)
	{
		int realRemoveNum = this.getRealRemoveNum(this.getListSum(avater.cardMag), removeNum);
		int[] randomRemoveLingQi = this.GetRandomRemoveLingQi(avater, realRemoveNum);
		for (int i = 0; i < 6; i++)
		{
			if (randomRemoveLingQi[i] > 0)
			{
				this.RoundTimeAutoRemoveCard(avater, i, randomRemoveLingQi[i]);
			}
		}
		avater.onCrystalChanged(avater.cardMag);
	}

	// Token: 0x060027E0 RID: 10208 RVA: 0x00137CCC File Offset: 0x00135ECC
	public void removeCard(Avatar avater, int removeNum, int removeType)
	{
		int realRemoveNum = this.getRealRemoveNum(this.getListSum(avater.cardMag), removeNum);
		this.RoundTimeAutoRemoveCard(avater, removeType, realRemoveNum);
		avater.onCrystalChanged(avater.cardMag);
	}

	// Token: 0x060027E1 RID: 10209 RVA: 0x0001F6D2 File Offset: 0x0001D8D2
	public void ExchengCard(Avatar avater, card _card, int type)
	{
		avater.onCrystalChanged(avater.cardMag);
	}

	// Token: 0x060027E2 RID: 10210 RVA: 0x00137D04 File Offset: 0x00135F04
	public void RoundTimeAutoRemoveCard(Avatar avater, int removeType, int count = 1)
	{
		avater.AbandonCryStal(removeType, count);
		if (this.NowUseLingQiType == UseLingQiType.释放技能后消耗)
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

	// Token: 0x060027E3 RID: 10211 RVA: 0x00137D7C File Offset: 0x00135F7C
	public static void EventFightTalk(string name, EventDelegate del, EventDelegate end = null)
	{
		if (Tools.instance.monstarMag.FightTalkID != 0)
		{
			Flowchart fightTalk = RoundManager.instance.FightTalk;
			if (fightTalk.HasBlock(name))
			{
				if (del != null)
				{
					del.Execute();
				}
				fightTalk.ExecuteBlock(name);
				if (end != null)
				{
					end.Execute();
				}
			}
		}
	}

	// Token: 0x060027E4 RID: 10212 RVA: 0x00137DCC File Offset: 0x00135FCC
	public void PlayerEndRound(bool canCancel = true)
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		avatar.spell.onBuffTickByType(44);
		if (avatar.SkillSeidFlag.ContainsKey(24) && avatar.SkillSeidFlag[24][0] == 1)
		{
			avatar.SkillSeidFlag[24][0] = 0;
			this.endRound(avatar);
			return;
		}
		bool flag = false;
		RoundManager.EventFightTalk("ClickEndRound", null, new EventDelegate(delegate()
		{
			Flowchart fightTalk = RoundManager.instance.FightTalk;
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
		if ((long)UIFightPanel.Inst.PlayerLingQiController.GetPlayerLingQiSum() <= (long)((ulong)avatar.NowCard) || avatar.buffmag.HasBuffSeid(99))
		{
			avatar.spell.onRemoveBuffByType(26, 1);
			this.endRound(avatar);
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
			UIFightPanel.Inst.FightCenterButtonController.SetOkCancelEvent(new UnityAction(this.OnPlayerEndRoundQiZhiLingQiOKClick), new UnityAction(this.OnPlayerEndRoundQiZhiLingQiCacelClick));
		}
		else
		{
			UIFightPanel.Inst.FightCenterButtonController.SetOnlyOKEvent(new UnityAction(this.OnPlayerEndRoundQiZhiLingQiOKClick));
		}
		UIFightPanel.Inst.FightCenterTip.ShowYiSan(UIFightPanel.Inst.NeedYiSanCount);
		UIPopTip.Inst.Pop("点击选择你要消散的灵气", PopTipIconType.叹号);
	}

	// Token: 0x060027E5 RID: 10213 RVA: 0x00137F9C File Offset: 0x0013619C
	private void OnPlayerEndRoundQiZhiLingQiOKClick()
	{
		Avatar player = PlayerEx.Player;
		int num = player.cardMag.getCardNum() - (int)player.NowCard;
		int cacheLingQiSum = UIFightPanel.Inst.CacheLingQiController.GetCacheLingQiSum();
		if (num != cacheLingQiSum)
		{
			UIPopTip.Inst.Pop(string.Format("还需要消散{0}点灵气", num - cacheLingQiSum), PopTipIconType.叹号);
			return;
		}
		Dictionary<LingQiType, int> nowCacheLingQi = UIFightPanel.Inst.CacheLingQiController.GetNowCacheLingQi();
		bool flag = true;
		foreach (KeyValuePair<LingQiType, int> keyValuePair in nowCacheLingQi)
		{
			if (player.cardMag.getCardTypeNum((int)keyValuePair.Key) < keyValuePair.Value)
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
			foreach (KeyValuePair<LingQiType, int> keyValuePair2 in nowCacheLingQi)
			{
				num2 += keyValuePair2.Value;
				player.AbandonCryStal((int)keyValuePair2.Key, keyValuePair2.Value);
			}
			list.Add(num2);
			player.spell.onBuffTickByType(18, list);
			UIFightPanel.Inst.UIFightState = UIFightState.自己回合普通状态;
			UIFightPanel.Inst.CacheLingQiController.DestoryAllLingQi();
			UIFightPanel.Inst.PlayerLingQiController.ResetPlayerLingQiCount();
			UIFightPanel.Inst.CacheLingQiController.ChangeCacheSlotNumber(0);
			this.endRound(player);
			return;
		}
		UIPopTip.Inst.Pop("没有足够的灵气", PopTipIconType.叹号);
	}

	// Token: 0x060027E6 RID: 10214 RVA: 0x00138144 File Offset: 0x00136344
	private void OnPlayerEndRoundQiZhiLingQiCacelClick()
	{
		UIFightPanel.Inst.CacheLingQiController.MoveAllLingQiToPlayer();
		UIFightPanel.Inst.CacheLingQiController.ChangeCacheSlotNumber(0);
		UIFightPanel.Inst.FightCenterButtonController.ButtonType = UIFightCenterButtonType.EndRound;
		UIFightPanel.Inst.UIFightState = UIFightState.自己回合普通状态;
		UIFightPanel.Inst.FightCenterTip.HideTip();
	}

	// Token: 0x060027E7 RID: 10215 RVA: 0x0013819C File Offset: 0x0013639C
	public card drawCardCreatSpritAndAddCrystal(Avatar avatar, int type)
	{
		card result = avatar.addCrystal(type, 1);
		if (avatar.isPlayer())
		{
			UIFightPanel.Inst.PlayerLingQiController.SlotList[type].LingQiCount++;
			this.PlayerCurRoundDrawCardNum++;
			return result;
		}
		this.NpcCurRoundDrawCardNum++;
		return result;
	}

	// Token: 0x060027E8 RID: 10216 RVA: 0x001381F8 File Offset: 0x001363F8
	public void DrawCardCreatSpritAndAddCrystal(Avatar avatar, int type, int count = 1)
	{
		avatar.addCrystal(type, count);
		if (avatar.isPlayer())
		{
			UIFightPanel.Inst.PlayerLingQiController.SlotList[type].LingQiCount += count;
			this.PlayerCurRoundDrawCardNum += count;
			return;
		}
		this.NpcCurRoundDrawCardNum += count;
	}

	// Token: 0x060027E9 RID: 10217 RVA: 0x00138258 File Offset: 0x00136458
	public void RandomDrawCard(Avatar avatar, int count = 1)
	{
		int[] randomLingQiTypes = this.GetRandomLingQiTypes(avatar, count);
		for (int i = 0; i < 6; i++)
		{
			if (randomLingQiTypes[i] > 0)
			{
				this.DrawCardCreatSpritAndAddCrystal(avatar, i, randomLingQiTypes[i]);
			}
		}
	}

	// Token: 0x060027EA RID: 10218 RVA: 0x0013828C File Offset: 0x0013648C
	private card drawCardRealize(Avatar avatar, int lingQiType)
	{
		List<int> list = new List<int>();
		list.Add(1);
		list.Add(0);
		avatar.spell.onBuffTickByType(24, list);
		return this.drawCardCreatSpritAndAddCrystal(avatar, lingQiType);
	}

	// Token: 0x060027EB RID: 10219 RVA: 0x001382C4 File Offset: 0x001364C4
	private void DrawCardRealize(Avatar avatar, int lingQiType)
	{
		List<int> list = new List<int>();
		list.Add(1);
		list.Add(0);
		avatar.spell.onBuffTickByType(24, list);
		this.DrawCardCreatSpritAndAddCrystal(avatar, lingQiType, 1);
	}

	// Token: 0x060027EC RID: 10220 RVA: 0x001382FC File Offset: 0x001364FC
	public card drawCard(Avatar avatar)
	{
		int randomLingQiType = this.GetRandomLingQiType(avatar);
		return this.drawCardRealize(avatar, randomLingQiType);
	}

	// Token: 0x060027ED RID: 10221 RVA: 0x0013831C File Offset: 0x0013651C
	public void DrawCard(Avatar avatar)
	{
		int randomLingQiType = this.GetRandomLingQiType(avatar);
		this.DrawCardRealize(avatar, randomLingQiType);
	}

	// Token: 0x060027EE RID: 10222 RVA: 0x0001F6E0 File Offset: 0x0001D8E0
	public void DrawCard(Avatar avatar, int lingQiType)
	{
		this.DrawCardRealize(avatar, lingQiType);
	}

	// Token: 0x060027EF RID: 10223 RVA: 0x0013833C File Offset: 0x0013653C
	public int getRemoveNum(Avatar avatar)
	{
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		foreach (KeyValuePair<int, int> keyValuePair in avatar.DrawWeight)
		{
			dictionary[keyValuePair.Key] = keyValuePair.Value;
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

	// Token: 0x060027F0 RID: 10224 RVA: 0x00138418 File Offset: 0x00136618
	public int GetRandomLingQiType(Avatar avatar)
	{
		if (Tools.instance.monstarMag.FightCardID != 0 && avatar.isPlayer())
		{
			int card = RoundManager.instance.FightDrawCard.getCard();
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
		foreach (int num2 in avatar.GetLingGeng)
		{
			if (num > 5)
			{
				break;
			}
			Dictionary<int, int> dictionary2 = dictionary;
			int key = num;
			dictionary2[key] += num2;
			num++;
		}
		foreach (KeyValuePair<int, int> keyValuePair in avatar.DrawWeight)
		{
			Dictionary<int, int> dictionary2 = dictionary;
			int key = keyValuePair.Key;
			dictionary2[key] += keyValuePair.Value;
		}
		if (avatar.SkillSeidFlag.ContainsKey(13))
		{
			foreach (KeyValuePair<int, int> keyValuePair2 in avatar.SkillSeidFlag[13])
			{
				Dictionary<int, int> dictionary2 = dictionary;
				int key = keyValuePair2.Key;
				dictionary2[key] += keyValuePair2.Value;
			}
		}
		int lingQiSum = this.GetLingQiSum(dictionary);
		int num3 = Random.Range(0, int.MaxValue) % lingQiSum;
		int result = 0;
		int num4 = 0;
		foreach (KeyValuePair<int, int> keyValuePair3 in dictionary)
		{
			num4 += keyValuePair3.Value;
			if (num4 > num3)
			{
				result = keyValuePair3.Key;
				break;
			}
		}
		return result;
	}

	// Token: 0x060027F1 RID: 10225 RVA: 0x0013862C File Offset: 0x0013682C
	public int[] GetRandomLingQiTypes(Avatar avatar, int count = 1)
	{
		int[] array = new int[6];
		int i = 0;
		if (Tools.instance.monstarMag.FightCardID != 0 && avatar.isPlayer())
		{
			int num = 0;
			while (num != -1 && i < count)
			{
				num = RoundManager.instance.FightDrawCard.getCard();
				if (num != -1)
				{
					array[num]++;
					i++;
				}
			}
		}
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		for (int j = 0; j < 6; j++)
		{
			dictionary.Add(j, 0);
		}
		int num2 = 0;
		foreach (int num3 in avatar.GetLingGeng)
		{
			if (num2 > 5)
			{
				break;
			}
			Dictionary<int, int> dictionary2 = dictionary;
			int key = num2;
			dictionary2[key] += num3;
			num2++;
		}
		foreach (KeyValuePair<int, int> keyValuePair in avatar.DrawWeight)
		{
			Dictionary<int, int> dictionary2 = dictionary;
			int key = keyValuePair.Key;
			dictionary2[key] += keyValuePair.Value;
		}
		if (avatar.SkillSeidFlag.ContainsKey(13))
		{
			foreach (KeyValuePair<int, int> keyValuePair2 in avatar.SkillSeidFlag[13])
			{
				Dictionary<int, int> dictionary2 = dictionary;
				int key = keyValuePair2.Key;
				dictionary2[key] += keyValuePair2.Value;
			}
		}
		int lingQiSum = this.GetLingQiSum(dictionary);
		while (i < count)
		{
			int num4 = Random.Range(0, int.MaxValue) % lingQiSum;
			int num5 = 0;
			foreach (KeyValuePair<int, int> keyValuePair3 in dictionary)
			{
				num5 += keyValuePair3.Value;
				if (num5 > num4)
				{
					array[keyValuePair3.Key]++;
					i++;
					break;
				}
			}
		}
		return array;
	}

	// Token: 0x060027F2 RID: 10226 RVA: 0x00138878 File Offset: 0x00136A78
	public void startRound(Entity _avater)
	{
		try
		{
			Avatar avatar = (Avatar)_avater;
			Debug.Log(avatar.name + " 回合开始");
			if (avatar.isPlayer())
			{
				UIFightPanel.Inst.UIFightState = UIFightState.自己回合普通状态;
				UIFightPanel.Inst.FightCenterButtonController.ButtonType = UIFightCenterButtonType.EndRound;
				this.PlayerCurRoundDrawCardNum = 0;
			}
			else
			{
				UIFightPanel.Inst.FightCenterButtonController.ButtonType = UIFightCenterButtonType.None;
				UIFightPanel.Inst.UIFightState = UIFightState.敌人回合;
				this.NpcCurRoundDrawCardNum = 0;
			}
			if (avatar.state != 1)
			{
				avatar.NowRoundUsedCard.Clear();
				avatar.state = 3;
				if (avatar.isPlayer())
				{
					this.StaticRoundNum++;
					if (this.PlayerFightEventProcessor != null)
					{
						this.PlayerFightEventProcessor.OnUpdateRound();
					}
					UIFightPanel.Inst.FightRoundCount.ShowRuond(this.StaticRoundNum);
				}
				avatar.spell.onBuffTickByType(2, new List<int>());
				avatar.spell.onRemoveBuffByType(5, 1);
				avatar.spell.onRemoveBuffByType(6, 1);
				avatar.spell.onRemoveBuffByType(14, 1);
				int num = avatar.NowDrawCardNum;
				List<int> list = new List<int>();
				list.Add(num);
				list.Add(0);
				list.Add(-123);
				list.Add(0);
				avatar.spell.onBuffTickByType(3, list);
				num = list[0];
				if (avatar.buffmag.HasBuffSeid(8))
				{
					num = (int)Math.Ceiling(Convert.ToDouble((float)num / 2f));
					avatar.spell.removeBuff(avatar.buffmag.getBuffBySeid(8)[0]);
				}
				if (list[3] == 1)
				{
					num = 0;
				}
				Debug.Log(string.Format("{0} 回合开始吸收{1}点灵气", avatar.name, num));
				this.RandomDrawCard(avatar, num);
				avatar.onCrystalChanged(avatar.cardMag);
				avatar.spell.onBuffTickByType(4, list);
				if (list[1] == 1)
				{
					if (avatar.isPlayer())
					{
						RoundManager.instance.PlayerEndRound(false);
					}
					else
					{
						avatar.AvatarEndRound();
					}
				}
				foreach (GUIPackage.Skill skill in avatar.skill)
				{
					if (avatar.SkillSeidFlag.ContainsKey(5) && avatar.SkillSeidFlag[5].ContainsKey(skill.skill_ID) && avatar.SkillSeidFlag[5][skill.skill_ID] == 1)
					{
						skill.CurCD = 50000f;
					}
					else if (skill.weaponuuid != null && skill.weaponuuid != "" && avatar.isPlayer())
					{
						if (this.WeaponSkillList.ContainsKey(skill.weaponuuid))
						{
							Dictionary<string, int> weaponSkillList = this.WeaponSkillList;
							string weaponuuid = skill.weaponuuid;
							int num2 = weaponSkillList[weaponuuid];
							weaponSkillList[weaponuuid] = num2 - 1;
							if (this.WeaponSkillList[skill.weaponuuid] > 0)
							{
								skill.CurCD = 50000f;
							}
							else
							{
								skill.CurCD = 0f;
							}
						}
						else
						{
							skill.CurCD = 0f;
						}
					}
					else if (avatar.SkillSeidFlag.ContainsKey(29) && avatar.SkillSeidFlag[29].ContainsKey(skill.skill_ID) && avatar.SkillSeidFlag[29][skill.skill_ID] >= 1)
					{
						Dictionary<int, int> dictionary = avatar.SkillSeidFlag[29];
						int num2 = skill.skill_ID;
						int num3 = dictionary[num2];
						dictionary[num2] = num3 - 1;
						if (avatar.SkillSeidFlag[29][skill.skill_ID] > 0)
						{
							skill.CurCD = 50000f;
						}
						else
						{
							skill.CurCD = 0f;
						}
					}
					else
					{
						skill.CurCD = 0f;
					}
				}
				UIFightPanel.Inst.RefreshCD();
			}
		}
		catch (Exception arg)
		{
			Debug.LogError(string.Format("startRound:\n{0}", arg));
		}
	}

	// Token: 0x060027F3 RID: 10227 RVA: 0x00138CB8 File Offset: 0x00136EB8
	public void chengeCrystal()
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		avatar.onCrystalChanged(avatar.cardMag);
		avatar.OtherAvatar.onCrystalChanged(avatar.OtherAvatar.cardMag);
	}

	// Token: 0x060027F4 RID: 10228 RVA: 0x0001F6EA File Offset: 0x0001D8EA
	public void eventChengeCrystal()
	{
		base.Invoke("chengeCrystal", 0.05f);
	}

	// Token: 0x060027F5 RID: 10229 RVA: 0x0001F6FC File Offset: 0x0001D8FC
	public Avatar GetMonstar()
	{
		return (Avatar)KBEngineApp.app.entities[11];
	}

	// Token: 0x060027F6 RID: 10230 RVA: 0x00138CF8 File Offset: 0x00136EF8
	public void initMonstar(int __monstarID)
	{
		Avatar avatar = (Avatar)KBEngineApp.app.entities[11];
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[string.Concat(__monstarID)];
		foreach (JSONObject jsonobject2 in jsonobject["skills"].list)
		{
			SkillItem skillItem = new SkillItem();
			skillItem.itemId = (int)jsonobject2.n;
			avatar.equipSkillList.Add(skillItem);
		}
		foreach (JSONObject jsonobject3 in jsonobject["staticSkills"].list)
		{
			SkillItem skillItem2 = new SkillItem();
			skillItem2.itemId = (int)jsonobject3.n;
			avatar.equipStaticSkillList.Add(skillItem2);
		}
		if (jsonobject.HasField("yuanying"))
		{
			int i = jsonobject["yuanying"].I;
			if (i != 0)
			{
				SkillItem skillItem3 = new SkillItem();
				skillItem3.itemId = i;
				skillItem3.itemIndex = 6;
				avatar.equipStaticSkillList.Add(skillItem3);
			}
		}
		if (jsonobject.HasField("HuaShenLingYu") && jsonobject["HuaShenLingYu"].I > 0)
		{
			SkillItem skillItem4 = new SkillItem();
			skillItem4.itemId = jsonobject["HuaShenLingYu"].I;
			avatar.equipSkillList.Add(skillItem4);
			avatar.HuaShenLingYuSkill = new JSONObject(jsonobject["HuaShenLingYu"].I);
		}
		for (int j = 0; j < jsonobject["LingGen"].Count; j++)
		{
			int item = (int)jsonobject["LingGen"][j].n;
			avatar.LingGeng.Add(item);
		}
		if (jsonobject["id"].I < 20000)
		{
			if ((int)jsonobject["equipWeapon"].n > 0)
			{
				avatar.YSequipItem((int)jsonobject["equipWeapon"].n, 0);
			}
			if ((int)jsonobject["equipClothing"].n > 0)
			{
				avatar.YSequipItem((int)jsonobject["equipClothing"].n, 1);
			}
			if ((int)jsonobject["equipRing"].n > 0)
			{
				avatar.YSequipItem((int)jsonobject["equipRing"].n, 2);
			}
		}
		avatar.ZiZhi = (int)jsonobject["ziZhi"].n;
		avatar.dunSu = (int)jsonobject["dunSu"].n;
		avatar.wuXin = (uint)jsonobject["wuXin"].n;
		avatar.shengShi = (int)jsonobject["shengShi"].n;
		avatar.shaQi = (uint)jsonobject["shaQi"].n;
		avatar.shouYuan = (uint)jsonobject["shouYuan"].n;
		avatar.age = (uint)jsonobject["age"].n;
		avatar.HP_Max = (int)jsonobject["HP"].n;
		avatar.HP = (int)jsonobject["HP"].n;
		avatar.level = (ushort)jsonobject["Level"].n;
		avatar.AvatarType = (uint)((ushort)jsonobject["AvatarType"].n);
		avatar.name = Tools.instance.Code64ToString(jsonData.instance.AvatarRandomJsonData[string.Concat(__monstarID)]["Name"].str);
		avatar.roleTypeCell = (uint)jsonobject["fightFace"].n;
		avatar.roleType = (uint)jsonobject["face"].n;
		avatar.fightTemp.MonstarID = __monstarID;
		avatar.fightTemp.useAI = true;
		avatar.Sex = (int)jsonobject["SexType"].n;
	}

	// Token: 0x060027F7 RID: 10231 RVA: 0x00139134 File Offset: 0x00137334
	public void InitGanYingMonstar(int monstarID)
	{
		this.initMonstar(monstarID);
		Avatar player = PlayerEx.Player;
		JSONObject jsonobject = jsonData.instance.AvatarRandomJsonData[string.Concat(1)];
		JSONObject jsonobject2 = jsonData.instance.AvatarRandomJsonData[string.Concat(monstarID)];
		jsonData.instance.AvatarRandomJsonData.SetField(string.Concat(monstarID), jsonobject.Clone());
		Avatar avatar = (Avatar)KBEngineApp.app.entities[11];
		avatar.roleTypeCell = player.roleTypeCell;
		avatar.roleType = player.roleType;
		avatar.Sex = player.Sex;
	}

	// Token: 0x060027F8 RID: 10232 RVA: 0x001391E0 File Offset: 0x001373E0
	public void initXinMoMonstar()
	{
		Tools.instance.MonstarID = 10000;
		Avatar avatar = (Avatar)KBEngineApp.app.entities[11];
		int num = 10000;
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[string.Concat(num)];
		Avatar player = Tools.instance.getPlayer();
		foreach (SkillItem skillItem in player.equipSkillList)
		{
			SkillItem skillItem2 = new SkillItem();
			skillItem2.itemId = skillItem.itemId;
			avatar.equipSkillList.Add(skillItem2);
		}
		foreach (SkillItem skillItem3 in player.equipStaticSkillList)
		{
			SkillItem skillItem4 = new SkillItem();
			skillItem4.itemId = skillItem3.itemId;
			avatar.equipStaticSkillList.Add(skillItem4);
		}
		for (int i = 0; i < jsonobject["LingGen"].Count; i++)
		{
			int item = (int)jsonobject["LingGen"][i].n;
			avatar.LingGeng.Add(item);
		}
		foreach (ITEM_INFO item_INFO in player.equipItemList.values)
		{
			avatar.YSequipItem(item_INFO.itemId, item_INFO.itemIndex);
		}
		avatar.ZiZhi = (int)jsonobject["ziZhi"].n;
		avatar.dunSu = (int)jsonobject["dunSu"].n;
		avatar.wuXin = (uint)jsonobject["wuXin"].n;
		avatar.shengShi = player.shengShi;
		avatar.shaQi = (uint)jsonobject["shaQi"].n;
		avatar.shouYuan = (uint)jsonobject["shouYuan"].n;
		avatar.age = (uint)jsonobject["age"].n;
		avatar.HP_Max = (int)jsonobject["HP"].n;
		avatar.HP = (int)jsonobject["HP"].n;
		JSONObject jsonobject2 = jsonData.instance.AvatarRandomJsonData[string.Concat(1)];
		JSONObject jsonobject3 = jsonData.instance.AvatarRandomJsonData[string.Concat(num)];
		jsonData.instance.AvatarRandomJsonData.SetField(string.Concat(num), jsonobject2.Clone());
		avatar.level = (ushort)jsonobject["Level"].n;
		avatar.AvatarType = (uint)((ushort)jsonobject["AvatarType"].n);
		avatar.name = player.name;
		avatar.roleTypeCell = player.roleTypeCell;
		avatar.roleType = player.roleType;
		avatar.Sex = player.Sex;
		avatar.fightTemp.MonstarID = num;
		avatar.fightTemp.useAI = true;
	}

	// Token: 0x060027F9 RID: 10233 RVA: 0x00139530 File Offset: 0x00137730
	public void gameStart()
	{
		UIFightPanel.Inst.Clear();
		this.PlayerUseSkillList = new List<int>();
		this.NpcUseSkillList = new List<int>();
		UIFightPanel.Inst.FightCenterButtonController.ButtonType = UIFightCenterButtonType.None;
		Tools.instance.getPlayer().fightTemp.LianQiBuffEquipDictionary = new Dictionary<int, JSONObject>();
		Tools.instance.getPlayer().fightTemp.LianQiEquipDictionary = new Dictionary<int, JSONObject>();
		this.UpdateWeaponCellSum();
		if (Tools.instance.monstarMag.FightTalkID != 0)
		{
			GameObject gameObject = Resources.Load("talkPrefab/FightPrefab/FightTalk" + Tools.instance.monstarMag.FightTalkID) as GameObject;
			if (gameObject != null)
			{
				GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject);
				this.FightTalk = gameObject2.GetComponentInChildren<Flowchart>();
			}
		}
		if (Tools.instance.monstarMag.FightCardID != 0)
		{
			GameObject gameObject3 = Resources.Load("talkPrefab/FightPrefab/FightCard" + Tools.instance.monstarMag.FightCardID) as GameObject;
			if (gameObject3 != null)
			{
				GameObject gameObject4 = Object.Instantiate<GameObject>(gameObject3);
				this.FightDrawCard = gameObject4.GetComponentInChildren<FightStaticDrawCard>();
			}
		}
		this.creatAvatar(11, 52, 100, new Vector3(5f, -1.4f, -1f), new Vector3(0f, 0f, -90f));
		KBEngineApp.app.entity_id = 10;
		if (Tools.instance.monstarMag.FightType == StartFight.FightEnumType.XingMo)
		{
			this.initXinMoMonstar();
		}
		else if (Tools.instance.monstarMag.FightType == StartFight.FightEnumType.天劫秘术领悟)
		{
			this.InitGanYingMonstar(Tools.instance.MonstarID);
			GlobalValue.SetTalk(0, 4032, "unknow");
		}
		else
		{
			this.initMonstar(Tools.instance.MonstarID);
		}
		Avatar player = (Avatar)KBEngineApp.app.player();
		this.initCrystal(player);
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
		foreach (KeyValuePair<int, Entity> keyValuePair in KBEngineApp.app.entities)
		{
			Avatar avatar2 = (Avatar)keyValuePair.Value;
			this.initAvatarInfo(avatar2);
			avatar2.fightTemp.showNowHp = avatar2.HP;
			Debug.Log(string.Format("{0} 游戏开始吸收{1}点灵气", avatar2.name, avatar2.NowStartCardNum));
			this.RandomDrawCard(avatar2, avatar2.NowStartCardNum);
			if (!avatar2.isPlayer())
			{
				avatar2.onCrystalChanged(avatar2.cardMag);
			}
		}
		UIFightLingQiSlot.IgnoreEffect = false;
		UIFightPanel.Inst.RefreshLingQiCount(true);
		if (Tools.instance.monstarMag.FightType != StartFight.FightEnumType.无装备无丹药擂台 && Tools.instance.monstarMag.FightType != StartFight.FightEnumType.天劫秘术领悟)
		{
			Dictionary<int, int> danYaoBuFFDict = player.StreamData.DanYaoBuFFDict;
			if (danYaoBuFFDict.Count > 0)
			{
				foreach (int num in danYaoBuFFDict.Keys)
				{
					player.spell.addBuff(num, danYaoBuFFDict[num]);
				}
				player.StreamData.DanYaoBuFFDict = new Dictionary<int, int>();
			}
		}
		foreach (KeyValuePair<int, int> keyValuePair2 in Tools.instance.monstarMag.monstarAddBuff)
		{
			avatar.spell.addDBuff(keyValuePair2.Key, keyValuePair2.Value);
		}
		foreach (KeyValuePair<int, int> keyValuePair3 in Tools.instance.monstarMag.HeroAddBuff)
		{
			player.spell.addDBuff(keyValuePair3.Key, keyValuePair3.Value);
		}
		int danDuLevel = player.GetDanDuLevel();
		if (danDuLevel >= 2)
		{
			player.spell.addDBuff(9999 + danDuLevel);
		}
		if (player.TianFuID.HasField(string.Concat(16)))
		{
			foreach (JSONObject jsonobject in player.TianFuID["16"].list)
			{
				player.spell.addDBuff((int)jsonobject.n);
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
			this.setJieDan();
		}
		else if (Tools.instance.monstarMag.FightType == StartFight.FightEnumType.JieYing)
		{
			this.SetJieYing();
		}
		else if (Tools.instance.monstarMag.FightType == StartFight.FightEnumType.ZhuJi)
		{
			this.setZhuJi();
		}
		else if (Tools.instance.monstarMag.FightType == StartFight.FightEnumType.HuaShen)
		{
			this.SetHuaShen();
		}
		else if (Tools.instance.monstarMag.FightType == StartFight.FightEnumType.天劫秘术领悟)
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
			this.PlayerFightEventProcessor = new TianJieMiShuLingWuFightEventProcessor();
			this.PlayerFightEventProcessor.SetAvatar(player, avatar);
		}
		else if (Tools.instance.monstarMag.FightType == StartFight.FightEnumType.FeiSheng)
		{
			this.SetFeiSheng();
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
				UIFightPanel.Inst.HuaShenLingYuBtn.GetComponent<UTooltipSkillTrigger>().SkillID = i;
				UIFightPanel.Inst.HuaShenLingYuBtn.transform.parent.gameObject.SetActive(true);
				UIFightPanel.Inst.HuaShenLingYuBtn.mouseUpEvent.RemoveAllListeners();
				UIFightPanel.Inst.HuaShenLingYuBtn.mouseUpEvent.AddListener(delegate()
				{
					if (UIFightPanel.Inst.UIFightState == UIFightState.敌人回合)
					{
						UIPopTip.Inst.Pop("敌方回合，无法使用", PopTipIconType.叹号);
						return;
					}
					if (player.TianJie.HasField("ShengYuTimeValue") && player.TianJie["ShengYuTimeValue"].I >= 100)
					{
						player.skill.Add(newSkill);
						player.spell.spellSkill(newSkill.skill_ID, "");
						TianJieManager.TianJieJiaSu(100);
						UIFightPanel.Inst.HuaShenLingYuBtn.transform.parent.gameObject.SetActive(false);
						return;
					}
					UIPopTip.Inst.Pop("天劫临近，无法使用", PopTipIconType.叹号);
				});
			}
			avatar.getLevelType();
		}
		((GameObject)player.renderObj).transform.GetChild(0).transform.eulerAngles = new Vector3(0f, -90f, 0f);
		player.spell.onBuffTickByType(23, new List<int>());
		avatar.spell.onBuffTickByType(23, new List<int>());
		this.gameStartAvatar = ((player.dunSu >= avatar.dunSu) ? player : avatar);
		player.onCrystalChanged(player.cardMag);
		if (this.PlayerFightEventProcessor != null)
		{
			this.PlayerFightEventProcessor.OnStartFight();
		}
		base.StartCoroutine(this.DelayStart(this.gameStartAvatar));
	}

	// Token: 0x060027FA RID: 10234 RVA: 0x0001F714 File Offset: 0x0001D914
	private IEnumerator DelayStart(Avatar gameStartAvatar)
	{
		yield return new WaitForSeconds(0.1f);
		this.startRound(gameStartAvatar);
		yield break;
	}

	// Token: 0x060027FB RID: 10235 RVA: 0x00139E7C File Offset: 0x0013807C
	private void TuPoPreDo(int saveBuffType)
	{
		Avatar player = Tools.instance.getPlayer();
		foreach (KeyValuePair<int, Entity> keyValuePair in KBEngineApp.app.entities)
		{
			((GameObject)keyValuePair.Value.renderObj).transform.localPosition = new Vector3(-5f, 10f, 0f);
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
		foreach (UIFightSkillItem uifightSkillItem in UIFightPanel.Inst.FightSkills)
		{
			uifightSkillItem.Clear();
		}
	}

	// Token: 0x060027FC RID: 10236 RVA: 0x00139FAC File Offset: 0x001381AC
	private void setZhuJi()
	{
		this.TuPoPreDo(14);
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
	}

	// Token: 0x060027FD RID: 10237 RVA: 0x0013A020 File Offset: 0x00138220
	private void setJieDan()
	{
		this.TuPoPreDo(10);
		Avatar player = Tools.instance.getPlayer();
		foreach (SkillItem skillItem in player.hasSkillList)
		{
			int skillKeyByID = Tools.instance.getSkillKeyByID(skillItem.itemId, player);
			if (skillKeyByID != -1 && _skillJsonData.DataDict[skillKeyByID].AttackType.Contains(13))
			{
				player.FightAddSkill(skillKeyByID, 6, 12);
			}
		}
		player.spell.addDBuff(4011);
	}

	// Token: 0x060027FE RID: 10238 RVA: 0x0013A0CC File Offset: 0x001382CC
	private void SetJieYing()
	{
		this.TuPoPreDo(9);
		Avatar player = Tools.instance.getPlayer();
		player.FightAddSkill(11047, 0, 6);
		player.FightAddSkill(11097, 6, 12);
		player.FightAddSkill(11102, 6, 12);
		foreach (SkillItem skillItem in player.hasSkillList)
		{
			int skillKeyByID = Tools.instance.getSkillKeyByID(skillItem.itemId, player);
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
	}

	// Token: 0x060027FF RID: 10239 RVA: 0x0013A1EC File Offset: 0x001383EC
	private void SetHuaShen()
	{
		this.TuPoPreDo(15);
		Avatar player = Tools.instance.getPlayer();
		List<int> list = new List<int>
		{
			1141,
			1142,
			1143,
			1144,
			1149
		};
		for (int i = 0; i < list.Count; i++)
		{
			int skillKeyByID = Tools.instance.getSkillKeyByID(list[i], player);
			if (skillKeyByID != -1)
			{
				player.FightAddSkill(skillKeyByID, 0, 6);
			}
		}
		List<int> list2 = new List<int>
		{
			1145,
			1146,
			1147,
			1148,
			1150
		};
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
	}

	// Token: 0x06002800 RID: 10240 RVA: 0x0013A33C File Offset: 0x0013853C
	private void SetFeiSheng()
	{
		this.TuPoPreDo(16);
		Avatar player = PlayerEx.Player;
		foreach (JSONObject jsonobject in player.TianJieEquipedSkills.list)
		{
			string str = jsonobject.Str;
			if (!string.IsNullOrWhiteSpace(str))
			{
				TianJieMiShuData tianJieMiShuData = TianJieMiShuData.DataDict[str];
				int skillKeyByID = Tools.instance.getSkillKeyByID(tianJieMiShuData.Skill_ID, player);
				player.FightAddSkill(skillKeyByID, 0, 12);
			}
		}
	}

	// Token: 0x06002801 RID: 10241 RVA: 0x0013A3D4 File Offset: 0x001385D4
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
			foreach (SkillItem skillItem in avatar.equipStaticSkillList)
			{
				new StaticSkill(skillItem.itemId, 0, 5).Puting(avatar, avatar, 2);
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
				this.newNpcFightManager = new NewNpcFightManager();
				this.newNpcFightManager.addNpcEquipSeid(Tools.instance.MonstarID, avatar);
			}
		}
		avatar.WorldsetRandomFace();
	}

	// Token: 0x06002802 RID: 10242 RVA: 0x0013A5C4 File Offset: 0x001387C4
	public void initUI_Target(UI_Target target, Avatar avatar)
	{
		GameEntity component = ((GameObject)avatar.renderObj).GetComponent<GameEntity>();
		target.GE_target = component;
		target.avatar = avatar;
		component.entity_name = avatar.name;
	}

	// Token: 0x06002803 RID: 10243 RVA: 0x0013A5FC File Offset: 0x001387FC
	public void creatAvatar(int avaterID, int roleType, int HP_Max, Vector3 position, Vector3 direction)
	{
		KBEngineApp.app.Client_onCreatedProxies((ulong)((long)avaterID), avaterID, "Avatar");
		Avatar avatar = (Avatar)KBEngineApp.app.entities[avaterID];
		avatar.position = position;
		avatar.direction = direction;
		this.initCrystal(avatar);
	}

	// Token: 0x06002804 RID: 10244 RVA: 0x0001F72A File Offset: 0x0001D92A
	public void initCrystal(Avatar avatar)
	{
		avatar.cardMag.Clear();
	}

	// Token: 0x06002805 RID: 10245 RVA: 0x0013A648 File Offset: 0x00138848
	private void UpdateWeaponCellSum()
	{
		Tools.instance.getPlayer();
		if (this.weapon2 != null)
		{
			if (PlayerEx.Player.checkHasStudyWuDaoSkillByID(2231))
			{
				UIFightPanel.Inst.FightWeapon[1].gameObject.SetActive(true);
				return;
			}
			UIFightPanel.Inst.FightWeapon[1].gameObject.SetActive(false);
		}
	}

	// Token: 0x06002806 RID: 10246 RVA: 0x0001F737 File Offset: 0x0001D937
	public bool checkCanHasWeaponKey()
	{
		return !(this.weapon2 == null) && !(this.weapon == null);
	}

	// Token: 0x06002807 RID: 10247 RVA: 0x0001F758 File Offset: 0x0001D958
	private void Update()
	{
		RoundManager.KeyHideCD -= Time.deltaTime;
	}

	// Token: 0x06002808 RID: 10248 RVA: 0x0013A6B8 File Offset: 0x001388B8
	public void UpdateCard(MessageData data)
	{
		Avatar player = Tools.instance.getPlayer();
		Avatar otherAvatar = player.OtherAvatar;
		if (this.PlayerCardText != null)
		{
			this.PlayerCardText.text = string.Format("{0}/{1}", player.cardMag.getCardNum(), player.NowCard);
		}
		if (this.NpcCardText != null)
		{
			this.NpcCardText.text = string.Format("{0}/{1}", otherAvatar.cardMag.getCardNum(), otherAvatar.NowCard);
		}
		if (this.PlayerFightEventProcessor != null)
		{
			this.PlayerFightEventProcessor.OnUpdateLingQi();
		}
		UIFightPanel.Inst.RefreshCD();
	}

	// Token: 0x040021B9 RID: 8633
	public GameObject weapon;

	// Token: 0x040021BA RID: 8634
	public GameObject weapon2;

	// Token: 0x040021BB RID: 8635
	public NewNpcFightManager newNpcFightManager;

	// Token: 0x040021BC RID: 8636
	public Dictionary<string, int> WeaponSkillList = new Dictionary<string, int>();

	// Token: 0x040021BD RID: 8637
	public int curRemoveBuffId;

	// Token: 0x040021BE RID: 8638
	public Text PlayerCardText;

	// Token: 0x040021BF RID: 8639
	public Text NpcCardText;

	// Token: 0x040021C0 RID: 8640
	public int PlayerCurRoundDrawCardNum;

	// Token: 0x040021C1 RID: 8641
	public int NpcCurRoundDrawCardNum;

	// Token: 0x040021C2 RID: 8642
	public List<int> PlayerUseSkillList;

	// Token: 0x040021C3 RID: 8643
	public List<int> NpcUseSkillList;

	// Token: 0x040021C4 RID: 8644
	public bool IsVirtual;

	// Token: 0x040021C5 RID: 8645
	public int VirtualSkillDamage;

	// Token: 0x040021C6 RID: 8646
	private Avatar gameStartAvatar;

	// Token: 0x040021C7 RID: 8647
	public IFightEventProcessor PlayerFightEventProcessor;

	// Token: 0x040021C8 RID: 8648
	public int StaticRoundNum;

	// Token: 0x040021C9 RID: 8649
	public GameObject playerHoldCard;

	// Token: 0x040021CA RID: 8650
	public static RoundManager instance;

	// Token: 0x040021CB RID: 8651
	public GUIPackage.Skill ChoiceSkill;

	// Token: 0x040021CC RID: 8652
	public TooltipSkill ToolitpSkill;

	// Token: 0x040021CD RID: 8653
	public TooltipSkill ItemToolitpSkill;

	// Token: 0x040021CE RID: 8654
	public Flowchart FightTalk;

	// Token: 0x040021CF RID: 8655
	public FightStaticDrawCard FightDrawCard;

	// Token: 0x040021D0 RID: 8656
	public GameObject FightInfoScrew;

	// Token: 0x040021D1 RID: 8657
	public GameObject FightInfoTemp;

	// Token: 0x040021D2 RID: 8658
	public SpriteRenderer BackGroud;

	// Token: 0x040021D3 RID: 8659
	public int gameOverSwitch;

	// Token: 0x040021D4 RID: 8660
	public static List<StartFight.FightEnumType> TuPoTypeList = new List<StartFight.FightEnumType>
	{
		StartFight.FightEnumType.ZhuJi,
		StartFight.FightEnumType.JieDan,
		StartFight.FightEnumType.JieYing,
		StartFight.FightEnumType.HuaShen,
		StartFight.FightEnumType.FeiSheng
	};

	// Token: 0x040021D5 RID: 8661
	private readonly List<StartFight.FightEnumType> TouXiangTypes = new List<StartFight.FightEnumType>
	{
		StartFight.FightEnumType.LeiTai,
		StartFight.FightEnumType.QieCuo,
		StartFight.FightEnumType.DouFa,
		StartFight.FightEnumType.无装备无丹药擂台
	};

	// Token: 0x040021D6 RID: 8662
	private int clickSkillChangeLingQiIndex;

	// Token: 0x040021D7 RID: 8663
	private static Dictionary<int, List<int[]>> lingQiKeNengXingZuHe;

	// Token: 0x040021D8 RID: 8664
	private static Dictionary<int, List<int[]>> lingQiKeNengXingPaiLie;

	// Token: 0x040021D9 RID: 8665
	private List<int> choiceSkillCanUseLingQiIndexList;

	// Token: 0x040021DA RID: 8666
	public int NowSkillUsedLingQiSum = -1;

	// Token: 0x040021DB RID: 8667
	public static float KeyHideCD;

	// Token: 0x040021DC RID: 8668
	[HideInInspector]
	public UseLingQiType NowUseLingQiType;

	// Token: 0x02000644 RID: 1604
	public enum entityID
	{
		// Token: 0x040021DE RID: 8670
		EntityPlayer = 10,
		// Token: 0x040021DF RID: 8671
		EntityMonster
	}
}
