using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Fungus;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using YSGame;
using YSGame.Fight;

namespace GUIPackage
{
	// Token: 0x02000D5C RID: 3420
	[Serializable]
	public class Skill
	{
		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06005197 RID: 20887 RVA: 0x0003ABA2 File Offset: 0x00038DA2
		// (set) Token: 0x06005198 RID: 20888 RVA: 0x0003ABB0 File Offset: 0x00038DB0
		public Texture2D skill_Icon
		{
			get
			{
				this.InitImage();
				return this._skill_Icon;
			}
			set
			{
				this._skill_Icon = value;
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06005199 RID: 20889 RVA: 0x0003ABB9 File Offset: 0x00038DB9
		// (set) Token: 0x0600519A RID: 20890 RVA: 0x0003ABC7 File Offset: 0x00038DC7
		public Texture2D SkillPingZhi
		{
			get
			{
				this.InitImage();
				return this._SkillPingZhi;
			}
			set
			{
				this._SkillPingZhi = value;
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x0600519B RID: 20891 RVA: 0x0003ABD0 File Offset: 0x00038DD0
		// (set) Token: 0x0600519C RID: 20892 RVA: 0x0003ABDE File Offset: 0x00038DDE
		public Sprite skillIconSprite
		{
			get
			{
				this.InitImage();
				return this._skillIconSprite;
			}
			set
			{
				this._skillIconSprite = value;
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x0600519D RID: 20893 RVA: 0x0003ABE7 File Offset: 0x00038DE7
		// (set) Token: 0x0600519E RID: 20894 RVA: 0x0003ABF5 File Offset: 0x00038DF5
		public Sprite SkillPingZhiSprite
		{
			get
			{
				this.InitImage();
				return this._SkillPingZhiSprite;
			}
			set
			{
				this._SkillPingZhiSprite = value;
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x0600519F RID: 20895 RVA: 0x0003ABFE File Offset: 0x00038DFE
		// (set) Token: 0x060051A0 RID: 20896 RVA: 0x0003AC0C File Offset: 0x00038E0C
		public Sprite newSkillPingZhiSprite
		{
			get
			{
				this.InitImage();
				return this._newSkillPingZhiSprite;
			}
			set
			{
				this._newSkillPingZhiSprite = value;
			}
		}

		// Token: 0x060051A1 RID: 20897 RVA: 0x00220134 File Offset: 0x0021E334
		private static void InitMethod(string methodName)
		{
			if (!Skill.methodDict.ContainsKey(methodName))
			{
				MethodInfo method = typeof(Skill).GetMethod(methodName);
				Skill.methodDict.Add(methodName, method);
			}
		}

		// Token: 0x060051A2 RID: 20898 RVA: 0x0022016C File Offset: 0x0021E36C
		public static bool IsSkillType(int skillID, int SkillType)
		{
			bool result = false;
			foreach (JSONObject jsonobject in jsonData.instance.skillJsonData[string.Concat(skillID)]["AttackType"].list)
			{
				if (SkillType == (int)jsonobject.n)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060051A3 RID: 20899 RVA: 0x002201EC File Offset: 0x0021E3EC
		public Dictionary<int, int> getSkillCast(Avatar _attaker)
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>(this.skillCast);
			using (List<int>.Enumerator enumerator = _skillJsonData.DataDict[this.skill_ID].seid.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == 97)
					{
						List<int> list = _attaker.fightTemp.NowRoundUsedSkills.FindAll((int aaa) => aaa == this.skill_ID);
						int num = (int)this.getSeidJson(97)["value1"].n;
						if (dictionary.ContainsKey(num))
						{
							Dictionary<int, int> dictionary2 = dictionary;
							int key = num;
							dictionary2[key] += list.Count;
						}
						else if (list.Count > 0)
						{
							dictionary[num] = list.Count;
						}
					}
				}
			}
			List<List<int>> buffBySeid = _attaker.buffmag.getBuffBySeid(140);
			if (buffBySeid.Count > 0)
			{
				foreach (List<int> list2 in buffBySeid)
				{
					if (this.skillCast.Count == 0)
					{
						break;
					}
					int key2 = list2[2];
					List<int> list3 = new List<int>();
					Skill.SetSkillFlag(list3, 0, this.skill_ID);
					if (jsonData.instance.Buff[key2].CanRealized(_attaker, list3, list2))
					{
						BuffSeidJsonData140 buffSeidJsonData = BuffSeidJsonData140.DataDict[key2];
						if (Skill.IsSkillType(this.skill_ID, buffSeidJsonData.value1))
						{
							Dictionary<int, int> dictionary2 = dictionary;
							int key = buffSeidJsonData.value3;
							dictionary2[key] += buffSeidJsonData.value2;
							if (dictionary[buffSeidJsonData.value3] < 0)
							{
								dictionary[buffSeidJsonData.value3] = 0;
							}
						}
					}
				}
			}
			List<List<int>> buffBySeid2 = _attaker.buffmag.getBuffBySeid(144);
			if (buffBySeid2.Count > 0)
			{
				foreach (List<int> list4 in buffBySeid2)
				{
					int key3 = list4[2];
					List<int> list5 = new List<int>();
					Skill.SetSkillFlag(list5, 0, this.skill_ID);
					if (jsonData.instance.Buff[key3].CanRealized(_attaker, list5, list4))
					{
						BuffSeidJsonData144 buffSeidJsonData2 = BuffSeidJsonData144.DataDict[key3];
						if (_skillJsonData.DataDict[this.skill_ID].Skill_ID == buffSeidJsonData2.value1)
						{
							Dictionary<int, int> dictionary2 = dictionary;
							int key = buffSeidJsonData2.value3;
							dictionary2[key] += buffSeidJsonData2.value2;
							if (dictionary[buffSeidJsonData2.value3] < 0)
							{
								dictionary[buffSeidJsonData2.value3] = 0;
							}
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060051A4 RID: 20900 RVA: 0x002204F4 File Offset: 0x0021E6F4
		public Skill(int id, int level, int max)
		{
			this.skillInit(id, level, max);
		}

		// Token: 0x060051A5 RID: 20901 RVA: 0x00220570 File Offset: 0x0021E770
		private void InitImage()
		{
			if (!this.initedImage)
			{
				this.initedImage = true;
				if (this.IsStaticSkill)
				{
					Texture2D texture2D = ResManager.inst.LoadTexture2D("StaticSkill Icon/" + Tools.instance.getStaticSkillIDByKey(this.skill_ID));
					if (texture2D)
					{
						this.skill_Icon = texture2D;
					}
					else
					{
						this.skill_Icon = ResManager.inst.LoadTexture2D("StaticSkill Icon/0");
					}
					Sprite sprite = ResManager.inst.LoadSprite("StaticSkill Icon/" + Tools.instance.getStaticSkillIDByKey(this.skill_ID));
					if (sprite)
					{
						this.skillIconSprite = sprite;
					}
					else
					{
						this.skillIconSprite = ResManager.inst.LoadSprite("StaticSkill Icon/0");
					}
				}
				else
				{
					Texture2D texture2D2 = ResManager.inst.LoadTexture2D("Skill Icon/" + Tools.instance.getSkillIDByKey(this.skill_ID));
					if (texture2D2)
					{
						this.skill_Icon = texture2D2;
					}
					else
					{
						this.skill_Icon = ResManager.inst.LoadTexture2D("Skill Icon/0");
					}
					Sprite sprite2 = ResManager.inst.LoadSprite("Skill Icon/" + Tools.instance.getSkillIDByKey(this.skill_ID));
					if (sprite2)
					{
						this.skillIconSprite = sprite2;
					}
					else
					{
						this.skillIconSprite = ResManager.inst.LoadSprite("Skill Icon/0");
					}
				}
				this.SkillPingZhi = ResManager.inst.LoadTexture2D("Ui Icon/tab/skill" + this.SkillQuality);
				this.SkillPingZhiSprite = ResManager.inst.LoadSprite("Ui Icon/tab/skill" + this.SkillQuality);
				this.newSkillPingZhiSprite = ResManager.inst.LoadSprite("NewUI/Inventory/Icon/MCS_icon_qualityup_" + this.SkillQuality * 2);
			}
		}

		// Token: 0x060051A6 RID: 20902 RVA: 0x00220754 File Offset: 0x0021E954
		public void skillInit(int id, int level, int max)
		{
			if (!_skillJsonData.DataDict.ContainsKey(id))
			{
				throw new Exception(string.Format("技能表中不存在id为{0}的技能，请检查配表", id));
			}
			this.IsStaticSkill = false;
			_skillJsonData skillJsonData = _skillJsonData.DataDict[id];
			if (skillJsonData == null)
			{
				return;
			}
			this.skill_Name = skillJsonData.name;
			this.skill_ID = id;
			this.SkillID = skillJsonData.Skill_ID;
			this.Skill_Lv = skillJsonData.Skill_Lv;
			if (this.Skill_Lv < 1 || this.Skill_Lv > 5)
			{
				throw new Exception(string.Format("技能表中id为[{0}] 名字为[{1}]的技能出现了等级不正确异常，Skill_Lv必须在1-5之间，当前值为:{2}，请检查配表", id, this.skill_Name, this.Skill_Lv));
			}
			this.skill_Desc = skillJsonData.descr;
			this.skill_level = level;
			this.Max_level = max;
			this.CoolDown = 10000f;
			this.CurCD = 0f;
			this.Damage = skillJsonData.HP;
			this.skillCast = new Dictionary<int, int>();
			if (skillJsonData.skill_CastType.Count == skillJsonData.skill_Cast.Count)
			{
				for (int i = 0; i < skillJsonData.skill_CastType.Count; i++)
				{
					int key = skillJsonData.skill_CastType[i];
					this.skillCast.Add(key, skillJsonData.skill_Cast[i]);
				}
				this.skillSameCast = new Dictionary<int, int>();
				for (int j = 0; j < skillJsonData.skill_SameCastNum.Count; j++)
				{
					this.skillSameCast.Add(j, skillJsonData.skill_SameCastNum[j]);
				}
				this.SkillQuality = skillJsonData.Skill_LV;
				this.ColorIndex = this.SkillQuality * 2 - 1;
				return;
			}
			throw new Exception(string.Format("技能表中id为[{0}] 名字为[{1}]的技能出现了数组越界异常，skill_CastType与skill_Cast的数组长度必须保持一致，请检查配表", id, this.skill_Name));
		}

		// Token: 0x060051A7 RID: 20903 RVA: 0x0022090C File Offset: 0x0021EB0C
		public Skill()
		{
			this.skill_ID = -1;
		}

		// Token: 0x060051A8 RID: 20904 RVA: 0x00220988 File Offset: 0x0021EB88
		public void initStaticSkill(int id, int level, int max)
		{
			if (!StaticSkillJsonData.DataDict.ContainsKey(id))
			{
				throw new Exception(string.Format("功法表中不存在id为{0}的功法，请检查配表", id));
			}
			StaticSkillJsonData staticSkillJsonData = StaticSkillJsonData.DataDict[id];
			if (staticSkillJsonData == null)
			{
				return;
			}
			this.IsStaticSkill = true;
			this.skill_Name = staticSkillJsonData.name;
			this.skill_ID = id;
			this.SkillID = staticSkillJsonData.Skill_ID;
			this.Skill_Lv = staticSkillJsonData.Skill_Lv;
			if (this.Skill_Lv < 1 || this.Skill_Lv > 5)
			{
				throw new Exception(string.Format("功法表中id为[{0}] 名字为[{1}]的功法出现了等级不正确异常，Skill_Lv必须在1-5之间，当前值为:{2}，请检查配表", id, this.skill_Name, this.Skill_Lv));
			}
			this.skill_Desc = staticSkillJsonData.descr;
			this.skill_level = level;
			this.Max_level = max;
			this.CoolDown = 10000f;
			this.CurCD = 0f;
			this.SkillQuality = staticSkillJsonData.Skill_LV;
			this.ColorIndex = this.SkillQuality * 2 - 1;
		}

		// Token: 0x060051A9 RID: 20905 RVA: 0x0003AC15 File Offset: 0x00038E15
		public Skill Clone()
		{
			return base.MemberwiseClone() as Skill;
		}

		// Token: 0x060051AA RID: 20906 RVA: 0x00220A80 File Offset: 0x0021EC80
		public SkillCanUseType CanUse(Entity _attaker, Entity _receiver, bool showError = true, string uuid = "")
		{
			Avatar avatar = (Avatar)_attaker;
			Avatar avatar2 = (Avatar)_receiver;
			if (this.CurCD != 0f)
			{
				if (avatar.isPlayer() && showError)
				{
					UIPopTip.Inst.Pop(Tools.getStr("shangweilengque"), PopTipIconType.叹号);
				}
				return SkillCanUseType.尚未冷却不能使用;
			}
			if (avatar.HP <= 0 || avatar.OtherAvatar.HP <= 0)
			{
				return SkillCanUseType.角色死亡不能使用;
			}
			if (avatar.state == 1 || avatar.OtherAvatar.state == 1)
			{
				return SkillCanUseType.角色死亡不能使用;
			}
			if (avatar.state != 3)
			{
				return SkillCanUseType.非自己回合不能使用;
			}
			if (avatar.buffmag.HasBuffSeid(83))
			{
				foreach (List<int> list in avatar.buffmag.getBuffBySeid(83))
				{
					int value = BuffSeidJsonData83.DataDict[list[2]].value1;
					if (avatar.fightTemp.NowRoundUsedSkills.Count >= value && jsonData.instance.Buff[list[2]].CanRealized(avatar, null, list))
					{
						if (avatar.isPlayer() && showError)
						{
							UIPopTip.Inst.Pop(Tools.getStr("yidaodaozuida"), PopTipIconType.叹号);
						}
						return SkillCanUseType.超过最多使用次数不能使用;
					}
				}
			}
			if (avatar.isPlayer() && avatar.OtherAvatar.buffmag.HasBuffSeid(103))
			{
				List<List<int>> buffBySeid = avatar.OtherAvatar.buffmag.getBuffBySeid(103);
				_skillJsonData skillJsonData = _skillJsonData.DataDict[this.skill_ID];
				foreach (List<int> list2 in buffBySeid)
				{
					if (jsonData.instance.Buff[list2[2]].CanRealized(avatar, null, list2))
					{
						Dictionary<int, int> nowCacheLingQiIntDict = UIFightPanel.Inst.CacheLingQiController.GetNowCacheLingQiIntDict();
						bool flag = skillJsonData.skill_CastType.Contains(5);
						if (nowCacheLingQiIntDict.ContainsKey(5) && !flag)
						{
							if (avatar.isPlayer() && showError)
							{
								UIPopTip.Inst.Pop("魔气无法当做同系灵气使用", PopTipIconType.叹号);
							}
							return SkillCanUseType.魔气无法当做同系灵气不能使用;
						}
					}
				}
			}
			foreach (int num in _skillJsonData.DataDict[this.skill_ID].seid)
			{
				if (num == 45 || num == 46 || num == 47)
				{
					JSONObject seidJson = this.getSeidJson(num);
					int i = seidJson["value1"].I;
					bool flag2 = true;
					if (avatar.buffmag.HasBuff(i) && avatar.buffmag.getBuffByID(i)[0][1] >= seidJson["value2"].I)
					{
						flag2 = false;
					}
					if (flag2)
					{
						if (avatar.isPlayer() && showError)
						{
							UIPopTip.Inst.Pop(Tools.getStr("Buffcengshubuzu"), PopTipIconType.叹号);
						}
						return SkillCanUseType.Buff层数不足无法使用;
					}
				}
				if (num == 57)
				{
					JSONObject seidJson2 = this.getSeidJson(num);
					JSONObject jsonobject = seidJson2["value1"];
					JSONObject jsonobject2 = seidJson2["value2"];
					for (int j = 0; j < jsonobject.Count; j++)
					{
						if (!avatar.buffmag.HasBuff(jsonobject[j].I))
						{
							if (avatar.isPlayer() && showError)
							{
								UIPopTip.Inst.Pop(Tools.getStr("Buffcengshubuzu"), PopTipIconType.叹号);
							}
							return SkillCanUseType.Buff层数不足无法使用;
						}
						if (avatar.buffmag.getBuffByID(jsonobject[j].I)[0][1] < jsonobject2[j].I)
						{
							if (avatar.isPlayer() && showError)
							{
								UIPopTip.Inst.Pop(Tools.getStr("Buffcengshubuzu"), PopTipIconType.叹号);
							}
							return SkillCanUseType.Buff层数不足无法使用;
						}
					}
				}
				if (num == 93 && avatar.dunSu < avatar.OtherAvatar.dunSu)
				{
					if (avatar.isPlayer() && showError)
					{
						UIPopTip.Inst.Pop("遁速需大于敌人", PopTipIconType.叹号);
					}
					return SkillCanUseType.遁速不足无法使用;
				}
				if (num == 76)
				{
					bool flag3 = true;
					int i2 = this.getSeidJson(num)["value1"].I;
					if (avatar.HP <= i2)
					{
						flag3 = false;
					}
					if (flag3)
					{
						if (avatar.isPlayer() && showError)
						{
							UIPopTip.Inst.Pop(Tools.getStr("xieliangdiyu").Replace("{X}", i2.ToString()), PopTipIconType.叹号);
						}
						return SkillCanUseType.血量太高无法使用;
					}
				}
				if (num == 109)
				{
					bool flag4 = true;
					int i3 = this.getSeidJson(num)["value1"].I;
					if (avatar.buffmag.HasXTypeBuff(i3))
					{
						flag4 = false;
					}
					if (flag4)
					{
						if (avatar.isPlayer() && showError)
						{
							UIPopTip.Inst.Pop(Tools.getStr("Buffcengshubuzu"), PopTipIconType.叹号);
						}
						return SkillCanUseType.Buff层数不足无法使用;
					}
				}
				if (num == 111)
				{
					bool flag5 = true;
					int i4 = this.getSeidJson(num)["value1"].I;
					if (avatar.isPlayer())
					{
						if ((ulong)(avatar.shouYuan - avatar.age) - (ulong)((long)i4) > 0UL)
						{
							flag5 = false;
						}
					}
					else if (NpcJieSuanManager.inst.GetNpcShengYuTime(Tools.instance.MonstarID) - i4 > 0)
					{
						flag5 = false;
					}
					if (flag5)
					{
						if (avatar.isPlayer() && showError)
						{
							UIPopTip.Inst.Pop("寿元不足无法使用", PopTipIconType.叹号);
						}
						return SkillCanUseType.寿元不足无法使用;
					}
				}
				if (num == 140)
				{
					JSONObject jsonobject3 = this.getSeidJson(num)["value1"];
					int count = jsonobject3.Count;
					int num2 = 0;
					for (int k = 0; k < count; k++)
					{
						if (avatar.buffmag.HasBuff(jsonobject3[k].I))
						{
							num2++;
						}
					}
					if (num2 < count)
					{
						if (showError && avatar.isPlayer())
						{
							UIPopTip.Inst.Pop("需要的buff不足无法释放", PopTipIconType.叹号);
						}
						return SkillCanUseType.Buff种类不足无法使用;
					}
				}
				if (num == 145)
				{
					bool flag6 = true;
					int i5 = this.getSeidJson(num)["value1"].I;
					if ((float)avatar.HP / (float)avatar.HP_Max * 100f <= (float)i5)
					{
						flag6 = false;
					}
					if (flag6)
					{
						if (avatar.isPlayer() && showError)
						{
							UIPopTip.Inst.Pop(Tools.getStr("xieliangdiyu").Replace("{X}", i5.ToString()), PopTipIconType.叹号);
						}
						return SkillCanUseType.血量太高无法使用;
					}
				}
				if (num == 147)
				{
					JSONObject seidJson3 = this.getSeidJson(num);
					int buffRoundByID = this.getTargetAvatar(147, _attaker as Avatar).buffmag.GetBuffRoundByID(seidJson3["value1"].I);
					if (!Tools.symbol(seidJson3["panduan"].str, buffRoundByID, seidJson3["value2"].I))
					{
						if (avatar.isPlayer() && showError)
						{
							UIPopTip.Inst.Pop("未满足释放条件", PopTipIconType.叹号);
						}
						return SkillCanUseType.Buff层数不足无法使用;
					}
				}
				if (num == 163)
				{
					bool flag7 = true;
					int i6 = this.getSeidJson(num)["value1"].I;
					if (avatar.shengShi > i6)
					{
						flag7 = false;
					}
					if (flag7)
					{
						if (avatar.isPlayer() && showError)
						{
							UIPopTip.Inst.Pop(string.Format("神识大于{0}才能使用", i6), PopTipIconType.叹号);
						}
						return SkillCanUseType.神识不足无法使用;
					}
				}
			}
			if (this.ItemAddSeid != null)
			{
				List<JSONObject> list3 = this.ItemAddSeid.list;
				if (list3.Count > 1)
				{
					for (int l = 0; l < list3.Count; l++)
					{
						if (list3[l]["id"].I == 145 && (float)avatar.HP / (float)avatar.HP_Max * 100f > list3[l]["value1"].n)
						{
							if (avatar.isPlayer() && showError)
							{
								UIPopTip.Inst.Pop(string.Format("生命值{0}%以下才能使用", list3[l]["value1"].n), PopTipIconType.叹号);
							}
							return SkillCanUseType.血量太高无法使用;
						}
					}
				}
			}
			using (List<int>.Enumerator enumerator2 = _skillJsonData.DataDict[this.skill_ID].seid.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current == 68 && avatar.fightTemp.NowRoundUsedSkills.Count > 0)
					{
						if (avatar.isPlayer() && showError)
						{
							UIPopTip.Inst.Pop(Tools.getStr("canNotUseSkill1"), PopTipIconType.叹号);
						}
						return SkillCanUseType.本回合使用过其他技能无法使用;
					}
				}
			}
			foreach (KeyValuePair<int, int> keyValuePair in this.getSkillCast(avatar))
			{
				if (avatar.cardMag.HasNoEnoughNum(keyValuePair.Key, keyValuePair.Value))
				{
					if (avatar.isPlayer() && showError)
					{
						UIPopTip.Inst.Pop(Tools.getStr("lingqikapaibuzu"), PopTipIconType.叹号);
					}
					return SkillCanUseType.灵气不足无法使用;
				}
			}
			List<int> list4 = this.getremoveCastNum(avatar);
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			foreach (KeyValuePair<int, int> keyValuePair2 in this.skillSameCast)
			{
				bool flag8 = false;
				for (int m = 0; m < list4.Count; m++)
				{
					if (m != 5 && list4[m] - keyValuePair2.Value >= 0 && !dictionary.ContainsKey(m))
					{
						dictionary.Add(m, keyValuePair2.Value);
						flag8 = true;
						break;
					}
				}
				if (!flag8)
				{
					if (avatar.isPlayer() && showError)
					{
						UIPopTip.Inst.Pop(Tools.getStr("tongxilinqikapaibuzu"), PopTipIconType.叹号);
					}
					return SkillCanUseType.灵气不足无法使用;
				}
			}
			return SkillCanUseType.可以使用;
		}

		// Token: 0x060051AB RID: 20907 RVA: 0x0022150C File Offset: 0x0021F70C
		public List<int> getremoveCastNum(Entity _attaker)
		{
			Avatar avatar = (Avatar)_attaker;
			List<int> list = avatar.crystal.ToListInt32();
			foreach (KeyValuePair<int, int> keyValuePair in this.getSkillCast(avatar))
			{
				if (!avatar.crystal.HasNoEnoughNum(keyValuePair.Key, keyValuePair.Value))
				{
					list[keyValuePair.Key] = avatar.crystal[keyValuePair.Key] - keyValuePair.Value;
				}
			}
			return list;
		}

		// Token: 0x060051AC RID: 20908 RVA: 0x002215B0 File Offset: 0x0021F7B0
		public void useCrystal(Entity _attaker)
		{
			Avatar avatar = (Avatar)_attaker;
			CardMag crystal = avatar.crystal;
			this.nowSkillUseCard.Clear();
			this.nowSkillIsChuFa.Clear();
			if (_attaker.isPlayer())
			{
				this.nowSkillUseCard = UIFightPanel.Inst.CacheLingQiController.GetNowCacheLingQiIntDict();
				using (Dictionary<int, int>.Enumerator enumerator = this.nowSkillUseCard.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<int, int> keyValuePair = enumerator.Current;
						avatar.UseCryStal(keyValuePair.Key, keyValuePair.Value);
					}
					return;
				}
			}
			foreach (KeyValuePair<int, int> keyValuePair2 in this.getSkillCast(avatar))
			{
				avatar.UseCryStal(keyValuePair2.Key, keyValuePair2.Value);
				this.nowSkillUseCard[keyValuePair2.Key] = keyValuePair2.Value;
			}
			List<int> list = this.getremoveCastNum(avatar);
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			foreach (KeyValuePair<int, int> keyValuePair3 in this.skillSameCast)
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i] - keyValuePair3.Value >= 0 && !dictionary.ContainsKey(i))
					{
						dictionary.Add(i, keyValuePair3.Value);
						avatar.UseCryStal(i, keyValuePair3.Value);
						break;
					}
				}
			}
			foreach (KeyValuePair<int, int> keyValuePair4 in dictionary)
			{
				if (!this.nowSkillUseCard.ContainsKey(keyValuePair4.Key))
				{
					this.nowSkillUseCard[keyValuePair4.Key] = 0;
				}
				Dictionary<int, int> dictionary2 = this.nowSkillUseCard;
				int key = keyValuePair4.Key;
				dictionary2[key] += keyValuePair4.Value;
			}
		}

		// Token: 0x060051AD RID: 20909 RVA: 0x002217E8 File Offset: 0x0021F9E8
		public int getNomelCastNum(Avatar attaker)
		{
			int num = 0;
			foreach (KeyValuePair<int, int> keyValuePair in this.getSkillCast(attaker))
			{
				num += keyValuePair.Value;
			}
			return num;
		}

		// Token: 0x060051AE RID: 20910 RVA: 0x00221844 File Offset: 0x0021FA44
		public int GetSameCastNum(Avatar attaker)
		{
			int num = 0;
			foreach (KeyValuePair<int, int> keyValuePair in this.skillSameCast)
			{
				num += keyValuePair.Value;
			}
			return num;
		}

		// Token: 0x060051AF RID: 20911 RVA: 0x002218A0 File Offset: 0x0021FAA0
		public void ShowScroll(Entity _attaker, Entity _receiver, int damage)
		{
			GameObject gameObject = (GameObject)_attaker.renderObj;
			GameObject gameObject2 = (GameObject)_receiver.renderObj;
			string text = "<color=#aeee7f>" + gameObject.GetComponent<GameEntity>().entity_name + "</color>";
			string text2 = "<color=#f27a2b>" + gameObject2.GetComponent<GameEntity>().entity_name + "</color>";
			string text3 = this.skill_Name.RemoveNumber();
			if (string.IsNullOrWhiteSpace(text3))
			{
				text3 = this.skill_Name;
			}
			string text4 = "<color=#f6c73c>" + text3 + "</color>";
			string text5;
			if (damage > 0)
			{
				text5 = ",造成<color=#ffec96>" + damage + "</color>点伤害";
			}
			else if (damage < 0)
			{
				text5 = ",回复<color=#ffec96>" + damage + "</color>点生命";
			}
			else
			{
				text5 = "。";
			}
			string text6;
			if (_attaker == _receiver)
			{
				text6 = text + "释放了" + text4 + text5;
			}
			else
			{
				text6 = string.Concat(new string[]
				{
					text,
					"对",
					text2,
					"释放了",
					text4,
					text5
				});
			}
			UIFightPanel.Inst.FightJiLu.AddText(text6);
		}

		// Token: 0x060051B0 RID: 20912 RVA: 0x002219D4 File Offset: 0x0021FBD4
		public Dictionary<int, int> GetSkillSameCast()
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			foreach (KeyValuePair<int, int> keyValuePair in this.nowSkillUseCard)
			{
				dictionary[keyValuePair.Key] = keyValuePair.Value;
			}
			foreach (KeyValuePair<int, int> keyValuePair2 in this.skillCast)
			{
				Dictionary<int, int> dictionary2 = dictionary;
				int value = keyValuePair2.Value;
				dictionary2[value] -= keyValuePair2.Value;
			}
			return dictionary;
		}

		// Token: 0x060051B1 RID: 20913 RVA: 0x00221A9C File Offset: 0x0021FC9C
		public bool setSeidNum(List<int> TempSeid, List<int> infoFlag, int _index)
		{
			if (infoFlag[4] > 0)
			{
				if (!TempSeid.Contains(139))
				{
					return true;
				}
				int num = TempSeid.IndexOf(139);
				if (_index > num)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060051B2 RID: 20914 RVA: 0x00221AD8 File Offset: 0x0021FCD8
		public void triggerStartSeid(List<int> SeidList, List<int> infoFlag, Entity _attaker, Entity _receiver, int type)
		{
			List<int> TempSeid = new List<int>();
			SeidList.ForEach(delegate(int aa)
			{
				TempSeid.Add(aa);
			});
			int num = 0;
			int num2 = -1;
			foreach (int num3 in TempSeid)
			{
				try
				{
					if (num2 > 0)
					{
						if (TempSeid.IndexOf(num3) < num2)
						{
							continue;
						}
						infoFlag[2] = 0;
						num2 = -1;
					}
					if (this.setSeidNum(TempSeid, infoFlag, num))
					{
						for (int i = 0; i < infoFlag[4]; i++)
						{
							this.realizeSeid(num3, infoFlag, _attaker, _receiver, type);
						}
					}
					else
					{
						this.realizeSeid(num3, infoFlag, _attaker, _receiver, type);
					}
					if (infoFlag[2] == 1)
					{
						if (TempSeid.Contains(117))
						{
							for (int j = TempSeid.IndexOf(num3); j < TempSeid.Count; j++)
							{
								if (TempSeid[j] == 117)
								{
									num2 = j;
									break;
								}
							}
						}
						if (num2 < 0)
						{
							break;
						}
					}
				}
				catch (Exception ex)
				{
					string text = "";
					for (int k = 0; k < infoFlag.Count; k++)
					{
						text += string.Format(" {0}", infoFlag[0]);
					}
					Debug.LogError(string.Concat(new object[]
					{
						"检测到技能错误！错误 SkillID:",
						this.skill_ID,
						" 技能特性:",
						num3,
						"额外数据：",
						text
					}));
					Debug.LogError("异常信息:" + ex.Message + "\n异常位置:" + ex.StackTrace);
					Debug.LogError(string.Format("{0}", ex));
				}
				num++;
			}
		}

		// Token: 0x060051B3 RID: 20915 RVA: 0x00221D00 File Offset: 0x0021FF00
		public void triggerBuffEndSeid(List<int> SeidList, List<int> infoFlag, Entity _attaker, Entity _receiver, int type)
		{
			List<int> TempSeid = new List<int>();
			SeidList.ForEach(delegate(int aa)
			{
				TempSeid.Add(aa);
			});
			int num = 0;
			foreach (int num2 in TempSeid)
			{
				try
				{
					if (infoFlag[2] == 1)
					{
						break;
					}
					if (this.setSeidNum(TempSeid, infoFlag, num))
					{
						for (int i = 0; i < infoFlag[4]; i++)
						{
							this.realizeBuffEndSeid(num2, infoFlag, _attaker, _receiver, type);
						}
					}
					else
					{
						this.realizeBuffEndSeid(num2, infoFlag, _attaker, _receiver, type);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError(string.Concat(new object[]
					{
						"检测到技能错误！错误 SkillID:",
						this.skill_ID,
						" 技能特性:",
						num2,
						"额外数据：",
						infoFlag.ToString()
					}));
					Debug.LogError(ex);
				}
				num++;
			}
		}

		// Token: 0x060051B4 RID: 20916 RVA: 0x00221E30 File Offset: 0x00220030
		public void triggerSkillFinalSeid(List<int> SeidList, List<int> infoFlag, Entity _attaker, Entity _receiver, int type)
		{
			List<int> TempSeid = new List<int>();
			SeidList.ForEach(delegate(int aa)
			{
				TempSeid.Add(aa);
			});
			int num = 0;
			foreach (int num2 in TempSeid)
			{
				try
				{
					if (this.setSeidNum(TempSeid, infoFlag, num))
					{
						for (int i = 0; i < infoFlag[4]; i++)
						{
							this.realizeFinalSeid(num2, infoFlag, _attaker, _receiver, type);
						}
					}
					else
					{
						this.realizeFinalSeid(num2, infoFlag, _attaker, _receiver, type);
					}
					if (infoFlag[2] == 1)
					{
						break;
					}
				}
				catch (Exception ex)
				{
					Debug.LogError(string.Concat(new object[]
					{
						"检测到技能错误！错误 SkillID:",
						this.skill_ID,
						" 技能特性:",
						num2,
						"额外数据：",
						infoFlag.ToString()
					}));
					Debug.LogError(ex);
				}
				num++;
			}
		}

		// Token: 0x060051B5 RID: 20917 RVA: 0x0003AC22 File Offset: 0x00038E22
		public static void SetSkillFlag(List<int> infoFlag, int damage, int skill_ID)
		{
			infoFlag.Add(damage);
			infoFlag.Add(skill_ID);
			infoFlag.Add(0);
			infoFlag.Add(0);
			infoFlag.Add(0);
		}

		// Token: 0x060051B6 RID: 20918 RVA: 0x00221F5C File Offset: 0x0022015C
		public List<int> VirtualPutingSkill(Entity _attaker, Entity _receiver, int type = 0)
		{
			Avatar avatar = (Avatar)_attaker;
			Avatar avatar2 = (Avatar)_receiver;
			this.NowSkillSeid.Clear();
			int num = this.Damage;
			int damage = this.Damage;
			List<int> list = new List<int>();
			Skill.SetSkillFlag(list, num, this.skill_ID);
			this.NowSkillSeid = Tools.JsonListToList(jsonData.instance.skillJsonData[string.Concat(this.skill_ID)]["seid"]);
			if (this.NowSkillSeid.Contains(999))
			{
				avatar2.spell.onBuffTickByType(46, list);
				avatar.spell.onBuffTickByType(9, list);
				avatar2.spell.onBuffTickByType(29, list);
				avatar.recvDamage(_attaker, avatar2, this.skill_ID, list[0], type);
				return list;
			}
			avatar.spell.onBuffTickByType(30, list);
			if (list[3] == 1)
			{
				List<int> list2 = Tools.JsonListToList(jsonData.instance.skillJsonData[string.Concat(this.skill_ID)]["seid"]);
				if (list2.Contains(29))
				{
					this.realizeSeid(29, list, _attaker, _receiver, type);
				}
				if (list2.Contains(5))
				{
					this.realizeSeid(5, list, _attaker, _receiver, type);
				}
				if (list2.Contains(6))
				{
					this.realizeSeid(6, list, _attaker, _receiver, type);
				}
				return list;
			}
			if (this.ItemAddSeid != null)
			{
				this.NowSkillSeid.Clear();
				this.NowSkillSeid.Add(29);
				List<JSONObject> list3 = this.ItemAddSeid.list;
				if (list3.Count > 1)
				{
					for (int i = 1; i < list3.Count; i++)
					{
						int i2 = list3[i]["id"].I;
						if (i2 == 145 && (float)avatar.HP / (float)avatar.HP_Max * 100f > list3[i]["value1"].n)
						{
							UIPopTip.Inst.Pop(string.Format("生命值{0}%以下才能使用", list3[i]["value1"].n), PopTipIconType.叹号);
							return list;
						}
						if (i2 == 79)
						{
							for (int j = 0; j < list3[i]["value1"].Count; j++)
							{
								avatar2.spell.addDBuff(list3[i]["value1"][j].I, list3[i]["value2"][j].I);
							}
						}
						if (i2 == 80)
						{
							for (int k = 0; k < list3[i]["value1"].Count; k++)
							{
								avatar.spell.addDBuff(list3[i]["value1"][k].I, list3[i]["value2"][k].I);
							}
						}
						if (i2 == 106)
						{
							for (int l = 0; l < list3[i]["value1"].Count; l++)
							{
								avatar.recvDamage(avatar, avatar, this.skill_ID, -list3[i]["value1"][l].I, 0);
							}
						}
						if (i2 == 11)
						{
							JSONObject jsonobject = list3[i]["AttackType"];
							int i3 = list3[i]["value1"].I;
							int i4 = list3[i]["value2"].I;
							int i5 = list3[i]["value3"].I;
							JSONObject arr = JSONObject.arr;
							arr.Add(999);
							_skillJsonData.DataDict[i5].AttackType = jsonobject.ToList();
							_skillJsonData.DataDict[i5].seid = arr.ToList();
							jsonData.instance.skillJsonData[i5.ToString()].SetField("AttackType", jsonobject);
							jsonData.instance.skillJsonData[i5.ToString()].SetField("seid", arr);
							Skill skill = new Skill(i5, (int)avatar.level, 5);
							skill.Damage = i4;
							for (int m = 0; m < i3; m++)
							{
								skill.PutingSkill(avatar, avatar2, type);
							}
						}
					}
				}
			}
			this.triggerStartSeid(this.NowSkillSeid, list, _attaker, _receiver, type);
			if (num > 0)
			{
				avatar.spell.onBuffTickByType(9, list);
				avatar2.spell.onBuffTickByType(29, list);
			}
			if (num > 0)
			{
				avatar.spell.onBuffTickByType(37, list);
			}
			this.triggerBuffEndSeid(this.NowSkillSeid, list, _attaker, _receiver, type);
			num = list[0];
			num = avatar.recvDamage(_attaker, avatar2, this.skill_ID, num, type);
			if (this.NowSkillSeid.Contains(11) && this.LateDamages != null)
			{
				foreach (LateDamage lateDamage in this.LateDamages)
				{
					List<int> list4 = new Skill(lateDamage.SkillId, (int)avatar.level, 5)
					{
						Damage = lateDamage.Damage
					}.PutingSkill(avatar, avatar2, type);
					if (list4.Count > 0)
					{
						num += list4[0];
					}
				}
				this.LateDamages = new List<LateDamage>();
			}
			list[0] = num;
			if (damage > 0)
			{
				avatar.spell.onBuffTickByType(40, list);
			}
			this.triggerSkillFinalSeid(this.NowSkillSeid, list, _attaker, _receiver, type);
			if (num > 0)
			{
				avatar2.OtherAvatar.spell.onBuffTickByType(33, list);
			}
			return list;
		}

		// Token: 0x060051B7 RID: 20919 RVA: 0x00222570 File Offset: 0x00220770
		public List<int> PutingSkill(Entity _attaker, Entity _receiver, int type = 0)
		{
			Avatar avatar = (Avatar)_attaker;
			Avatar avatar2 = (Avatar)_receiver;
			this.NowSkillSeid.Clear();
			int num = this.Damage;
			int damage = this.Damage;
			List<int> list = new List<int>();
			Skill.SetSkillFlag(list, num, this.skill_ID);
			this.NowSkillSeid = Tools.JsonListToList(jsonData.instance.skillJsonData[string.Concat(this.skill_ID)]["seid"]);
			if (this.NowSkillSeid.Contains(999))
			{
				avatar2.spell.onBuffTickByType(46, list);
				avatar.spell.onBuffTickByType(9, list);
				avatar2.spell.onBuffTickByType(29, list);
				avatar.recvDamage(_attaker, avatar2, this.skill_ID, list[0], type);
				return list;
			}
			avatar.spell.onBuffTickByType(30, list);
			if (list[3] == 1)
			{
				List<int> list2 = Tools.JsonListToList(jsonData.instance.skillJsonData[string.Concat(this.skill_ID)]["seid"]);
				if (list2.Contains(29))
				{
					this.realizeSeid(29, list, _attaker, _receiver, type);
				}
				if (list2.Contains(5))
				{
					this.realizeSeid(5, list, _attaker, _receiver, type);
				}
				if (list2.Contains(6))
				{
					this.realizeSeid(6, list, _attaker, _receiver, type);
				}
				return list;
			}
			if (this.ItemAddSeid != null)
			{
				this.NowSkillSeid.Clear();
				this.NowSkillSeid.Add(29);
				List<JSONObject> list3 = this.ItemAddSeid.list;
				if (list3.Count > 1)
				{
					for (int i = 1; i < list3.Count; i++)
					{
						int i2 = list3[i]["id"].I;
						if (i2 == 145 && (float)avatar.HP / (float)avatar.HP_Max * 100f > list3[i]["value1"].n)
						{
							UIPopTip.Inst.Pop(string.Format("生命值{0}%以下才能使用", list3[i]["value1"].n), PopTipIconType.叹号);
							return list;
						}
						if (i2 == 79)
						{
							for (int j = 0; j < list3[i]["value1"].Count; j++)
							{
								avatar2.spell.addDBuff(list3[i]["value1"][j].I, list3[i]["value2"][j].I);
							}
						}
						if (i2 == 80)
						{
							for (int k = 0; k < list3[i]["value1"].Count; k++)
							{
								avatar.spell.addDBuff(list3[i]["value1"][k].I, list3[i]["value2"][k].I);
							}
						}
						if (i2 == 106)
						{
							for (int l = 0; l < list3[i]["value1"].Count; l++)
							{
								avatar.recvDamage(avatar, avatar, this.skill_ID, -list3[i]["value1"][l].I, 0);
							}
						}
						if (i2 == 11)
						{
							JSONObject jsonobject = list3[i]["AttackType"];
							int i3 = list3[i]["value1"].I;
							int i4 = list3[i]["value2"].I;
							int i5 = list3[i]["value3"].I;
							JSONObject arr = JSONObject.arr;
							arr.Add(999);
							_skillJsonData.DataDict[i5].AttackType = jsonobject.ToList();
							_skillJsonData.DataDict[i5].seid = arr.ToList();
							jsonData.instance.skillJsonData[i5.ToString()].SetField("AttackType", jsonobject);
							jsonData.instance.skillJsonData[i5.ToString()].SetField("seid", arr);
							Skill skill = new Skill(i5, (int)avatar.level, 5);
							skill.Damage = i4;
							for (int m = 0; m < i3; m++)
							{
								skill.PutingSkill(avatar, avatar2, type);
							}
						}
					}
				}
			}
			this.triggerStartSeid(this.NowSkillSeid, list, _attaker, _receiver, type);
			if (num > 0)
			{
				avatar2.spell.onBuffTickByType(46, list);
				avatar.spell.onBuffTickByType(9, list);
				avatar2.spell.onBuffTickByType(29, list);
			}
			if (num > 0)
			{
				avatar.spell.onBuffTickByType(37, list);
			}
			this.triggerBuffEndSeid(this.NowSkillSeid, list, _attaker, _receiver, type);
			num = list[0];
			num = avatar.recvDamage(_attaker, avatar2, this.skill_ID, num, type);
			if (this.NowSkillSeid.Contains(11) && this.LateDamages != null)
			{
				foreach (LateDamage lateDamage in this.LateDamages)
				{
					List<int> list4 = new Skill(lateDamage.SkillId, (int)avatar.level, 5)
					{
						Damage = lateDamage.Damage
					}.PutingSkill(avatar, avatar2, type);
					if (list4.Count > 0)
					{
						num += list4[0];
					}
				}
				this.LateDamages = new List<LateDamage>();
			}
			list[0] = num;
			if (damage > 0)
			{
				avatar.spell.onBuffTickByType(40, list);
			}
			if (avatar.spell.UseSkillLateDict != null && avatar.spell.UseSkillLateDict.Count > 0)
			{
				foreach (int num2 in avatar.spell.UseSkillLateDict.Keys)
				{
					avatar.spell.addBuff(num2, avatar.spell.UseSkillLateDict[num2]);
				}
				avatar.spell.UseSkillLateDict = new Dictionary<int, int>();
			}
			this.triggerSkillFinalSeid(this.NowSkillSeid, list, _attaker, _receiver, type);
			if (num > 0)
			{
				avatar2.OtherAvatar.spell.onBuffTickByType(33, list);
			}
			return list;
		}

		// Token: 0x060051B8 RID: 20920 RVA: 0x0003AC47 File Offset: 0x00038E47
		public Avatar getTargetAvatar(int seid, Avatar attker)
		{
			if (this.getSeidJson(seid)["target"].I == 1)
			{
				return attker;
			}
			return attker.OtherAvatar;
		}

		// Token: 0x060051B9 RID: 20921 RVA: 0x00222C2C File Offset: 0x00220E2C
		public void SkillStartWait(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			attaker.fightTemp.WaitAttacker = attaker;
			attaker.fightTemp.WaitReceiver = receiver;
			attaker.fightTemp.WaitDamage = damage;
			attaker.fightTemp.WaitSkill = this;
			attaker.state = 5;
			bool flag = false;
			attaker.fightTemp.WaitSeid = new List<int>();
			foreach (JSONObject jsonobject in jsonData.instance.skillJsonData[string.Concat(this.skill_ID)]["seid"].list)
			{
				if (flag)
				{
					attaker.fightTemp.WaitSeid.Add(jsonobject.I);
				}
				if (jsonobject.I == seid)
				{
					flag = true;
				}
			}
			damage[2] = 1;
		}

		// Token: 0x060051BA RID: 20922 RVA: 0x00222D18 File Offset: 0x00220F18
		public void VirtualPuting(Entity _attaker, Entity _receiver, int type = 0, string uuid = "")
		{
			Avatar avatar = (Avatar)_attaker;
			Avatar avatar2 = (Avatar)_receiver;
			List<List<int>> list = new List<List<int>>();
			List<List<int>> list2 = new List<List<int>>();
			List<int> collection = new List<int>(avatar.UsedSkills);
			Dictionary<int, Dictionary<int, int>> dictionary = new Dictionary<int, Dictionary<int, int>>();
			Dictionary<int, Dictionary<int, int>> dictionary2 = new Dictionary<int, Dictionary<int, int>>();
			foreach (int key in avatar.BuffSeidFlag.Keys)
			{
				dictionary[key] = new Dictionary<int, int>(avatar.BuffSeidFlag[key]);
			}
			foreach (int key2 in avatar.OtherAvatar.BuffSeidFlag.Keys)
			{
				dictionary2[key2] = new Dictionary<int, int>(avatar.OtherAvatar.BuffSeidFlag[key2]);
			}
			foreach (List<int> list3 in avatar.bufflist)
			{
				List<int> list4 = new List<int>();
				foreach (int item in list3)
				{
					list4.Add(item);
				}
				list.Add(list4);
			}
			foreach (List<int> list5 in avatar.OtherAvatar.bufflist)
			{
				List<int> list6 = new List<int>();
				foreach (int item2 in list5)
				{
					list6.Add(item2);
				}
				list2.Add(list6);
			}
			List<card> list7 = new List<card>();
			foreach (card item3 in avatar.cardMag._cardlist)
			{
				list7.Add(item3);
			}
			List<card> list8 = new List<card>();
			foreach (card item4 in avatar.OtherAvatar.cardMag._cardlist)
			{
				list8.Add(item4);
			}
			int hp = avatar.HP;
			int hp2 = avatar.OtherAvatar.HP;
			if (this.CanUse(_attaker, _receiver, true, uuid) != SkillCanUseType.可以使用)
			{
				return;
			}
			this.useCrystal(_attaker);
			List<int> list9 = this.VirtualPutingSkill(_attaker, _receiver, type);
			avatar.spell.onBuffTickByType(8, list9);
			avatar.spell.onRemoveBuffByType(10, 1);
			avatar.UsedSkills = new List<int>(collection);
			avatar2.spell.AutoRemoveBuff();
			avatar.spell.AutoRemoveBuff();
			RoundManager.instance.VirtualSkillDamage = list9[0];
			avatar.bufflist = list;
			avatar.OtherAvatar.bufflist = list2;
			avatar.cardMag._cardlist = list7;
			avatar.OtherAvatar.cardMag._cardlist = list8;
			avatar.HP = hp;
			avatar.OtherAvatar.HP = hp2;
			avatar.BuffSeidFlag = dictionary;
			avatar.OtherAvatar.BuffSeidFlag = dictionary2;
			this.CurCD = 0f;
			RoundManager.instance.eventChengeCrystal();
		}

		// Token: 0x060051BB RID: 20923 RVA: 0x002230E0 File Offset: 0x002212E0
		public void Puting(Entity _attaker, Entity _receiver, int type = 0, string uuid = "")
		{
			Avatar attaker = (Avatar)_attaker;
			Avatar receiver = (Avatar)_receiver;
			if (this.CanUse(_attaker, _receiver, true, uuid) != SkillCanUseType.可以使用)
			{
				return;
			}
			Queue<UnityAction> queue = new Queue<UnityAction>();
			UnityAction item = delegate()
			{
				((GameObject)attaker.renderObj).transform.GetChild(0).GetComponent<Animator>().Play("Punch", -1, 0f);
				YSFuncList.Ints.Continue();
			};
			queue.Enqueue(item);
			RoundManager.EventFightTalk("UseSkill", new EventDelegate(delegate()
			{
				Flowchart fightTalk = RoundManager.instance.FightTalk;
				fightTalk.SetBooleanVariable("attaker", attaker.isPlayer());
				fightTalk.SetBooleanVariable("receiver", receiver.isPlayer());
				fightTalk.SetIntegerVariable("SkillID", (int)jsonData.instance.skillJsonData[this.skill_ID.ToString()]["Skill_ID"].n);
			}), null);
			Debug.Log(attaker.name + "施放了" + this.skill_Name);
			UnityAction item2 = delegate()
			{
				string demage = this.skill_Name.Replace("1", "").Replace("2", "").Replace("3", "").Replace("4", "").Replace("5", "");
				((GameObject)attaker.renderObj).GetComponentInChildren<AvatarShowSkill>().setText(demage, attaker);
				YSFuncList.Ints.Continue();
			};
			queue.Enqueue(item2);
			YSFuncList.Ints.AddFunc(queue);
			RoundManager.instance.NowUseLingQiType = UseLingQiType.释放技能消耗;
			this.useCrystal(_attaker);
			UIFightPanel.Inst.CacheLingQiController.DestoryAllLingQi();
			UIFightPanel.Inst.PlayerLingQiController.ResetPlayerLingQiCount();
			RoundManager.instance.chengeCrystal();
			RoundManager.instance.NowUseLingQiType = UseLingQiType.释放技能后消耗;
			List<int> list = this.PutingSkill(_attaker, _receiver, type);
			attaker.spell.onBuffTickByType(8, list);
			attaker.fightTemp.UseSkill(this.skill_ID);
			attaker.spell.onRemoveBuffByType(10, 1);
			receiver.spell.AutoRemoveBuff();
			attaker.spell.AutoRemoveBuff();
			RoundManager.instance.eventChengeCrystal();
			this.ShowScroll(_attaker, _receiver, list[0]);
			UIFightPanel.Inst.RefreshCD();
			RoundManager.instance.NowUseLingQiType = UseLingQiType.None;
		}

		// Token: 0x060051BC RID: 20924 RVA: 0x00223274 File Offset: 0x00221474
		public int realizeSeid(int seid, List<int> damage, Entity _attaker, Entity _receiver, int type)
		{
			Avatar avatar = (Avatar)_attaker;
			Avatar avatar2 = (Avatar)_receiver;
			string text = "realizeSeid" + seid;
			Skill.InitMethod(text);
			if (Skill.methodDict[text] != null)
			{
				Skill.methodDict[text].Invoke(this, new object[]
				{
					seid,
					damage,
					avatar,
					avatar2,
					type
				});
			}
			return damage[0];
		}

		// Token: 0x060051BD RID: 20925 RVA: 0x002232F8 File Offset: 0x002214F8
		public int realizeBuffEndSeid(int seid, List<int> damage, Entity _attaker, Entity _receiver, int type)
		{
			Avatar avatar = (Avatar)_attaker;
			Avatar avatar2 = (Avatar)_receiver;
			string text = "realizeBuffEndSeid" + seid;
			Skill.InitMethod(text);
			if (Skill.methodDict[text] != null)
			{
				Skill.methodDict[text].Invoke(this, new object[]
				{
					seid,
					damage,
					avatar,
					avatar2,
					type
				});
			}
			return damage[0];
		}

		// Token: 0x060051BE RID: 20926 RVA: 0x0022337C File Offset: 0x0022157C
		public int realizeFinalSeid(int seid, List<int> damage, Entity _attaker, Entity _receiver, int type)
		{
			Avatar avatar = (Avatar)_attaker;
			Avatar avatar2 = (Avatar)_receiver;
			string text = "realizeFinalSeid" + seid;
			Skill.InitMethod(text);
			if (Skill.methodDict[text] != null)
			{
				Skill.methodDict[text].Invoke(this, new object[]
				{
					seid,
					damage,
					avatar,
					avatar2,
					type
				});
			}
			return damage[0];
		}

		// Token: 0x060051BF RID: 20927 RVA: 0x00223400 File Offset: 0x00221600
		public bool CanRealizeSeid(Avatar attaker, Avatar receiver)
		{
			foreach (int num in _skillJsonData.DataDict[this.skill_ID].seid)
			{
				if (num <= 58)
				{
					if (num != 10)
					{
						switch (num)
						{
						case 52:
							if (!receiver.buffmag.HasBuff((int)this.getSeidJson(num)["value1"].n))
							{
								return false;
							}
							break;
						case 53:
							if (this.getCrystalNum(receiver) != 0)
							{
								return false;
							}
							break;
						case 54:
							if (this.getCrystalNum(attaker) < (int)this.getSeidJson(num)["value1"].n)
							{
								return false;
							}
							break;
						case 56:
						{
							JSONObject seidJson = this.getSeidJson(num);
							if (!attaker.buffmag.HasBuff(seidJson["value1"].I))
							{
								return false;
							}
							if (attaker.buffmag.getBuffByID(seidJson["value1"].I)[0][1] < seidJson["value2"].I)
							{
								return false;
							}
							break;
						}
						case 58:
						{
							if (attaker.UsedSkills.Count <= 0)
							{
								return false;
							}
							bool flag = true;
							int i = this.getSeidJson(num)["value1"].I;
							using (List<int>.Enumerator enumerator2 = _skillJsonData.DataDict[attaker.UsedSkills[attaker.UsedSkills.Count - 1]].AttackType.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									if (enumerator2.Current == i)
									{
										flag = false;
										break;
									}
								}
							}
							if (flag)
							{
								return false;
							}
							break;
						}
						}
					}
					else if (attaker.shengShi <= receiver.shengShi)
					{
						return false;
					}
				}
				else if (num != 78)
				{
					if (num == 160)
					{
						if (attaker.OtherAvatar.buffmag.HasBuff(this.getSeidJson(num)["value1"].I))
						{
							return false;
						}
					}
				}
				else if (attaker.crystal.getCardNum() != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060051C0 RID: 20928 RVA: 0x0003AC6A File Offset: 0x00038E6A
		public int getCrystalNum(Avatar attaker)
		{
			return attaker.crystal.getCardNum();
		}

		// Token: 0x060051C1 RID: 20929 RVA: 0x00223694 File Offset: 0x00221894
		public void setSeidSkillFlag(Avatar attaker, int seid, int num)
		{
			if (!attaker.SkillSeidFlag.ContainsKey(seid))
			{
				attaker.SkillSeidFlag.Add(seid, new Dictionary<int, int>());
				attaker.SkillSeidFlag[seid].Add(this.skill_ID, 0);
			}
			attaker.SkillSeidFlag[seid][this.skill_ID] = num;
		}

		// Token: 0x060051C2 RID: 20930 RVA: 0x002236F0 File Offset: 0x002218F0
		public int GetSeidSkillFlag(Avatar attaker, int seid)
		{
			if (!attaker.SkillSeidFlag.ContainsKey(seid))
			{
				attaker.SkillSeidFlag.Add(seid, new Dictionary<int, int>());
				attaker.SkillSeidFlag[seid].Add(this.skill_ID, 0);
			}
			return attaker.SkillSeidFlag[seid][this.skill_ID];
		}

		// Token: 0x060051C3 RID: 20931 RVA: 0x0022374C File Offset: 0x0022194C
		public void AddSeidSkillFlag(Avatar attaker, int seid, int Addnum)
		{
			if (!attaker.SkillSeidFlag.ContainsKey(seid))
			{
				attaker.SkillSeidFlag.Add(seid, new Dictionary<int, int>());
				attaker.SkillSeidFlag[seid].Add(this.skill_ID, 0);
			}
			Dictionary<int, int> dictionary = attaker.SkillSeidFlag[seid];
			int key = this.skill_ID;
			dictionary[key] += Addnum;
		}

		// Token: 0x060051C4 RID: 20932 RVA: 0x002237B4 File Offset: 0x002219B4
		public void reduceBuff(Avatar Targ, int X, int Y)
		{
			List<int> list = Targ.buffmag.getBuffByID(X)[0];
			list[1] = list[1] - Y;
		}

		// Token: 0x060051C5 RID: 20933 RVA: 0x002237E4 File Offset: 0x002219E4
		public int RemoveBuff(Avatar Targ, int BuffID)
		{
			List<List<int>> buffByID = Targ.buffmag.getBuffByID(BuffID);
			int num = 0;
			foreach (List<int> list in buffByID)
			{
				num += list[1];
				Targ.spell.removeBuff(list);
			}
			return num;
		}

		// Token: 0x060051C6 RID: 20934 RVA: 0x00223850 File Offset: 0x00221A50
		public void createSkill(Avatar attaker, int CreateSkillID)
		{
			Skill skill = new Skill(CreateSkillID, (int)attaker.level, 5);
			if (jsonData.instance.skillJsonData[string.Concat(skill.skill_ID)]["script"].str == "SkillAttack")
			{
				skill.PutingSkill(attaker, attaker.OtherAvatar, 0);
				return;
			}
			if (jsonData.instance.skillJsonData[string.Concat(skill.skill_ID)]["script"].str == "SkillSelf")
			{
				skill.PutingSkill(attaker, attaker, 0);
			}
		}

		// Token: 0x060051C7 RID: 20935 RVA: 0x002238FC File Offset: 0x00221AFC
		public JSONObject getSeidJson(int seid)
		{
			if (this.ItemAddSeid != null)
			{
				foreach (JSONObject jsonobject in this.ItemAddSeid.list)
				{
					if (jsonobject["id"].I == seid)
					{
						return jsonobject;
					}
				}
			}
			JSONObject result = null;
			if (seid < jsonData.instance.SkillSeidJsonData.Length)
			{
				JSONObject jsonobject2 = jsonData.instance.SkillSeidJsonData[seid];
				if (jsonobject2.HasField(this.skill_ID.ToString()))
				{
					return jsonobject2[this.skill_ID.ToString()];
				}
				Debug.LogError(string.Format("获取技能seid数据失败，技能id:{0}，seid:{1}，技能seid{2}表中不存在id为{3}的数据，请检查配表", new object[]
				{
					this.skill_ID,
					seid,
					seid,
					this.skill_ID
				}));
			}
			else
			{
				Debug.LogError(string.Format("获取技能seid数据失败，技能id:{0}，seid:{1}，seid超出了jsonData.instance.SkillSeidJsonData.Length，请检查配表", this.skill_ID, seid));
			}
			return result;
		}

		// Token: 0x060051C8 RID: 20936 RVA: 0x00223A20 File Offset: 0x00221C20
		public void realizeBuffEndSeid1(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int num = (int)((float)damage[0] * this.getSeidJson(seid)["value1"].n);
			attaker.recvDamage(attaker, attaker, this.skill_ID, -num, type);
		}

		// Token: 0x060051C9 RID: 20937 RVA: 0x00223A64 File Offset: 0x00221C64
		public void realizeSeid2(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int i = this.getSeidJson(seid)["value1"].I;
			RoundManager.instance.RandomDrawCard(attaker, i);
			RoundManager.instance.chengeCrystal();
		}

		// Token: 0x060051CA RID: 20938 RVA: 0x00223AA0 File Offset: 0x00221CA0
		public void realizeSeid3(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int num = 0;
			JSONObject seidJson = this.getSeidJson(seid);
			List<int> list = new List<int>();
			foreach (List<int> list2 in receiver.bufflist)
			{
				for (int i = 0; i < seidJson["value1"].list.Count; i++)
				{
					if (list2[2] == seidJson["value1"][i].I)
					{
						for (int j = 0; j < seidJson["value2"][i].I; j++)
						{
							list.Add(num);
						}
						break;
					}
				}
				num++;
			}
			foreach (int index in list)
			{
				receiver.spell.onBuffTick(index, new List<int>
				{
					0
				}, 0);
			}
		}

		// Token: 0x060051CB RID: 20939 RVA: 0x00223BD0 File Offset: 0x00221DD0
		public void realizeSeid4(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			for (int i = 0; i < seidJson["value1"].Count; i++)
			{
				int i2 = seidJson["value1"][i].I;
				int i3 = seidJson["value2"][i].I;
				if (i3 >= 100)
				{
					receiver.spell.addBuff(i2, i3);
				}
				else
				{
					for (int j = 0; j < i3; j++)
					{
						receiver.spell.addDBuff(i2);
					}
				}
			}
		}

		// Token: 0x060051CC RID: 20940 RVA: 0x00223C64 File Offset: 0x00221E64
		public void realizeSeid5(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if (!attaker.SkillSeidFlag.ContainsKey(seid))
			{
				attaker.SkillSeidFlag.Add(seid, new Dictionary<int, int>());
				attaker.SkillSeidFlag[seid].Add(0, 1);
			}
			attaker.SkillSeidFlag[seid][this.skill_ID] = 1;
			this.CurCD = 50000f;
		}

		// Token: 0x060051CD RID: 20941 RVA: 0x0003AC77 File Offset: 0x00038E77
		public void realizeSeid6(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			this.CurCD = 50000f;
		}

		// Token: 0x060051CE RID: 20942 RVA: 0x00223CC8 File Offset: 0x00221EC8
		public void realizeSeid7(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			for (int i = 0; i < seidJson["value1"].Count; i++)
			{
				int i2 = seidJson["value1"][i].I;
				for (int j = 0; j < seidJson["value2"][i].I; j++)
				{
					RoundManager.instance.DrawCard(attaker, i2);
				}
			}
		}

		// Token: 0x060051CF RID: 20943 RVA: 0x000042DD File Offset: 0x000024DD
		public void realizeSeid8(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
		}

		// Token: 0x060051D0 RID: 20944 RVA: 0x00223D3C File Offset: 0x00221F3C
		public void realizeSeid9(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int num = 0;
			int i = this.getSeidJson(seid)["value1"].I;
			foreach (List<int> list in receiver.bufflist)
			{
				if (list[2] == i)
				{
					for (int j = 0; j < list[1]; j++)
					{
						receiver.spell.onBuffTick(num, null, 0);
					}
					break;
				}
				num++;
			}
		}

		// Token: 0x060051D1 RID: 20945 RVA: 0x0003AC84 File Offset: 0x00038E84
		public void realizeSeid10(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if (attaker.shengShi <= attaker.OtherAvatar.shengShi)
			{
				damage[2] = 1;
			}
		}

		// Token: 0x060051D2 RID: 20946 RVA: 0x00223DD8 File Offset: 0x00221FD8
		public void realizeSeid11(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			int i = seidJson["value1"].I;
			int i2 = seidJson["value2"].I;
			int i3 = seidJson["value3"].I;
			for (int j = 0; j < i; j++)
			{
				if (this.LateDamages == null)
				{
					this.LateDamages = new List<LateDamage>();
				}
				this.LateDamages.Add(new LateDamage
				{
					SkillId = i2,
					Damage = i3
				});
			}
		}

		// Token: 0x060051D3 RID: 20947 RVA: 0x0003ACA1 File Offset: 0x00038EA1
		public void realizeSeid12(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			RoundManager.instance.removeCard(receiver, this.getSeidJson(seid)["value1"].I);
		}

		// Token: 0x060051D4 RID: 20948 RVA: 0x00223E5C File Offset: 0x0022205C
		public void realizeSeid13(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			int i = seidJson["value1"].I;
			if (!receiver.SkillSeidFlag.ContainsKey(seid))
			{
				receiver.SkillSeidFlag.Add(seid, new Dictionary<int, int>());
			}
			if (!receiver.SkillSeidFlag[seid].ContainsKey(i))
			{
				receiver.SkillSeidFlag[seid].Add(i, 0);
			}
			int i2 = seidJson["value2"].I;
			Dictionary<int, int> dictionary = receiver.SkillSeidFlag[seid];
			int key = i;
			dictionary[key] += i2;
		}

		// Token: 0x060051D5 RID: 20949 RVA: 0x000042DD File Offset: 0x000024DD
		public void realizeSeid14(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
		}

		// Token: 0x060051D6 RID: 20950 RVA: 0x00223EFC File Offset: 0x002220FC
		public void realizeSeid15(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			RoundManager.instance.removeCard(receiver, seidJson["value1"].I, seidJson["value2"].I);
		}

		// Token: 0x060051D7 RID: 20951 RVA: 0x00223F40 File Offset: 0x00222140
		public void realizeSeid16(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int listSum = RoundManager.instance.getListSum(receiver.crystal);
			int count = (int)(receiver.NowCard - (uint)listSum);
			RoundManager.instance.RandomDrawCard(attaker, count);
		}

		// Token: 0x060051D8 RID: 20952 RVA: 0x00223F78 File Offset: 0x00222178
		public void realizeSeid17(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			int i = seidJson["value1"].I;
			int i2 = seidJson["value2"].I;
			for (int j = 0; j < i; j++)
			{
				card card = RoundManager.instance.drawCard(attaker);
				if (i2 == card.cardType)
				{
					RoundManager.instance.DrawCard(attaker, card.cardType);
				}
			}
		}

		// Token: 0x060051D9 RID: 20953 RVA: 0x00223FE0 File Offset: 0x002221E0
		public void realizeSeid18(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int num = 0;
			foreach (int num2 in attaker.crystal.ToListInt32())
			{
				for (int i = 0; i < num2; i++)
				{
					RoundManager.instance.DrawCard(attaker, num);
				}
				num++;
			}
		}

		// Token: 0x060051DA RID: 20954 RVA: 0x00224050 File Offset: 0x00222250
		public void realizeSeid19(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int listSum = RoundManager.instance.getListSum(receiver.crystal);
			RoundManager.instance.removeCard(receiver, listSum);
		}

		// Token: 0x060051DB RID: 20955 RVA: 0x0022407C File Offset: 0x0022227C
		public void realizeSeid20(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int listSum = RoundManager.instance.getListSum(receiver.crystal);
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			RoundManager.instance.removeCard(receiver, listSum);
			stopwatch.Stop();
			Debug.Log(string.Format("移除耗时{0}ms", stopwatch.ElapsedMilliseconds));
			stopwatch.Reset();
			stopwatch.Start();
			RoundManager.instance.RandomDrawCard(attaker, listSum);
			stopwatch.Stop();
			Debug.Log(string.Format("添加耗时{0}ms", stopwatch.ElapsedMilliseconds));
		}

		// Token: 0x060051DC RID: 20956 RVA: 0x0022410C File Offset: 0x0022230C
		public void realizeSeid21(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int i = this.getSeidJson(seid)["value1"].I;
			RoundManager.instance.removeCard(receiver, receiver.crystal[i], i);
		}

		// Token: 0x060051DD RID: 20957 RVA: 0x000042DD File Offset: 0x000024DD
		public void realizeSeid22(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
		}

		// Token: 0x060051DE RID: 20958 RVA: 0x0022414C File Offset: 0x0022234C
		public void realizeSeid23(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			damage[0] = damage[0] + (int)(this.getSeidJson(seid)["value1"].n * (float)attaker.useSkillNum);
		}

		// Token: 0x060051DF RID: 20959 RVA: 0x0022418C File Offset: 0x0022238C
		public void realizeSeid24(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if (!attaker.SkillSeidFlag.ContainsKey(seid))
			{
				attaker.SkillSeidFlag.Add(seid, new Dictionary<int, int>());
				attaker.SkillSeidFlag[seid].Add(0, 1);
			}
			attaker.SkillSeidFlag[seid][0] = 1;
		}

		// Token: 0x060051E0 RID: 20960 RVA: 0x0003ACC5 File Offset: 0x00038EC5
		public void realizeSeid25(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			attaker.spell.addDBuff(5, attaker.HP_Max - attaker.HP);
		}

		// Token: 0x060051E1 RID: 20961 RVA: 0x002241E0 File Offset: 0x002223E0
		public void realizeSeid26(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			damage[0] = receiver.HP / 2;
			int i = this.getSeidJson(seid)["value1"].I;
			if (damage[0] > i)
			{
				damage[0] = i;
			}
		}

		// Token: 0x060051E2 RID: 20962 RVA: 0x00224228 File Offset: 0x00222428
		public void realizeSeid27(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int listSum = RoundManager.instance.getListSum(attaker.crystal);
			damage[0] = damage[0] + listSum * this.getSeidJson(seid)["value1"].I;
		}

		// Token: 0x060051E3 RID: 20963 RVA: 0x00224270 File Offset: 0x00222470
		public void realizeFinalSeid28(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int num = receiver.HP - damage[0] - receiver.HP_Max;
			if (num > 0)
			{
				receiver.spell.addDBuff(5, num);
			}
		}

		// Token: 0x060051E4 RID: 20964 RVA: 0x002242A8 File Offset: 0x002224A8
		public void realizeSeid29(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int i = this.getSeidJson(seid)["value1"].I;
			if (this.weaponuuid != null && this.weaponuuid != "")
			{
				if (RoundManager.instance.WeaponSkillList.ContainsKey(this.weaponuuid))
				{
					RoundManager.instance.WeaponSkillList[this.weaponuuid] = i;
				}
				else
				{
					RoundManager.instance.WeaponSkillList.Add(this.weaponuuid, i);
				}
				this.CurCD = 50000f;
				return;
			}
			if (!attaker.SkillSeidFlag.ContainsKey(seid))
			{
				attaker.SkillSeidFlag.Add(seid, new Dictionary<int, int>());
				attaker.SkillSeidFlag[seid].Add(0, 1);
			}
			attaker.SkillSeidFlag[seid][this.skill_ID] = i;
			this.CurCD = 50000f;
		}

		// Token: 0x060051E5 RID: 20965 RVA: 0x0022438C File Offset: 0x0022258C
		public void realizeSeid30(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			int i = seidJson["value1"].I;
			int i2 = seidJson["value2"].I;
			int[] randomLingQiTypes = RoundManager.instance.GetRandomLingQiTypes(attaker, i);
			for (int j = 0; j < randomLingQiTypes.Length; j++)
			{
				if (randomLingQiTypes[j] > 0)
				{
					RoundManager.instance.DrawCardCreatSpritAndAddCrystal(attaker, j, randomLingQiTypes[j]);
				}
			}
			for (int k = 0; k < randomLingQiTypes.Length; k++)
			{
				if (randomLingQiTypes[k] > 0 && k != i2)
				{
					RoundManager.instance.RoundTimeAutoRemoveCard(attaker, k, randomLingQiTypes[k]);
				}
			}
		}

		// Token: 0x060051E6 RID: 20966 RVA: 0x00224420 File Offset: 0x00222620
		public void realizeSeid31(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			for (int i = 0; i < seidJson["value1"].Count; i++)
			{
				int i2 = seidJson["value1"][i].I;
				int i3 = seidJson["value2"][i].I;
				if (this.SkillID == 221)
				{
					for (int j = 0; j < i2; j++)
					{
						attaker.spell.addDBuff(i3);
					}
				}
				else
				{
					attaker.spell.addBuff(i3, i2);
				}
			}
		}

		// Token: 0x060051E7 RID: 20967 RVA: 0x002244B8 File Offset: 0x002226B8
		public void realizeSeid32(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			List<List<int>> buffByID = receiver.buffmag.getBuffByID(seidJson["value2"].I);
			if (buffByID.Count > 0)
			{
				int i = seidJson["value1"].I;
				if (buffByID[0][1] >= i)
				{
					List<int> list = buffByID[0];
					list[1] = list[1] - i;
					return;
				}
				buffByID[0][1] = 0;
			}
		}

		// Token: 0x060051E8 RID: 20968 RVA: 0x0022453C File Offset: 0x0022273C
		public void realizeSeid33(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			List<List<int>> buffByID = attaker.buffmag.getBuffByID(seidJson["value2"].I);
			if (buffByID.Count > 0)
			{
				int i = seidJson["value1"].I;
				if (buffByID[0][1] >= i)
				{
					List<int> list = buffByID[0];
					list[1] = list[1] - i;
					return;
				}
				buffByID[0][1] = 0;
			}
		}

		// Token: 0x060051E9 RID: 20969 RVA: 0x0003ACE0 File Offset: 0x00038EE0
		public void realizeBuffEndSeid34(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			damage[0] = damage[0] * this.getSeidJson(seid)["value1"].I;
		}

		// Token: 0x060051EA RID: 20970 RVA: 0x000042DD File Offset: 0x000024DD
		public void realizeSeidFinal34(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
		}

		// Token: 0x060051EB RID: 20971 RVA: 0x002245C0 File Offset: 0x002227C0
		public void realizeSeid35(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			damage[0] = damage[0] + attaker.NowRoundUsedCard.Count * this.getSeidJson(seid)["value1"].I;
		}

		// Token: 0x060051EC RID: 20972 RVA: 0x00224600 File Offset: 0x00222800
		public void realizeSeid36(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			int value1 = seidJson["value1"].I;
			int j = seidJson["value2"].I;
			int count = attaker.NowRoundUsedCard.FindAll((int i) => i == value1).Count;
			damage[0] = damage[0] + count * j;
		}

		// Token: 0x060051ED RID: 20973 RVA: 0x00224678 File Offset: 0x00222878
		public void realizeSeid37(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int cardNum = attaker.crystal.getCardNum();
			attaker.recvDamage(attaker, attaker, this.skill_ID, -cardNum * this.getSeidJson(seid)["value1"].I, type);
		}

		// Token: 0x060051EE RID: 20974 RVA: 0x002246BC File Offset: 0x002228BC
		public void realizeSeid38(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			int num = attaker.crystal[seidJson["value1"].I];
			damage[0] = damage[0] + num * seidJson["value2"].I;
		}

		// Token: 0x060051EF RID: 20975 RVA: 0x00224710 File Offset: 0x00222910
		public void realizeSeid39(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int listSum = RoundManager.instance.getListSum(attaker.crystal);
			RoundManager.instance.removeCard(attaker, listSum);
			damage[0] = damage[0] + listSum * this.getSeidJson(seid)["value1"].I;
		}

		// Token: 0x060051F0 RID: 20976 RVA: 0x00224764 File Offset: 0x00222964
		public void realizeSeid40(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			int i = seidJson["value1"].I;
			int i2 = seidJson["value2"].I;
			int num = attaker.crystal[i];
			RoundManager.instance.removeCard(attaker, num, i);
			damage[0] = damage[0] + num * i2;
		}

		// Token: 0x060051F1 RID: 20977 RVA: 0x002247C8 File Offset: 0x002229C8
		public void realizeSeid41(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if (!attaker.SkillSeidFlag.ContainsKey(seid))
			{
				attaker.SkillSeidFlag.Add(seid, new Dictionary<int, int>());
				attaker.SkillSeidFlag[seid].Add(0, 1);
			}
			if (!attaker.SkillSeidFlag[seid].ContainsKey(this.skill_ID))
			{
				attaker.SkillSeidFlag[seid][this.skill_ID] = 0;
			}
			Dictionary<int, int> dictionary = attaker.SkillSeidFlag[seid];
			int key = this.skill_ID;
			dictionary[key]++;
			int num = damage[0];
			JSONObject seidJson = this.getSeidJson(seid);
			damage[0] = damage[0] + attaker.SkillSeidFlag[seid][this.skill_ID] / seidJson["value1"].I * seidJson["value2"].I;
			if (num > 0 && damage[0] < 0)
			{
				damage[0] = 0;
			}
			if (num < 0 && damage[0] > 0)
			{
				damage[0] = 0;
			}
		}

		// Token: 0x060051F2 RID: 20978 RVA: 0x002248E0 File Offset: 0x00222AE0
		public void realizeSeid42(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			int i = seidJson["value1"].I;
			int i2 = seidJson["value2"].I;
			if (receiver.buffmag.HasBuff(i))
			{
				List<List<int>> buffByID = receiver.buffmag.getBuffByID(i);
				damage[0] = damage[0] + buffByID[0][1] * i2;
			}
		}

		// Token: 0x060051F3 RID: 20979 RVA: 0x00224954 File Offset: 0x00222B54
		public void realizeSeid43(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			for (int i = 0; i < seidJson["value1"].list.Count; i++)
			{
				int i2 = seidJson["value1"][i].I;
				if (attaker.buffmag.HasBuff(i2))
				{
					List<List<int>> buffByID = attaker.buffmag.getBuffByID(i2);
					damage[0] = damage[0] + buffByID[0][1] * seidJson["value2"][i].I;
				}
			}
		}

		// Token: 0x060051F4 RID: 20980 RVA: 0x002249F8 File Offset: 0x00222BF8
		public void realizeSeid44(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			int i = seidJson["value1"].I;
			int i2 = seidJson["value2"].I;
			if (attaker.buffmag.HasBuff(i))
			{
				int num = attaker.buffmag.getBuffByID(i)[0][1] * i2;
				attaker.recvDamage(attaker, attaker, this.skill_ID, -num, type);
			}
		}

		// Token: 0x060051F5 RID: 20981 RVA: 0x00224A6C File Offset: 0x00222C6C
		public void realizeSeid45(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			this.reduceBuff(attaker, seidJson["value1"].I, seidJson["value2"].I);
			damage[0] = damage[0] + seidJson["value3"].I;
		}

		// Token: 0x060051F6 RID: 20982 RVA: 0x00224ACC File Offset: 0x00222CCC
		public void realizeSeid46(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			this.reduceBuff(attaker, seidJson["value1"].I, seidJson["value2"].I);
			attaker.recvDamage(attaker, attaker, this.skill_ID, -seidJson["value3"].I, type);
		}

		// Token: 0x060051F7 RID: 20983 RVA: 0x00224B2C File Offset: 0x00222D2C
		public void realizeSeid47(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			this.reduceBuff(attaker, seidJson["value1"].I, seidJson["value2"].I);
			this.createSkill(attaker, Tools.instance.getSkillKeyByID(seidJson["value3"].I, attaker));
		}

		// Token: 0x060051F8 RID: 20984 RVA: 0x00224B8C File Offset: 0x00222D8C
		public void realizeSeid48(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			int i = seidJson["value1"].I;
			if (attaker.buffmag.HasBuff(i))
			{
				int num = this.RemoveBuff(attaker, i);
				attaker.recvDamage(attaker, receiver, 10001 + seidJson["value3"].I, num * seidJson["value2"].I, type);
			}
		}

		// Token: 0x060051F9 RID: 20985 RVA: 0x00224C00 File Offset: 0x00222E00
		public void realizeSeid49(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int num = 0;
			JSONObject seidJson = this.getSeidJson(seid);
			JSONObject jsonobject = seidJson["value1"];
			foreach (JSONObject jsonobject2 in jsonobject.list)
			{
				if (attaker.buffmag.HasBuff(jsonobject[num].I))
				{
					int num2 = this.RemoveBuff(attaker, jsonobject[num].I);
					damage[0] = damage[0] + num2 * seidJson["value2"][num].I;
				}
				num++;
			}
		}

		// Token: 0x060051FA RID: 20986 RVA: 0x00224CC4 File Offset: 0x00222EC4
		public void realizeSeid50(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			int i = seidJson["value1"].I;
			if (attaker.buffmag.HasBuff(i))
			{
				int num = this.RemoveBuff(attaker, i);
				attaker.recvDamage(attaker, attaker, this.skill_ID, -num * seidJson["value2"].I, type);
			}
		}

		// Token: 0x060051FB RID: 20987 RVA: 0x00224D28 File Offset: 0x00222F28
		public void realizeSeid51(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int num = 0;
			JSONObject seidJson = this.getSeidJson(seid);
			JSONObject jsonobject = seidJson["value1"];
			foreach (JSONObject jsonobject2 in jsonobject.list)
			{
				if (attaker.buffmag.HasBuff(jsonobject[num].I))
				{
					int num2 = this.RemoveBuff(attaker, jsonobject[num].I);
					int i = seidJson["value2"][num].I;
					int i2 = seidJson["value3"][num].I;
					receiver.spell.addBuff(i, i2 * num2);
				}
				num++;
			}
		}

		// Token: 0x060051FC RID: 20988 RVA: 0x0003AD07 File Offset: 0x00038F07
		public void realizeSeid52(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if (!receiver.buffmag.HasBuff(this.getSeidJson(seid)["value1"].I))
			{
				damage[2] = 1;
			}
		}

		// Token: 0x060051FD RID: 20989 RVA: 0x0003AD35 File Offset: 0x00038F35
		public void realizeSeid53(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if (this.getCrystalNum(receiver) != 0)
			{
				damage[2] = 1;
			}
		}

		// Token: 0x060051FE RID: 20990 RVA: 0x00224E04 File Offset: 0x00223004
		public void realizeSeid54(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int crystalNum = this.getCrystalNum(attaker);
			int i = this.getSeidJson(seid)["value1"].I;
			if (crystalNum < i)
			{
				damage[2] = 1;
			}
		}

		// Token: 0x060051FF RID: 20991 RVA: 0x0003AD49 File Offset: 0x00038F49
		public void realizeSeid55(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if (RoundManager.instance.drawCard(attaker).cardType != this.getSeidJson(seid)["value1"].I)
			{
				damage[2] = 1;
			}
		}

		// Token: 0x06005200 RID: 20992 RVA: 0x00224E3C File Offset: 0x0022303C
		public void realizeSeid56(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			int i = seidJson["value1"].I;
			if (attaker.buffmag.HasBuff(i))
			{
				if (attaker.buffmag.getBuffByID(i)[0][1] < seidJson["value2"].I)
				{
					damage[2] = 1;
					return;
				}
			}
			else
			{
				damage[2] = 1;
			}
		}

		// Token: 0x06005201 RID: 20993 RVA: 0x00224EAC File Offset: 0x002230AC
		public void realizeSeid57(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			for (int i = 0; i < seidJson["value1"].Count; i++)
			{
				this.reduceBuff(attaker, seidJson["value1"][i].I, seidJson["value2"][i].I);
			}
		}

		// Token: 0x06005202 RID: 20994 RVA: 0x00224F10 File Offset: 0x00223110
		public void realizeSeid58(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if (attaker.UsedSkills.Count > 0)
			{
				bool flag = true;
				List<int> attackType = _skillJsonData.DataDict[attaker.UsedSkills[attaker.UsedSkills.Count - 1]].AttackType;
				int i = this.getSeidJson(seid)["value1"].I;
				using (List<int>.Enumerator enumerator = attackType.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current == i)
						{
							flag = false;
							break;
						}
					}
				}
				if (flag)
				{
					damage[2] = 1;
					return;
				}
			}
			else
			{
				damage[2] = 1;
			}
		}

		// Token: 0x06005203 RID: 20995 RVA: 0x0003AD7B File Offset: 0x00038F7B
		public void realizeSeid59(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			attaker.recvDamage(attaker, attaker, this.skill_ID, -attaker.HP_Max * (int)this.getSeidJson(seid)["value1"].n / 100, type);
		}

		// Token: 0x06005204 RID: 20996 RVA: 0x0003ADB0 File Offset: 0x00038FB0
		public void realizeSeid60(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			attaker.recvDamage(attaker, attaker, this.skill_ID, -(attaker.HP_Max - attaker.HP) * (int)this.getSeidJson(seid)["value1"].n / 100, type);
		}

		// Token: 0x06005205 RID: 20997 RVA: 0x00224FC0 File Offset: 0x002231C0
		public void realizeSeid61(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			damage[0] = damage[0] + attaker.HP_Max * (int)this.getSeidJson(seid)["value1"].n / 100;
		}

		// Token: 0x06005206 RID: 20998 RVA: 0x00225000 File Offset: 0x00223200
		public void realizeSeid62(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int i = this.getSeidJson(seid)["value1"].I;
			List<int> list = new List<int>();
			foreach (List<int> list2 in attaker.bufflist)
			{
				int num = list2[2];
				if (_BuffJsonData.DataDict[num].bufftype == i)
				{
					list.Add(num);
				}
			}
			foreach (int id in list)
			{
				receiver.spell.onBuffTickById(id, damage, 0);
			}
		}

		// Token: 0x06005207 RID: 20999 RVA: 0x002250D4 File Offset: 0x002232D4
		public void realizeSeid63(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			int i = seidJson["value1"].I;
			int i2 = seidJson["value2"].I;
			foreach (List<int> list in attaker.bufflist)
			{
				int key = list[2];
				if (_BuffJsonData.DataDict[key].bufftype == i)
				{
					damage[0] = damage[0] + i2;
				}
			}
		}

		// Token: 0x06005208 RID: 21000 RVA: 0x0003ADEC File Offset: 0x00038FEC
		public void realizeSeid64(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			attaker.recvDamage(attaker, receiver, this.skill_ID, receiver.HP_Max * (int)this.getSeidJson(seid)["value1"].n / 100, type);
		}

		// Token: 0x06005209 RID: 21001 RVA: 0x000042DD File Offset: 0x000024DD
		public void realizeSeid65(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
		}

		// Token: 0x0600520A RID: 21002 RVA: 0x00225178 File Offset: 0x00223378
		public void realizeSeid66(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			damage[0] = damage[0] + (int)this.getSeidJson(seid)["value1"].n;
		}

		// Token: 0x0600520B RID: 21003 RVA: 0x002251B0 File Offset: 0x002233B0
		public void realizeSeid67(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			int i = seidJson["value1"].I;
			int i2 = seidJson["value2"].I;
			int i3 = seidJson["value3"].I;
			int i4 = seidJson["value4"].I;
			List<List<int>> buffByID = attaker.buffmag.getBuffByID(i);
			if (buffByID.Count > 0 && buffByID[0][1] >= i2)
			{
				int num = buffByID[0][1] / i2;
				for (int j = 0; j < num; j++)
				{
					receiver.spell.addDBuff(i3, i4);
				}
			}
		}

		// Token: 0x0600520C RID: 21004 RVA: 0x000042DD File Offset: 0x000024DD
		public void realizeSeid68(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
		}

		// Token: 0x0600520D RID: 21005 RVA: 0x000042DD File Offset: 0x000024DD
		public void realizeSeid69(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
		}

		// Token: 0x0600520E RID: 21006 RVA: 0x0003AE22 File Offset: 0x00039022
		public void realizeSeid70(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			RoundManager.instance.startRound(attaker);
		}

		// Token: 0x0600520F RID: 21007 RVA: 0x00225264 File Offset: 0x00223464
		public void realizeSeid71(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			attaker.setHP(attaker.HP - seidJson["value1"].I);
			attaker.spell.addDBuff(seidJson["value2"].I, seidJson["value3"].I);
		}

		// Token: 0x06005210 RID: 21008 RVA: 0x002252C4 File Offset: 0x002234C4
		public void realizeSeid72(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int listSum = RoundManager.instance.getListSum(receiver.crystal);
			RoundManager.instance.removeCard(receiver, listSum);
			RoundManager.instance.RandomDrawCard(attaker, listSum);
		}

		// Token: 0x06005211 RID: 21009 RVA: 0x002252FC File Offset: 0x002234FC
		public void realizeSeid73(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			damage[0] = damage[0] + attaker.fightTemp.lastRoundDamage[1] / 2;
		}

		// Token: 0x06005212 RID: 21010 RVA: 0x0022532C File Offset: 0x0022352C
		public void realizeSeid74(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			foreach (List<int> buffid in attaker.buffmag.getAllBuffByType(this.getSeidJson(seid)["value1"].I))
			{
				attaker.spell.removeBuff(buffid);
			}
		}

		// Token: 0x06005213 RID: 21011 RVA: 0x002253A0 File Offset: 0x002235A0
		public void realizeSeid75(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			List<List<int>> buffByID = receiver.buffmag.getBuffByID(seidJson["value1"].I);
			if (buffByID.Count == 0)
			{
				return;
			}
			int i = seidJson["value2"].I;
			int num;
			if (buffByID[0][1] >= i)
			{
				num = i;
			}
			else
			{
				num = buffByID[0][1];
			}
			int num2 = 0;
			using (List<List<int>>.Enumerator enumerator = receiver.bufflist.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == buffByID[0])
					{
						for (int j = 0; j < num; j++)
						{
							receiver.spell.onBuffTick(num2, new List<int>(), 0);
							RoundManager.instance.DrawCard(attaker);
						}
						break;
					}
					num2++;
				}
			}
		}

		// Token: 0x06005214 RID: 21012 RVA: 0x0003AE2F File Offset: 0x0003902F
		public void realizeSeid77(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if ((int)((float)attaker.HP / (float)attaker.HP_Max * 100f) > this.getSeidJson(seid)["value1"].I)
			{
				damage[2] = 1;
			}
		}

		// Token: 0x06005215 RID: 21013 RVA: 0x0003AE67 File Offset: 0x00039067
		public void realizeSeid78(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if (attaker.crystal.getCardNum() != 0)
			{
				damage[2] = 1;
			}
		}

		// Token: 0x06005216 RID: 21014 RVA: 0x00225494 File Offset: 0x00223694
		public void realizeSeid79(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			JSONObject jsonobject = seidJson["value1"];
			JSONObject jsonobject2 = seidJson["value2"];
			for (int i = 0; i < jsonobject.Count; i++)
			{
				receiver.spell.addBuff(jsonobject[i].I, jsonobject2[i].I);
			}
		}

		// Token: 0x06005217 RID: 21015 RVA: 0x002254F8 File Offset: 0x002236F8
		public void realizeSeid80(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			JSONObject jsonobject = seidJson["value1"];
			JSONObject jsonobject2 = seidJson["value2"];
			for (int i = 0; i < jsonobject2.Count; i++)
			{
				attaker.spell.addBuff(jsonobject[i].I, jsonobject2[i].I);
			}
		}

		// Token: 0x06005218 RID: 21016 RVA: 0x00225558 File Offset: 0x00223758
		public void realizeSeid81(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			attaker.cardMag.getCardNum();
			int i = this.getSeidJson(seid)["value1"].I;
			RoundManager.instance.removeCard(attaker, i);
		}

		// Token: 0x06005219 RID: 21017 RVA: 0x00225594 File Offset: 0x00223794
		public void realizeSeid82(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			List<List<int>> buffByID = receiver.buffmag.getBuffByID(seidJson["value1"].I);
			if (buffByID.Count > 0)
			{
				buffByID[0][1] = seidJson["value2"].I;
			}
		}

		// Token: 0x0600521A RID: 21018 RVA: 0x002255EC File Offset: 0x002237EC
		public void realizeSeid83(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int num = 0;
			JSONObject seidJson = this.getSeidJson(seid);
			JSONObject jsonobject = seidJson["value1"];
			JSONObject jsonobject2 = seidJson["value2"];
			foreach (JSONObject jsonobject3 in jsonobject.list)
			{
				int i = jsonobject[num].I;
				int i2 = jsonobject2[num].I;
				List<List<int>> buffByID = attaker.buffmag.getBuffByID(i);
				if (buffByID.Count > 0)
				{
					buffByID[0][1] = i2;
				}
				else
				{
					attaker.spell.addDBuff(i, i2);
				}
				num++;
			}
		}

		// Token: 0x0600521B RID: 21019 RVA: 0x002256B4 File Offset: 0x002238B4
		public void realizeSeid84(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			int num = (attaker.shengShi > attaker.OtherAvatar.shengShi) ? (attaker.shengShi - attaker.OtherAvatar.shengShi) : 0;
			damage[0] = damage[0] + (int)((float)num / seidJson["value1"].n) * seidJson["value2"].I;
		}

		// Token: 0x0600521C RID: 21020 RVA: 0x00225728 File Offset: 0x00223928
		public void realizeSeid85(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int dunSu = attaker.dunSu;
			JSONObject seidJson = this.getSeidJson(seid);
			damage[0] = damage[0] + (int)((float)dunSu / seidJson["value1"].n) * seidJson["value2"].I;
		}

		// Token: 0x0600521D RID: 21021 RVA: 0x0003AE7E File Offset: 0x0003907E
		public void realizeSeid86(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			attaker.recvDamage(attaker, receiver, this.skill_ID, receiver.HP * (int)this.getSeidJson(seid)["value1"].n / 100, type);
		}

		// Token: 0x0600521E RID: 21022 RVA: 0x0022577C File Offset: 0x0022397C
		public void realizeSeid87(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int i = this.getSeidJson(seid)["value1"].I;
			int i2 = this.getSeidJson(seid)["value2"].I;
			for (int j = 0; j < i; j++)
			{
				card randomCard = attaker.OtherAvatar.cardMag.getRandomCard();
				if (randomCard != null)
				{
					int cardType = randomCard.cardType;
					card card = attaker.OtherAvatar.cardMag.ChengCard(randomCard.cardType, i2);
					if (attaker.OtherAvatar.isPlayer() && card != null)
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

		// Token: 0x0600521F RID: 21023 RVA: 0x0022587C File Offset: 0x00223A7C
		public void realizeSeid88(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			int i = seidJson["value1"].I;
			int i2 = seidJson["value2"].I;
			for (int j = 0; j < i; j++)
			{
				RoundManager.instance.DrawCard(receiver, i2);
			}
		}

		// Token: 0x06005220 RID: 21024 RVA: 0x0003AEB4 File Offset: 0x000390B4
		public void realizeSeid89(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if (receiver.cardMag[(int)this.getSeidJson(seid)["value1"].n] <= 0)
			{
				damage[2] = 1;
			}
		}

		// Token: 0x06005221 RID: 21025 RVA: 0x002258CC File Offset: 0x00223ACC
		public void realizeSeid90(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			int num = attaker.OtherAvatar.crystal[seidJson["value1"].I];
			damage[0] = damage[0] + num * seidJson["value2"].I;
		}

		// Token: 0x06005222 RID: 21026 RVA: 0x00225928 File Offset: 0x00223B28
		public void realizeSeid91(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			int value1 = seidJson["value1"].I;
			int count = attaker.UsedSkills.FindAll((int id) => _skillJsonData.DataDict[id].Skill_ID == value1).Count;
			damage[0] = damage[0] + count * seidJson["value2"].I;
		}

		// Token: 0x06005223 RID: 21027 RVA: 0x0022599C File Offset: 0x00223B9C
		public void realizeSeid92(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			RoundManager.instance.removeCard(attaker, seidJson["value1"].I, seidJson["value2"].I);
		}

		// Token: 0x06005224 RID: 21028 RVA: 0x002259DC File Offset: 0x00223BDC
		public void realizeSeid94(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			List<int> list = receiver.spell.addDBuff(seidJson["value1"].I);
			if (list != null)
			{
				list[1] = seidJson["value2"].I;
			}
		}

		// Token: 0x06005225 RID: 21029 RVA: 0x0003AEE4 File Offset: 0x000390E4
		public void realizeSeid95(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if (!receiver.buffmag.HasXTypeBuff(this.getSeidJson(seid)["value1"].I))
			{
				damage[2] = 1;
			}
		}

		// Token: 0x06005226 RID: 21030 RVA: 0x00225A28 File Offset: 0x00223C28
		public void realizeSeid96(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int i = this.getSeidJson(seid)["value1"].I;
			foreach (List<int> list in receiver.bufflist)
			{
				if (i == _BuffJsonData.DataDict[list[2]].bufftype)
				{
					List<int> list2 = list;
					list2[1] = list2[1] - 1;
				}
			}
		}

		// Token: 0x06005227 RID: 21031 RVA: 0x00225AB8 File Offset: 0x00223CB8
		public void realizeSeid100(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			int buffRoundByID = receiver.buffmag.GetBuffRoundByID(seidJson["value1"].I);
			string str = seidJson["panduan"].Str;
			if (!this.PanDuan(str, buffRoundByID, seidJson["value2"].I))
			{
				damage[2] = 1;
			}
		}

		// Token: 0x06005228 RID: 21032 RVA: 0x0003AF12 File Offset: 0x00039112
		public void realizeSeid101(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			attaker.setHP((int)this.getSeidJson(seid)["value1"].n);
		}

		// Token: 0x06005229 RID: 21033 RVA: 0x00225B20 File Offset: 0x00223D20
		public void realizeSeid102(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int num = 0;
			JSONObject seidJson = this.getSeidJson(seid);
			JSONObject jsonobject = seidJson["value1"];
			JSONObject jsonobject2 = seidJson["value2"];
			foreach (List<int> list in attaker.bufflist)
			{
				for (int i = 0; i < jsonobject.list.Count; i++)
				{
					if (list[2] == jsonobject[i].I)
					{
						for (int j = 0; j < jsonobject2[i].I; j++)
						{
							attaker.spell.onBuffTick(num, new List<int>
							{
								0
							}, 0);
						}
						break;
					}
				}
				num++;
			}
		}

		// Token: 0x0600522A RID: 21034 RVA: 0x00225BFC File Offset: 0x00223DFC
		public void realizeFinalSeid103(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			this.AddSeidSkillFlag(attaker, seid, damage[0]);
			int num = attaker.SkillSeidFlag[seid][this.skill_ID];
			JSONObject seidJson = this.getSeidJson(seid);
			int i = seidJson["value1"].I;
			int i2 = seidJson["value2"].I;
			int i3 = seidJson["value3"].I;
			if (num >= i)
			{
				int num2 = num / i;
				this.setSeidSkillFlag(attaker, seid, num % i);
				for (int j = 0; j < num2; j++)
				{
					receiver.spell.addDBuff(i2, i3);
				}
			}
		}

		// Token: 0x0600522B RID: 21035 RVA: 0x0003AF31 File Offset: 0x00039131
		public void realizeFinalSeid104(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			Tools.instance.ToolsStartCoroutine(this.ISeid104(seid));
		}

		// Token: 0x0600522C RID: 21036 RVA: 0x0003AF44 File Offset: 0x00039144
		public IEnumerator ISeid104(int seid)
		{
			yield return new WaitForSeconds(0.01f);
			this.skillInit(this.getSeidJson(seid)["value1"].I, 0, 10);
			yield break;
		}

		// Token: 0x0600522D RID: 21037 RVA: 0x00225C9C File Offset: 0x00223E9C
		public void realizeSeid105(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int skillID = Tools.instance.getSkillKeyByID(this.getSeidJson(seid)["value1"].I, attaker);
			Skill skill = new Skill(skillID, 0, 10);
			List<int> _damage = new List<int>();
			Tools.AddQueue(delegate
			{
				RoundManager.instance.NowUseLingQiType = UseLingQiType.释放技能后消耗;
				string script = _skillJsonData.DataDict[skillID].script;
				if (script == "SkillAttack")
				{
					_damage = skill.PutingSkill(attaker, attaker.OtherAvatar, 0);
				}
				else if (script == "SkillSelf")
				{
					_damage = skill.PutingSkill(attaker, attaker, 0);
				}
				attaker.spell.onBuffTickByType(8, _damage);
				RoundManager.instance.NowUseLingQiType = UseLingQiType.None;
				YSFuncList.Ints.Continue();
			});
		}

		// Token: 0x0600522E RID: 21038 RVA: 0x00225D14 File Offset: 0x00223F14
		public void realizeSeid107(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			Avatar targetAvatar = this.getTargetAvatar(seid, attaker);
			int buffSum = targetAvatar.buffmag.GetBuffSum(this.getSeidJson(seid)["value1"].I);
			if (buffSum > 0)
			{
				targetAvatar.recvDamage(attaker, attaker, this.skill_ID, -buffSum, type);
			}
		}

		// Token: 0x0600522F RID: 21039 RVA: 0x00225D64 File Offset: 0x00223F64
		public void realizeSeid108(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			int num = attaker.crystal[seidJson["value1"].I];
			attaker.spell.addBuff(seidJson["value2"].I, seidJson["value3"].I * num);
		}

		// Token: 0x06005230 RID: 21040 RVA: 0x00225DC4 File Offset: 0x00223FC4
		public void realizeSeid110(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int i = this.getSeidJson(seid)["value1"].I;
			int num = attaker.HP * i / 100;
			attaker.setHP(attaker.HP - num);
			damage[0] = damage[0] + num;
		}

		// Token: 0x06005231 RID: 21041 RVA: 0x00225E14 File Offset: 0x00224014
		public void realizeSeid111(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int i = this.getSeidJson(seid)["value1"].I;
			if (attaker.isPlayer())
			{
				attaker.shouYuan -= (uint)i;
				return;
			}
			if (NpcJieSuanManager.inst.isCanJieSuan && Tools.instance.MonstarID >= 20000)
			{
				NpcJieSuanManager.inst.npcSetField.AddNpcShouYuan(Tools.instance.MonstarID, -i);
			}
		}

		// Token: 0x06005232 RID: 21042 RVA: 0x00225494 File Offset: 0x00223694
		public void realizeSeid112(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			JSONObject jsonobject = seidJson["value1"];
			JSONObject jsonobject2 = seidJson["value2"];
			for (int i = 0; i < jsonobject.Count; i++)
			{
				receiver.spell.addBuff(jsonobject[i].I, jsonobject2[i].I);
			}
		}

		// Token: 0x06005233 RID: 21043 RVA: 0x00225E88 File Offset: 0x00224088
		public void realizeSeid113(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			JSONObject jsonobject = seidJson["value1"];
			JSONObject jsonobject2 = seidJson["value2"];
			for (int i = 0; i < jsonobject2.Count; i++)
			{
				attaker.spell.addDBuff(jsonobject[i].I, jsonobject2[i].I);
			}
		}

		// Token: 0x06005234 RID: 21044 RVA: 0x00225EE8 File Offset: 0x002240E8
		public void realizeSeid114(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			JSONObject jsonobject = seidJson["value1"];
			JSONObject jsonobject2 = seidJson["value2"];
			Avatar monstar = RoundManager.instance.GetMonstar();
			for (int i = 0; i < jsonobject.Count; i++)
			{
				monstar.spell.addBuff(jsonobject[i].I, jsonobject2[i].I);
			}
		}

		// Token: 0x06005235 RID: 21045 RVA: 0x00225F54 File Offset: 0x00224154
		public void realizeSeid115(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if (attaker.isPlayer())
			{
				if (RoundManager.instance.PlayerUseSkillList.Contains(this.skill_ID))
				{
					damage[2] = 1;
					return;
				}
				RoundManager.instance.PlayerUseSkillList.Add(this.skill_ID);
				return;
			}
			else
			{
				if (RoundManager.instance.NpcUseSkillList.Contains(this.skill_ID))
				{
					damage[2] = 1;
					return;
				}
				RoundManager.instance.NpcUseSkillList.Add(this.skill_ID);
				return;
			}
		}

		// Token: 0x06005236 RID: 21046 RVA: 0x00225FD4 File Offset: 0x002241D4
		public void realizeSeid116(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			int num = attaker.buffmag.GetBuffSum(seidJson["value1"].I) / seidJson["value2"].I;
			int num2 = num * seidJson["value3"].I;
			List<List<int>> buffByID = attaker.buffmag.getBuffByID(seidJson["value1"].I);
			if (buffByID.Count > 0)
			{
				RoundManager.instance.NowSkillUsedLingQiSum += num;
				buffByID[0][1] = 0;
				damage[0] = damage[0] + num2;
			}
		}

		// Token: 0x06005237 RID: 21047 RVA: 0x00226084 File Offset: 0x00224284
		public void realizeSeid118(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int cardNum = attaker.cardMag.getCardNum();
			RoundManager.instance.removeCard(attaker, cardNum);
			JSONObject seidJson = this.getSeidJson(seid);
			List<int> list = seidJson["value1"].ToList();
			List<int> list2 = seidJson["value2"].ToList();
			for (int i = 0; i < list.Count; i++)
			{
				attaker.spell.addBuff(list[i], list2[i] * cardNum);
			}
		}

		// Token: 0x06005238 RID: 21048 RVA: 0x00226100 File Offset: 0x00224300
		public void realizeSeid119(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int value = GlobalValue.Get(this.getSeidJson(seid)["value1"].I, "特性119");
			damage[0] = value;
		}

		// Token: 0x06005239 RID: 21049 RVA: 0x00226138 File Offset: 0x00224338
		public void realizeSeid120(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			int i = seidJson["value1"].I;
			int num = GlobalValue.Get(seidJson["value2"].I, "特性120");
			attaker.spell.addBuff(i, num);
		}

		// Token: 0x0600523A RID: 21050 RVA: 0x00226188 File Offset: 0x00224388
		public void realizeSeid121(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			int i = seidJson["value1"].I;
			int i2 = seidJson["value2"].I;
			GlobalValue.Get(seidJson["value3"].I, "特性121");
		}

		// Token: 0x0600523B RID: 21051 RVA: 0x002261D8 File Offset: 0x002243D8
		public void realizeSeid122(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int num = GlobalValue.Get(this.getSeidJson(seid)["value1"].I, "特性122");
			attaker.setHP(attaker.HP + num);
		}

		// Token: 0x0600523C RID: 21052 RVA: 0x000042DD File Offset: 0x000024DD
		public void realizeSeid140(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
		}

		// Token: 0x0600523D RID: 21053 RVA: 0x00226214 File Offset: 0x00224414
		public void realizeSeid148(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			Avatar targetAvatar = this.getTargetAvatar(seid, attaker);
			int buffRoundByID = targetAvatar.buffmag.GetBuffRoundByID(seidJson["value1"].I);
			if (!Tools.symbol(seidJson["panduan"].str, buffRoundByID, targetAvatar.buffmag.GetBuffSum(seidJson["value2"].I)))
			{
				damage[2] = 1;
			}
		}

		// Token: 0x0600523E RID: 21054 RVA: 0x00226214 File Offset: 0x00224414
		public void realizeSeid149(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			Avatar targetAvatar = this.getTargetAvatar(seid, attaker);
			int buffRoundByID = targetAvatar.buffmag.GetBuffRoundByID(seidJson["value1"].I);
			if (!Tools.symbol(seidJson["panduan"].str, buffRoundByID, targetAvatar.buffmag.GetBuffSum(seidJson["value2"].I)))
			{
				damage[2] = 1;
			}
		}

		// Token: 0x0600523F RID: 21055 RVA: 0x0003AF5A File Offset: 0x0003915A
		public void realizeSeid141(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			attaker.jieyin.AddJinMai((int)this.getSeidJson(seid)["value1"].n);
		}

		// Token: 0x06005240 RID: 21056 RVA: 0x0003AF7E File Offset: 0x0003917E
		public void realizeSeid142(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			attaker.jieyin.AddYiZhi((int)this.getSeidJson(seid)["value1"].n);
		}

		// Token: 0x06005241 RID: 21057 RVA: 0x0003AFA2 File Offset: 0x000391A2
		public void realizeSeid143(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			attaker.jieyin.AddHuaYing((int)this.getSeidJson(seid)["value1"].n);
		}

		// Token: 0x06005242 RID: 21058 RVA: 0x0022628C File Offset: 0x0022448C
		public void realizeSeid144(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int i = this.getSeidJson(seid)["value1"].I;
			attaker.jieyin.AddJinDanHP(i);
			if (i < 0)
			{
				attaker.spell.onBuffTickByType(39, new List<int>());
			}
		}

		// Token: 0x06005243 RID: 21059 RVA: 0x0003AFC6 File Offset: 0x000391C6
		public void realizeSeid146(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			attaker.ZhuJiJinDu += this.getSeidJson(seid)["value1"].I;
		}

		// Token: 0x06005244 RID: 21060 RVA: 0x002262D4 File Offset: 0x002244D4
		public void realizeSeid150(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			this.NowSkillSeid.Clear();
			List<int> flag1 = new List<int>();
			damage.ForEach(delegate(int aa)
			{
				flag1.Add(aa);
			});
			this.triggerStartSeid(new List<int>
			{
				seidJson["value1"].I
			}, flag1, attaker, receiver, type);
			if (flag1[2] == 1)
			{
				this.NowSkillSeid = Tools.JsonListToList(seidJson["value3"]);
				this.triggerStartSeid(this.NowSkillSeid, damage, attaker, receiver, type);
				return;
			}
			this.NowSkillSeid = Tools.JsonListToList(seidJson["value2"]);
			this.triggerStartSeid(this.NowSkillSeid, damage, attaker, receiver, type);
		}

		// Token: 0x06005245 RID: 21061 RVA: 0x0003AFEB File Offset: 0x000391EB
		public void realizeSeid151(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			this.realizeSeid150(seid, damage, attaker, receiver, type);
		}

		// Token: 0x06005246 RID: 21062 RVA: 0x002263A4 File Offset: 0x002245A4
		public void realizeSeid152(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			int buffHasYTime = this.getTargetAvatar(seid, attaker).buffmag.GetBuffHasYTime(seidJson["value1"].I, seidJson["value2"].I);
			if (buffHasYTime > 0)
			{
				damage[4] = buffHasYTime;
				return;
			}
			damage[2] = 1;
		}

		// Token: 0x06005247 RID: 21063 RVA: 0x00226404 File Offset: 0x00224604
		public void realizeSeid153(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			Avatar targetAvatar = this.getTargetAvatar(seid, attaker);
			JSONObject seidJson = this.getSeidJson(seid);
			int buffHasYTime = targetAvatar.buffmag.GetBuffHasYTime(seidJson["value1"].I, seidJson["value2"].I);
			if (buffHasYTime > 0)
			{
				this.RemoveBuff(targetAvatar, seidJson["value1"].I);
				damage[4] = buffHasYTime;
				return;
			}
			damage[2] = 1;
		}

		// Token: 0x06005248 RID: 21064 RVA: 0x000042DD File Offset: 0x000024DD
		public void realizeSeid154(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
		}

		// Token: 0x06005249 RID: 21065 RVA: 0x0022647C File Offset: 0x0022467C
		public void realizeSeid155(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if (this.skillSameCast.Count > 0)
			{
				Dictionary<int, int> xiangSheng = Tools.GetXiangSheng();
				int num = 0;
				if (this.nowSkillUseCard.Count <= 1)
				{
					damage[2] = 1;
				}
				foreach (KeyValuePair<int, int> keyValuePair in this.nowSkillUseCard)
				{
					if (this.nowSkillUseCard.ContainsKey(xiangSheng[keyValuePair.Key]))
					{
						num++;
					}
				}
				if (num < this.nowSkillUseCard.Count - 1)
				{
					damage[2] = 1;
				}
			}
		}

		// Token: 0x0600524A RID: 21066 RVA: 0x00226530 File Offset: 0x00224730
		public void realizeSeid156(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if (this.skillSameCast.Count > 0)
			{
				Dictionary<int, int> xiangKe = Tools.GetXiangKe();
				int num = 0;
				if (this.nowSkillUseCard.Count <= 1)
				{
					damage[2] = 1;
				}
				foreach (KeyValuePair<int, int> keyValuePair in this.nowSkillUseCard)
				{
					if (this.nowSkillUseCard.ContainsKey(xiangKe[keyValuePair.Key]))
					{
						num++;
					}
				}
				if (num < this.nowSkillUseCard.Count - 1)
				{
					damage[2] = 1;
				}
			}
		}

		// Token: 0x0600524B RID: 21067 RVA: 0x0003AFFA File Offset: 0x000391FA
		public void realizeSeid157(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if (this.skillSameCast.Count > 0 && this.nowSkillUseCard.Count != 1)
			{
				damage[2] = 1;
			}
		}

		// Token: 0x0600524C RID: 21068 RVA: 0x002265E4 File Offset: 0x002247E4
		public void realizeSeid158(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			this.NowSkillSeid.Clear();
			List<int> flag1 = new List<int>();
			damage.ForEach(delegate(int aa)
			{
				flag1.Add(aa);
			});
			JSONObject seidJson = this.getSeidJson(seid);
			this.triggerStartSeid(new List<int>
			{
				seidJson["value1"].I
			}, flag1, attaker, receiver, type);
			if (flag1[2] != 1)
			{
				this.NowSkillSeid = Tools.JsonListToList(seidJson["value2"]);
			}
			List<int> flag2 = new List<int>();
			damage.ForEach(delegate(int aa)
			{
				flag2.Add(aa);
			});
			this.triggerStartSeid(new List<int>
			{
				seidJson["value3"].I
			}, flag2, attaker, receiver, type);
			if (flag2[2] != 1)
			{
				seidJson["value4"].list.ForEach(delegate(JSONObject aa)
				{
					this.NowSkillSeid.Add(aa.I);
				});
			}
			this.triggerStartSeid(this.NowSkillSeid, damage, attaker, receiver, type);
		}

		// Token: 0x0600524D RID: 21069 RVA: 0x0022670C File Offset: 0x0022490C
		public void realizeSeid159(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int buffTypeNum = this.getTargetAvatar(seid, attaker).buffmag.getBuffTypeNum(this.getSeidJson(seid)["value1"].I);
			if (buffTypeNum > 0)
			{
				damage[4] = buffTypeNum;
				return;
			}
			damage[2] = 1;
		}

		// Token: 0x0600524E RID: 21070 RVA: 0x0003B020 File Offset: 0x00039220
		public void realizeSeid160(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if (attaker.OtherAvatar.buffmag.HasBuff(this.getSeidJson(seid)["value1"].I))
			{
				damage[2] = 1;
			}
		}

		// Token: 0x0600524F RID: 21071 RVA: 0x0003B052 File Offset: 0x00039252
		public void realizeSeid161(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if (attaker.dunSu <= attaker.OtherAvatar.dunSu)
			{
				damage[2] = 1;
			}
		}

		// Token: 0x06005250 RID: 21072 RVA: 0x00226758 File Offset: 0x00224958
		public void realizeSeid162(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			Avatar targetAvatar = this.getTargetAvatar(seid, attaker);
			JSONObject seidJson = this.getSeidJson(seid);
			int num = this.RemoveBuff(targetAvatar, seidJson["value1"].I);
			receiver.spell.addBuff(seidJson["value2"].I, num);
		}

		// Token: 0x06005251 RID: 21073 RVA: 0x002267AC File Offset: 0x002249AC
		public void realizeSeid164(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int i = this.getSeidJson(seid)["target"].I;
			string str = this.getSeidJson(seid)["panduan"].str;
			int i2 = this.getSeidJson(seid)["value1"].I;
			int statr = (i == 1) ? attaker.shengShi : attaker.OtherAvatar.shengShi;
			if (!Tools.symbol(str, statr, i2))
			{
				damage[2] = 1;
			}
		}

		// Token: 0x06005252 RID: 21074 RVA: 0x00226828 File Offset: 0x00224A28
		public bool PanDuan(string fuhao, int value1, int value2)
		{
			if (fuhao == "=")
			{
				return value1 == value2;
			}
			if (!(fuhao == ">"))
			{
				return fuhao == "<" && value1 < value2;
			}
			return value1 > value2;
		}

		// Token: 0x04005267 RID: 21095
		public string skill_Name;

		// Token: 0x04005268 RID: 21096
		public int skill_ID = -1;

		// Token: 0x04005269 RID: 21097
		public int SkillID;

		// Token: 0x0400526A RID: 21098
		public int Skill_Lv;

		// Token: 0x0400526B RID: 21099
		public string skill_Desc;

		// Token: 0x0400526C RID: 21100
		private Texture2D _skill_Icon;

		// Token: 0x0400526D RID: 21101
		private Texture2D _SkillPingZhi;

		// Token: 0x0400526E RID: 21102
		private Sprite _skillIconSprite;

		// Token: 0x0400526F RID: 21103
		private Sprite _SkillPingZhiSprite;

		// Token: 0x04005270 RID: 21104
		private Sprite _newSkillPingZhiSprite;

		// Token: 0x04005271 RID: 21105
		public int ColorIndex;

		// Token: 0x04005272 RID: 21106
		public int skill_level;

		// Token: 0x04005273 RID: 21107
		public int Max_level;

		// Token: 0x04005274 RID: 21108
		public float CoolDown;

		// Token: 0x04005275 RID: 21109
		public float CurCD;

		// Token: 0x04005276 RID: 21110
		public int Damage;

		// Token: 0x04005277 RID: 21111
		public int SkillQuality;

		// Token: 0x04005278 RID: 21112
		public string weaponuuid = "";

		// Token: 0x04005279 RID: 21113
		public Dictionary<int, int> skillCast = new Dictionary<int, int>();

		// Token: 0x0400527A RID: 21114
		public Dictionary<int, int> skillSameCast = new Dictionary<int, int>();

		// Token: 0x0400527B RID: 21115
		public List<LateDamage> LateDamages = new List<LateDamage>();

		// Token: 0x0400527C RID: 21116
		public List<int> NowSkillSeid = new List<int>();

		// Token: 0x0400527D RID: 21117
		public Dictionary<int, JSONObject> SkillSeidList = new Dictionary<int, JSONObject>();

		// Token: 0x0400527E RID: 21118
		public JSONObject ItemAddSeid;

		// Token: 0x0400527F RID: 21119
		public Dictionary<int, int> nowSkillUseCard = new Dictionary<int, int>();

		// Token: 0x04005280 RID: 21120
		public Dictionary<int, bool> nowSkillIsChuFa = new Dictionary<int, bool>();

		// Token: 0x04005281 RID: 21121
		private static Dictionary<string, MethodInfo> methodDict = new Dictionary<string, MethodInfo>();

		// Token: 0x04005282 RID: 21122
		public bool IsStaticSkill;

		// Token: 0x04005283 RID: 21123
		private bool initedImage;

		// Token: 0x02000D5D RID: 3421
		public enum ALLSkill
		{
			// Token: 0x04005285 RID: 21125
			SKILL34 = 34,
			// Token: 0x04005286 RID: 21126
			SKILL65 = 65,
			// Token: 0x04005287 RID: 21127
			SKILL66,
			// Token: 0x04005288 RID: 21128
			SKILL68 = 68,
			// Token: 0x04005289 RID: 21129
			SKILL69,
			// Token: 0x0400528A RID: 21130
			SKILL97 = 97
		}
	}
}
