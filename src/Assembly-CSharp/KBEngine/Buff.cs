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

namespace KBEngine
{
	// Token: 0x0200103A RID: 4154
	public class Buff
	{
		// Token: 0x0600636D RID: 25453 RVA: 0x0027B7FC File Offset: 0x002799FC
		private static void InitMethod(string methodName)
		{
			if (!Buff.methodDict.ContainsKey(methodName))
			{
				MethodInfo method = typeof(Buff).GetMethod(methodName);
				Buff.methodDict.Add(methodName, method);
			}
		}

		// Token: 0x0600636E RID: 25454 RVA: 0x0027B834 File Offset: 0x00279A34
		public Buff(int buffid)
		{
			this.buffID = buffid;
			this._totalTime = jsonData.instance.BuffJsonData[buffid.ToString()]["totaltime"].I;
			foreach (JSONObject jsonobject in jsonData.instance.BuffJsonData[buffid.ToString()]["seid"].list)
			{
				this.seid.Add(jsonobject.I);
			}
		}

		// Token: 0x0600636F RID: 25455 RVA: 0x0027B90C File Offset: 0x00279B0C
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

		// Token: 0x06006370 RID: 25456 RVA: 0x0027BA84 File Offset: 0x00279C84
		public void onAttach(Entity _avatar, List<int> buffInfo)
		{
			foreach (int num in this.seid)
			{
				this.onAttachRealizeSeid(num, _avatar, buffInfo);
			}
		}

		// Token: 0x06006371 RID: 25457 RVA: 0x0027BADC File Offset: 0x00279CDC
		public void onDetach(Entity _avatar, List<int> buffInfo)
		{
			foreach (int num in this.seid)
			{
				this.onDetachRealizeSeid(num, _avatar, buffInfo);
			}
		}

		// Token: 0x06006372 RID: 25458 RVA: 0x0027BB34 File Offset: 0x00279D34
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

		// Token: 0x06006373 RID: 25459 RVA: 0x0027BBA0 File Offset: 0x00279DA0
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

		// Token: 0x06006374 RID: 25460 RVA: 0x0027BC08 File Offset: 0x00279E08
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

		// Token: 0x06006375 RID: 25461 RVA: 0x0004492F File Offset: 0x00042B2F
		public Avatar getTargetAvatar(int seid, Avatar attker)
		{
			if (this.getSeidJson(seid)["target"].I == 1)
			{
				return attker;
			}
			return attker.OtherAvatar;
		}

		// Token: 0x06006376 RID: 25462 RVA: 0x0027BC70 File Offset: 0x00279E70
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

		// Token: 0x06006377 RID: 25463 RVA: 0x00044952 File Offset: 0x00042B52
		public static JSONObject getSeidJson(int seid, int _buffID)
		{
			return jsonData.instance.BuffSeidJsonData[seid][_buffID.ToString()];
		}

		// Token: 0x06006378 RID: 25464 RVA: 0x0027BD54 File Offset: 0x00279F54
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

		// Token: 0x06006379 RID: 25465 RVA: 0x0027BDB4 File Offset: 0x00279FB4
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
				if (!_skillJsonData.DataDict[flag[1]].seid.Contains(this.getSeidJson(nowSeid)["value1"].I))
				{
					return false;
				}
				if (flag[2] == 1)
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

		// Token: 0x0600637A RID: 25466 RVA: 0x0027CF98 File Offset: 0x0027B198
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

		// Token: 0x0600637B RID: 25467 RVA: 0x0027D1CC File Offset: 0x0027B3CC
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

		// Token: 0x0600637C RID: 25468 RVA: 0x0027D250 File Offset: 0x0027B450
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

		// Token: 0x0600637D RID: 25469 RVA: 0x0027D2CC File Offset: 0x0027B4CC
		public void SeidAddBuff(int seid, Avatar avatar, List<int> buffInfo)
		{
			Dictionary<int, int> dictionary = avatar.BuffSeidFlag[seid];
			int key = this.buffID;
			dictionary[key] -= (int)this.getSeidJson(seid)["value1"].n;
			avatar.spell.addDBuff((int)this.getSeidJson(seid)["value2"].n, (int)this.getSeidJson(seid)["value3"].n * buffInfo[1]);
		}

		// Token: 0x0600637E RID: 25470 RVA: 0x0027D354 File Offset: 0x0027B554
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

		// Token: 0x0600637F RID: 25471 RVA: 0x0027D394 File Offset: 0x0027B594
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

		// Token: 0x06006380 RID: 25472 RVA: 0x0004496C File Offset: 0x00042B6C
		public bool RandomX(int percent)
		{
			return jsonData.instance.getRandom() % 100 > percent;
		}

		// Token: 0x06006381 RID: 25473 RVA: 0x0027D3D4 File Offset: 0x0027B5D4
		public void ListRealizeSeid1(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			if (avatar.buffmag.HasBuffSeid(58))
			{
				List<List<int>> buffBySeid = avatar.buffmag.getBuffBySeid(58);
				avatar.spell.onBuffTick(avatar.buffmag.getBuffIndex(buffBySeid[0]), null, 0);
			}
			avatar.recvDamage(avatar, avatar, 10006, (int)this.getSeidJson(seid)["value1"].n * buffInfo[1], 0);
		}

		// Token: 0x06006382 RID: 25474 RVA: 0x0027D44C File Offset: 0x0027B64C
		public void ListRealizeSeid2(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int num = this.getSeidJson(seid)["value1"].I * buffInfo[1];
			for (int i = 0; i < num; i++)
			{
				this.SeidAddCard(avatar, flag);
			}
		}

		// Token: 0x06006383 RID: 25475 RVA: 0x0027D490 File Offset: 0x0027B690
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

		// Token: 0x06006384 RID: 25476 RVA: 0x0027D4FC File Offset: 0x0027B6FC
		public void ListRealizeSeid4(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int realReduceDamage = 0;
			if (flag[0] > buffInfo[1])
			{
				realReduceDamage = buffInfo[1];
			}
			else if (flag[0] <= buffInfo[1])
			{
				realReduceDamage = flag[0];
			}
			if (!(RoundManager.instance != null) || !RoundManager.instance.IsVirtual)
			{
				Queue<UnityAction> queue = new Queue<UnityAction>();
				UnityAction item = delegate()
				{
					((GameObject)avatar.renderObj).GetComponentInChildren<AvatarShowHpDamage>().show(realReduceDamage, 1);
					YSFuncList.Ints.Continue();
				};
				queue.Enqueue(item);
				YSFuncList.Ints.AddFunc(queue);
			}
			flag[0] = flag[0] - realReduceDamage;
			buffInfo[1] = buffInfo[1] - realReduceDamage;
			if (buffInfo[2] == 5 && buffInfo[1] <= 0)
			{
				avatar.spell.onBuffTickByType(43, flag);
			}
		}

		// Token: 0x06006385 RID: 25477 RVA: 0x0027D5F0 File Offset: 0x0027B7F0
		public void ListRealizeSeid5(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			for (int i = 0; i < this.getSeidJson(seid)["value1"].list.Count; i++)
			{
				int i2 = this.getSeidJson(seid)["value1"][i].I;
				int num = this.getSeidJson(seid)["value2"][i].I * buffInfo[1];
				avatar.spell.addBuff(i2, num);
			}
		}

		// Token: 0x06006386 RID: 25478 RVA: 0x000042DD File Offset: 0x000024DD
		public void ListRealizeSeid6(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
		}

		// Token: 0x06006387 RID: 25479 RVA: 0x000042DD File Offset: 0x000024DD
		public void ListRealizeSeid7(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
		}

		// Token: 0x06006388 RID: 25480 RVA: 0x000042DD File Offset: 0x000024DD
		public void ListRealizeSeid8(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
		}

		// Token: 0x06006389 RID: 25481 RVA: 0x0027D674 File Offset: 0x0027B874
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

		// Token: 0x0600638A RID: 25482 RVA: 0x0027D720 File Offset: 0x0027B920
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

		// Token: 0x0600638B RID: 25483 RVA: 0x00044981 File Offset: 0x00042B81
		public void ListRealizeSeid11(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			avatar.recvDamage(avatar, avatar.OtherAvatar, 10006, (int)this.getSeidJson(seid)["value1"].n * buffInfo[1], 0);
		}

		// Token: 0x0600638C RID: 25484 RVA: 0x000449B6 File Offset: 0x00042BB6
		public void ListRealizeSeid12(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			flag[0] = flag[0] * 2;
		}

		// Token: 0x0600638D RID: 25485 RVA: 0x000449CA File Offset: 0x00042BCA
		public void ListRealizeSeid13(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			flag[1] = 1;
			avatar.state = 5;
		}

		// Token: 0x0600638E RID: 25486 RVA: 0x000449DC File Offset: 0x00042BDC
		public void ListRealizeSeid14(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			if (flag[0] < 0)
			{
				flag[0] = (int)Math.Ceiling(Convert.ToDouble(flag[0] / 2));
			}
		}

		// Token: 0x0600638F RID: 25487 RVA: 0x0027D7A4 File Offset: 0x0027B9A4
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

		// Token: 0x06006390 RID: 25488 RVA: 0x00044A06 File Offset: 0x00042C06
		public void ListRealizeSeid16(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			avatar.recvDamage(avatar, avatar, 10006, avatar.HP - avatar.HP_Max, 0);
		}

		// Token: 0x06006391 RID: 25489 RVA: 0x0027D864 File Offset: 0x0027BA64
		public void ListRealizeSeid17(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int i = this.getSeidJson(seid)["value1"].I;
			int num = this.getSeidJson(seid)["value2"].I * buffInfo[1];
			for (int j = 0; j < num; j++)
			{
				avatar.OtherAvatar.spell.addDBuff(i);
			}
		}

		// Token: 0x06006392 RID: 25490 RVA: 0x0027D8C8 File Offset: 0x0027BAC8
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

		// Token: 0x06006393 RID: 25491 RVA: 0x000042DD File Offset: 0x000024DD
		public void ListRealizeSeid19(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
		}

		// Token: 0x06006394 RID: 25492 RVA: 0x000042DD File Offset: 0x000024DD
		public void ListRealizeSeid20(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
		}

		// Token: 0x06006395 RID: 25493 RVA: 0x0027D968 File Offset: 0x0027BB68
		public void ListRealizeSeid21(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			flag[0] = flag[0] - (int)this.getSeidJson(seid)["value1"].n * buffInfo[1];
		}

		// Token: 0x06006396 RID: 25494 RVA: 0x00044A24 File Offset: 0x00042C24
		public void ListRealizeSeid22(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			if (!avatar.buffmag.HasBuffSeid(23))
			{
				avatar.fightTemp.tempNowCard += (int)this.getSeidJson(seid)["value1"].n;
			}
		}

		// Token: 0x06006397 RID: 25495 RVA: 0x0027D9A8 File Offset: 0x0027BBA8
		public void onDetachRealizeSeid22(int seid, Avatar avatar, List<int> buffInfo)
		{
			if (!avatar.buffmag.HasBuffSeid(23))
			{
				avatar.fightTemp.tempNowCard -= (int)this.getSeidJson(seid)["value1"].n * buffInfo[1];
			}
		}

		// Token: 0x06006398 RID: 25496 RVA: 0x0027D9F8 File Offset: 0x0027BBF8
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

		// Token: 0x06006399 RID: 25497 RVA: 0x00044A5E File Offset: 0x00042C5E
		public void onDetachRealizeSeid23(int seid, Avatar avatar, List<int> buffInfo)
		{
			avatar.BuffSeidFlag.Remove(seid);
		}

		// Token: 0x0600639A RID: 25498 RVA: 0x0027DA70 File Offset: 0x0027BC70
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

		// Token: 0x0600639B RID: 25499 RVA: 0x0027DAF0 File Offset: 0x0027BCF0
		public void ListRealizeSeid25(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			avatar.recvDamage(avatar, avatar.OtherAvatar, 10001 + (int)this.getSeidJson(seid)["value2"].n, (int)this.getSeidJson(seid)["value1"].n * buffInfo[1], 0);
		}

		// Token: 0x0600639C RID: 25500 RVA: 0x0027DB48 File Offset: 0x0027BD48
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

		// Token: 0x0600639D RID: 25501 RVA: 0x0027DBA4 File Offset: 0x0027BDA4
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

		// Token: 0x0600639E RID: 25502 RVA: 0x0027DC5C File Offset: 0x0027BE5C
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

		// Token: 0x0600639F RID: 25503 RVA: 0x0027DD1C File Offset: 0x0027BF1C
		public void ListRealizeSeid29(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int num = flag[0];
			if (avatar.HP_Max < avatar.HP - num)
			{
				avatar.recvDamage(avatar, avatar.OtherAvatar, 10006, avatar.HP - num - avatar.HP_Max, 0);
			}
		}

		// Token: 0x060063A0 RID: 25504 RVA: 0x0027DD68 File Offset: 0x0027BF68
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

		// Token: 0x060063A1 RID: 25505 RVA: 0x0027DE30 File Offset: 0x0027C030
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

		// Token: 0x060063A2 RID: 25506 RVA: 0x0027DF0C File Offset: 0x0027C10C
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

		// Token: 0x060063A3 RID: 25507 RVA: 0x0027DFA0 File Offset: 0x0027C1A0
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

		// Token: 0x060063A4 RID: 25508 RVA: 0x0027DFA0 File Offset: 0x0027C1A0
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

		// Token: 0x060063A5 RID: 25509 RVA: 0x0027E058 File Offset: 0x0027C258
		public void ListRealizeSeid36(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int num = RoundManager.instance.getListSum(avatar.crystal) / (int)this.getSeidJson(seid)["value1"].n;
			if (num > 0)
			{
				avatar.spell.addBuff(this.getSeidJson(seid)["value2"].I, num * this.getSeidJson(seid)["value3"].I * buffInfo[1]);
			}
		}

		// Token: 0x060063A6 RID: 25510 RVA: 0x0027E0D4 File Offset: 0x0027C2D4
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

		// Token: 0x060063A7 RID: 25511 RVA: 0x0027DF0C File Offset: 0x0027C10C
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

		// Token: 0x060063A8 RID: 25512 RVA: 0x0027E170 File Offset: 0x0027C370
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

		// Token: 0x060063A9 RID: 25513 RVA: 0x0027E264 File Offset: 0x0027C464
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

		// Token: 0x060063AA RID: 25514 RVA: 0x0027E2F0 File Offset: 0x0027C4F0
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

		// Token: 0x060063AB RID: 25515 RVA: 0x0027E3EC File Offset: 0x0027C5EC
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

		// Token: 0x060063AC RID: 25516 RVA: 0x0027E264 File Offset: 0x0027C464
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

		// Token: 0x060063AD RID: 25517 RVA: 0x0027E508 File Offset: 0x0027C708
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

		// Token: 0x060063AE RID: 25518 RVA: 0x0027E624 File Offset: 0x0027C824
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

		// Token: 0x060063AF RID: 25519 RVA: 0x0027E6A0 File Offset: 0x0027C8A0
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

		// Token: 0x060063B0 RID: 25520 RVA: 0x0027E6F4 File Offset: 0x0027C8F4
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

		// Token: 0x060063B1 RID: 25521 RVA: 0x0027E774 File Offset: 0x0027C974
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

		// Token: 0x060063B2 RID: 25522 RVA: 0x0027E834 File Offset: 0x0027CA34
		public void ListRealizeSeid51(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			for (int i = 0; i < (int)this.getSeidJson(seid)["value1"].n; i++)
			{
				avatar.recvDamage(avatar, avatar.OtherAvatar, 10006, (int)this.getSeidJson(seid)["value2"].n, 0);
			}
		}

		// Token: 0x060063B3 RID: 25523 RVA: 0x0027E264 File Offset: 0x0027C464
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

		// Token: 0x060063B4 RID: 25524 RVA: 0x0027E890 File Offset: 0x0027CA90
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

		// Token: 0x060063B5 RID: 25525 RVA: 0x0027E954 File Offset: 0x0027CB54
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

		// Token: 0x060063B6 RID: 25526 RVA: 0x00044A6D File Offset: 0x00042C6D
		public void ListRealizeSeid55(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			avatar.fightTemp.tempShenShi[this.buffID] = (int)this.getSeidJson(seid)["value1"].n * buffInfo[1];
		}

		// Token: 0x060063B7 RID: 25527 RVA: 0x00044AA4 File Offset: 0x00042CA4
		public void onDetachRealizeSeid55(int seid, Avatar avatar, List<int> buffInfo)
		{
			avatar.fightTemp.tempShenShi[this.buffID] = 0;
		}

		// Token: 0x060063B8 RID: 25528 RVA: 0x00044ABD File Offset: 0x00042CBD
		public void ListRealizeSeid56(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			avatar.fightTemp.TempHP_Max[this.buffID] = (int)this.getSeidJson(seid)["value1"].n * buffInfo[1];
		}

		// Token: 0x060063B9 RID: 25529 RVA: 0x00044AF4 File Offset: 0x00042CF4
		public void onDetachRealizeSeid56(int seid, Avatar avatar, List<int> buffInfo)
		{
			avatar.fightTemp.TempHP_Max[this.buffID] = 0;
		}

		// Token: 0x060063BA RID: 25530 RVA: 0x0027EA14 File Offset: 0x0027CC14
		public void ListRealizeSeid57(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int num = flag[1];
			if (jsonData.instance.skillJsonData[num.ToString()]["AttackType"].list.Find((JSONObject aa) => (int)aa.n == (int)this.getSeidJson(seid)["value1"].n) != null)
			{
				flag[0] = flag[0] + (int)((float)flag[0] * (this.getSeidJson(seid)["value2"].n * (float)buffInfo[1] / 100f));
			}
		}

		// Token: 0x060063BB RID: 25531 RVA: 0x000042DD File Offset: 0x000024DD
		public void ListRealizeSeid58(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
		}

		// Token: 0x060063BC RID: 25532 RVA: 0x000042DD File Offset: 0x000024DD
		public void ListRealizeSeid59(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
		}

		// Token: 0x060063BD RID: 25533 RVA: 0x0027EAC0 File Offset: 0x0027CCC0
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

		// Token: 0x060063BE RID: 25534 RVA: 0x00044B0D File Offset: 0x00042D0D
		public void ListRealizeSeid61(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			avatar.fightTemp.tempDunSu[this.buffID] = (int)this.getSeidJson(seid)["value1"].n * buffInfo[1];
		}

		// Token: 0x060063BF RID: 25535 RVA: 0x00044B44 File Offset: 0x00042D44
		public void onDetachRealizeSeid61(int seid, Avatar avatar, List<int> buffInfo)
		{
			avatar.fightTemp.tempDunSu[this.buffID] = 0;
		}

		// Token: 0x060063C0 RID: 25536 RVA: 0x000042DD File Offset: 0x000024DD
		public void ListRealizeSeid62(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
		}

		// Token: 0x060063C1 RID: 25537 RVA: 0x00044B5D File Offset: 0x00042D5D
		public void ListRealizeSeid63(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			RoundManager.instance.removeCard(avatar, this.getSeidJson(seid)["value1"].I * buffInfo[1]);
		}

		// Token: 0x060063C2 RID: 25538 RVA: 0x0027EBF4 File Offset: 0x0027CDF4
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

		// Token: 0x060063C3 RID: 25539 RVA: 0x000042DD File Offset: 0x000024DD
		public void ListRealizeSeid65(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
		}

		// Token: 0x060063C4 RID: 25540 RVA: 0x0027ECBC File Offset: 0x0027CEBC
		public void ListRealizeSeid66(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			List<List<int>> buffByID = avatar.OtherAvatar.buffmag.getBuffByID((int)this.getSeidJson(seid)["value1"].n);
			if (buffByID.Count > 0)
			{
				avatar.recvDamage(avatar, avatar.OtherAvatar, 10006, (int)this.getSeidJson(seid)["value2"].n * buffByID[0][1] * buffInfo[1], 0);
			}
		}

		// Token: 0x060063C5 RID: 25541 RVA: 0x0027ED3C File Offset: 0x0027CF3C
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

		// Token: 0x060063C6 RID: 25542 RVA: 0x00044B88 File Offset: 0x00042D88
		public void ListRealizeSeid68(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			if (!avatar.buffmag.HasBuffSeid(23))
			{
				avatar.fightTemp.tempNowCard -= (int)this.getSeidJson(seid)["value1"].n;
			}
		}

		// Token: 0x060063C7 RID: 25543 RVA: 0x0027EDFC File Offset: 0x0027CFFC
		public void onDetachRealizeSeid68(int seid, Avatar avatar, List<int> buffInfo)
		{
			if (!avatar.buffmag.HasBuffSeid(23))
			{
				avatar.fightTemp.tempNowCard += (int)this.getSeidJson(seid)["value1"].n * buffInfo[1];
			}
		}

		// Token: 0x060063C8 RID: 25544 RVA: 0x0027EE4C File Offset: 0x0027D04C
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

		// Token: 0x060063C9 RID: 25545 RVA: 0x0027EF88 File Offset: 0x0027D188
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

		// Token: 0x060063CA RID: 25546 RVA: 0x00044BC2 File Offset: 0x00042DC2
		public void ListRealizeSeid71(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			avatar.setHP(avatar.HP - (int)this.getSeidJson(seid)["value1"].n * buffInfo[1]);
		}

		// Token: 0x060063CB RID: 25547 RVA: 0x0027F02C File Offset: 0x0027D22C
		public void ListRealizeSeid72(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int num = flag[1];
			int CheckskillID = (int)this.getSeidJson(seid)["value1"].n;
			if (flag[0] > 0 && jsonData.instance.skillJsonData[string.Concat(num)]["AttackType"].list.FindAll((JSONObject aa) => (int)aa.n == CheckskillID).Count > 0)
			{
				flag[0] = Mathf.Clamp(flag[0] - (int)this.getSeidJson(seid)["value2"].n * buffInfo[1], 0, 99999999);
			}
		}

		// Token: 0x060063CC RID: 25548 RVA: 0x000042DD File Offset: 0x000024DD
		public void ListRealizeSeid73(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
		}

		// Token: 0x060063CD RID: 25549 RVA: 0x00044BF0 File Offset: 0x00042DF0
		public void ListRealizeSeid78(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			flag[3] = 1;
		}

		// Token: 0x060063CE RID: 25550 RVA: 0x0027F0F0 File Offset: 0x0027D2F0
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

		// Token: 0x060063CF RID: 25551 RVA: 0x0027F1DC File Offset: 0x0027D3DC
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

		// Token: 0x060063D0 RID: 25552 RVA: 0x000042DD File Offset: 0x000024DD
		public void ListRealizeSeid81(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
		}

		// Token: 0x060063D1 RID: 25553 RVA: 0x0027F2E0 File Offset: 0x0027D4E0
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

		// Token: 0x060063D2 RID: 25554 RVA: 0x000042DD File Offset: 0x000024DD
		public void ListRealizeSeid83(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
		}

		// Token: 0x060063D3 RID: 25555 RVA: 0x00044BFB File Offset: 0x00042DFB
		public void ListRealizeSeid84(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			if (flag[0] < (int)this.getSeidJson(seid)["value1"].n)
			{
				flag[0] = 0;
			}
		}

		// Token: 0x060063D4 RID: 25556 RVA: 0x0027F390 File Offset: 0x0027D590
		public void ListRealizeSeid87(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int num = (int)this.getSeidJson(seid)["value2"].n * buffInfo[1];
			for (int i = 0; i < num; i++)
			{
				avatar.spell.addDBuff((int)this.getSeidJson(seid)["value1"].n);
			}
		}

		// Token: 0x060063D5 RID: 25557 RVA: 0x0027F3EC File Offset: 0x0027D5EC
		public void ListRealizeSeid88(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			for (int i = 0; i < (int)(this.getSeidJson(seid)["value2"].n * (float)buffInfo[1]); i++)
			{
				avatar.OtherAvatar.spell.addDBuff((int)this.getSeidJson(seid)["value1"].n);
			}
		}

		// Token: 0x060063D6 RID: 25558 RVA: 0x0027F44C File Offset: 0x0027D64C
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

		// Token: 0x060063D7 RID: 25559 RVA: 0x000042DD File Offset: 0x000024DD
		public void ListRealizeSeid90(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
		}

		// Token: 0x060063D8 RID: 25560 RVA: 0x0027F508 File Offset: 0x0027D708
		public void ListRealizeSeid92(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			List<List<int>> buffByID = avatar.buffmag.getBuffByID((int)this.getSeidJson(seid)["value1"].n);
			if (buffByID.Count > 0)
			{
				buffByID[0][1] = (int)this.getSeidJson(seid)["value2"].n;
			}
			Event.fireOut("UpdataBuff", Array.Empty<object>());
		}

		// Token: 0x060063D9 RID: 25561 RVA: 0x0027F574 File Offset: 0x0027D774
		public void ListRealizeSeid93(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int damage = -(int)((float)avatar.HP_Max * this.getSeidJson(seid)["value1"].n / 100f * (float)buffInfo[1]);
			avatar.recvDamage(avatar, avatar, 10006, damage, 0);
		}

		// Token: 0x060063DA RID: 25562 RVA: 0x0027F5C4 File Offset: 0x0027D7C4
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

		// Token: 0x060063DB RID: 25563 RVA: 0x0027F658 File Offset: 0x0027D858
		public void ListRealizeSeid98(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int damage = (int)this.getSeidJson(seid)["value1"].n * buffInfo[1];
			avatar.recvDamage(avatar, avatar, 10001 + (int)this.getSeidJson(seid)["value2"].n, damage, 0);
		}

		// Token: 0x060063DC RID: 25564 RVA: 0x00044BF0 File Offset: 0x00042DF0
		public void ListRealizeSeid100(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			flag[3] = 1;
		}

		// Token: 0x060063DD RID: 25565 RVA: 0x0027F6B0 File Offset: 0x0027D8B0
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

		// Token: 0x060063DE RID: 25566 RVA: 0x0027F7B0 File Offset: 0x0027D9B0
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

		// Token: 0x060063DF RID: 25567 RVA: 0x0027F84C File Offset: 0x0027DA4C
		public void onAttachRealizeSeid108(int seid, Avatar avatar, List<int> buffInfo)
		{
			int endIndex = (JieDanManager.instence == null) ? 10 : 6;
			avatar.FightClearSkill(0, endIndex);
		}

		// Token: 0x060063E0 RID: 25568 RVA: 0x0027F874 File Offset: 0x0027DA74
		public void onAttachRealizeSeid109(int seid, Avatar avatar, List<int> buffInfo)
		{
			int endIndex = (JieDanManager.instence == null) ? 10 : 6;
			foreach (JSONObject jsonobject in this.getSeidJson(seid)["value1"].list)
			{
				avatar.FightAddSkill((int)jsonobject.n, 0, endIndex);
			}
		}

		// Token: 0x060063E1 RID: 25569 RVA: 0x0027F8F4 File Offset: 0x0027DAF4
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

		// Token: 0x060063E2 RID: 25570 RVA: 0x0027F9A4 File Offset: 0x0027DBA4
		public void onDetachRealizeSeid112(int seid, Avatar avatar, List<int> buffInfo)
		{
			if (avatar.buffmag.HasBuffSeid(58))
			{
				List<List<int>> buffBySeid = avatar.buffmag.getBuffBySeid(58);
				avatar.spell.onBuffTick(avatar.buffmag.getBuffIndex(buffBySeid[0]), null, 0);
			}
			avatar.recvDamage(avatar, avatar, 10006, (int)this.getSeidJson(seid)["value1"].n, 0);
		}

		// Token: 0x060063E3 RID: 25571 RVA: 0x00044C27 File Offset: 0x00042E27
		public void onDetachRealizeSeid114(int seid, Avatar avatar, List<int> buffInfo)
		{
			avatar.setHP(-1);
		}

		// Token: 0x060063E4 RID: 25572 RVA: 0x00044C30 File Offset: 0x00042E30
		public void ListRealizeSeid115(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			flag[0] = flag[0] + flag[0] * (int)this.getSeidJson(seid)["value1"].n * buffInfo[1] / 100;
		}

		// Token: 0x060063E5 RID: 25573 RVA: 0x0027FA14 File Offset: 0x0027DC14
		public void ListRealizeSeid117(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			avatar.spell.addDBuff(this.getSeidJson(seid)["value1"].I, this.getSeidJson(seid)["value2"].I * RoundManager.instance.NowSkillUsedLingQiSum * buffInfo[1]);
		}

		// Token: 0x060063E6 RID: 25574 RVA: 0x0027FA6C File Offset: 0x0027DC6C
		public void ListRealizeSeid123(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			UIFightPanel.Inst.FightSelectLingQi.gameObject.SetActive(true);
			UIFightPanel.Inst.FightSelectLingQi.SetSelectAction(delegate(LingQiType lq)
			{
				RoundManager.instance.DrawCard(avatar, (int)lq);
			});
		}

		// Token: 0x060063E7 RID: 25575 RVA: 0x000042DD File Offset: 0x000024DD
		public void ListRealizeSeid124(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
		}

		// Token: 0x060063E8 RID: 25576 RVA: 0x000042DD File Offset: 0x000024DD
		public void ListRealizeSeid125(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
		}

		// Token: 0x060063E9 RID: 25577 RVA: 0x0027FAB8 File Offset: 0x0027DCB8
		public void ListRealizeSeid127(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			for (int i = 0; i < this.getSeidJson(seid)["value1"].list.Count; i++)
			{
				avatar.spell.addDBuff((int)this.getSeidJson(seid)["value1"][i].n, (int)this.getSeidJson(seid)["value2"][i].n);
			}
		}

		// Token: 0x060063EA RID: 25578 RVA: 0x0027FB30 File Offset: 0x0027DD30
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

		// Token: 0x060063EB RID: 25579 RVA: 0x0027FC50 File Offset: 0x0027DE50
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

		// Token: 0x060063EC RID: 25580 RVA: 0x00044C6E File Offset: 0x00042E6E
		public void ListRealizeSeid131(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			this.ListRealizeSeid130(seid, avatar, buffInfo, flag);
		}

		// Token: 0x060063ED RID: 25581 RVA: 0x00044C6E File Offset: 0x00042E6E
		public void ListRealizeSeid132(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			this.ListRealizeSeid130(seid, avatar, buffInfo, flag);
		}

		// Token: 0x060063EE RID: 25582 RVA: 0x0027FD98 File Offset: 0x0027DF98
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

		// Token: 0x060063EF RID: 25583 RVA: 0x0027FE8C File Offset: 0x0027E08C
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

		// Token: 0x060063F0 RID: 25584 RVA: 0x0027FF28 File Offset: 0x0027E128
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
			avatar.spell.addDBuff(jsonobject["buffid"].I, (int)this.getSeidJson(seid)["value1"].n * buffInfo[1]);
		}

		// Token: 0x060063F1 RID: 25585 RVA: 0x0028000C File Offset: 0x0027E20C
		public void ListRealizeSeid137(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			if (flag[0] < 0)
			{
				flag[0] = flag[0] + (int)Math.Ceiling(Convert.ToDouble((float)flag[0] * this.getSeidJson(seid)["value1"].n));
			}
		}

		// Token: 0x060063F2 RID: 25586 RVA: 0x00280060 File Offset: 0x0027E260
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

		// Token: 0x060063F3 RID: 25587 RVA: 0x00044C7B File Offset: 0x00042E7B
		public void ListRealizeSeid139(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			flag[4] = this.getSeidJson(seid)["value2"].I;
		}

		// Token: 0x060063F4 RID: 25588 RVA: 0x0028011C File Offset: 0x0027E31C
		public void ListRealizeSeid141(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			if (flag[0] > 0)
			{
				flag[0] = flag[0] + (int)Math.Ceiling(Convert.ToDouble((float)flag[0] * this.getSeidJson(seid)["value1"].n));
				flag[0] = Mathf.Max(0, flag[0]);
			}
		}

		// Token: 0x060063F5 RID: 25589 RVA: 0x00280188 File Offset: 0x0027E388
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

		// Token: 0x060063F6 RID: 25590 RVA: 0x00280230 File Offset: 0x0027E430
		public void ListRealizeSeid146(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int num = flag[0];
			avatar.recvDamage(avatar, avatar, 10006, -num, 0);
		}

		// Token: 0x060063F7 RID: 25591 RVA: 0x00280258 File Offset: 0x0027E458
		public void ListRealizeSeid147(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			List<List<int>> buffByID = avatar.buffmag.getBuffByID((int)this.getSeidJson(seid)["value1"].n);
			if (buffByID.Count > 0)
			{
				avatar.recvDamage(avatar, avatar, 10006, -buffByID[0][1], 0);
			}
		}

		// Token: 0x060063F8 RID: 25592 RVA: 0x002802B0 File Offset: 0x0027E4B0
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

		// Token: 0x060063F9 RID: 25593 RVA: 0x00280358 File Offset: 0x0027E558
		public void ListRealizeSeid149(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int num = flag[0];
			for (int i = 0; i < this.getSeidJson(seid)["value1"].Count; i++)
			{
				avatar.spell.addDBuff(this.getSeidJson(seid)["value1"][i].I, buffInfo[1] * num);
			}
		}

		// Token: 0x060063FA RID: 25594 RVA: 0x002803C0 File Offset: 0x0027E5C0
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

		// Token: 0x060063FB RID: 25595 RVA: 0x002804C8 File Offset: 0x0027E6C8
		public void ListRealizeSeid179(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int listSum = RoundManager.instance.getListSum(avatar.crystal);
			avatar.recvDamage(avatar, avatar, 10006, -(listSum * (int)this.getSeidJson(seid)["value1"].n), 0);
		}

		// Token: 0x060063FC RID: 25596 RVA: 0x00280510 File Offset: 0x0027E710
		public void ListRealizeSeid185(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			for (int i = 0; i < this.getSeidJson(seid)["value1"].list.Count; i++)
			{
				avatar.OtherAvatar.spell.addDBuff((int)this.getSeidJson(seid)["value1"][i].n, (int)this.getSeidJson(seid)["value2"][i].n);
			}
		}

		// Token: 0x060063FD RID: 25597 RVA: 0x00044C9B File Offset: 0x00042E9B
		public void ListRealizeSeid186(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			avatar.spell.addBuff(this.getSeidJson(seid)["value1"].I, flag[0]);
		}

		// Token: 0x060063FE RID: 25598 RVA: 0x00280590 File Offset: 0x0027E790
		public void ListRealizeSeid187(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int num = flag[0];
			int num2 = num * this.getSeidJson(seid)["value1"].I / 100;
			flag[0] = num - num2;
			avatar.OtherAvatar.setHP(avatar.OtherAvatar.HP - num2);
		}

		// Token: 0x060063FF RID: 25599 RVA: 0x002805E8 File Offset: 0x0027E7E8
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

		// Token: 0x06006400 RID: 25600 RVA: 0x00280658 File Offset: 0x0027E858
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

		// Token: 0x06006401 RID: 25601 RVA: 0x002806A8 File Offset: 0x0027E8A8
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

		// Token: 0x06006402 RID: 25602 RVA: 0x000042DD File Offset: 0x000024DD
		public void ListRealizeSeid193(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
		}

		// Token: 0x06006403 RID: 25603 RVA: 0x0028073C File Offset: 0x0027E93C
		public void ListRealizeSeid194(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			if (avatar.buffmag.GetBuffSum(this.getSeidJson(seid)["value1"].I) > 0)
			{
				Dictionary<int, int> tempDunSu = avatar.fightTemp.tempDunSu;
				int key = this.buffID;
				tempDunSu[key] -= this.getSeidJson(seid)["value2"].I;
			}
		}

		// Token: 0x06006404 RID: 25604 RVA: 0x002807A8 File Offset: 0x0027E9A8
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

		// Token: 0x06006405 RID: 25605 RVA: 0x002808A8 File Offset: 0x0027EAA8
		public void ListRealizeSeid198(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int nowSkillUsedLingQiSum = RoundManager.instance.NowSkillUsedLingQiSum;
			if (flag[0] > 0)
			{
				flag[0] = flag[0] + buffInfo[1] * nowSkillUsedLingQiSum;
			}
		}

		// Token: 0x06006406 RID: 25606 RVA: 0x002808E8 File Offset: 0x0027EAE8
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

		// Token: 0x06006407 RID: 25607 RVA: 0x00044CC7 File Offset: 0x00042EC7
		public void ListRealizeSeid200(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			avatar.jieyin.AddJinMai((int)this.getSeidJson(seid)["value1"].n * buffInfo[1]);
		}

		// Token: 0x06006408 RID: 25608 RVA: 0x00044CF3 File Offset: 0x00042EF3
		public void ListRealizeSeid201(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			avatar.jieyin.AddYiZhi((int)this.getSeidJson(seid)["value1"].n * buffInfo[1]);
		}

		// Token: 0x06006409 RID: 25609 RVA: 0x00044D1F File Offset: 0x00042F1F
		public void ListRealizeSeid202(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			avatar.jieyin.AddHuaYing((int)this.getSeidJson(seid)["value1"].n * buffInfo[1]);
		}

		// Token: 0x0600640A RID: 25610 RVA: 0x00044D4B File Offset: 0x00042F4B
		public void ListRealizeSeid203(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			avatar.jieyin.AddJinDanHP((int)this.getSeidJson(seid)["value1"].n * buffInfo[1]);
		}

		// Token: 0x0600640B RID: 25611 RVA: 0x0028098C File Offset: 0x0027EB8C
		public void ListRealizeSeid208(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			float num = this.getSeidJson(seid)["value1"].n / 100f;
			int num2 = (int)((float)(avatar.jieyin.JinDanHP_Max - avatar.jieyin.JinDanHP) * num);
			avatar.jieyin.AddJinDanHP(num2);
		}

		// Token: 0x0600640C RID: 25612 RVA: 0x002809E0 File Offset: 0x0027EBE0
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

		// Token: 0x0600640D RID: 25613 RVA: 0x00280ACC File Offset: 0x0027ECCC
		public void ListRealizeSeid217(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int num = avatar.shengShi - avatar.OtherAvatar.shengShi;
			if (num > 0)
			{
				flag[0] = flag[0] + num;
			}
		}

		// Token: 0x0600640E RID: 25614 RVA: 0x00044D77 File Offset: 0x00042F77
		public void ListRealizeSeid300(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			flag[0] = 0;
		}

		// Token: 0x0600640F RID: 25615 RVA: 0x00280B04 File Offset: 0x0027ED04
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

		// Token: 0x06006410 RID: 25616 RVA: 0x00280BB4 File Offset: 0x0027EDB4
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

		// Token: 0x06006411 RID: 25617 RVA: 0x00280C4C File Offset: 0x0027EE4C
		public void ListRealizeSeid315(int seid, Avatar avatar, List<int> buffInfo, List<int> flag)
		{
			int i = this.getSeidJson(seid)["value1"].I;
			int num = this.getSeidJson(seid)["value2"].I * buffInfo[1];
			avatar.OtherAvatar.spell.addBuff(i, num);
		}

		// Token: 0x04005D20 RID: 23840
		public Dictionary<int, JSONObject> buffSeidList = new Dictionary<int, JSONObject>();

		// Token: 0x04005D21 RID: 23841
		public JObject NowBuffInfo = new JObject();

		// Token: 0x04005D22 RID: 23842
		public int buffID;

		// Token: 0x04005D23 RID: 23843
		public int _loopTime;

		// Token: 0x04005D24 RID: 23844
		public int _totalTime;

		// Token: 0x04005D25 RID: 23845
		public List<int> seid = new List<int>();

		// Token: 0x04005D26 RID: 23846
		private static Dictionary<string, MethodInfo> methodDict = new Dictionary<string, MethodInfo>();

		// Token: 0x04005D27 RID: 23847
		public static int _NeiShangLoopCount;

		// Token: 0x0200103B RID: 4155
		public enum AllBUFF
		{
			// Token: 0x04005D29 RID: 23849
			BUFF1 = 1,
			// Token: 0x04005D2A RID: 23850
			BUFF2,
			// Token: 0x04005D2B RID: 23851
			BUFF3,
			// Token: 0x04005D2C RID: 23852
			BUFF4,
			// Token: 0x04005D2D RID: 23853
			BUFF5,
			// Token: 0x04005D2E RID: 23854
			BUFF6,
			// Token: 0x04005D2F RID: 23855
			BUFF7,
			// Token: 0x04005D30 RID: 23856
			BUFF8,
			// Token: 0x04005D31 RID: 23857
			BUFF9,
			// Token: 0x04005D32 RID: 23858
			BUFF10,
			// Token: 0x04005D33 RID: 23859
			BUFF11,
			// Token: 0x04005D34 RID: 23860
			BUFF12,
			// Token: 0x04005D35 RID: 23861
			BUFF13,
			// Token: 0x04005D36 RID: 23862
			BUFF14,
			// Token: 0x04005D37 RID: 23863
			BUFF15,
			// Token: 0x04005D38 RID: 23864
			BUFF16,
			// Token: 0x04005D39 RID: 23865
			BUFF17,
			// Token: 0x04005D3A RID: 23866
			BUFF18,
			// Token: 0x04005D3B RID: 23867
			BUFF19,
			// Token: 0x04005D3C RID: 23868
			BUFF20,
			// Token: 0x04005D3D RID: 23869
			BUFF21,
			// Token: 0x04005D3E RID: 23870
			BUFF22,
			// Token: 0x04005D3F RID: 23871
			BUFF23,
			// Token: 0x04005D40 RID: 23872
			BUFF24,
			// Token: 0x04005D41 RID: 23873
			BUFF25,
			// Token: 0x04005D42 RID: 23874
			BUFF26,
			// Token: 0x04005D43 RID: 23875
			BUFF27,
			// Token: 0x04005D44 RID: 23876
			BUFF28,
			// Token: 0x04005D45 RID: 23877
			BUFF29,
			// Token: 0x04005D46 RID: 23878
			BUFF30,
			// Token: 0x04005D47 RID: 23879
			BUFF31,
			// Token: 0x04005D48 RID: 23880
			BUFF32,
			// Token: 0x04005D49 RID: 23881
			BUFF33,
			// Token: 0x04005D4A RID: 23882
			BUFF34,
			// Token: 0x04005D4B RID: 23883
			BUFF35,
			// Token: 0x04005D4C RID: 23884
			BUFF36,
			// Token: 0x04005D4D RID: 23885
			BUFF37,
			// Token: 0x04005D4E RID: 23886
			BUFF38,
			// Token: 0x04005D4F RID: 23887
			BUFF39,
			// Token: 0x04005D50 RID: 23888
			BUFF40,
			// Token: 0x04005D51 RID: 23889
			BUFF41,
			// Token: 0x04005D52 RID: 23890
			BUFF42,
			// Token: 0x04005D53 RID: 23891
			BUFF43,
			// Token: 0x04005D54 RID: 23892
			BUFF44,
			// Token: 0x04005D55 RID: 23893
			BUFF45,
			// Token: 0x04005D56 RID: 23894
			BUFF46,
			// Token: 0x04005D57 RID: 23895
			BUFF47,
			// Token: 0x04005D58 RID: 23896
			BUFF48,
			// Token: 0x04005D59 RID: 23897
			BUFF49,
			// Token: 0x04005D5A RID: 23898
			BUFF58 = 58,
			// Token: 0x04005D5B RID: 23899
			BUFF59,
			// Token: 0x04005D5C RID: 23900
			BUFF62 = 62,
			// Token: 0x04005D5D RID: 23901
			BUFF65 = 65,
			// Token: 0x04005D5E RID: 23902
			BUFF68 = 68,
			// Token: 0x04005D5F RID: 23903
			BUFF73 = 73,
			// Token: 0x04005D60 RID: 23904
			BUFF74,
			// Token: 0x04005D61 RID: 23905
			BUFF75,
			// Token: 0x04005D62 RID: 23906
			BUFF76,
			// Token: 0x04005D63 RID: 23907
			BUFF77,
			// Token: 0x04005D64 RID: 23908
			BUFF81 = 81,
			// Token: 0x04005D65 RID: 23909
			BUFF83 = 83,
			// Token: 0x04005D66 RID: 23910
			BUFF85 = 85,
			// Token: 0x04005D67 RID: 23911
			BUFF86,
			// Token: 0x04005D68 RID: 23912
			BUFF90 = 90,
			// Token: 0x04005D69 RID: 23913
			BUFF91,
			// Token: 0x04005D6A RID: 23914
			BUFF95 = 95,
			// Token: 0x04005D6B RID: 23915
			BUFF96,
			// Token: 0x04005D6C RID: 23916
			BUFF97,
			// Token: 0x04005D6D RID: 23917
			BUFF99 = 99,
			// Token: 0x04005D6E RID: 23918
			BUFF101 = 101,
			// Token: 0x04005D6F RID: 23919
			BUFF102,
			// Token: 0x04005D70 RID: 23920
			BUFF103,
			// Token: 0x04005D71 RID: 23921
			BUFF106 = 106,
			// Token: 0x04005D72 RID: 23922
			BUFF107,
			// Token: 0x04005D73 RID: 23923
			BUFF110 = 110,
			// Token: 0x04005D74 RID: 23924
			BUFF113 = 113,
			// Token: 0x04005D75 RID: 23925
			BUFF116 = 116,
			// Token: 0x04005D76 RID: 23926
			BUFF118 = 118,
			// Token: 0x04005D77 RID: 23927
			BUFF119,
			// Token: 0x04005D78 RID: 23928
			BUFF120,
			// Token: 0x04005D79 RID: 23929
			BUFF121,
			// Token: 0x04005D7A RID: 23930
			BUFF122,
			// Token: 0x04005D7B RID: 23931
			BUFF126 = 126,
			// Token: 0x04005D7C RID: 23932
			BUFF128 = 128,
			// Token: 0x04005D7D RID: 23933
			BUFF129,
			// Token: 0x04005D7E RID: 23934
			BUFF136 = 136,
			// Token: 0x04005D7F RID: 23935
			BUFF139 = 139,
			// Token: 0x04005D80 RID: 23936
			BUFF140,
			// Token: 0x04005D81 RID: 23937
			BUFF143 = 143,
			// Token: 0x04005D82 RID: 23938
			BUFF144,
			// Token: 0x04005D83 RID: 23939
			BUFF150 = 150,
			// Token: 0x04005D84 RID: 23940
			BUFF151,
			// Token: 0x04005D85 RID: 23941
			BUFF152,
			// Token: 0x04005D86 RID: 23942
			BUFF155 = 155,
			// Token: 0x04005D87 RID: 23943
			BUFF156,
			// Token: 0x04005D88 RID: 23944
			BUFF157,
			// Token: 0x04005D89 RID: 23945
			BUFF158,
			// Token: 0x04005D8A RID: 23946
			BUFF159,
			// Token: 0x04005D8B RID: 23947
			BUFF160,
			// Token: 0x04005D8C RID: 23948
			BUFF161,
			// Token: 0x04005D8D RID: 23949
			BUFF162,
			// Token: 0x04005D8E RID: 23950
			BUFF163,
			// Token: 0x04005D8F RID: 23951
			BUFF164,
			// Token: 0x04005D90 RID: 23952
			BUFF165,
			// Token: 0x04005D91 RID: 23953
			BUFF166,
			// Token: 0x04005D92 RID: 23954
			BUFF167,
			// Token: 0x04005D93 RID: 23955
			BUFF168,
			// Token: 0x04005D94 RID: 23956
			BUFF169,
			// Token: 0x04005D95 RID: 23957
			BUFF170,
			// Token: 0x04005D96 RID: 23958
			BUFF171,
			// Token: 0x04005D97 RID: 23959
			BUFF172,
			// Token: 0x04005D98 RID: 23960
			BUFF173,
			// Token: 0x04005D99 RID: 23961
			BUFF174,
			// Token: 0x04005D9A RID: 23962
			BUFF175,
			// Token: 0x04005D9B RID: 23963
			BUFF176,
			// Token: 0x04005D9C RID: 23964
			BUFF177,
			// Token: 0x04005D9D RID: 23965
			BUFF178,
			// Token: 0x04005D9E RID: 23966
			BUFF180 = 180,
			// Token: 0x04005D9F RID: 23967
			BUFF181,
			// Token: 0x04005DA0 RID: 23968
			BUFF182,
			// Token: 0x04005DA1 RID: 23969
			BUFF189 = 189,
			// Token: 0x04005DA2 RID: 23970
			BUFF204 = 204,
			// Token: 0x04005DA3 RID: 23971
			BUFF205,
			// Token: 0x04005DA4 RID: 23972
			BUFF206,
			// Token: 0x04005DA5 RID: 23973
			BUFF207,
			// Token: 0x04005DA6 RID: 23974
			BUFF209 = 209,
			// Token: 0x04005DA7 RID: 23975
			BUFF210,
			// Token: 0x04005DA8 RID: 23976
			BUFF211,
			// Token: 0x04005DA9 RID: 23977
			BUFF213 = 213,
			// Token: 0x04005DAA RID: 23978
			BUFF214,
			// Token: 0x04005DAB RID: 23979
			BUFF215,
			// Token: 0x04005DAC RID: 23980
			BUFF216
		}
	}
}
