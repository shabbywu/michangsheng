using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using MoonSharp.Interpreter;
using UnityEngine;

public class TianJieMiShuLingWuFightEventProcessor : IFightEventProcessor
{
	public static string MiShuID;

	public Avatar player;

	public Avatar target;

	public TianJieMiShuData miShuData;

	public int recordValue;

	public bool isSucess;

	public Script luaScript;

	public string checkSucessScript = "";

	public UIGanYingFight GanYingUI;

	public static Dictionary<string, object> OtherBindLuaObjects = new Dictionary<string, object>();

	public TianJieMiShuLingWuFightEventProcessor()
	{
		luaScript = new Script();
		miShuData = TianJieMiShuData.DataDict[MiShuID];
		checkSucessScript = miShuData.PanDing ?? "";
		BindLua();
	}

	public void BindLua()
	{
		luaScript.Globals["自身buff层数"] = new Func<bool, int, int>(MeBuffCount);
		luaScript.Globals["对方buff层数"] = new Func<bool, int, int>(TargetBuffCount);
		luaScript.Globals["单系累计造成伤害"] = new Func<bool, int, int>(TotalAttackTypeDamage);
		luaScript.Globals["累计造成伤害"] = new Func<bool, int>(TotalDamage);
		luaScript.Globals["单回合累计造成伤害"] = new Func<bool, int>(RoundDamage);
		luaScript.Globals["单回合累计造成伤害次数"] = new Func<bool, int>(RoundDamageCount);
		luaScript.Globals["累计吸收灵气"] = new Func<bool, int>(TotalAddLingQi);
		luaScript.Globals["累计恢复生命"] = new Func<bool, int>(TotalHealHP);
		luaScript.Globals["自身单回合损失血量"] = new Func<bool, int>(MeRoundLossHP);
		luaScript.Globals["对方单回合损失血量"] = new Func<bool, int>(TargetRoundLossHP);
		luaScript.Globals["自身累计损失血量"] = new Func<bool, int>(MeTotalLossHP);
		luaScript.Globals["对方累计损失血量"] = new Func<bool, int>(TargetTotalLossHP);
		luaScript.Globals["灵气总数"] = new Func<bool, int>(TotalLingQiCount);
		luaScript.Globals["灵气类型总数"] = new Func<bool, int>(HasLingQiTypeCount);
		luaScript.Globals["设置玩家血量"] = new Action<int>(SetPlayerHP);
		luaScript.Globals["增加玩家buff"] = new Action<int, int>(AddPlayerBuff);
		if (OtherBindLuaObjects == null)
		{
			return;
		}
		foreach (KeyValuePair<string, object> otherBindLuaObject in OtherBindLuaObjects)
		{
			luaScript.Globals[otherBindLuaObject.Key] = otherBindLuaObject.Value;
		}
	}

	void IFightEventProcessor.OnStartFight()
	{
		GameObject val = Object.Instantiate<GameObject>(Resources.Load<GameObject>("NewUI/DuJie/UIGanYingFight"), ((Component)NewUICanvas.Inst.Canvas).transform);
		GanYingUI = val.GetComponent<UIGanYingFight>();
		GanYingUI.Processor = this;
		Debug.Log((object)("TianJieMiShuLingWuFightEventProcessor.OnStartFight:\n" + miShuData.StartFightAction));
		if (!string.IsNullOrWhiteSpace(miShuData.StartFightAction))
		{
			luaScript.DoString(miShuData.StartFightAction);
		}
	}

	void IFightEventProcessor.SetAvatar(Avatar player, Avatar monstar)
	{
		this.player = player;
		target = monstar;
	}

	void IFightEventProcessor.OnUpdateBuff()
	{
		OnAnyUpdate();
	}

	void IFightEventProcessor.OnUpdateHP()
	{
		OnAnyUpdate();
	}

	void IFightEventProcessor.OnUpdateLingQi()
	{
		OnAnyUpdate();
	}

	void IFightEventProcessor.OnUpdateRound()
	{
		if (RoundManager.instance.StaticRoundNum > miShuData.RoundLimit)
		{
			SaveRecordValue();
			if (isSucess)
			{
				if (miShuData.StaticValueID != 0)
				{
					OnSucessSetStaticValue();
				}
				player.TianJieCanLingWuSkills.Add(MiShuID);
				UIPopTip.Inst.Pop("感应成功");
				RoundManager.instance.PlayerFightEventProcessor = null;
				target.die();
			}
			else
			{
				UIPopTip.Inst.Pop("感应失败");
				RoundManager.instance.PlayerFightEventProcessor = null;
				player.die();
			}
		}
		else
		{
			OnAnyUpdate();
		}
	}

	private void OnAnyUpdate()
	{
		try
		{
			if (luaScript.DoString(checkSucessScript).Boolean && !isSucess)
			{
				isSucess = true;
				Debug.Log((object)"已感应成功");
			}
			if ((Object)(object)GanYingUI != (Object)null)
			{
				GanYingUI.Refresh();
			}
		}
		catch (Exception arg)
		{
			Debug.LogError((object)$"天劫秘术判断时出现异常:\n{arg}");
		}
	}

	private void RecordValue(int value)
	{
		if (value > recordValue)
		{
			Debug.Log((object)$"感应时记录值:{value}");
			recordValue = value;
		}
	}

	public int GetSaveRecordValue()
	{
		Avatar avatar = PlayerEx.Player;
		if (avatar.TianJieSkillRecordValue.HasField(MiShuID))
		{
			return avatar.TianJieSkillRecordValue[MiShuID].I;
		}
		return 0;
	}

	public void SaveRecordValue()
	{
		player.TianJieSkillRecordValue.SetField(MiShuID, recordValue);
		Debug.Log((object)$"保存了 {MiShuID} 的历史记录:{recordValue}");
	}

	private void OnSucessSetStaticValue()
	{
		JSONObject jSONObject = null;
		foreach (JSONObject item in jsonData.instance.TianJieMiShuData.list)
		{
			if (item["id"].Str == MiShuID)
			{
				jSONObject = item;
				break;
			}
		}
		if (jSONObject == null)
		{
			Debug.LogError((object)("感应成功时设置全局变量出错，找不到ID为" + MiShuID + "的json"));
			return;
		}
		float n = jSONObject["DiYiXiang"].n;
		float n2 = jSONObject["GongBi"].n;
		float n3 = jSONObject["XiuZhengZhi"].n;
		float num = (float)recordValue * n3;
		float num2 = n * (1f - Mathf.Pow(n2, num)) / (1f - n2);
		int num3 = Mathf.RoundToInt(num2);
		Debug.Log((object)$"感应成功，最终分数{miShuData.StaticValueID}={num3}。计算过程:a1(1-q^n)/(1-q) a1:{n} q:{n2} x:{n3} n:{num} value = {n} * (1 - Mathf.Pow({n2}, {num})) / (1 - {n2}) = {num2}");
		int num4 = GlobalValue.Get(miShuData.StaticValueID, "感应天劫秘术" + MiShuID);
		if (num3 > num4)
		{
			Debug.Log((object)$"分数刷新记录，设置全局变量{miShuData.StaticValueID}={num3}");
			GlobalValue.Set(miShuData.StaticValueID, num3, "感应天劫秘术" + MiShuID);
		}
		else
		{
			Debug.Log((object)$"最终分数{num3}小于当前记录分数{num4}，不记录");
		}
	}

	public void SetPlayerHP(int hp)
	{
		player.setHP(hp);
	}

	public void AddPlayerBuff(int buffid, int count)
	{
		player.spell.addBuff(buffid, count);
	}

	public int MeBuffCount(bool needRecord, int buffid)
	{
		int buffSum = player.buffmag.GetBuffSum(buffid);
		if (needRecord)
		{
			RecordValue(buffSum);
		}
		return buffSum;
	}

	public int TargetBuffCount(bool needRecord, int buffid)
	{
		int buffSum = target.buffmag.GetBuffSum(buffid);
		if (needRecord)
		{
			RecordValue(buffSum);
		}
		return buffSum;
	}

	public int TotalDamage(bool needRecord)
	{
		int allDamage = player.fightTemp.AllDamage;
		if (needRecord)
		{
			RecordValue(allDamage);
		}
		return allDamage;
	}

	public int TotalAttackTypeDamage(bool needRecord, int attackType)
	{
		int attackTypeRoundDamage = player.fightTemp.GetAttackTypeRoundDamage(attackType);
		if (needRecord)
		{
			RecordValue(attackTypeRoundDamage);
		}
		return attackTypeRoundDamage;
	}

	public int RoundDamage(bool needRecord)
	{
		int num = player.fightTemp.lastRoundDamage[0];
		if (needRecord)
		{
			RecordValue(num);
		}
		return num;
	}

	public int RoundDamageCount(bool needRecord)
	{
		int num = player.fightTemp.lastRoundDamageCount[0];
		if (needRecord)
		{
			RecordValue(num);
		}
		return num;
	}

	public int TotalAddLingQi(bool needRecord)
	{
		int totalAddLingQi = player.fightTemp.TotalAddLingQi;
		if (needRecord)
		{
			RecordValue(totalAddLingQi);
		}
		return totalAddLingQi;
	}

	public int TotalHealHP(bool needRecord)
	{
		int totalHealHP = player.fightTemp.TotalHealHP;
		if (needRecord)
		{
			RecordValue(totalHealHP);
		}
		return totalHealHP;
	}

	public int TotalLingQiCount(bool needRecord)
	{
		int cardNum = player.cardMag.getCardNum();
		if (needRecord)
		{
			RecordValue(cardNum);
		}
		return cardNum;
	}

	public int MeRoundLossHP(bool needRecord)
	{
		int num = player.fightTemp.RoundLossHP[0];
		if (needRecord)
		{
			RecordValue(num);
		}
		return num;
	}

	public int TargetRoundLossHP(bool needRecord)
	{
		int num = target.fightTemp.RoundLossHP[0];
		if (needRecord)
		{
			RecordValue(num);
		}
		return num;
	}

	public int MeTotalLossHP(bool needRecord)
	{
		int totalLossHP = player.fightTemp.TotalLossHP;
		if (needRecord)
		{
			RecordValue(totalLossHP);
		}
		return totalLossHP;
	}

	public int TargetTotalLossHP(bool needRecord)
	{
		int totalLossHP = target.fightTemp.TotalLossHP;
		if (needRecord)
		{
			RecordValue(totalLossHP);
		}
		return totalLossHP;
	}

	public int HasLingQiTypeCount(bool needRecord)
	{
		int num = 0;
		for (int i = 0; i <= 5; i++)
		{
			if (player.cardMag.getCardTypeNum(i) > 0)
			{
				num++;
			}
		}
		if (needRecord)
		{
			RecordValue(num);
		}
		return num;
	}
}
