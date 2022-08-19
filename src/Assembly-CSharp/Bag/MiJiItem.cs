using System;
using System.Collections.Generic;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;

namespace Bag
{
	// Token: 0x020009AD RID: 2477
	[Serializable]
	public class MiJiItem : BaseItem
	{
		// Token: 0x060044E7 RID: 17639 RVA: 0x001D4AD8 File Offset: 0x001D2CD8
		public override void SetItem(int id, int count)
		{
			base.SetItem(id, count);
			this.MiJiType = this.GetMiJiType();
			this.CanUse = _ItemJsonData.DataDict[id].CanUse;
			this.PinJie = _ItemJsonData.DataDict[id].typePinJie;
		}

		// Token: 0x060044E8 RID: 17640 RVA: 0x001D4B25 File Offset: 0x001D2D25
		public override string GetQualityName()
		{
			if (this.MiJiType == MiJiType.功法 || this.MiJiType == MiJiType.技能)
			{
				return this.GetPinJie() + this.GetPinJieName();
			}
			return base.GetQualityName();
		}

		// Token: 0x060044E9 RID: 17641 RVA: 0x001D4B54 File Offset: 0x001D2D54
		public override JiaoBiaoType GetJiaoBiaoType()
		{
			if (Tools.getJsonobject(Tools.instance.getPlayer().NaiYaoXin, string.Concat(this.Id)) >= this.CanUse && this.MiJiType == MiJiType.书籍2)
			{
				return JiaoBiaoType.悟;
			}
			if (this.MiJiType == MiJiType.丹方)
			{
				if (Tools.instance.getPlayer().ISStudyDanFan(ItemsSeidJsonData13.DataDict[this.Id].value1))
				{
					return JiaoBiaoType.悟;
				}
			}
			else if (this.MiJiType == MiJiType.功法)
			{
				int id = 0;
				if (this.Id > jsonData.QingJiaoItemIDSegment)
				{
					id = ItemsSeidJsonData2.DataDict[this.Id - jsonData.QingJiaoItemIDSegment].value1;
				}
				else
				{
					id = ItemsSeidJsonData2.DataDict[this.Id].value1;
				}
				if (PlayerEx.Player.hasStaticSkillList.Find((SkillItem s) => s.itemId == id) != null)
				{
					return JiaoBiaoType.悟;
				}
			}
			else if (this.MiJiType == MiJiType.技能)
			{
				int id = 0;
				if (this.Id > jsonData.QingJiaoItemIDSegment)
				{
					id = ItemsSeidJsonData1.DataDict[this.Id - jsonData.QingJiaoItemIDSegment].value1;
				}
				else
				{
					id = ItemsSeidJsonData1.DataDict[this.Id].value1;
				}
				if (PlayerEx.Player.hasSkillList.Find((SkillItem s) => s.itemId == id) != null)
				{
					return JiaoBiaoType.悟;
				}
			}
			return base.GetJiaoBiaoType();
		}

		// Token: 0x060044EA RID: 17642 RVA: 0x001D4CD8 File Offset: 0x001D2ED8
		public override string GetDesc1()
		{
			if (this.MiJiType == MiJiType.功法)
			{
				int num;
				if (int.TryParse(_ItemJsonData.DataDict[this.Id].desc.Replace(".0", ""), out num))
				{
					using (List<StaticSkillJsonData>.Enumerator enumerator = StaticSkillJsonData.DataList.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							StaticSkillJsonData staticSkillJsonData = enumerator.Current;
							if (staticSkillJsonData.Skill_ID == num)
							{
								return staticSkillJsonData.descr;
							}
						}
						goto IL_162;
					}
				}
				string text = string.Format("获取描述异常，id为{0}的功法书无法将描述转换为功法ID，请检查配表", this.Id);
				Debug.LogError(text);
				return text;
			}
			if (this.MiJiType == MiJiType.技能)
			{
				int num;
				if (int.TryParse(_ItemJsonData.DataDict[this.Id].desc.Replace(".0", ""), out num))
				{
					using (List<_skillJsonData>.Enumerator enumerator2 = _skillJsonData.DataList.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							_skillJsonData skillJsonData = enumerator2.Current;
							if (skillJsonData.Skill_ID == num && skillJsonData.Skill_Lv == Tools.instance.getPlayer().getLevelType())
							{
								return skillJsonData.descr.Replace("（attack）", skillJsonData.HP.ToString());
							}
						}
						goto IL_162;
					}
				}
				string text2 = string.Format("获取描述异常，id为{0}的技能书无法将描述转换为技能ID，请检查配表", this.Id);
				Debug.LogError(text2);
				return text2;
			}
			IL_162:
			return base.GetDesc1();
		}

		// Token: 0x060044EB RID: 17643 RVA: 0x001D4E6C File Offset: 0x001D306C
		public override List<int> GetCiZhui()
		{
			new List<int>();
			if (this.MiJiType == MiJiType.功法)
			{
				int num;
				if (int.TryParse(_ItemJsonData.DataDict[this.Id].desc.Replace(".0", ""), out num))
				{
					using (List<StaticSkillJsonData>.Enumerator enumerator = StaticSkillJsonData.DataList.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							StaticSkillJsonData staticSkillJsonData = enumerator.Current;
							if (staticSkillJsonData.Skill_ID == num && staticSkillJsonData.Skill_Lv == Tools.instance.getPlayer().getLevelType())
							{
								return new List<int>(staticSkillJsonData.Affix);
							}
						}
						goto IL_174;
					}
				}
				Debug.LogError(string.Format("获取词缀异常，id为{0}的功法书无法将描述转换为功法ID，请检查配表", this.Id));
			}
			else if (this.MiJiType == MiJiType.技能)
			{
				int num;
				if (int.TryParse(_ItemJsonData.DataDict[this.Id].desc.Replace(".0", ""), out num))
				{
					using (List<_skillJsonData>.Enumerator enumerator2 = _skillJsonData.DataList.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							_skillJsonData skillJsonData = enumerator2.Current;
							if (skillJsonData.Skill_ID == num && skillJsonData.Skill_Lv == Tools.instance.getPlayer().getLevelType())
							{
								return new List<int>(skillJsonData.Affix2);
							}
						}
						goto IL_174;
					}
				}
				Debug.LogError(string.Format("获取词缀异常，id为{0}的技能书无法将描述转换为技能ID，请检查配表", this.Id));
			}
			IL_174:
			return base.GetCiZhui();
		}

		// Token: 0x060044EC RID: 17644 RVA: 0x001D5014 File Offset: 0x001D3214
		public List<SkillCost> GetMiJiCost()
		{
			int id = int.Parse(_ItemJsonData.DataDict[this.Id].desc.Replace(".0", ""));
			ActiveSkill activeSkill = new ActiveSkill();
			activeSkill.SetSkill(id, Tools.instance.getPlayer().getLevelType());
			return activeSkill.GetSkillCost();
		}

		// Token: 0x060044ED RID: 17645 RVA: 0x001D506C File Offset: 0x001D326C
		public override Sprite GetQualitySprite()
		{
			return BagMag.Inst.QualityDict[this.GetImgQuality().ToString()];
		}

		// Token: 0x060044EE RID: 17646 RVA: 0x001D5098 File Offset: 0x001D3298
		public override Sprite GetQualityUpSprite()
		{
			return BagMag.Inst.QualityUpDict[this.GetImgQuality().ToString()];
		}

		// Token: 0x060044EF RID: 17647 RVA: 0x001D50C4 File Offset: 0x001D32C4
		public override int GetImgQuality()
		{
			int num = this.Quality;
			if (this.MiJiType == MiJiType.功法 || this.MiJiType == MiJiType.技能)
			{
				num *= 2;
			}
			return num;
		}

		// Token: 0x060044F0 RID: 17648 RVA: 0x001D50F0 File Offset: 0x001D32F0
		public MiJiType GetMiJiType()
		{
			MiJiType result = MiJiType.技能;
			int type = this.Type;
			if (type != 3)
			{
				if (type != 4)
				{
					switch (type)
					{
					case 10:
						result = MiJiType.丹方;
						break;
					case 12:
						result = MiJiType.书籍1;
						break;
					case 13:
						result = MiJiType.书籍2;
						break;
					}
				}
				else
				{
					result = MiJiType.功法;
				}
			}
			else
			{
				result = MiJiType.技能;
			}
			return result;
		}

		// Token: 0x060044F1 RID: 17649 RVA: 0x001D513C File Offset: 0x001D333C
		private string GetPinJie()
		{
			switch (this.Quality)
			{
			case 1:
				return "人阶";
			case 2:
				return "地阶";
			case 3:
				return "天阶";
			default:
				return "无";
			}
		}

		// Token: 0x060044F2 RID: 17650 RVA: 0x001D5180 File Offset: 0x001D3380
		public string GetPinJieName()
		{
			switch (this.PinJie)
			{
			case 1:
				return "下";
			case 2:
				return "中";
			case 3:
				return "上";
			default:
				return "无";
			}
		}

		// Token: 0x060044F3 RID: 17651 RVA: 0x001D51C4 File Offset: 0x001D33C4
		public override void Use()
		{
			if (this.MiJiType == MiJiType.书籍1 || this.MiJiType == MiJiType.丹方)
			{
				new item(this.Id).gongneng(delegate
				{
					Tools.instance.getPlayer().removeItem(this.Id, 1);
					MessageMag.Instance.Send(MessageName.MSG_PLAYER_USE_ITEM, null);
				}, false);
				return;
			}
			UIPopTip.Inst.Pop("需在闭关时领悟", PopTipIconType.叹号);
		}

		// Token: 0x040046B3 RID: 18099
		public MiJiType MiJiType;

		// Token: 0x040046B4 RID: 18100
		public int CanUse;

		// Token: 0x040046B5 RID: 18101
		public new int PinJie;
	}
}
