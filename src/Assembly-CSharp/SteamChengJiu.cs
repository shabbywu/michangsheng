using System;
using System.Collections.Generic;
using KBEngine;
using Newtonsoft.Json.Linq;
using Steamworks;
using UnityEngine;

// Token: 0x02000485 RID: 1157
public class SteamChengJiu
{
	// Token: 0x1700028F RID: 655
	// (get) Token: 0x0600244F RID: 9295 RVA: 0x000FB08A File Offset: 0x000F928A
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

	// Token: 0x06002450 RID: 9296 RVA: 0x000FB0A8 File Offset: 0x000F92A8
	public void YSSetAchievement(string name)
	{
		if (this.YSHasAchivement(name))
		{
			return;
		}
		Tools.instance.getPlayer().AvatarHasAchivement.Add(name);
	}

	// Token: 0x06002451 RID: 9297 RVA: 0x000FB0C9 File Offset: 0x000F92C9
	public void SetAchievement(string name)
	{
		if (this.HasAchievement(name))
		{
			return;
		}
		SteamUserStats.SetAchievement(name);
		SteamUserStats.StoreStats();
	}

	// Token: 0x06002452 RID: 9298 RVA: 0x000FB0E4 File Offset: 0x000F92E4
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

	// Token: 0x06002453 RID: 9299 RVA: 0x000FB134 File Offset: 0x000F9334
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

	// Token: 0x06002454 RID: 9300 RVA: 0x000FB170 File Offset: 0x000F9370
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

	// Token: 0x06002455 RID: 9301 RVA: 0x000FB4C0 File Offset: 0x000F96C0
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

	// Token: 0x06002456 RID: 9302 RVA: 0x000FB510 File Offset: 0x000F9710
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

	// Token: 0x06002457 RID: 9303 RVA: 0x000FB55C File Offset: 0x000F975C
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

	// Token: 0x06002458 RID: 9304 RVA: 0x000FB5A6 File Offset: 0x000F97A6
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

	// Token: 0x06002459 RID: 9305 RVA: 0x000FB5D4 File Offset: 0x000F97D4
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

	// Token: 0x0600245A RID: 9306 RVA: 0x000FB690 File Offset: 0x000F9890
	public bool HasAchievement(string name)
	{
		bool result = true;
		SteamUserStats.GetAchievement(name, ref result);
		return result;
	}

	// Token: 0x0600245B RID: 9307 RVA: 0x000FB6AC File Offset: 0x000F98AC
	public bool YSHasAchivement(string name)
	{
		return Tools.instance.getPlayer().AvatarHasAchivement.list.Find((JSONObject aa) => aa.str == name) != null;
	}

	// Token: 0x0600245C RID: 9308 RVA: 0x000FB6F0 File Offset: 0x000F98F0
	public void StoreStats()
	{
		SteamUserStats.StoreStats();
	}

	// Token: 0x0600245D RID: 9309 RVA: 0x00004095 File Offset: 0x00002295
	public void ResetStatt()
	{
	}

	// Token: 0x04001D06 RID: 7430
	private static SteamChengJiu _int;

	// Token: 0x020013B1 RID: 5041
	public enum chengjiuType
	{
		// Token: 0x04006910 RID: 26896
		BuffRound = 1,
		// Token: 0x04006911 RID: 26897
		SkillAll,
		// Token: 0x04006912 RID: 26898
		SkillOnce
	}
}
