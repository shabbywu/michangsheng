using System;
using System.Collections.Generic;
using Fungus;
using KBEngine;

// Token: 0x020001D1 RID: 465
public class MonstarMag
{
	// Token: 0x0600134D RID: 4941 RVA: 0x000797C4 File Offset: 0x000779C4
	public void ClearBuff()
	{
		this.monstarAddBuff.Clear();
		this.HeroAddBuff.Clear();
	}

	// Token: 0x0600134E RID: 4942 RVA: 0x000797DC File Offset: 0x000779DC
	public bool isNotNamolFight()
	{
		bool result = false;
		if (Tools.instance.monstarMag.FightType == StartFight.FightEnumType.LeiTai)
		{
			result = true;
		}
		else if (Tools.instance.monstarMag.FightType == StartFight.FightEnumType.DuJie)
		{
			result = true;
		}
		else if (Tools.instance.monstarMag.FightType == StartFight.FightEnumType.XingMo)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x0600134F RID: 4943 RVA: 0x0007982C File Offset: 0x00077A2C
	public JSONObject GetAddWuDao(int monstarId)
	{
		return jsonData.instance.KillAvatarLingGuangJson.list.Find((JSONObject temp) => temp["avatar"].HasItem(monstarId));
	}

	// Token: 0x06001350 RID: 4944 RVA: 0x00079868 File Offset: 0x00077A68
	public void AddKillAvatarWuDao(Avatar player, int monstarId)
	{
		JSONObject addWuDao = this.GetAddWuDao(monstarId);
		if (addWuDao != null && !player.WuDaoKillAvatar.HasItem(addWuDao["id"].I))
		{
			player.WuDaoKillAvatar.Add(addWuDao["id"].I);
			player.wuDaoMag.AddLingGuangByJsonID(addWuDao["lingguangid"].I);
			UIPopTip.Inst.Pop("获得新的感悟", PopTipIconType.感悟);
		}
	}

	// Token: 0x06001351 RID: 4945 RVA: 0x000798E4 File Offset: 0x00077AE4
	public JSONObject getFightTypeInfo()
	{
		StartFight.FightEnumType fightType = Tools.instance.monstarMag.FightType;
		foreach (JSONObject jsonobject in jsonData.instance.FightTypeInfoJsonData.list)
		{
			if (jsonobject["id"].I == (int)fightType)
			{
				return jsonobject;
			}
		}
		return null;
	}

	// Token: 0x06001352 RID: 4946 RVA: 0x00079964 File Offset: 0x00077B64
	public bool shouldReloadSaveHp()
	{
		JSONObject fightTypeInfo = this.getFightTypeInfo();
		return fightTypeInfo != null && (int)fightTypeInfo["Type1"].n == 1;
	}

	// Token: 0x06001353 RID: 4947 RVA: 0x00079994 File Offset: 0x00077B94
	public int ReloadHpType()
	{
		JSONObject fightTypeInfo = this.getFightTypeInfo();
		if (fightTypeInfo != null && (int)fightTypeInfo["Type9"].n > 0)
		{
			return (int)fightTypeInfo["Type9"].n;
		}
		return 0;
	}

	// Token: 0x06001354 RID: 4948 RVA: 0x000799D4 File Offset: 0x00077BD4
	public bool PlayerNotDeath()
	{
		JSONObject fightTypeInfo = this.getFightTypeInfo();
		return fightTypeInfo != null && (int)fightTypeInfo["Type2"].n == 1;
	}

	// Token: 0x06001355 RID: 4949 RVA: 0x00079A04 File Offset: 0x00077C04
	public bool isInFubenNotDeath()
	{
		Avatar player = Tools.instance.getPlayer();
		return !(player.NowFuBen == "FRandomBase") && (player.fubenContorl.isInFuBen() && (int)jsonData.instance.FuBenInfoJsonData[player.NowFuBen]["CanDie"].n == 0);
	}

	// Token: 0x06001356 RID: 4950 RVA: 0x00079A68 File Offset: 0x00077C68
	public int FightLose()
	{
		JSONObject fightTypeInfo = this.getFightTypeInfo();
		if (fightTypeInfo != null && (int)fightTypeInfo["Type3"].n > 0)
		{
			return (int)fightTypeInfo["Type3"].n;
		}
		return 0;
	}

	// Token: 0x06001357 RID: 4951 RVA: 0x00079AA8 File Offset: 0x00077CA8
	public bool CanDrowpItem()
	{
		JSONObject fightTypeInfo = this.getFightTypeInfo();
		return fightTypeInfo != null && (int)fightTypeInfo["Type4"].n == 1;
	}

	// Token: 0x06001358 RID: 4952 RVA: 0x00079AD8 File Offset: 0x00077CD8
	public bool CanRunAway()
	{
		JSONObject fightTypeInfo = this.getFightTypeInfo();
		return fightTypeInfo != null && (int)fightTypeInfo["Type5"].n == 1;
	}

	// Token: 0x06001359 RID: 4953 RVA: 0x00079B08 File Offset: 0x00077D08
	public int CanNotRunAwayEvent()
	{
		JSONObject fightTypeInfo = this.getFightTypeInfo();
		if (fightTypeInfo != null && (int)fightTypeInfo["Type6"].n > 0)
		{
			return (int)fightTypeInfo["Type6"].n;
		}
		return 0;
	}

	// Token: 0x0600135A RID: 4954 RVA: 0x00079B48 File Offset: 0x00077D48
	public bool MonstarCanDeath()
	{
		JSONObject fightTypeInfo = this.getFightTypeInfo();
		return fightTypeInfo != null && (int)fightTypeInfo["Type7"].n == 1;
	}

	// Token: 0x0600135B RID: 4955 RVA: 0x00079B78 File Offset: 0x00077D78
	public bool PlayerAddShaQi()
	{
		JSONObject fightTypeInfo = this.getFightTypeInfo();
		return fightTypeInfo != null && (int)fightTypeInfo["Type8"].n == 1;
	}

	// Token: 0x04000EA0 RID: 3744
	public Dictionary<int, int> monstarAddBuff = new Dictionary<int, int>();

	// Token: 0x04000EA1 RID: 3745
	public Dictionary<int, int> HeroAddBuff = new Dictionary<int, int>();

	// Token: 0x04000EA2 RID: 3746
	public StartFight.FightEnumType FightType;

	// Token: 0x04000EA3 RID: 3747
	public int FightTalkID;

	// Token: 0x04000EA4 RID: 3748
	public int FightCardID;

	// Token: 0x04000EA5 RID: 3749
	public int FightImageID;

	// Token: 0x04000EA6 RID: 3750
	public int gameStartHP;
}
