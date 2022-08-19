using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using MoonSharp.Interpreter;
using UnityEngine;

// Token: 0x02000488 RID: 1160
public class TianJieMiShuLingWuFightEventProcessor : IFightEventProcessor
{
	// Token: 0x06002478 RID: 9336 RVA: 0x000FBE6C File Offset: 0x000FA06C
	public TianJieMiShuLingWuFightEventProcessor()
	{
		this.luaScript = new Script();
		this.miShuData = TianJieMiShuData.DataDict[TianJieMiShuLingWuFightEventProcessor.MiShuID];
		this.checkSucessScript = (this.miShuData.PanDing ?? "");
		this.BindLua();
	}

	// Token: 0x06002479 RID: 9337 RVA: 0x000FBECC File Offset: 0x000FA0CC
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

	// Token: 0x0600247A RID: 9338 RVA: 0x000FC14C File Offset: 0x000FA34C
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

	// Token: 0x0600247B RID: 9339 RVA: 0x000FC1D5 File Offset: 0x000FA3D5
	void IFightEventProcessor.SetAvatar(Avatar player, Avatar monstar)
	{
		this.player = player;
		this.target = monstar;
	}

	// Token: 0x0600247C RID: 9340 RVA: 0x000FC1E5 File Offset: 0x000FA3E5
	void IFightEventProcessor.OnUpdateBuff()
	{
		this.OnAnyUpdate();
	}

	// Token: 0x0600247D RID: 9341 RVA: 0x000FC1E5 File Offset: 0x000FA3E5
	void IFightEventProcessor.OnUpdateHP()
	{
		this.OnAnyUpdate();
	}

	// Token: 0x0600247E RID: 9342 RVA: 0x000FC1E5 File Offset: 0x000FA3E5
	void IFightEventProcessor.OnUpdateLingQi()
	{
		this.OnAnyUpdate();
	}

	// Token: 0x0600247F RID: 9343 RVA: 0x000FC1F0 File Offset: 0x000FA3F0
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

	// Token: 0x06002480 RID: 9344 RVA: 0x000FC2A4 File Offset: 0x000FA4A4
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

	// Token: 0x06002481 RID: 9345 RVA: 0x000FC324 File Offset: 0x000FA524
	private void RecordValue(int value)
	{
		if (value > this.recordValue)
		{
			Debug.Log(string.Format("感应时记录值:{0}", value));
			this.recordValue = value;
		}
	}

	// Token: 0x06002482 RID: 9346 RVA: 0x000FC34C File Offset: 0x000FA54C
	public int GetSaveRecordValue()
	{
		Avatar avatar = PlayerEx.Player;
		if (avatar.TianJieSkillRecordValue.HasField(TianJieMiShuLingWuFightEventProcessor.MiShuID))
		{
			return avatar.TianJieSkillRecordValue[TianJieMiShuLingWuFightEventProcessor.MiShuID].I;
		}
		return 0;
	}

	// Token: 0x06002483 RID: 9347 RVA: 0x000FC388 File Offset: 0x000FA588
	public void SaveRecordValue()
	{
		this.player.TianJieSkillRecordValue.SetField(TianJieMiShuLingWuFightEventProcessor.MiShuID, this.recordValue);
		Debug.Log(string.Format("保存了 {0} 的历史记录:{1}", TianJieMiShuLingWuFightEventProcessor.MiShuID, this.recordValue));
	}

	// Token: 0x06002484 RID: 9348 RVA: 0x000FC3C4 File Offset: 0x000FA5C4
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

	// Token: 0x06002485 RID: 9349 RVA: 0x000FC5D4 File Offset: 0x000FA7D4
	public void SetPlayerHP(int hp)
	{
		this.player.setHP(hp);
	}

	// Token: 0x06002486 RID: 9350 RVA: 0x000FC5E2 File Offset: 0x000FA7E2
	public void AddPlayerBuff(int buffid, int count)
	{
		this.player.spell.addBuff(buffid, count);
	}

	// Token: 0x06002487 RID: 9351 RVA: 0x000FC5F8 File Offset: 0x000FA7F8
	public int MeBuffCount(bool needRecord, int buffid)
	{
		int buffSum = this.player.buffmag.GetBuffSum(buffid);
		if (needRecord)
		{
			this.RecordValue(buffSum);
		}
		return buffSum;
	}

	// Token: 0x06002488 RID: 9352 RVA: 0x000FC624 File Offset: 0x000FA824
	public int TargetBuffCount(bool needRecord, int buffid)
	{
		int buffSum = this.target.buffmag.GetBuffSum(buffid);
		if (needRecord)
		{
			this.RecordValue(buffSum);
		}
		return buffSum;
	}

	// Token: 0x06002489 RID: 9353 RVA: 0x000FC650 File Offset: 0x000FA850
	public int TotalDamage(bool needRecord)
	{
		int allDamage = this.player.fightTemp.AllDamage;
		if (needRecord)
		{
			this.RecordValue(allDamage);
		}
		return allDamage;
	}

	// Token: 0x0600248A RID: 9354 RVA: 0x000FC67C File Offset: 0x000FA87C
	public int TotalAttackTypeDamage(bool needRecord, int attackType)
	{
		int attackTypeRoundDamage = this.player.fightTemp.GetAttackTypeRoundDamage(attackType);
		if (needRecord)
		{
			this.RecordValue(attackTypeRoundDamage);
		}
		return attackTypeRoundDamage;
	}

	// Token: 0x0600248B RID: 9355 RVA: 0x000FC6A8 File Offset: 0x000FA8A8
	public int RoundDamage(bool needRecord)
	{
		int num = this.player.fightTemp.lastRoundDamage[0];
		if (needRecord)
		{
			this.RecordValue(num);
		}
		return num;
	}

	// Token: 0x0600248C RID: 9356 RVA: 0x000FC6D4 File Offset: 0x000FA8D4
	public int RoundDamageCount(bool needRecord)
	{
		int num = this.player.fightTemp.lastRoundDamageCount[0];
		if (needRecord)
		{
			this.RecordValue(num);
		}
		return num;
	}

	// Token: 0x0600248D RID: 9357 RVA: 0x000FC700 File Offset: 0x000FA900
	public int TotalAddLingQi(bool needRecord)
	{
		int totalAddLingQi = this.player.fightTemp.TotalAddLingQi;
		if (needRecord)
		{
			this.RecordValue(totalAddLingQi);
		}
		return totalAddLingQi;
	}

	// Token: 0x0600248E RID: 9358 RVA: 0x000FC72C File Offset: 0x000FA92C
	public int TotalHealHP(bool needRecord)
	{
		int totalHealHP = this.player.fightTemp.TotalHealHP;
		if (needRecord)
		{
			this.RecordValue(totalHealHP);
		}
		return totalHealHP;
	}

	// Token: 0x0600248F RID: 9359 RVA: 0x000FC758 File Offset: 0x000FA958
	public int TotalLingQiCount(bool needRecord)
	{
		int cardNum = this.player.cardMag.getCardNum();
		if (needRecord)
		{
			this.RecordValue(cardNum);
		}
		return cardNum;
	}

	// Token: 0x06002490 RID: 9360 RVA: 0x000FC784 File Offset: 0x000FA984
	public int MeRoundLossHP(bool needRecord)
	{
		int num = this.player.fightTemp.RoundLossHP[0];
		if (needRecord)
		{
			this.RecordValue(num);
		}
		return num;
	}

	// Token: 0x06002491 RID: 9361 RVA: 0x000FC7B0 File Offset: 0x000FA9B0
	public int TargetRoundLossHP(bool needRecord)
	{
		int num = this.target.fightTemp.RoundLossHP[0];
		if (needRecord)
		{
			this.RecordValue(num);
		}
		return num;
	}

	// Token: 0x06002492 RID: 9362 RVA: 0x000FC7DC File Offset: 0x000FA9DC
	public int MeTotalLossHP(bool needRecord)
	{
		int totalLossHP = this.player.fightTemp.TotalLossHP;
		if (needRecord)
		{
			this.RecordValue(totalLossHP);
		}
		return totalLossHP;
	}

	// Token: 0x06002493 RID: 9363 RVA: 0x000FC808 File Offset: 0x000FAA08
	public int TargetTotalLossHP(bool needRecord)
	{
		int totalLossHP = this.target.fightTemp.TotalLossHP;
		if (needRecord)
		{
			this.RecordValue(totalLossHP);
		}
		return totalLossHP;
	}

	// Token: 0x06002494 RID: 9364 RVA: 0x000FC834 File Offset: 0x000FAA34
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

	// Token: 0x04001D22 RID: 7458
	public static string MiShuID;

	// Token: 0x04001D23 RID: 7459
	public Avatar player;

	// Token: 0x04001D24 RID: 7460
	public Avatar target;

	// Token: 0x04001D25 RID: 7461
	public TianJieMiShuData miShuData;

	// Token: 0x04001D26 RID: 7462
	public int recordValue;

	// Token: 0x04001D27 RID: 7463
	public bool isSucess;

	// Token: 0x04001D28 RID: 7464
	public Script luaScript;

	// Token: 0x04001D29 RID: 7465
	public string checkSucessScript = "";

	// Token: 0x04001D2A RID: 7466
	public UIGanYingFight GanYingUI;

	// Token: 0x04001D2B RID: 7467
	public static Dictionary<string, object> OtherBindLuaObjects = new Dictionary<string, object>();
}
