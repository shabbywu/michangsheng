using System;
using System.Collections.Generic;
using KBEngine;
using Newtonsoft.Json.Linq;
using Steamworks;
using UnityEngine;

// Token: 0x02000650 RID: 1616
public class SteamChengJiu
{
	// Token: 0x170002D8 RID: 728
	// (get) Token: 0x0600282B RID: 10283 RVA: 0x0001F8CC File Offset: 0x0001DACC
	public static SteamChengJiu ints
	{
		get
		{
			if (SteamChengJiu._int == null)
			{
				SteamUserStats.RequestCurrentStats();
				SteamChengJiu._int = new SteamChengJiu();
			}
			return SteamChengJiu._int;
		}
	}

	// Token: 0x0600282C RID: 10284 RVA: 0x0001F8EA File Offset: 0x0001DAEA
	public void YSSetAchievement(string name)
	{
		if (this.YSHasAchivement(name))
		{
			return;
		}
		Tools.instance.getPlayer().AvatarHasAchivement.Add(name);
	}

	// Token: 0x0600282D RID: 10285 RVA: 0x0001F90B File Offset: 0x0001DB0B
	public void SetAchievement(string name)
	{
		if (this.HasAchievement(name))
		{
			return;
		}
		SteamUserStats.SetAchievement(name);
	}

	// Token: 0x0600282E RID: 10286 RVA: 0x0013AC68 File Offset: 0x00138E68
	public void SetStat(string name, int num)
	{
		if (PlayerPrefs.GetInt("NowPlayerFileAvatar") >= 100)
		{
			return;
		}
		Avatar player = Tools.instance.getPlayer();
		if (!player.AvatarChengJiuData.HasField(name))
		{
			player.AvatarChengJiuData.SetField(name, 0);
		}
		player.AvatarChengJiuData.SetField(name, num);
	}

	// Token: 0x0600282F RID: 10287 RVA: 0x0013ACB8 File Offset: 0x00138EB8
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

	// Token: 0x06002830 RID: 10288 RVA: 0x0013ACF4 File Offset: 0x00138EF4
	public JToken IsGetChengJiu(Avatar avatar, int type, int value1, int value2)
	{
		switch (type)
		{
		case 1:
			goto IL_21B;
		case 2:
			using (IEnumerator<KeyValuePair<string, JToken>> enumerator = jsonData.instance.ChengJiuJson.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<string, JToken> keyValuePair = enumerator.Current;
					if ((int)keyValuePair.Value["type"] == 2 && (((int)keyValuePair.Value["target"] == 1 && avatar == Tools.instance.getPlayer()) || ((int)keyValuePair.Value["target"] == 2 && avatar == Tools.instance.getPlayer().OtherAvatar)) && value1 == (int)keyValuePair.Value["value1"] && value2 >= (int)keyValuePair.Value["value2"] && !this.YSHasAchivement((string)keyValuePair.Value["SteamName"]))
					{
						return keyValuePair.Value;
					}
				}
				goto IL_317;
			}
			break;
		case 3:
			break;
		default:
			goto IL_317;
		}
		using (IEnumerator<KeyValuePair<string, JToken>> enumerator = jsonData.instance.ChengJiuJson.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<string, JToken> keyValuePair2 = enumerator.Current;
				if ((int)keyValuePair2.Value["type"] == 3 && (((int)keyValuePair2.Value["target"] == 1 && avatar == Tools.instance.getPlayer()) || ((int)keyValuePair2.Value["target"] == 2 && avatar == Tools.instance.getPlayer().OtherAvatar)) && value1 == (int)keyValuePair2.Value["value1"] && value2 >= (int)keyValuePair2.Value["value2"] && !this.YSHasAchivement((string)keyValuePair2.Value["SteamName"]))
				{
					return keyValuePair2.Value;
				}
			}
			goto IL_317;
		}
		IL_21B:
		foreach (KeyValuePair<string, JToken> keyValuePair3 in jsonData.instance.ChengJiuJson)
		{
			if ((int)keyValuePair3.Value["type"] == 1 && (((int)keyValuePair3.Value["target"] == 1 && avatar == Tools.instance.getPlayer()) || ((int)keyValuePair3.Value["target"] == 2 && avatar == Tools.instance.getPlayer().OtherAvatar)) && value1 == (int)keyValuePair3.Value["value1"] && value2 >= (int)keyValuePair3.Value["value2"] && !this.YSHasAchivement((string)keyValuePair3.Value["SteamName"]))
			{
				return keyValuePair3.Value;
			}
		}
		IL_317:
		return null;
	}

	// Token: 0x06002831 RID: 10289 RVA: 0x0013B044 File Offset: 0x00139244
	public void FightSkillAllDamageSetStat(Avatar avatar, int skillType, int damage)
	{
		string name = "DamageAll" + skillType;
		this.SetStat(name, this.GetStat(name) + damage);
		JToken jtoken = this.IsGetChengJiu(avatar, 2, skillType, this.GetStat(name));
		if (jtoken != null)
		{
			this.YSSetFightAchievment("DamageAll_", jtoken);
		}
	}

	// Token: 0x06002832 RID: 10290 RVA: 0x0013B094 File Offset: 0x00139294
	public void FightSkillOnceDamageSetStat(Avatar avatar, int skillType, int damage)
	{
		JToken jtoken = this.IsGetChengJiu(avatar, 3, skillType, damage);
		if (jtoken != null)
		{
			string name = "DamageOnce" + skillType;
			if (damage > this.GetStat(name))
			{
				this.SetStat(name, damage);
				this.YSSetFightAchievment("DamageOnce_", jtoken);
			}
		}
	}

	// Token: 0x06002833 RID: 10291 RVA: 0x0013B0E0 File Offset: 0x001392E0
	public void FightBuffOnceSetStat(Avatar avatar, int buffID, int Round)
	{
		JToken jtoken = this.IsGetChengJiu(avatar, 1, buffID, Round);
		if (jtoken != null)
		{
			string name = "BuffOnce" + buffID;
			if (Round > this.GetStat(name))
			{
				this.SetStat(name, Round);
				this.YSSetFightAchievment("BuffOnce_", jtoken);
			}
		}
	}

	// Token: 0x06002834 RID: 10292 RVA: 0x0001F91E File Offset: 0x0001DB1E
	public string getAchievmentName(string baseName, int value1, int value2)
	{
		return string.Concat(new object[]
		{
			baseName,
			value1,
			"_",
			value2
		});
	}

	// Token: 0x06002835 RID: 10293 RVA: 0x0013B12C File Offset: 0x0013932C
	public void YSSetFightAchievment(string NameBase, JToken CanSetStat)
	{
		string text = (string)CanSetStat["SteamName"];
		if (!this.YSHasAchivement(text))
		{
			this.YSSetAchievement(text);
			this.SetAchievement(text);
			if ((int)CanSetStat["LingGuang"] != 0)
			{
				Tools.instance.getPlayer().wuDaoMag.AddLingGuangByJsonID((int)CanSetStat["LingGuang"]);
				JSONObject jsonobject = jsonData.instance.LingGuangJson[((int)CanSetStat["LingGuang"]).ToString()];
				UIPopTip.Inst.Pop("获得新的感悟" + jsonobject["name"].Str, PopTipIconType.感悟);
			}
		}
	}

	// Token: 0x06002836 RID: 10294 RVA: 0x0013B1E8 File Offset: 0x001393E8
	public bool HasAchievement(string name)
	{
		bool result = true;
		SteamUserStats.GetAchievement(name, ref result);
		return result;
	}

	// Token: 0x06002837 RID: 10295 RVA: 0x0013B204 File Offset: 0x00139404
	public bool YSHasAchivement(string name)
	{
		return Tools.instance.getPlayer().AvatarHasAchivement.list.Find((JSONObject aa) => aa.str == name) != null;
	}

	// Token: 0x06002838 RID: 10296 RVA: 0x0001F949 File Offset: 0x0001DB49
	public void StoreStats()
	{
		SteamUserStats.StoreStats();
	}

	// Token: 0x06002839 RID: 10297 RVA: 0x000042DD File Offset: 0x000024DD
	public void ResetStatt()
	{
	}

	// Token: 0x040021F7 RID: 8695
	private static SteamChengJiu _int;

	// Token: 0x02000651 RID: 1617
	public enum chengjiuType
	{
		// Token: 0x040021F9 RID: 8697
		BuffRound = 1,
		// Token: 0x040021FA RID: 8698
		SkillAll,
		// Token: 0x040021FB RID: 8699
		SkillOnce
	}
}
