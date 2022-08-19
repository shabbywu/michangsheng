using System;
using System.Collections.Generic;
using System.Reflection;
using Fungus;
using GUIPackage;
using JSONClass;
using Newtonsoft.Json.Linq;
using script.world_script;
using UnityEngine;
using YSGame;
using YSGame.Fight;

namespace KBEngine
{
	// Token: 0x02000C82 RID: 3202
	public class Buff
	{
		// Token: 0x060058C4 RID: 22724 RVA: 0x0024FD80 File Offset: 0x0024DF80
		private static void InitMethod(string methodName)
		{
			if (!Buff.methodDict.ContainsKey(methodName))
			{
				MethodInfo method = typeof(Buff).GetMethod(methodName);
				Buff.methodDict.Add(methodName, method);
			}
		}

		// Token: 0x060058C5 RID: 22725 RVA: 0x0024FDB8 File Offset: 0x0024DFB8
		public Buff(int buffid)
		{
			this.buffID = buffid;
			this._totalTime = jsonData.instance.BuffJsonData[buffid.ToString()]["totaltime"].I;
			foreach (JSONObject jsonobject in jsonData.instance.BuffJsonData[buffid.ToString()]["seid"].list)
			{
				this.seid.Add(jsonobject.I);
			}
		}

		// Token: 0x060058C6 RID: 22726 RVA: 0x0024FE90 File Offset: 0x0024E090
		public void onLoopTrigger(Entity _avatar, List<int> buffInfo, List<int> flag, BuffLoopData buffLoopData)
		{
			try
			{
				foreach (int nowSeid in buffLoopData.seid)
				{
					for (int i = 0; i < buffLoopData.TargetLoopTime; i++)
					{
						this.loopRealizeSeid(nowSeid, _avatar, buffInfo, flag);
					}
					if (!this.CanRealizeSeid((Avatar)_avatar, flag, nowSeid, buffLoopData, buffInfo))
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
						text += string.Format("{0} ", buffInfo[j]);
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
						text2 += string.Format("{0} ", flag[k]);
					}
				}
				string text3 = "";
				if (buffLoopData == null)
				{
					text3 = "null";
				}
				else
				{
					text3 = text3.ToString();
				}
				Debug.LogError(string.Format("检测到buff错误！错误 BuffID:{0}\nbuffInfo:{1}\nflag:{2}\nbuffLoopDataStr:{3}", new object[]
				{
					buffInfo[2],
					text,
					text2,
					text3
				}));
				Debug.LogError(ex);
			}
		}

		// Token: 0x060058C7 RID: 22727 RVA: 0x00250008 File Offset: 0x0024E208
		public void onAttach(Entity _avatar, List<int> buffInfo)
		{
			foreach (int num in this.seid)
			{
				this.onAttachRealizeSeid(num, _avatar, buffInfo);
			}
		}

		// Token: 0x060058C8 RID: 22728 RVA: 0x00250060 File Offset: 0x0024E260
		public void onDetach(Entity _avatar, List<int> buffInfo)
		{
			foreach (int num in this.seid)
			{
				this.onDetachRealizeSeid(num, _avatar, buffInfo);
			}
		}

		// Token: 0x060058C9 RID: 22729 RVA: 0x002500B8 File Offset: 0x0024E2B8
		public void loopRealizeSeid(int seid, Entity _avatar, List<int> buffInfo, List<int> flag)
		{
			Avatar avatar = (Avatar)_avatar;
			string text = "ListRealizeSeid" + seid;
			Buff.InitMethod(text);
			if (Buff.methodDict[text] != null)
			{
				Buff.methodDict[text].Invoke(this, new object[]
				{
					seid,
					avatar,
					buffInfo,
					flag
				});
			}
		}

		// Token: 0x060058CA RID: 22730 RVA: 0x00250124 File Offset: 0x0024E324
		public void onAttachRealizeSeid(int seid, Entity _avatar, List<int> buffInfo)
		{
			Avatar avatar = (Avatar)_avatar;
			string text = "onAttachRealizeSeid" + seid;
			Buff.InitMethod(text);
			if (Buff.methodDict[text] != null)
			{
				Buff.methodDict[text].Invoke(this, new object[]
				{
					seid,
					avatar,
					buffInfo
				});
			}
		}

		// Token: 0x060058CB RID: 22731 RVA: 0x0025018C File Offset: 0x0024E38C
		public void onDetachRealizeSeid(int seid, Entity _avatar, List<int> buffInfo)
		{
			Avatar avatar = (Avatar)_avatar;
			string text = "onDetachRealizeSeid" + seid;
			Buff.InitMethod(text);
			if (Buff.methodDict[text] != null)
			{
				Buff.methodDict[text].Invoke(this, new object[]
				{
					seid,
					avatar,
					buffInfo
				});
			}
		}

		// Token: 0x060058CC RID: 22732 RVA: 0x002501F3 File Offset: 0x0024E3F3
		public Avatar getTargetAvatar(int seid, Avatar attker)
		{
			if (this.getSeidJson(seid)["target"].I == 1)
			{
				return attker;
			}
			return attker.OtherAvatar;
		}

		// Token: 0x060058CD RID: 22733 RVA: 0x00250218 File Offset: 0x0024E418
		public JSONObject getSeidJson(int seid)
		{
			if (!this.buffSeidList.ContainsKey(seid))
			{
				if (seid >= jsonData.instance.BuffSeidJsonData.Length)
				{
					Debug.LogError(string.Format("获取buff seid数据失败，buff id:{0}，seid:{1}，seid超出了jsonData.instance.BuffSeidJsonData.Length，请检查配表", this.buffID, seid));
					return null;
				}
				JSONObject jsonobject = jsonData.instance.BuffSeidJsonData[seid];
				if (jsonobject.HasField(this.buffID.ToString()))
				{
					this.buffSeidList[seid] = jsonobject[this.buffID.ToString()];
				}
				else
				{
					Debug.LogError(string.Format("获取buff seid数据失败，buff id:{0}，seid:{1}，buff seid{2}表中不存在buff {3}的数据，请检查配表", new object[]
					{
						this.buffID,
						seid,
						seid,
						this.buffID
					}));
				}
			}
			return this.buffSeidList[seid];
		}

		// Token: 0x060058CE RID: 22734 RVA: 0x002502FA File Offset: 0x0024E4FA
		public static JSONObject getSeidJson(int seid, int _buffID)
		{
			return jsonData.instance.BuffSeidJsonData[seid][_buffID.ToString()];
		}

		// Token: 0x060058CF RID: 22735 RVA: 0x00250314 File Offset: 0x0024E514
		public bool CanRealized(Avatar _avatar, List<int> flag, List<int> buffInfo = null)
		{
			foreach (int nowSeid in this.seid)
			{
				if (!this.CanRealizeSeid(_avatar, flag, nowSeid, null, buffInfo))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060058D0 RID: 22736 RVA: 0x00250374 File Offset: 0x0024E574
		public bool CanRealizeSeid(Avatar _avatar, List<int> flag, int nowSeid, BuffLoopData buffLoopData = null, List<int> buffInfo = null)
		{
			if (nowSeid == 62 && (float)_avatar.HP / (float)_avatar.HP_Max * 100f > (float)this.getSeidJson(nowSeid)["value1"].I)
			{
				return false;
			}
			if (nowSeid == 65 && this.RandomX(this.getSeidJson(nowSeid)["value1"].I))
			{
				return false;
			}
			if (nowSeid == 73 && _avatar.OtherAvatar.crystal.getCardNum() == 0)
			{
				return false;
			}
			if (nowSeid == 74 && !_avatar.OtherAvatar.buffmag.HasBuff(this.getSeidJson(nowSeid)["value1"].I))
			{
				return false;
			}
			if (nowSeid == 75)
			{
				for (int i = 0; i < this.getSeidJson(nowSeid)["value1"].Count; i++)
				{
					if (!_avatar.buffmag.HasBuff(this.getSeidJson(nowSeid)["value1"][i].I))
					{
						return false;
					}
				}
			}
			if (nowSeid == 76)
			{
				try
				{
					if (_skillJsonData.DataDict[flag[1]].Skill_ID != this.getSeidJson(nowSeid)["value1"].I)
					{
						return false;
					}
				}
				catch (Exception ex)
				{
					Debug.Log(ex);
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
				Skill skill = null;
				foreach (Skill skill2 in _avatar.skill)
				{
					if (num == skill2.skill_ID)
					{
						skill = skill2;
						using (Dictionary<int, int>.Enumerator enumerator2 = skill2.nowSkillUseCard.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								KeyValuePair<int, int> keyValuePair = enumerator2.Current;
								num2 += keyValuePair.Value;
							}
							break;
						}
					}
				}
				if (num2 < this.getSeidJson(nowSeid)["value1"].I)
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
				List<JSONObject> list = this.getSeidJson(nowSeid)["value1"].list;
				bool flag2 = true;
				using (List<JSONObject>.Enumerator enumerator3 = list.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						JSONObject __i = enumerator3.Current;
						if (_skillJsonData.DataDict[key].AttackType.FindAll((int a) => a == __i.I).Count > 0)
						{
							flag2 = false;
							break;
						}
					}
				}
				if (flag2)
				{
					return false;
				}
			}
			if (nowSeid == 86 && Tools.instance.MonstarID != this.getSeidJson(nowSeid)["value1"].I)
			{
				return false;
			}
			if (nowSeid == 91 && _avatar.OtherAvatar.buffmag.checkHasBuff(this.getSeidJson(nowSeid)["value1"].I, _avatar.OtherAvatar))
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
			if (nowSeid == 97 && (float)_avatar.HP / (float)_avatar.HP_Max * 100f > (float)this.getSeidJson(nowSeid)["value1"].I)
			{
				return false;
			}
			if (nowSeid == 101 && _avatar.cardMag.getCardTypeNum(this.getSeidJson(nowSeid)["value1"].I) <= 0)
			{
				return false;
			}
			if (nowSeid == 102 && (ulong)_avatar.AvatarType == (ulong)((long)this.getSeidJson(nowSeid)["value1"].I))
			{
				return false;
			}
			if (nowSeid == 110)
			{
				foreach (JSONObject jsonobject in this.getSeidJson(nowSeid)["value1"].list)
				{
					if (!_avatar.buffmag.HasBuff(jsonobject.I))
					{
						return false;
					}
				}
			}
			if (nowSeid == 113)
			{
				int key2 = flag[1];
				List<JSONObject> list2 = this.getSeidJson(nowSeid)["value1"].list;
				bool flag3 = true;
				using (List<JSONObject>.Enumerator enumerator3 = list2.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						JSONObject __i = enumerator3.Current;
						if (_skillJsonData.DataDict[key2].AttackType.FindAll((int a) => a == __i.I).Count == 0)
						{
							flag3 = false;
							break;
						}
					}
				}
				if (flag3)
				{
					return false;
				}
			}
			if (nowSeid == 116 && _avatar.Dandu <= this.getSeidJson(nowSeid)["value1"].I)
			{
				return false;
			}
			if (nowSeid == 118 && _avatar.fightTemp.AllDamage <= this.getSeidJson(nowSeid)["value1"].I)
			{
				return false;
			}
			if (nowSeid == 119)
			{
				List<List<int>> buffByID = this.getTargetAvatar(nowSeid, _avatar).buffmag.getBuffByID(this.getSeidJson(nowSeid)["value1"].I);
				int num3 = 0;
				foreach (List<int> list3 in buffByID)
				{
					num3 += list3[1];
				}
				if (num3 <= this.getSeidJson(nowSeid)["value2"].I)
				{
					return false;
				}
			}
			if (nowSeid == 120 && _avatar.cardMag[this.getSeidJson(nowSeid)["value1"].I] == 0)
			{
				return false;
			}
			if (nowSeid == 121 && _avatar.cardMag[this.getSeidJson(nowSeid)["value1"].I] != 0)
			{
				return false;
			}
			if (nowSeid == 122)
			{
				int key3 = flag[1];
				JSONObject jsonobject2 = jsonData.instance.skillJsonData[key3.ToString()]["AttackType"];
				if (!_skillJsonData.DataDict[key3].AttackType.Contains(this.getSeidJson(nowSeid)["value1"].I))
				{
					return false;
				}
			}
			if (nowSeid == 126)
			{
				int buffRoundByID = _avatar.buffmag.GetBuffRoundByID(this.getSeidJson(nowSeid)["value1"].I);
				int buffRoundByID2 = _avatar.buffmag.GetBuffRoundByID(this.getSeidJson(nowSeid)["value2"].I);
				if (buffRoundByID <= buffRoundByID2)
				{
					return false;
				}
			}
			if (nowSeid == 128)
			{
				int i2 = this.getSeidJson(nowSeid)["value1"].I;
				if (_avatar.BuffSeidFlag[nowSeid][this.buffID] < i2)
				{
					return false;
				}
				bool flag4 = true;
				foreach (JSONObject jsonobject3 in this.getSeidJson(nowSeid)["value2"].list)
				{
					if (!_skillJsonData.DataDict[flag[1]].AttackType.Contains(jsonobject3.I))
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
				int i3 = this.getSeidJson(nowSeid)["value1"].I;
				if (!Skill.IsSkillType(_avatar.UsedSkills[_avatar.UsedSkills.Count - 1], this.getSeidJson(nowSeid)["value1"].I))
				{
					return false;
				}
			}
			if (nowSeid == 151)
			{
				int key4 = flag[1];
				JSONObject jsonobject4 = jsonData.instance.skillJsonData[key4.ToString()];
				if (_skillJsonData.DataDict[key4].script != "SkillAttack")
				{
					return false;
				}
			}
			if (nowSeid == 152)
			{
				int key5 = flag[1];
				JSONObject jsonobject5 = jsonData.instance.skillJsonData[key5.ToString()];
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
				int num6 = this.getTargetAvatar(nowSeid, _avatar).buffmag.getBuffTypeNum(this.getSeidJson(nowSeid)["value1"].I);
				if (buffLoopData == null || num6 <= 0)
				{
					return false;
				}
				num6--;
				buffLoopData.TargetLoopTime += num6;
			}
			if (nowSeid == 157)
			{
				Avatar targetAvatar = this.getTargetAvatar(nowSeid, _avatar);
				int i4 = this.getSeidJson(nowSeid)["value2"].I;
				int num7 = 0;
				using (List<List<int>>.Enumerator enumerator4 = targetAvatar.fightTemp.RoundHasBuff.GetEnumerator())
				{
					while (enumerator4.MoveNext())
					{
						if (enumerator4.Current.Contains(i4))
						{
							num7++;
						}
						else
						{
							num7 = 0;
						}
					}
				}
				if (num7 < this.getSeidJson(nowSeid)["value1"].I)
				{
					return false;
				}
			}
			if (nowSeid == 158 && this.getTargetAvatar(nowSeid, _avatar).crystal.getCardNum() != 0)
			{
				return false;
			}
			if (nowSeid == 159)
			{
				int buffRoundByID3 = this.getTargetAvatar(nowSeid, _avatar).buffmag.GetBuffRoundByID(this.getSeidJson(nowSeid)["value1"].I);
				if (!Tools.symbol(this.getSeidJson(nowSeid)["panduan"].str, buffRoundByID3, this.getSeidJson(nowSeid)["value2"].I))
				{
					return false;
				}
			}
			if (nowSeid == 160)
			{
				int i5 = this.getSeidJson(nowSeid)["value2"].I;
				int num8 = 0;
				foreach (int key6 in _avatar.fightTemp.NowRoundDamageSkills)
				{
					if (_skillJsonData.DataDict[key6].AttackType.Contains(i5))
					{
						num8++;
					}
				}
				if (num8 <= this.getSeidJson(nowSeid)["value1"].I)
				{
					return false;
				}
			}
			if (nowSeid == 161)
			{
				int cardNum = _avatar.crystal.getCardNum();
				if (!Tools.symbol(this.getSeidJson(nowSeid)["panduan"].str, cardNum, (int)_avatar.NowCard))
				{
					return false;
				}
			}
			if (nowSeid == 162)
			{
				List<int> list4 = _skillJsonData.DataDict[flag[1]].seid;
				bool flag5 = false;
				foreach (int item in this.getSeidJson(nowSeid)["value1"].ToList())
				{
					if (list4.Contains(item))
					{
						SkillCheck skillCheck;
						if (_avatar.isPlayer())
						{
							skillCheck = RoundManager.instance.PlayerSkillCheck;
						}
						else
						{
							skillCheck = RoundManager.instance.NpcSkillCheck;
						}
						if (skillCheck != null && skillCheck.SkillId == flag[1] && skillCheck.HasPassSeid.Contains(item))
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
				int num9 = flag[0];
				int i6 = this.getSeidJson(nowSeid)["value1"].I;
				if (num9 != i6)
				{
					return false;
				}
			}
			if (nowSeid == 165)
			{
				int i7 = this.getSeidJson(nowSeid)["value1"].I;
				if (_avatar.buffmag.HasBuff(i7))
				{
					return false;
				}
			}
			if ((nowSeid == 166 || nowSeid == 170) && !this.IsXiangShengXiangKeTongXi(this.getSeidJson(nowSeid)["value1"].I, flag[1], _avatar, buffInfo))
			{
				return false;
			}
			if (nowSeid == 167)
			{
				Avatar targetAvatar2 = this.getTargetAvatar(nowSeid, _avatar);
				int i8 = this.getSeidJson(nowSeid)["value1"].I;
				if (targetAvatar2.buffmag.getAllBuffByType(i8).Count <= 0)
				{
					return false;
				}
			}
			if (nowSeid == 168)
			{
				Avatar targetAvatar3 = this.getTargetAvatar(nowSeid, _avatar);
				int i9 = this.getSeidJson(nowSeid)["value1"].I;
				List<List<int>> allBuffByType = targetAvatar3.buffmag.getAllBuffByType(i9);
				if (!Tools.symbol(this.getSeidJson(nowSeid)["panduan"].str, allBuffByType.Count, this.getSeidJson(nowSeid)["value2"].I))
				{
					return false;
				}
			}
			if (nowSeid == 169)
			{
				int key7 = flag[1];
				JSONObject jsonobject6 = jsonData.instance.skillJsonData[key7.ToString()];
				if (!_skillJsonData.DataDict[key7].seid.Contains(this.getSeidJson(nowSeid)["value1"].I))
				{
					return false;
				}
			}
			if (nowSeid == 172)
			{
				int num10 = buffInfo[1];
				int i10 = this.getSeidJson(nowSeid)["value1"].I;
				if (num10 > i10)
				{
					return false;
				}
			}
			if (nowSeid == 173)
			{
				int num11 = flag[0];
				int i11 = this.getSeidJson(nowSeid)["value1"].I;
				if (num11 < i11)
				{
					return false;
				}
			}
			if (nowSeid == 174 && _avatar.dunSu < _avatar.OtherAvatar.dunSu)
			{
				return false;
			}
			if (nowSeid == 175 && _avatar.fightTemp.RoundReceiveDamage[0] < this.getSeidJson(nowSeid)["value1"].I)
			{
				return false;
			}
			if (nowSeid == 176)
			{
				Avatar targetAvatar4 = this.getTargetAvatar(nowSeid, _avatar);
				int i12 = this.getSeidJson(nowSeid)["value1"].I;
				if (!targetAvatar4.BuffSeidFlag.ContainsKey(176))
				{
					return false;
				}
				if (!targetAvatar4.BuffSeidFlag[176].ContainsKey(i12))
				{
					return false;
				}
			}
			if (nowSeid == 177)
			{
				int key8 = flag[1];
				List<int> attackType = _skillJsonData.DataDict[key8].AttackType;
				for (int j = 0; j < attackType.Count; j++)
				{
					if (attackType[j] == this.getSeidJson(nowSeid)["value1"].I)
					{
						this.BuffSeidFlagAddNum(nowSeid, 1, _avatar);
						break;
					}
				}
				if (!_avatar.BuffSeidFlag.ContainsKey(nowSeid))
				{
					return false;
				}
				int key9 = buffInfo[2];
				if (!_avatar.BuffSeidFlag[nowSeid].ContainsKey(key9))
				{
					return false;
				}
				int i13 = this.getSeidJson(nowSeid)["value2"].I;
				if (_avatar.BuffSeidFlag[nowSeid][key9] < i13)
				{
					return false;
				}
				_avatar.BuffSeidFlag[nowSeid][key9] = 0;
			}
			if (nowSeid == 178 && _avatar.shengShi < _avatar.OtherAvatar.shengShi)
			{
				return false;
			}
			if (nowSeid == 180)
			{
				int i14 = this.getSeidJson(nowSeid)["value1"].I;
				if (!Tools.symbol(this.getSeidJson(nowSeid)["panduan"].str, _avatar.cardMag.getCardNum(), i14))
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
				int i15 = this.getSeidJson(nowSeid)["value1"].I;
				if (!Tools.symbol(this.getSeidJson(nowSeid)["panduan"].str, _avatar.fightTemp.NowRoundUsedSkills.Count, this.getSeidJson(nowSeid)["value1"].I))
				{
					return false;
				}
			}
			if (nowSeid == 197)
			{
				int buffSum = this.getTargetAvatar(nowSeid, _avatar).buffmag.GetBuffSum(this.getSeidJson(nowSeid)["value1"].I);
				if (buffLoopData != null)
				{
					buffLoopData.TargetLoopTime += buffSum;
				}
			}
			if (nowSeid == 206)
			{
				int nowValue = _avatar.jieyin.GetNowValue(this.getSeidJson(nowSeid)["value1"].I);
				if (!Tools.symbol(this.getSeidJson(nowSeid)["panduan"].str, nowValue, this.getSeidJson(nowSeid)["value2"].I))
				{
					return false;
				}
			}
			if (nowSeid == 213)
			{
				int huaYing = _avatar.jieyin.HuaYing;
				if (!Tools.symbol(this.getSeidJson(nowSeid)["panduan"].str, huaYing, this.getSeidJson(nowSeid)["value1"].I))
				{
					return false;
				}
			}
			if (nowSeid == 214)
			{
				int statr = flag[0];
				if (!Tools.symbol(this.getSeidJson(nowSeid)["panduan"].str, statr, this.getSeidJson(nowSeid)["value1"].I))
				{
					return false;
				}
			}
			if (nowSeid == 216)
			{
				int key10 = flag[1];
				int i16 = this.getSeidJson(nowSeid)["value1"].I;
				if (!_skillJsonData.DataDict[key10].AttackType.Contains(i16))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060058D1 RID: 22737 RVA: 0x002515E0 File Offset: 0x0024F7E0
		public bool IsXiangShengXiangKeTongXi(int type, int skillID, Avatar avatar, List<int> buffInfo)
		{
			Skill skill = avatar.skill.Find((Skill aa) => aa.skill_ID == skillID);
			if (skill == null || buffInfo == null)
			{
				return false;
			}
			if (skill.nowSkillIsChuFa.ContainsKey(buffInfo[2]) && skill.nowSkillIsChuFa[buffInfo[2]])
			{
				return false;
			}
			if (type == 1)
			{
				Dictionary<int, int> xiangSheng = Tools.GetXiangSheng();
				int num = 0;
				if (skill.nowSkillUseCard.Count <= 1)
				{
					return false;
				}
				foreach (KeyValuePair<int, int> keyValuePair in skill.nowSkillUseCard)
				{
					if (skill.nowSkillUseCard.ContainsKey(xiangSheng[keyValuePair.Key]))
					{
						num++;
					}
				}
				if (num >= skill.nowSkillUseCard.Count - 1)
				{
					skill.nowSkillIsChuFa[buffInfo[2]] = true;
					return true;
				}
			}
			else if (type == 2)
			{
				Dictionary<int, int> xiangKe = Tools.GetXiangKe();
				int num2 = 0;
				if (skill.nowSkillUseCard.Count <= 1)
				{
					return false;
				}
				foreach (KeyValuePair<int, int> keyValuePair2 in skill.nowSkillUseCard)
				{
					if (skill.nowSkillUseCard.ContainsKey(xiangKe[keyValuePair2.Key]))
					{
						num2++;
					}
				}
				if (num2 >= skill.nowSkillUseCard.Count - 1)
				{
					skill.nowSkillIsChuFa[buffInfo[2]] = true;
					return true;
				}
			}
			else if (type == 3)
			{
				int num3 = 0;
				foreach (KeyValuePair<int, int> keyValuePair3 in skill.nowSkillUseCard)
				{
					num3 += keyValuePair3.Value;
				}
				if (num3 <= 1)
				{
					return false;
				}
				if (skill.nowSkillUseCard.Count == 1)
				{
					skill.nowSkillIsChuFa[buffInfo[2]] = true;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060058D2 RID: 22738 RVA: 0x00251814 File Offset: 0x0024FA14
		public void BuffSeidFlagAddNum(int seid, int addNum, Avatar avatar)
		{
			if (!avatar.BuffSeidFlag.ContainsKey(seid))
			{
				avatar.BuffSeidFlag.Add(seid, new Dictionary<int, int>());
			}
			if (!avatar.BuffSeidFlag[seid].ContainsKey(this.buffID))
			{
				avatar.BuffSeidFlag[seid].Add(this.buffID, 0);
			}
			Dictionary<int, int> dictionary = avatar.BuffSeidFlag[seid];
			int key = this.buffID;
			dictionary[key] += addNum;
		}

		// Token: 0x060058D3 RID: 22739 RVA: 0x00251898 File Offset: 0x0024FA98
		public void ReloadSelf(int seid, Avatar avatar, List<int> buffInfo, int Type)
		{
			List<int> list = new List<int>();
			list.Add(0);
			list.Add(Type);
			string text = "ListRealizeSeid" + seid;
			Buff.InitMethod(text);
			if (Buff.methodDict[text] != null)
			{
				Buff.methodDict[text].Invoke(this, new object[]
				{
					seid,
					avatar,
					buffInfo,
					list
				});
			}
		}

		// Token: 0x060058D4 RID: 22740 RVA: 0x00251914 File Offset: 0x0024FB14
		public void SeidAddBuff(int seid, Avatar avatar, List<int> buffInfo)
		{
			Dictionary<int, int> dictionary = avatar.BuffSeidFlag[seid];
			int key = this.buffID;
			dictionary[key] -= (int)this.getSeidJson(seid)["value1"].n;
			avatar.spell.addDBuff((int)this.getSeidJson(seid)["value2"].n, (int)this.getSeidJson(seid)["value3"].n * buffInfo[1]);
		}

		// Token: 0x060058D5 RID: 22741 RVA: 0x0025199C File Offset: 0x0024FB9C
		public void SeidAddCard(Avatar avatar, List<int> flag)
		{
			if (flag.Count >= 3 && flag[2] == -123)
			{
				int num = flag[0];
				flag[0] = num + 1;
				return;
			}
			RoundManager.instance.DrawCard(avatar);
		}

		// Token: 0x060058D6 RID: 22742 RVA: 0x002519DC File Offset: 0x0024FBDC
		public void SeidAddCard(Avatar avatar, List<int> flag, int cardType)
		{
			if (flag.Count >= 3 && flag[2] == 1)
			{
				int num = flag[0];
				flag[0] = num + 1;
				return;
			}
			RoundManager.instance.DrawCard(avatar, cardType);
		}

		// Token: 0x060058D7 RID: 22743 RVA: 0x00251A1B File Offset: 0x0024FC1B
		public bool RandomX(int percent)
		{
			return jsonData.instance.getRandom() % 100 > percent;
		}

		// Token: 0x060058D8 RID: 22744 RVA: 0x00251A30 File Offset: 0x0024FC30
		public void ListRealizeSeid1(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			if (avatar.buffmag.HasBuffSeid(58))
			{
				List<List<int>> buffBySeid = avatar.buffmag.getBuffBySeid(58);
				avatar.spell.onBuffTick(avatar.buffmag.getBuffIndex(buffBySeid[0]), null, 0);
			}
			avatar.recvDamage(avatar, avatar, 10006, (int)this.getSeidJson(seid)["value1"].n * buffInfo[1], 0);
		}

		// Token: 0x060058D9 RID: 22745 RVA: 0x00251AA8 File Offset: 0x0024FCA8
		public void ListRealizeSeid2(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int num = this.getSeidJson(seid)["value1"].I * buffInfo[1];
			for (int i = 0; i < num; i++)
			{
				this.SeidAddCard(avatar, flag);
			}
		}

		// Token: 0x060058DA RID: 22746 RVA: 0x00251AEC File Offset: 0x0024FCEC
		public void ListRealizeSeid3(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int num = flag[0] + (int)this.getSeidJson(seid)["value1"].n * buffInfo[1];
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

		// Token: 0x060058DB RID: 22747 RVA: 0x00251B58 File Offset: 0x0024FD58
		public void ListRealizeSeid4(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int num = 0;
			if (flag[0] > buffInfo[1])
			{
				num = buffInfo[1];
			}
			else if (flag[0] <= buffInfo[1])
			{
				num = flag[0];
			}
			if (!(RoundManager.instance != null) || !RoundManager.instance.IsVirtual)
			{
				((GameObject)avatar.renderObj).GetComponentInChildren<AvatarShowHpDamage>().SpecialShow(num, 1);
			}
			flag[0] = flag[0] - num;
			buffInfo[1] = buffInfo[1] - num;
			if (buffInfo[2] == 5 && buffInfo[1] <= 0)
			{
				avatar.spell.onBuffTickByType(43, flag);
			}
		}

		// Token: 0x060058DC RID: 22748 RVA: 0x00251C14 File Offset: 0x0024FE14
		public void ListRealizeSeid5(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			for (int i = 0; i < this.getSeidJson(seid)["value1"].list.Count; i++)
			{
				int i2 = this.getSeidJson(seid)["value1"][i].I;
				int num = this.getSeidJson(seid)["value2"][i].I * buffInfo[1];
				avatar.spell.addBuff(i2, num);
			}
		}

		// Token: 0x060058DD RID: 22749 RVA: 0x00004095 File Offset: 0x00002295
		public void ListRealizeSeid6(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
		}

		// Token: 0x060058DE RID: 22750 RVA: 0x00004095 File Offset: 0x00002295
		public void ListRealizeSeid7(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
		}

		// Token: 0x060058DF RID: 22751 RVA: 0x00004095 File Offset: 0x00002295
		public void ListRealizeSeid8(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
		}

		// Token: 0x060058E0 RID: 22752 RVA: 0x00251C98 File Offset: 0x0024FE98
		public void ListRealizeSeid9(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int num = this.getSeidJson(seid)["value2"].I * buffInfo[1];
			foreach (JSONObject jsonobject in this.getSeidJson(seid)["value1"].list)
			{
				if (Tools.instance.getSkillIDByKey(flag[1]) == jsonobject.I)
				{
					flag[0] = flag[0] + num;
					break;
				}
			}
		}

		// Token: 0x060058E1 RID: 22753 RVA: 0x00251D44 File Offset: 0x0024FF44
		public void ListRealizeSeid10(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int num = flag[0];
			flag[0] = flag[0] + (int)this.getSeidJson(seid)["value1"].n * buffInfo[1];
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

		// Token: 0x060058E2 RID: 22754 RVA: 0x00251DC5 File Offset: 0x0024FFC5
		public void ListRealizeSeid11(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			avatar.recvDamage(avatar, avatar.OtherAvatar, 10006, (int)this.getSeidJson(seid)["value1"].n * buffInfo[1], 0);
		}

		// Token: 0x060058E3 RID: 22755 RVA: 0x00251DFA File Offset: 0x0024FFFA
		public void ListRealizeSeid12(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			flag[0] = flag[0] * 2;
		}

		// Token: 0x060058E4 RID: 22756 RVA: 0x00251E0E File Offset: 0x0025000E
		public void ListRealizeSeid13(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			flag[1] = 1;
			avatar.state = 5;
		}

		// Token: 0x060058E5 RID: 22757 RVA: 0x00251E20 File Offset: 0x00250020
		public void ListRealizeSeid14(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			if (flag[0] < 0)
			{
				flag[0] = (int)Math.Ceiling(Convert.ToDouble(flag[0] / 2));
			}
		}

		// Token: 0x060058E6 RID: 22758 RVA: 0x00251E4C File Offset: 0x0025004C
		public void ListRealizeSeid15(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int i = this.getSeidJson(seid)["value1"].I;
			int i2 = this.getSeidJson(seid)["value2"].I;
			bool flag2 = false;
			foreach (int num in _skillJsonData.DataDict[flag[1]].AttackType)
			{
				if (i == num)
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

		// Token: 0x060058E7 RID: 22759 RVA: 0x00251F0C File Offset: 0x0025010C
		public void ListRealizeSeid16(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			avatar.recvDamage(avatar, avatar, 10006, avatar.HP - avatar.HP_Max, 0);
		}

		// Token: 0x060058E8 RID: 22760 RVA: 0x00251F2C File Offset: 0x0025012C
		public void ListRealizeSeid17(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int i = this.getSeidJson(seid)["value1"].I;
			int num = this.getSeidJson(seid)["value2"].I * buffInfo[1];
			for (int j = 0; j < num; j++)
			{
				avatar.OtherAvatar.spell.addDBuff(i);
			}
		}

		// Token: 0x060058E9 RID: 22761 RVA: 0x00251F90 File Offset: 0x00250190
		public void ListRealizeSeid18(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			if (avatar.OtherAvatar.buffmag.HasBuffSeid(107))
			{
				return;
			}
			if (RoundManager.instance.IsVirtual)
			{
				return;
			}
			if (flag[0] <= 0)
			{
				return;
			}
			if (jsonData.instance.getRandom() % 100 <= (int)this.getSeidJson(seid)["value1"].n)
			{
				((GameObject)avatar.renderObj).GetComponentInChildren<AvatarShowHpDamage>().show("闪避");
				flag[0] = 0;
				avatar.spell.onBuffTickByType(35, new List<int>
				{
					0
				});
			}
		}

		// Token: 0x060058EA RID: 22762 RVA: 0x00004095 File Offset: 0x00002295
		public void ListRealizeSeid19(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
		}

		// Token: 0x060058EB RID: 22763 RVA: 0x00004095 File Offset: 0x00002295
		public void ListRealizeSeid20(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
		}

		// Token: 0x060058EC RID: 22764 RVA: 0x00252030 File Offset: 0x00250230
		public void ListRealizeSeid21(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			flag[0] = flag[0] - (int)this.getSeidJson(seid)["value1"].n * buffInfo[1];
		}

		// Token: 0x060058ED RID: 22765 RVA: 0x0025206E File Offset: 0x0025026E
		public void ListRealizeSeid22(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			if (!avatar.buffmag.HasBuffSeid(23))
			{
				avatar.fightTemp.tempNowCard += (int)this.getSeidJson(seid)["value1"].n;
			}
		}

		// Token: 0x060058EE RID: 22766 RVA: 0x002520A8 File Offset: 0x002502A8
		public void onDetachRealizeSeid22(int seid, Avatar avatar, List<int> buffInfo)
		{
			if (!avatar.buffmag.HasBuffSeid(23))
			{
				avatar.fightTemp.tempNowCard -= (int)this.getSeidJson(seid)["value1"].n * buffInfo[1];
			}
		}

		// Token: 0x060058EF RID: 22767 RVA: 0x002520F8 File Offset: 0x002502F8
		public void ListRealizeSeid23(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			if (!avatar.BuffSeidFlag.ContainsKey(seid))
			{
				avatar.BuffSeidFlag.Add(seid, new Dictionary<int, int>());
			}
			if (!avatar.BuffSeidFlag[seid].ContainsKey(this.buffID))
			{
				avatar.BuffSeidFlag[seid].Add(this.buffID, 0);
			}
			avatar.BuffSeidFlag[seid][this.buffID] = 0;
		}

		// Token: 0x060058F0 RID: 22768 RVA: 0x0025216D File Offset: 0x0025036D
		public void onDetachRealizeSeid23(int seid, Avatar avatar, List<int> buffInfo)
		{
			avatar.BuffSeidFlag.Remove(seid);
		}

		// Token: 0x060058F1 RID: 22769 RVA: 0x0025217C File Offset: 0x0025037C
		public void ListRealizeSeid24(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int skillID = Tools.instance.getSkillKeyByID((int)this.getSeidJson(seid)["value1"].n, avatar);
			Skill skill = new Skill(skillID, 0, 10);
			List<int> _damage = new List<int>();
			Tools.AddQueue(delegate
			{
				RoundManager.instance.NowUseLingQiType = UseLingQiType.释放技能后消耗;
				if (jsonData.instance.skillJsonData[string.Concat(skillID)]["script"].str == "SkillAttack")
				{
					_damage = skill.PutingSkill(avatar, avatar.OtherAvatar, 0);
				}
				else if (jsonData.instance.skillJsonData[string.Concat(skillID)]["script"].str == "SkillSelf")
				{
					_damage = skill.PutingSkill(avatar, avatar, 0);
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

		// Token: 0x060058F2 RID: 22770 RVA: 0x002521FC File Offset: 0x002503FC
		public void ListRealizeSeid25(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			avatar.recvDamage(avatar, avatar.OtherAvatar, 10001 + (int)this.getSeidJson(seid)["value2"].n, (int)this.getSeidJson(seid)["value1"].n * buffInfo[1], 0);
		}

		// Token: 0x060058F3 RID: 22771 RVA: 0x00252254 File Offset: 0x00250454
		public void ListRealizeSeid26(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int i = this.getSeidJson(seid)["value1"].I;
			int i2 = this.getSeidJson(seid)["value2"].I;
			int num = i * buffInfo[1];
			for (int j = 0; j < num; j++)
			{
				RoundManager.instance.DrawCard(avatar, i2);
			}
		}

		// Token: 0x060058F4 RID: 22772 RVA: 0x002522B0 File Offset: 0x002504B0
		public void ListRealizeSeid27(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int i = this.getSeidJson(seid)["value1"].I;
			bool flag2 = false;
			foreach (int num in _skillJsonData.DataDict[flag[1]].AttackType)
			{
				if (i == num)
				{
					flag2 = true;
					break;
				}
			}
			if (flag2)
			{
				int i2 = this.getSeidJson(seid)["value2"].I;
				flag[0] = flag[0] + i2 * buffInfo[1];
			}
		}

		// Token: 0x060058F5 RID: 22773 RVA: 0x00252368 File Offset: 0x00250568
		public void ListRealizeSeid28(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int i = this.getSeidJson(seid)["value1"].I;
			int i2 = this.getSeidJson(seid)["value2"].I;
			int num = i * buffInfo[1];
			for (int j = 0; j < num; j++)
			{
				foreach (List<int> item in avatar.OtherAvatar.buffmag.getBuffByID(i2))
				{
					avatar.OtherAvatar.spell.onBuffTick(avatar.OtherAvatar.bufflist.IndexOf(item), null, 0);
				}
			}
		}

		// Token: 0x060058F6 RID: 22774 RVA: 0x00252428 File Offset: 0x00250628
		public void ListRealizeSeid29(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int num = flag[0];
			if (avatar.HP_Max < avatar.HP - num)
			{
				avatar.recvDamage(avatar, avatar.OtherAvatar, 10006, avatar.HP - num - avatar.HP_Max, 0);
			}
		}

		// Token: 0x060058F7 RID: 22775 RVA: 0x00252474 File Offset: 0x00250674
		public void ListRealizeSeid31(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int num = flag[0];
			if (avatar.HP - num <= 0)
			{
				flag[0] = 0;
				avatar.HP = num + 1;
				int num2 = 0;
				foreach (JSONObject jsonobject in this.getSeidJson(seid)["value1"].list)
				{
					num2 += avatar.buffmag.GetBuffSum(jsonobject.I);
				}
				num2 *= this.getSeidJson(seid)["value2"].I;
				avatar.recvDamage(avatar, avatar, 10006, -num2, 0);
			}
		}

		// Token: 0x060058F8 RID: 22776 RVA: 0x0025253C File Offset: 0x0025073C
		public void ListRealizeSeid32(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			if (flag.Count < 1)
			{
				return;
			}
			int addNum = flag[0];
			this.BuffSeidFlagAddNum(seid, addNum, avatar);
			int num = avatar.BuffSeidFlag[seid][this.buffID] / this.getSeidJson(seid)["value1"].I;
			if (num > 0)
			{
				Dictionary<int, int> dictionary = avatar.BuffSeidFlag[seid];
				int key = this.buffID;
				dictionary[key] -= (int)this.getSeidJson(seid)["value1"].n * num;
				avatar.spell.addBuff(this.getSeidJson(seid)["value2"].I, num * this.getSeidJson(seid)["value3"].I * buffInfo[1]);
			}
		}

		// Token: 0x060058F9 RID: 22777 RVA: 0x00252618 File Offset: 0x00250818
		public void ListRealizeSeid33(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int addNum = flag[0];
			this.BuffSeidFlagAddNum(seid, addNum, avatar);
			if (avatar.BuffSeidFlag[seid][this.buffID] >= (int)this.getSeidJson(seid)["value1"].n)
			{
				this.SeidAddBuff(seid, avatar, buffInfo);
				if (avatar.BuffSeidFlag[seid][this.buffID] >= (int)this.getSeidJson(seid)["value1"].n)
				{
					this.ReloadSelf(seid, avatar, buffInfo, 0);
				}
			}
		}

		// Token: 0x060058FA RID: 22778 RVA: 0x002526AC File Offset: 0x002508AC
		public void ListRealizeSeid34(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int addNum = flag[0];
			int num = flag[1];
			if (num == (int)this.getSeidJson(seid)["value4"].n)
			{
				this.BuffSeidFlagAddNum(seid, addNum, avatar);
				if (avatar.BuffSeidFlag[seid][this.buffID] >= (int)this.getSeidJson(seid)["value1"].n)
				{
					this.SeidAddBuff(seid, avatar, buffInfo);
					if (avatar.BuffSeidFlag[seid][this.buffID] >= (int)this.getSeidJson(seid)["value1"].n)
					{
						this.ReloadSelf(seid, avatar, buffInfo, num);
					}
				}
			}
		}

		// Token: 0x060058FB RID: 22779 RVA: 0x00252764 File Offset: 0x00250964
		public void ListRealizeSeid35(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int addNum = flag[0];
			int num = flag[1];
			if (num == (int)this.getSeidJson(seid)["value4"].n)
			{
				this.BuffSeidFlagAddNum(seid, addNum, avatar);
				if (avatar.BuffSeidFlag[seid][this.buffID] >= (int)this.getSeidJson(seid)["value1"].n)
				{
					this.SeidAddBuff(seid, avatar, buffInfo);
					if (avatar.BuffSeidFlag[seid][this.buffID] >= (int)this.getSeidJson(seid)["value1"].n)
					{
						this.ReloadSelf(seid, avatar, buffInfo, num);
					}
				}
			}
		}

		// Token: 0x060058FC RID: 22780 RVA: 0x0025281C File Offset: 0x00250A1C
		public void ListRealizeSeid36(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int num = RoundManager.instance.getListSum(avatar.crystal) / (int)this.getSeidJson(seid)["value1"].n;
			if (num > 0)
			{
				avatar.spell.addBuff(this.getSeidJson(seid)["value2"].I, num * this.getSeidJson(seid)["value3"].I * buffInfo[1]);
			}
		}

		// Token: 0x060058FD RID: 22781 RVA: 0x00252898 File Offset: 0x00250A98
		public void ListRealizeSeid37(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int i = this.getSeidJson(seid)["value1"].I;
			int i2 = this.getSeidJson(seid)["value2"].I;
			int i3 = this.getSeidJson(seid)["value3"].I;
			int i4 = this.getSeidJson(seid)["value4"].I;
			int num = avatar.crystal[i4] / i;
			if (num > 0)
			{
				int num2 = i3 * buffInfo[1];
				avatar.spell.addBuff(i2, num * num2);
			}
		}

		// Token: 0x060058FE RID: 22782 RVA: 0x00252934 File Offset: 0x00250B34
		public void ListRealizeSeid38(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int addNum = flag[0];
			this.BuffSeidFlagAddNum(seid, addNum, avatar);
			if (avatar.BuffSeidFlag[seid][this.buffID] >= (int)this.getSeidJson(seid)["value1"].n)
			{
				this.SeidAddBuff(seid, avatar, buffInfo);
				if (avatar.BuffSeidFlag[seid][this.buffID] >= (int)this.getSeidJson(seid)["value1"].n)
				{
					this.ReloadSelf(seid, avatar, buffInfo, 0);
				}
			}
		}

		// Token: 0x060058FF RID: 22783 RVA: 0x002529C8 File Offset: 0x00250BC8
		public void ListRealizeSeid39(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int addNum = flag[0];
			if (flag[1] == (int)this.getSeidJson(seid)["value4"].n)
			{
				this.BuffSeidFlagAddNum(seid, addNum, avatar);
				int num = avatar.BuffSeidFlag[seid][this.buffID] / this.getSeidJson(seid)["value1"].I;
				if (num > 0)
				{
					Dictionary<int, int> dictionary = avatar.BuffSeidFlag[seid];
					int key = this.buffID;
					dictionary[key] -= (int)this.getSeidJson(seid)["value1"].n * num;
					avatar.spell.UseSkillLateAddBuff(this.getSeidJson(seid)["value2"].I, num * this.getSeidJson(seid)["value3"].I * buffInfo[1]);
				}
			}
		}

		// Token: 0x06005900 RID: 22784 RVA: 0x00252ABC File Offset: 0x00250CBC
		public void ListRealizeSeid40(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			this.BuffSeidFlagAddNum(seid, 1, avatar);
			if (avatar.BuffSeidFlag[seid][this.buffID] >= (int)this.getSeidJson(seid)["value1"].n)
			{
				this.SeidAddBuff(seid, avatar, buffInfo);
				if (avatar.BuffSeidFlag[seid][this.buffID] >= (int)this.getSeidJson(seid)["value1"].n)
				{
					this.ReloadSelf(seid, avatar, buffInfo, 0);
				}
			}
		}

		// Token: 0x06005901 RID: 22785 RVA: 0x00252B48 File Offset: 0x00250D48
		public void ListRealizeSeid41(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int skillIDByKey = Tools.instance.getSkillIDByKey(flag[1]);
			foreach (JSONObject jsonobject in this.getSeidJson(seid)["value4"].list)
			{
				if (skillIDByKey == jsonobject.I)
				{
					this.BuffSeidFlagAddNum(seid, 1, avatar);
					if (avatar.BuffSeidFlag[seid][this.buffID] >= (int)this.getSeidJson(seid)["value1"].n)
					{
						this.SeidAddBuff(seid, avatar, buffInfo);
						if (avatar.BuffSeidFlag[seid][this.buffID] >= (int)this.getSeidJson(seid)["value1"].n)
						{
							this.ReloadSelf(seid, avatar, buffInfo, 0);
						}
					}
				}
			}
		}

		// Token: 0x06005902 RID: 22786 RVA: 0x00252C44 File Offset: 0x00250E44
		public void ListRealizeSeid42(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int num = (int)this.getSeidJson(seid)["value4"].n;
			bool flag2 = false;
			foreach (JSONObject jsonobject in jsonData.instance.skillJsonData[string.Concat(flag[1])]["AttackType"].list)
			{
				if (num == (int)jsonobject.n)
				{
					flag2 = true;
				}
			}
			if (flag2)
			{
				this.BuffSeidFlagAddNum(seid, 1, avatar);
				if (avatar.BuffSeidFlag[seid][this.buffID] >= (int)this.getSeidJson(seid)["value1"].n)
				{
					this.SeidAddBuff(seid, avatar, buffInfo);
					if (avatar.BuffSeidFlag[seid][this.buffID] >= (int)this.getSeidJson(seid)["value1"].n)
					{
						this.ReloadSelf(seid, avatar, buffInfo, 0);
					}
				}
			}
		}

		// Token: 0x06005903 RID: 22787 RVA: 0x00252D60 File Offset: 0x00250F60
		public void ListRealizeSeid43(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			this.BuffSeidFlagAddNum(seid, 1, avatar);
			if (avatar.BuffSeidFlag[seid][this.buffID] >= (int)this.getSeidJson(seid)["value1"].n)
			{
				this.SeidAddBuff(seid, avatar, buffInfo);
				if (avatar.BuffSeidFlag[seid][this.buffID] >= (int)this.getSeidJson(seid)["value1"].n)
				{
					this.ReloadSelf(seid, avatar, buffInfo, 0);
				}
			}
		}

		// Token: 0x06005904 RID: 22788 RVA: 0x00252DEC File Offset: 0x00250FEC
		public void ListRealizeSeid44(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int num = flag[1];
			bool flag2 = false;
			foreach (JSONObject jsonobject in jsonData.instance.skillJsonData[string.Concat(num)]["AttackType"].list)
			{
				if ((int)this.getSeidJson(seid)["value4"].n == (int)jsonobject.n)
				{
					flag2 = true;
				}
			}
			if (flag2)
			{
				this.BuffSeidFlagAddNum(seid, 1, avatar);
				if (avatar.BuffSeidFlag[seid][this.buffID] >= (int)this.getSeidJson(seid)["value1"].n)
				{
					this.SeidAddBuff(seid, avatar, buffInfo);
					if (avatar.BuffSeidFlag[seid][this.buffID] >= (int)this.getSeidJson(seid)["value1"].n)
					{
						this.ReloadSelf(seid, avatar, buffInfo, 0);
					}
				}
			}
		}

		// Token: 0x06005905 RID: 22789 RVA: 0x00252F08 File Offset: 0x00251108
		public void ListRealizeSeid46(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			if (avatar.OtherAvatar.buffmag.HasBuffSeid(107))
			{
				return;
			}
			if (buffInfo[1] > 0)
			{
				buffInfo[1] = buffInfo[1] - 1;
				flag[0] = 0;
				((GameObject)avatar.renderObj).GetComponentInChildren<AvatarShowHpDamage>().show("闪避");
			}
			avatar.spell.onBuffTickByType(35, new List<int>
			{
				0
			});
		}

		// Token: 0x06005906 RID: 22790 RVA: 0x00252F84 File Offset: 0x00251184
		public void ListRealizeSeid47(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			if (RoundManager.instance.getListSum(avatar.crystal) <= 0)
			{
				for (int i = 0; i < (int)(this.getSeidJson(seid)["value1"].n * (float)buffInfo[1]); i++)
				{
					this.SeidAddCard(avatar, flag);
				}
			}
		}

		// Token: 0x06005907 RID: 22791 RVA: 0x00252FD8 File Offset: 0x002511D8
		public void ListRealizeSeid48(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int i = this.getSeidJson(seid)["value1"].I;
			if (buffInfo[1] >= i)
			{
				int num = buffInfo[1] / i;
				int i2 = this.getSeidJson(seid)["value2"].I;
				int i3 = this.getSeidJson(seid)["value3"].I;
				avatar.spell.addBuff(i2, i3 * num);
				buffInfo[1] = 0;
			}
		}

		// Token: 0x06005908 RID: 22792 RVA: 0x00253058 File Offset: 0x00251258
		public void ListRealizeSeid50(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int key = flag[1];
			bool flag2 = false;
			List<int> attackType = _skillJsonData.DataDict[key].AttackType;
			int i = this.getSeidJson(seid)["value1"].I;
			foreach (int num in attackType)
			{
				if (i == num)
				{
					flag2 = true;
					break;
				}
			}
			if (flag2)
			{
				int i2 = this.getSeidJson(seid)["value2"].I;
				int num2 = buffInfo[1];
				flag[0] = flag[0] + i2 * num2;
			}
		}

		// Token: 0x06005909 RID: 22793 RVA: 0x00253118 File Offset: 0x00251318
		public void ListRealizeSeid51(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			for (int i = 0; i < (int)this.getSeidJson(seid)["value1"].n; i++)
			{
				avatar.recvDamage(avatar, avatar.OtherAvatar, 10006, (int)this.getSeidJson(seid)["value2"].n, 0);
			}
		}

		// Token: 0x0600590A RID: 22794 RVA: 0x00253174 File Offset: 0x00251374
		public void ListRealizeSeid52(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			this.BuffSeidFlagAddNum(seid, 1, avatar);
			if (avatar.BuffSeidFlag[seid][this.buffID] >= (int)this.getSeidJson(seid)["value1"].n)
			{
				this.SeidAddBuff(seid, avatar, buffInfo);
				if (avatar.BuffSeidFlag[seid][this.buffID] >= (int)this.getSeidJson(seid)["value1"].n)
				{
					this.ReloadSelf(seid, avatar, buffInfo, 0);
				}
			}
		}

		// Token: 0x0600590B RID: 22795 RVA: 0x00253200 File Offset: 0x00251400
		public void ListRealizeSeid53(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			List<List<int>> buffByID = avatar.OtherAvatar.buffmag.getBuffByID(this.getSeidJson(seid)["value2"].I);
			if (buffByID.Count < 1)
			{
				return;
			}
			if (buffByID[0][1] >= this.getSeidJson(seid)["value1"].I)
			{
				int num = buffByID[0][1] / this.getSeidJson(seid)["value1"].I;
				avatar.spell.addBuff(this.getSeidJson(seid)["value4"].I, num * this.getSeidJson(seid)["value3"].I);
			}
		}

		// Token: 0x0600590C RID: 22796 RVA: 0x002532C4 File Offset: 0x002514C4
		public void ListRealizeSeid54(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			List<List<int>> buffByID = avatar.buffmag.getBuffByID(this.getSeidJson(seid)["value2"].I);
			if (buffByID.Count < 1)
			{
				return;
			}
			if (buffByID[0][1] >= this.getSeidJson(seid)["value1"].I)
			{
				int num = buffByID[0][1] / this.getSeidJson(seid)["value1"].I;
				avatar.spell.addBuff(this.getSeidJson(seid)["value4"].I, num * this.getSeidJson(seid)["value3"].I);
			}
		}

		// Token: 0x0600590D RID: 22797 RVA: 0x00253381 File Offset: 0x00251581
		public void ListRealizeSeid55(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			avatar.fightTemp.tempShenShi[this.buffID] = (int)this.getSeidJson(seid)["value1"].n * buffInfo[1];
		}

		// Token: 0x0600590E RID: 22798 RVA: 0x002533B8 File Offset: 0x002515B8
		public void onDetachRealizeSeid55(int seid, Avatar avatar, List<int> buffInfo)
		{
			avatar.fightTemp.tempShenShi[this.buffID] = 0;
		}

		// Token: 0x0600590F RID: 22799 RVA: 0x002533D1 File Offset: 0x002515D1
		public void ListRealizeSeid56(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			avatar.fightTemp.TempHP_Max[this.buffID] = (int)this.getSeidJson(seid)["value1"].n * buffInfo[1];
		}

		// Token: 0x06005910 RID: 22800 RVA: 0x00253408 File Offset: 0x00251608
		public void onDetachRealizeSeid56(int seid, Avatar avatar, List<int> buffInfo)
		{
			avatar.fightTemp.TempHP_Max[this.buffID] = 0;
		}

		// Token: 0x06005911 RID: 22801 RVA: 0x00253424 File Offset: 0x00251624
		public void ListRealizeSeid57(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int num = flag[1];
			if (jsonData.instance.skillJsonData[num.ToString()]["AttackType"].list.Find((JSONObject aa) => (int)aa.n == (int)this.getSeidJson(seid)["value1"].n) != null)
			{
				flag[0] = flag[0] + (int)((float)flag[0] * (this.getSeidJson(seid)["value2"].n * (float)buffInfo[1] / 100f));
			}
		}

		// Token: 0x06005912 RID: 22802 RVA: 0x00004095 File Offset: 0x00002295
		public void ListRealizeSeid58(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
		}

		// Token: 0x06005913 RID: 22803 RVA: 0x00004095 File Offset: 0x00002295
		public void ListRealizeSeid59(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
		}

		// Token: 0x06005914 RID: 22804 RVA: 0x002534D0 File Offset: 0x002516D0
		public void ListRealizeSeid60(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			flag[0] = 0;
			RoundManager.instance.creatAvatar(11, 50, 100, new Vector3(5f, -1.7f, -1f), new Vector3(0f, 0f, -90f));
			Avatar avatar2 = (Avatar)KBEngineApp.app.entities[11];
			avatar2.equipSkillList = new List<SkillItem>();
			avatar2.equipStaticSkillList = new List<SkillItem>();
			avatar2.LingGeng = new List<int>();
			RoundManager.instance.initMonstar((int)this.getSeidJson(seid)["value1"].n);
			KBEngineApp.app.entity_id = 10;
			Avatar avatar3 = (Avatar)KBEngineApp.app.entities[10];
			avatar3.OtherAvatar = avatar2;
			avatar2.OtherAvatar = avatar3;
			World.instance.onLeaveWorld(avatar);
			avatar2.skill = new List<Skill>();
			RoundManager.instance.initAvatarInfo(avatar2);
			GameObject gameObject = GameObject.Find("Canvas_target");
			RoundManager.instance.initUI_Target(gameObject.GetComponent<UI_Target>(), avatar2);
			avatar = avatar2;
			Event.fireOut("UpdataBuff", Array.Empty<object>());
			RoundManager.instance.endRound(avatar);
		}

		// Token: 0x06005915 RID: 22805 RVA: 0x00253602 File Offset: 0x00251802
		public void ListRealizeSeid61(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			avatar.fightTemp.tempDunSu[this.buffID] = (int)this.getSeidJson(seid)["value1"].n * buffInfo[1];
		}

		// Token: 0x06005916 RID: 22806 RVA: 0x00253639 File Offset: 0x00251839
		public void onDetachRealizeSeid61(int seid, Avatar avatar, List<int> buffInfo)
		{
			avatar.fightTemp.tempDunSu[this.buffID] = 0;
		}

		// Token: 0x06005917 RID: 22807 RVA: 0x00004095 File Offset: 0x00002295
		public void ListRealizeSeid62(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
		}

		// Token: 0x06005918 RID: 22808 RVA: 0x00253652 File Offset: 0x00251852
		public void ListRealizeSeid63(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			RoundManager.instance.removeCard(avatar, this.getSeidJson(seid)["value1"].I * buffInfo[1]);
		}

		// Token: 0x06005919 RID: 22809 RVA: 0x00253680 File Offset: 0x00251880
		public void ListRealizeSeid64(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int i = this.getSeidJson(seid)["value1"].I;
			int i2 = this.getSeidJson(seid)["value2"].I;
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
			Dictionary<int, int> dictionary = avatar.SkillSeidFlag[13];
			int key = i;
			dictionary[key] += i2 * num;
		}

		// Token: 0x0600591A RID: 22810 RVA: 0x00004095 File Offset: 0x00002295
		public void ListRealizeSeid65(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
		}

		// Token: 0x0600591B RID: 22811 RVA: 0x00253748 File Offset: 0x00251948
		public void ListRealizeSeid66(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			List<List<int>> buffByID = avatar.OtherAvatar.buffmag.getBuffByID((int)this.getSeidJson(seid)["value1"].n);
			if (buffByID.Count > 0)
			{
				avatar.recvDamage(avatar, avatar.OtherAvatar, 10006, (int)this.getSeidJson(seid)["value2"].n * buffByID[0][1] * buffInfo[1], 0);
			}
		}

		// Token: 0x0600591C RID: 22812 RVA: 0x002537C8 File Offset: 0x002519C8
		public void ListRealizeSeid67(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int i = this.getSeidJson(seid)["value1"].I;
			int i2 = this.getSeidJson(seid)["value2"].I;
			int i3 = this.getSeidJson(seid)["value3"].I;
			int i4 = this.getSeidJson(seid)["value4"].I;
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

		// Token: 0x0600591D RID: 22813 RVA: 0x00253886 File Offset: 0x00251A86
		public void ListRealizeSeid68(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			if (!avatar.buffmag.HasBuffSeid(23))
			{
				avatar.fightTemp.tempNowCard -= (int)this.getSeidJson(seid)["value1"].n;
			}
		}

		// Token: 0x0600591E RID: 22814 RVA: 0x002538C0 File Offset: 0x00251AC0
		public void onDetachRealizeSeid68(int seid, Avatar avatar, List<int> buffInfo)
		{
			if (!avatar.buffmag.HasBuffSeid(23))
			{
				avatar.fightTemp.tempNowCard += (int)this.getSeidJson(seid)["value1"].n * buffInfo[1];
			}
		}

		// Token: 0x0600591F RID: 22815 RVA: 0x00253910 File Offset: 0x00251B10
		public void ListRealizeSeid69(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int num = flag[1];
			int num2 = 0;
			foreach (JSONObject jsonobject in jsonData.instance.skillJsonData[num.ToString()]["skill_Cast"].list)
			{
				num2 += (int)jsonobject.n;
			}
			foreach (JSONObject jsonobject2 in jsonData.instance.skillJsonData[num.ToString()]["skill_SameCastNum"].list)
			{
				num2 += (int)jsonobject2.n;
			}
			if (num2 >= (int)this.getSeidJson(seid)["value1"].n)
			{
				for (int i = 0; i < (int)this.getSeidJson(seid)["value2"].n * buffInfo[1]; i++)
				{
					RoundManager.instance.DrawCard(avatar);
				}
			}
		}

		// Token: 0x06005920 RID: 22816 RVA: 0x00253A4C File Offset: 0x00251C4C
		public void ListRealizeSeid70(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			using (List<JSONObject>.Enumerator enumerator = this.getSeidJson(seid)["value1"].list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.n == (float)Tools.instance.MonstarID && flag[0] > 0)
					{
						flag[0] = flag[0] - (int)this.getSeidJson(seid)["value2"].n;
					}
				}
			}
		}

		// Token: 0x06005921 RID: 22817 RVA: 0x00253AF0 File Offset: 0x00251CF0
		public void ListRealizeSeid71(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			avatar.setHP(avatar.HP - (int)this.getSeidJson(seid)["value1"].n * buffInfo[1]);
		}

		// Token: 0x06005922 RID: 22818 RVA: 0x00253B20 File Offset: 0x00251D20
		public void ListRealizeSeid72(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int num = flag[1];
			int CheckskillID = (int)this.getSeidJson(seid)["value1"].n;
			if (flag[0] > 0 && jsonData.instance.skillJsonData[string.Concat(num)]["AttackType"].list.FindAll((JSONObject aa) => (int)aa.n == CheckskillID).Count > 0)
			{
				flag[0] = Mathf.Clamp(flag[0] - (int)this.getSeidJson(seid)["value2"].n * buffInfo[1], 0, 99999999);
			}
		}

		// Token: 0x06005923 RID: 22819 RVA: 0x00004095 File Offset: 0x00002295
		public void ListRealizeSeid73(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
		}

		// Token: 0x06005924 RID: 22820 RVA: 0x00253BE1 File Offset: 0x00251DE1
		public void ListRealizeSeid78(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			flag[3] = 1;
		}

		// Token: 0x06005925 RID: 22821 RVA: 0x00253BEC File Offset: 0x00251DEC
		public void ListRealizeSeid79(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int cardNum = avatar.OtherAvatar.crystal.getCardNum();
			int num = (int)this.getSeidJson(seid)["value1"].n * buffInfo[1];
			int num2 = Mathf.Min(cardNum, num);
			int num3 = 0;
			int[] array = new int[6];
			foreach (card card in avatar.OtherAvatar.crystal._cardlist)
			{
				if (num3 < num2)
				{
					array[card.cardType]++;
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

		// Token: 0x06005926 RID: 22822 RVA: 0x00253CD8 File Offset: 0x00251ED8
		public void ListRealizeSeid80(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int num = flag[1];
			if (jsonData.instance.skillJsonData[num.ToString()]["AttackType"].list.Find((JSONObject cc) => (int)cc.n == (int)this.getSeidJson(seid)["value1"].n) != null)
			{
				foreach (List<int> list in avatar.buffmag.getBuffByID(this.getSeidJson(seid)["value2"].I))
				{
					flag[0] = flag[0] + this.getSeidJson(seid)["value3"].I * list[1] * buffInfo[1];
				}
			}
		}

		// Token: 0x06005927 RID: 22823 RVA: 0x00004095 File Offset: 0x00002295
		public void ListRealizeSeid81(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
		}

		// Token: 0x06005928 RID: 22824 RVA: 0x00253DDC File Offset: 0x00251FDC
		public void ListRealizeSeid82(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			for (int i = 0; i < (int)this.getSeidJson(seid)["value1"].n * buffInfo[1]; i++)
			{
				foreach (List<int> item in avatar.buffmag.getBuffByID((int)this.getSeidJson(seid)["value2"].n))
				{
					avatar.spell.onBuffTick(avatar.bufflist.IndexOf(item), null, 0);
				}
			}
		}

		// Token: 0x06005929 RID: 22825 RVA: 0x00004095 File Offset: 0x00002295
		public void ListRealizeSeid83(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
		}

		// Token: 0x0600592A RID: 22826 RVA: 0x00253E8C File Offset: 0x0025208C
		public void ListRealizeSeid84(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			if (flag[0] < (int)this.getSeidJson(seid)["value1"].n)
			{
				flag[0] = 0;
			}
		}

		// Token: 0x0600592B RID: 22827 RVA: 0x00253EB8 File Offset: 0x002520B8
		public void ListRealizeSeid87(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int num = (int)this.getSeidJson(seid)["value2"].n * buffInfo[1];
			for (int i = 0; i < num; i++)
			{
				avatar.spell.addDBuff((int)this.getSeidJson(seid)["value1"].n);
			}
		}

		// Token: 0x0600592C RID: 22828 RVA: 0x00253F14 File Offset: 0x00252114
		public void ListRealizeSeid88(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			for (int i = 0; i < (int)(this.getSeidJson(seid)["value2"].n * (float)buffInfo[1]); i++)
			{
				avatar.OtherAvatar.spell.addDBuff((int)this.getSeidJson(seid)["value1"].n);
			}
		}

		// Token: 0x0600592D RID: 22829 RVA: 0x00253F74 File Offset: 0x00252174
		public void ListRealizeSeid89(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			if (avatar.buffmag.HasBuff((int)this.getSeidJson(seid)["value2"].n))
			{
				foreach (List<int> list in avatar.buffmag.getBuffByID((int)this.getSeidJson(seid)["value2"].n))
				{
					list[1] = list[1] - (int)this.getSeidJson(seid)["value1"].n * buffInfo[1];
				}
			}
		}

		// Token: 0x0600592E RID: 22830 RVA: 0x00004095 File Offset: 0x00002295
		public void ListRealizeSeid90(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
		}

		// Token: 0x0600592F RID: 22831 RVA: 0x00254030 File Offset: 0x00252230
		public void ListRealizeSeid92(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			List<List<int>> buffByID = avatar.buffmag.getBuffByID((int)this.getSeidJson(seid)["value1"].n);
			if (buffByID.Count > 0)
			{
				buffByID[0][1] = (int)this.getSeidJson(seid)["value2"].n;
			}
			Event.fireOut("UpdataBuff", Array.Empty<object>());
		}

		// Token: 0x06005930 RID: 22832 RVA: 0x0025409C File Offset: 0x0025229C
		public void ListRealizeSeid93(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int damage = -(int)((float)avatar.HP_Max * this.getSeidJson(seid)["value1"].n / 100f * (float)buffInfo[1]);
			avatar.recvDamage(avatar, avatar, 10006, damage, 0);
		}

		// Token: 0x06005931 RID: 22833 RVA: 0x002540EC File Offset: 0x002522EC
		public void ListRealizeSeid94(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			float num = (float)(avatar.HP_Max - avatar.HP);
			int i = this.getSeidJson(seid)["value1"].I;
			int num2 = -(int)(num * (float)i / 100f * (float)buffInfo[1]);
			avatar.recvDamage(avatar, avatar, 10006, num2, 0);
			if (avatar.OtherAvatar.buffmag.HasBuffSeid(310) && i < 0)
			{
				avatar.OtherAvatar.recvDamage(avatar.OtherAvatar, avatar.OtherAvatar, 10006, -num2, 0);
			}
		}

		// Token: 0x06005932 RID: 22834 RVA: 0x00254180 File Offset: 0x00252380
		public void ListRealizeSeid98(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int damage = (int)this.getSeidJson(seid)["value1"].n * buffInfo[1];
			avatar.recvDamage(avatar, avatar, 10001 + (int)this.getSeidJson(seid)["value2"].n, damage, 0);
		}

		// Token: 0x06005933 RID: 22835 RVA: 0x00253BE1 File Offset: 0x00251DE1
		public void ListRealizeSeid100(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			flag[3] = 1;
		}

		// Token: 0x06005934 RID: 22836 RVA: 0x002541D8 File Offset: 0x002523D8
		public void ListRealizeSeid104(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int i = this.getSeidJson(seid)["value1"].I;
			int i2 = this.getSeidJson(seid)["value2"].I;
			for (int j = 0; j < i; j++)
			{
				card randomCard = avatar.OtherAvatar.cardMag.getRandomCard();
				if (randomCard != null)
				{
					int cardType = randomCard.cardType;
					card card = avatar.OtherAvatar.cardMag.ChengCard(randomCard.cardType, i2);
					if (avatar.OtherAvatar.isPlayer() && card != null)
					{
						Debug.Log(string.Format("将{0}污染为{1}", (LingQiType)cardType, (LingQiType)i2));
						UIFightLingQiPlayerSlot uifightLingQiPlayerSlot = UIFightPanel.Inst.PlayerLingQiController.SlotList[cardType];
						int lingQiCount = uifightLingQiPlayerSlot.LingQiCount;
						uifightLingQiPlayerSlot.LingQiCount = lingQiCount - 1;
						UIFightLingQiPlayerSlot uifightLingQiPlayerSlot2 = UIFightPanel.Inst.PlayerLingQiController.SlotList[i2];
						lingQiCount = uifightLingQiPlayerSlot2.LingQiCount;
						uifightLingQiPlayerSlot2.LingQiCount = lingQiCount + 1;
					}
				}
			}
		}

		// Token: 0x06005935 RID: 22837 RVA: 0x002542D8 File Offset: 0x002524D8
		public void ListRealizeSeid105(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			if (avatar.buffmag.HasBuffSeid(58))
			{
				List<List<int>> buffBySeid = avatar.buffmag.getBuffBySeid(58);
				avatar.spell.onBuffTick(avatar.buffmag.getBuffIndex(buffBySeid[0]), null, 0);
			}
			int num = avatar.cardMag[(int)this.getSeidJson(seid)["value2"].n];
			avatar.recvDamage(avatar, avatar, 10006, (int)this.getSeidJson(seid)["value1"].n * num * buffInfo[1], 0);
		}

		// Token: 0x06005936 RID: 22838 RVA: 0x00254374 File Offset: 0x00252574
		public void onAttachRealizeSeid108(int seid, Avatar avatar, List<int> buffInfo)
		{
			int endIndex = (JieDanManager.instence == null) ? 10 : 6;
			avatar.FightClearSkill(0, endIndex);
		}

		// Token: 0x06005937 RID: 22839 RVA: 0x0025439C File Offset: 0x0025259C
		public void onAttachRealizeSeid109(int seid, Avatar avatar, List<int> buffInfo)
		{
			int endIndex = (JieDanManager.instence == null) ? 10 : 6;
			foreach (JSONObject jsonobject in this.getSeidJson(seid)["value1"].list)
			{
				avatar.FightAddSkill((int)jsonobject.n, 0, endIndex);
			}
		}

		// Token: 0x06005938 RID: 22840 RVA: 0x0025441C File Offset: 0x0025261C
		public void ListRealizeSeid111(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			foreach (JSONObject jsonobject in this.getSeidJson(seid)["value1"].list)
			{
				foreach (List<int> buffid in avatar.buffmag.getBuffByID((int)jsonobject.n))
				{
					avatar.spell.removeBuff(buffid);
				}
			}
		}

		// Token: 0x06005939 RID: 22841 RVA: 0x002544CC File Offset: 0x002526CC
		public void onDetachRealizeSeid112(int seid, Avatar avatar, List<int> buffInfo)
		{
			if (avatar.buffmag.HasBuffSeid(58))
			{
				List<List<int>> buffBySeid = avatar.buffmag.getBuffBySeid(58);
				avatar.spell.onBuffTick(avatar.buffmag.getBuffIndex(buffBySeid[0]), null, 0);
			}
			avatar.recvDamage(avatar, avatar, 10006, (int)this.getSeidJson(seid)["value1"].n, 0);
		}

		// Token: 0x0600593A RID: 22842 RVA: 0x0025453B File Offset: 0x0025273B
		public void onDetachRealizeSeid114(int seid, Avatar avatar, List<int> buffInfo)
		{
			avatar.setHP(-1);
		}

		// Token: 0x0600593B RID: 22843 RVA: 0x00254544 File Offset: 0x00252744
		public void ListRealizeSeid115(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			flag[0] = flag[0] + flag[0] * (int)this.getSeidJson(seid)["value1"].n * buffInfo[1] / 100;
		}

		// Token: 0x0600593C RID: 22844 RVA: 0x00254584 File Offset: 0x00252784
		public void ListRealizeSeid117(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			avatar.spell.addDBuff(this.getSeidJson(seid)["value1"].I, this.getSeidJson(seid)["value2"].I * RoundManager.instance.NowSkillUsedLingQiSum * buffInfo[1]);
		}

		// Token: 0x0600593D RID: 22845 RVA: 0x002545DC File Offset: 0x002527DC
		public void ListRealizeSeid123(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			UIFightPanel.Inst.FightSelectLingQi.gameObject.SetActive(true);
			UIFightPanel.Inst.FightSelectLingQi.SetSelectAction(delegate(LingQiType lq)
			{
				RoundManager.instance.DrawCard(avatar, (int)lq);
			});
		}

		// Token: 0x0600593E RID: 22846 RVA: 0x00004095 File Offset: 0x00002295
		public void ListRealizeSeid124(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
		}

		// Token: 0x0600593F RID: 22847 RVA: 0x00004095 File Offset: 0x00002295
		public void ListRealizeSeid125(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
		}

		// Token: 0x06005940 RID: 22848 RVA: 0x00254628 File Offset: 0x00252828
		public void ListRealizeSeid127(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			for (int i = 0; i < this.getSeidJson(seid)["value1"].list.Count; i++)
			{
				avatar.spell.addDBuff((int)this.getSeidJson(seid)["value1"][i].n, (int)this.getSeidJson(seid)["value2"][i].n);
			}
		}

		// Token: 0x06005941 RID: 22849 RVA: 0x002546A0 File Offset: 0x002528A0
		public void ListRealizeSeid128(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			List<JSONObject> list = this.getSeidJson(seid)["value2"].list;
			int i = this.getSeidJson(seid)["value1"].I;
			bool flag2 = true;
			foreach (JSONObject jsonobject in list)
			{
				if (!jsonData.instance.skillJsonData[string.Concat(flag[1])]["AttackType"].HasItem(jsonobject.I))
				{
					flag2 = false;
				}
			}
			this.BuffSeidFlagAddNum(seid, 0, avatar);
			if (flag2)
			{
				if (avatar.BuffSeidFlag[seid][this.buffID] >= i)
				{
					Dictionary<int, int> dictionary = avatar.BuffSeidFlag[seid];
					int key = this.buffID;
					dictionary[key] -= (int)this.getSeidJson(seid)["value1"].n;
				}
				this.BuffSeidFlagAddNum(seid, 1, avatar);
			}
		}

		// Token: 0x06005942 RID: 22850 RVA: 0x002547C0 File Offset: 0x002529C0
		public void ListRealizeSeid130(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			List<JSONObject> list = this.getSeidJson(seid)["value2"].list;
			List<JSONObject> list2 = this.getSeidJson(seid)["value3"].list;
			List<int> flag1 = new List<int>();
			flag.ForEach(delegate(int aa)
			{
				flag1.Add(aa);
			});
			if (this.CanRealizeSeid(avatar, flag1, this.getSeidJson(seid)["value1"].I, null, buffInfo))
			{
				using (List<JSONObject>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						JSONObject jsonobject = enumerator.Current;
						this.loopRealizeSeid(jsonobject.I, avatar, buffInfo, flag);
						if (!this.CanRealizeSeid(avatar, flag, jsonobject.I, null, buffInfo))
						{
							break;
						}
					}
					return;
				}
			}
			foreach (JSONObject jsonobject2 in list2)
			{
				this.loopRealizeSeid(jsonobject2.I, avatar, buffInfo, flag);
				if (!this.CanRealizeSeid(avatar, flag, jsonobject2.I, null, buffInfo))
				{
					break;
				}
			}
		}

		// Token: 0x06005943 RID: 22851 RVA: 0x00254908 File Offset: 0x00252B08
		public void ListRealizeSeid131(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			this.ListRealizeSeid130(seid, avatar, buffInfo, flag);
		}

		// Token: 0x06005944 RID: 22852 RVA: 0x00254908 File Offset: 0x00252B08
		public void ListRealizeSeid132(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			this.ListRealizeSeid130(seid, avatar, buffInfo, flag);
		}

		// Token: 0x06005945 RID: 22853 RVA: 0x00254918 File Offset: 0x00252B18
		public void ListRealizeSeid133(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			bool flag2 = false;
			foreach (JSONObject jsonobject in jsonData.instance.skillJsonData[string.Concat(flag[1])]["AttackType"].list)
			{
				if ((int)this.getSeidJson(seid)["value1"].n == (int)jsonobject.n)
				{
					flag2 = true;
				}
			}
			if (flag2)
			{
				int num = this.getTargetAvatar(seid, avatar).crystal[(int)this.getSeidJson(seid)["value3"].n];
				flag[0] = flag[0] - num;
				flag[0] = Mathf.Max(0, flag[0]);
			}
		}

		// Token: 0x06005946 RID: 22854 RVA: 0x00254A0C File Offset: 0x00252C0C
		public void ListRealizeSeid134(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			List<List<int>> allBuffByType = this.getTargetAvatar(seid, avatar).buffmag.getAllBuffByType((int)this.getSeidJson(seid)["value2"].n);
			if (allBuffByType.Count > 0)
			{
				int index = jsonData.GetRandom() % allBuffByType.Count;
				List<int> list = allBuffByType[index];
				list[1] = list[1] - (int)this.getSeidJson(seid)["value1"].n;
				if (allBuffByType[index][1] < 0)
				{
					allBuffByType[index][1] = 0;
				}
			}
		}

		// Token: 0x06005947 RID: 22855 RVA: 0x00254AA8 File Offset: 0x00252CA8
		public void ListRealizeSeid135(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int i = this.getSeidJson(seid)["value2"].I;
			List<JSONObject> list = new List<JSONObject>();
			foreach (KeyValuePair<string, JSONObject> keyValuePair in jsonData.instance.BuffJsonData)
			{
				if (keyValuePair.Value["bufftype"].I == i)
				{
					list.Add(keyValuePair.Value);
				}
			}
			JSONObject jsonobject = list[jsonData.GetRandom() % list.Count];
			avatar.spell.addDBuff(jsonobject["buffid"].I, this.getSeidJson(seid)["value1"].I * buffInfo[1]);
		}

		// Token: 0x06005948 RID: 22856 RVA: 0x00254B88 File Offset: 0x00252D88
		public void ListRealizeSeid137(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			if (flag[0] < 0)
			{
				flag[0] = flag[0] + (int)Math.Ceiling(Convert.ToDouble((float)flag[0] * this.getSeidJson(seid)["value1"].n));
			}
		}

		// Token: 0x06005949 RID: 22857 RVA: 0x00254BDC File Offset: 0x00252DDC
		public void ListRealizeSeid138(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			Avatar targetAvatar = this.getTargetAvatar(seid, avatar);
			int i = this.getSeidJson(seid)["value1"].I;
			int i2 = this.getSeidJson(seid)["value2"].I;
			int num = buffInfo[1];
			List<List<int>> allBuffByType = targetAvatar.buffmag.GetAllBuffByType(i);
			if (allBuffByType.Count > 0)
			{
				foreach (List<int> list in allBuffByType)
				{
					int num2 = list[1] - i2 * num;
					num2 = Mathf.Max(0, num2);
					list[1] = num2;
				}
			}
		}

		// Token: 0x0600594A RID: 22858 RVA: 0x00254C98 File Offset: 0x00252E98
		public void ListRealizeSeid139(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			flag[4] = this.getSeidJson(seid)["value2"].I;
			List<int> list = _skillJsonData.DataDict[flag[1]].seid;
			if (list.Contains(139) && flag[4] >= 2 && RoundManager.instance != null && RoundManager.instance.CurSkill != null)
			{
				Skill curSkill = RoundManager.instance.CurSkill;
				List<int> list2 = new List<int>();
				bool flag2 = false;
				foreach (int num in list)
				{
					if (flag2)
					{
						list2.Add(num);
					}
					if (num == 139)
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
					foreach (int num2 in list2)
					{
						curSkill.realizeSeid(num2, flag, curSkill.attack, curSkill.target, curSkill.type);
						curSkill.realizeBuffEndSeid(num2, flag, curSkill.attack, curSkill.target, curSkill.type);
						curSkill.realizeFinalSeid(num2, flag, curSkill.attack, curSkill.target, curSkill.type);
					}
				}
			}
		}

		// Token: 0x0600594B RID: 22859 RVA: 0x00254E38 File Offset: 0x00253038
		public void ListRealizeSeid141(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			if (flag[0] > 0)
			{
				flag[0] = flag[0] + (int)Math.Ceiling(Convert.ToDouble((float)flag[0] * this.getSeidJson(seid)["value1"].n));
				flag[0] = Mathf.Max(0, flag[0]);
			}
		}

		// Token: 0x0600594C RID: 22860 RVA: 0x00254EA4 File Offset: 0x002530A4
		public void ListRealizeSeid142(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			List<List<int>> buffByID = this.getTargetAvatar(seid, avatar).buffmag.getBuffByID((int)this.getSeidJson(seid)["value1"].n);
			if (buffByID.Count > 0)
			{
				foreach (List<int> list in buffByID)
				{
					list[1] = list[1] + list[1] * (int)this.getSeidJson(seid)["value2"].n;
				}
			}
		}

		// Token: 0x0600594D RID: 22861 RVA: 0x00254F4C File Offset: 0x0025314C
		public void ListRealizeSeid146(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int num = flag[0];
			avatar.recvDamage(avatar, avatar, 10006, -num, 0);
		}

		// Token: 0x0600594E RID: 22862 RVA: 0x00254F74 File Offset: 0x00253174
		public void ListRealizeSeid147(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			List<List<int>> buffByID = avatar.buffmag.getBuffByID((int)this.getSeidJson(seid)["value1"].n);
			if (buffByID.Count > 0)
			{
				avatar.recvDamage(avatar, avatar, 10006, -buffByID[0][1], 0);
			}
		}

		// Token: 0x0600594F RID: 22863 RVA: 0x00254FCC File Offset: 0x002531CC
		public void ListRealizeSeid148(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			bool flag2 = false;
			foreach (JSONObject jsonobject in jsonData.instance.skillJsonData[string.Concat(flag[1])]["AttackType"].list)
			{
				if ((int)this.getSeidJson(seid)["value1"].n == (int)jsonobject.n)
				{
					flag2 = true;
				}
			}
			if (flag2)
			{
				flag[0] = 0;
			}
		}

		// Token: 0x06005950 RID: 22864 RVA: 0x00255074 File Offset: 0x00253274
		public void ListRealizeSeid149(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int num = flag[0];
			for (int i = 0; i < this.getSeidJson(seid)["value1"].Count; i++)
			{
				avatar.spell.addDBuff(this.getSeidJson(seid)["value1"][i].I, buffInfo[1] * num);
			}
		}

		// Token: 0x06005951 RID: 22865 RVA: 0x002550DC File Offset: 0x002532DC
		public void ListRealizeSeid171(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			Buff._NeiShangLoopCount++;
			if (Buff._NeiShangLoopCount > 20)
			{
				Debug.Log("内伤循环达到20次，退出循环");
				Buff._NeiShangLoopCount = 0;
				return;
			}
			int addNum = flag[0];
			this.BuffSeidFlagAddNum(seid, addNum, avatar);
			int num = avatar.BuffSeidFlag[seid][this.buffID];
			int i = this.getSeidJson(seid)["value1"].I;
			int i2 = this.getSeidJson(seid)["value2"].I;
			int num2 = num / i;
			if (num2 > 0)
			{
				Dictionary<int, int> dictionary = avatar.BuffSeidFlag[seid];
				int key = this.buffID;
				dictionary[key] -= num2 * i;
				avatar.spell.addBuff(i2, num2);
				if (avatar.state == 1 || avatar.OtherAvatar.state == 1)
				{
					return;
				}
				if (avatar.BuffSeidFlag[seid][this.buffID] >= i)
				{
					this.ReloadSelf(seid, avatar, buffInfo, 0);
				}
			}
		}

		// Token: 0x06005952 RID: 22866 RVA: 0x002551E4 File Offset: 0x002533E4
		public void ListRealizeSeid179(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int listSum = RoundManager.instance.getListSum(avatar.crystal);
			avatar.recvDamage(avatar, avatar, 10006, -(listSum * (int)this.getSeidJson(seid)["value1"].n), 0);
		}

		// Token: 0x06005953 RID: 22867 RVA: 0x0025522C File Offset: 0x0025342C
		public void ListRealizeSeid185(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			for (int i = 0; i < this.getSeidJson(seid)["value1"].list.Count; i++)
			{
				avatar.OtherAvatar.spell.addDBuff((int)this.getSeidJson(seid)["value1"][i].n, (int)this.getSeidJson(seid)["value2"][i].n);
			}
		}

		// Token: 0x06005954 RID: 22868 RVA: 0x002552A9 File Offset: 0x002534A9
		public void ListRealizeSeid186(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			avatar.spell.addBuff(this.getSeidJson(seid)["value1"].I, flag[0]);
		}

		// Token: 0x06005955 RID: 22869 RVA: 0x002552D8 File Offset: 0x002534D8
		public void ListRealizeSeid187(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int num = flag[0];
			int num2 = num * this.getSeidJson(seid)["value1"].I / 100;
			flag[0] = num - num2;
			avatar.OtherAvatar.setHP(avatar.OtherAvatar.HP - num2);
		}

		// Token: 0x06005956 RID: 22870 RVA: 0x00255330 File Offset: 0x00253530
		public void ListRealizeSeid190(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			JSONObject seidJson = this.getSeidJson(seid);
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

		// Token: 0x06005957 RID: 22871 RVA: 0x002553A0 File Offset: 0x002535A0
		public void ListRealizeSeid191(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int num;
			if (this.getTargetAvatar(seid, avatar).isPlayer())
			{
				num = RoundManager.instance.PlayerCurRoundDrawCardNum;
			}
			else
			{
				num = RoundManager.instance.NpcCurRoundDrawCardNum;
			}
			avatar.recvDamage(avatar, avatar, 10006, num * buffInfo[1], 0);
		}

		// Token: 0x06005958 RID: 22872 RVA: 0x002553F0 File Offset: 0x002535F0
		public void ListRealizeSeid192(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int key = flag[1];
			if (!_skillJsonData.DataDict[key].AttackType.Contains(this.getSeidJson(seid)["value2"].I))
			{
				return;
			}
			if (flag[0] > 0)
			{
				flag[0] = flag[0] + (int)Math.Ceiling(Convert.ToDouble((float)(flag[0] * buffInfo[1]) * this.getSeidJson(seid)["value1"].n));
			}
		}

		// Token: 0x06005959 RID: 22873 RVA: 0x00004095 File Offset: 0x00002295
		public void ListRealizeSeid193(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
		}

		// Token: 0x0600595A RID: 22874 RVA: 0x00255484 File Offset: 0x00253684
		public void ListRealizeSeid194(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			if (avatar.buffmag.GetBuffSum(this.getSeidJson(seid)["value1"].I) > 0)
			{
				Dictionary<int, int> tempDunSu = avatar.fightTemp.tempDunSu;
				int key = this.buffID;
				tempDunSu[key] -= this.getSeidJson(seid)["value2"].I;
			}
		}

		// Token: 0x0600595B RID: 22875 RVA: 0x002554F0 File Offset: 0x002536F0
		public void ListRealizeSeid195(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			if (flag.Count < 2)
			{
				return;
			}
			int addNum = flag[0];
			if (flag[1] == (int)this.getSeidJson(seid)["value2"].n)
			{
				this.BuffSeidFlagAddNum(seid, addNum, avatar);
				while (avatar.BuffSeidFlag[seid][this.buffID] >= (int)this.getSeidJson(seid)["value1"].n)
				{
					Dictionary<int, int> dictionary = avatar.BuffSeidFlag[seid];
					int key = this.buffID;
					dictionary[key] -= (int)this.getSeidJson(seid)["value1"].n;
					avatar.spell.addDBuff((int)this.getSeidJson(seid)["value3"].n, (int)this.getSeidJson(seid)["value4"].n * buffInfo[1]);
				}
			}
		}

		// Token: 0x0600595C RID: 22876 RVA: 0x002555F0 File Offset: 0x002537F0
		public void ListRealizeSeid198(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int nowSkillUsedLingQiSum = RoundManager.instance.NowSkillUsedLingQiSum;
			if (flag[0] > 0)
			{
				flag[0] = flag[0] + buffInfo[1] * nowSkillUsedLingQiSum;
			}
		}

		// Token: 0x0600595D RID: 22877 RVA: 0x00255630 File Offset: 0x00253830
		public void ListRealizeSeid199(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			if (_skillJsonData.DataDict[flag[1]].AttackType.Contains(this.getSeidJson(seid)["value1"].I))
			{
				int nowSkillUsedLingQiSum = RoundManager.instance.NowSkillUsedLingQiSum;
				if (flag[0] > 0)
				{
					flag[0] = flag[0] + (int)Math.Ceiling(Convert.ToDouble((float)(flag[0] * buffInfo[1] * nowSkillUsedLingQiSum) * this.getSeidJson(seid)["value2"].n / 100f));
				}
			}
		}

		// Token: 0x0600595E RID: 22878 RVA: 0x002556D3 File Offset: 0x002538D3
		public void ListRealizeSeid200(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			avatar.jieyin.AddJinMai((int)this.getSeidJson(seid)["value1"].n * buffInfo[1]);
		}

		// Token: 0x0600595F RID: 22879 RVA: 0x002556FF File Offset: 0x002538FF
		public void ListRealizeSeid201(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			avatar.jieyin.AddYiZhi((int)this.getSeidJson(seid)["value1"].n * buffInfo[1]);
		}

		// Token: 0x06005960 RID: 22880 RVA: 0x0025572B File Offset: 0x0025392B
		public void ListRealizeSeid202(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			avatar.jieyin.AddHuaYing((int)this.getSeidJson(seid)["value1"].n * buffInfo[1]);
		}

		// Token: 0x06005961 RID: 22881 RVA: 0x00255757 File Offset: 0x00253957
		public void ListRealizeSeid203(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			avatar.jieyin.AddJinDanHP((int)this.getSeidJson(seid)["value1"].n * buffInfo[1]);
		}

		// Token: 0x06005962 RID: 22882 RVA: 0x00255784 File Offset: 0x00253984
		public void ListRealizeSeid208(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			float num = this.getSeidJson(seid)["value1"].n / 100f;
			int num2 = (int)((float)(avatar.jieyin.JinDanHP_Max - avatar.jieyin.JinDanHP) * num);
			avatar.jieyin.AddJinDanHP(num2);
		}

		// Token: 0x06005963 RID: 22883 RVA: 0x002557D8 File Offset: 0x002539D8
		public void ListRealizeSeid215(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int i = this.getSeidJson(seid)["value1"].I;
			int i2 = this.getSeidJson(seid)["value2"].I;
			int i3 = this.getSeidJson(seid)["value3"].I;
			int i4 = this.getSeidJson(seid)["value4"].I;
			int buffSum = avatar.buffmag.GetBuffSum(i);
			if (buffSum > 0)
			{
				foreach (List<int> buffid in avatar.buffmag.getBuffByID(i))
				{
					avatar.spell.removeBuff(buffid);
				}
				int num = buffSum / i2;
				if (num > 0)
				{
					avatar.spell.addBuff(i3, num * i4);
				}
			}
		}

		// Token: 0x06005964 RID: 22884 RVA: 0x002558C4 File Offset: 0x00253AC4
		public void ListRealizeSeid217(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int num = avatar.shengShi - avatar.OtherAvatar.shengShi;
			if (num > 0)
			{
				flag[0] = flag[0] + num;
			}
		}

		// Token: 0x06005965 RID: 22885 RVA: 0x002558FB File Offset: 0x00253AFB
		public void ListRealizeSeid300(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			flag[0] = 0;
		}

		// Token: 0x06005966 RID: 22886 RVA: 0x00255908 File Offset: 0x00253B08
		public void ListRealizeSeid312(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int i = this.getSeidJson(seid)["value1"].I;
			if (Tools.symbol(this.getSeidJson(seid)["panduan"].str, avatar.fightTemp.NowRoundUsedSkills.Count, this.getSeidJson(seid)["value1"].I))
			{
				if (!avatar.SkillSeidFlag.ContainsKey(24))
				{
					avatar.SkillSeidFlag.Add(24, new Dictionary<int, int>());
					avatar.SkillSeidFlag[24].Add(0, 1);
				}
				avatar.SkillSeidFlag[24][0] = 1;
			}
		}

		// Token: 0x06005967 RID: 22887 RVA: 0x002559B8 File Offset: 0x00253BB8
		public void ListRealizeSeid314(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int key = flag[1];
			if (!_skillJsonData.DataDict[key].AttackType.Contains(this.getSeidJson(seid)["value2"].I))
			{
				return;
			}
			int buffSum = this.getTargetAvatar(seid, avatar).buffmag.GetBuffSum(this.getSeidJson(seid)["value1"].I);
			int nowSkillUsedLingQiSum = RoundManager.instance.NowSkillUsedLingQiSum;
			if (flag[0] > 0)
			{
				flag[0] = flag[0] + nowSkillUsedLingQiSum * buffSum;
			}
		}

		// Token: 0x06005968 RID: 22888 RVA: 0x00255A50 File Offset: 0x00253C50
		public void ListRealizeSeid315(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int i = this.getSeidJson(seid)["value1"].I;
			int num = this.getSeidJson(seid)["value2"].I * buffInfo[1];
			avatar.OtherAvatar.spell.addBuff(i, num);
		}

		// Token: 0x0400520A RID: 21002
		public Dictionary<int, JSONObject> buffSeidList = new Dictionary<int, JSONObject>();

		// Token: 0x0400520B RID: 21003
		public JObject NowBuffInfo = new JObject();

		// Token: 0x0400520C RID: 21004
		public int buffID;

		// Token: 0x0400520D RID: 21005
		public int _loopTime;

		// Token: 0x0400520E RID: 21006
		public int _totalTime;

		// Token: 0x0400520F RID: 21007
		public List<int> seid = new List<int>();

		// Token: 0x04005210 RID: 21008
		private static Dictionary<string, MethodInfo> methodDict = new Dictionary<string, MethodInfo>();

		// Token: 0x04005211 RID: 21009
		public static int _NeiShangLoopCount;

		// Token: 0x0200162E RID: 5678
		public enum AllBUFF
		{
			// Token: 0x04007173 RID: 29043
			BUFF1 = 1,
			// Token: 0x04007174 RID: 29044
			BUFF2,
			// Token: 0x04007175 RID: 29045
			BUFF3,
			// Token: 0x04007176 RID: 29046
			BUFF4,
			// Token: 0x04007177 RID: 29047
			BUFF5,
			// Token: 0x04007178 RID: 29048
			BUFF6,
			// Token: 0x04007179 RID: 29049
			BUFF7,
			// Token: 0x0400717A RID: 29050
			BUFF8,
			// Token: 0x0400717B RID: 29051
			BUFF9,
			// Token: 0x0400717C RID: 29052
			BUFF10,
			// Token: 0x0400717D RID: 29053
			BUFF11,
			// Token: 0x0400717E RID: 29054
			BUFF12,
			// Token: 0x0400717F RID: 29055
			BUFF13,
			// Token: 0x04007180 RID: 29056
			BUFF14,
			// Token: 0x04007181 RID: 29057
			BUFF15,
			// Token: 0x04007182 RID: 29058
			BUFF16,
			// Token: 0x04007183 RID: 29059
			BUFF17,
			// Token: 0x04007184 RID: 29060
			BUFF18,
			// Token: 0x04007185 RID: 29061
			BUFF19,
			// Token: 0x04007186 RID: 29062
			BUFF20,
			// Token: 0x04007187 RID: 29063
			BUFF21,
			// Token: 0x04007188 RID: 29064
			BUFF22,
			// Token: 0x04007189 RID: 29065
			BUFF23,
			// Token: 0x0400718A RID: 29066
			BUFF24,
			// Token: 0x0400718B RID: 29067
			BUFF25,
			// Token: 0x0400718C RID: 29068
			BUFF26,
			// Token: 0x0400718D RID: 29069
			BUFF27,
			// Token: 0x0400718E RID: 29070
			BUFF28,
			// Token: 0x0400718F RID: 29071
			BUFF29,
			// Token: 0x04007190 RID: 29072
			BUFF30,
			// Token: 0x04007191 RID: 29073
			BUFF31,
			// Token: 0x04007192 RID: 29074
			BUFF32,
			// Token: 0x04007193 RID: 29075
			BUFF33,
			// Token: 0x04007194 RID: 29076
			BUFF34,
			// Token: 0x04007195 RID: 29077
			BUFF35,
			// Token: 0x04007196 RID: 29078
			BUFF36,
			// Token: 0x04007197 RID: 29079
			BUFF37,
			// Token: 0x04007198 RID: 29080
			BUFF38,
			// Token: 0x04007199 RID: 29081
			BUFF39,
			// Token: 0x0400719A RID: 29082
			BUFF40,
			// Token: 0x0400719B RID: 29083
			BUFF41,
			// Token: 0x0400719C RID: 29084
			BUFF42,
			// Token: 0x0400719D RID: 29085
			BUFF43,
			// Token: 0x0400719E RID: 29086
			BUFF44,
			// Token: 0x0400719F RID: 29087
			BUFF45,
			// Token: 0x040071A0 RID: 29088
			BUFF46,
			// Token: 0x040071A1 RID: 29089
			BUFF47,
			// Token: 0x040071A2 RID: 29090
			BUFF48,
			// Token: 0x040071A3 RID: 29091
			BUFF49,
			// Token: 0x040071A4 RID: 29092
			BUFF58 = 58,
			// Token: 0x040071A5 RID: 29093
			BUFF59,
			// Token: 0x040071A6 RID: 29094
			BUFF62 = 62,
			// Token: 0x040071A7 RID: 29095
			BUFF65 = 65,
			// Token: 0x040071A8 RID: 29096
			BUFF68 = 68,
			// Token: 0x040071A9 RID: 29097
			BUFF73 = 73,
			// Token: 0x040071AA RID: 29098
			BUFF74,
			// Token: 0x040071AB RID: 29099
			BUFF75,
			// Token: 0x040071AC RID: 29100
			BUFF76,
			// Token: 0x040071AD RID: 29101
			BUFF77,
			// Token: 0x040071AE RID: 29102
			BUFF81 = 81,
			// Token: 0x040071AF RID: 29103
			BUFF83 = 83,
			// Token: 0x040071B0 RID: 29104
			BUFF85 = 85,
			// Token: 0x040071B1 RID: 29105
			BUFF86,
			// Token: 0x040071B2 RID: 29106
			BUFF90 = 90,
			// Token: 0x040071B3 RID: 29107
			BUFF91,
			// Token: 0x040071B4 RID: 29108
			BUFF95 = 95,
			// Token: 0x040071B5 RID: 29109
			BUFF96,
			// Token: 0x040071B6 RID: 29110
			BUFF97,
			// Token: 0x040071B7 RID: 29111
			BUFF99 = 99,
			// Token: 0x040071B8 RID: 29112
			BUFF101 = 101,
			// Token: 0x040071B9 RID: 29113
			BUFF102,
			// Token: 0x040071BA RID: 29114
			BUFF103,
			// Token: 0x040071BB RID: 29115
			BUFF106 = 106,
			// Token: 0x040071BC RID: 29116
			BUFF107,
			// Token: 0x040071BD RID: 29117
			BUFF110 = 110,
			// Token: 0x040071BE RID: 29118
			BUFF113 = 113,
			// Token: 0x040071BF RID: 29119
			BUFF116 = 116,
			// Token: 0x040071C0 RID: 29120
			BUFF118 = 118,
			// Token: 0x040071C1 RID: 29121
			BUFF119,
			// Token: 0x040071C2 RID: 29122
			BUFF120,
			// Token: 0x040071C3 RID: 29123
			BUFF121,
			// Token: 0x040071C4 RID: 29124
			BUFF122,
			// Token: 0x040071C5 RID: 29125
			BUFF126 = 126,
			// Token: 0x040071C6 RID: 29126
			BUFF128 = 128,
			// Token: 0x040071C7 RID: 29127
			BUFF129,
			// Token: 0x040071C8 RID: 29128
			BUFF136 = 136,
			// Token: 0x040071C9 RID: 29129
			BUFF139 = 139,
			// Token: 0x040071CA RID: 29130
			BUFF140,
			// Token: 0x040071CB RID: 29131
			BUFF143 = 143,
			// Token: 0x040071CC RID: 29132
			BUFF144,
			// Token: 0x040071CD RID: 29133
			BUFF150 = 150,
			// Token: 0x040071CE RID: 29134
			BUFF151,
			// Token: 0x040071CF RID: 29135
			BUFF152,
			// Token: 0x040071D0 RID: 29136
			BUFF155 = 155,
			// Token: 0x040071D1 RID: 29137
			BUFF156,
			// Token: 0x040071D2 RID: 29138
			BUFF157,
			// Token: 0x040071D3 RID: 29139
			BUFF158,
			// Token: 0x040071D4 RID: 29140
			BUFF159,
			// Token: 0x040071D5 RID: 29141
			BUFF160,
			// Token: 0x040071D6 RID: 29142
			BUFF161,
			// Token: 0x040071D7 RID: 29143
			BUFF162,
			// Token: 0x040071D8 RID: 29144
			BUFF163,
			// Token: 0x040071D9 RID: 29145
			BUFF164,
			// Token: 0x040071DA RID: 29146
			BUFF165,
			// Token: 0x040071DB RID: 29147
			BUFF166,
			// Token: 0x040071DC RID: 29148
			BUFF167,
			// Token: 0x040071DD RID: 29149
			BUFF168,
			// Token: 0x040071DE RID: 29150
			BUFF169,
			// Token: 0x040071DF RID: 29151
			BUFF170,
			// Token: 0x040071E0 RID: 29152
			BUFF171,
			// Token: 0x040071E1 RID: 29153
			BUFF172,
			// Token: 0x040071E2 RID: 29154
			BUFF173,
			// Token: 0x040071E3 RID: 29155
			BUFF174,
			// Token: 0x040071E4 RID: 29156
			BUFF175,
			// Token: 0x040071E5 RID: 29157
			BUFF176,
			// Token: 0x040071E6 RID: 29158
			BUFF177,
			// Token: 0x040071E7 RID: 29159
			BUFF178,
			// Token: 0x040071E8 RID: 29160
			BUFF180 = 180,
			// Token: 0x040071E9 RID: 29161
			BUFF181,
			// Token: 0x040071EA RID: 29162
			BUFF182,
			// Token: 0x040071EB RID: 29163
			BUFF189 = 189,
			// Token: 0x040071EC RID: 29164
			BUFF204 = 204,
			// Token: 0x040071ED RID: 29165
			BUFF205,
			// Token: 0x040071EE RID: 29166
			BUFF206,
			// Token: 0x040071EF RID: 29167
			BUFF207,
			// Token: 0x040071F0 RID: 29168
			BUFF209 = 209,
			// Token: 0x040071F1 RID: 29169
			BUFF210,
			// Token: 0x040071F2 RID: 29170
			BUFF211,
			// Token: 0x040071F3 RID: 29171
			BUFF213 = 213,
			// Token: 0x040071F4 RID: 29172
			BUFF214,
			// Token: 0x040071F5 RID: 29173
			BUFF215,
			// Token: 0x040071F6 RID: 29174
			BUFF216,
			// Token: 0x040071F7 RID: 29175
			BUFF217,
			// Token: 0x040071F8 RID: 29176
			BUFF218,
			// Token: 0x040071F9 RID: 29177
			BUFF219
		}
	}
}
