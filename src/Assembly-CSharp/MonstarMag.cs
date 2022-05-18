using System;
using System.Collections.Generic;
using Fungus;
using KBEngine;

// Token: 0x020002DC RID: 732
public class MonstarMag
{
	// Token: 0x06001609 RID: 5641 RVA: 0x00013BC5 File Offset: 0x00011DC5
	public void ClearBuff()
	{
		this.monstarAddBuff.Clear();
		this.HeroAddBuff.Clear();
	}

	// Token: 0x0600160A RID: 5642 RVA: 0x000C6518 File Offset: 0x000C4718
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

	// Token: 0x0600160B RID: 5643 RVA: 0x000C6568 File Offset: 0x000C4768
	public JSONObject GetAddWuDao(int monstarId)
	{
		return jsonData.instance.KillAvatarLingGuangJson.list.Find((JSONObject temp) => temp["avatar"].HasItem(monstarId));
	}

	// Token: 0x0600160C RID: 5644 RVA: 0x000C65A4 File Offset: 0x000C47A4
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

	// Token: 0x0600160D RID: 5645 RVA: 0x000C6620 File Offset: 0x000C4820
	public JSONObject getFightTypeInfo()
	{
		StartFight.FightEnumType fightType = Tools.instance.monstarMag.FightType;
		foreach (JSONObject jsonobject in jsonData.instance.FightTypeInfoJsonData.list)
		{
			if ((StartFight.FightEnumType)jsonobject["id"].n == fightType)
			{
				return jsonobject;
			}
		}
		return null;
	}

	// Token: 0x0600160E RID: 5646 RVA: 0x000C66A0 File Offset: 0x000C48A0
	public bool shouldReloadSaveHp()
	{
		JSONObject fightTypeInfo = this.getFightTypeInfo();
		return fightTypeInfo != null && (int)fightTypeInfo["Type1"].n == 1;
	}

	// Token: 0x0600160F RID: 5647 RVA: 0x000C66D0 File Offset: 0x000C48D0
	public int ReloadHpType()
	{
		JSONObject fightTypeInfo = this.getFightTypeInfo();
		if (fightTypeInfo != null && (int)fightTypeInfo["Type9"].n > 0)
		{
			return (int)fightTypeInfo["Type9"].n;
		}
		return 0;
	}

	// Token: 0x06001610 RID: 5648 RVA: 0x000C6710 File Offset: 0x000C4910
	public bool PlayerNotDeath()
	{
		JSONObject fightTypeInfo = this.getFightTypeInfo();
		return fightTypeInfo != null && (int)fightTypeInfo["Type2"].n == 1;
	}

	// Token: 0x06001611 RID: 5649 RVA: 0x000C6740 File Offset: 0x000C4940
	public bool isInFubenNotDeath()
	{
		Avatar player = Tools.instance.getPlayer();
		return !(player.NowFuBen == "FRandomBase") && (player.fubenContorl.isInFuBen() && (int)jsonData.instance.FuBenInfoJsonData[player.NowFuBen]["CanDie"].n == 0);
	}

	// Token: 0x06001612 RID: 5650 RVA: 0x000C67A4 File Offset: 0x000C49A4
	public int FightLose()
	{
		JSONObject fightTypeInfo = this.getFightTypeInfo();
		if (fightTypeInfo != null && (int)fightTypeInfo["Type3"].n > 0)
		{
			return (int)fightTypeInfo["Type3"].n;
		}
		return 0;
	}

	// Token: 0x06001613 RID: 5651 RVA: 0x000C67E4 File Offset: 0x000C49E4
	public bool CanDrowpItem()
	{
		JSONObject fightTypeInfo = this.getFightTypeInfo();
		return fightTypeInfo != null && (int)fightTypeInfo["Type4"].n == 1;
	}

	// Token: 0x06001614 RID: 5652 RVA: 0x000C6814 File Offset: 0x000C4A14
	public bool CanRunAway()
	{
		JSONObject fightTypeInfo = this.getFightTypeInfo();
		return fightTypeInfo != null && (int)fightTypeInfo["Type5"].n == 1;
	}

	// Token: 0x06001615 RID: 5653 RVA: 0x000C6844 File Offset: 0x000C4A44
	public int CanNotRunAwayEvent()
	{
		JSONObject fightTypeInfo = this.getFightTypeInfo();
		if (fightTypeInfo != null && (int)fightTypeInfo["Type6"].n > 0)
		{
			return (int)fightTypeInfo["Type6"].n;
		}
		return 0;
	}

	// Token: 0x06001616 RID: 5654 RVA: 0x000C6884 File Offset: 0x000C4A84
	public bool MonstarCanDeath()
	{
		JSONObject fightTypeInfo = this.getFightTypeInfo();
		return fightTypeInfo != null && (int)fightTypeInfo["Type7"].n == 1;
	}

	// Token: 0x06001617 RID: 5655 RVA: 0x000C68B4 File Offset: 0x000C4AB4
	public bool PlayerAddShaQi()
	{
		JSONObject fightTypeInfo = this.getFightTypeInfo();
		return fightTypeInfo != null && (int)fightTypeInfo["Type8"].n == 1;
	}

	// Token: 0x040011DF RID: 4575
	public Dictionary<int, int> monstarAddBuff = new Dictionary<int, int>();

	// Token: 0x040011E0 RID: 4576
	public Dictionary<int, int> HeroAddBuff = new Dictionary<int, int>();

	// Token: 0x040011E1 RID: 4577
	public StartFight.FightEnumType FightType;

	// Token: 0x040011E2 RID: 4578
	public int FightTalkID;

	// Token: 0x040011E3 RID: 4579
	public int FightCardID;

	// Token: 0x040011E4 RID: 4580
	public int FightImageID;

	// Token: 0x040011E5 RID: 4581
	public int gameStartHP;
}
