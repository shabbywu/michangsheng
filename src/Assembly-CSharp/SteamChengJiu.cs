using System.Collections.Generic;
using KBEngine;
using Newtonsoft.Json.Linq;
using Steamworks;
using UnityEngine;

public class SteamChengJiu
{
	public enum chengjiuType
	{
		BuffRound = 1,
		SkillAll,
		SkillOnce
	}

	private static SteamChengJiu _int;

	public static SteamChengJiu ints
	{
		get
		{
			if (_int == null)
			{
				SteamUserStats.RequestCurrentStats();
				_int = new SteamChengJiu();
			}
			return _int;
		}
	}

	public void YSSetAchievement(string name)
	{
		if (!YSHasAchivement(name))
		{
			Tools.instance.getPlayer().AvatarHasAchivement.Add(name);
		}
	}

	public void SetAchievement(string name)
	{
		if (!HasAchievement(name))
		{
			SteamUserStats.SetAchievement(name);
			SteamUserStats.StoreStats();
		}
	}

	public void SetStat(string name, int num)
	{
		if (PlayerPrefs.GetInt("NowPlayerFileAvatar") < 100)
		{
			Avatar player = Tools.instance.getPlayer();
			if (!player.AvatarChengJiuData.HasField(name))
			{
				player.AvatarChengJiuData.SetField(name, 0);
			}
			player.AvatarChengJiuData.SetField(name, num);
		}
	}

	public int GetStat(string name)
	{
		int result = 0;
		Avatar player = Tools.instance.getPlayer();
		if (player.AvatarChengJiuData.HasField(name))
		{
			result = player.AvatarChengJiuData[name].I;
		}
		return result;
	}

	public JToken IsGetChengJiu(Avatar avatar, int type, int value1, int value2)
	{
		switch (type)
		{
		case 2:
			foreach (KeyValuePair<string, JToken> item in jsonData.instance.ChengJiuJson)
			{
				if ((int)item.Value[(object)"type"] == 2 && (((int)item.Value[(object)"target"] == 1 && avatar == Tools.instance.getPlayer()) || ((int)item.Value[(object)"target"] == 2 && avatar == Tools.instance.getPlayer().OtherAvatar)) && value1 == (int)item.Value[(object)"value1"] && value2 >= (int)item.Value[(object)"value2"] && !YSHasAchivement((string)item.Value[(object)"SteamName"]))
				{
					return item.Value;
				}
			}
			break;
		case 3:
			foreach (KeyValuePair<string, JToken> item2 in jsonData.instance.ChengJiuJson)
			{
				if ((int)item2.Value[(object)"type"] == 3 && (((int)item2.Value[(object)"target"] == 1 && avatar == Tools.instance.getPlayer()) || ((int)item2.Value[(object)"target"] == 2 && avatar == Tools.instance.getPlayer().OtherAvatar)) && value1 == (int)item2.Value[(object)"value1"] && value2 >= (int)item2.Value[(object)"value2"] && !YSHasAchivement((string)item2.Value[(object)"SteamName"]))
				{
					return item2.Value;
				}
			}
			break;
		case 1:
			foreach (KeyValuePair<string, JToken> item3 in jsonData.instance.ChengJiuJson)
			{
				if ((int)item3.Value[(object)"type"] == 1 && (((int)item3.Value[(object)"target"] == 1 && avatar == Tools.instance.getPlayer()) || ((int)item3.Value[(object)"target"] == 2 && avatar == Tools.instance.getPlayer().OtherAvatar)) && value1 == (int)item3.Value[(object)"value1"] && value2 >= (int)item3.Value[(object)"value2"] && !YSHasAchivement((string)item3.Value[(object)"SteamName"]))
				{
					return item3.Value;
				}
			}
			break;
		}
		return null;
	}

	public void FightSkillAllDamageSetStat(Avatar avatar, int skillType, int damage)
	{
		string name = "DamageAll" + skillType;
		SetStat(name, GetStat(name) + damage);
		JToken val = IsGetChengJiu(avatar, 2, skillType, GetStat(name));
		if (val != null)
		{
			YSSetFightAchievment("DamageAll_", val);
		}
	}

	public void FightSkillOnceDamageSetStat(Avatar avatar, int skillType, int damage)
	{
		JToken val = IsGetChengJiu(avatar, 3, skillType, damage);
		if (val != null)
		{
			string name = "DamageOnce" + skillType;
			if (damage > GetStat(name))
			{
				SetStat(name, damage);
				YSSetFightAchievment("DamageOnce_", val);
			}
		}
	}

	public void FightBuffOnceSetStat(Avatar avatar, int buffID, int Round)
	{
		JToken val = IsGetChengJiu(avatar, 1, buffID, Round);
		if (val != null)
		{
			string name = "BuffOnce" + buffID;
			if (Round > GetStat(name))
			{
				SetStat(name, Round);
				YSSetFightAchievment("BuffOnce_", val);
			}
		}
	}

	public string getAchievmentName(string baseName, int value1, int value2)
	{
		return baseName + value1 + "_" + value2;
	}

	public void YSSetFightAchievment(string NameBase, JToken CanSetStat)
	{
		string text = (string)CanSetStat[(object)"SteamName"];
		if (!YSHasAchivement(text))
		{
			YSSetAchievement(text);
			SetAchievement(text);
			if ((int)CanSetStat[(object)"LingGuang"] != 0)
			{
				Tools.instance.getPlayer().wuDaoMag.AddLingGuangByJsonID((int)CanSetStat[(object)"LingGuang"]);
				JSONObject jSONObject = jsonData.instance.LingGuangJson[((int)CanSetStat[(object)"LingGuang"]).ToString()];
				UIPopTip.Inst.Pop("获得新的感悟" + jSONObject["name"].Str, PopTipIconType.感悟);
			}
		}
	}

	public bool HasAchievement(string name)
	{
		bool result = true;
		SteamUserStats.GetAchievement(name, ref result);
		return result;
	}

	public bool YSHasAchivement(string name)
	{
		if (Tools.instance.getPlayer().AvatarHasAchivement.list.Find((JSONObject aa) => aa.str == name) == null)
		{
			return false;
		}
		return true;
	}

	public void StoreStats()
	{
		SteamUserStats.StoreStats();
	}

	public void ResetStatt()
	{
	}
}
