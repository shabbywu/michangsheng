using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using MoonSharp.Interpreter;
using UnityEngine;

// Token: 0x02000655 RID: 1621
public class TianJieMiShuLingWuFightEventProcessor : IFightEventProcessor
{
	// Token: 0x06002845 RID: 10309 RVA: 0x0013B420 File Offset: 0x00139620
	public TianJieMiShuLingWuFightEventProcessor()
	{
		this.luaScript = new Script();
		this.miShuData = TianJieMiShuData.DataDict[TianJieMiShuLingWuFightEventProcessor.MiShuID];
		this.checkSucessScript = (this.miShuData.PanDing ?? "");
		this.BindLua();
	}

	// Token: 0x06002846 RID: 10310 RVA: 0x0013B480 File Offset: 0x00139680
	public void BindLua()
	{
		this.luaScript.Globals["自身buff层数"] = new Func<bool, int, int>(this.MeBuffCount);
		this.luaScript.Globals["对方buff层数"] = new Func<bool, int, int>(this.TargetBuffCount);
		this.luaScript.Globals["单系累计造成伤害"] = new Func<bool, int, int>(this.TotalAttackTypeDamage);
		this.luaScript.Globals["累计造成伤害"] = new Func<bool, int>(this.TotalDamage);
		this.luaScript.Globals["单回合累计造成伤害"] = new Func<bool, int>(this.RoundDamage);
		this.luaScript.Globals["单回合累计造成伤害次数"] = new Func<bool, int>(this.RoundDamageCount);
		this.luaScript.Globals["累计吸收灵气"] = new Func<bool, int>(this.TotalAddLingQi);
		this.luaScript.Globals["累计恢复生命"] = new Func<bool, int>(this.TotalHealHP);
		this.luaScript.Globals["自身单回合损失血量"] = new Func<bool, int>(this.MeRoundLossHP);
		this.luaScript.Globals["对方单回合损失血量"] = new Func<bool, int>(this.TargetRoundLossHP);
		this.luaScript.Globals["自身累计损失血量"] = new Func<bool, int>(this.MeTotalLossHP);
		this.luaScript.Globals["对方累计损失血量"] = new Func<bool, int>(this.TargetTotalLossHP);
		this.luaScript.Globals["灵气总数"] = new Func<bool, int>(this.TotalLingQiCount);
		this.luaScript.Globals["灵气类型总数"] = new Func<bool, int>(this.HasLingQiTypeCount);
		this.luaScript.Globals["设置玩家血量"] = new Action<int>(this.SetPlayerHP);
		this.luaScript.Globals["增加玩家buff"] = new Action<int, int>(this.AddPlayerBuff);
		if (TianJieMiShuLingWuFightEventProcessor.OtherBindLuaObjects != null)
		{
			foreach (KeyValuePair<string, object> keyValuePair in TianJieMiShuLingWuFightEventProcessor.OtherBindLuaObjects)
			{
				this.luaScript.Globals[keyValuePair.Key] = keyValuePair.Value;
			}
		}
	}

	// Token: 0x06002847 RID: 10311 RVA: 0x0013B700 File Offset: 0x00139900
	void IFightEventProcessor.OnStartFight()
	{
		GameObject gameObject = Object.Instantiate<GameObject>(Resources.Load<GameObject>("NewUI/DuJie/UIGanYingFight"), NewUICanvas.Inst.Canvas.transform);
		this.GanYingUI = gameObject.GetComponent<UIGanYingFight>();
		this.GanYingUI.Processor = this;
		Debug.Log("TianJieMiShuLingWuFightEventProcessor.OnStartFight:\n" + this.miShuData.StartFightAction);
		if (!string.IsNullOrWhiteSpace(this.miShuData.StartFightAction))
		{
			this.luaScript.DoString(this.miShuData.StartFightAction, null, null);
		}
	}

	// Token: 0x06002848 RID: 10312 RVA: 0x0001F970 File Offset: 0x0001DB70
	void IFightEventProcessor.SetAvatar(Avatar player, Avatar monstar)
	{
		this.player = player;
		this.target = monstar;
	}

	// Token: 0x06002849 RID: 10313 RVA: 0x0001F980 File Offset: 0x0001DB80
	void IFightEventProcessor.OnUpdateBuff()
	{
		this.OnAnyUpdate();
	}

	// Token: 0x0600284A RID: 10314 RVA: 0x0001F980 File Offset: 0x0001DB80
	void IFightEventProcessor.OnUpdateHP()
	{
		this.OnAnyUpdate();
	}

	// Token: 0x0600284B RID: 10315 RVA: 0x0001F980 File Offset: 0x0001DB80
	void IFightEventProcessor.OnUpdateLingQi()
	{
		this.OnAnyUpdate();
	}

	// Token: 0x0600284C RID: 10316 RVA: 0x0013B78C File Offset: 0x0013998C
	void IFightEventProcessor.OnUpdateRound()
	{
		if (RoundManager.instance.StaticRoundNum <= this.miShuData.RoundLimit)
		{
			this.OnAnyUpdate();
			return;
		}
		this.SaveRecordValue();
		if (this.isSucess)
		{
			if (this.miShuData.StaticValueID != 0)
			{
				this.OnSucessSetStaticValue();
			}
			this.player.TianJieCanLingWuSkills.Add(TianJieMiShuLingWuFightEventProcessor.MiShuID);
			UIPopTip.Inst.Pop("感应成功", PopTipIconType.叹号);
			RoundManager.instance.PlayerFightEventProcessor = null;
			this.target.die();
			return;
		}
		UIPopTip.Inst.Pop("感应失败", PopTipIconType.叹号);
		RoundManager.instance.PlayerFightEventProcessor = null;
		this.player.die();
	}

	// Token: 0x0600284D RID: 10317 RVA: 0x0013B840 File Offset: 0x00139A40
	private void OnAnyUpdate()
	{
		try
		{
			if (this.luaScript.DoString(this.checkSucessScript, null, null).Boolean && !this.isSucess)
			{
				this.isSucess = true;
				Debug.Log("已感应成功");
			}
			if (this.GanYingUI != null)
			{
				this.GanYingUI.Refresh();
			}
		}
		catch (Exception arg)
		{
			Debug.LogError(string.Format("天劫秘术判断时出现异常:\n{0}", arg));
		}
	}

	// Token: 0x0600284E RID: 10318 RVA: 0x0001F988 File Offset: 0x0001DB88
	private void RecordValue(int value)
	{
		if (value > this.recordValue)
		{
			Debug.Log(string.Format("感应时记录值:{0}", value));
			this.recordValue = value;
		}
	}

	// Token: 0x0600284F RID: 10319 RVA: 0x0013B8C0 File Offset: 0x00139AC0
	public int GetSaveRecordValue()
	{
		Avatar avatar = PlayerEx.Player;
		if (avatar.TianJieSkillRecordValue.HasField(TianJieMiShuLingWuFightEventProcessor.MiShuID))
		{
			return avatar.TianJieSkillRecordValue[TianJieMiShuLingWuFightEventProcessor.MiShuID].I;
		}
		return 0;
	}

	// Token: 0x06002850 RID: 10320 RVA: 0x0001F9AF File Offset: 0x0001DBAF
	public void SaveRecordValue()
	{
		this.player.TianJieSkillRecordValue.SetField(TianJieMiShuLingWuFightEventProcessor.MiShuID, this.recordValue);
		Debug.Log(string.Format("保存了 {0} 的历史记录:{1}", TianJieMiShuLingWuFightEventProcessor.MiShuID, this.recordValue));
	}

	// Token: 0x06002851 RID: 10321 RVA: 0x0013B8FC File Offset: 0x00139AFC
	private void OnSucessSetStaticValue()
	{
		JSONObject jsonobject = null;
		foreach (JSONObject jsonobject2 in jsonData.instance.TianJieMiShuData.list)
		{
			if (jsonobject2["id"].Str == TianJieMiShuLingWuFightEventProcessor.MiShuID)
			{
				jsonobject = jsonobject2;
				break;
			}
		}
		if (jsonobject == null)
		{
			Debug.LogError("感应成功时设置全局变量出错，找不到ID为" + TianJieMiShuLingWuFightEventProcessor.MiShuID + "的json");
			return;
		}
		float n = jsonobject["DiYiXiang"].n;
		float n2 = jsonobject["GongBi"].n;
		float n3 = jsonobject["XiuZhengZhi"].n;
		float num = (float)this.recordValue * n3;
		float num2 = n * (1f - Mathf.Pow(n2, num)) / (1f - n2);
		int num3 = Mathf.RoundToInt(num2);
		Debug.Log(string.Format("感应成功，最终分数{0}={1}。计算过程:a1(1-q^n)/(1-q) a1:{2} q:{3} x:{4} n:{5} value = {6} * (1 - Mathf.Pow({7}, {8})) / (1 - {9}) = {10}", new object[]
		{
			this.miShuData.StaticValueID,
			num3,
			n,
			n2,
			n3,
			num,
			n,
			n2,
			num,
			n2,
			num2
		}));
		int num4 = GlobalValue.Get(this.miShuData.StaticValueID, "感应天劫秘术" + TianJieMiShuLingWuFightEventProcessor.MiShuID);
		if (num3 > num4)
		{
			Debug.Log(string.Format("分数刷新记录，设置全局变量{0}={1}", this.miShuData.StaticValueID, num3));
			GlobalValue.Set(this.miShuData.StaticValueID, num3, "感应天劫秘术" + TianJieMiShuLingWuFightEventProcessor.MiShuID);
			return;
		}
		Debug.Log(string.Format("最终分数{0}小于当前记录分数{1}，不记录", num3, num4));
	}

	// Token: 0x06002852 RID: 10322 RVA: 0x0001F9EB File Offset: 0x0001DBEB
	public void SetPlayerHP(int hp)
	{
		this.player.setHP(hp);
	}

	// Token: 0x06002853 RID: 10323 RVA: 0x0001F9F9 File Offset: 0x0001DBF9
	public void AddPlayerBuff(int buffid, int count)
	{
		this.player.spell.addBuff(buffid, count);
	}

	// Token: 0x06002854 RID: 10324 RVA: 0x0013BB0C File Offset: 0x00139D0C
	public int MeBuffCount(bool needRecord, int buffid)
	{
		int buffSum = this.player.buffmag.GetBuffSum(buffid);
		if (needRecord)
		{
			this.RecordValue(buffSum);
		}
		return buffSum;
	}

	// Token: 0x06002855 RID: 10325 RVA: 0x0013BB38 File Offset: 0x00139D38
	public int TargetBuffCount(bool needRecord, int buffid)
	{
		int buffSum = this.target.buffmag.GetBuffSum(buffid);
		if (needRecord)
		{
			this.RecordValue(buffSum);
		}
		return buffSum;
	}

	// Token: 0x06002856 RID: 10326 RVA: 0x0013BB64 File Offset: 0x00139D64
	public int TotalDamage(bool needRecord)
	{
		int allDamage = this.player.fightTemp.AllDamage;
		if (needRecord)
		{
			this.RecordValue(allDamage);
		}
		return allDamage;
	}

	// Token: 0x06002857 RID: 10327 RVA: 0x0013BB90 File Offset: 0x00139D90
	public int TotalAttackTypeDamage(bool needRecord, int attackType)
	{
		int attackTypeRoundDamage = this.player.fightTemp.GetAttackTypeRoundDamage(attackType);
		if (needRecord)
		{
			this.RecordValue(attackTypeRoundDamage);
		}
		return attackTypeRoundDamage;
	}

	// Token: 0x06002858 RID: 10328 RVA: 0x0013BBBC File Offset: 0x00139DBC
	public int RoundDamage(bool needRecord)
	{
		int num = this.player.fightTemp.lastRoundDamage[0];
		if (needRecord)
		{
			this.RecordValue(num);
		}
		return num;
	}

	// Token: 0x06002859 RID: 10329 RVA: 0x0013BBE8 File Offset: 0x00139DE8
	public int RoundDamageCount(bool needRecord)
	{
		int num = this.player.fightTemp.lastRoundDamageCount[0];
		if (needRecord)
		{
			this.RecordValue(num);
		}
		return num;
	}

	// Token: 0x0600285A RID: 10330 RVA: 0x0013BC14 File Offset: 0x00139E14
	public int TotalAddLingQi(bool needRecord)
	{
		int totalAddLingQi = this.player.fightTemp.TotalAddLingQi;
		if (needRecord)
		{
			this.RecordValue(totalAddLingQi);
		}
		return totalAddLingQi;
	}

	// Token: 0x0600285B RID: 10331 RVA: 0x0013BC40 File Offset: 0x00139E40
	public int TotalHealHP(bool needRecord)
	{
		int totalHealHP = this.player.fightTemp.TotalHealHP;
		if (needRecord)
		{
			this.RecordValue(totalHealHP);
		}
		return totalHealHP;
	}

	// Token: 0x0600285C RID: 10332 RVA: 0x0013BC6C File Offset: 0x00139E6C
	public int TotalLingQiCount(bool needRecord)
	{
		int cardNum = this.player.cardMag.getCardNum();
		if (needRecord)
		{
			this.RecordValue(cardNum);
		}
		return cardNum;
	}

	// Token: 0x0600285D RID: 10333 RVA: 0x0013BC98 File Offset: 0x00139E98
	public int MeRoundLossHP(bool needRecord)
	{
		int num = this.player.fightTemp.RoundLossHP[0];
		if (needRecord)
		{
			this.RecordValue(num);
		}
		return num;
	}

	// Token: 0x0600285E RID: 10334 RVA: 0x0013BCC4 File Offset: 0x00139EC4
	public int TargetRoundLossHP(bool needRecord)
	{
		int num = this.target.fightTemp.RoundLossHP[0];
		if (needRecord)
		{
			this.RecordValue(num);
		}
		return num;
	}

	// Token: 0x0600285F RID: 10335 RVA: 0x0013BCF0 File Offset: 0x00139EF0
	public int MeTotalLossHP(bool needRecord)
	{
		int totalLossHP = this.player.fightTemp.TotalLossHP;
		if (needRecord)
		{
			this.RecordValue(totalLossHP);
		}
		return totalLossHP;
	}

	// Token: 0x06002860 RID: 10336 RVA: 0x0013BD1C File Offset: 0x00139F1C
	public int TargetTotalLossHP(bool needRecord)
	{
		int totalLossHP = this.target.fightTemp.TotalLossHP;
		if (needRecord)
		{
			this.RecordValue(totalLossHP);
		}
		return totalLossHP;
	}

	// Token: 0x06002861 RID: 10337 RVA: 0x0013BD48 File Offset: 0x00139F48
	public int HasLingQiTypeCount(bool needRecord)
	{
		int num = 0;
		for (int i = 0; i <= 5; i++)
		{
			if (this.player.cardMag.getCardTypeNum(i) > 0)
			{
				num++;
			}
		}
		if (needRecord)
		{
			this.RecordValue(num);
		}
		return num;
	}

	// Token: 0x040021FF RID: 8703
	public static string MiShuID;

	// Token: 0x04002200 RID: 8704
	public Avatar player;

	// Token: 0x04002201 RID: 8705
	public Avatar target;

	// Token: 0x04002202 RID: 8706
	public TianJieMiShuData miShuData;

	// Token: 0x04002203 RID: 8707
	public int recordValue;

	// Token: 0x04002204 RID: 8708
	public bool isSucess;

	// Token: 0x04002205 RID: 8709
	public Script luaScript;

	// Token: 0x04002206 RID: 8710
	public string checkSucessScript = "";

	// Token: 0x04002207 RID: 8711
	public UIGanYingFight GanYingUI;

	// Token: 0x04002208 RID: 8712
	public static Dictionary<string, object> OtherBindLuaObjects = new Dictionary<string, object>();
}
