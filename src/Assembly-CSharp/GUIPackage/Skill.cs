using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Fungus;
using JSONClass;
using KBEngine;
using script.world_script;
using UnityEngine;
using UnityEngine.Events;
using YSGame;
using YSGame.Fight;

namespace GUIPackage
{
	// Token: 0x02000A50 RID: 2640
	[Serializable]
	public class Skill
	{
		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060048CF RID: 18639 RVA: 0x001EC898 File Offset: 0x001EAA98
		// (set) Token: 0x060048D0 RID: 18640 RVA: 0x001EC8A6 File Offset: 0x001EAAA6
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

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060048D1 RID: 18641 RVA: 0x001EC8AF File Offset: 0x001EAAAF
		// (set) Token: 0x060048D2 RID: 18642 RVA: 0x001EC8BD File Offset: 0x001EAABD
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

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060048D3 RID: 18643 RVA: 0x001EC8C6 File Offset: 0x001EAAC6
		// (set) Token: 0x060048D4 RID: 18644 RVA: 0x001EC8D4 File Offset: 0x001EAAD4
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

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x060048D5 RID: 18645 RVA: 0x001EC8DD File Offset: 0x001EAADD
		// (set) Token: 0x060048D6 RID: 18646 RVA: 0x001EC8EB File Offset: 0x001EAAEB
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

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x060048D7 RID: 18647 RVA: 0x001EC8F4 File Offset: 0x001EAAF4
		// (set) Token: 0x060048D8 RID: 18648 RVA: 0x001EC902 File Offset: 0x001EAB02
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

		// Token: 0x060048D9 RID: 18649 RVA: 0x001EC90C File Offset: 0x001EAB0C
		private static void InitMethod(string methodName)
		{
			if (!Skill.methodDict.ContainsKey(methodName))
			{
				MethodInfo method = typeof(Skill).GetMethod(methodName);
				Skill.methodDict.Add(methodName, method);
			}
		}

		// Token: 0x060048DA RID: 18650 RVA: 0x001EC944 File Offset: 0x001EAB44
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

		// Token: 0x060048DB RID: 18651 RVA: 0x001EC9C4 File Offset: 0x001EABC4
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

		// Token: 0x060048DC RID: 18652 RVA: 0x001ECCCC File Offset: 0x001EAECC
		public Skill(int id, int level, int max)
		{
			this.skillInit(id, level, max);
		}

		// Token: 0x060048DD RID: 18653 RVA: 0x001ECD48 File Offset: 0x001EAF48
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

		// Token: 0x060048DE RID: 18654 RVA: 0x001ECF2C File Offset: 0x001EB12C
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

		// Token: 0x060048DF RID: 18655 RVA: 0x001ED0E4 File Offset: 0x001EB2E4
		public Skill()
		{
			this.skill_ID = -1;
		}

		// Token: 0x060048E0 RID: 18656 RVA: 0x001ED160 File Offset: 0x001EB360
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

		// Token: 0x060048E1 RID: 18657 RVA: 0x001ED257 File Offset: 0x001EB457
		public Skill Clone()
		{
			return base.MemberwiseClone() as Skill;
		}

		// Token: 0x060048E2 RID: 18658 RVA: 0x001ED264 File Offset: 0x001EB464
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

		// Token: 0x060048E3 RID: 18659 RVA: 0x001EDCF0 File Offset: 0x001EBEF0
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

		// Token: 0x060048E4 RID: 18660 RVA: 0x001EDD94 File Offset: 0x001EBF94
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

		// Token: 0x060048E5 RID: 18661 RVA: 0x001EDFCC File Offset: 0x001EC1CC
		public int getNomelCastNum(Avatar attaker)
		{
			int num = 0;
			foreach (KeyValuePair<int, int> keyValuePair in this.getSkillCast(attaker))
			{
				num += keyValuePair.Value;
			}
			return num;
		}

		// Token: 0x060048E6 RID: 18662 RVA: 0x001EE028 File Offset: 0x001EC228
		public int GetSameCastNum(Avatar attaker)
		{
			int num = 0;
			foreach (KeyValuePair<int, int> keyValuePair in this.skillSameCast)
			{
				num += keyValuePair.Value;
			}
			return num;
		}

		// Token: 0x060048E7 RID: 18663 RVA: 0x001EE084 File Offset: 0x001EC284
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

		// Token: 0x060048E8 RID: 18664 RVA: 0x001EE1B8 File Offset: 0x001EC3B8
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

		// Token: 0x060048E9 RID: 18665 RVA: 0x001EE280 File Offset: 0x001EC480
		public bool setSeidNum(List<int> TempSeid, List<int> infoFlag, int _index)
		{
			return infoFlag[4] > 0 && !TempSeid.Contains(139);
		}

		// Token: 0x060048EA RID: 18666 RVA: 0x001EE2A0 File Offset: 0x001EC4A0
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
					else if (_attaker.isPlayer())
					{
						if (RoundManager.instance.PlayerSkillCheck != null)
						{
							RoundManager.instance.PlayerSkillCheck.HasPassSeid.Add(num3);
						}
					}
					else if (RoundManager.instance.NpcSkillCheck != null)
					{
						RoundManager.instance.NpcSkillCheck.HasPassSeid.Add(num3);
					}
				}
				catch (Exception ex)
				{
					string text = "";
					for (int k = 0; k < infoFlag.Count; k++)
					{
						text += string.Format(" {0}", infoFlag[k]);
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

		// Token: 0x060048EB RID: 18667 RVA: 0x001EE514 File Offset: 0x001EC714
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

		// Token: 0x060048EC RID: 18668 RVA: 0x001EE644 File Offset: 0x001EC844
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

		// Token: 0x060048ED RID: 18669 RVA: 0x001EE770 File Offset: 0x001EC970
		public static void SetSkillFlag(List<int> infoFlag, int damage, int skill_ID)
		{
			infoFlag.Add(damage);
			infoFlag.Add(skill_ID);
			infoFlag.Add(0);
			infoFlag.Add(0);
			infoFlag.Add(0);
		}

		// Token: 0x060048EE RID: 18670 RVA: 0x001EE798 File Offset: 0x001EC998
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
			avatar.spell.onBuffTickByType(47, list);
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

		// Token: 0x060048EF RID: 18671 RVA: 0x001EEDBC File Offset: 0x001ECFBC
		public List<int> PutingSkill(Entity _attaker, Entity _receiver, int type = 0)
		{
			Avatar avatar = (Avatar)_attaker;
			Avatar avatar2 = (Avatar)_receiver;
			this.NowSkillSeid.Clear();
			RoundManager.instance.CurSkill = this;
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
			avatar.spell.onBuffTickByType(47, list);
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

		// Token: 0x060048F0 RID: 18672 RVA: 0x001EF490 File Offset: 0x001ED690
		public Avatar getTargetAvatar(int seid, Avatar attker)
		{
			if (this.getSeidJson(seid)["target"].I == 1)
			{
				return attker;
			}
			return attker.OtherAvatar;
		}

		// Token: 0x060048F1 RID: 18673 RVA: 0x001EF4B4 File Offset: 0x001ED6B4
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

		// Token: 0x060048F2 RID: 18674 RVA: 0x001EF5A0 File Offset: 0x001ED7A0
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

		// Token: 0x060048F3 RID: 18675 RVA: 0x001EF968 File Offset: 0x001EDB68
		public void Puting(Entity _attaker, Entity _receiver, int type = 0, string uuid = "")
		{
			Avatar attaker = (Avatar)_attaker;
			Avatar receiver = (Avatar)_receiver;
			this.attack = attaker;
			this.target = receiver;
			this.type = type;
			if (this.CanUse(_attaker, _receiver, true, uuid) != SkillCanUseType.可以使用)
			{
				return;
			}
			if (attaker.isPlayer())
			{
				RoundManager.instance.PlayerSkillCheck = new SkillCheck
				{
					SkillId = this.skill_ID
				};
			}
			else
			{
				RoundManager.instance.NpcSkillCheck = new SkillCheck
				{
					SkillId = this.skill_ID
				};
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
				fightTalk.SetIntegerVariable("SkillID", jsonData.instance.skillJsonData[this.skill_ID.ToString()]["Skill_ID"].I);
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

		// Token: 0x060048F4 RID: 18676 RVA: 0x001EFB60 File Offset: 0x001EDD60
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

		// Token: 0x060048F5 RID: 18677 RVA: 0x001EFBE4 File Offset: 0x001EDDE4
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

		// Token: 0x060048F6 RID: 18678 RVA: 0x001EFC68 File Offset: 0x001EDE68
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

		// Token: 0x060048F7 RID: 18679 RVA: 0x001EFCEC File Offset: 0x001EDEEC
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
					if (num != 124)
					{
						if (num == 160)
						{
							if (attaker.OtherAvatar.buffmag.HasBuff(this.getSeidJson(num)["value1"].I))
							{
								return false;
							}
						}
					}
					else
					{
						if (attaker.UsedSkills.Count <= 0)
						{
							return false;
						}
						bool flag2 = true;
						int i2 = this.getSeidJson(num)["value1"].I;
						using (List<int>.Enumerator enumerator2 = _skillJsonData.DataDict[attaker.UsedSkills[attaker.UsedSkills.Count - 1]].AttackType.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								if (enumerator2.Current == i2)
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
				}
				else if (attaker.crystal.getCardNum() != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060048F8 RID: 18680 RVA: 0x001F0040 File Offset: 0x001EE240
		public int getCrystalNum(Avatar attaker)
		{
			return attaker.crystal.getCardNum();
		}

		// Token: 0x060048F9 RID: 18681 RVA: 0x001F0050 File Offset: 0x001EE250
		public void setSeidSkillFlag(Avatar attaker, int seid, int num)
		{
			if (!attaker.SkillSeidFlag.ContainsKey(seid))
			{
				attaker.SkillSeidFlag.Add(seid, new Dictionary<int, int>());
				attaker.SkillSeidFlag[seid].Add(this.skill_ID, 0);
			}
			attaker.SkillSeidFlag[seid][this.skill_ID] = num;
		}

		// Token: 0x060048FA RID: 18682 RVA: 0x001F00AC File Offset: 0x001EE2AC
		public int GetSeidSkillFlag(Avatar attaker, int seid)
		{
			if (!attaker.SkillSeidFlag.ContainsKey(seid))
			{
				attaker.SkillSeidFlag.Add(seid, new Dictionary<int, int>());
				attaker.SkillSeidFlag[seid].Add(this.skill_ID, 0);
			}
			return attaker.SkillSeidFlag[seid][this.skill_ID];
		}

		// Token: 0x060048FB RID: 18683 RVA: 0x001F0108 File Offset: 0x001EE308
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

		// Token: 0x060048FC RID: 18684 RVA: 0x001F0170 File Offset: 0x001EE370
		public void reduceBuff(Avatar Targ, int X, int Y)
		{
			List<int> list = Targ.buffmag.getBuffByID(X)[0];
			list[1] = list[1] - Y;
		}

		// Token: 0x060048FD RID: 18685 RVA: 0x001F01A0 File Offset: 0x001EE3A0
		public int RemoveBuff(Avatar target, int BuffID)
		{
			return target.buffmag.RemoveBuff(BuffID);
		}

		// Token: 0x060048FE RID: 18686 RVA: 0x001F01B0 File Offset: 0x001EE3B0
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

		// Token: 0x060048FF RID: 18687 RVA: 0x001F025C File Offset: 0x001EE45C
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

		// Token: 0x06004900 RID: 18688 RVA: 0x001F0380 File Offset: 0x001EE580
		public void realizeBuffEndSeid1(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int num = (int)((float)damage[0] * this.getSeidJson(seid)["value1"].n);
			attaker.recvDamage(attaker, attaker, this.skill_ID, -num, type);
		}

		// Token: 0x06004901 RID: 18689 RVA: 0x001F03C4 File Offset: 0x001EE5C4
		public void realizeSeid2(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int i = this.getSeidJson(seid)["value1"].I;
			RoundManager.instance.RandomDrawCard(attaker, i);
			RoundManager.instance.chengeCrystal();
		}

		// Token: 0x06004902 RID: 18690 RVA: 0x001F0400 File Offset: 0x001EE600
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

		// Token: 0x06004903 RID: 18691 RVA: 0x001F0530 File Offset: 0x001EE730
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

		// Token: 0x06004904 RID: 18692 RVA: 0x001F05C4 File Offset: 0x001EE7C4
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

		// Token: 0x06004905 RID: 18693 RVA: 0x001F0626 File Offset: 0x001EE826
		public void realizeSeid6(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			this.CurCD = 50000f;
		}

		// Token: 0x06004906 RID: 18694 RVA: 0x001F0634 File Offset: 0x001EE834
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

		// Token: 0x06004907 RID: 18695 RVA: 0x00004095 File Offset: 0x00002295
		public void realizeSeid8(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
		}

		// Token: 0x06004908 RID: 18696 RVA: 0x001F06A8 File Offset: 0x001EE8A8
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

		// Token: 0x06004909 RID: 18697 RVA: 0x001F0744 File Offset: 0x001EE944
		public void realizeSeid10(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if (attaker.shengShi <= attaker.OtherAvatar.shengShi)
			{
				damage[2] = 1;
			}
		}

		// Token: 0x0600490A RID: 18698 RVA: 0x001F0764 File Offset: 0x001EE964
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
				if (this.skill_ID == 12508)
				{
					int buffSum = attaker.buffmag.GetBuffSum(i3);
					this.LateDamages.Add(new LateDamage
					{
						SkillId = i2,
						Damage = buffSum
					});
				}
				else if (this.SkillID == 1170)
				{
					int damage2 = GlobalValue.Get(i3, "unknow");
					this.LateDamages.Add(new LateDamage
					{
						SkillId = i2,
						Damage = damage2
					});
				}
				else
				{
					this.LateDamages.Add(new LateDamage
					{
						SkillId = i2,
						Damage = i3
					});
				}
			}
		}

		// Token: 0x0600490B RID: 18699 RVA: 0x001F0864 File Offset: 0x001EEA64
		public void realizeSeid12(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			RoundManager.instance.removeCard(receiver, this.getSeidJson(seid)["value1"].I);
		}

		// Token: 0x0600490C RID: 18700 RVA: 0x001F0888 File Offset: 0x001EEA88
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

		// Token: 0x0600490D RID: 18701 RVA: 0x00004095 File Offset: 0x00002295
		public void realizeSeid14(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
		}

		// Token: 0x0600490E RID: 18702 RVA: 0x001F0928 File Offset: 0x001EEB28
		public void realizeSeid15(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			RoundManager.instance.removeCard(receiver, seidJson["value1"].I, seidJson["value2"].I);
		}

		// Token: 0x0600490F RID: 18703 RVA: 0x001F096C File Offset: 0x001EEB6C
		public void realizeSeid16(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int listSum = RoundManager.instance.getListSum(receiver.crystal);
			int count = (int)(receiver.NowCard - (uint)listSum);
			RoundManager.instance.RandomDrawCard(attaker, count);
		}

		// Token: 0x06004910 RID: 18704 RVA: 0x001F09A4 File Offset: 0x001EEBA4
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

		// Token: 0x06004911 RID: 18705 RVA: 0x001F0A0C File Offset: 0x001EEC0C
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

		// Token: 0x06004912 RID: 18706 RVA: 0x001F0A7C File Offset: 0x001EEC7C
		public void realizeSeid19(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int listSum = RoundManager.instance.getListSum(receiver.crystal);
			RoundManager.instance.removeCard(receiver, listSum);
		}

		// Token: 0x06004913 RID: 18707 RVA: 0x001F0AA8 File Offset: 0x001EECA8
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

		// Token: 0x06004914 RID: 18708 RVA: 0x001F0B38 File Offset: 0x001EED38
		public void realizeSeid21(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int i = this.getSeidJson(seid)["value1"].I;
			RoundManager.instance.removeCard(receiver, receiver.crystal[i], i);
		}

		// Token: 0x06004915 RID: 18709 RVA: 0x00004095 File Offset: 0x00002295
		public void realizeSeid22(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
		}

		// Token: 0x06004916 RID: 18710 RVA: 0x001F0B78 File Offset: 0x001EED78
		public void realizeSeid23(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			damage[0] = damage[0] + (int)(this.getSeidJson(seid)["value1"].n * (float)attaker.useSkillNum);
		}

		// Token: 0x06004917 RID: 18711 RVA: 0x001F0BB8 File Offset: 0x001EEDB8
		public void realizeSeid24(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if (!attaker.SkillSeidFlag.ContainsKey(seid))
			{
				attaker.SkillSeidFlag.Add(seid, new Dictionary<int, int>());
				attaker.SkillSeidFlag[seid].Add(0, 1);
			}
			attaker.SkillSeidFlag[seid][0] = 1;
		}

		// Token: 0x06004918 RID: 18712 RVA: 0x001F0C0A File Offset: 0x001EEE0A
		public void realizeSeid25(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			attaker.spell.addDBuff(5, attaker.HP_Max - attaker.HP);
		}

		// Token: 0x06004919 RID: 18713 RVA: 0x001F0C28 File Offset: 0x001EEE28
		public void realizeSeid26(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			damage[0] = receiver.HP / 2;
			int i = this.getSeidJson(seid)["value1"].I;
			if (damage[0] > i)
			{
				damage[0] = i;
			}
		}

		// Token: 0x0600491A RID: 18714 RVA: 0x001F0C70 File Offset: 0x001EEE70
		public void realizeSeid27(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int listSum = RoundManager.instance.getListSum(attaker.crystal);
			damage[0] = damage[0] + listSum * this.getSeidJson(seid)["value1"].I;
		}

		// Token: 0x0600491B RID: 18715 RVA: 0x001F0CB8 File Offset: 0x001EEEB8
		public void realizeFinalSeid28(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int num = receiver.HP - damage[0] - receiver.HP_Max;
			if (num > 0)
			{
				receiver.spell.addDBuff(5, num);
			}
		}

		// Token: 0x0600491C RID: 18716 RVA: 0x001F0CF0 File Offset: 0x001EEEF0
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

		// Token: 0x0600491D RID: 18717 RVA: 0x001F0DD4 File Offset: 0x001EEFD4
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

		// Token: 0x0600491E RID: 18718 RVA: 0x001F0E68 File Offset: 0x001EF068
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

		// Token: 0x0600491F RID: 18719 RVA: 0x001F0F00 File Offset: 0x001EF100
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

		// Token: 0x06004920 RID: 18720 RVA: 0x001F0F84 File Offset: 0x001EF184
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

		// Token: 0x06004921 RID: 18721 RVA: 0x001F1006 File Offset: 0x001EF206
		public void realizeBuffEndSeid34(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			damage[0] = damage[0] * this.getSeidJson(seid)["value1"].I;
		}

		// Token: 0x06004922 RID: 18722 RVA: 0x00004095 File Offset: 0x00002295
		public void realizeSeidFinal34(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
		}

		// Token: 0x06004923 RID: 18723 RVA: 0x001F1030 File Offset: 0x001EF230
		public void realizeSeid35(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			damage[0] = damage[0] + attaker.NowRoundUsedCard.Count * this.getSeidJson(seid)["value1"].I;
		}

		// Token: 0x06004924 RID: 18724 RVA: 0x001F1070 File Offset: 0x001EF270
		public void realizeSeid36(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			int value1 = seidJson["value1"].I;
			int j = seidJson["value2"].I;
			int count = attaker.NowRoundUsedCard.FindAll((int i) => i == value1).Count;
			damage[0] = damage[0] + count * j;
		}

		// Token: 0x06004925 RID: 18725 RVA: 0x001F10E8 File Offset: 0x001EF2E8
		public void realizeSeid37(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int cardNum = attaker.crystal.getCardNum();
			attaker.recvDamage(attaker, attaker, this.skill_ID, -cardNum * this.getSeidJson(seid)["value1"].I, type);
		}

		// Token: 0x06004926 RID: 18726 RVA: 0x001F112C File Offset: 0x001EF32C
		public void realizeSeid38(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			int num = attaker.crystal[seidJson["value1"].I];
			damage[0] = damage[0] + num * seidJson["value2"].I;
		}

		// Token: 0x06004927 RID: 18727 RVA: 0x001F1180 File Offset: 0x001EF380
		public void realizeSeid39(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int listSum = RoundManager.instance.getListSum(attaker.crystal);
			RoundManager.instance.removeCard(attaker, listSum);
			damage[0] = damage[0] + listSum * this.getSeidJson(seid)["value1"].I;
		}

		// Token: 0x06004928 RID: 18728 RVA: 0x001F11D4 File Offset: 0x001EF3D4
		public void realizeSeid40(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			int i = seidJson["value1"].I;
			int i2 = seidJson["value2"].I;
			int num = attaker.crystal[i];
			RoundManager.instance.removeCard(attaker, num, i);
			damage[0] = damage[0] + num * i2;
		}

		// Token: 0x06004929 RID: 18729 RVA: 0x001F1238 File Offset: 0x001EF438
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

		// Token: 0x0600492A RID: 18730 RVA: 0x001F1350 File Offset: 0x001EF550
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

		// Token: 0x0600492B RID: 18731 RVA: 0x001F13C4 File Offset: 0x001EF5C4
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

		// Token: 0x0600492C RID: 18732 RVA: 0x001F1468 File Offset: 0x001EF668
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

		// Token: 0x0600492D RID: 18733 RVA: 0x001F14DC File Offset: 0x001EF6DC
		public void realizeSeid45(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			this.reduceBuff(attaker, seidJson["value1"].I, seidJson["value2"].I);
			damage[0] = damage[0] + seidJson["value3"].I;
		}

		// Token: 0x0600492E RID: 18734 RVA: 0x001F153C File Offset: 0x001EF73C
		public void realizeSeid46(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			this.reduceBuff(attaker, seidJson["value1"].I, seidJson["value2"].I);
			attaker.recvDamage(attaker, attaker, this.skill_ID, -seidJson["value3"].I, type);
		}

		// Token: 0x0600492F RID: 18735 RVA: 0x001F159C File Offset: 0x001EF79C
		public void realizeSeid47(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			this.reduceBuff(attaker, seidJson["value1"].I, seidJson["value2"].I);
			this.createSkill(attaker, Tools.instance.getSkillKeyByID(seidJson["value3"].I, attaker));
		}

		// Token: 0x06004930 RID: 18736 RVA: 0x001F15FC File Offset: 0x001EF7FC
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

		// Token: 0x06004931 RID: 18737 RVA: 0x001F1670 File Offset: 0x001EF870
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

		// Token: 0x06004932 RID: 18738 RVA: 0x001F1734 File Offset: 0x001EF934
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

		// Token: 0x06004933 RID: 18739 RVA: 0x001F1798 File Offset: 0x001EF998
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

		// Token: 0x06004934 RID: 18740 RVA: 0x001F1874 File Offset: 0x001EFA74
		public void realizeSeid52(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if (!receiver.buffmag.HasBuff(this.getSeidJson(seid)["value1"].I))
			{
				damage[2] = 1;
			}
		}

		// Token: 0x06004935 RID: 18741 RVA: 0x001F18A2 File Offset: 0x001EFAA2
		public void realizeSeid53(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if (this.getCrystalNum(receiver) != 0)
			{
				damage[2] = 1;
			}
		}

		// Token: 0x06004936 RID: 18742 RVA: 0x001F18B8 File Offset: 0x001EFAB8
		public void realizeSeid54(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int crystalNum = this.getCrystalNum(attaker);
			int i = this.getSeidJson(seid)["value1"].I;
			if (crystalNum < i)
			{
				damage[2] = 1;
			}
		}

		// Token: 0x06004937 RID: 18743 RVA: 0x001F18EE File Offset: 0x001EFAEE
		public void realizeSeid55(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if (RoundManager.instance.drawCard(attaker).cardType != this.getSeidJson(seid)["value1"].I)
			{
				damage[2] = 1;
			}
		}

		// Token: 0x06004938 RID: 18744 RVA: 0x001F1920 File Offset: 0x001EFB20
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

		// Token: 0x06004939 RID: 18745 RVA: 0x001F1990 File Offset: 0x001EFB90
		public void realizeSeid57(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			for (int i = 0; i < seidJson["value1"].Count; i++)
			{
				this.reduceBuff(attaker, seidJson["value1"][i].I, seidJson["value2"][i].I);
			}
		}

		// Token: 0x0600493A RID: 18746 RVA: 0x001F19F4 File Offset: 0x001EFBF4
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

		// Token: 0x0600493B RID: 18747 RVA: 0x001F1AA4 File Offset: 0x001EFCA4
		public void realizeSeid59(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			attaker.recvDamage(attaker, attaker, this.skill_ID, -attaker.HP_Max * (int)this.getSeidJson(seid)["value1"].n / 100, type);
		}

		// Token: 0x0600493C RID: 18748 RVA: 0x001F1AD9 File Offset: 0x001EFCD9
		public void realizeSeid60(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			attaker.recvDamage(attaker, attaker, this.skill_ID, -(attaker.HP_Max - attaker.HP) * (int)this.getSeidJson(seid)["value1"].n / 100, type);
		}

		// Token: 0x0600493D RID: 18749 RVA: 0x001F1B18 File Offset: 0x001EFD18
		public void realizeSeid61(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			damage[0] = damage[0] + attaker.HP_Max * (int)this.getSeidJson(seid)["value1"].n / 100;
		}

		// Token: 0x0600493E RID: 18750 RVA: 0x001F1B58 File Offset: 0x001EFD58
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

		// Token: 0x0600493F RID: 18751 RVA: 0x001F1C2C File Offset: 0x001EFE2C
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

		// Token: 0x06004940 RID: 18752 RVA: 0x001F1CD0 File Offset: 0x001EFED0
		public void realizeSeid64(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			attaker.recvDamage(attaker, receiver, this.skill_ID, receiver.HP_Max * (int)this.getSeidJson(seid)["value1"].n / 100, type);
		}

		// Token: 0x06004941 RID: 18753 RVA: 0x00004095 File Offset: 0x00002295
		public void realizeSeid65(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
		}

		// Token: 0x06004942 RID: 18754 RVA: 0x001F1D08 File Offset: 0x001EFF08
		public void realizeSeid66(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			damage[0] = damage[0] + (int)this.getSeidJson(seid)["value1"].n;
		}

		// Token: 0x06004943 RID: 18755 RVA: 0x001F1D40 File Offset: 0x001EFF40
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

		// Token: 0x06004944 RID: 18756 RVA: 0x00004095 File Offset: 0x00002295
		public void realizeSeid68(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
		}

		// Token: 0x06004945 RID: 18757 RVA: 0x00004095 File Offset: 0x00002295
		public void realizeSeid69(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
		}

		// Token: 0x06004946 RID: 18758 RVA: 0x001F1DF1 File Offset: 0x001EFFF1
		public void realizeSeid70(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			RoundManager.instance.startRound(attaker);
		}

		// Token: 0x06004947 RID: 18759 RVA: 0x001F1E00 File Offset: 0x001F0000
		public void realizeSeid71(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			attaker.setHP(attaker.HP - seidJson["value1"].I);
			attaker.spell.addDBuff(seidJson["value2"].I, seidJson["value3"].I);
		}

		// Token: 0x06004948 RID: 18760 RVA: 0x001F1E60 File Offset: 0x001F0060
		public void realizeSeid72(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int listSum = RoundManager.instance.getListSum(receiver.crystal);
			RoundManager.instance.removeCard(receiver, listSum);
			RoundManager.instance.RandomDrawCard(attaker, listSum);
		}

		// Token: 0x06004949 RID: 18761 RVA: 0x001F1E98 File Offset: 0x001F0098
		public void realizeSeid73(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			damage[0] = damage[0] + attaker.fightTemp.lastRoundDamage[1] / 2;
		}

		// Token: 0x0600494A RID: 18762 RVA: 0x001F1EC8 File Offset: 0x001F00C8
		public void realizeSeid74(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			foreach (List<int> buffid in attaker.buffmag.getAllBuffByType(this.getSeidJson(seid)["value1"].I))
			{
				attaker.spell.removeBuff(buffid);
			}
		}

		// Token: 0x0600494B RID: 18763 RVA: 0x001F1F3C File Offset: 0x001F013C
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

		// Token: 0x0600494C RID: 18764 RVA: 0x001F2030 File Offset: 0x001F0230
		public void realizeSeid77(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if ((int)((float)attaker.HP / (float)attaker.HP_Max * 100f) > this.getSeidJson(seid)["value1"].I)
			{
				damage[2] = 1;
			}
		}

		// Token: 0x0600494D RID: 18765 RVA: 0x001F2068 File Offset: 0x001F0268
		public void realizeSeid78(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if (attaker.crystal.getCardNum() != 0)
			{
				damage[2] = 1;
			}
		}

		// Token: 0x0600494E RID: 18766 RVA: 0x001F2080 File Offset: 0x001F0280
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

		// Token: 0x0600494F RID: 18767 RVA: 0x001F20E4 File Offset: 0x001F02E4
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

		// Token: 0x06004950 RID: 18768 RVA: 0x001F2144 File Offset: 0x001F0344
		public void realizeSeid81(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			attaker.cardMag.getCardNum();
			int i = this.getSeidJson(seid)["value1"].I;
			RoundManager.instance.removeCard(attaker, i);
		}

		// Token: 0x06004951 RID: 18769 RVA: 0x001F2180 File Offset: 0x001F0380
		public void realizeSeid82(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			List<List<int>> buffByID = receiver.buffmag.getBuffByID(seidJson["value1"].I);
			if (buffByID.Count > 0)
			{
				buffByID[0][1] = seidJson["value2"].I;
			}
		}

		// Token: 0x06004952 RID: 18770 RVA: 0x001F21D8 File Offset: 0x001F03D8
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

		// Token: 0x06004953 RID: 18771 RVA: 0x001F22A0 File Offset: 0x001F04A0
		public void realizeSeid84(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			int num = (attaker.shengShi > attaker.OtherAvatar.shengShi) ? (attaker.shengShi - attaker.OtherAvatar.shengShi) : 0;
			damage[0] = damage[0] + (int)((float)num / seidJson["value1"].n) * seidJson["value2"].I;
		}

		// Token: 0x06004954 RID: 18772 RVA: 0x001F2314 File Offset: 0x001F0514
		public void realizeSeid85(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int dunSu = attaker.dunSu;
			JSONObject seidJson = this.getSeidJson(seid);
			damage[0] = damage[0] + (int)((float)dunSu / seidJson["value1"].n) * seidJson["value2"].I;
		}

		// Token: 0x06004955 RID: 18773 RVA: 0x001F2366 File Offset: 0x001F0566
		public void realizeSeid86(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			attaker.recvDamage(attaker, receiver, this.skill_ID, receiver.HP * (int)this.getSeidJson(seid)["value1"].n / 100, type);
		}

		// Token: 0x06004956 RID: 18774 RVA: 0x001F239C File Offset: 0x001F059C
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

		// Token: 0x06004957 RID: 18775 RVA: 0x001F249C File Offset: 0x001F069C
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

		// Token: 0x06004958 RID: 18776 RVA: 0x001F24EA File Offset: 0x001F06EA
		public void realizeSeid89(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if (receiver.cardMag[(int)this.getSeidJson(seid)["value1"].n] <= 0)
			{
				damage[2] = 1;
			}
		}

		// Token: 0x06004959 RID: 18777 RVA: 0x001F251C File Offset: 0x001F071C
		public void realizeSeid90(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			int num = attaker.OtherAvatar.crystal[seidJson["value1"].I];
			damage[0] = damage[0] + num * seidJson["value2"].I;
		}

		// Token: 0x0600495A RID: 18778 RVA: 0x001F2578 File Offset: 0x001F0778
		public void realizeSeid91(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			int value1 = seidJson["value1"].I;
			int count = attaker.UsedSkills.FindAll((int id) => _skillJsonData.DataDict[id].Skill_ID == value1).Count;
			damage[0] = damage[0] + count * seidJson["value2"].I;
		}

		// Token: 0x0600495B RID: 18779 RVA: 0x001F25EC File Offset: 0x001F07EC
		public void realizeSeid92(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			RoundManager.instance.removeCard(attaker, seidJson["value1"].I, seidJson["value2"].I);
		}

		// Token: 0x0600495C RID: 18780 RVA: 0x001F262C File Offset: 0x001F082C
		public void realizeSeid94(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			List<int> list = receiver.spell.addDBuff(seidJson["value1"].I);
			if (list != null)
			{
				list[1] = seidJson["value2"].I;
			}
		}

		// Token: 0x0600495D RID: 18781 RVA: 0x001F2678 File Offset: 0x001F0878
		public void realizeSeid95(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if (!receiver.buffmag.HasXTypeBuff(this.getSeidJson(seid)["value1"].I))
			{
				damage[2] = 1;
			}
		}

		// Token: 0x0600495E RID: 18782 RVA: 0x001F26A8 File Offset: 0x001F08A8
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

		// Token: 0x0600495F RID: 18783 RVA: 0x001F2738 File Offset: 0x001F0938
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

		// Token: 0x06004960 RID: 18784 RVA: 0x001F279D File Offset: 0x001F099D
		public void realizeSeid101(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			attaker.setHP((int)this.getSeidJson(seid)["value1"].n);
		}

		// Token: 0x06004961 RID: 18785 RVA: 0x001F27BC File Offset: 0x001F09BC
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

		// Token: 0x06004962 RID: 18786 RVA: 0x001F2898 File Offset: 0x001F0A98
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

		// Token: 0x06004963 RID: 18787 RVA: 0x001F2938 File Offset: 0x001F0B38
		public void realizeFinalSeid104(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			Tools.instance.ToolsStartCoroutine(this.ISeid104(seid));
		}

		// Token: 0x06004964 RID: 18788 RVA: 0x001F294B File Offset: 0x001F0B4B
		public IEnumerator ISeid104(int seid)
		{
			yield return new WaitForSeconds(0.01f);
			this.skillInit(this.getSeidJson(seid)["value1"].I, 0, 10);
			yield break;
		}

		// Token: 0x06004965 RID: 18789 RVA: 0x001F2964 File Offset: 0x001F0B64
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

		// Token: 0x06004966 RID: 18790 RVA: 0x001F29DC File Offset: 0x001F0BDC
		public void realizeSeid107(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			Avatar targetAvatar = this.getTargetAvatar(seid, attaker);
			int buffSum = targetAvatar.buffmag.GetBuffSum(this.getSeidJson(seid)["value1"].I);
			if (buffSum > 0)
			{
				targetAvatar.recvDamage(attaker, attaker, this.skill_ID, -buffSum, type);
			}
		}

		// Token: 0x06004967 RID: 18791 RVA: 0x001F2A2C File Offset: 0x001F0C2C
		public void realizeSeid108(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			int num = attaker.crystal[seidJson["value1"].I];
			attaker.spell.addBuff(seidJson["value2"].I, seidJson["value3"].I * num);
		}

		// Token: 0x06004968 RID: 18792 RVA: 0x001F2A8C File Offset: 0x001F0C8C
		public void realizeSeid110(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int i = this.getSeidJson(seid)["value1"].I;
			int num = attaker.HP * i / 100;
			attaker.setHP(attaker.HP - num);
			damage[0] = damage[0] + num;
		}

		// Token: 0x06004969 RID: 18793 RVA: 0x001F2ADC File Offset: 0x001F0CDC
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

		// Token: 0x0600496A RID: 18794 RVA: 0x001F2B50 File Offset: 0x001F0D50
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

		// Token: 0x0600496B RID: 18795 RVA: 0x001F2BB4 File Offset: 0x001F0DB4
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

		// Token: 0x0600496C RID: 18796 RVA: 0x001F2C14 File Offset: 0x001F0E14
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

		// Token: 0x0600496D RID: 18797 RVA: 0x001F2C80 File Offset: 0x001F0E80
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

		// Token: 0x0600496E RID: 18798 RVA: 0x001F2D00 File Offset: 0x001F0F00
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

		// Token: 0x0600496F RID: 18799 RVA: 0x001F2DB0 File Offset: 0x001F0FB0
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

		// Token: 0x06004970 RID: 18800 RVA: 0x001F2E2C File Offset: 0x001F102C
		public void realizeSeid119(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int value = GlobalValue.Get(this.getSeidJson(seid)["value1"].I, "特性119");
			damage[0] = value;
		}

		// Token: 0x06004971 RID: 18801 RVA: 0x001F2E64 File Offset: 0x001F1064
		public void realizeSeid120(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			int i = seidJson["value1"].I;
			int num = GlobalValue.Get(seidJson["value2"].I, "特性120");
			attaker.spell.addBuff(i, num);
		}

		// Token: 0x06004972 RID: 18802 RVA: 0x00004095 File Offset: 0x00002295
		public void realizeSeid121(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
		}

		// Token: 0x06004973 RID: 18803 RVA: 0x001F2EB4 File Offset: 0x001F10B4
		public void realizeSeid122(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int num = GlobalValue.Get(this.getSeidJson(seid)["value1"].I, "特性122");
			attaker.setHP(attaker.HP + num);
		}

		// Token: 0x06004974 RID: 18804 RVA: 0x001F2EF0 File Offset: 0x001F10F0
		public void realizeSeid123(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			JSONObject seidJson = this.getSeidJson(seid);
			List<JSONObject> list = seidJson["value1"].list;
			List<JSONObject> list2 = seidJson["value2"].list;
			foreach (KeyValuePair<int, int> keyValuePair in this.nowSkillUseCard)
			{
				if (keyValuePair.Value > 0)
				{
					attaker.spell.addBuff(list[keyValuePair.Key].I, list2[keyValuePair.Key].I);
				}
			}
		}

		// Token: 0x06004975 RID: 18805 RVA: 0x001F2FA0 File Offset: 0x001F11A0
		public void realizeSeid124(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
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

		// Token: 0x06004976 RID: 18806 RVA: 0x00004095 File Offset: 0x00002295
		public void realizeSeid140(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
		}

		// Token: 0x06004977 RID: 18807 RVA: 0x001F3050 File Offset: 0x001F1250
		public void realizeSeid141(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			attaker.jieyin.AddJinMai((int)this.getSeidJson(seid)["value1"].n);
		}

		// Token: 0x06004978 RID: 18808 RVA: 0x001F3074 File Offset: 0x001F1274
		public void realizeSeid142(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			attaker.jieyin.AddYiZhi((int)this.getSeidJson(seid)["value1"].n);
		}

		// Token: 0x06004979 RID: 18809 RVA: 0x001F3098 File Offset: 0x001F1298
		public void realizeSeid143(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			attaker.jieyin.AddHuaYing((int)this.getSeidJson(seid)["value1"].n);
		}

		// Token: 0x0600497A RID: 18810 RVA: 0x001F30BC File Offset: 0x001F12BC
		public void realizeSeid144(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int i = this.getSeidJson(seid)["value1"].I;
			attaker.jieyin.AddJinDanHP(i);
			if (i < 0)
			{
				attaker.spell.onBuffTickByType(39, new List<int>());
			}
		}

		// Token: 0x0600497B RID: 18811 RVA: 0x001F3102 File Offset: 0x001F1302
		public void realizeSeid146(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			attaker.ZhuJiJinDu += this.getSeidJson(seid)["value1"].I;
		}

		// Token: 0x0600497C RID: 18812 RVA: 0x001F3128 File Offset: 0x001F1328
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

		// Token: 0x0600497D RID: 18813 RVA: 0x001F31A0 File Offset: 0x001F13A0
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

		// Token: 0x0600497E RID: 18814 RVA: 0x001F3218 File Offset: 0x001F1418
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

		// Token: 0x0600497F RID: 18815 RVA: 0x001F32E7 File Offset: 0x001F14E7
		public void realizeSeid151(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			this.realizeSeid150(seid, damage, attaker, receiver, type);
		}

		// Token: 0x06004980 RID: 18816 RVA: 0x001F32F8 File Offset: 0x001F14F8
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

		// Token: 0x06004981 RID: 18817 RVA: 0x001F3358 File Offset: 0x001F1558
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

		// Token: 0x06004982 RID: 18818 RVA: 0x00004095 File Offset: 0x00002295
		public void realizeSeid154(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
		}

		// Token: 0x06004983 RID: 18819 RVA: 0x001F33D0 File Offset: 0x001F15D0
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

		// Token: 0x06004984 RID: 18820 RVA: 0x001F3484 File Offset: 0x001F1684
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

		// Token: 0x06004985 RID: 18821 RVA: 0x001F3538 File Offset: 0x001F1738
		public void realizeSeid157(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if (this.skillSameCast.Count > 0 && this.nowSkillUseCard.Count != 1)
			{
				damage[2] = 1;
			}
		}

		// Token: 0x06004986 RID: 18822 RVA: 0x001F3560 File Offset: 0x001F1760
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

		// Token: 0x06004987 RID: 18823 RVA: 0x001F3688 File Offset: 0x001F1888
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

		// Token: 0x06004988 RID: 18824 RVA: 0x001F36D3 File Offset: 0x001F18D3
		public void realizeSeid160(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if (attaker.OtherAvatar.buffmag.HasBuff(this.getSeidJson(seid)["value1"].I))
			{
				damage[2] = 1;
			}
		}

		// Token: 0x06004989 RID: 18825 RVA: 0x001F3705 File Offset: 0x001F1905
		public void realizeSeid161(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if (attaker.dunSu <= attaker.OtherAvatar.dunSu)
			{
				damage[2] = 1;
			}
		}

		// Token: 0x0600498A RID: 18826 RVA: 0x001F3724 File Offset: 0x001F1924
		public void realizeSeid162(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			Avatar targetAvatar = this.getTargetAvatar(seid, attaker);
			JSONObject seidJson = this.getSeidJson(seid);
			int num = this.RemoveBuff(targetAvatar, seidJson["value1"].I);
			receiver.spell.addBuff(seidJson["value2"].I, num);
		}

		// Token: 0x0600498B RID: 18827 RVA: 0x001F3778 File Offset: 0x001F1978
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

		// Token: 0x0600498C RID: 18828 RVA: 0x001F37F2 File Offset: 0x001F19F2
		public void realizeSeid165(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if (PlayerEx.GetGameDifficulty() == 游戏难度.极简)
			{
				attaker.ZhuJiJinDu += this.getSeidJson(seid)["value1"].I;
			}
		}

		// Token: 0x0600498D RID: 18829 RVA: 0x001F3820 File Offset: 0x001F1A20
		public void realizeSeid166(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			List<ITEM_INFO> list = new List<ITEM_INFO>();
			List<ITEM_INFO> list2 = new List<ITEM_INFO>();
			int count = attaker.itemList.values.Count;
			for (int i = 0; i < count; i++)
			{
				ITEM_INFO item_INFO = attaker.itemList.values[i];
				if (_ItemJsonData.DataDict.ContainsKey(item_INFO.itemId))
				{
					_ItemJsonData itemJsonData = _ItemJsonData.DataDict[item_INFO.itemId];
					if (itemJsonData.type >= 0 && itemJsonData.type <= 2)
					{
						list.Add(item_INFO);
					}
				}
			}
			count = attaker.equipItemList.values.Count;
			for (int j = 0; j < count; j++)
			{
				ITEM_INFO item_INFO2 = attaker.equipItemList.values[j];
				if (_ItemJsonData.DataDict.ContainsKey(item_INFO2.itemId))
				{
					_ItemJsonData itemJsonData2 = _ItemJsonData.DataDict[item_INFO2.itemId];
					if (itemJsonData2.type >= 0 && itemJsonData2.type <= 2)
					{
						list.Add(item_INFO2);
						list2.Add(item_INFO2);
					}
				}
			}
			if (list.Count <= 0)
			{
				Debug.Log("Seid166:没有法宝用于销毁");
				return;
			}
			ITEM_INFO randomOne = list.GetRandomOne<ITEM_INFO>();
			if (randomOne.itemId >= 18001 && randomOne.itemId <= 18010)
			{
				if (randomOne.Seid != null)
				{
					int key = 1;
					int num = 1;
					if (randomOne.Seid.HasField("quality"))
					{
						key = randomOne.Seid["quality"].I;
					}
					if (randomOne.Seid.HasField("QPingZhi"))
					{
						num = randomOne.Seid["QPingZhi"].I;
					}
					int num2 = Skill.seid166Dict[key][num - 1];
					attaker.spell.addBuff(5, num2);
				}
			}
			else if (_ItemJsonData.DataDict.ContainsKey(randomOne.itemId))
			{
				_ItemJsonData itemJsonData3 = _ItemJsonData.DataDict[randomOne.itemId];
				int num3 = Skill.seid166Dict[itemJsonData3.quality][itemJsonData3.typePinJie - 1];
				attaker.spell.addBuff(5, num3);
			}
			if (list2.Contains(randomOne))
			{
				attaker.removeEquipItem(randomOne.uuid);
				return;
			}
			attaker.removeItem(randomOne.uuid);
		}

		// Token: 0x0600498E RID: 18830 RVA: 0x001F3A78 File Offset: 0x001F1C78
		public void realizeSeid170(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int i = this.getSeidJson(seid)["value1"].I;
			int cardNum = receiver.cardMag.getCardNum();
			damage[0] = damage[0] + (int)((float)(i * cardNum) / 100f);
		}

		// Token: 0x0600498F RID: 18831 RVA: 0x001F3AC8 File Offset: 0x001F1CC8
		public void realizeSeid171(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			int xinjin = PlayerEx.Player.xinjin;
			damage[0] = damage[0] + (10000 - 5 * xinjin);
		}

		// Token: 0x06004990 RID: 18832 RVA: 0x001F3AFC File Offset: 0x001F1CFC
		public void realizeSeid172(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if (!receiver.SkillSeidFlag.ContainsKey(13))
			{
				receiver.SkillSeidFlag.Add(13, new Dictionary<int, int>());
			}
			for (int i = 0; i < 5; i++)
			{
				receiver.SkillSeidFlag[13][i] = 20 - receiver.LingGeng[i];
			}
		}

		// Token: 0x06004991 RID: 18833 RVA: 0x00004095 File Offset: 0x00002295
		public void realizeSeid173(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
		}

		// Token: 0x06004992 RID: 18834 RVA: 0x001F3B5C File Offset: 0x001F1D5C
		public void realizeSeid174(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if (receiver.HP < receiver.HP_Max)
			{
				int i = this.getSeidJson(seid)["value1"].I;
				damage[0] = (int)((float)damage[0] * (1f + (float)i / 100f));
			}
		}

		// Token: 0x06004993 RID: 18835 RVA: 0x001F3BB0 File Offset: 0x001F1DB0
		public void realizeSeid175(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if (TianJieManager.Inst.LeiJieIndex < 9)
			{
				int damage2 = TianJieLeiJieShangHai.DataDict[TianJieManager.Inst.LeiJieIndex + 1].Damage;
				attaker.spell.addBuff(3150, damage2);
				if (TianJieManager.Inst.NowLeiJie == "乾天劫")
				{
					attaker.spell.addBuff(3155, 1);
				}
				TianJieManager.Inst.EffectManager.PlayXuLi();
				TianJieManager.Inst.YiXuLi = true;
			}
		}

		// Token: 0x06004994 RID: 18836 RVA: 0x001F3C3C File Offset: 0x001F1E3C
		public void realizeSeid176(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if (TianJieManager.Inst.LeiJieIndex < 9)
			{
				string nowLeiJie = TianJieManager.Inst.NowLeiJie;
				int leiJieSkillID = TianJieLeiJieType.DataDict[nowLeiJie].SkillId;
				Debug.Log(string.Format("释放雷劫 {0} 技能ID:{1}", nowLeiJie, leiJieSkillID));
				UIFightPanel.Inst.PlayerStatus.NoRefresh = true;
				TianJieEffectManager.Inst.PlayAttack(delegate
				{
					this.createSkill(attaker, Tools.instance.getSkillKeyByID(leiJieSkillID, attaker));
				});
				TianJieManager.Inst.YiXuLi = false;
			}
		}

		// Token: 0x06004995 RID: 18837 RVA: 0x001F3CE8 File Offset: 0x001F1EE8
		public void realizeSeid177(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			if (TianJieManager.Inst.LeiJieIndex < 8)
			{
				int num = TianJieManager.Inst.LeiJieIndex + 1;
				string text = TianJieManager.Inst.LeiJieList[num];
				for (int i = 0; i < 10; i++)
				{
					string text2 = TianJieManager.Inst.RollLeiJie(num);
					if (text2 != text)
					{
						TianJieManager.Inst.LeiJieList[num] = text2;
						Debug.Log(string.Format("Skill特性177:将{0}号雷劫 {1}替换为{2}", num, text, text2));
						return;
					}
				}
			}
		}

		// Token: 0x06004996 RID: 18838 RVA: 0x001F3D6C File Offset: 0x001F1F6C
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

		// Token: 0x04004915 RID: 18709
		public string skill_Name;

		// Token: 0x04004916 RID: 18710
		public int skill_ID = -1;

		// Token: 0x04004917 RID: 18711
		public int SkillID;

		// Token: 0x04004918 RID: 18712
		public int Skill_Lv;

		// Token: 0x04004919 RID: 18713
		public string skill_Desc;

		// Token: 0x0400491A RID: 18714
		public Avatar attack;

		// Token: 0x0400491B RID: 18715
		public Avatar target;

		// Token: 0x0400491C RID: 18716
		public int type;

		// Token: 0x0400491D RID: 18717
		private Texture2D _skill_Icon;

		// Token: 0x0400491E RID: 18718
		private Texture2D _SkillPingZhi;

		// Token: 0x0400491F RID: 18719
		private Sprite _skillIconSprite;

		// Token: 0x04004920 RID: 18720
		private Sprite _SkillPingZhiSprite;

		// Token: 0x04004921 RID: 18721
		private Sprite _newSkillPingZhiSprite;

		// Token: 0x04004922 RID: 18722
		public int ColorIndex;

		// Token: 0x04004923 RID: 18723
		public int skill_level;

		// Token: 0x04004924 RID: 18724
		public int Max_level;

		// Token: 0x04004925 RID: 18725
		public float CoolDown;

		// Token: 0x04004926 RID: 18726
		public float CurCD;

		// Token: 0x04004927 RID: 18727
		public int Damage;

		// Token: 0x04004928 RID: 18728
		public int SkillQuality;

		// Token: 0x04004929 RID: 18729
		public string weaponuuid = "";

		// Token: 0x0400492A RID: 18730
		public Dictionary<int, int> skillCast = new Dictionary<int, int>();

		// Token: 0x0400492B RID: 18731
		public Dictionary<int, int> skillSameCast = new Dictionary<int, int>();

		// Token: 0x0400492C RID: 18732
		public List<LateDamage> LateDamages = new List<LateDamage>();

		// Token: 0x0400492D RID: 18733
		public List<int> NowSkillSeid = new List<int>();

		// Token: 0x0400492E RID: 18734
		public Dictionary<int, JSONObject> SkillSeidList = new Dictionary<int, JSONObject>();

		// Token: 0x0400492F RID: 18735
		public JSONObject ItemAddSeid;

		// Token: 0x04004930 RID: 18736
		public Dictionary<int, int> nowSkillUseCard = new Dictionary<int, int>();

		// Token: 0x04004931 RID: 18737
		public Dictionary<int, bool> nowSkillIsChuFa = new Dictionary<int, bool>();

		// Token: 0x04004932 RID: 18738
		private static Dictionary<string, MethodInfo> methodDict = new Dictionary<string, MethodInfo>();

		// Token: 0x04004933 RID: 18739
		public bool IsStaticSkill;

		// Token: 0x04004934 RID: 18740
		private bool initedImage;

		// Token: 0x04004935 RID: 18741
		private static Dictionary<int, List<int>> seid166Dict = new Dictionary<int, List<int>>
		{
			{
				1,
				new List<int>
				{
					1780,
					1800,
					1820
				}
			},
			{
				2,
				new List<int>
				{
					1950,
					2000,
					2050
				}
			},
			{
				3,
				new List<int>
				{
					2300,
					2400,
					2500
				}
			},
			{
				4,
				new List<int>
				{
					2800,
					3000,
					3200
				}
			},
			{
				5,
				new List<int>
				{
					3500,
					4000,
					4500
				}
			}
		};

		// Token: 0x04004936 RID: 18742
		private static List<int> seid166BanList = new List<int>
		{
			1,
			117,
			218,
			304,
			314
		};

		// Token: 0x02001580 RID: 5504
		public enum ALLSkill
		{
			// Token: 0x04006F98 RID: 28568
			SKILL34 = 34,
			// Token: 0x04006F99 RID: 28569
			SKILL65 = 65,
			// Token: 0x04006F9A RID: 28570
			SKILL66,
			// Token: 0x04006F9B RID: 28571
			SKILL68 = 68,
			// Token: 0x04006F9C RID: 28572
			SKILL69,
			// Token: 0x04006F9D RID: 28573
			SKILL97 = 97
		}
	}
}
