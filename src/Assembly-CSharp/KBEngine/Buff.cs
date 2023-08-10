using System;
using System.Collections.Generic;
using System.Reflection;
using Fungus;
using GUIPackage;
using JSONClass;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Events;
using YSGame;
using YSGame.Fight;
using script.world_script;

namespace KBEngine;

public class Buff
{
	public enum AllBUFF
	{
		BUFF1 = 1,
		BUFF2 = 2,
		BUFF3 = 3,
		BUFF4 = 4,
		BUFF5 = 5,
		BUFF6 = 6,
		BUFF7 = 7,
		BUFF8 = 8,
		BUFF9 = 9,
		BUFF10 = 10,
		BUFF11 = 11,
		BUFF12 = 12,
		BUFF13 = 13,
		BUFF14 = 14,
		BUFF15 = 15,
		BUFF16 = 16,
		BUFF17 = 17,
		BUFF18 = 18,
		BUFF19 = 19,
		BUFF20 = 20,
		BUFF21 = 21,
		BUFF22 = 22,
		BUFF23 = 23,
		BUFF24 = 24,
		BUFF25 = 25,
		BUFF26 = 26,
		BUFF27 = 27,
		BUFF28 = 28,
		BUFF29 = 29,
		BUFF30 = 30,
		BUFF31 = 31,
		BUFF32 = 32,
		BUFF33 = 33,
		BUFF34 = 34,
		BUFF35 = 35,
		BUFF36 = 36,
		BUFF37 = 37,
		BUFF38 = 38,
		BUFF39 = 39,
		BUFF40 = 40,
		BUFF41 = 41,
		BUFF42 = 42,
		BUFF43 = 43,
		BUFF44 = 44,
		BUFF45 = 45,
		BUFF46 = 46,
		BUFF47 = 47,
		BUFF48 = 48,
		BUFF49 = 49,
		BUFF58 = 58,
		BUFF59 = 59,
		BUFF62 = 62,
		BUFF65 = 65,
		BUFF68 = 68,
		BUFF73 = 73,
		BUFF74 = 74,
		BUFF75 = 75,
		BUFF76 = 76,
		BUFF77 = 77,
		BUFF81 = 81,
		BUFF83 = 83,
		BUFF85 = 85,
		BUFF86 = 86,
		BUFF90 = 90,
		BUFF91 = 91,
		BUFF95 = 95,
		BUFF96 = 96,
		BUFF97 = 97,
		BUFF99 = 99,
		BUFF101 = 101,
		BUFF102 = 102,
		BUFF103 = 103,
		BUFF106 = 106,
		BUFF107 = 107,
		BUFF110 = 110,
		BUFF113 = 113,
		BUFF116 = 116,
		BUFF118 = 118,
		BUFF119 = 119,
		BUFF120 = 120,
		BUFF121 = 121,
		BUFF122 = 122,
		BUFF126 = 126,
		BUFF128 = 128,
		BUFF129 = 129,
		BUFF136 = 136,
		BUFF139 = 139,
		BUFF140 = 140,
		BUFF143 = 143,
		BUFF144 = 144,
		BUFF150 = 150,
		BUFF151 = 151,
		BUFF152 = 152,
		BUFF155 = 155,
		BUFF156 = 156,
		BUFF157 = 157,
		BUFF158 = 158,
		BUFF159 = 159,
		BUFF160 = 160,
		BUFF161 = 161,
		BUFF162 = 162,
		BUFF163 = 163,
		BUFF164 = 164,
		BUFF165 = 165,
		BUFF166 = 166,
		BUFF167 = 167,
		BUFF168 = 168,
		BUFF169 = 169,
		BUFF170 = 170,
		BUFF171 = 171,
		BUFF172 = 172,
		BUFF173 = 173,
		BUFF174 = 174,
		BUFF175 = 175,
		BUFF176 = 176,
		BUFF177 = 177,
		BUFF178 = 178,
		BUFF180 = 180,
		BUFF181 = 181,
		BUFF182 = 182,
		BUFF189 = 189,
		BUFF204 = 204,
		BUFF205 = 205,
		BUFF206 = 206,
		BUFF207 = 207,
		BUFF209 = 209,
		BUFF210 = 210,
		BUFF211 = 211,
		BUFF213 = 213,
		BUFF214 = 214,
		BUFF215 = 215,
		BUFF216 = 216,
		BUFF217 = 217,
		BUFF218 = 218,
		BUFF219 = 219
	}

	public Dictionary<int, JSONObject> buffSeidList = new Dictionary<int, JSONObject>();

	public JObject NowBuffInfo = new JObject();

	public int buffID;

	public int _loopTime;

	public int _totalTime;

	public List<int> seid = new List<int>();

	private static Dictionary<string, MethodInfo> methodDict = new Dictionary<string, MethodInfo>();

	public static int _NeiShangLoopCount;

	private static void InitMethod(string methodName)
	{
		if (!methodDict.ContainsKey(methodName))
		{
			MethodInfo method = typeof(Buff).GetMethod(methodName);
			methodDict.Add(methodName, method);
		}
	}

	public Buff(int buffid)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		buffID = buffid;
		_totalTime = jsonData.instance.BuffJsonData[buffid.ToString()]["totaltime"].I;
		foreach (JSONObject item in jsonData.instance.BuffJsonData[buffid.ToString()]["seid"].list)
		{
			seid.Add(item.I);
		}
	}

	public void onLoopTrigger(Entity _avatar, List<int> buffInfo, List<int> flag, BuffLoopData buffLoopData)
	{
		try
		{
			foreach (int item in buffLoopData.seid)
			{
				for (int i = 0; i < buffLoopData.TargetLoopTime; i++)
				{
					loopRealizeSeid(item, _avatar, buffInfo, flag);
				}
				if (!CanRealizeSeid((Avatar)_avatar, flag, item, buffLoopData, buffInfo))
				{
					break;
				}
			}
		}
		catch (Exception ex)
		{
			string text = "";
			if (buffInfo == null)
			{
				text = "null";
			}
			else
			{
				for (int j = 0; j < buffInfo.Count; j++)
				{
					text += $"{buffInfo[j]} ";
				}
			}
			string text2 = "";
			if (flag == null)
			{
				text2 = "null";
			}
			else
			{
				for (int k = 0; k < flag.Count; k++)
				{
					text2 += $"{flag[k]} ";
				}
			}
			string text3 = "";
			text3 = ((buffLoopData != null) ? text3.ToString() : "null");
			Debug.LogError((object)$"检测到buff错误！错误 BuffID:{buffInfo[2]}\nbuffInfo:{text}\nflag:{text2}\nbuffLoopDataStr:{text3}");
			Debug.LogError((object)ex);
		}
	}

	public void onAttach(Entity _avatar, List<int> buffInfo)
	{
		foreach (int item in seid)
		{
			onAttachRealizeSeid(item, _avatar, buffInfo);
		}
	}

	public void onDetach(Entity _avatar, List<int> buffInfo)
	{
		foreach (int item in seid)
		{
			onDetachRealizeSeid(item, _avatar, buffInfo);
		}
	}

	public void loopRealizeSeid(int seid, Entity _avatar, List<int> buffInfo, List<int> flag)
	{
		Avatar avatar = (Avatar)_avatar;
		string text = "ListRealizeSeid" + seid;
		InitMethod(text);
		if (methodDict[text] != null)
		{
			methodDict[text].Invoke(this, new object[4] { seid, avatar, buffInfo, flag });
		}
	}

	public void onAttachRealizeSeid(int seid, Entity _avatar, List<int> buffInfo)
	{
		Avatar avatar = (Avatar)_avatar;
		string text = "onAttachRealizeSeid" + seid;
		InitMethod(text);
		if (methodDict[text] != null)
		{
			methodDict[text].Invoke(this, new object[3] { seid, avatar, buffInfo });
		}
	}

	public void onDetachRealizeSeid(int seid, Entity _avatar, List<int> buffInfo)
	{
		Avatar avatar = (Avatar)_avatar;
		string text = "onDetachRealizeSeid" + seid;
		InitMethod(text);
		if (methodDict[text] != null)
		{
			methodDict[text].Invoke(this, new object[3] { seid, avatar, buffInfo });
		}
	}

	public Avatar getTargetAvatar(int seid, Avatar attker)
	{
		if (getSeidJson(seid)["target"].I == 1)
		{
			return attker;
		}
		return attker.OtherAvatar;
	}

	public JSONObject getSeidJson(int seid)
	{
		if (!buffSeidList.ContainsKey(seid))
		{
			if (seid >= jsonData.instance.BuffSeidJsonData.Length)
			{
				Debug.LogError((object)$"获取buff seid数据失败，buff id:{buffID}，seid:{seid}，seid超出了jsonData.instance.BuffSeidJsonData.Length，请检查配表");
				return null;
			}
			JSONObject jSONObject = jsonData.instance.BuffSeidJsonData[seid];
			if (jSONObject.HasField(buffID.ToString()))
			{
				buffSeidList[seid] = jSONObject[buffID.ToString()];
			}
			else
			{
				Debug.LogError((object)$"获取buff seid数据失败，buff id:{buffID}，seid:{seid}，buff seid{seid}表中不存在buff {buffID}的数据，请检查配表");
			}
		}
		return buffSeidList[seid];
	}

	public static JSONObject getSeidJson(int seid, int _buffID)
	{
		return jsonData.instance.BuffSeidJsonData[seid][_buffID.ToString()];
	}

	public bool CanRealized(Avatar _avatar, List<int> flag, List<int> buffInfo = null)
	{
		foreach (int item in seid)
		{
			if (!CanRealizeSeid(_avatar, flag, item, null, buffInfo))
			{
				return false;
			}
		}
		return true;
	}

	public bool CanRealizeSeid(Avatar _avatar, List<int> flag, int nowSeid, BuffLoopData buffLoopData = null, List<int> buffInfo = null)
	{
		if (nowSeid == 62 && (float)_avatar.HP / (float)_avatar.HP_Max * 100f > (float)getSeidJson(nowSeid)["value1"].I)
		{
			return false;
		}
		if (nowSeid == 65 && RandomX(getSeidJson(nowSeid)["value1"].I))
		{
			return false;
		}
		if (nowSeid == 73 && _avatar.OtherAvatar.crystal.getCardNum() == 0)
		{
			return false;
		}
		if (nowSeid == 74 && !_avatar.OtherAvatar.buffmag.HasBuff(getSeidJson(nowSeid)["value1"].I))
		{
			return false;
		}
		if (nowSeid == 75)
		{
			for (int i = 0; i < getSeidJson(nowSeid)["value1"].Count; i++)
			{
				if (!_avatar.buffmag.HasBuff(getSeidJson(nowSeid)["value1"][i].I))
				{
					return false;
				}
			}
		}
		if (nowSeid == 76)
		{
			try
			{
				if (_skillJsonData.DataDict[flag[1]].Skill_ID != getSeidJson(nowSeid)["value1"].I)
				{
					return false;
				}
			}
			catch (Exception ex)
			{
				Debug.Log((object)ex);
			}
		}
		if (nowSeid == 77 && _avatar.HP < _avatar.HP_Max)
		{
			return false;
		}
		if (nowSeid == 81)
		{
			int num = flag[1];
			int num2 = 0;
			GUIPackage.Skill skill = null;
			foreach (GUIPackage.Skill item in _avatar.skill)
			{
				if (num != item.skill_ID)
				{
					continue;
				}
				skill = item;
				foreach (KeyValuePair<int, int> item2 in item.nowSkillUseCard)
				{
					num2 += item2.Value;
				}
				break;
			}
			if (num2 < getSeidJson(nowSeid)["value1"].I)
			{
				return false;
			}
			if (skill != null && buffInfo != null)
			{
				if (skill.nowSkillIsChuFa.ContainsKey(buffInfo[2]) && skill.nowSkillIsChuFa[buffInfo[2]])
				{
					return false;
				}
				skill.nowSkillIsChuFa[buffInfo[2]] = true;
			}
		}
		if (nowSeid == 85)
		{
			int key = flag[1];
			List<JSONObject> list = getSeidJson(nowSeid)["value1"].list;
			bool flag2 = true;
			foreach (JSONObject __i2 in list)
			{
				if (_skillJsonData.DataDict[key].AttackType.FindAll((int a) => a == __i2.I).Count > 0)
				{
					flag2 = false;
					break;
				}
			}
			if (flag2)
			{
				return false;
			}
		}
		if (nowSeid == 86 && Tools.instance.MonstarID != getSeidJson(nowSeid)["value1"].I)
		{
			return false;
		}
		if (nowSeid == 91 && _avatar.OtherAvatar.buffmag.checkHasBuff(getSeidJson(nowSeid)["value1"].I, _avatar.OtherAvatar))
		{
			return false;
		}
		if (nowSeid == 95 && _avatar.fightTemp.NowRoundUsedSkills.Count > 0)
		{
			return false;
		}
		if (nowSeid == 96 && _avatar.fightTemp.lastRoundDamage[0] > 0)
		{
			return false;
		}
		if (nowSeid == 97 && (float)_avatar.HP / (float)_avatar.HP_Max * 100f > (float)getSeidJson(nowSeid)["value1"].I)
		{
			return false;
		}
		if (nowSeid == 101 && _avatar.cardMag.getCardTypeNum(getSeidJson(nowSeid)["value1"].I) <= 0)
		{
			return false;
		}
		if (nowSeid == 102 && _avatar.AvatarType == getSeidJson(nowSeid)["value1"].I)
		{
			return false;
		}
		if (nowSeid == 110)
		{
			foreach (JSONObject item3 in getSeidJson(nowSeid)["value1"].list)
			{
				if (!_avatar.buffmag.HasBuff(item3.I))
				{
					return false;
				}
			}
		}
		if (nowSeid == 113)
		{
			int key2 = flag[1];
			List<JSONObject> list2 = getSeidJson(nowSeid)["value1"].list;
			bool flag3 = true;
			foreach (JSONObject __i in list2)
			{
				if (_skillJsonData.DataDict[key2].AttackType.FindAll((int a) => a == __i.I).Count == 0)
				{
					flag3 = false;
					break;
				}
			}
			if (flag3)
			{
				return false;
			}
		}
		if (nowSeid == 116 && _avatar.Dandu <= getSeidJson(nowSeid)["value1"].I)
		{
			return false;
		}
		if (nowSeid == 118 && _avatar.fightTemp.AllDamage <= getSeidJson(nowSeid)["value1"].I)
		{
			return false;
		}
		if (nowSeid == 119)
		{
			List<List<int>> buffByID = getTargetAvatar(nowSeid, _avatar).buffmag.getBuffByID(getSeidJson(nowSeid)["value1"].I);
			int num3 = 0;
			foreach (List<int> item4 in buffByID)
			{
				num3 += item4[1];
			}
			if (num3 <= getSeidJson(nowSeid)["value2"].I)
			{
				return false;
			}
		}
		if (nowSeid == 120 && _avatar.cardMag[getSeidJson(nowSeid)["value1"].I] == 0)
		{
			return false;
		}
		if (nowSeid == 121 && _avatar.cardMag[getSeidJson(nowSeid)["value1"].I] != 0)
		{
			return false;
		}
		if (nowSeid == 122)
		{
			int key3 = flag[1];
			_ = jsonData.instance.skillJsonData[key3.ToString()]["AttackType"];
			if (!_skillJsonData.DataDict[key3].AttackType.Contains(getSeidJson(nowSeid)["value1"].I))
			{
				return false;
			}
		}
		if (nowSeid == 126)
		{
			int buffRoundByID = _avatar.buffmag.GetBuffRoundByID(getSeidJson(nowSeid)["value1"].I);
			int buffRoundByID2 = _avatar.buffmag.GetBuffRoundByID(getSeidJson(nowSeid)["value2"].I);
			if (buffRoundByID <= buffRoundByID2)
			{
				return false;
			}
		}
		if (nowSeid == 128)
		{
			int i2 = getSeidJson(nowSeid)["value1"].I;
			if (_avatar.BuffSeidFlag[nowSeid][buffID] < i2)
			{
				return false;
			}
			bool flag4 = true;
			foreach (JSONObject item5 in getSeidJson(nowSeid)["value2"].list)
			{
				if (!_skillJsonData.DataDict[flag[1]].AttackType.Contains(item5.I))
				{
					flag4 = false;
				}
			}
			if (!flag4)
			{
				return false;
			}
		}
		if (nowSeid == 150)
		{
			if (_avatar.UsedSkills.Count == 0)
			{
				return false;
			}
			_ = getSeidJson(nowSeid)["value1"].I;
			if (!GUIPackage.Skill.IsSkillType(_avatar.UsedSkills[_avatar.UsedSkills.Count - 1], getSeidJson(nowSeid)["value1"].I))
			{
				return false;
			}
		}
		if (nowSeid == 151)
		{
			int key4 = flag[1];
			_ = jsonData.instance.skillJsonData[key4.ToString()];
			if (_skillJsonData.DataDict[key4].script != "SkillAttack")
			{
				return false;
			}
		}
		if (nowSeid == 152)
		{
			int key5 = flag[1];
			_ = jsonData.instance.skillJsonData[key5.ToString()];
			if (_skillJsonData.DataDict[key5].HP <= 0)
			{
				return false;
			}
		}
		if (nowSeid == 155)
		{
			if (_avatar.UsedSkills.Count == 0)
			{
				return false;
			}
			int num4 = flag[1];
			int num5 = _avatar.UsedSkills[_avatar.UsedSkills.Count - 1];
			if (num4 != num5)
			{
				return false;
			}
		}
		if (nowSeid == 156)
		{
			int buffTypeNum = getTargetAvatar(nowSeid, _avatar).buffmag.getBuffTypeNum(getSeidJson(nowSeid)["value1"].I);
			if (buffLoopData == null || buffTypeNum <= 0)
			{
				return false;
			}
			buffTypeNum--;
			buffLoopData.TargetLoopTime += buffTypeNum;
		}
		if (nowSeid == 157)
		{
			Avatar targetAvatar = getTargetAvatar(nowSeid, _avatar);
			int i3 = getSeidJson(nowSeid)["value2"].I;
			int num6 = 0;
			foreach (List<int> item6 in targetAvatar.fightTemp.RoundHasBuff)
			{
				num6 = (item6.Contains(i3) ? (num6 + 1) : 0);
			}
			if (num6 < getSeidJson(nowSeid)["value1"].I)
			{
				return false;
			}
		}
		if (nowSeid == 158 && getTargetAvatar(nowSeid, _avatar).crystal.getCardNum() != 0)
		{
			return false;
		}
		if (nowSeid == 159)
		{
			int buffRoundByID3 = getTargetAvatar(nowSeid, _avatar).buffmag.GetBuffRoundByID(getSeidJson(nowSeid)["value1"].I);
			if (!Tools.symbol(getSeidJson(nowSeid)["panduan"].str, buffRoundByID3, getSeidJson(nowSeid)["value2"].I))
			{
				return false;
			}
		}
		if (nowSeid == 160)
		{
			int i4 = getSeidJson(nowSeid)["value2"].I;
			int num7 = 0;
			foreach (int nowRoundDamageSkill in _avatar.fightTemp.NowRoundDamageSkills)
			{
				if (_skillJsonData.DataDict[nowRoundDamageSkill].AttackType.Contains(i4))
				{
					num7++;
				}
			}
			if (num7 <= getSeidJson(nowSeid)["value1"].I)
			{
				return false;
			}
		}
		if (nowSeid == 161)
		{
			int cardNum = _avatar.crystal.getCardNum();
			if (!Tools.symbol(getSeidJson(nowSeid)["panduan"].str, cardNum, (int)_avatar.NowCard))
			{
				return false;
			}
		}
		if (nowSeid == 162)
		{
			List<int> list3 = _skillJsonData.DataDict[flag[1]].seid;
			bool flag5 = false;
			foreach (int item7 in getSeidJson(nowSeid)["value1"].ToList())
			{
				if (list3.Contains(item7))
				{
					SkillCheck skillCheck = ((!_avatar.isPlayer()) ? RoundManager.instance.NpcSkillCheck : RoundManager.instance.PlayerSkillCheck);
					if (skillCheck != null && skillCheck.SkillId == flag[1] && skillCheck.HasPassSeid.Contains(item7))
					{
						flag5 = true;
						break;
					}
				}
			}
			if (!flag5)
			{
				return false;
			}
		}
		if (nowSeid == 163 && _avatar.wuDaoMag.GetAllWuDaoSkills().Count <= 0)
		{
			return false;
		}
		if (nowSeid == 164)
		{
			int num8 = flag[0];
			int i5 = getSeidJson(nowSeid)["value1"].I;
			if (num8 != i5)
			{
				return false;
			}
		}
		if (nowSeid == 165)
		{
			int i6 = getSeidJson(nowSeid)["value1"].I;
			if (_avatar.buffmag.HasBuff(i6))
			{
				return false;
			}
		}
		if ((nowSeid == 166 || nowSeid == 170) && !IsXiangShengXiangKeTongXi(getSeidJson(nowSeid)["value1"].I, flag[1], _avatar, buffInfo))
		{
			return false;
		}
		if (nowSeid == 167)
		{
			Avatar targetAvatar2 = getTargetAvatar(nowSeid, _avatar);
			int i7 = getSeidJson(nowSeid)["value1"].I;
			if (targetAvatar2.buffmag.getAllBuffByType(i7).Count <= 0)
			{
				return false;
			}
		}
		if (nowSeid == 168)
		{
			Avatar targetAvatar3 = getTargetAvatar(nowSeid, _avatar);
			int i8 = getSeidJson(nowSeid)["value1"].I;
			List<List<int>> allBuffByType = targetAvatar3.buffmag.getAllBuffByType(i8);
			if (!Tools.symbol(getSeidJson(nowSeid)["panduan"].str, allBuffByType.Count, getSeidJson(nowSeid)["value2"].I))
			{
				return false;
			}
		}
		if (nowSeid == 169)
		{
			int key6 = flag[1];
			_ = jsonData.instance.skillJsonData[key6.ToString()];
			if (!_skillJsonData.DataDict[key6].seid.Contains(getSeidJson(nowSeid)["value1"].I))
			{
				return false;
			}
		}
		if (nowSeid == 172)
		{
			int num9 = buffInfo[1];
			int i9 = getSeidJson(nowSeid)["value1"].I;
			if (num9 > i9)
			{
				return false;
			}
		}
		if (nowSeid == 173)
		{
			int num10 = flag[0];
			int i10 = getSeidJson(nowSeid)["value1"].I;
			if (num10 < i10)
			{
				return false;
			}
		}
		if (nowSeid == 174 && _avatar.dunSu < _avatar.OtherAvatar.dunSu)
		{
			return false;
		}
		if (nowSeid == 175 && _avatar.fightTemp.RoundReceiveDamage[0] < getSeidJson(nowSeid)["value1"].I)
		{
			return false;
		}
		if (nowSeid == 176)
		{
			Avatar targetAvatar4 = getTargetAvatar(nowSeid, _avatar);
			int i11 = getSeidJson(nowSeid)["value1"].I;
			if (!targetAvatar4.BuffSeidFlag.ContainsKey(176))
			{
				return false;
			}
			if (!targetAvatar4.BuffSeidFlag[176].ContainsKey(i11))
			{
				return false;
			}
		}
		if (nowSeid == 177)
		{
			int key7 = flag[1];
			List<int> attackType = _skillJsonData.DataDict[key7].AttackType;
			for (int j = 0; j < attackType.Count; j++)
			{
				if (attackType[j] == getSeidJson(nowSeid)["value1"].I)
				{
					BuffSeidFlagAddNum(nowSeid, 1, _avatar);
					break;
				}
			}
			if (!_avatar.BuffSeidFlag.ContainsKey(nowSeid))
			{
				return false;
			}
			int key8 = buffInfo[2];
			if (!_avatar.BuffSeidFlag[nowSeid].ContainsKey(key8))
			{
				return false;
			}
			int i12 = getSeidJson(nowSeid)["value2"].I;
			if (_avatar.BuffSeidFlag[nowSeid][key8] < i12)
			{
				return false;
			}
			_avatar.BuffSeidFlag[nowSeid][key8] = 0;
		}
		if (nowSeid == 178 && _avatar.shengShi < _avatar.OtherAvatar.shengShi)
		{
			return false;
		}
		if (nowSeid == 180)
		{
			int i13 = getSeidJson(nowSeid)["value1"].I;
			if (!Tools.symbol(getSeidJson(nowSeid)["panduan"].str, _avatar.cardMag.getCardNum(), i13))
			{
				return false;
			}
		}
		if (nowSeid == 181 && Tools.instance.monstarMag.FightType != StartFight.FightEnumType.ZhuJi)
		{
			return false;
		}
		if (nowSeid == 182 && Tools.instance.monstarMag.FightType == StartFight.FightEnumType.XinShouYinDao)
		{
			return false;
		}
		if (nowSeid == 189 && _avatar.shengShi > 0)
		{
			return false;
		}
		if (nowSeid == 196)
		{
			_ = getSeidJson(nowSeid)["value1"].I;
			if (!Tools.symbol(getSeidJson(nowSeid)["panduan"].str, _avatar.fightTemp.NowRoundUsedSkills.Count, getSeidJson(nowSeid)["value1"].I))
			{
				return false;
			}
		}
		if (nowSeid == 197)
		{
			int buffSum = getTargetAvatar(nowSeid, _avatar).buffmag.GetBuffSum(getSeidJson(nowSeid)["value1"].I);
			if (buffLoopData != null)
			{
				buffLoopData.TargetLoopTime += buffSum;
			}
		}
		if (nowSeid == 206)
		{
			int nowValue = _avatar.jieyin.GetNowValue(getSeidJson(nowSeid)["value1"].I);
			if (!Tools.symbol(getSeidJson(nowSeid)["panduan"].str, nowValue, getSeidJson(nowSeid)["value2"].I))
			{
				return false;
			}
		}
		if (nowSeid == 213)
		{
			int huaYing = _avatar.jieyin.HuaYing;
			if (!Tools.symbol(getSeidJson(nowSeid)["panduan"].str, huaYing, getSeidJson(nowSeid)["value1"].I))
			{
				return false;
			}
		}
		if (nowSeid == 214)
		{
			int statr = flag[0];
			if (!Tools.symbol(getSeidJson(nowSeid)["panduan"].str, statr, getSeidJson(nowSeid)["value1"].I))
			{
				return false;
			}
		}
		if (nowSeid == 216)
		{
			int key9 = flag[1];
			int i14 = getSeidJson(nowSeid)["value1"].I;
			if (!_skillJsonData.DataDict[key9].AttackType.Contains(i14))
			{
				return false;
			}
		}
		return true;
	}

	public bool IsXiangShengXiangKeTongXi(int type, int skillID, Avatar avatar, List<int> buffInfo)
	{
		GUIPackage.Skill skill = avatar.skill.Find((GUIPackage.Skill aa) => aa.skill_ID == skillID);
		if (skill == null || buffInfo == null)
		{
			return false;
		}
		if (skill.nowSkillIsChuFa.ContainsKey(buffInfo[2]) && skill.nowSkillIsChuFa[buffInfo[2]])
		{
			return false;
		}
		switch (type)
		{
		case 1:
		{
			Dictionary<int, int> xiangSheng = Tools.GetXiangSheng();
			int num3 = 0;
			if (skill.nowSkillUseCard.Count <= 1)
			{
				return false;
			}
			foreach (KeyValuePair<int, int> item in skill.nowSkillUseCard)
			{
				if (skill.nowSkillUseCard.ContainsKey(xiangSheng[item.Key]))
				{
					num3++;
				}
			}
			if (num3 >= skill.nowSkillUseCard.Count - 1)
			{
				skill.nowSkillIsChuFa[buffInfo[2]] = true;
				return true;
			}
			break;
		}
		case 2:
		{
			Dictionary<int, int> xiangKe = Tools.GetXiangKe();
			int num2 = 0;
			if (skill.nowSkillUseCard.Count <= 1)
			{
				return false;
			}
			foreach (KeyValuePair<int, int> item2 in skill.nowSkillUseCard)
			{
				if (skill.nowSkillUseCard.ContainsKey(xiangKe[item2.Key]))
				{
					num2++;
				}
			}
			if (num2 >= skill.nowSkillUseCard.Count - 1)
			{
				skill.nowSkillIsChuFa[buffInfo[2]] = true;
				return true;
			}
			break;
		}
		case 3:
		{
			int num = 0;
			foreach (KeyValuePair<int, int> item3 in skill.nowSkillUseCard)
			{
				num += item3.Value;
			}
			if (num <= 1)
			{
				return false;
			}
			if (skill.nowSkillUseCard.Count == 1)
			{
				skill.nowSkillIsChuFa[buffInfo[2]] = true;
				return true;
			}
			break;
		}
		}
		return false;
	}

	public void BuffSeidFlagAddNum(int seid, int addNum, Avatar avatar)
	{
		if (!avatar.BuffSeidFlag.ContainsKey(seid))
		{
			avatar.BuffSeidFlag.Add(seid, new Dictionary<int, int>());
		}
		if (!avatar.BuffSeidFlag[seid].ContainsKey(buffID))
		{
			avatar.BuffSeidFlag[seid].Add(buffID, 0);
		}
		avatar.BuffSeidFlag[seid][buffID] += addNum;
	}

	public void ReloadSelf(int seid, Avatar avatar, List<int> buffInfo, int Type)
	{
		List<int> list = new List<int>();
		list.Add(0);
		list.Add(Type);
		string text = "ListRealizeSeid" + seid;
		InitMethod(text);
		if (methodDict[text] != null)
		{
			methodDict[text].Invoke(this, new object[4] { seid, avatar, buffInfo, list });
		}
	}

	public void SeidAddBuff(int seid, Avatar avatar, List<int> buffInfo)
	{
		avatar.BuffSeidFlag[seid][buffID] -= (int)getSeidJson(seid)["value1"].n;
		avatar.spell.addDBuff((int)getSeidJson(seid)["value2"].n, (int)getSeidJson(seid)["value3"].n * buffInfo[1]);
	}

	public void SeidAddCard(Avatar avatar, List<int> flag)
	{
		if (flag.Count >= 3 && flag[2] == -123)
		{
			flag[0]++;
		}
		else
		{
			RoundManager.instance.DrawCard(avatar);
		}
	}

	public void SeidAddCard(Avatar avatar, List<int> flag, int cardType)
	{
		if (flag.Count >= 3 && flag[2] == 1)
		{
			flag[0]++;
		}
		else
		{
			RoundManager.instance.DrawCard(avatar, cardType);
		}
	}

	public bool RandomX(int percent)
	{
		if (jsonData.instance.getRandom() % 100 > percent)
		{
			return true;
		}
		return false;
	}

	public void ListRealizeSeid1(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		if (avatar.buffmag.HasBuffSeid(58))
		{
			List<List<int>> buffBySeid = avatar.buffmag.getBuffBySeid(58);
			avatar.spell.onBuffTick(avatar.buffmag.getBuffIndex(buffBySeid[0]));
		}
		avatar.recvDamage(avatar, avatar, 10006, (int)getSeidJson(seid)["value1"].n * buffInfo[1]);
	}

	public void ListRealizeSeid2(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int num = getSeidJson(seid)["value1"].I * buffInfo[1];
		for (int i = 0; i < num; i++)
		{
			SeidAddCard(avatar, flag);
		}
	}

	public void ListRealizeSeid3(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int num = flag[0] + (int)getSeidJson(seid)["value1"].n * buffInfo[1];
		int value = 0;
		if (flag[0] > 0)
		{
			value = Mathf.Max(0, num);
		}
		else if (flag[0] < 0)
		{
			value = Mathf.Min(0, num);
		}
		flag[0] = value;
	}

	public void ListRealizeSeid4(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		int num = 0;
		if (flag[0] > buffInfo[1])
		{
			num = buffInfo[1];
		}
		else if (flag[0] <= buffInfo[1])
		{
			num = flag[0];
		}
		if (!((Object)(object)RoundManager.instance != (Object)null) || !RoundManager.instance.IsVirtual)
		{
			((GameObject)avatar.renderObj).GetComponentInChildren<AvatarShowHpDamage>().SpecialShow(num, 1);
		}
		flag[0] -= num;
		buffInfo[1] -= num;
		if (buffInfo[2] == 5 && buffInfo[1] <= 0)
		{
			avatar.spell.onBuffTickByType(43, flag);
		}
	}

	public void ListRealizeSeid5(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		for (int i = 0; i < getSeidJson(seid)["value1"].list.Count; i++)
		{
			int i2 = getSeidJson(seid)["value1"][i].I;
			int num = getSeidJson(seid)["value2"][i].I * buffInfo[1];
			avatar.spell.addBuff(i2, num);
		}
	}

	public void ListRealizeSeid6(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
	}

	public void ListRealizeSeid7(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
	}

	public void ListRealizeSeid8(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
	}

	public void ListRealizeSeid9(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int num = getSeidJson(seid)["value2"].I * buffInfo[1];
		foreach (JSONObject item in getSeidJson(seid)["value1"].list)
		{
			if (Tools.instance.getSkillIDByKey(flag[1]) == item.I)
			{
				flag[0] += num;
				break;
			}
		}
	}

	public void ListRealizeSeid10(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int num = flag[0];
		flag[0] += (int)getSeidJson(seid)["value1"].n * buffInfo[1];
		if (num > 0 && flag[0] < 0)
		{
			flag[0] = 0;
		}
		if (num < 0 && flag[0] > 0)
		{
			flag[0] = 0;
		}
		if (num == 0)
		{
			flag[0] = 0;
		}
	}

	public void ListRealizeSeid11(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		avatar.recvDamage(avatar, avatar.OtherAvatar, 10006, (int)getSeidJson(seid)["value1"].n * buffInfo[1]);
	}

	public void ListRealizeSeid12(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		flag[0] *= 2;
	}

	public void ListRealizeSeid13(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		flag[1] = 1;
		avatar.state = 5;
	}

	public void ListRealizeSeid14(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		if (flag[0] < 0)
		{
			flag[0] = (int)Math.Ceiling(Convert.ToDouble(flag[0] / 2));
		}
	}

	public void ListRealizeSeid15(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int i = getSeidJson(seid)["value1"].I;
		int i2 = getSeidJson(seid)["value2"].I;
		bool flag2 = false;
		foreach (int item in _skillJsonData.DataDict[flag[1]].AttackType)
		{
			if (i == item)
			{
				flag2 = true;
				break;
			}
		}
		if (flag2)
		{
			flag[0] = (int)((float)flag[0] - (float)flag[0] * ((float)i2 / 100f));
		}
	}

	public void ListRealizeSeid16(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		avatar.recvDamage(avatar, avatar, 10006, avatar.HP - avatar.HP_Max);
	}

	public void ListRealizeSeid17(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int i = getSeidJson(seid)["value1"].I;
		int num = getSeidJson(seid)["value2"].I * buffInfo[1];
		for (int j = 0; j < num; j++)
		{
			avatar.OtherAvatar.spell.addDBuff(i);
		}
	}

	public void ListRealizeSeid18(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		if (!avatar.OtherAvatar.buffmag.HasBuffSeid(107) && !RoundManager.instance.IsVirtual && flag[0] > 0 && jsonData.instance.getRandom() % 100 <= (int)getSeidJson(seid)["value1"].n)
		{
			((GameObject)avatar.renderObj).GetComponentInChildren<AvatarShowHpDamage>().show("闪避");
			flag[0] = 0;
			avatar.spell.onBuffTickByType(35, new List<int> { 0 });
		}
	}

	public void ListRealizeSeid19(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
	}

	public void ListRealizeSeid20(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
	}

	public void ListRealizeSeid21(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		flag[0] -= (int)getSeidJson(seid)["value1"].n * buffInfo[1];
	}

	public void ListRealizeSeid22(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		if (!avatar.buffmag.HasBuffSeid(23))
		{
			avatar.fightTemp.tempNowCard += (int)getSeidJson(seid)["value1"].n;
		}
	}

	public void onDetachRealizeSeid22(int seid, Avatar avatar, List<int> buffInfo)
	{
		if (!avatar.buffmag.HasBuffSeid(23))
		{
			avatar.fightTemp.tempNowCard -= (int)getSeidJson(seid)["value1"].n * buffInfo[1];
		}
	}

	public void ListRealizeSeid23(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		if (!avatar.BuffSeidFlag.ContainsKey(seid))
		{
			avatar.BuffSeidFlag.Add(seid, new Dictionary<int, int>());
		}
		if (!avatar.BuffSeidFlag[seid].ContainsKey(buffID))
		{
			avatar.BuffSeidFlag[seid].Add(buffID, 0);
		}
		avatar.BuffSeidFlag[seid][buffID] = 0;
	}

	public void onDetachRealizeSeid23(int seid, Avatar avatar, List<int> buffInfo)
	{
		avatar.BuffSeidFlag.Remove(seid);
	}

	public void ListRealizeSeid24(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Expected O, but got Unknown
		int skillID = Tools.instance.getSkillKeyByID((int)getSeidJson(seid)["value1"].n, avatar);
		GUIPackage.Skill skill = new GUIPackage.Skill(skillID, 0, 10);
		List<int> _damage = new List<int>();
		Tools.AddQueue((UnityAction)delegate
		{
			RoundManager.instance.NowUseLingQiType = UseLingQiType.释放技能后消耗;
			if (jsonData.instance.skillJsonData[string.Concat(skillID)]["script"].str == "SkillAttack")
			{
				_damage = skill.PutingSkill(avatar, avatar.OtherAvatar);
			}
			else if (jsonData.instance.skillJsonData[string.Concat(skillID)]["script"].str == "SkillSelf")
			{
				_damage = skill.PutingSkill(avatar, avatar);
			}
			if (avatar.UsedSkills != null)
			{
				avatar.UsedSkills.Add(skillID);
			}
			if (!this.seid.Contains(129))
			{
				avatar.spell.onBuffTickByType(8, _damage);
			}
			RoundManager.instance.NowUseLingQiType = UseLingQiType.None;
			YSFuncList.Ints.Continue();
		});
	}

	public void ListRealizeSeid25(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		avatar.recvDamage(avatar, avatar.OtherAvatar, 10001 + (int)getSeidJson(seid)["value2"].n, (int)getSeidJson(seid)["value1"].n * buffInfo[1]);
	}

	public void ListRealizeSeid26(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int i = getSeidJson(seid)["value1"].I;
		int i2 = getSeidJson(seid)["value2"].I;
		int num = i * buffInfo[1];
		for (int j = 0; j < num; j++)
		{
			RoundManager.instance.DrawCard(avatar, i2);
		}
	}

	public void ListRealizeSeid27(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int i = getSeidJson(seid)["value1"].I;
		bool flag2 = false;
		foreach (int item in _skillJsonData.DataDict[flag[1]].AttackType)
		{
			if (i == item)
			{
				flag2 = true;
				break;
			}
		}
		if (flag2)
		{
			int i2 = getSeidJson(seid)["value2"].I;
			flag[0] += i2 * buffInfo[1];
		}
	}

	public void ListRealizeSeid28(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int i = getSeidJson(seid)["value1"].I;
		int i2 = getSeidJson(seid)["value2"].I;
		int num = i * buffInfo[1];
		for (int j = 0; j < num; j++)
		{
			foreach (List<int> item in avatar.OtherAvatar.buffmag.getBuffByID(i2))
			{
				avatar.OtherAvatar.spell.onBuffTick(avatar.OtherAvatar.bufflist.IndexOf(item));
			}
		}
	}

	public void ListRealizeSeid29(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int num = flag[0];
		if (avatar.HP_Max < avatar.HP - num)
		{
			avatar.recvDamage(avatar, avatar.OtherAvatar, 10006, avatar.HP - num - avatar.HP_Max);
		}
	}

	public void ListRealizeSeid31(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int num = flag[0];
		if (avatar.HP - num > 0)
		{
			return;
		}
		flag[0] = 0;
		avatar.HP = num + 1;
		int num2 = 0;
		foreach (JSONObject item in getSeidJson(seid)["value1"].list)
		{
			num2 += avatar.buffmag.GetBuffSum(item.I);
		}
		num2 *= getSeidJson(seid)["value2"].I;
		avatar.recvDamage(avatar, avatar, 10006, -num2);
	}

	public void ListRealizeSeid32(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		if (flag.Count >= 1)
		{
			int addNum = flag[0];
			BuffSeidFlagAddNum(seid, addNum, avatar);
			int num = avatar.BuffSeidFlag[seid][buffID] / getSeidJson(seid)["value1"].I;
			if (num > 0)
			{
				avatar.BuffSeidFlag[seid][buffID] -= (int)getSeidJson(seid)["value1"].n * num;
				avatar.spell.addBuff(getSeidJson(seid)["value2"].I, num * getSeidJson(seid)["value3"].I * buffInfo[1]);
			}
		}
	}

	public void ListRealizeSeid33(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int addNum = flag[0];
		BuffSeidFlagAddNum(seid, addNum, avatar);
		if (avatar.BuffSeidFlag[seid][buffID] >= (int)getSeidJson(seid)["value1"].n)
		{
			SeidAddBuff(seid, avatar, buffInfo);
			if (avatar.BuffSeidFlag[seid][buffID] >= (int)getSeidJson(seid)["value1"].n)
			{
				ReloadSelf(seid, avatar, buffInfo, 0);
			}
		}
	}

	public void ListRealizeSeid34(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int addNum = flag[0];
		int num = flag[1];
		if (num != (int)getSeidJson(seid)["value4"].n)
		{
			return;
		}
		BuffSeidFlagAddNum(seid, addNum, avatar);
		if (avatar.BuffSeidFlag[seid][buffID] >= (int)getSeidJson(seid)["value1"].n)
		{
			SeidAddBuff(seid, avatar, buffInfo);
			if (avatar.BuffSeidFlag[seid][buffID] >= (int)getSeidJson(seid)["value1"].n)
			{
				ReloadSelf(seid, avatar, buffInfo, num);
			}
		}
	}

	public void ListRealizeSeid35(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int addNum = flag[0];
		int num = flag[1];
		if (num != (int)getSeidJson(seid)["value4"].n)
		{
			return;
		}
		BuffSeidFlagAddNum(seid, addNum, avatar);
		if (avatar.BuffSeidFlag[seid][buffID] >= (int)getSeidJson(seid)["value1"].n)
		{
			SeidAddBuff(seid, avatar, buffInfo);
			if (avatar.BuffSeidFlag[seid][buffID] >= (int)getSeidJson(seid)["value1"].n)
			{
				ReloadSelf(seid, avatar, buffInfo, num);
			}
		}
	}

	public void ListRealizeSeid36(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int num = RoundManager.instance.getListSum(avatar.crystal) / (int)getSeidJson(seid)["value1"].n;
		if (num > 0)
		{
			avatar.spell.addBuff(getSeidJson(seid)["value2"].I, num * getSeidJson(seid)["value3"].I * buffInfo[1]);
		}
	}

	public void ListRealizeSeid37(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int i = getSeidJson(seid)["value1"].I;
		int i2 = getSeidJson(seid)["value2"].I;
		int i3 = getSeidJson(seid)["value3"].I;
		int i4 = getSeidJson(seid)["value4"].I;
		int num = avatar.crystal[i4] / i;
		if (num > 0)
		{
			int num2 = i3 * buffInfo[1];
			avatar.spell.addBuff(i2, num * num2);
		}
	}

	public void ListRealizeSeid38(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int addNum = flag[0];
		BuffSeidFlagAddNum(seid, addNum, avatar);
		if (avatar.BuffSeidFlag[seid][buffID] >= (int)getSeidJson(seid)["value1"].n)
		{
			SeidAddBuff(seid, avatar, buffInfo);
			if (avatar.BuffSeidFlag[seid][buffID] >= (int)getSeidJson(seid)["value1"].n)
			{
				ReloadSelf(seid, avatar, buffInfo, 0);
			}
		}
	}

	public void ListRealizeSeid39(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int addNum = flag[0];
		if (flag[1] == (int)getSeidJson(seid)["value4"].n)
		{
			BuffSeidFlagAddNum(seid, addNum, avatar);
			int num = avatar.BuffSeidFlag[seid][buffID] / getSeidJson(seid)["value1"].I;
			if (num > 0)
			{
				avatar.BuffSeidFlag[seid][buffID] -= (int)getSeidJson(seid)["value1"].n * num;
				avatar.spell.UseSkillLateAddBuff(getSeidJson(seid)["value2"].I, num * getSeidJson(seid)["value3"].I * buffInfo[1]);
			}
		}
	}

	public void ListRealizeSeid40(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		BuffSeidFlagAddNum(seid, 1, avatar);
		if (avatar.BuffSeidFlag[seid][buffID] >= (int)getSeidJson(seid)["value1"].n)
		{
			SeidAddBuff(seid, avatar, buffInfo);
			if (avatar.BuffSeidFlag[seid][buffID] >= (int)getSeidJson(seid)["value1"].n)
			{
				ReloadSelf(seid, avatar, buffInfo, 0);
			}
		}
	}

	public void ListRealizeSeid41(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int skillIDByKey = Tools.instance.getSkillIDByKey(flag[1]);
		foreach (JSONObject item in getSeidJson(seid)["value4"].list)
		{
			if (skillIDByKey != item.I)
			{
				continue;
			}
			BuffSeidFlagAddNum(seid, 1, avatar);
			if (avatar.BuffSeidFlag[seid][buffID] >= (int)getSeidJson(seid)["value1"].n)
			{
				SeidAddBuff(seid, avatar, buffInfo);
				if (avatar.BuffSeidFlag[seid][buffID] >= (int)getSeidJson(seid)["value1"].n)
				{
					ReloadSelf(seid, avatar, buffInfo, 0);
				}
			}
		}
	}

	public void ListRealizeSeid42(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int num = (int)getSeidJson(seid)["value4"].n;
		bool flag2 = false;
		foreach (JSONObject item in jsonData.instance.skillJsonData[string.Concat(flag[1])]["AttackType"].list)
		{
			if (num == (int)item.n)
			{
				flag2 = true;
			}
		}
		if (!flag2)
		{
			return;
		}
		BuffSeidFlagAddNum(seid, 1, avatar);
		if (avatar.BuffSeidFlag[seid][buffID] >= (int)getSeidJson(seid)["value1"].n)
		{
			SeidAddBuff(seid, avatar, buffInfo);
			if (avatar.BuffSeidFlag[seid][buffID] >= (int)getSeidJson(seid)["value1"].n)
			{
				ReloadSelf(seid, avatar, buffInfo, 0);
			}
		}
	}

	public void ListRealizeSeid43(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		BuffSeidFlagAddNum(seid, 1, avatar);
		if (avatar.BuffSeidFlag[seid][buffID] >= (int)getSeidJson(seid)["value1"].n)
		{
			SeidAddBuff(seid, avatar, buffInfo);
			if (avatar.BuffSeidFlag[seid][buffID] >= (int)getSeidJson(seid)["value1"].n)
			{
				ReloadSelf(seid, avatar, buffInfo, 0);
			}
		}
	}

	public void ListRealizeSeid44(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int num = flag[1];
		bool flag2 = false;
		foreach (JSONObject item in jsonData.instance.skillJsonData[string.Concat(num)]["AttackType"].list)
		{
			if ((int)getSeidJson(seid)["value4"].n == (int)item.n)
			{
				flag2 = true;
			}
		}
		if (!flag2)
		{
			return;
		}
		BuffSeidFlagAddNum(seid, 1, avatar);
		if (avatar.BuffSeidFlag[seid][buffID] >= (int)getSeidJson(seid)["value1"].n)
		{
			SeidAddBuff(seid, avatar, buffInfo);
			if (avatar.BuffSeidFlag[seid][buffID] >= (int)getSeidJson(seid)["value1"].n)
			{
				ReloadSelf(seid, avatar, buffInfo, 0);
			}
		}
	}

	public void ListRealizeSeid46(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		if (!avatar.OtherAvatar.buffmag.HasBuffSeid(107))
		{
			if (buffInfo[1] > 0)
			{
				buffInfo[1]--;
				flag[0] = 0;
				((GameObject)avatar.renderObj).GetComponentInChildren<AvatarShowHpDamage>().show("闪避");
			}
			avatar.spell.onBuffTickByType(35, new List<int> { 0 });
		}
	}

	public void ListRealizeSeid47(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		if (RoundManager.instance.getListSum(avatar.crystal) <= 0)
		{
			for (int i = 0; i < (int)(getSeidJson(seid)["value1"].n * (float)buffInfo[1]); i++)
			{
				SeidAddCard(avatar, flag);
			}
		}
	}

	public void ListRealizeSeid48(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int i = getSeidJson(seid)["value1"].I;
		if (buffInfo[1] >= i)
		{
			int num = buffInfo[1] / i;
			int i2 = getSeidJson(seid)["value2"].I;
			int i3 = getSeidJson(seid)["value3"].I;
			avatar.spell.addBuff(i2, i3 * num);
			buffInfo[1] = 0;
		}
	}

	public void ListRealizeSeid50(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int key = flag[1];
		bool flag2 = false;
		List<int> attackType = _skillJsonData.DataDict[key].AttackType;
		int i = getSeidJson(seid)["value1"].I;
		foreach (int item in attackType)
		{
			if (i == item)
			{
				flag2 = true;
				break;
			}
		}
		if (flag2)
		{
			int i2 = getSeidJson(seid)["value2"].I;
			int num = buffInfo[1];
			flag[0] += i2 * num;
		}
	}

	public void ListRealizeSeid51(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		for (int i = 0; i < (int)getSeidJson(seid)["value1"].n; i++)
		{
			avatar.recvDamage(avatar, avatar.OtherAvatar, 10006, (int)getSeidJson(seid)["value2"].n);
		}
	}

	public void ListRealizeSeid52(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		BuffSeidFlagAddNum(seid, 1, avatar);
		if (avatar.BuffSeidFlag[seid][buffID] >= (int)getSeidJson(seid)["value1"].n)
		{
			SeidAddBuff(seid, avatar, buffInfo);
			if (avatar.BuffSeidFlag[seid][buffID] >= (int)getSeidJson(seid)["value1"].n)
			{
				ReloadSelf(seid, avatar, buffInfo, 0);
			}
		}
	}

	public void ListRealizeSeid53(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		List<List<int>> buffByID = avatar.OtherAvatar.buffmag.getBuffByID(getSeidJson(seid)["value2"].I);
		if (buffByID.Count >= 1 && buffByID[0][1] >= getSeidJson(seid)["value1"].I)
		{
			int num = buffByID[0][1] / getSeidJson(seid)["value1"].I;
			avatar.spell.addBuff(getSeidJson(seid)["value4"].I, num * getSeidJson(seid)["value3"].I);
		}
	}

	public void ListRealizeSeid54(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		List<List<int>> buffByID = avatar.buffmag.getBuffByID(getSeidJson(seid)["value2"].I);
		if (buffByID.Count >= 1 && buffByID[0][1] >= getSeidJson(seid)["value1"].I)
		{
			int num = buffByID[0][1] / getSeidJson(seid)["value1"].I;
			avatar.spell.addBuff(getSeidJson(seid)["value4"].I, num * getSeidJson(seid)["value3"].I);
		}
	}

	public void ListRealizeSeid55(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		avatar.fightTemp.tempShenShi[buffID] = (int)getSeidJson(seid)["value1"].n * buffInfo[1];
	}

	public void onDetachRealizeSeid55(int seid, Avatar avatar, List<int> buffInfo)
	{
		avatar.fightTemp.tempShenShi[buffID] = 0;
	}

	public void ListRealizeSeid56(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		avatar.fightTemp.TempHP_Max[buffID] = (int)getSeidJson(seid)["value1"].n * buffInfo[1];
	}

	public void onDetachRealizeSeid56(int seid, Avatar avatar, List<int> buffInfo)
	{
		avatar.fightTemp.TempHP_Max[buffID] = 0;
	}

	public void ListRealizeSeid57(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int num = flag[1];
		if (jsonData.instance.skillJsonData[num.ToString()]["AttackType"].list.Find((JSONObject aa) => (int)aa.n == (int)getSeidJson(seid)["value1"].n) != null)
		{
			flag[0] += (int)((float)flag[0] * (getSeidJson(seid)["value2"].n * (float)buffInfo[1] / 100f));
		}
	}

	public void ListRealizeSeid58(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
	}

	public void ListRealizeSeid59(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
	}

	public void ListRealizeSeid60(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		flag[0] = 0;
		RoundManager.instance.creatAvatar(11, 50, 100, new Vector3(5f, -1.7f, -1f), new Vector3(0f, 0f, -90f));
		Avatar avatar2 = (Avatar)KBEngineApp.app.entities[11];
		avatar2.equipSkillList = new List<SkillItem>();
		avatar2.equipStaticSkillList = new List<SkillItem>();
		avatar2.LingGeng = new List<int>();
		RoundManager.instance.initMonstar((int)getSeidJson(seid)["value1"].n);
		KBEngineApp.app.entity_id = 10;
		Avatar avatar3 = (Avatar)KBEngineApp.app.entities[10];
		avatar3.OtherAvatar = avatar2;
		avatar2.OtherAvatar = avatar3;
		World.instance.onLeaveWorld(avatar);
		avatar2.skill = new List<GUIPackage.Skill>();
		RoundManager.instance.initAvatarInfo(avatar2);
		GameObject val = GameObject.Find("Canvas_target");
		RoundManager.instance.initUI_Target(val.GetComponent<UI_Target>(), avatar2);
		avatar = avatar2;
		Event.fireOut("UpdataBuff");
		RoundManager.instance.endRound(avatar);
	}

	public void ListRealizeSeid61(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		avatar.fightTemp.tempDunSu[buffID] = (int)getSeidJson(seid)["value1"].n * buffInfo[1];
	}

	public void onDetachRealizeSeid61(int seid, Avatar avatar, List<int> buffInfo)
	{
		avatar.fightTemp.tempDunSu[buffID] = 0;
	}

	public void ListRealizeSeid62(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
	}

	public void ListRealizeSeid63(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		RoundManager.instance.removeCard(avatar, getSeidJson(seid)["value1"].I * buffInfo[1]);
	}

	public void ListRealizeSeid64(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int i = getSeidJson(seid)["value1"].I;
		int i2 = getSeidJson(seid)["value2"].I;
		int num = buffInfo[1];
		if (!avatar.SkillSeidFlag.ContainsKey(13))
		{
			avatar.SkillSeidFlag.Add(13, new Dictionary<int, int>());
			avatar.SkillSeidFlag[13].Add(i, 0);
		}
		if (!avatar.SkillSeidFlag[13].ContainsKey(i))
		{
			avatar.SkillSeidFlag[13][i] = 0;
		}
		avatar.SkillSeidFlag[13][i] += i2 * num;
	}

	public void ListRealizeSeid65(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
	}

	public void ListRealizeSeid66(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		List<List<int>> buffByID = avatar.OtherAvatar.buffmag.getBuffByID((int)getSeidJson(seid)["value1"].n);
		if (buffByID.Count > 0)
		{
			avatar.recvDamage(avatar, avatar.OtherAvatar, 10006, (int)getSeidJson(seid)["value2"].n * buffByID[0][1] * buffInfo[1]);
		}
	}

	public void ListRealizeSeid67(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int i = getSeidJson(seid)["value1"].I;
		int i2 = getSeidJson(seid)["value2"].I;
		int i3 = getSeidJson(seid)["value3"].I;
		int i4 = getSeidJson(seid)["value4"].I;
		List<List<int>> buffByID = avatar.buffmag.getBuffByID(i2);
		if (buffByID.Count > 0)
		{
			int num = buffByID[0][1] / i;
			int num2 = i4 * buffInfo[1];
			for (int j = 0; j < num; j++)
			{
				avatar.spell.addBuff(i3, num2);
			}
		}
	}

	public void ListRealizeSeid68(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		if (!avatar.buffmag.HasBuffSeid(23))
		{
			avatar.fightTemp.tempNowCard -= (int)getSeidJson(seid)["value1"].n;
		}
	}

	public void onDetachRealizeSeid68(int seid, Avatar avatar, List<int> buffInfo)
	{
		if (!avatar.buffmag.HasBuffSeid(23))
		{
			avatar.fightTemp.tempNowCard += (int)getSeidJson(seid)["value1"].n * buffInfo[1];
		}
	}

	public void ListRealizeSeid69(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int num = flag[1];
		int num2 = 0;
		foreach (JSONObject item in jsonData.instance.skillJsonData[num.ToString()]["skill_Cast"].list)
		{
			num2 += (int)item.n;
		}
		foreach (JSONObject item2 in jsonData.instance.skillJsonData[num.ToString()]["skill_SameCastNum"].list)
		{
			num2 += (int)item2.n;
		}
		if (num2 >= (int)getSeidJson(seid)["value1"].n)
		{
			for (int i = 0; i < (int)getSeidJson(seid)["value2"].n * buffInfo[1]; i++)
			{
				RoundManager.instance.DrawCard(avatar);
			}
		}
	}

	public void ListRealizeSeid70(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		foreach (JSONObject item in getSeidJson(seid)["value1"].list)
		{
			if (item.n == (float)Tools.instance.MonstarID && flag[0] > 0)
			{
				flag[0] -= (int)getSeidJson(seid)["value2"].n;
			}
		}
	}

	public void ListRealizeSeid71(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		avatar.setHP(avatar.HP - (int)getSeidJson(seid)["value1"].n * buffInfo[1]);
	}

	public void ListRealizeSeid72(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int num = flag[1];
		int CheckskillID = (int)getSeidJson(seid)["value1"].n;
		if (flag[0] > 0 && jsonData.instance.skillJsonData[string.Concat(num)]["AttackType"].list.FindAll((JSONObject aa) => (int)aa.n == CheckskillID).Count > 0)
		{
			flag[0] = Mathf.Clamp(flag[0] - (int)getSeidJson(seid)["value2"].n * buffInfo[1], 0, 99999999);
		}
	}

	public void ListRealizeSeid73(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
	}

	public void ListRealizeSeid78(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		flag[3] = 1;
	}

	public void ListRealizeSeid79(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int cardNum = avatar.OtherAvatar.crystal.getCardNum();
		int num = (int)getSeidJson(seid)["value1"].n * buffInfo[1];
		int num2 = Mathf.Min(cardNum, num);
		int num3 = 0;
		int[] array = new int[6];
		foreach (card item in avatar.OtherAvatar.crystal._cardlist)
		{
			if (num3 < num2)
			{
				array[item.cardType]++;
			}
			num3++;
		}
		for (int i = 0; i < 6; i++)
		{
			if (array[i] > 0)
			{
				RoundManager.instance.DrawCardCreatSpritAndAddCrystal(avatar, i, array[i]);
			}
		}
		RoundManager.instance.removeCard(avatar.OtherAvatar, num2);
	}

	public void ListRealizeSeid80(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int num = flag[1];
		if (jsonData.instance.skillJsonData[num.ToString()]["AttackType"].list.Find((JSONObject cc) => (int)cc.n == (int)getSeidJson(seid)["value1"].n) == null)
		{
			return;
		}
		foreach (List<int> item in avatar.buffmag.getBuffByID(getSeidJson(seid)["value2"].I))
		{
			flag[0] += getSeidJson(seid)["value3"].I * item[1] * buffInfo[1];
		}
	}

	public void ListRealizeSeid81(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
	}

	public void ListRealizeSeid82(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		for (int i = 0; i < (int)getSeidJson(seid)["value1"].n * buffInfo[1]; i++)
		{
			foreach (List<int> item in avatar.buffmag.getBuffByID((int)getSeidJson(seid)["value2"].n))
			{
				avatar.spell.onBuffTick(avatar.bufflist.IndexOf(item));
			}
		}
	}

	public void ListRealizeSeid83(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
	}

	public void ListRealizeSeid84(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		if (flag[0] < (int)getSeidJson(seid)["value1"].n)
		{
			flag[0] = 0;
		}
	}

	public void ListRealizeSeid87(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int num = (int)getSeidJson(seid)["value2"].n * buffInfo[1];
		for (int i = 0; i < num; i++)
		{
			avatar.spell.addDBuff((int)getSeidJson(seid)["value1"].n);
		}
	}

	public void ListRealizeSeid88(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		for (int i = 0; i < (int)(getSeidJson(seid)["value2"].n * (float)buffInfo[1]); i++)
		{
			avatar.OtherAvatar.spell.addDBuff((int)getSeidJson(seid)["value1"].n);
		}
	}

	public void ListRealizeSeid89(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		if (!avatar.buffmag.HasBuff((int)getSeidJson(seid)["value2"].n))
		{
			return;
		}
		foreach (List<int> item in avatar.buffmag.getBuffByID((int)getSeidJson(seid)["value2"].n))
		{
			item[1] -= (int)getSeidJson(seid)["value1"].n * buffInfo[1];
		}
	}

	public void ListRealizeSeid90(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
	}

	public void ListRealizeSeid92(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		List<List<int>> buffByID = avatar.buffmag.getBuffByID((int)getSeidJson(seid)["value1"].n);
		if (buffByID.Count > 0)
		{
			buffByID[0][1] = (int)getSeidJson(seid)["value2"].n;
		}
		Event.fireOut("UpdataBuff");
	}

	public void ListRealizeSeid93(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int damage = -(int)((float)avatar.HP_Max * getSeidJson(seid)["value1"].n / 100f * (float)buffInfo[1]);
		avatar.recvDamage(avatar, avatar, 10006, damage);
	}

	public void ListRealizeSeid94(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int num = avatar.HP_Max - avatar.HP;
		int i = getSeidJson(seid)["value1"].I;
		int num2 = -(int)((float)(num * i) / 100f * (float)buffInfo[1]);
		avatar.recvDamage(avatar, avatar, 10006, num2);
		if (avatar.OtherAvatar.buffmag.HasBuffSeid(310) && i < 0)
		{
			avatar.OtherAvatar.recvDamage(avatar.OtherAvatar, avatar.OtherAvatar, 10006, -num2);
		}
	}

	public void ListRealizeSeid98(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int damage = (int)getSeidJson(seid)["value1"].n * buffInfo[1];
		avatar.recvDamage(avatar, avatar, 10001 + (int)getSeidJson(seid)["value2"].n, damage);
	}

	public void ListRealizeSeid100(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		flag[3] = 1;
	}

	public void ListRealizeSeid104(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int i = getSeidJson(seid)["value1"].I;
		int i2 = getSeidJson(seid)["value2"].I;
		for (int j = 0; j < i; j++)
		{
			card randomCard = avatar.OtherAvatar.cardMag.getRandomCard();
			if (randomCard != null)
			{
				int cardType = randomCard.cardType;
				card card2 = avatar.OtherAvatar.cardMag.ChengCard(randomCard.cardType, i2);
				if (avatar.OtherAvatar.isPlayer() && card2 != null)
				{
					Debug.Log((object)$"将{(LingQiType)cardType}污染为{(LingQiType)i2}");
					UIFightPanel.Inst.PlayerLingQiController.SlotList[cardType].LingQiCount--;
					UIFightPanel.Inst.PlayerLingQiController.SlotList[i2].LingQiCount++;
				}
			}
		}
	}

	public void ListRealizeSeid105(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		if (avatar.buffmag.HasBuffSeid(58))
		{
			List<List<int>> buffBySeid = avatar.buffmag.getBuffBySeid(58);
			avatar.spell.onBuffTick(avatar.buffmag.getBuffIndex(buffBySeid[0]));
		}
		int num = avatar.cardMag[(int)getSeidJson(seid)["value2"].n];
		avatar.recvDamage(avatar, avatar, 10006, (int)getSeidJson(seid)["value1"].n * num * buffInfo[1]);
	}

	public void onAttachRealizeSeid108(int seid, Avatar avatar, List<int> buffInfo)
	{
		int endIndex = (((Object)(object)JieDanManager.instence == (Object)null) ? 10 : 6);
		avatar.FightClearSkill(0, endIndex);
	}

	public void onAttachRealizeSeid109(int seid, Avatar avatar, List<int> buffInfo)
	{
		int endIndex = (((Object)(object)JieDanManager.instence == (Object)null) ? 10 : 6);
		foreach (JSONObject item in getSeidJson(seid)["value1"].list)
		{
			avatar.FightAddSkill((int)item.n, 0, endIndex);
		}
	}

	public void ListRealizeSeid111(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		foreach (JSONObject item in getSeidJson(seid)["value1"].list)
		{
			foreach (List<int> item2 in avatar.buffmag.getBuffByID((int)item.n))
			{
				avatar.spell.removeBuff(item2);
			}
		}
	}

	public void onDetachRealizeSeid112(int seid, Avatar avatar, List<int> buffInfo)
	{
		if (avatar.buffmag.HasBuffSeid(58))
		{
			List<List<int>> buffBySeid = avatar.buffmag.getBuffBySeid(58);
			avatar.spell.onBuffTick(avatar.buffmag.getBuffIndex(buffBySeid[0]));
		}
		avatar.recvDamage(avatar, avatar, 10006, (int)getSeidJson(seid)["value1"].n);
	}

	public void onDetachRealizeSeid114(int seid, Avatar avatar, List<int> buffInfo)
	{
		avatar.setHP(-1);
	}

	public void ListRealizeSeid115(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		flag[0] += flag[0] * (int)getSeidJson(seid)["value1"].n * buffInfo[1] / 100;
	}

	public void ListRealizeSeid117(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		avatar.spell.addDBuff(getSeidJson(seid)["value1"].I, getSeidJson(seid)["value2"].I * RoundManager.instance.NowSkillUsedLingQiSum * buffInfo[1]);
	}

	public void ListRealizeSeid123(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		((Component)UIFightPanel.Inst.FightSelectLingQi).gameObject.SetActive(true);
		UIFightPanel.Inst.FightSelectLingQi.SetSelectAction(delegate(LingQiType lq)
		{
			RoundManager.instance.DrawCard(avatar, (int)lq);
		});
	}

	public void ListRealizeSeid124(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
	}

	public void ListRealizeSeid125(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
	}

	public void ListRealizeSeid127(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		for (int i = 0; i < getSeidJson(seid)["value1"].list.Count; i++)
		{
			avatar.spell.addDBuff((int)getSeidJson(seid)["value1"][i].n, (int)getSeidJson(seid)["value2"][i].n);
		}
	}

	public void ListRealizeSeid128(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		List<JSONObject> list = getSeidJson(seid)["value2"].list;
		int i = getSeidJson(seid)["value1"].I;
		bool flag2 = true;
		foreach (JSONObject item in list)
		{
			if (!jsonData.instance.skillJsonData[string.Concat(flag[1])]["AttackType"].HasItem(item.I))
			{
				flag2 = false;
			}
		}
		BuffSeidFlagAddNum(seid, 0, avatar);
		if (flag2)
		{
			if (avatar.BuffSeidFlag[seid][buffID] >= i)
			{
				avatar.BuffSeidFlag[seid][buffID] -= (int)getSeidJson(seid)["value1"].n;
			}
			BuffSeidFlagAddNum(seid, 1, avatar);
		}
	}

	public void ListRealizeSeid130(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		List<JSONObject> list = getSeidJson(seid)["value2"].list;
		List<JSONObject> list2 = getSeidJson(seid)["value3"].list;
		List<int> flag2 = new List<int>();
		flag.ForEach(delegate(int aa)
		{
			flag2.Add(aa);
		});
		if (CanRealizeSeid(avatar, flag2, getSeidJson(seid)["value1"].I, null, buffInfo))
		{
			foreach (JSONObject item in list)
			{
				loopRealizeSeid(item.I, avatar, buffInfo, flag);
				if (!CanRealizeSeid(avatar, flag, item.I, null, buffInfo))
				{
					break;
				}
			}
			return;
		}
		foreach (JSONObject item2 in list2)
		{
			loopRealizeSeid(item2.I, avatar, buffInfo, flag);
			if (!CanRealizeSeid(avatar, flag, item2.I, null, buffInfo))
			{
				break;
			}
		}
	}

	public void ListRealizeSeid131(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		ListRealizeSeid130(seid, avatar, buffInfo, flag);
	}

	public void ListRealizeSeid132(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		ListRealizeSeid130(seid, avatar, buffInfo, flag);
	}

	public void ListRealizeSeid133(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		bool flag2 = false;
		foreach (JSONObject item in jsonData.instance.skillJsonData[string.Concat(flag[1])]["AttackType"].list)
		{
			if ((int)getSeidJson(seid)["value1"].n == (int)item.n)
			{
				flag2 = true;
			}
		}
		if (flag2)
		{
			int num = getTargetAvatar(seid, avatar).crystal[(int)getSeidJson(seid)["value3"].n];
			flag[0] -= num;
			flag[0] = Mathf.Max(0, flag[0]);
		}
	}

	public void ListRealizeSeid134(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		List<List<int>> allBuffByType = getTargetAvatar(seid, avatar).buffmag.getAllBuffByType((int)getSeidJson(seid)["value2"].n);
		if (allBuffByType.Count > 0)
		{
			int index = jsonData.GetRandom() % allBuffByType.Count;
			allBuffByType[index][1] -= (int)getSeidJson(seid)["value1"].n;
			if (allBuffByType[index][1] < 0)
			{
				allBuffByType[index][1] = 0;
			}
		}
	}

	public void ListRealizeSeid135(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int i = getSeidJson(seid)["value2"].I;
		List<JSONObject> list = new List<JSONObject>();
		foreach (KeyValuePair<string, JSONObject> buffJsonDatum in jsonData.instance.BuffJsonData)
		{
			if (buffJsonDatum.Value["bufftype"].I == i)
			{
				list.Add(buffJsonDatum.Value);
			}
		}
		JSONObject jSONObject = list[jsonData.GetRandom() % list.Count];
		avatar.spell.addDBuff(jSONObject["buffid"].I, getSeidJson(seid)["value1"].I * buffInfo[1]);
	}

	public void ListRealizeSeid137(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		if (flag[0] < 0)
		{
			flag[0] += (int)Math.Ceiling(Convert.ToDouble((float)flag[0] * getSeidJson(seid)["value1"].n));
		}
	}

	public void ListRealizeSeid138(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		Avatar targetAvatar = getTargetAvatar(seid, avatar);
		int i = getSeidJson(seid)["value1"].I;
		int i2 = getSeidJson(seid)["value2"].I;
		int num = buffInfo[1];
		List<List<int>> allBuffByType = targetAvatar.buffmag.GetAllBuffByType(i);
		if (allBuffByType.Count <= 0)
		{
			return;
		}
		foreach (List<int> item in allBuffByType)
		{
			int num2 = item[1] - i2 * num;
			num2 = Mathf.Max(0, num2);
			item[1] = num2;
		}
	}

	public void ListRealizeSeid139(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		flag[4] = getSeidJson(seid)["value2"].I;
		List<int> list = _skillJsonData.DataDict[flag[1]].seid;
		if (!list.Contains(139) || flag[4] < 2 || !((Object)(object)RoundManager.instance != (Object)null) || RoundManager.instance.CurSkill == null)
		{
			return;
		}
		GUIPackage.Skill curSkill = RoundManager.instance.CurSkill;
		List<int> list2 = new List<int>();
		bool flag2 = false;
		foreach (int item in list)
		{
			if (flag2)
			{
				list2.Add(item);
			}
			if (item == 139)
			{
				flag2 = true;
			}
		}
		if (list2.Count < 1)
		{
			return;
		}
		for (int i = 0; i < flag[4] - 1; i++)
		{
			foreach (int item2 in list2)
			{
				curSkill.realizeSeid(item2, flag, curSkill.attack, curSkill.target, curSkill.type);
				curSkill.realizeBuffEndSeid(item2, flag, curSkill.attack, curSkill.target, curSkill.type);
				curSkill.realizeFinalSeid(item2, flag, curSkill.attack, curSkill.target, curSkill.type);
			}
		}
	}

	public void ListRealizeSeid141(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		if (flag[0] > 0)
		{
			flag[0] += (int)Math.Ceiling(Convert.ToDouble((float)flag[0] * getSeidJson(seid)["value1"].n));
			flag[0] = Mathf.Max(0, flag[0]);
		}
	}

	public void ListRealizeSeid142(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		List<List<int>> buffByID = getTargetAvatar(seid, avatar).buffmag.getBuffByID((int)getSeidJson(seid)["value1"].n);
		if (buffByID.Count <= 0)
		{
			return;
		}
		foreach (List<int> item in buffByID)
		{
			item[1] += item[1] * (int)getSeidJson(seid)["value2"].n;
		}
	}

	public void ListRealizeSeid146(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int num = flag[0];
		avatar.recvDamage(avatar, avatar, 10006, -num);
	}

	public void ListRealizeSeid147(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		List<List<int>> buffByID = avatar.buffmag.getBuffByID((int)getSeidJson(seid)["value1"].n);
		if (buffByID.Count > 0)
		{
			avatar.recvDamage(avatar, avatar, 10006, -buffByID[0][1]);
		}
	}

	public void ListRealizeSeid148(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		bool flag2 = false;
		foreach (JSONObject item in jsonData.instance.skillJsonData[string.Concat(flag[1])]["AttackType"].list)
		{
			if ((int)getSeidJson(seid)["value1"].n == (int)item.n)
			{
				flag2 = true;
			}
		}
		if (flag2)
		{
			flag[0] = 0;
		}
	}

	public void ListRealizeSeid149(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int num = flag[0];
		for (int i = 0; i < getSeidJson(seid)["value1"].Count; i++)
		{
			avatar.spell.addDBuff(getSeidJson(seid)["value1"][i].I, buffInfo[1] * num);
		}
	}

	public void ListRealizeSeid171(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		_NeiShangLoopCount++;
		if (_NeiShangLoopCount > 20)
		{
			Debug.Log((object)"内伤循环达到20次，退出循环");
			_NeiShangLoopCount = 0;
			return;
		}
		int addNum = flag[0];
		BuffSeidFlagAddNum(seid, addNum, avatar);
		int num = avatar.BuffSeidFlag[seid][buffID];
		int i = getSeidJson(seid)["value1"].I;
		int i2 = getSeidJson(seid)["value2"].I;
		int num2 = num / i;
		if (num2 > 0)
		{
			avatar.BuffSeidFlag[seid][buffID] -= num2 * i;
			avatar.spell.addBuff(i2, num2);
			if (avatar.state != 1 && avatar.OtherAvatar.state != 1 && avatar.BuffSeidFlag[seid][buffID] >= i)
			{
				ReloadSelf(seid, avatar, buffInfo, 0);
			}
		}
	}

	public void ListRealizeSeid179(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int listSum = RoundManager.instance.getListSum(avatar.crystal);
		avatar.recvDamage(avatar, avatar, 10006, -(listSum * (int)getSeidJson(seid)["value1"].n));
	}

	public void ListRealizeSeid185(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		for (int i = 0; i < getSeidJson(seid)["value1"].list.Count; i++)
		{
			avatar.OtherAvatar.spell.addDBuff((int)getSeidJson(seid)["value1"][i].n, (int)getSeidJson(seid)["value2"][i].n);
		}
	}

	public void ListRealizeSeid186(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		avatar.spell.addBuff(getSeidJson(seid)["value1"].I, flag[0]);
	}

	public void ListRealizeSeid187(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int num = flag[0];
		int num2 = num * getSeidJson(seid)["value1"].I / 100;
		flag[0] = num - num2;
		avatar.OtherAvatar.setHP(avatar.OtherAvatar.HP - num2);
	}

	public void ListRealizeSeid190(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		JSONObject seidJson = getSeidJson(seid);
		List<int> list = seidJson["value1"].ToList();
		List<int> list2 = seidJson["value2"].ToList();
		for (int i = 0; i < list.Count; i++)
		{
			int num = GlobalValue.Get(list[i], "Buff.ListRealizeSeid190");
			if (num > 0)
			{
				avatar.spell.addBuff(list2[i], num);
			}
		}
	}

	public void ListRealizeSeid191(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		Avatar targetAvatar = getTargetAvatar(seid, avatar);
		int num = 0;
		num = ((!targetAvatar.isPlayer()) ? RoundManager.instance.NpcCurRoundDrawCardNum : RoundManager.instance.PlayerCurRoundDrawCardNum);
		avatar.recvDamage(avatar, avatar, 10006, num * buffInfo[1]);
	}

	public void ListRealizeSeid192(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int key = flag[1];
		if (_skillJsonData.DataDict[key].AttackType.Contains(getSeidJson(seid)["value2"].I) && flag[0] > 0)
		{
			flag[0] += (int)Math.Ceiling(Convert.ToDouble((float)(flag[0] * buffInfo[1]) * getSeidJson(seid)["value1"].n));
		}
	}

	public void ListRealizeSeid193(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
	}

	public void ListRealizeSeid194(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		if (avatar.buffmag.GetBuffSum(getSeidJson(seid)["value1"].I) > 0)
		{
			avatar.fightTemp.tempDunSu[buffID] -= getSeidJson(seid)["value2"].I;
		}
	}

	public void ListRealizeSeid195(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		if (flag.Count < 2)
		{
			return;
		}
		int addNum = flag[0];
		if (flag[1] == (int)getSeidJson(seid)["value2"].n)
		{
			BuffSeidFlagAddNum(seid, addNum, avatar);
			while (avatar.BuffSeidFlag[seid][buffID] >= (int)getSeidJson(seid)["value1"].n)
			{
				avatar.BuffSeidFlag[seid][buffID] -= (int)getSeidJson(seid)["value1"].n;
				avatar.spell.addDBuff((int)getSeidJson(seid)["value3"].n, (int)getSeidJson(seid)["value4"].n * buffInfo[1]);
			}
		}
	}

	public void ListRealizeSeid198(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int nowSkillUsedLingQiSum = RoundManager.instance.NowSkillUsedLingQiSum;
		if (flag[0] > 0)
		{
			flag[0] += buffInfo[1] * nowSkillUsedLingQiSum;
		}
	}

	public void ListRealizeSeid199(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		if (_skillJsonData.DataDict[flag[1]].AttackType.Contains(getSeidJson(seid)["value1"].I))
		{
			int nowSkillUsedLingQiSum = RoundManager.instance.NowSkillUsedLingQiSum;
			if (flag[0] > 0)
			{
				flag[0] += (int)Math.Ceiling(Convert.ToDouble((float)(flag[0] * buffInfo[1] * nowSkillUsedLingQiSum) * getSeidJson(seid)["value2"].n / 100f));
			}
		}
	}

	public void ListRealizeSeid200(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		avatar.jieyin.AddJinMai((int)getSeidJson(seid)["value1"].n * buffInfo[1]);
	}

	public void ListRealizeSeid201(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		avatar.jieyin.AddYiZhi((int)getSeidJson(seid)["value1"].n * buffInfo[1]);
	}

	public void ListRealizeSeid202(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		avatar.jieyin.AddHuaYing((int)getSeidJson(seid)["value1"].n * buffInfo[1]);
	}

	public void ListRealizeSeid203(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		avatar.jieyin.AddJinDanHP((int)getSeidJson(seid)["value1"].n * buffInfo[1]);
	}

	public void ListRealizeSeid208(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		float num = getSeidJson(seid)["value1"].n / 100f;
		int num2 = (int)((float)(avatar.jieyin.JinDanHP_Max - avatar.jieyin.JinDanHP) * num);
		avatar.jieyin.AddJinDanHP(num2);
	}

	public void ListRealizeSeid215(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int i = getSeidJson(seid)["value1"].I;
		int i2 = getSeidJson(seid)["value2"].I;
		int i3 = getSeidJson(seid)["value3"].I;
		int i4 = getSeidJson(seid)["value4"].I;
		int buffSum = avatar.buffmag.GetBuffSum(i);
		if (buffSum <= 0)
		{
			return;
		}
		foreach (List<int> item in avatar.buffmag.getBuffByID(i))
		{
			avatar.spell.removeBuff(item);
		}
		int num = buffSum / i2;
		if (num > 0)
		{
			avatar.spell.addBuff(i3, num * i4);
		}
	}

	public void ListRealizeSeid217(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int num = avatar.shengShi - avatar.OtherAvatar.shengShi;
		if (num > 0)
		{
			flag[0] += num;
		}
	}

	public void ListRealizeSeid300(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		flag[0] = 0;
	}

	public void ListRealizeSeid312(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		_ = getSeidJson(seid)["value1"].I;
		if (Tools.symbol(getSeidJson(seid)["panduan"].str, avatar.fightTemp.NowRoundUsedSkills.Count, getSeidJson(seid)["value1"].I))
		{
			if (!avatar.SkillSeidFlag.ContainsKey(24))
			{
				avatar.SkillSeidFlag.Add(24, new Dictionary<int, int>());
				avatar.SkillSeidFlag[24].Add(0, 1);
			}
			avatar.SkillSeidFlag[24][0] = 1;
		}
	}

	public void ListRealizeSeid314(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int key = flag[1];
		if (_skillJsonData.DataDict[key].AttackType.Contains(getSeidJson(seid)["value2"].I))
		{
			int buffSum = getTargetAvatar(seid, avatar).buffmag.GetBuffSum(getSeidJson(seid)["value1"].I);
			int nowSkillUsedLingQiSum = RoundManager.instance.NowSkillUsedLingQiSum;
			if (flag[0] > 0)
			{
				flag[0] += nowSkillUsedLingQiSum * buffSum;
			}
		}
	}

	public void ListRealizeSeid315(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
	{
		int i = getSeidJson(seid)["value1"].I;
		int num = getSeidJson(seid)["value2"].I * buffInfo[1];
		avatar.OtherAvatar.spell.addBuff(i, num);
	}
}
