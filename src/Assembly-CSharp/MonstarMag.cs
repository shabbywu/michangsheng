using System.Collections.Generic;
using Fungus;
using KBEngine;

public class MonstarMag
{
	public Dictionary<int, int> monstarAddBuff = new Dictionary<int, int>();

	public Dictionary<int, int> HeroAddBuff = new Dictionary<int, int>();

	public StartFight.FightEnumType FightType;

	public int FightTalkID;

	public int FightCardID;

	public int FightImageID;

	public int gameStartHP;

	public void ClearBuff()
	{
		monstarAddBuff.Clear();
		HeroAddBuff.Clear();
	}

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

	public JSONObject GetAddWuDao(int monstarId)
	{
		return jsonData.instance.KillAvatarLingGuangJson.list.Find((JSONObject temp) => temp["avatar"].HasItem(monstarId));
	}

	public void AddKillAvatarWuDao(Avatar player, int monstarId)
	{
		JSONObject addWuDao = GetAddWuDao(monstarId);
		if (addWuDao != null && !player.WuDaoKillAvatar.HasItem(addWuDao["id"].I))
		{
			player.WuDaoKillAvatar.Add(addWuDao["id"].I);
			player.wuDaoMag.AddLingGuangByJsonID(addWuDao["lingguangid"].I);
			UIPopTip.Inst.Pop("获得新的感悟", PopTipIconType.感悟);
		}
	}

	public JSONObject getFightTypeInfo()
	{
		StartFight.FightEnumType fightType = Tools.instance.monstarMag.FightType;
		foreach (JSONObject item in jsonData.instance.FightTypeInfoJsonData.list)
		{
			if (item["id"].I == (int)fightType)
			{
				return item;
			}
		}
		return null;
	}

	public bool shouldReloadSaveHp()
	{
		JSONObject fightTypeInfo = getFightTypeInfo();
		if (fightTypeInfo != null && (int)fightTypeInfo["Type1"].n == 1)
		{
			return true;
		}
		return false;
	}

	public int ReloadHpType()
	{
		JSONObject fightTypeInfo = getFightTypeInfo();
		if (fightTypeInfo != null && (int)fightTypeInfo["Type9"].n > 0)
		{
			return (int)fightTypeInfo["Type9"].n;
		}
		return 0;
	}

	public bool PlayerNotDeath()
	{
		JSONObject fightTypeInfo = getFightTypeInfo();
		if (fightTypeInfo != null && (int)fightTypeInfo["Type2"].n == 1)
		{
			return true;
		}
		return false;
	}

	public bool isInFubenNotDeath()
	{
		Avatar player = Tools.instance.getPlayer();
		if (player.NowFuBen == "FRandomBase")
		{
			return false;
		}
		if (player.fubenContorl.isInFuBen() && (int)jsonData.instance.FuBenInfoJsonData[player.NowFuBen]["CanDie"].n == 0)
		{
			return true;
		}
		return false;
	}

	public int FightLose()
	{
		JSONObject fightTypeInfo = getFightTypeInfo();
		if (fightTypeInfo != null && (int)fightTypeInfo["Type3"].n > 0)
		{
			return (int)fightTypeInfo["Type3"].n;
		}
		return 0;
	}

	public bool CanDrowpItem()
	{
		JSONObject fightTypeInfo = getFightTypeInfo();
		if (fightTypeInfo != null && (int)fightTypeInfo["Type4"].n == 1)
		{
			return true;
		}
		return false;
	}

	public bool CanRunAway()
	{
		JSONObject fightTypeInfo = getFightTypeInfo();
		if (fightTypeInfo != null && (int)fightTypeInfo["Type5"].n == 1)
		{
			return true;
		}
		return false;
	}

	public int CanNotRunAwayEvent()
	{
		JSONObject fightTypeInfo = getFightTypeInfo();
		if (fightTypeInfo != null && (int)fightTypeInfo["Type6"].n > 0)
		{
			return (int)fightTypeInfo["Type6"].n;
		}
		return 0;
	}

	public bool MonstarCanDeath()
	{
		JSONObject fightTypeInfo = getFightTypeInfo();
		if (fightTypeInfo != null && (int)fightTypeInfo["Type7"].n == 1)
		{
			return true;
		}
		return false;
	}

	public bool PlayerAddShaQi()
	{
		JSONObject fightTypeInfo = getFightTypeInfo();
		if (fightTypeInfo != null && (int)fightTypeInfo["Type8"].n == 1)
		{
			return true;
		}
		return false;
	}
}
