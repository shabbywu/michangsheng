using System;
using System.Collections.Generic;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;

namespace Bag
{
	// Token: 0x02000D33 RID: 3379
	[Serializable]
	public class MiJiItem : BaseItem
	{
		// Token: 0x06005046 RID: 20550 RVA: 0x00219148 File Offset: 0x00217348
		public override void SetItem(int id, int count)
		{
			base.SetItem(id, count);
			this.MiJiType = this.GetMiJiType();
			this.CanUse = _ItemJsonData.DataDict[id].CanUse;
			this.PinJie = _ItemJsonData.DataDict[id].typePinJie;
		}

		// Token: 0x06005047 RID: 20551 RVA: 0x00039DE1 File Offset: 0x00037FE1
		public override string GetQualityName()
		{
			if (this.MiJiType == MiJiType.功法 || this.MiJiType == MiJiType.技能)
			{
				return this.GetPinJie() + this.GetPinJieName();
			}
			return base.GetQualityName();
		}

		// Token: 0x06005048 RID: 20552 RVA: 0x00219198 File Offset: 0x00217398
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
				if (this.Id > 100000)
				{
					id = ItemsSeidJsonData2.DataDict[this.Id - 100000].value1;
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
				if (this.Id > 100000)
				{
					id = ItemsSeidJsonData1.DataDict[this.Id - 100000].value1;
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

		// Token: 0x06005049 RID: 20553 RVA: 0x0021931C File Offset: 0x0021751C
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

		// Token: 0x0600504A RID: 20554 RVA: 0x002194B0 File Offset: 0x002176B0
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

		// Token: 0x0600504B RID: 20555 RVA: 0x00219658 File Offset: 0x00217858
		public List<SkillCost> GetMiJiCost()
		{
			int id = int.Parse(_ItemJsonData.DataDict[this.Id].desc.Replace(".0", ""));
			ActiveSkill activeSkill = new ActiveSkill();
			activeSkill.SetSkill(id, Tools.instance.getPlayer().getLevelType());
			return activeSkill.GetSkillCost();
		}

		// Token: 0x0600504C RID: 20556 RVA: 0x002182A8 File Offset: 0x002164A8
		public override Sprite GetQualitySprite()
		{
			return BagMag.Inst.QualityDict[this.GetImgQuality().ToString()];
		}

		// Token: 0x0600504D RID: 20557 RVA: 0x002182D4 File Offset: 0x002164D4
		public override Sprite GetQualityUpSprite()
		{
			return BagMag.Inst.QualityUpDict[this.GetImgQuality().ToString()];
		}

		// Token: 0x0600504E RID: 20558 RVA: 0x002196B0 File Offset: 0x002178B0
		public override int GetImgQuality()
		{
			int num = this.Quality;
			if (this.MiJiType == MiJiType.功法 || this.MiJiType == MiJiType.技能)
			{
				num *= 2;
			}
			return num;
		}

		// Token: 0x0600504F RID: 20559 RVA: 0x002196DC File Offset: 0x002178DC
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

		// Token: 0x06005050 RID: 20560 RVA: 0x00219728 File Offset: 0x00217928
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

		// Token: 0x06005051 RID: 20561 RVA: 0x0021976C File Offset: 0x0021796C
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

		// Token: 0x06005052 RID: 20562 RVA: 0x002197B0 File Offset: 0x002179B0
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

		// Token: 0x040051B3 RID: 20915
		public MiJiType MiJiType;

		// Token: 0x040051B4 RID: 20916
		public int CanUse;

		// Token: 0x040051B5 RID: 20917
		public new int PinJie;
	}
}
